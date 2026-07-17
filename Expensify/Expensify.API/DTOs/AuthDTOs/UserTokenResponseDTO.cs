namespace Expensify.API.DTOs.AuthDTOs;

public class UserTokenResponseDTO
{
    public required UserResponseDTO User { get; set; }
    public required string Token { get; set; }
}
