using Expensify.Entities.Models;

namespace Expensify.API.ServiceClasses.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
