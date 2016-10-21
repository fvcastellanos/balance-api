
using Microsoft.AspNetCore.Mvc;

namespace BalanceApi.Controllers 
{
    [Route("api/foo")]
    public class FooController: Controller 
    {
        [HttpGet]
        public IActionResult fooAction() 
        {
            return Ok("Hello world");
        }
        
    }

}