using Yapily.BO.Models;

namespace Yapily.Core.SDK.SDK.Institutions
{
    public interface IInstitutionService
    {
        Task<Institution> GetInstitutionsAsync();

    }
}
