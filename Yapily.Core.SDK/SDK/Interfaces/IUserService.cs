using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string applicationUserId);
    }
}
