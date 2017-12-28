using System;
using System.Collections.Generic;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using Dapper;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BalanceApi.Model.Data.Dapper
{
    public class TransactionDao : BaseDao, ITransactionDao
    {
        public TransactionDao(IOptions<AppSettings> appSettings, ILogger<TransactionDao> logger) : base(appSettings, logger)
        {
        }

        public IEnumerable<Transaction> GetByDateRange(DateTime from, DateTime to)
        {
            using (var db = GetConnection())
            {
                var sql = "select t.id, t.transaction_type_id TransactionTypeId, t.account_id AccountId, " +
                          " t.date, t.description, t.amount, t.currency, tt.name TransactionType, a.account_number AccountNumber " +
                          " from transaction t " +
                          "  inner join transaction_type tt on t.transaction_type_id = tt.id " +
                          "  inner join account a on t.account_id = a.id " +
                          " where t.date between @From and @To;";

                var transactions = (db.Query<Transaction>(sql, new {From = from, To = to})).AsList();
                
                return transactions;
            }
        }

        public Optional<Transaction> GetById(long id)
        {
            using (var db = GetConnection())
            {
                var sql = "select t.id, t.transaction_type_id TransactionTypeId, t.account_id AccountId, " +
                          " t.date, t.description, t.amount, t.currency, tt.name TransactionType, a.account_number AccountNumber " +
                          " from transaction t " +
                          "  inner join transaction_type tt on t.transaction_type_id = tt.id " +
                          "  inner join account a on t.account_id = a.id " +
                          " where t.id = @Id;";

                var transaction = db.QuerySingleOrDefault<Transaction>(sql, new {Id = id});
                
                return new Optional<Transaction>(transaction);
            }
        }

        public long Add(Transaction transaction)
        {
            using (var db = GetConnection())
            {
                var sql = "insert into transaction " +
                          " (transaction_type_id, account_id, date, description, amount, currency) " +
                          " values " +
                          " (@TransactionTypeId, @AccountId, @Date, @Description, @Amount, @Currency)";

                db.Execute(sql, 
                    new {
                        TransactionTypeId = transaction.TransactionTypeId,
                        AccountId = transaction.AccountId,
                        Date = transaction.Date,
                        Description = transaction.Description,
                        Amount = transaction.Amount,
                        Currency = transaction.Currency
                    });

                return GetLasInsertedId();
            }
        }

        public void Update(Transaction transaction)
        {
            using (var db = GetConnection())
            {
                var sql = "update transaction " +
                          " set " +
                          "  transaction_type_id = @TransactionTypeId, " +
                          "  account_id = @AccountId, " +
                          "  date = @Date, " +
                          "  description = @Description, " +
                          "  amount = @Amount, " +
                          "  currency = @Currency) " +
                          " where id = @Id";

                db.Execute(sql, 
                    new {
                        Id = transaction.Id,
                        TransactionTypeId = transaction.TransactionTypeId,
                        AccountId = transaction.AccountId,
                        Date = transaction.Date,
                        Description = transaction.Description,
                        Amount = transaction.Amount,
                        Currency = transaction.Currency
                    });
            }
        }

        public void Delete(long id)
        {
            using (var db = GetConnection())
            {
                db.Execute("delete from transaction where id = @Id", new {Id = id});
            }
        }
    }
}