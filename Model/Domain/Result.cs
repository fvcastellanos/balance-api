

namespace BalanceApi.Model.Domain
{
    public class Result<L, R>
    {
        private R payload;

        private L failure;

        private bool success;

        // private Result(R payload, bool success) {
        //     this.payload = payload;
        //     this.success = success;
        // }

        private Result(R payload) {
            this.payload = payload;
            this.success = true;
        }

        private Result(L failure) {
            this.failure = failure;
            this.success = false;
        }

        // public R GetPayload() {
        //     return payload;
        // }

        // public R GetFailure() {
        //     return GetPayload();
        // }

        public R GetPayload() {
            return payload;
        }

        public L GetFailure() {
            return failure;
        }

        public bool isSuccess() {
            return success;
        }

        // public static Result<T> forSuccess<T>(T obj) {
        //     return new Result<T>(obj, true);
        // }

        // public static Result<L> forFailure<L>(L obj) {
        //     return new Result<L>(obj, false);
        // }

        public static Result<L, R> ForSuccess(R obj) {
            return new Result<L, R>(obj);
        }

        public static Result<L, R> ForFailure(L ex) {
            return new Result<L, R>(ex);
        }

    }
}