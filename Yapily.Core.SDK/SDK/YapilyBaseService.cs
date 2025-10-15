using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Yapily.Core.SDK.SDK
{
    public abstract class YapilyBaseService
    {
        protected readonly HttpClient _http;
        protected readonly string _baseUrl = YapilyConfig.BaseUrl;

        protected YapilyBaseService(HttpClient httpClient = null)
        {
            _http = httpClient ?? new HttpClient();
            var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{YapilyConfig.AppKey}:{YapilyConfig.AppSecret}"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> SendRequestAsync<T>(HttpRequestMessage req)
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
    }
}
