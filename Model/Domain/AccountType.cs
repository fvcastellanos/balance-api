
namespace BalanceApi.Model.Domain
{
    public class AccountType
    {
        public long id { get; set;  }
        public string name { get; set; }

        public AccountType(long id, string name) {
            this.id = id;
            this.name = name;
        }

        public AccountType() {
        
        }
    }
}
