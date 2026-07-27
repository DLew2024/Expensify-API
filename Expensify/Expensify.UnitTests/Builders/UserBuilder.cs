using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

namespace Expensify.UnitTests.Builders;

public sealed class UserBuilder
{
    private Guid _id = Guid.NewGuid();

    private Role? _role;

    private string _fullName = "Test User";
    private string _email = $"test-{Guid.NewGuid()}@test.com";
    private string _password = "TestPassword123!";

    public UserBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UserBuilder WithRole(Role role)
    {
        _role = role;
        return this;
    }

    public UserBuilder WithFullName(string fullName)
    {
        _fullName = fullName;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public User Build()
    {
        if (_role is null)
        {
            throw new InvalidOperationException("A role must be provided before building a user.");
        }

        return new User
        {
            Id = _id,
            FullName = _fullName,
            Email = _email,
            Password = _password,
            RoleId = _role.Id,
            Role = _role,
        };
    }
}
