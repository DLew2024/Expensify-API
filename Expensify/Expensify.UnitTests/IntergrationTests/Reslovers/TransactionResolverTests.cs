using Expensify.API.Services.Resolvers;
using Expensify.DataAccessLayer.Enums;
using Expensify.UnitTests.Infrastructure;

namespace Expensify.UnitTests.IntergrationTests.Reslovers;

public class TransactionResolverTests
{
    private readonly SqliteTestDatabase _database;
    private readonly TestDataSeeder _seeder;
    private readonly TransactionResolver _resolver;

    public TransactionResolverTests()
    {
        _database = new SqliteTestDatabase();
        _seeder = new TestDataSeeder(_database.Context);
        _resolver = new TransactionResolver(_database.Context);
    }

    // MethodName_StateUnderTest_ExpectedBehavior
    // Arrange
    // Act
    // Assert
    [Fact]
    public async Task ResolveTransactionByUserIdAndType_ValidTransaction_ReturnsTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        var expectedTransaction = await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: userId
        );

        // Act
        var result = await _resolver.ResolveTransactionByUserIdAndType(
            userId,
            transactionId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedTransaction.Id, result.Id);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(TransactionType.Income, result.Type);
    }

    [Fact]
    public async Task ResolveTransactionByUserIdAndType_TransactionDoesNotExist_ReturnsNull()
    {
        // Act
        var result = await _resolver.ResolveTransactionByUserIdAndType(
            Guid.NewGuid(),
            Guid.NewGuid(),
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveTransactionByUserIdAndType_TransactionBelongsToAnotherUser_ReturnsNull()
    {
        // Arrange
        var transactionOwnerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: transactionOwnerId
        );

        // Act
        var result = await _resolver.ResolveTransactionByUserIdAndType(
            requestingUserId,
            transactionId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveTransactionByUserIdAndType_TransactionTypeDoesNotMatch_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: userId
        );

        // Act
        var result = await _resolver.ResolveTransactionByUserIdAndType(
            userId,
            transactionId,
            TransactionType.Expense,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveTransactionByUserIdAndType_TransactionIsSoftDeleted_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: userId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveTransactionByUserIdAndType(
            userId,
            transactionId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUser_Account_Type_OrderedByDateDESC_ReturnsMatchingTransactionsInDescendingOrder()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var oldestTransaction = await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: accountId,
            transactionDate: 1_700_000_000
        );

        var newestTransaction = await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: accountId,
            transactionDate: 1_800_000_000
        );

        var middleTransaction = await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: accountId,
            transactionDate: 1_750_000_000
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
            userId,
            accountId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(3, result.Count);

        Assert.Equal(newestTransaction.Id, result[0].Id);
        Assert.Equal(middleTransaction.Id, result[1].Id);
        Assert.Equal(oldestTransaction.Id, result[2].Id);
    }

    [Fact]
    public async Task ResolveTransactionsByUser_Account_Type_OrderedByDateDESC_WrongUser_ExcludesTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: otherUserId,
            accountId: accountId
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
            userId,
            accountId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUser_Account_Type_OrderedByDateDESC_WrongAccount_ExcludesTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var otherAccountId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: otherAccountId
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
            userId,
            accountId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUser_Account_Type_OrderedByDateDESC_WrongType_ExcludesTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Expense,
            userId: userId,
            accountId: accountId
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
            userId,
            accountId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUser_Account_Type_OrderedByDateDESC_SoftDeletedTransaction_ExcludesTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: accountId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
            userId,
            accountId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUser_Account_Type_OrderedByDateDESC_NoMatchingTransactions_ReturnsEmptyList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        // Act
        var result = await _resolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
            userId,
            accountId,
            TransactionType.Income,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUserAndAccount_ValidTransactions_ReturnsTransactions()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var firstTransaction = await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: accountId
        );

        var secondTransaction = await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Expense,
            userId: userId,
            accountId: accountId
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUserAndAccount(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(2, result.Length);

        Assert.Contains(result, transaction => transaction.Id == firstTransaction.Id);
        Assert.Contains(result, transaction => transaction.Id == secondTransaction.Id);
    }

    [Fact]
    public async Task ResolveTransactionsByUserAndAccount_TransactionBelongsToAnotherUser_ExcludesTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: otherUserId,
            accountId: accountId
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUserAndAccount(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUserAndAccount_TransactionBelongsToAnotherAccount_ExcludesTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var otherAccountId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: otherAccountId
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUserAndAccount(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUserAndAccount_TransactionIsSoftDeleted_ExcludesTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            userId: userId,
            accountId: accountId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveTransactionsByUserAndAccount(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResolveTransactionsByUserAndAccount_NoTransactionsExist_ReturnsEmptyArray()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        // Act
        var result = await _resolver.ResolveTransactionsByUserAndAccount(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
