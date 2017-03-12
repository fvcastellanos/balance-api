using System;
using BalanceApi.Domain;
using BalanceApi.Model.Data.Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dapper;
using System.Linq;
using BalanceApi.Security.Model.Domain;

namespace BalanceApi.Security.Model.Dapper
{
    public class UserDao : BaseDao, IUserDao
    {
        private readonly ILogger _logger;
        
        public UserDao(IOptions<AppSettings> settings, ILogger<UserDao> logger)
            : base(settings, logger)
        {
            _logger = logger;
        }

        public User AuthenticateUser(string email, string password)
        {
            var user = GetConnection().Query("select * from user where email = @Email and password = @Password",
                new { Email = email, Password = password }).SingleOrDefault();

            return user;
        }
    }


}