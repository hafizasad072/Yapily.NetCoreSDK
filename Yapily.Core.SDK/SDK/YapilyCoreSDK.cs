using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Yapily.BO.Models;

public class YapilyCoreSDK
{
    private readonly HttpClient _http;
    private readonly string _baseUrl = "https://api.yapily.com";
    private readonly string _appKey;
    private readonly string _appSecret;

    public YapilyCoreSDK(string appKey, string appSecret, HttpClient httpClient = null)
    {
        _appKey = appKey;
        _appSecret = appSecret;
        _http = httpClient ?? new HttpClient();
        var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_appKey}:{_appSecret}"));
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private async Task<T> SendRequestAsync<T>(HttpRequestMessage req)
    {
        var resp = await _http.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        string json = await resp.Content.ReadAsStringAsync();
        var obj = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return obj;
    }

    // 1. Create User
    public async Task<User> CreateUserAsync(string applicationUserId)
    {
        var url = $"{_baseUrl}/users";
        var body = new { applicationUserId };
        var req = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };
        return await SendRequestAsync<User>(req);
    }

    // 2. (Optional) Get Institutions (bank list) — may not be needed if using hosted link approach
    public async Task<Institution> GetInstitutionsAsync(string country = null)
    {
        string url = $"{_baseUrl}/institutions";
        if (!string.IsNullOrEmpty(country))
            url += $"?country={country}";
        var req = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendRequestAsync<Institution>(req);
    }

    // 3. Create Account Authorisation Request (redirect / hosted flow)
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

    // 3b. For embedded flow: initiate embedded auth
    public async Task<EmbeddedAccountAuthRequest> CreateEmbeddedAccountAuthRequestAsync(
        string userUuid,
        string institutionId,
        string applicationUserId,
        string callbackUrl)
    {
        var url = $"{_baseUrl}/embedded-account-auth-requests";
        var body = new
        {
            userUuid,
            institutionId,
            applicationUserId,
            callback = callbackUrl
        };
        var req = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };
        return await SendRequestAsync<EmbeddedAccountAuthRequest>(req);
    }

    // 3c. Continue embedded flow: selecting SCA method or submitting code
    public async Task<EmbeddedAccountAuthRequest> UpdateEmbeddedAuthAsync(string consentId, EmbeddedAuthUpdate update)
    {
        var url = $"{_baseUrl}/embedded-account-auth-requests/{consentId}";
        var req = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(update), Encoding.UTF8, "application/json")
        };
        return await SendRequestAsync<EmbeddedAccountAuthRequest>(req);
    }

    // 5. Get Consent / Authorisation details
    public async Task<AccountResponce> GetConsentAsync(string consentId)
    {
        var url = $"{_baseUrl}/consents/{consentId}";
        var req = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendRequestAsync<AccountResponce>(req);
    }

    // 6. Get Accounts (under a consent)
    public async Task<Accounts> GetAccountsAsync(string consentId)
    {
        var url = $"{_baseUrl}/accounts";
        var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Add("consent", consentId);
        return await SendRequestAsync<Accounts>(req);
    }

    // 7. Get Account Balances
    public async Task<BalanceVM> GetAccountBalancesAsync(string consentId, string accountId)
    {
        var url = $"{_baseUrl}/accounts/{accountId}/balances";
        var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Add("consent", consentId);
        return await SendRequestAsync<BalanceVM>(req);
    }

    // 8. Get Transactions
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

    // 9. Refresh / Re-authorise (for redirect / hosted)
    public async Task<AccountResponce> RefreshAuthRequestAsync(string consentId)
    {
        var url = $"{_baseUrl}/account-auth-requests/{consentId}";
        var req = new HttpRequestMessage(HttpMethod.Patch, url);
        return await SendRequestAsync<AccountResponce>(req);
    }
}
