using BalanceApi.Security.Model.Domain;

namespace BalanceApi.Security.Model
{
    public interface IUserDao
    {
        User AuthenticateUser(string user, string password);
    }
}
