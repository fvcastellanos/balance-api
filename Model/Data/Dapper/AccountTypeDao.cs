using System;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using BalanceApi.Model.Domain;
using BalanceApi.Configuration;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Model.Data.Dapper
{
    public class AccountTypeDao : BaseDao, IAccountTypeDao
    {
        private ILogger<AccountTypeDao> logger;

        public AccountTypeDao(AppSettingsHelper settingsHelper, 
            ILogger<AccountTypeDao> logger) : base(settingsHelper)
        {
            this.logger = logger;
        }

        public List<AccountType> findAll()
        {
            try
            {
                return getConnection().Query<AccountType>("select account_type_id id, name from account_type").AsList();
            }
            catch(Exception ex)
            {
                logger.LogError("Unable to get the account types", ex);
            }

            return null;
        }

        public AccountType findById(long id)
        {
            try
            {
                return getConnection().Query<AccountType>("select account_type_id id, name from account_type " +
                    " where account_type_id = @Id", new { Id = id }).Single<AccountType>();
            }
            catch(Exception ex)
            {
                logger.LogError("Unable to perform the query", ex);
            }

            return null;
        }

        public AccountType findByName(string name)
        {
            try
            {
                return getConnection().Query<AccountType>("select account_type_id id, name from account_type " +
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
