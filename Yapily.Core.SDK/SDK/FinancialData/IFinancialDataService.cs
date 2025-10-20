using Yapily.BO.Models;
using Yapily.Core.SDK.Models.Accounts;
using Yapily.Core.SDK.Models.Transactions;

namespace Yapily.Core.SDK.SDK.FinancialData
{
    public interface IFinancialDataService
    {
        Task<AccountResponce> CreateAccountAuthRequestAsync(CreateAccountAuthRequest createAccountAuthRequest);

        Task<Accounts> GetAccountsAsync(string consentId);

        Task<BalanceVM> GetAccountBalancesAsync(string consentId, string accountId);

        Task<BO.Models.Transaction> GetTransactionsAsync(GetTransactionsRequest request);
    }
}
