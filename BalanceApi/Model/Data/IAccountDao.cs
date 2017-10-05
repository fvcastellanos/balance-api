
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Model.Data
{
    public interface IAccountDao
    {
        ICollection<Account> GetAll();
        Account GetById(long id);
        Account GetByAccountNumber(string number);
    }
}