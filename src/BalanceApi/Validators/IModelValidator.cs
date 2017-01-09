
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Validators {

    public interface IModelValidator<T> {

        ValidationResult Validate(T obj);

    }
}