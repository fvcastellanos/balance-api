

using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BalanceApi.Controllers
{
    public abstract class BaseController : Controller
    {

        protected IActionResult internalServerError(object payload) {
            return StatusCode((int)HttpStatusCode.InternalServerError, payload);
        }

        protected IActionResult Accepted(object payload) {
            return StatusCode((int)HttpStatusCode.Accepted, payload);
        }
    }
}