using BalanceApi.Model.Domain;

namespace BalanceApi.Services
{
    public abstract class BaseService
    {
        protected Error BuildError(string message)
        {
            return new Error(message);
        }

        protected Result<Error, T> BuildSuccessResult<T>(T payload)
        {
            return Result<Error, T>.ForSuccess(payload);
        }

        protected Result<Error, T> BuildFailedResult<T>(string error)
        {
            return Result<Error, T>.ForFailure(BuildError(error));
        }
    }
}