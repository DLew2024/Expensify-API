using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

namespace Expensify.API.DTOs.AuthDTOs;

public class UserResponseDTO
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string ProfileImageUrl { get; init; }

    public static UserResponseDTO FromUser(User user)
    {
        return new UserResponseDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            ProfileImageUrl = user.ProfileImageUrl,
        };
    }

    public static Expression<Func<User, UserResponseDTO>> Projection =>
        user => new UserResponseDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            ProfileImageUrl = user.ProfileImageUrl,
        };
}
