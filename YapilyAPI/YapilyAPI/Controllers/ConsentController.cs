using Microsoft.AspNetCore.Mvc;
using Yapily.Core.SDK.SDK.Consent;

namespace YapilyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsentController : ControllerBase
    {
        private readonly IConsentService _consentService;

        public ConsentController(IConsentService consentService)
        {
            _consentService = consentService;
        }

        [HttpPost("GET")]
        public async Task<IActionResult> Get(string consentId)
        {
            try
            {
                var response = await _consentService.GetConsentAsync(consentId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
