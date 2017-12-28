using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using BalanceApi.Model.Views.Response;

namespace BalanceApi.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult ForException(Exception ex)
        {
            return InternalServerError(ex.Message);
        }
        
        protected IActionResult InternalServerError(object payload)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, payload);
        }

        protected IActionResult ForFailure<T>(T failure)
        {
            // Unprocessable entity
            return StatusCode(422, failure);
        }

        protected string BuildResourceUri(string resource, long id)
        {
            var host = HttpContext.Request.Host.Value;
            var http = HttpContext.Request.IsHttps ? "https" : "http";
            var uri = http + "://" + host + resource + "/" + id;

            return uri;
        }
    }
}