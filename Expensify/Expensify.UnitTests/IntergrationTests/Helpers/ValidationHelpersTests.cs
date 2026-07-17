using Expensify.API.Utility.Validators;

namespace Expensify.UnitTests.IntergrationTests.Helpers;

public class ValidationHelpersTests
{
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
}
