using System;
using System.Threading.Tasks;
using Odachi.AspNetCore.Authentication.Basic;

namespace BalanceApi.Security
{
    public class CustomAuthenticationEvent : BasicEvents
    {
        public override Task SignIn(BasicSignInContext context)
        {
            if (context.Username == "usr")
            {
                
            }

            return Task.FromResult(0);
        }
    }
}