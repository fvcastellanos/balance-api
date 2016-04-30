using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalanceApi.Model.Domain.Response
{
    public abstract class ResponseBase<T>
    {
        public int code;
        public string message;
        public T data;
    }
}
