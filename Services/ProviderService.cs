using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;

namespace BalanceApi.Services
{
    public class ProviderService
    {
        private readonly ILogger _logger;

        private readonly IProviderDao _providerDao;

        public ProviderService(ILogger<ProviderService> logger, IProviderDao providerDao) {
            this._logger = logger;
            this._providerDao = providerDao;
        }

        public Result<Exception, List<Provider>> GetAll() {
            try {
                _logger.LogInformation("Getting all the providers");
                var providers = _providerDao.GetAll();
                return Result<Exception, List<Provider>>.ForSuccess(providers);
            } catch(Exception ex) {
                return Result<Exception, List<Provider>>.ForFailure(ex);
            }
        }

        public Result<Exception, Provider> GetById(long id) {
            try {
                _logger.LogInformation("Getting provider with id: {0}", id);
                var provider = _providerDao.GetById(id);
                return Result<Exception, Provider>.ForSuccess(provider);
            } catch (Exception ex) {
                return Result<Exception, Provider>.ForFailure(ex);
            }
        }

        public Result<Exception, List<Provider>> GetByCountry(string country) {
            try {
                _logger.LogInformation("Getting provider for country: {0}", country);
                var providers = _providerDao.GetByCountry(country);
                return Result<Exception, List<Provider>>.ForSuccess(providers);
            } catch(Exception ex) {
                return Result<Exception, List<Provider>>.ForFailure(ex);
            }
        }

        private bool IsValid(Provider provider) {
            if(provider == null) {
                return false;
            }

            return (provider.name != null) && (provider.country != null);
        }

        public Result<Exception, Provider> New(Provider provider) {
            try {
                if(IsValid(provider)) {
                    _logger.LogInformation("Adding a new provider with name: {0} and country: {1}", provider.name, provider.country);
                    var id = _providerDao.New(provider.name, provider.country);
                    var newProvider = _providerDao.GetById(id);
                    return Result<Exception, Provider>.ForSuccess(newProvider);
                } else {
                    return Result<Exception, Provider>.ForFailure(new Exception("Validation exception"));
                }
            } catch(Exception ex) {
                _logger.LogError("Unable to create a new provider due: {0}", ex);
                return Result<Exception, Provider>.ForFailure(ex);
            }
        }

        public Result<Exception, int> Delete(long id) {
            try {
                _logger.LogInformation("Deleting provider with id {0}", id);
                var rows = _providerDao.Delete(id);
                _logger.LogInformation("Provider with id {0} deleted", id);
                return Result<Exception, int>.ForSuccess(rows);
            } catch(Exception ex) {
                return Result<Exception, int>.ForFailure(ex);
            }
        }

        public Result<Exception, Provider> Update(Provider provider) {
            try {
                _logger.LogInformation("Trying to update provider with id: {0}", provider.id);
                var p = _providerDao.Update(provider);
                _logger.LogInformation("Provider udpated");
                return Result<Exception, Provider>.ForSuccess(p);
            } catch(Exception ex) {
                return Result<Exception, Provider>.ForFailure(ex);
            }
        }
    }
}
