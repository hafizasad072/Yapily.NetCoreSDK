using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Users
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserCreateRequest body);
        Task<User> GetUserByIdAsync(string userUuid);
        Task<List<User>> GetUsersAsync(string applicationUserId);
        Task<User> UpdateUsersAsync(string userUuid, UserUpdate update);
    }
}
