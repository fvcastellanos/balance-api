
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Model.Data 
{
    public interface ITransactionTypeDao
    {
        List<TransactionType> GetAll();
        TransactionType GetById(long id);
        long New(TransactionType transactionType);
        TransactionType Update(TransactionType transactionType);
        int Delete(long id);
    }
}