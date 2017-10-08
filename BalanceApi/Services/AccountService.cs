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

                return BuildSuccessResult(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't get accounts", ex);
                return BuildFailedResult<ICollection<Account>>("Can't get accounts");
            }
        }

        public Result<Error, Account> GetById(long id)
        {
            try 
            {
                _logger.LogInformation("Getting account using id: {0}", id);
                var account = _accountDao.GetById(id);

                return BuildSuccessResult(account);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't get account by id: {0}", id, ex);
                return BuildFailedResult<Account>("Can't get account");
            }
        }

        public Result<Error, Account> AddNew(long accountTypeId, long providerId, string name, string number)
        {
            try
            {
                var account = _accountDao.GetAccount(accountTypeId, providerId, number);

                if (account != null) return BuildFailedResult<Account>("Looks like the account already exists");

                var id = _accountDao.CreateAccount(accountTypeId, providerId, name, number);

                if (id == 0) return BuildFailedResult<Account>("Account was not created");

                var createdAccount = _accountDao.GetById(id);
                return BuildSuccessResult(createdAccount);
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't create a new account: ", ex);
                return BuildFailedResult<Account>("Can't create new account");
            }
        }

        public Result<Error, Account> Update(Account account)
        {
            try 
            {
                _logger.LogInformation("Getting account: {0] - {1}", account.Id, account.AccountNumber);
                var storedAccount = _accountDao.GetById(account.Id);

                if (storedAccount == null) 
                {
                    _logger.LogError("Getting account: {0] - {1} not found", account.Id, account.AccountNumber);
                    return BuildFailedResult<Account>("Account not found");
                }

                if (!ProvierExists(account.ProviderId)) return BuildFailedResult<Account>("Provider not found");

                if (!AccountTypeExists(account.AccountTypeId))
                    return BuildFailedResult<Account>("Account type not found");
                
                _logger.LogInformation("Updating account: {0} - {1}", account.Id, account.Name);
                var updatedAccount = _accountDao.Update(account);

                return updatedAccount != null ? BuildSuccessResult(updatedAccount) : BuildFailedResult<Account>("Account not updated");
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't update account: {0}", account.Id, ex);
                return BuildFailedResult<Account>("Can't update account");
            }
        }

        private bool ProvierExists(long id)
        {
            _logger.LogInformation("Getting provider: {0}", id);
            var provider = _providerDao.GetById(id);
            
            if (provider != null) return true;
            
            _logger.LogError("Provider {0} not found", id);
            return false;
        }

        private bool AccountTypeExists(long id)
        {
            _logger.LogInformation("Getting account type: {0}", id);
            var accountType = _accountTypeDao.FindById(id);

            if (accountType != null) return true;
            
            _logger.LogError("Account type {0} not found", id);
            return false;
        }
        
        


    }
}
