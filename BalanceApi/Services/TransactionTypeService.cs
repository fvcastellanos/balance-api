using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class TransactionTypeService : BaseService
    {
        private readonly ILogger _logger;

        private readonly ITransactionTypeDao _transactionTypeDao;

        public TransactionTypeService(ILogger<TransactionTypeService> logger, ITransactionTypeDao transactionTypeDao)
        {
            _logger = logger;
            _transactionTypeDao = transactionTypeDao;
        }

        public Result<Error, List<TransactionType>> GetAll() 
        {
            try 
            {
                _logger.LogInformation("Getting all the transaction types");
                var list = _transactionTypeDao.GetAll();

                return BuildSuccessResult(list);; 
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't get the transactions types: ", ex);
                return BuildFailedResult<List<TransactionType>>("Can't get the transactions types");
            }
        }

        public Result<Error, TransactionType> GetById(long id)
        {
            try 
            {
                _logger.LogInformation("Getting transaction type with id: {0}", id);
                var transactionType = _transactionTypeDao.GetById(id);

                return BuildSuccessResult(transactionType);
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't get transaction type", ex);
                return BuildFailedResult<TransactionType>("Can't get transaction type");
            }
        }

        public Result<Error, TransactionType> New(TransactionType transactionType)
        {
            try 
            {
                _logger.LogInformation("Adding a new transaction type");
                var id = _transactionTypeDao.New(transactionType);
                
                if (id == 0) return BuildFailedResult<TransactionType>("Can't create new account type");

                var value = _transactionTypeDao.GetById(id);
                return BuildSuccessResult(value);
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't create new account type: ", ex);
                return BuildFailedResult<TransactionType>("Can't create new account type");
            }
        }

        public Result<Error, TransactionType> Update(TransactionType transactionType) {
            try
            {
                _logger.LogInformation("Updating transaction type: {0}", transactionType.Name);
                var transactionTypeOld = _transactionTypeDao.GetById(transactionType.Id);

                if (transactionTypeOld == null) return BuildFailedResult<TransactionType>("Transaction Type not found");

                var updated = _transactionTypeDao.Update(transactionType);

                return BuildSuccessResult(updated);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Can't update transaction type: ", ex);
                return BuildFailedResult<TransactionType>("Can't update transaction type");
            }
        }

        public Result<Error, int> Delete(long id)
        {
            try
            {
                _logger.LogInformation("Deleting Transaction Type with id: {0}", id);
                
                var rows = _transactionTypeDao.Delete(id);
                _logger.LogInformation("Transaction Type with Id: {0} was deleted", id);

                return BuildSuccessResult(rows);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to delete Transaction type with id: {0} -> {1}", id, ex);
                return BuildFailedResult<int>("Can't delete transaction type");
            }
        }

    }
}