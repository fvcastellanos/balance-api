using System.Collections.Generic;
using System.Linq;
using BalanceApi.Model.Domain;
using BalanceApi.Model.Views.Request;
using BalanceApi.Model.Views.Response;
using BalanceApi.Services;
using BalanceApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BalanceApi.Controllers.Routes;

namespace BalanceApi.Controllers 
{
    [Route(TransactionTypes)]
    public class TransactionTypeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly TransactionTypeService _transactionTypeService;
        private readonly IModelValidator<TransactionType> _validator;

        public TransactionTypeController(ILogger<TransactionTypeController> logger, TransactionTypeService transactionTypeService,
            IModelValidator<TransactionType> transactionTypeValidator)
        {
            _logger = logger;
            _transactionTypeService = transactionTypeService;
            _validator = transactionTypeValidator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _transactionTypeService.GetAll();

            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var responseView = BuildResponseList(result.GetPayload());

            return Ok(responseView);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) 
        {
            var result = _transactionTypeService.GetById(id);
            
            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var value = result.GetPayload();
            if (value == null) return NotFound();

            var responseView = BuildResponse(result.GetPayload());
            return Ok(responseView);
        }

        [HttpPost]
        public IActionResult New([FromBody] TransactionTypeRequest transactionTypeRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transactionType = new TransactionType() { Name = transactionTypeRequest.Name, Credit = transactionTypeRequest.Credit };
            var result = _transactionTypeService.New(transactionType);

            if (result.HasErrors()) ForFailure(result.GetFailure());

            var payload = BuildResponse(result.GetPayload());
            var uri = BuildResourceUri(TransactionTypes, payload.Id);

            return Created(uri, payload);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TransactionTypeRequest transactionTypeRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transactionType = new TransactionType()
            {
                Id = id,
                Name = transactionTypeRequest.Name,
                Credit = transactionTypeRequest.Credit
            };
            
            var result = _transactionTypeService.Update(transactionType);

            return result.HasErrors() ? ForFailure(result.GetFailure()) : Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _transactionTypeService.Delete(id);
            
            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var rows = result.GetPayload();
            if (rows > 0) return NoContent();

            return NotFound();
        }

        private ICollection<TransactionTypeResponse> BuildResponseList(IEnumerable<TransactionType> transactionTypes)
        {
            var responseList = (from transactionType in transactionTypes
                    select new TransactionTypeResponse(transactionType.Id, transactionType.Name,
                        transactionType.Credit))
                .ToList();

            return responseList;
        }

        private TransactionTypeResponse BuildResponse(TransactionType transactionType)
        {
            return new TransactionTypeResponse(transactionType.Id, transactionType.Name, transactionType.Credit);
        }
    }
}