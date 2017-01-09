using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class TransactionTypeService
    {
        private readonly ILogger _logger;

        private readonly ITransactionTypeDao _transactionTypeDao;

        public TransactionTypeService(ILogger<TransactionTypeService> logger, ITransactionTypeDao transactionTypeDao)
        {
            _logger = logger;
            _transactionTypeDao = transactionTypeDao;
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

                return Result<Exception, TransactionType>.ForFailure(new Exception("Can't create new account type"));
            }
            catch(Exception ex)
            {
                return Result<Exception, TransactionType>.ForFailure(ex);
            }
        }

        public Result<Exception, TransactionType> Update(TransactionType transactionType) {
            try
            {
                _logger.LogInformation("Updating transaction type: {0}", transactionType.Name);
                var transactionTypeOld = _transactionTypeDao.GetById(transactionType.Id);

                if(transactionTypeOld == null)
                {
                    return Result<Exception, TransactionType>.ForFailure(new Exception("Transaction Type not found"));
                }

                var updated = _transactionTypeDao.Update(transactionType);

                return Result<Exception, TransactionType>.ForSuccess(updated);
            }
            catch(Exception ex)
            {
                return Result<Exception, TransactionType>.ForFailure(ex);
            }
        }

        public Result<Exception, int> Delete(long id)
        {
            try
            {
                _logger.LogInformation("Deleting Transaction Type with id: {0}", id);
                var rows = _transactionTypeDao.Delete(id);
                _logger.LogInformation("Transaction Type with Id: {0} was deleted", id);

                return Result<Exception, int>.ForSuccess(rows);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to delete Transaction type with id: {0} -> {1}", id, ex);
                return Result<Exception, int>.ForFailure(ex);
            }
        }

    }
}