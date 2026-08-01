using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

namespace Expensify.API.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
