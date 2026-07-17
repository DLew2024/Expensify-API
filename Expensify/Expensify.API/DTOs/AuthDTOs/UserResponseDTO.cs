using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models;

namespace Expensify.API.DTOs.AuthDTOs;

public class UserResponseDTO
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string ProfileImageUrl { get; init; } = string.Empty;

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
            FullName = user.FullName,
            Email = user.Email,
            ProfileImageUrl = user.ProfileImageUrl,
        };
}
