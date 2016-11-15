
using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class TransactionTypeService
    {

        private ILogger Logger;

        private ITransactionTypeDao TransactionTypeDao;

        public TransactionTypeService(ILogger<TransactionTypeService> Logger, ITransactionTypeDao TransactionTypeDao) 
        {
            this.Logger = Logger;
            this.TransactionTypeDao = TransactionTypeDao;
        }

        public Result<Exception, List<TransactionType>> GetAll() {
            try 
            {
                List<TransactionType> list = TransactionTypeDao.GetAll();
                return Result<Exception, List<TransactionType>>.ForSuccess(list); 
            }
            catch(Exception ex)
            {
                return Result<Exception, List<TransactionType>>.ForFailure(ex);
            }
        }
    }
}