using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Model.Domain;
using Services;
using Microsoft.Extensions.Logging;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BalanceApi.Controllers
{
    [Route("api/[controller]")]
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
        public Response<List<AccountType>> GetAll()
        {
            return service.GetAccountTypes();
        }



        
    }
}
