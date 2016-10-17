using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using BalanceApi.Model.Domain;
using BalanceApi.Model.Data;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IAccountTypeDao accountTypeDao;
        private ILogger<AccountController> logger;

        public AccountController(IAccountTypeDao accountTypeDao,
            ILogger<AccountController> logger)
        {
            this.logger = logger;
            this.accountTypeDao = accountTypeDao;
        }

        [HttpGet]
        public List<AccountType> Get()
        {
            return accountTypeDao.findAll();
        }

        [HttpGet("{id}")]
        public AccountType GetById(long id)
        {
            return accountTypeDao.findById(id);
        }

    }
}
