
using System;
using System.Collections.Generic;
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
        private ILogger Logger;

        private TransactionTypeService TransactionTypeService;

        private IModelValidator<TransactionType> _validator;

        public TransactionTypeController(ILogger<TransactionTypeController> Logger, TransactionTypeService TransactionTypeService, 
            IModelValidator<TransactionType> transactionTypeValidator)
        {
            this.Logger = Logger;
            this.TransactionTypeService = TransactionTypeService;
            this._validator = transactionTypeValidator;
        }

        [HttpGet]
        public IActionResult GetAll() {
            Result<Exception, List<TransactionType>> result = TransactionTypeService.GetAll();
            if(result.isSuccess())
            {
                return Ok(result.GetPayload());
            }
            else {
                Logger.LogError("Unable to get the providers because: {0}", result.GetFailure());
                return ForException(result.GetFailure());
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) 
        {
            Result<Exception, TransactionType> result = TransactionTypeService.GetById(id);
            if(result.isSuccess()) 
            {
                var value = result.GetPayload();
                if(value != null) 
                {
                    return Ok(result.GetPayload());
                }
                else 
                {
                    return NotFound();
                }
            }
            else
            {
                Logger.LogError("Unable to get the transaction type with id: {0}", id);
                return ForException(result.GetFailure());
            }
        }

        [HttpPost]
        public IActionResult New([FromBody] TransactionType transactionType)
        {
            var obj = _validator.validate(transactionType);
            if(obj.isSuccess())
            {
                var result = TransactionTypeService.New(transactionType);
                if(result.isSuccess())
                {
                    return Created("NewTransactionType", result.GetPayload());
                }
                else 
                {
                    return ForException(result.GetFailure());
                }
            }
            else 
            {
                return BadRequest(obj.GetFailure());
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TransactionType transactionType) 
        {
            var obj = _validator.validate(transactionType);
            if(obj.isSuccess())
            {
                var result = TransactionTypeService.Update(obj.GetPayload());
                if (result.isSuccess())
                {
                    return Created("UpdateTransactionType", result.GetPayload());
                }
                else 
                {
                    return ForException(result.GetFailure());
                }
            }

            return BadRequest(obj.GetFailure());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = TransactionTypeService.Delete(id);
            if(result.isSuccess())
            {
                var rows = result.GetPayload();
                if (rows > 0)
                {
                    return Accepted(rows);
                } 
                else 
                {
                    return NotFound();
                }
            }
            else
            {
                return ForException(result.GetFailure());
            }
        }
    }
}