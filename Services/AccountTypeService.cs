using System.Collections.Generic;

using BalanceApi.Model.Domain;
using BalanceApi.Model.Data;
using Microsoft.Extensions.Logging;
using System;
using BalanceApi.Domain;

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

        public Result GetAccountTypes()
        {
            try {
                logger.LogInformation("Getting all the account types");
                List<AccountType> list = accountTypeDao.findAll();
                return Result.forSuccess(list);
            }
            catch(Exception ex) {
                logger.LogError("Unable to get the account types, {0}", ex);
                return Result.forException(ex);
            }
        }
    }
}
