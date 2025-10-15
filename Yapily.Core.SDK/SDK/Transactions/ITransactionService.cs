namespace Yapily.Core.SDK.SDK.Transactions
{
    public interface ITransactionService
    {
        Task<BO.Models.Transaction> GetTransactionsAsync(
            string consentId,
            string accountId,
            DateTime from,
            DateTime to);
    }
}
