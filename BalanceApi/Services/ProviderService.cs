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
                var providerHolder = _providerDao.GetById(id);

                if (!providerHolder.HasValue)
                {
                    _logger.LogInformation("Provider with id: {0} not found", id);
                    return Result<Error, Provider>.ForFailure(BuildError("Provider Not Found"));
                }
                    
                return Result<Error, Provider>.ForSuccess(providerHolder.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to get provider with id: {0}", id, ex);
                return Result<Error, Provider>.ForFailure(BuildError("Can't get the Provider"));
            }
        }

        public Result<Error, List<Provider>> GetByCountry(string country)
        {
            try
            {
                _logger.LogInformation("Getting provider for country: {0}", country);
                var providers = _providerDao.GetByCountry(country);

                return Result<Error, List<Provider>>.ForSuccess(providers);
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't get providers by country: ", ex);
                return Result<Error, List<Provider>>.ForFailure(BuildError("Can't get provider by selected country"));
            }
        }

        public Result<Error, Provider> New(Provider provider) {
            try
            {
                var providerHolder = _providerDao.FindProvider(provider.Name, provider.Country);
                if (providerHolder.HasValue)
                {
                    _logger.LogError("Provider: {0} - {1} already exists", provider.Name, provider.Country);
                    return Result<Error, Provider>.ForFailure(BuildError("Provider already exists"));
                }
                
                _logger.LogInformation("Adding a new provider with name: {0} and country: {1}", provider.Name, provider.Country);
                var id = _providerDao.New(provider.Name, provider.Country);
                var newProviderHolder = _providerDao.GetById(id);

                return Result<Error, Provider>.ForSuccess(newProviderHolder.Value);
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to create a new provider due: {0}", ex);
                return Result<Error, Provider>.ForFailure(BuildError(ex.Message));
            }
        }

        public Result<Error, int> Delete(long id)
        {
            try
            {
                if (!ProviderExists(id))
                {
                    _logger.LogError("Provider with id: {0} not found", id);
                    return Result<Error, int>.ForFailure(BuildError("Provider not found"));
                }
                
                _logger.LogInformation("Deleting provider with id {0}", id);
                var rows = _providerDao.Delete(id);
                _logger.LogInformation("Provider with id {0} deleted", id);

                return Result<Error, int>.ForSuccess(rows);
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't delete provider: {0}", ex.StackTrace);
                return Result<Error, int>.ForFailure(BuildError("Can't delete provider"));
            }
        }

        public Result<Error, Provider> Update(Provider provider)
        {
            try
            {
                if (!ProviderExists(provider.Id))
                {
                    _logger.LogError("Provider with id: {0} not found", provider.Id);
                    return Result<Error, Provider>.ForFailure(BuildError("Provider not found"));
                    
                }

                _logger.LogInformation("Trying to update provider with id: {0}", provider.Id);
                var p = _providerDao.Update(provider);
                _logger.LogInformation("Provider udpated");

                return Result<Error, Provider>.ForSuccess(p);
            }
            catch(Exception ex)
            {
                _logger.LogError("Can't update provider: {0}", ex.StackTrace);
                return Result<Error, Provider>.ForFailure(BuildError("Can't update provider"));
            }
        }

        private bool ProviderExists(long id)
        {
            var providerHolder = _providerDao.GetById(id);

            return providerHolder.HasValue;
        }
        
    }
}
