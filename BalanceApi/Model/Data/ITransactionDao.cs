using System;
using System.Collections.Generic;
using BalanceApi.Model.Domain;
using Microsoft.CodeAnalysis;

namespace BalanceApi.Model.Data
{
    public interface ITransactionDao
    {
        IEnumerable<Transaction> GetByDateRange(DateTime from, DateTime to);
        Optional<Transaction> GetById(long id);
        long Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(long id);
    }
}