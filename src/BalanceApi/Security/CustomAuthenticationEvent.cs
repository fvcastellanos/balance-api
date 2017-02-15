using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BalanceApi.Model.Data;
using BalanceApi.Model.Domain;
using Odachi.AspNetCore.Authentication.Basic;

namespace BalanceApi.Security
{
    public class CustomAuthenticationEvent : BasicEvents
    {
        private IProviderDao _providerDao;

        public CustomAuthenticationEvent(IProviderDao providerDao)
        {
            _providerDao = providerDao;
        }

        public override Task SignIn(BasicSignInContext context)
        {
            List<Provider> providers = _providerDao.GetAll();

            if (context.Username == "usr")
            {
                
            }

            return Task.FromResult(0);
        }
    }
}