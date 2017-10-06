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
        private readonly ILogger _logger;

        public AccountService(ILogger<AccountService> logger, IAccountDao accountDao)
        {
            _accountDao = accountDao;
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

    }
}
