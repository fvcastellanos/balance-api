using BalanceApi.Model.Domain;

namespace BalanceApi.Validators
{
    public class TransactionTypeValidator : IModelValidator<TransactionType>
    {
        private readonly ValidationResult _result;
        public TransactionTypeValidator()
        {
            _result = new ValidationResult();
        }

        public ValidationResult Validate(TransactionType obj)
        {
            if (obj == null)
            {
                _result.Add("Transaction type is null");
                return _result;
            }

            if (string.IsNullOrEmpty(obj.Name))
            {
                _result.Add("Transaction type name is required");
            }

            return _result;
        }
    }
}