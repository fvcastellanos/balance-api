using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

using BalanceApi.Services;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace BalanceApi.Controllers
{
    [Route("api/account-type")]
    public class AccountTypeController : BaseController
    {
        private ILogger<AccountTypeController> logger;
        private AccountTypeService service;

        public AccountTypeController(ILogger<AccountTypeController> logger,
            AccountTypeService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Result result = service.GetAccountTypes();
            if(result.isSuccess()) {
                return Ok(result.getObject<List<AccountType>>());
            } else {
                return internalServerError(result.getException());
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            Result result = service.GetAccountTypeById(id);
            if(result.isSuccess()) {
                AccountType accountType = result.getObject<AccountType>();
                if(accountType != null) {
                    return Ok(accountType);
                } else {
                    return NotFound();
                }
            } else {
                return internalServerError(result.getException());
            }
        }

        [HttpPost]
        public IActionResult New( [FromBody] AccountType accountType) {
            Result result = service.newAccountType(accountType);
            if(result.isSuccess()) {
                AccountType item = result.getObject<AccountType>();
                if(item != null) {
                    return Created("New", item);
                } else {
                    return BadRequest();
                }
            } else {
                return internalServerError(result.getException());
            }
        }
    }
}
