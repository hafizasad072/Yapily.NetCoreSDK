using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Interfaces
{
    public interface IConsentService
    {
        Task<AccountResponce> GetConsentAsync(string consentId);
    }
}
