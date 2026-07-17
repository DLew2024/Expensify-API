using Expensify.API.ServiceClasses;

namespace Expensify.UnitTests.IntergrationTests;

public class PasswordServiceTests
{
    private readonly PasswordService _passwordService = new();

    [Fact]
    public void HashPassword_WhenPasswordIsProvided_ReturnsHashedPassword()
    {
        // Arrange
        const string password = "Password123!";

        // Act
        var result = _passwordService.HashPassword(password);

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().NotBe(password);
    }

    [Fact]
    public void HashPassword_WhenCalledTwice_ReturnsDifferentHashes()
    {
        // Arrange
        const string password = "Password123!";

        // Act
        var firstHash = _passwordService.HashPassword(password);
        var secondHash = _passwordService.HashPassword(password);

        // Assert
        firstHash.Should().NotBe(secondHash);
    }

    [Fact]
    public void VerifyPassword_WhenPasswordMatches_ReturnsTrue()
    {
        // Arrange
        const string password = "Password123!";
        var storedPasswordHash = _passwordService.HashPassword(password);

        // Act
        var result = _passwordService.VerifyPassword(password, storedPasswordHash);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_WhenPasswordDoesNotMatch_ReturnsFalse()
    {
        // Arrange
        const string correctPassword = "Password123!";
        const string incorrectPassword = "WrongPassword123!";

        var storedPasswordHash = _passwordService.HashPassword(correctPassword);

        // Act
        var result = _passwordService.VerifyPassword(
            incorrectPassword,
            storedPasswordHash
        );

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("Password123!")]
    [InlineData("AnotherSecurePassword456$")]
    [InlineData("short")]
    [InlineData("password with spaces")]
    public void HashAndVerifyPassword_WhenPasswordIsValid_ReturnsTrue(string password)
    {
        // Act
        var storedPasswordHash = _passwordService.HashPassword(password);

        var result = _passwordService.VerifyPassword(
            password,
            storedPasswordHash
        );

        // Assert
        result.Should().BeTrue();
    }
}
