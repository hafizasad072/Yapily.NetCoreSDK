using Microsoft.AspNetCore.Mvc;
using Yapily.BO.Models;
using Yapily.Core.SDK.SDK.Account;

namespace YapilyAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsService _accountService;

        public AccountController(IAccountsService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("POST")]
        public async Task<IActionResult> CreateAccountAuthRequestAsync(string userUuid,
           string institutionId,
           string applicationUserId,
           string callbackUrl,
           bool oneTimeToken = false)
        {
            try
            {
                var response = await _accountService.CreateAccountAuthRequestAsync(userUuid, institutionId, applicationUserId, callbackUrl, oneTimeToken = false);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GET")]
        public async Task<IActionResult> GetAccountsAsync(string consentId)
        {
            try
            {
                var response = await _accountService.GetAccountsAsync(consentId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("AccountBalances")]
        public async Task<IActionResult> GetAccountBalancesAsync(string consentId, string accountId)
        {
            try
            {
                var response = await _accountService.GetAccountBalancesAsync(consentId, accountId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
