using BalanceApi.Model.Domain;

namespace BalanceApi.Services
{
    public abstract class BaseService
    {
        protected Error BuildError(string message)
        {
            return new Error(message);
        }
        
    }
}