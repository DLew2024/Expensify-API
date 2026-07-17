namespace Expensify.API.DTOs.AuthDTOs;

// Add DTO Validator
public class RegisterUserDTO
{
    public required string FirstName { get; init; } 

    public required string LastName { get; init; } 

    public required string Email { get; init; } 

    public required string Password { get; init; }
    public string ProfileImageURl { get; init; } = string.Empty;
}
