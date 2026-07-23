using System.ComponentModel.DataAnnotations;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.ServiceClasses.Helpers
{
    public class TransactionHelper
    {
        /// <summary>
        /// Reverses the balance impact of a transaction on its associated account.
        /// </summary>
        public static void ReverseTransactionBalance(Account account, Transaction transaction)
        {
            switch (transaction.Type)
            {
                case TransactionType.Income:
                    account.CurrentBalance -= transaction.Amount;
                    account.AvailableBalance -= transaction.Amount;
                    break;

                case TransactionType.Expense:
                    account.CurrentBalance += transaction.Amount;
                    account.AvailableBalance += transaction.Amount;
                    break;

                default:
                    throw new ValidationException(
                        $"Transaction type '{transaction.Type}' cannot be reversed."
                    );
            }
        }

        /// <summary>
        /// Marks a transaction as soft deleted and updates its audit information.
        /// </summary>
        public static void SoftDeleteTransaction(Transaction transaction, Guid userId)
        {
            transaction.IsDeleted = true;
            transaction.LastUpdatedBy = userId;
            transaction.UpdatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
