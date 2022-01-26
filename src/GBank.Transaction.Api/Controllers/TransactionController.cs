using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GBank.Transaction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Retrieve transaction by filter
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <param name="filter">Default limit is 10</param>
        /// <returns></returns>
        [Authorize(AuthConstants.RoleUser)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id, [FromQuery]Filter filter)
        {
            Result<List<TransactionDto>> result = _transactionService.GetCustomerTransactions(id,filter);
            if(result.IsSuccess)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Adding new transaction to account
        /// </summary>
        /// <param name="newTransaction"></param>
        /// <returns></returns>
        [Authorize(AuthConstants.RoleAdmin)]
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionDto newTransaction)
        {
            var result = await _transactionService.AddTransaction(newTransaction);
            if(result.IsSuccess)
                return StatusCode(201, result);

            return StatusCode(204, result);
        }
    }
}


