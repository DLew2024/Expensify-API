using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;

namespace Expensify.UnitTests.Builders;

public sealed class AccountBuilder
{
    private Guid _id = Guid.NewGuid();

    private User? _user;
    private AccountType? _accountType;
    private CurrencyCode? _currency;

    private string _name = "Test User Account";
    private string _institutionName = "Test Bank";
    private readonly string _lastFourDigits = "1234";

    private decimal _currentBalance = 1_000m;
    private decimal _availableBalance = 1_000m;

    private bool _isActive = true;
    private bool _isDeleted;
    private bool _isDefault;
    private readonly bool _includeInNetWorth = true;

    public AccountBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public AccountBuilder WithUser(User user)
    {
        _user = user;
        return this;
    }

    public AccountBuilder WithAccountType(AccountType accountType)
    {
        _accountType = accountType;
        return this;
    }

    public AccountBuilder WithCurrency(CurrencyCode currency)
    {
        _currency = currency;
        return this;
    }

    public AccountBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public AccountBuilder WithInstitutionName(string institutionName)
    {
        _institutionName = institutionName;
        return this;
    }

    public AccountBuilder WithBalance(decimal balance)
    {
        _currentBalance = balance;
        _availableBalance = balance;

        return this;
    }

    public AccountBuilder WithActiveStatus(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public AccountBuilder WithDeletedStatus(bool isDeleted)
    {
        _isDeleted = isDeleted;
        return this;
    }

    public AccountBuilder WithDefaultStatus(bool isDefault)
    {
        _isDefault = isDefault;
        return this;
    }

    public Account Build()
    {
        if (_user is null)
        {
            throw new InvalidOperationException(
                "A user must be provided before building an account."
            );
        }

        if (_accountType is null)
        {
            throw new InvalidOperationException(
                "An account type must be provided before building an account."
            );
        }

        if (_currency is null)
        {
            throw new InvalidOperationException(
                "A currency must be provided before building an account."
            );
        }

        return new Account
        {
            Id = _id,

            UserId = _user.Id,
            User = _user,

            AccountTypeId = _accountType.Id,
            AccountType = _accountType,

            CurrencyCodeId = _currency.Id,
            CurrencyCode = _currency,

            Name = _name,
            InstitutionName = _institutionName,
            LastFourDigits = _lastFourDigits,

            CurrentBalance = _currentBalance,
            AvailableBalance = _availableBalance,

            IncludeInNetWorth = _includeInNetWorth,
            IsActive = _isActive,
            IsDeleted = _isDeleted,
            IsDefault = _isDefault,

            CreatedBy = _user.Id,
            LastUpdatedBy = _user.Id,
        };
    }
}
