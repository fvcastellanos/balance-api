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

        public List<AccountType> findAll()
        {
            try
            {
                logger.LogInformation("Getting account types");
                return getConnection().Query<AccountType>("select id, name from account_type").AsList();
            }
            catch(Exception ex)
            {
                logger.LogError("Unable to get the account types", ex);
                throw ex;
            }
        }

        public AccountType findById(long id)
        {
            try
            {
                logger.LogInformation("Getting account type with Id: {0}", id);
                return getConnection().Query<AccountType>("select id, name from account_type " +
                    " where account_type_id = @Id", new { Id = id }).Single<AccountType>();
            }
            catch(Exception ex)
            {
                logger.LogError("Unable to perform the query", ex);
                throw ex;
            }
        }

        public AccountType findByName(string name)
        {
            try
            {
                logger.LogInformation("Getting account type with name: {0}", name);
                return getConnection().Query<AccountType>("select id, name from account_type " +
                    " where name = @Name", new { Name = name }).Single<AccountType>();
            }
            catch (Exception ex)
            {
                logger.LogError("Unable to perform the query", ex);
            }

            return null;
        }
    }
}
