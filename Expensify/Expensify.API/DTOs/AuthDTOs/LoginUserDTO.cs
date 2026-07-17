namespace Expensify.API.DTOs.AuthDTOs;

public class LoginUserDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
