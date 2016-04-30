using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

using Model.Domain;
using Model.Data;
using Microsoft.Extensions.Logging;

namespace balance_api.Controllers
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

        [HttpGet("{name}")]
        public AccountType GetByName(string name)
        {
            return accountTypeDao.findByName(name);
        }
    }
}
