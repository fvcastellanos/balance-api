

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;

namespace BalanceApi.Controllers
{
    public abstract class BaseController : Controller
    {

        protected IActionResult ForException(Exception ex) {
            return InternalServerError(ex.Message);
        }
        protected IActionResult InternalServerError(object payload) {
            return StatusCode((int)HttpStatusCode.InternalServerError, payload);
        }

        // protected IActionResult Accepted(object payload) {
        //     return StatusCode((int)HttpStatusCode.Accepted, payload);
        // }
    }
}