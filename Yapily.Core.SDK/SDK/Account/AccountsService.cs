using System.Text;
using System.Text.Json;
using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Account
{
    public class AccountsService : YapilyBaseService, IAccountsService
    {
        public AccountsService(HttpClient httpClient = null) : base(httpClient) { }

        // Create Account Authorisation Request (redirect / hosted flow)
        public async Task<AccountResponce> CreateAccountAuthRequestAsync(
            string userUuid,
            string institutionId,
            string applicationUserId,
            string callbackUrl,
            bool oneTimeToken = false)
        {
            var url = $"{_baseUrl}/account-auth-requests";
            var body = new
            {
                userUuid,
                institutionId,
                applicationUserId,
                callback = callbackUrl,
                oneTimeToken
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
    }
}
