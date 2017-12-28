using BalanceApi.Model.Domain;

namespace BalanceApi.Services
{
    public abstract class BaseService
    {
        protected static Error BuildError(string message)
        {
            return new Error(message);
        }
    }
}