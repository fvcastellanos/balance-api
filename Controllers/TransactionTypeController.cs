
using System;
using System.Collections.Generic;
using BalanceApi.Model.Domain;
using BalanceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Controllers 
{
    [Route("/api/transaction-type")]
    public class TransactionTypeController : BaseController
    {
        private ILogger Logger;

        private TransactionTypeService TransactionTypeService;

        public TransactionTypeController(ILogger<TransactionTypeController> Logger, TransactionTypeService TransactionTypeService)
        {
            this.Logger = Logger;
            this.TransactionTypeService = TransactionTypeService;
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
    }
}