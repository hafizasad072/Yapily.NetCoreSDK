using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Users
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string applicationUserId);
    }
}
