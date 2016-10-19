
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Model.Data
{
    public interface IAccountTypeDao
    {
        List<AccountType> findAll();
        AccountType findById(long id);
        AccountType findByName(string name);
    }
}
