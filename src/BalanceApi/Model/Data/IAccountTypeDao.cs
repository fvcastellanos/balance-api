
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Model.Data
{
    public interface IAccountTypeDao
    {
        List<AccountType> FindAll();
        AccountType FindById(long id);
        AccountType FindByName(string name);
        long AddNew(string name);
        int Delete(long id);
        AccountType Update(AccountType accountType);
    }
}
