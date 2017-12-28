using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class TransactionService : BaseService
    {
        private readonly ILogger _logger;
        private readonly ITransactionDao _transactionDao;

        public TransactionService(ILogger<TransactionService> logger, ITransactionDao transactionDao)
        {
            _logger = logger;
            _transactionDao = transactionDao;
        }

        public Result<Error, IEnumerable<Transaction>> GetUsingDateRange(DateTime from, DateTime to)
        {
            try
            {
                var transactions = _transactionDao.GetByDateRange(from, to);
                
                return Result<Error, IEnumerable<Transaction>>.ForSuccess(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't get transactions: {0}", ex);
                return Result<Error, IEnumerable<Transaction>>.ForFailure(BuildError("Can't get transactions"));
            }
        }
    }
}