using BalanceApi.Model.Domain;

namespace BalanceApi.Validators {

    public class ProviderValidator : IModelValidator<Provider>
    {
        private readonly ValidationResult _result;

        public ProviderValidator()
        {
            _result = new ValidationResult();
        }

        public ValidationResult Validate(Provider obj)
        {
            if (obj == null)
            {
                _result.Add("Provider is null");
                return _result;
            }

            if (string.IsNullOrEmpty(obj.Name))
            {
                _result.Add("Must provide a name");
            }

            if (string.IsNullOrEmpty(obj.Country))
            {
                _result.Add("A country must be defined");
            }

            if (obj.Country.Length != 2)
            {
                _result.Add("Country code is not following the ISO standard");
            }

            return _result;
        }

    }
}