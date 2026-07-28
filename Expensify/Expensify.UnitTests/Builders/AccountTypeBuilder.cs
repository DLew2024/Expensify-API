using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

namespace Expensify.UnitTests.Builders;

public sealed class AccountTypeBuilder
{
    private Guid _id = Guid.NewGuid();

    private User? _user;

    private string _name = "Test Checking Account";
    private bool _isActive = true;
    private bool _isDeleted = false;
    private bool _isSystemDefault = false;

    public AccountTypeBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public AccountTypeBuilder WithUser(User user)
    {
        _user = user;
        return this;
    }

    public AccountTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public AccountTypeBuilder WithActiveStatus(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public AccountTypeBuilder WithDeletedStatus(bool isDeleted)
    {
        _isDeleted = isDeleted;
        return this;
    }

    public AccountTypeBuilder WithSystemDefault(bool isDefault)
    {
        _isSystemDefault = isDefault;
        return this;
    }

    public AccountType Build()
    {
        if (_user is null)
        {
            throw new InvalidOperationException(
                "A user must be provided before building an account."
            );
        }

        return new AccountType
        {
            Id = _id,

            UserId = _user.Id,
            User = _user,

            Name = _name,

            IsActive = _isActive,
            IsDeleted = _isDeleted,
            IsSystemDefault = _isSystemDefault,

            CreatedBy = _user.Id,
            LastUpdatedBy = _user.Id,
        };
    }
}
