using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

namespace Expensify.API.ServiceClasses.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
