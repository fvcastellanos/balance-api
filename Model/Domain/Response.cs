
namespace BalanceApi.Model.Domain
{
    public class Response<T>
    {
        public int code;
        public string message;
        public T data;

        public Response(int code, string message, T data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }

    }
}
