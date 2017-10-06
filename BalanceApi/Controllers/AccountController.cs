using System.Collections.Generic;
using System.Linq;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using BalanceApi.Model.Views.Request;
using BalanceApi.Model.Views.Response;
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

            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var responseView = BuldResponseList(result.GetPayload());
            return Ok(responseView);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _accountService.GetById(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var account = result.GetPayload();
            if (account == null) return NotFound();

            var responseView = BuildResponse(account);
            return Ok(responseView);
        }

        [HttpPost]
        public IActionResult AddNew([FromBody] AddAccountRequest addAccountRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _accountService.AddNew(addAccountRequest.AccountTypeId, addAccountRequest.ProviderId, 
                addAccountRequest.Name, addAccountRequest.AccountNumber);

            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var responseView = BuildResponse(result.GetPayload());
            return Created("NewAccount", responseView);
        }

        private ICollection<AccountResponse> BuldResponseList(IEnumerable<Account> accounts)
        {
            var responseList = (from account in accounts
                select BuildResponse(account)).ToList();

            return responseList;
        }

        private AccountResponse BuildResponse(Account account)
        {
            var accountType = new AccountTypeResponse(account.AccountTypeId, account.AccountType);
            var provider = new ProviderResponse(account.ProviderId, account.Provider, account.ProviderCountry);

            var response = new AccountResponse()
            {
                Id = account.Id,
                Name =  account.Name,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Provider = provider,
                AccountType = accountType
            };
            
            return response;
        }

    }
}
