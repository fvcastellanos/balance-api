

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;

namespace BalanceApi.Controllers
{
    public abstract class BaseController : Controller
    {

        protected IActionResult ForException(Exception ex) {
            return internalServerError(ex.Message);
        }
        protected IActionResult internalServerError(object payload) {
            return StatusCode((int)HttpStatusCode.InternalServerError, payload);
        }

        protected IActionResult Accepted(object payload) {
            return StatusCode((int)HttpStatusCode.Accepted, payload);
        }
    }
}