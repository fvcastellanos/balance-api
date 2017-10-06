
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Model.Data
{
    public interface IAccountDao
    {
        ICollection<Account> GetAll();
        Account GetById(long id);
        Account GetByAccountNumber(string number);
        Account GetAccount(long accountTypeId, long providerId, string accountNumber);
        long CreateAccount(long accountTypeId, long providerId, string name, string accountNumber);
    }
}