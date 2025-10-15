using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Consent
{
    public interface IConsentService
    {
        Task<AccountResponce> GetConsentAsync(string consentId);
    }
}
