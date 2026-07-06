namespace Expensify.API.DTOs.AuthDTOs
{
    public class LoginUserResponseDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
