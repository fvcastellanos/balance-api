using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountTypeDao _accountTypeDao;
        private ILogger<AccountController> _logger;

        public AccountController(IAccountTypeDao accountTypeDao,
            ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountTypeDao = accountTypeDao;
        }

        [HttpGet]
        public List<AccountType> Get()
        {
            return _accountTypeDao.FindAll();
        }

        [HttpGet("{id}")]
        public AccountType GetById(long id)
        {
            return _accountTypeDao.FindById(id);
        }

    }
}
