using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Model.Data
{
    public interface IProviderDao
    {
        List<Provider> GetAll();
        Provider GetById(long id);
        List<Provider> GetByCountry(string country);
    }
}
