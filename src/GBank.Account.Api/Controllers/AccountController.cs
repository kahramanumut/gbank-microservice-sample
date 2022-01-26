using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GBank.Account.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [Authorize(AuthConstants.RoleUser)]
        [HttpGet("{id}")]
        public IActionResult HasExistAccount(string id)
        {
            Result result = _accountService.HasExistAccount(id);
            if(result.IsSuccess)
                return Ok(result);

            return NotFound();
        }

        [Authorize(AuthConstants.RoleUser)]
        [HttpGet("customer/{id}")]
        public IActionResult GetAccountsByCustomerId(string id)
        {
            Result<List<AccountDto>> result = _accountService.GetAccountsByCustomerId(id);
            if(result.IsSuccess)
                return Ok(result);

            return NotFound();
        }

        [Authorize(AuthConstants.RoleAdmin)]
        [HttpPost]
        public IActionResult AddNewAccount(AddAccountDto dto)
        {
            Result<AccountDto> result = _accountService.AddAccount(dto);
            if(result.IsSuccess)
                return Ok(result);

            return NotFound();
        }

    }
}


