using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Interfaces
{
    public interface IInstitutionService
    {
        Task<Institution> GetInstitutionsAsync(string country = null);

    }
}
