using Yapily.BO.Models;
using Yapily.Core.SDK.SDK.Interfaces;

namespace Yapily.Core.SDK.SDK.Institutions
{
    public class InstitutionService : YapilyBaseService, IInstitutionService
    {
        public InstitutionService(HttpClient httpClient = null)
        : base(httpClient) { }

        public async Task<Institution> GetInstitutionsAsync(string country = null)
        {
            string url = $"{_baseUrl}/institutions";

            if (!string.IsNullOrEmpty(country))
                url += $"?country={country}";

            var req = new HttpRequestMessage(HttpMethod.Get, url);

            return await SendRequestAsync<Institution>(req);
        }
    }
}
