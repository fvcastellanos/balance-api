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

        public Result GetById(long id) {
            try {
                logger.LogInformation("Getting provider with id: {0}", id);
                Provider provider = providerDao.GetById(id);
                return Result.forSuccess(provider);
            } catch (Exception ex) {
                return Result.forException(ex);
            }
        }

        public Result GetByCountry(string country) {
            try {
                logger.LogInformation("Getting provider for country: {0}", country);
                List<Provider> providers = providerDao.GetByCountry(country);
                return Result.forSuccess(providers);
            } catch(Exception ex) {
                return Result.forException(ex);
            }
        }

        private bool IsValid(Provider provider) {
            if(provider == null) {
                return false;
            }

            if((provider.name == null) || (provider.country == null)) {
                return false;
            }

            return true;
        }

        public Result New(Provider provider) {
            try {
                if(IsValid(provider)) {
                    logger.LogInformation("Adding a new provider with name: {0} and country: {1}", provider.name, provider.country);
                    long id = providerDao.New(provider.name, provider.country);
                    Provider newProvider = providerDao.GetById(id);
                    return Result.forSuccess(newProvider);
                } else {
                    return Result.forException(new Exception("Validation exception"));
                }
            } catch(Exception ex) {
                logger.LogError("Unable to create a new provider due: {0}", ex);
                return Result.forException(ex);
            }
        }

        
    }
}
