using System.Collections.Generic;
using BalanceApi.Model.Domain;
using Microsoft.CodeAnalysis;

namespace BalanceApi.Model.Data
{
    public interface IProviderDao
    {
        List<Provider> GetAll();
        Optional<Provider> GetById(long id);
        List<Provider> GetByCountry(string country);
        Optional<Provider> FindProvider(string name, string country);
        long New(string name, string country);
        int Delete(long id);
        Provider Update(Provider provider);
    }
}
