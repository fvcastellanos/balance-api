using System;
using System.Collections.Generic;
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

        public Result<Exception, List<Provider>> GetAll() {
            try {
                logger.LogInformation("Getting all the providers");
                List<Provider> providers = providerDao.GetAll();
                return Result<Exception, List<Provider>>.ForSuccess(providers);
            } catch(Exception ex) {
                return Result<Exception, List<Provider>>.ForFailure(ex);
            }
        }

        public Result<Exception, Provider> GetById(long id) {
            try {
                logger.LogInformation("Getting provider with id: {0}", id);
                Provider provider = providerDao.GetById(id);
                return Result<Exception, Provider>.ForSuccess(provider);
            } catch (Exception ex) {
                return Result<Exception, Provider>.ForFailure(ex);
            }
        }

        public Result<Exception, List<Provider>> GetByCountry(string country) {
            try {
                logger.LogInformation("Getting provider for country: {0}", country);
                List<Provider> providers = providerDao.GetByCountry(country);
                return Result<Exception, List<Provider>>.ForSuccess(providers);
            } catch(Exception ex) {
                return Result<Exception, List<Provider>>.ForFailure(ex);
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

        public Result<Exception, Provider> New(Provider provider) {
            try {
                if(IsValid(provider)) {
                    logger.LogInformation("Adding a new provider with name: {0} and country: {1}", provider.name, provider.country);
                    long id = providerDao.New(provider.name, provider.country);
                    Provider newProvider = providerDao.GetById(id);
                    return Result<Exception, Provider>.ForSuccess(newProvider);
                } else {
                    return Result<Exception, Provider>.ForFailure(new Exception("Validation exception"));
                }
            } catch(Exception ex) {
                logger.LogError("Unable to create a new provider due: {0}", ex);
                return Result<Exception, Provider>.ForFailure(ex);
            }
        }

        public Result<Exception, int> Delete(long id) {
            try {
                logger.LogInformation("Deleting provider with id {0}", id);
                int rows = providerDao.Delete(id);
                logger.LogInformation("Provider with id {0} deleted", id);
                return Result<Exception, int>.ForSuccess(rows);
            } catch(Exception ex) {
                return Result<Exception, int>.ForFailure(ex);
            }
        }

        public Result<Exception, Provider> Update(Provider provider) {
            try {
                logger.LogInformation("Trying to update provider with id: {0}", provider.id);
                Provider p = providerDao.Update(provider);
                logger.LogInformation("Provider udpated");
                return Result<Exception, Provider>.ForSuccess(p);
            } catch(Exception ex) {
                return Result<Exception, Provider>.ForFailure(ex);
            }
        }
    }
}
