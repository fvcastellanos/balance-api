
using System;

namespace BalanceApi.Domain
{
    public class Result 
    {
        private object obj;

        private Exception ex;

        private bool success;

        private Result(object obj) {
            this.obj = obj;
            success = true; 
        }

        private Result(Exception ex) {
            this.ex = ex;
            success = false;
        }

        public T getObject<T>() {
            return (T) obj;
        }

        public Exception getException() {
            return ex;
        }

        public bool isSuccess() {
            return success;
        }

        public static Result forSuccess(object obj) {
            return new Result(obj);
        }

        public static Result forException(Exception ex) {
            return new Result(ex);
        }

    }
}