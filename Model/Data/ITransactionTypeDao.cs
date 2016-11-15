
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Model.Data 
{
    public interface ITransactionTypeDao
    {
        List<TransactionType> GetAll();
    }
}