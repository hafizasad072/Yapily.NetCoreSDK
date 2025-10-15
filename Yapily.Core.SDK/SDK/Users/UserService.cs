using System.Text;
using System.Text.Json;
using Yapily.BO.Models;
using Yapily.Core.SDK.SDK.Interfaces;

namespace Yapily.Core.SDK.SDK.Users
{
    public class UserService : YapilyBaseService, IUserService
    {
        public UserService(HttpClient httpClient = null)
        : base(httpClient) { }

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
    }
}
