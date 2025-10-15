namespace Yapily.Core.SDK.SDK.Interfaces
{
    public interface ITransactionService
    {
        Task<Yapily.BO.Models.Transaction> GetTransactionsAsync(
            string consentId,
            string accountId,
            DateTime from,
            DateTime to);
    }
}
