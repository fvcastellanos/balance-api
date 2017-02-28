using BalanceApi.Security.Model;

namespace BalanceApi.Security.Service
{
    public class SecurityService
    {
        private IUserDao _userDao;

        public SecurityService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public bool AuthenticateUser(string email, string password)
        {
            var user = _userDao.AuthenticateUser(email, password);

            if(user == null) {
                return false;
            }

            return true;
        }

    }
}
