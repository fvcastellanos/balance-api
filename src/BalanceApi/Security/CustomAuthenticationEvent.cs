using System;
using System.Threading.Tasks;
using BalanceApi.Security.Service;
using Odachi.AspNetCore.Authentication.Basic;

namespace BalanceApi.Security
{
    public class CustomAuthenticationEvent : BasicEvents
    {
        private SecurityService _securityService;

        public CustomAuthenticationEvent(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override Task SignIn(BasicSignInContext context)
        {

            var userAuthenticated = _securityService.AuthenticateUser(context.Username, context.Password);

            if (userAuthenticated)
            {
                return Task.FromResult(0);
            }

            return Task.FromException(new Exception("Not Authorized"));
        }
    }
}