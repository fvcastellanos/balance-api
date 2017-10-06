using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using BalanceApi.Model.ViewModels;
using BalanceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Controllers
{
    [Route(template: "api/account")]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;
        private ILogger<AccountController> _logger;

        public AccountController(AccountService accountService, ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountService.GetAll();
            return result.IsSuccess()? Ok(result.GetPayload()) : ForFailure(result.GetFailure());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _accountService.GetById(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var account = result.GetPayload();
            if (account == null) return NotFound();

            return Ok(account);
        }

        [HttpPost]
        public IActionResult AddNew([FromBody] AddAccountRequest addAccountRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _accountService.AddNew(addAccountRequest.AccountTypeId, addAccountRequest.ProviderId, 
                addAccountRequest.Name, addAccountRequest.AccountNumber);

            return result.IsSuccess() ? Created("NewAccount", result.GetPayload()) : ForFailure(result.GetFailure());
        }

    }
}
