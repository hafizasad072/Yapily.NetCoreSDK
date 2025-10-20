using System.Text;
using System.Text.Json;
using Yapily.BO.Models;
using Yapily.Core.SDK.Models.Accounts;
using Yapily.Core.SDK.Models.Transactions;

namespace Yapily.Core.SDK.SDK.FinancialData
{
    public class FinancialDataService : YapilyBaseService, IFinancialDataService
    {
        public FinancialDataService(HttpClient httpClient = null) : base(httpClient) { }

        // Create Account Authorisation Request (redirect / hosted flow)
        public async Task<AccountResponce> CreateAccountAuthRequestAsync(CreateAccountAuthRequest createAccountAuthRequest)
        {
            var url = $"{_baseUrl}/account-auth-requests";

            var body = new
            {
                userUuid = createAccountAuthRequest.UserUuid,
                institutionId = createAccountAuthRequest.InstitutionId,
                applicationUserId = createAccountAuthRequest.ApplicationUserId,
                callback = createAccountAuthRequest.Callback,
                oneTimeToken = createAccountAuthRequest.OneTimeToken,
            };

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };
            return await SendRequestAsync<AccountResponce>(req);
        }

        // Get Accounts (under a consent)
        public async Task<Accounts> GetAccountsAsync(string consentId)
        {
            var url = $"{_baseUrl}/accounts";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("consent", consentId);
            return await SendRequestAsync<Accounts>(req);
        }

        // Get Account Balances
        public async Task<BalanceVM> GetAccountBalancesAsync(string consentId, string accountId)
        {
            var url = $"{_baseUrl}/accounts/{accountId}/balances";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("consent", consentId);
            return await SendRequestAsync<BalanceVM>(req);
        }

        // Get Transactions
        public async Task<Yapily.BO.Models.Transaction> GetTransactionsAsync(GetTransactionsRequest request)
        {
            var queryParams = new List<string>();

            if (request.From.HasValue)
            {
                queryParams.Add($"from={request.From.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
            }
            if (request.To.HasValue)
            {
                queryParams.Add($"before={request.To.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
            }
            if (request.Limit.HasValue)
            {
                queryParams.Add($"limit={request.Limit.Value}");
            }
            if (!string.IsNullOrEmpty(request.Sort))
            {
                queryParams.Add($"sort={request.Sort}");
            }
            if (request.Offset.HasValue)
            {
                queryParams.Add($"offset={request.Offset.Value}");
            }
            if (!string.IsNullOrEmpty(request.Cursor))
            {
                queryParams.Add($"cursor={request.Cursor}");
            }

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;

            var url = $"{_baseUrl}/accounts/{request.AccountId}/transactions{queryString}";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("consent", request.ConsentId);

            return await SendRequestAsync<Yapily.BO.Models.Transaction>(req);
        }
    }
}
