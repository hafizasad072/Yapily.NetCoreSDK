using Yapily.BO.Models;
using Yapily.Core.SDK.SDK.Interfaces;

namespace Yapily.Core.SDK.SDK.Consent
{
    public class ConsentService : YapilyBaseService, IConsentService
    {
        public ConsentService(HttpClient httpClient = null) : base(httpClient) { }

        // Get Consent / Authorisation details
        public async Task<AccountResponce> GetConsentAsync(string consentId)
        {
            var url = $"{_baseUrl}/consents/{consentId}";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            return await SendRequestAsync<AccountResponce>(req);
        }
    }
}
