
using BalanceApi.Model.Domain;
using BalanceApi.Services;
using BalanceApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Controllers 
{
    [Route("/api/transaction-type")]
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
        public IActionResult GetAll() {
            var result = _transactionTypeService.GetAll();

            if (result.IsSuccess())
            {
                return Ok(result.GetPayload());
            }

            _logger.LogError("Unable to get the providers because: {0}", result.GetFailure());
            return ForException(result.GetFailure());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) 
        {
            var result = _transactionTypeService.GetById(id);

            if (result.IsSuccess())
            {
                var value = result.GetPayload();
                if (value != null)
                {
                    return Ok(result.GetPayload());
                }

                return NotFound();
            }

            _logger.LogError("Unable to get the transaction type with id: {0}", id);
            return ForException(result.GetFailure());
        }

        [HttpPost]
        public IActionResult New([FromBody] TransactionType transactionType)
        {
            var validation = _validator.Validate(transactionType);

            if (validation.HasFailed())
            {
                return BadRequest(validation.GetErrors());
            }

            var result = _transactionTypeService.New(transactionType);

            if (result.IsSuccess())
            {
                return Created("NewTransactionType", result.GetPayload());
            }

            return ForException(result.GetFailure());
        }

        [HttpPut]
        public IActionResult Update([FromBody] TransactionType transactionType) 
        {
            var validation = _validator.Validate(transactionType);

            if (validation.HasFailed())
            {
                return BadRequest(validation.GetErrors());
            }

            var result = _transactionTypeService.Update(transactionType);

            if (result.IsSuccess())
            {
                return Created("UpdateTransactionType", result.GetPayload());
            }

            return ForException(result.GetFailure());

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _transactionTypeService.Delete(id);

            if (result.IsSuccess())
            {
                var rows = result.GetPayload();

                if (rows > 0)
                {
                    return Accepted(rows);
                }

                return NotFound();
            }

            return ForException(result.GetFailure());
        }
    }
}