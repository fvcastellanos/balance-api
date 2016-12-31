using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Validators
{
    public class TransactionTypeValidator : IModelValidator<TransactionType>
    {
        private List<string> errors;

        public TransactionTypeValidator()
        {
            errors = new List<string>();
        }

        public Result<List<string>, TransactionType> validate(TransactionType obj)
        {
            if(obj == null)
            {
                errors.Add("Transaction type is null");
                return Result<List<string>, TransactionType>.ForFailure(errors);
            }

            if((obj.name == null) || (obj.Equals("")))
            {
                errors.Add("Transaction type name is required");
                return Result<List<string>, TransactionType>.ForFailure(errors);
            }

            // Should validate if the type already exists ??

            return Result<List<string>, TransactionType>.ForSuccess(obj);
        }
    }
}