using System.Collections.Generic;

using BalanceApi.Model.Domain;
using BalanceApi.Model.Data;
using Microsoft.Extensions.Logging;
using System;

namespace BalanceApi.Services
{
    public class AccountTypeService
    {
        private ILogger<AccountTypeService> logger;
        private IAccountTypeDao accountTypeDao;

        public AccountTypeService(ILogger<AccountTypeService> logger, 
            IAccountTypeDao accountTypeDao)
        {
            this.accountTypeDao = accountTypeDao;
            this.logger = logger;
        }

        public Result<Exception, List<AccountType>> GetAccountTypes()
        {
            try {
                logger.LogInformation("Getting all the account types");
                List<AccountType> list = accountTypeDao.findAll();
                return Result<Exception, List<AccountType>>.ForSuccess(list);
            }
            catch(Exception ex) {
                logger.LogError("Unable to get the account types, {0}", ex);
                return Result<Exception, List<AccountType>>.ForFailure(ex);
            }
        }

        public Result<Exception, AccountType> GetAccountTypeById(long id) {
            try {
                logger.LogInformation("Getting Account Type for id: {0}", id);
                AccountType accountType = accountTypeDao.findById(id);
                return Result<Exception, AccountType>.ForSuccess(accountType);
            } catch(Exception ex) {
                logger.LogError("Unable to get the account type: {0}, due: {1}", id, ex);
                return Result<Exception, AccountType>.ForFailure(ex);
            }
        }

        public Result<Exception, AccountType> newAccountType(AccountType accountType) {
            try {
                if((accountType != null) && (accountType.name != null)) {
                    long value = accountTypeDao.addNew(accountType.name);
                    return Result<Exception, AccountType>.ForSuccess(new AccountType(value, accountType.name));
                } else {
                    return Result<Exception, AccountType>.ForFailure(new Exception("Can't create account type"));
                }
            } catch(Exception ex) {
                logger.LogError("Unable to create a new account type due: {0}", ex);
                return Result<Exception, AccountType>.ForFailure(ex);
            }
        }

        public Result<Exception, int> deleteAccountType(long id) {
            try {
                logger.LogInformation("Trying to delete account type with id: {0}", id);
                int rows = accountTypeDao.delete(id);
                if(rows > 0) {
                    return Result<Exception, int>.ForSuccess(rows);
                } else {
                    return Result<Exception, int>.ForFailure(new Exception("No account type with id: {0} was deleted"));
                }
            } catch(Exception ex) {
                logger.LogError("Unable to delete account type with id: {0}, due: {1}", id, ex);
                return Result<Exception, int>.ForFailure(ex);
            }
        }

        public Result<Exception, AccountType> updateAccountType(AccountType accountType) {
            try {
                logger.LogInformation("Updating account type: {0}", accountType);
                AccountType at = accountTypeDao.update(accountType);
                return Result<Exception, AccountType>.ForSuccess(at);
            } catch(Exception ex) {
                logger.LogError("Unable to update account type with id: {0}, due: {1}", accountType, ex);
                return Result<Exception, AccountType>.ForFailure(ex);
            }
            
        }
    }
}
