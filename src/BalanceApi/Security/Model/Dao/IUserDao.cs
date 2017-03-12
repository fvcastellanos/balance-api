namespace BalanceApi.Security.Model.Dao
{
    public interface IUserDao
    {
        bool AuthenticateUser(string user, string password);
    }
}
