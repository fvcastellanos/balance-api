using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Model.Domain;

namespace BalanceApi.Model.Domain.Response
{
    public class AccountTypeResponse : ResponseBase<AccountType>
    {
        public AccountTypeResponse(int code, string message, AccountType data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}
