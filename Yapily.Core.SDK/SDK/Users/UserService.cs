using System.Text;
using System.Text.Json;
using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Users
{
    public class UserService : YapilyBaseService, IUserService
    {
        public UserService(HttpClient httpClient = null)
        : base(httpClient) { }

        public async Task<User> CreateUserAsync(string applicationUserId)
        {
            var url = $"{_baseUrl}/users";
            var body = new { applicationUserId = applicationUserId };
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };

            return await SendRequestAsync<User>(req);
        }
        public async Task<User> GetUserByIdAsync(string userUuid)
        {
            var url = $"{_baseUrl}/users/{userUuid}";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            return await SendRequestAsync<User>(req);
        }

        public async Task<List<User>> GetUsersAsync(string applicationUserId)
        {
            var url = $"{_baseUrl}/users";

            if (!string.IsNullOrEmpty(applicationUserId))
            {
                url += $"?filter%5BapplicationUserId%5D={Uri.EscapeDataString(applicationUserId)}";
            }

            var req = new HttpRequestMessage(HttpMethod.Get, url);
            return await SendRequestAsync<List<User>>(req);
        }

        public async Task<User> UpdateUsersAsync(string userUuid, UserUpdate update)
        {
            var url = $"{_baseUrl}/users/{userUuid}";
            var req = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(update), Encoding.UTF8, "application/json-patch+json")
            };
            return await SendRequestAsync<User>(req);
        }
    }
}
