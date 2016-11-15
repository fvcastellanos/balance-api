
using System;
using System.Collections.Generic;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dapper;

namespace BalanceApi.Model.Data.Dapper
{
    public class TransactionTypeDao : BaseDao, ITransactionTypeDao
    {
        private ILogger<TransactionTypeDao> logger;
        public TransactionTypeDao(IOptions<AppSettings> appSettings, ILogger<TransactionTypeDao> Logger) : base(appSettings, Logger)
        {
            this.logger = Logger;
        }

        public List<TransactionType> GetAll()
        {
            try
            {
                logger.LogInformation("Getting the transactions types from DB");
                return getConnection().Query<TransactionType>("select * from account_type").AsList();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}