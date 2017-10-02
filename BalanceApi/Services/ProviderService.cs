using System;
using System.Collections.Generic;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Services
{
    public class ProviderService : BaseService
    {
        private readonly ILogger _logger;

        private readonly IProviderDao _providerDao;

        public ProviderService(ILogger<ProviderService> logger, IProviderDao providerDao)
        {
            _logger = logger;
            _providerDao = providerDao;
        }

        public Result<Error, List<Provider>> GetAll()
        {
            try
            {
                _logger.LogInformation("Getting all the providers");
                var providers = _providerDao.GetAll();

                return Result<Error, List<Provider>>.ForSuccess(providers);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception: ", ex);
                return Result<Error, List<Provider>>.ForFailure(BuildError(ex.Message));
            }
        }

        public Result<Error, Provider> GetById(long id)
        {
            try
            {
                _logger.LogInformation("Getting provider with id: {0}", id);
                var provider = _providerDao.GetById(id);

                return Result<Error, Provider>.ForSuccess(provider);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to get provider with id: {0}", id, ex);
                return Result<Error, Provider>.ForFailure(BuildError("Can't get the Provider"));
            }
        }

        public Result<Exception, List<Provider>> GetByCountry(string country)
        {
            try
            {
                _logger.LogInformation("Getting provider for country: {0}", country);
                var providers = _providerDao.GetByCountry(country);

                return Result<Exception, List<Provider>>.ForSuccess(providers);
            }
            catch(Exception ex)
            {
                return Result<Exception, List<Provider>>.ForFailure(ex);
            }
        }

        public Result<Error, Provider> New(Provider provider) {
            try
            {
                _logger.LogInformation("Adding a new provider with name: {0} and country: {1}", provider.Name, provider.Country);
                var id = _providerDao.New(provider.Name, provider.Country);
                var newProvider = _providerDao.GetById(id);

                return Result<Error, Provider>.ForSuccess(newProvider);
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to create a new provider due: {0}", ex);
                return Result<Error, Provider>.ForFailure(BuildError(ex.Message));
            }
        }

        public Result<Exception, int> Delete(long id)
        {
            try
            {
                _logger.LogInformation("Deleting provider with id {0}", id);
                var rows = _providerDao.Delete(id);
                _logger.LogInformation("Provider with id {0} deleted", id);

                return Result<Exception, int>.ForSuccess(rows);
            }
            catch(Exception ex)
            {
                return Result<Exception, int>.ForFailure(ex);
            }
        }

        public Result<Exception, Provider> Update(Provider provider)
        {
            try
            {
                _logger.LogInformation("Trying to update provider with id: {0}", provider.Id);
                var p = _providerDao.Update(provider);
                _logger.LogInformation("Provider udpated");

                return Result<Exception, Provider>.ForSuccess(p);
            }
            catch(Exception ex)
            {
                return Result<Exception, Provider>.ForFailure(ex);
            }
        }
    }
}
