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
    public class AccountTypeController : Controller
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
                return BadRequest(result.getException());
            }
        }
    }
}
