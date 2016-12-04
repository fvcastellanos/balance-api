using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class TransactionTypeService
    {
        private ILogger _logger;

        private readonly ITransactionTypeDao _transactionTypeDao;

        public TransactionTypeService(ILogger<TransactionTypeService> logger, ITransactionTypeDao transactionTypeDao)
        {
            this._logger = logger;
            this._transactionTypeDao = transactionTypeDao;
        }

        public Result<Exception, List<TransactionType>> GetAll() {
            try 
            {
                _logger.LogInformation("Getting all the transaction types");
                var list = _transactionTypeDao.GetAll();
                return Result<Exception, List<TransactionType>>.ForSuccess(list); 
            }
            catch(Exception ex)
            {
                return Result<Exception, List<TransactionType>>.ForFailure(ex);
            }
        }

        public Result<Exception, TransactionType> GetById(long id)
        {
            try 
            {
                _logger.LogInformation("Getting transaction type with id: {0}", id);
                var transactionType = _transactionTypeDao.GetById(id);
                return Result<Exception, TransactionType>.ForSuccess(transactionType);
            }
            catch(Exception ex)
            {
                return Result<Exception, TransactionType>.ForFailure(ex);
            }
        }

        public Result<Exception, TransactionType> New(TransactionType transactionType)
        {
            try 
            {
                _logger.LogInformation("Adding a new transaction type");
                var id = _transactionTypeDao.New(transactionType);

                if(id != 0)
                {
                    TransactionType value = _transactionTypeDao.GetById(id);
                    return Result<Exception, TransactionType>.ForSuccess(value);
                }
                else 
                {
                    return Result<Exception, TransactionType>.ForFailure(new Exception("Can't create new account type"));
                }
            }
            catch(Exception ex)
            {
                return Result<Exception, TransactionType>.ForFailure(ex);
            }
        }

    }
}