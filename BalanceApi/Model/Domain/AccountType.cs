
namespace BalanceApi.Model.Domain
{
    public class AccountType
    {
        public long Id { get; set;  }
        public string Name { get; set; }

        public AccountType(long id, string name) {
            this.Id = id;
            this.Name = name;
        }

        public AccountType() {
        
        }
    }
}
