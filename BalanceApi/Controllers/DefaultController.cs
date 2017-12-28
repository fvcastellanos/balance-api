using Microsoft.AspNetCore.Mvc;
using static BalanceApi.Controllers.Routes;

namespace BalanceApi.Controllers
{

    [Route("/")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public IActionResult RedirectToIndex()
        {
            return Redirect(IndexPage);
        }
    }
}