
using System.Collections.Generic;
using BalanceApi.Model.Domain;
using Microsoft.CodeAnalysis;

namespace BalanceApi.Model.Data
{
    public interface IAccountTypeDao
    {
        List<AccountType> FindAll();
        Optional<AccountType> FindById(long id);
        long AddNew(string name);
        int Delete(long id);
        AccountType Update(AccountType accountType);
    }
}
