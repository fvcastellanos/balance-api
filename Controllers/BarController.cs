

using System.Collections.Generic;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using BalanceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BalanceApi.Controllers
{
    [Route("api/bar")]
    public class BarController : Controller
    {
        private AccountTypeService service;
        
        public BarController(AccountTypeService service) {
            this.service = service;
        }

        [HttpGet]
        public IActionResult getBars() {
            Result result = service.GetAccountTypes();
            if(result.isSuccess()) {
                return Ok(result.getObject<List<AccountType>>());
            } else {
                return BadRequest(result.getException());
            }
        }
    }
}