
using System.Collections.Generic;
using BalanceApi.Model.Domain;
using Microsoft.CodeAnalysis;

namespace BalanceApi.Model.Data
{
    public interface IAccountDao
    {
        ICollection<Account> GetAll();
        Optional<Account> GetById(long id);
        Optional<Account> GetByAccountNumber(string number);
        Optional<Account> GetAccount(long accountTypeId, long providerId, string accountNumber);
        long CreateAccount(long accountTypeId, long providerId, string name, string accountNumber);
        void Update(Account account);
    }
}