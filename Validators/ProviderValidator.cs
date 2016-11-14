using System;
using System.Collections.Generic;
using BalanceApi.Model.Domain;


namespace BalanceApi.Validators {

    public class ProviderValidator : IModelValidator<Provider>
    {
        private List<string> errors;

        public ProviderValidator() {
            errors = new List<string>();
        }

        public Result<List<string>, Provider> validate(Provider obj)
        {
            if(obj == null) {
                errors.Add("Provider is null");
                return Result<List<string>, Provider>.ForFailure(errors);
            } else {
                if(string.IsNullOrEmpty(obj.name)) {
                    errors.Add("Must provide a name");
                }

                if(string.IsNullOrEmpty(obj.country)) {
                    errors.Add("A country must be defined");
                }

                // validate country code according with ISO (2 letter code)
            }

            return Result<List<string>, Provider>.ForSuccess(obj);
        }

    }
}