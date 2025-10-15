using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Interfaces
{
    public interface IAccountsService
    {
        Task<AccountResponce> CreateAccountAuthRequestAsync(
            string userUuid,
            string institutionId,
            string applicationUserId,
            string callbackUrl,
            bool oneTimeToken = false);

        Task<Accounts> GetAccountsAsync(string consentId);

        Task<BalanceVM> GetAccountBalancesAsync(string consentId, string accountId);
    }
}
