using System.Collections.Generic;
using System.Linq;
using BalanceApi.Model.Domain;
using BalanceApi.Model.Views.Request;
using BalanceApi.Model.Views.Response;
using BalanceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BalanceApi.Controllers.Routes;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace BalanceApi.Controllers
{
    [Route(AccountTypes)]
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
            return result.IsSuccess() ? Ok(BuildResponseList(result.GetPayload())) : ForFailure(result.GetFailure());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _service.GetAccountTypeById(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var accountType = result.GetPayload();
                
            if(accountType == null)
                return NotFound();
            
            return Ok(BuildResponse(accountType));
        }

        [HttpPost]
        public IActionResult New([FromBody] AccountTypeRequest accountTypeRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = _service.NewAccountType(accountTypeRequest.Name);

            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var item = BuildResponse(result.GetPayload());
            var uri = BuildResourceUri(AccountTypes, item.Id);
            
            return Created(uri, item);

        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] AccountTypeRequest accountTypeRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var accountType = new AccountType(id, accountTypeRequest.Name);
            var result = _service.UpdateAccountType(accountType);

            return result.HasErrors() ? ForFailure(result.GetFailure()) : Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _service.DeleteAccountType(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var rows = result.GetPayload();
            
            if(rows > 0) return NoContent();

            return NotFound();
        }

        private ICollection<AccountTypeResponse> BuildResponseList(IEnumerable<AccountType> accountTypes)
        {
            var responseList = (from accountType in accountTypes
                select new AccountTypeResponse(accountType.Id, accountType.Name)).ToList();

            return responseList;
        }

        private AccountTypeResponse BuildResponse(AccountType accountType)
        {
            return new AccountTypeResponse(accountType.Id, accountType.Name);
        }
        
    }
}
