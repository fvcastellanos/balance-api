using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class AccountService : BaseService
    {
        private readonly IAccountDao _accountDao;
        private readonly IProviderDao _providerDao;
        private readonly IAccountTypeDao _accountTypeDao;
        private readonly ILogger _logger;

        public AccountService(ILogger<AccountService> logger, IAccountDao accountDao,
                IProviderDao providerDao, IAccountTypeDao accountTypeDao)
        {
            _accountDao = accountDao;
            _providerDao = providerDao;
            _accountTypeDao = accountTypeDao;
            _logger = logger;
        }
        
        public Result<Error, ICollection<Account>> GetAll()
        {
            try
            {
                _logger.LogInformation("Getting accounts");
                var accounts = _accountDao.GetAll();

                return Result<Error, ICollection<Account>>.ForSuccess(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't get accounts", ex);
                return Result<Error, ICollection<Account>>.ForFailure(BuildError("Can't get accounts"));
            }
        }

        public Result<Error, Account> GetById(long id)
        {
            try 
            {
                _logger.LogInformation("Getting account using id: {0}", id);
                var accountHolder = _accountDao.GetById(id);
                
                if (!accountHolder.HasValue) return Result<Error, Account>.ForFailure(BuildError("Account not found"));

                return Result<Error, Account>.ForSuccess(accountHolder.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't get account by id: {0}", id, ex);
                return Result<Error, Account>.ForFailure(BuildError("Can't get account"));
            }
        }

        public Result<Error, Account> AddNew(long accountTypeId, long providerId, string name, string number)
        {
            try
            {
                var accountHolder = _accountDao.GetAccount(accountTypeId, providerId, number);

                if (accountHolder.HasValue) return Result<Error, Account>.ForFailure(BuildError("Looks like the account already exists"));

                var id = _accountDao.CreateAccount(accountTypeId, providerId, name, number);

                var createdAccount = _accountDao.GetById(id).Value;
                return Result<Error, Account>.ForSuccess(createdAccount);
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't create a new account: ", ex);
                return Result<Error, Account>.ForFailure(BuildError("Can't create new account"));
            }
        }

        public Result<Error, Account> Update(Account account)
        {
            try 
            {
                _logger.LogInformation("Getting account: {0] - {1}", account.Id, account.AccountNumber);
                var storedAccountHolder = _accountDao.GetById(account.Id);

                if (storedAccountHolder.HasValue) 
                {
                    _logger.LogError("Getting account: {0] - {1} not found", account.Id, account.AccountNumber);
                    return Result<Error, Account>.ForFailure(BuildError("Account not found"));
                }

                if (!ProvierExist(account.ProviderId)) return Result<Error, Account>.ForFailure(BuildError("Provider not found"));

                if (!AccountTypeExist(account.AccountTypeId))
                    return Result<Error, Account>.ForFailure(BuildError("Account type not found"));
                
                _logger.LogInformation("Updating account: {0} - {1}", account.Id, account.Name);
                _accountDao.Update(account);

                return Result<Error, Account>.ForSuccess(account);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't update account: {0}", account.Id, ex);
                return Result<Error, Account>.ForFailure(BuildError("Can't update account"));
            }
        }

        private bool ProvierExist(long id)
        {
            _logger.LogInformation("Getting provider: {0}", id);
            var providerHolder = _providerDao.GetById(id);

            return providerHolder.HasValue;
        }

        private bool AccountTypeExist(long id)
        {
            _logger.LogInformation("Getting account type: {0}", id);
            var accountTypeHolder = _accountTypeDao.FindById(id);

            return accountTypeHolder.HasValue;
        }
    }
}
