using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.UnitTests.Builders;

public sealed class TransactionBuilder
{
    private Guid _id = Guid.NewGuid();

    private Account? _account;
    private User? _user;

    private decimal _amount = 100m;
    private readonly string _MerchantName = "Test Transaction";
    private string _description = "Test transaction";
    private long _transactionDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    private bool _isDeleted = false;

    public TransactionBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public TransactionBuilder WithAccount(Account account)
    {
        _account = account;
        return this;
    }

    public TransactionBuilder WithUser(User user)
    {
        _user = user;
        return this;
    }

    public TransactionBuilder WithDeletedStatus(bool isDeleted)
    {
        _isDeleted = isDeleted;
        return this;
    }

    public TransactionBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public TransactionBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public TransactionBuilder WithTransactionDate(long transactionDate)
    {
        _transactionDate = transactionDate;
        return this;
    }

    public Transaction Build(TransactionType transactionType)
    {
        if (_user is null)
        {
            throw new InvalidOperationException(
                "A user must be provided before building a transaction."
            );
        }

        if (_account is null)
        {
            throw new InvalidOperationException(
                "An account must be provided before building a transaction."
            );
        }

        return new Transaction
        {
            Id = _id,
            MerchantName = _MerchantName,

            UserId = _account.UserId,

            AccountId = _account.Id,
            Account = _account,

            Amount = _amount,
            AccountBalanceAfterTransaction = _account.CurrentBalance + _amount,

            Description = _description,
            TransactionDate = _transactionDate,

            IsDeleted = _isDeleted,
            CreatedBy = _account.UserId,
            LastUpdatedBy = _account.UserId,

            Type = transactionType,
            Status = TransactionPostedStatus.Posted,
        };
    }
}
