using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Microsoft.EntityFrameworkCore;
using static Expensify.API.Authentication.Handlers.DevelopmentAuthenticationHandler;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.API.Development;

public static class DevelopmentDataSeeder
{
    public static async Task SeedDevelopmentDataAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var userExists = await context.Users.AnyAsync(user => user.Id == DevelopmentUsers.UserId);

        if (userExists)
        {
            return;
        }

        var user = new User
        {
            Id = DevelopmentUsers.UserId,
            FullName = "Developer",
            Email = DevelopmentUsers.Email,
            RoleId = RoleIds.Admin,
            Password = string.Empty,
            IsEmailVerified = true,
        };

        context.Users.Add(user);

        await context.SaveChangesAsync();
    }
}
