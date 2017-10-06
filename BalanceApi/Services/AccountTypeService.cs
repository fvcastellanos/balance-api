using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class AccountTypeService : BaseService
    {
        private readonly ILogger<AccountTypeService> _logger;
        private readonly IAccountTypeDao _accountTypeDao;

        public AccountTypeService(ILogger<AccountTypeService> logger, 
            IAccountTypeDao accountTypeDao)
        {
            _accountTypeDao = accountTypeDao;
            _logger = logger;
        }

        public Result<Error, List<AccountType>> GetAccountTypes()
        {
            try
            {
                _logger.LogInformation("Getting all the account types");
                var list = _accountTypeDao.FindAll();

                return Result<Error, List<AccountType>>.ForSuccess(list);
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to get the account types, {0}", ex);
                return Result<Error, List<AccountType>>.ForFailure(BuildError("Can't get the account types"));
            }
        }

        public Result<Error, AccountType> GetAccountTypeById(long id)
        {
            try
            {
                _logger.LogInformation("Getting Account Type for id: {0}", id);
                var accountType = _accountTypeDao.FindById(id);

                return Result<Error, AccountType>.ForSuccess(accountType);
            } catch(Exception ex)
            {
                _logger.LogError("Unable to get the account type: {0}, due: {1}", id, ex);
                return Result<Error, AccountType>.ForFailure(BuildError("Can't get the account type requested"));
            }
        }

        public Result<Error, AccountType> NewAccountType(string name)
        {
            try
            {
                if (name == null) return Result<Error, AccountType>.ForFailure(new Error("Can't create account type"));
                
                _logger.LogInformation("Adding new account type: {0}", name);
                var value = _accountTypeDao.AddNew(name);
                
                return Result<Error, AccountType>.ForSuccess(new AccountType(value, name));
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to create a new account type due: {0}", ex);
                return Result<Error, AccountType>.ForFailure(BuildError("Can't create the account type"));
            }
        }

        public Result<Exception, int> DeleteAccountType(long id)
        {
            try
            {
                _logger.LogInformation("Trying to delete account type with id: {0}", id);
                var rows = _accountTypeDao.Delete(id);

                if (rows > 0)
                {
                    return Result<Exception, int>.ForSuccess(rows);
                }

                return Result<Exception, int>.ForFailure(new Exception("No account type with id: {0} was deleted"));
            }
            catch(Exception ex) {
                _logger.LogError("Unable to delete account type with id: {0}, due: {1}", id, ex);
                return Result<Exception, int>.ForFailure(ex);
            }
        }

        public Result<Exception, AccountType> UpdateAccountType(AccountType accountType) {
            try {
                _logger.LogInformation("Updating account type: {0}", accountType);
                AccountType at = _accountTypeDao.Update(accountType);
                return Result<Exception, AccountType>.ForSuccess(at);
            } catch(Exception ex) {
                _logger.LogError("Unable to update account type with id: {0}, due: {1}", accountType, ex);
                return Result<Exception, AccountType>.ForFailure(ex);
            }
            
        }
    }
}
