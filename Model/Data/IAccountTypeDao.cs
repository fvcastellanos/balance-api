
using System.Collections.Generic;

using Model.Domain;

namespace Model.Data
{
    public interface IAccountTypeDao
    {
        List<AccountType> findAll();
        AccountType findById(long id);
        AccountType findByName(string name);
    }
}
