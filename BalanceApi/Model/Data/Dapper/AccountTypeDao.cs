using System;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;
using BalanceApi.Domain;
using Microsoft.Extensions.Options;

namespace BalanceApi.Model.Data.Dapper
{
    public class AccountTypeDao : BaseDao, IAccountTypeDao
    {
        private ILogger<AccountTypeDao> logger;

        public AccountTypeDao(IOptions<AppSettings> settings, 
            ILogger<AccountTypeDao> logger) : base(settings, logger)
        {
            this.logger = logger;
        }

        public List<AccountType> FindAll()
        {
            try
            {
                logger.LogInformation("Getting account types");
                return GetConnection().Query<AccountType>("select id, name from account_type").AsList();
            }
            catch(Exception ex)
            {
                logger.LogError("Unable to get the account types", ex);
                throw;
            }
        }

        public AccountType FindById(long id)
        {
            try
            {
                logger.LogInformation("Getting account type with Id: {0}", id);
                return GetConnection().Query<AccountType>("select id, name from account_type " +
                    " where id = @Id", new { Id = id }).SingleOrDefault<AccountType>();
            }
            catch(Exception ex)
            {
                logger.LogError("Unable to perform the query", ex);
                throw ex;
            }
        }

        public AccountType FindByName(string name)
        {
            try
            {
                logger.LogInformation("Getting account type with name: {0}", name);
                return GetConnection().Query<AccountType>("select id, name from account_type " +
                    " where name = @Name", new { Name = name }).SingleOrDefault<AccountType>();
            }
            catch (Exception ex)
            {
                logger.LogError("Unable to perform the query", ex);
                throw ex;
            }
        }

        public long AddNew(string name) {
            try {
                long id = 0;
                int rows = GetConnection().Execute("insert into account_type (name) values (@Name)", new { Name = name });
                if(rows > 0) {
                    id = GetConnection().Query<long>("select LAST_INSERT_ID()").Single();
                }

                return id;
            } catch(Exception ex) {
                logger.LogError("Unable to create an account type of name: {0}", name);
                throw ex;
            }
        }

        public int Delete(long id) {
            try {
                int rows = GetConnection().Execute("delete from account_type where id = @Id", new {Id = id});
                return rows;
            } catch(Exception ex) {
                logger.LogError("Unable to delete account type with id: {0}", id);
                throw ex;
            }
        }

        public AccountType Update(AccountType accountType) {
            try {
                GetConnection().Execute("update account_type set name = @Name where id = @Id", 
                    new {Name = accountType.Name, Id = accountType.Id});

                return FindById(accountType.Id);
            } catch(Exception ex) {
                logger.LogError("Unable to update account type due: {0}", ex.Message);
                throw ex;
            }
        }


    }
}
