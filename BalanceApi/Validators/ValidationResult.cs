using System.Collections.Generic;

namespace BalanceApi.Validators
{
    public class ValidationResult
    {
        private readonly IList<string> _errors;

        public ValidationResult()
        {
            _errors = new List<string>();
        }

        public void Add(string message)
        {
            _errors.Add(message);
        }

        public bool HasFailed()
        {
            return !_errors.Count.Equals(0);
        }

        public IList<string> GetErrors()
        {
            return _errors;
        }
    }
}