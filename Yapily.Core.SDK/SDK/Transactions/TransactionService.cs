using Yapily.Core.SDK.SDK.Interfaces;

namespace Yapily.Core.SDK.SDK.Transactions
{
    public class TransactionService : YapilyBaseService, ITransactionService
    {
        public TransactionService(HttpClient httpClient = null)
        : base(httpClient) { }

        // Get Transactions
        public async Task<Yapily.BO.Models.Transaction> GetTransactionsAsync(
            string consentId,
            string accountId,
            DateTime from,
            DateTime to)
        {
            string fr = from.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string toS = to.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var url = $"{_baseUrl}/accounts/{accountId}/transactions?from={Uri.EscapeDataString(fr)}&before={Uri.EscapeDataString(toS)}";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("consent", consentId);
            return await SendRequestAsync<Yapily.BO.Models.Transaction>(req);
        }
    }
}
