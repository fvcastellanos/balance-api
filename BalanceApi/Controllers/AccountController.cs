using System.Collections.Generic;
using System.Linq;
using BalanceApi.Model.Domain;
using BalanceApi.Model.Views.Request;
using BalanceApi.Model.Views.Response;
using BalanceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BalanceApi.Controllers.Routes;

namespace BalanceApi.Controllers
{
    [Route(Accounts)]
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
        public IActionResult AddNew([FromBody] AccountRequest accountRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _accountService.AddNew(accountRequest.AccountTypeId, accountRequest.ProviderId, 
                accountRequest.Name, accountRequest.AccountNumber);

            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var responseView = BuildResponse(result.GetPayload());
            var uri = BuildResourceUri(Accounts, responseView.Id);
            return Created(uri, responseView);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] AccountRequest accountRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var account = BuildAccount(id, accountRequest);
            var result = _accountService.Update(account);

            return result.HasErrors() ? ForFailure(result.GetFailure()) : Ok();
        }

        private static ICollection<AccountResponse> BuldResponseList(IEnumerable<Account> accounts)
        {
            var responseList = (from account in accounts
                select BuildResponse(account)).ToList();

            return responseList;
        }

        private static Account BuildAccount(long id, AccountRequest accountRequest)
        {
            return new Account()
            {
                Id = id,
                AccountTypeId = accountRequest.AccountTypeId,
                ProviderId = accountRequest.ProviderId,
                AccountNumber = accountRequest.AccountNumber,
                Name = accountRequest.Name
            };
        }
        
        private static AccountResponse BuildResponse(Account account)
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
