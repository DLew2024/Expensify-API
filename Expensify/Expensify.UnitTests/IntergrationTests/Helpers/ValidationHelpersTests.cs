using Expensify.API.Utility.Validators;

namespace Expensify.UnitTests.IntergrationTests.Helpers;

public class ValidationHelpersTests
{
    #region HasEmptyOrWhiteSpace
    [Fact]
    public void HasEmptyOrWhiteSpace_WhenValueIsEmpty_ReturnsTrue()
    {
        // Arrange
        var values = new[] { "test@example.com", string.Empty };

        // Act
        var result = ValidationHelpers.HasEmptyOrWhiteSpace(values);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasEmptyOrWhiteSpace_WhenAllValuesAreProvided_ReturnsFalse()
    {
        // Arrange
        var values = new[] { "test@example.com", "Password123!" };

        // Act
        var result = ValidationHelpers.HasEmptyOrWhiteSpace(values);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("     ")]
    [InlineData(null)]
    public void HasEmptyOrWhiteSpace_WhenValueIsInvalid_ReturnsTrue(string? value)
    {
        var result = ValidationHelpers.HasEmptyOrWhiteSpace(value);

        result.Should().BeTrue();
    }
    #endregion


    #region IsValidEmail
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void IsValidEmail_WhenEmailIsEmpty_ReturnsFalse(string? email)
    {
        // Act
        var result = ValidationHelpers.IsValidEmail(email);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("USER@EXAMPLE.COM")]
    public void IsValidEmail_WhenEmailIsValid_ReturnsTrue(string email)
    {
        // Act
        var result = ValidationHelpers.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("@example.com")]
    [InlineData("user@")]
    [InlineData("user.com")]
    public void IsValidEmail_WhenEmailIsInvalid_ReturnsFalse(string email)
    {
        // Act
        var result = ValidationHelpers.IsValidEmail(email);

        // Assert
        result.Should().BeFalse();
    }
    #endregion

    #region Trim
    [Fact]
    public void Trim_WhenValueIsNull_ReturnsEmptyString()
    {
        // Act
        var result = ValidationHelpers.Trim(null);

        // Assert
        result.Should().Be(string.Empty);
    }

    [Theory]
    [InlineData(" test ", "test")]
    [InlineData("   hello", "hello")]
    [InlineData("world   ", "world")]
    public void Trim_WhenValueContainsWhitespace_ReturnsTrimmedValue(string input, string expected)
    {
        // Act
        var result = ValidationHelpers.Trim(input);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("hello")]
    [InlineData("123")]
    public void Trim_WhenValueHasNoWhitespace_ReturnsOriginalValue(string input)
    {
        // Act
        var result = ValidationHelpers.Trim(input);

        // Assert
        result.Should().Be(input);
    }
    #endregion Trim

    [Fact]
    public void Normalize_WhenValueIsNull_ReturnsEmptyString()
    {
        // Act
        var result = ValidationHelpers.Normalize(null);

        // Assert
        result.Should().Be(string.Empty);
    }

    [Theory]
    [InlineData(" TEST ", "test")]
    [InlineData("DARIUS@EXAMPLE.COM", "darius@example.com")]
    [InlineData(" Hello World ", "hello world")]
    public void Normalize_WhenValueIsProvided_ReturnsNormalizedValue(string input, string expected)
    {
        // Act
        var result = ValidationHelpers.Normalize(input);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("hello")]
    [InlineData("test@example.com")]
    public void Normalize_WhenValueIsAlreadyNormalized_ReturnsOriginalValue(string input)
    {
        // Act
        var result = ValidationHelpers.Normalize(input);

        // Assert
        result.Should().Be(input);
    }
}
