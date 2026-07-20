using System.ComponentModel.DataAnnotations;
using Expensify.API.Configurations;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServiceClasses;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace Expensify.UnitTests.IntergrationTests;

public class AuthServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;

    private readonly Mock<IJwtService> _jwtServiceMock = new();
    private readonly Mock<IPasswordService> _passwordServiceMock = new();
    private readonly Mock<ISecurityService> _securityServiceMock = new();
    private readonly Mock<IEmailService> _emailServiceMock = new();

    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        var databaseOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(warnings =>
                warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning)
            )
            .Options;

        _context = new ApplicationDbContext(databaseOptions);

        var frontendSettings = Options.Create(
            new FrontendSettings { BaseUrl = "https://localhost:5173" }
        );

        var jwtSettings = Options.Create(
            new JwtSettings
            {
                Key = "test-secret-key-that-is-long-enough-for-testing",
                Issuer = "Expensify.Tests",
                Audience = "Expensify.Tests",
                ExpirationMinutes = 15,
                RefreshTokenExpirationDays = 30,
            }
        );

        _authService = new AuthService(
            _context,
            _jwtServiceMock.Object,
            _passwordServiceMock.Object,
            _securityServiceMock.Object,
            _emailServiceMock.Object,
            frontendSettings,
            jwtSettings
        );
    }

    [Fact]
    public async Task RegisterUser_WhenRequiredFieldIsMissing_ReturnsValidationException()
    {
        // Arrange
        var request = new RegisterUserDTO
        {
            FirstName = string.Empty,
            LastName = "Lewis",
            Email = "darius@example.com",
            Password = "Password123!",
        };

        // Act
        var result = await _authService.RegisterUser(request, CancellationToken.None);

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<ValidationException>();
        exception.Message.Should().Be("All required fields must be provided.");

        _context.Users.Should().BeEmpty();

        _passwordServiceMock.Verify(
            service => service.HashPassword(It.IsAny<string>()),
            Times.Never
        );

        _jwtServiceMock.Verify(service => service.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("user@")]
    [InlineData("@example.com")]
    public async Task RegisterUser_WhenEmailIsInvalid_ReturnsValidationException(string email)
    {
        // Arrange
        var request = new RegisterUserDTO
        {
            FirstName = "Darius",
            LastName = "Lewis",
            Email = email,
            Password = "Password123!",
        };

        // Act
        var result = await _authService.RegisterUser(request, CancellationToken.None);

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<ValidationException>();
        exception.Message.Should().Be("Passed email is not in the correct format.");

        _context.Users.Should().BeEmpty();
    }

    [Fact]
    public async Task RegisterUser_WhenEmailAlreadyExists_ReturnsConflictException()
    {
        // Arrange
        _context.Users.Add(
            new User
            {
                Id = Guid.NewGuid(),
                FullName = "Existing User",
                Email = "existing@example.com",
                Password = "existing-password-hash",
            }
        );

        await _context.SaveChangesAsync();

        var request = new RegisterUserDTO
        {
            FirstName = "Another",
            LastName = "User",
            Email = " Existing@Example.com ",
            Password = "Password123!",
        };

        // Act
        var result = await _authService.RegisterUser(request, CancellationToken.None);

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<ConflictException>();
        exception.Message.Should().Be("A user with this email already exists.");

        _context.Users.Should().ContainSingle();

        _passwordServiceMock.Verify(
            service => service.HashPassword(It.IsAny<string>()),
            Times.Never
        );
    }

    [Fact]
    public async Task RegisterUser_WhenRequestIsValid_CreatesUserAndReturnsToken()
    {
        // Arrange
        const string rawPassword = "Password123!";
        const string passwordHash = "generated-password-hash";
        const string accessToken = "generated-jwt-token";

        _passwordServiceMock
            .Setup(service => service.HashPassword(rawPassword))
            .Returns(passwordHash);

        _jwtServiceMock
            .Setup(service => service.GenerateToken(It.IsAny<User>()))
            .Returns(accessToken);

        var request = new RegisterUserDTO
        {
            FirstName = " Darius ",
            LastName = " Lewis ",
            Email = " DARIUS@EXAMPLE.COM ",
            Password = rawPassword,
            ProfileImageURl = "https://example.com/profile.png",
        };

        // Act
        var result = await _authService.RegisterUser(request, CancellationToken.None);

        // Assert
        var response = GetSuccess(result);

        response.Token.Should().Be(accessToken);
        response.User.Email.Should().Be("darius@example.com");
        response.User.FullName.Should().Be("Darius Lewis");

        var createdUser = await _context.Users.SingleAsync();

        createdUser.FullName.Should().Be("Darius Lewis");
        createdUser.Email.Should().Be("darius@example.com");
        createdUser.Password.Should().Be(passwordHash);
        createdUser.ProfileImageUrl.Should().Be("https://example.com/profile.png");

        _passwordServiceMock.Verify(service => service.HashPassword(rawPassword), Times.Once);

        _jwtServiceMock.Verify(
            service =>
                service.GenerateToken(
                    It.Is<User>(user =>
                        user.Email == "darius@example.com" && user.FullName == "Darius Lewis"
                    )
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task LoginUser_WhenUserDoesNotExist_ReturnsUnauthorizedAccessException()
    {
        // Arrange
        var request = new LoginUserDTO { Email = "missing@example.com", Password = "Password123!" };

        // Act
        var result = await _authService.LoginUser(request, CancellationToken.None);

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<UnauthorizedAccessException>();
        exception.Message.Should().Be("Invalid email or password.");

        _passwordServiceMock.Verify(
            service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never
        );

        _jwtServiceMock.Verify(service => service.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginUser_WhenPasswordIsIncorrect_ReturnsUnauthorizedAccessException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Darius Lewis",
            Email = "darius@example.com",
            Password = "stored-password-hash",
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _passwordServiceMock
            .Setup(service => service.VerifyPassword("WrongPassword!", "stored-password-hash"))
            .Returns(false);

        var request = new LoginUserDTO
        {
            Email = "darius@example.com",
            Password = "WrongPassword!",
        };

        // Act
        var result = await _authService.LoginUser(request, CancellationToken.None);

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<UnauthorizedAccessException>();
        exception.Message.Should().Be("Invalid email or password.");

        _jwtServiceMock.Verify(service => service.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginUser_WhenCredentialsAreValid_ReturnsUserAndToken()
    {
        // Arrange
        const string accessToken = "generated-access-token";

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Darius Lewis",
            Email = "darius@example.com",
            Password = "stored-password-hash",
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _passwordServiceMock
            .Setup(service => service.VerifyPassword("Password123!", "stored-password-hash"))
            .Returns(true);

        _jwtServiceMock
            .Setup(service => service.GenerateToken(It.IsAny<User>()))
            .Returns(accessToken);

        var request = new LoginUserDTO
        {
            Email = " DARIUS@EXAMPLE.COM ",
            Password = "Password123!",
        };

        // Act
        var result = await _authService.LoginUser(request, CancellationToken.None);

        // Assert
        var response = GetSuccess(result);

        response.Token.Should().Be(accessToken);
        response.User.Id.Should().Be(user.Id);
        response.User.Email.Should().Be(user.Email);
        response.User.FullName.Should().Be(user.FullName);

        _passwordServiceMock.Verify(
            service => service.VerifyPassword("Password123!", "stored-password-hash"),
            Times.Once
        );

        _jwtServiceMock.Verify(
            service => service.GenerateToken(It.Is<User>(foundUser => foundUser.Id == user.Id)),
            Times.Once
        );
    }

    [Fact]
    public async Task ChangePassword_WhenPasswordsDoNotMatch_ReturnsValidationException()
    {
        // Arrange
        var request = new ChangePasswordDTO
        {
            CurrentPassword = "CurrentPassword123!",
            NewPassword = "NewPassword123!",
            ConfirmNewPassword = "DifferentPassword123!",
        };

        // Act
        var result = await _authService.ChangePassword(
            Guid.NewGuid(),
            request,
            CancellationToken.None
        );

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<ValidationException>();
        exception.Message.Should().Be("New passwords do not match.");

        _passwordServiceMock.Verify(
            service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never
        );
    }

    [Fact]
    public async Task ChangePassword_WhenCurrentPasswordIsIncorrect_ReturnsValidationException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Darius Lewis",
            Email = "darius@example.com",
            Password = "stored-password-hash",
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _passwordServiceMock
            .Setup(service =>
                service.VerifyPassword("IncorrectCurrentPassword!", "stored-password-hash")
            )
            .Returns(false);

        var request = new ChangePasswordDTO
        {
            CurrentPassword = "IncorrectCurrentPassword!",
            NewPassword = "NewPassword123!",
            ConfirmNewPassword = "NewPassword123!",
        };

        // Act
        var result = await _authService.ChangePassword(user.Id, request, CancellationToken.None);

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<ValidationException>();
        exception.Message.Should().Be("The current password is incorrect.");

        _passwordServiceMock.Verify(
            service => service.HashPassword(It.IsAny<string>()),
            Times.Never
        );
    }

    [Fact]
    public async Task ChangePassword_WhenNewPasswordMatchesCurrentPassword_ReturnsValidationException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Darius Lewis",
            Email = "darius@example.com",
            Password = "stored-password-hash",
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _passwordServiceMock
            .Setup(service => service.VerifyPassword("CurrentPassword123!", "stored-password-hash"))
            .Returns(true);

        var request = new ChangePasswordDTO
        {
            CurrentPassword = "CurrentPassword123!",
            NewPassword = "CurrentPassword123!",
            ConfirmNewPassword = "CurrentPassword123!",
        };

        // Act
        var result = await _authService.ChangePassword(user.Id, request, CancellationToken.None);

        // Assert
        var exception = GetException(result);

        exception.Should().BeOfType<ValidationException>();
        exception
            .Message.Should()
            .Be("The new password must be different from the current password.");

        _passwordServiceMock.Verify(
            service => service.HashPassword(It.IsAny<string>()),
            Times.Never
        );
    }

    [Fact]
    public async Task ChangePassword_WhenRequestIsValid_UpdatesPassword()
    {
        // Arrange
        const string currentPassword = "CurrentPassword123!";
        const string newPassword = "NewPassword123!";
        const string currentPasswordHash = "current-password-hash";
        const string newPasswordHash = "new-password-hash";

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Darius Lewis",
            Email = "darius@example.com",
            Password = currentPasswordHash,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _passwordServiceMock
            .Setup(service => service.VerifyPassword(currentPassword, currentPasswordHash))
            .Returns(true);

        _passwordServiceMock
            .Setup(service => service.VerifyPassword(newPassword, currentPasswordHash))
            .Returns(false);

        _passwordServiceMock
            .Setup(service => service.HashPassword(newPassword))
            .Returns(newPasswordHash);

        var request = new ChangePasswordDTO
        {
            CurrentPassword = currentPassword,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword,
        };

        // Act
        var result = await _authService.ChangePassword(user.Id, request, CancellationToken.None);

        // Assert
        var response = GetSuccess(result);

        response.Should().BeTrue();

        var updatedUser = await _context.Users.FindAsync(user.Id);

        updatedUser.Should().NotBeNull();
        updatedUser!.Password.Should().Be(newPasswordHash);

        _passwordServiceMock.Verify(service => service.HashPassword(newPassword), Times.Once);
    }

    private static T GetSuccess<T>(Result<T> result)
    {
        return result.Match(
            success => success,
            exception =>
                throw new Xunit.Sdk.XunitException(
                    $"Expected a successful result, but received: {exception}"
                )
        );
    }

    private static Exception GetException<T>(Result<T> result)
    {
        return result.Match<Exception>(
            _ =>
                throw new Xunit.Sdk.XunitException(
                    "Expected a failed result, but the operation succeeded."
                ),
            exception => exception
        );
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();

        GC.SuppressFinalize(this);
    }
}
