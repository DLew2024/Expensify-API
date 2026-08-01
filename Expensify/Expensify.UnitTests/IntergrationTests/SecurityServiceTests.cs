using Expensify.API.Services;

namespace Expensify.UnitTests.IntergrationTests;

public class SecurityServiceTests
{
    private readonly SecurityService _securityService = new();

    [Fact]
    public void GenerateSecureToken_WhenCalled_ReturnsToken()
    {
        // Act
        var token = _securityService.GenerateSecureToken();

        // Assert
        token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GenerateSecureToken_WhenUsingDefaultLength_Returns64Bytes()
    {
        // Act
        var token = _securityService.GenerateSecureToken();

        var tokenBytes = Convert.FromBase64String(token);

        // Assert
        tokenBytes.Should().HaveCount(64);
    }

    [Theory]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(64)]
    [InlineData(128)]
    public void GenerateSecureToken_WhenLengthIsProvided_ReturnsRequestedNumberOfBytes(
        int byteLength
    )
    {
        // Act
        var token = _securityService.GenerateSecureToken(byteLength);

        var tokenBytes = Convert.FromBase64String(token);

        // Assert
        tokenBytes.Should().HaveCount(byteLength);
    }

    [Fact]
    public void GenerateSecureToken_WhenCalledTwice_ReturnsDifferentTokens()
    {
        // Act
        var firstToken = _securityService.GenerateSecureToken();
        var secondToken = _securityService.GenerateSecureToken();

        // Assert
        firstToken.Should().NotBe(secondToken);
    }

    [Fact]
    public void HashToken_WhenValueIsProvided_ReturnsHash()
    {
        // Arrange
        const string token = "example-token";

        // Act
        var hash = _securityService.HashToken(token);

        // Assert
        hash.Should().NotBeNullOrWhiteSpace();
        hash.Should().NotBe(token);
    }

    [Fact]
    public void HashToken_WhenSameValueIsHashedTwice_ReturnsSameHash()
    {
        // Arrange
        const string token = "example-token";

        // Act
        var firstHash = _securityService.HashToken(token);
        var secondHash = _securityService.HashToken(token);

        // Assert
        firstHash.Should().Be(secondHash);
    }

    [Fact]
    public void HashToken_WhenValuesAreDifferent_ReturnsDifferentHashes()
    {
        // Act
        var firstHash = _securityService.HashToken("first-token");
        var secondHash = _securityService.HashToken("second-token");

        // Assert
        firstHash.Should().NotBe(secondHash);
    }

    [Fact]
    public void HashToken_WhenValueIsProvided_Returns32ByteHash()
    {
        // Arrange
        const string token = "example-token";

        // Act
        var hash = _securityService.HashToken(token);

        var hashBytes = Convert.FromBase64String(hash);

        // Assert
        hashBytes.Should().HaveCount(32);
    }
}
