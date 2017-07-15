using Microsoft.AspNetCore.Mvc;

namespace BalanceApi.Controllers
{
    [Route("/")]
    public class SwaggerController : Controller
    {
        [HttpGet]
        public IActionResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/ui/index.html");
        }
    }
}