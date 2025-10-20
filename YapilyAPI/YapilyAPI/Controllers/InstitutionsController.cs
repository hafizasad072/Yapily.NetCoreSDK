using Microsoft.AspNetCore.Mvc;
using Yapily.Core.SDK.SDK.Institutions;

namespace YapilyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly IInstitutionService _institutionsService;

        public InstitutionsController(IInstitutionService InstitutionsService)
        {
            _institutionsService = InstitutionsService;
        }

        [HttpPost("GET")]
        public async Task<IActionResult> Get(string country = null)
        {
            try
            {
                var institution = await _institutionsService.GetInstitutionsAsync(country);
                return Ok(institution);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
