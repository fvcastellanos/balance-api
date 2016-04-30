using System.Collections.Generic;

using Model.Domain;
using Model.Data;
using Microsoft.Extensions.Logging;
using System;

namespace Services
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

        public Response<List<AccountType>> GetAccountTypes()
        {
            Response<List<AccountType>> response = null;
            try
            {
                List<AccountType> list = accountTypeDao.findAll();
                response = new Response<List<AccountType>>(200, "OK", list);
            }
            catch(Exception ex)
            {
                logger.LogError("Unable to get account types", ex);
                response = new Response<List<AccountType>>(500, "ERROR", null);
            }
            return response;
        }
    }
}
