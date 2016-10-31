using System;
using System.Collections.Generic;
using BalanceApi.Domain;
using Microsoft.Extensions.Logging;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;

namespace BalanceApi.Services
{
    public class ProviderService
    {
        private ILogger logger;

        private IProviderDao providerDao;

        public ProviderService(ILogger<ProviderService> logger, IProviderDao providerDao) {
            this.logger = logger;
            this.providerDao = providerDao;
        }

        public Result GetAll() {
            try {
                logger.LogInformation("Getting all the providers");
                List<Provider> providers = providerDao.GetAll();
                return Result.forSuccess(providers);
            } catch(Exception ex) {
                return Result.forException(ex);
            }
        }
    }
}
