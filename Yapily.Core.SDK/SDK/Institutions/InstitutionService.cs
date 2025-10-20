using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Institutions
{
    public class InstitutionService : YapilyBaseService, IInstitutionService
    {
        public InstitutionService(HttpClient httpClient = null)
        : base(httpClient) { }

        public async Task<Institution> GetInstitutionsAsync()
        {
            string url = $"{_baseUrl}/institutions";

            var req = new HttpRequestMessage(HttpMethod.Get, url);

            return await SendRequestAsync<Institution>(req);
        }
    }
}
