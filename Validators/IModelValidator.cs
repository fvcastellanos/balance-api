
using System.Collections.Generic;
using BalanceApi.Model.Domain;

namespace BalanceApi.Validators {

    public interface IModelValidator<T> {

        Result<List<string>, T> validate(T obj);

    }
}