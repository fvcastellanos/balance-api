
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
    public class AccountDao : BaseDao, IAccountDao
    {
        private readonly ILogger _logger;
        
        public AccountDao(IOptions<AppSettings> appSettings, ILogger<AccountDao> logger) : base(appSettings, logger)
        {
            _logger = logger;
        }

        public ICollection<Account> GetAll()
        {
            try 
            {
                var query = "select a.id, a.account_type_id AccountTypeId, at.name AccountType, a.provider_id ProviderId, p.name Provider, " +
                    "  a.name, a.account_number AccountNumber, a.balance " +
                    " from account a " +
                    "   inner join account_type at on a.account_type_id = at.id " +
                    "   inner join provider p on a.provider_id = p.id";

                _logger.LogInformation("Getting all the accounts from DB");
                var accounts = GetConnection().Query<Account>(query).AsList();

                return accounts;
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't get accounts from DB", ex);
                throw;
            }
        }

        public Account GetById(long id)
        {
            try 
            {
                var query = "select a.id, a.account_type_id AccountTypeId, at.name AccountType, a.provider_id ProviderId, p.name Provider, " +
                    "  a.name, a.account_number AccountNumber, a.balance " +
                    " from account a " +
                    "   inner join account_type at on a.account_type_id = at.id " +
                    "   inner join provider p on a.provider_id = p.id " +
                    " where a.id = @Id";

                _logger.LogInformation("Getting account with id: {0}", id);
                var account = GetConnection().Query<Account>(query, new { Id = id }).SingleOrDefault<Account>();

                return account;
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't get account from DB", ex);
                throw;
            }
        }

        public Account GetByAccountNumber(string number)
        {
            try 
            {
                var query = "select a.id, a.account_type_id AccountTypeId, at.name AccountType, a.provider_id ProviderId, p.name Provider, " +
                    "  a.name, a.account_number AccountNumber, a.balance " +
                    " from account a " +
                    "   inner join account_type at on a.account_type_id = at.id " +
                    "   inner join provider p on a.provider_id = p.id " +
                    " where a.account_number = @Number";

                _logger.LogInformation("Getting account with number: {0}", number);
                var account = GetConnection().Query<Account>(query, new { Number = number }).SingleOrDefault<Account>();

                return account;
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't get account from DB", ex);
                throw;
            }
        }

        public Account GetAccount(long accountTypeId, long providerId, string accountNumber)
        {
            try 
            {
                var query = "select a.id, a.account_type_id AccountTypeId, at.name AccountType, a.provider_id ProviderId, p.name Provider, " +
                    "  a.name, a.account_number AccountNumber, a.balance " +
                    " from account a " +
                    "   inner join account_type at on a.account_type_id = at.id " +
                    "   inner join provider p on a.provider_id = p.id " +
                    " where a.account_number = @Number and a.account_type_id = @AccountTypeId and a.provider_id = @ProviderId";

                _logger.LogInformation("Getting account [number: {0}, type_id: {1}, provider_id: {2}", accountNumber, accountTypeId, providerId);
                var account = GetConnection().Query<Account>(query, new { Number = accountNumber, AccountTypeId = accountTypeId, ProviderId = providerId })
                        .SingleOrDefault<Account>();

                return account;
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't get account from DB", ex);
                throw;
            }
        }

        public long CreateAccount(long accountTypeId, long providerId, string name, string accountNumber)
        {
            try 
            {
                var query = "insert into account (account_type_id, provider_id, name, account_number) " +
                    "  values (@AccountTypeId, @ProviderId, @Name, @AccountNumber) ";

                _logger.LogInformation("Creating account [number: {0}, type_id: {1}, provider_id: {2}, name: {3}", accountNumber, accountTypeId, providerId, name);
                var rows = GetConnection().Execute(query, new { AccountNumber = accountNumber, AccountTypeId = accountTypeId, ProviderId = providerId, Name = name });
                if (rows > 0) return GetLasInsertedId();

                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't insert new account DB", ex);
                throw;
            }
        }
    }
}