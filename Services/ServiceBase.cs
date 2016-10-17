﻿
using BalanceApi.Model.Domain;

namespace BalanceApi.Services
{
    public abstract class ServiceBase<T>
    {
        protected static string OK_MESSAGE = "OK";
        protected static int OK_CODE = 200;
        protected static int ERROR_CODE = 500;

        protected Response<T> CreateSuccessResponse(T data)
        {
            return new Response<T>(OK_CODE, OK_MESSAGE, data);
        }
    }
}