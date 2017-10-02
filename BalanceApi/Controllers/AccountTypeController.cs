using BalanceApi.Controllers.Views;
using BalanceApi.Controllers.Views.Request;
using BalanceApi.Model.Domain;
using BalanceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace BalanceApi.Controllers
{
    [Route("api/account-type")]
    public class AccountTypeController : BaseController
    {
        private ILogger<AccountTypeController> _logger;
        private readonly AccountTypeService _service;

        public AccountTypeController(ILogger<AccountTypeController> logger,
            AccountTypeService service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAccountTypes();
            return result.IsSuccess() ? Ok(result.GetPayload()) : ForFailure(result.GetFailure());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            var result = _service.GetAccountTypeById(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var accountType = result.GetPayload();
                
            if(accountType == null)
                return NotFound();
            
            return Ok(accountType);
        }

        [HttpPost]
        public IActionResult New([FromBody] AddAccountType accountType) {

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            var result = _service.NewAccountType(accountType.Name);
            
            if(result.IsSuccess()) {
                var item = result.GetPayload();
                
                if(item != null) {
                    return Created("New", item);
                } else {
                    return BadRequest();
                }
            } else {
                return ForFailure(result.GetFailure());
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateAccountType updateAccountType)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var accountType = new AccountType(updateAccountType.Id, updateAccountType.Name);
            var result = _service.UpdateAccountType(accountType);

            return result.HasErrors() ? ForFailure(result.GetFailure()) : Ok(result.GetPayload());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = _service.deleteAccountType(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var rows = result.GetPayload();
            
            if(rows > 0) return Accepted(rows);

            return NotFound();
        }
    }
}
