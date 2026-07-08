namespace Expensify.API.DTOs.AuthDTOs;

public class RegisterUserDTO
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string ProfileImageURl { get; set; } = string.Empty;
}
