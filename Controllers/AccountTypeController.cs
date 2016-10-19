using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BalanceApi.Model.Domain;
using BalanceApi.Services;


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
        [Route("/")]
        public Response<List<AccountType>> GetAll()
        {
            return service.GetAccountTypes();
        }



        
    }
}
