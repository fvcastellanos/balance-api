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

    }
}
