using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.UnitTests.Builders;

public sealed class TransactionBuilder
{
    private Guid _id = Guid.NewGuid();

    private Account? _account;
    private User? _user;
    private PaymentMethod? _paymentMethod;

    private decimal _amount = 100m;
    private readonly string _merchantName = "Test Transaction";
    private string _description = "Test Transaction";
    private long _transactionDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    private bool _isDeleted;

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

    public TransactionBuilder WithPaymentMethod(PaymentMethod paymentMethod)
    {
        _paymentMethod = paymentMethod;
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

        if (_paymentMethod is null)
        {
            throw new InvalidOperationException(
                "A payment method must be provided before building a transaction."
            );
        }

        return new Transaction
        {
            Id = _id,
            MerchantName = _merchantName,

            UserId = _user.Id,
            User = _user,

            PaymentMethodId = _paymentMethod.Id,
            PaymentMethod = _paymentMethod,

            AccountId = _account.Id,
            Account = _account,

            Amount = _amount,
            AccountBalanceAfterTransaction = _account.CurrentBalance + _amount,

            Description = _description,
            TransactionDate = _transactionDate,

            IsDeleted = _isDeleted,
            CreatedBy = _user.Id,
            LastUpdatedBy = _user.Id,

            Type = transactionType,
            Status = TransactionPostedStatus.Posted,
        };
    }
}