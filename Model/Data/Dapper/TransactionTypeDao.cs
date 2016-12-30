
using System;
using System.Collections.Generic;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dapper;
using System.Linq;

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
                return getConnection().Query<TransactionType>("select * from transaction_type").AsList();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public TransactionType GetById(long id)
        {
            try 
            {
                return getConnection().Query<TransactionType>("select * from transaction_type where id = @Id", 
                    new { Id = id }).SingleOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public long New(TransactionType transactionType)
        {
            try 
            {
                long id = 0;
                logger.LogInformation("Adding a new transaction type with name: {0} and type: {1}", transactionType.name, transactionType.credit);
                var rows = getConnection().Execute("insert into transaction_type (name, credit) values (@Name, @Credit)", 
                    new { Name = transactionType.name, Credit = transactionType.credit });
                
                if(rows > 0) 
                {
                    id = GetLasInsertedId();
                }

                return id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public TransactionType Update(TransactionType transactionType)
        {
            try 
            {
                getConnection().Execute("update transaction_type set name = @Name, credit = @Credit where id = @Id", 
                    new { Name = transactionType.name, Credit = transactionType.credit, Id = transactionType.id });
                
                return GetById(transactionType.id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(long id)
        {
            try
            {
                int rows = getConnection().Execute("delete from transaction_type where id = @Id",
                    new { Id = id });

                return rows;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}