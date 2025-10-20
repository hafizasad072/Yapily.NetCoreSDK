using Microsoft.AspNetCore.Mvc;
using Yapily.Core.SDK.SDK.Transactions;

namespace YapilyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("GET")]
        public async Task<IActionResult> Get(string consentId, string accountId, DateTime from, DateTime to)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsAsync(consentId, accountId, from, to);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
