using Microsoft.AspNetCore.Mvc;
using static BalanceApi.Controllers.Routes;

namespace BalanceApi.Controllers
{
    [Route(Transactions)]
    public class TransactionController : BaseController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}