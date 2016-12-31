using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

using BalanceApi.Services;
using BalanceApi.Model.Domain;
using System;


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
            Result<Exception, List<AccountType>> result = service.GetAccountTypes();
            if(result.IsSuccess()) {
                return Ok(result.GetPayload());
            } else {
                return ForException(result.GetFailure());
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            Result<Exception, AccountType> result = service.GetAccountTypeById(id);
            if(result.IsSuccess()) {
                AccountType accountType = result.GetPayload();
                if(accountType != null) {
                    return Ok(accountType);
                } else {
                    return NotFound();
                }
            } else {
                return ForException(result.GetFailure());
            }
        }

        [HttpPost]
        public IActionResult New( [FromBody] AccountType accountType) {
            Result<Exception, AccountType> result = service.NewAccountType(accountType);
            if(result.IsSuccess()) {
                AccountType item = result.GetPayload();
                if(item != null) {
                    return Created("New", item);
                } else {
                    return BadRequest();
                }
            } else {
                return ForException(result.GetFailure());
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] AccountType accountType) {
            Result<Exception, AccountType> result = service.UpdateAccountType(accountType);
            if(result.IsSuccess()) {
                return Ok(result.GetPayload());
            } else {
                return ForException(result.GetFailure());
            }
        }

        [HttpDeleteAttribute("{id}")]
        public IActionResult Delete(long id) {
            Result<Exception, int> result = service.deleteAccountType(id);
            if(result.IsSuccess()) {
                int rows = result.GetPayload();
                if(rows > 0) {
                    return Accepted(rows);
                } else {
                    return NotFound();
                }
            } else {
                return ForException(result.GetFailure());
            }
        }
    }
}
