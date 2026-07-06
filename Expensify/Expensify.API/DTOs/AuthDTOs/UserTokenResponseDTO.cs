namespace Expensify.API.DTOs.AuthDTOs
{
    public class UserTokenResponseDTO
    {
        public Guid UserId { get; set; }
        public required string Token { get; set; }
    }
}
