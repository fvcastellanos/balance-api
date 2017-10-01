
using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.Domain
{
    public class AccountType
    {
        [Required]
        public long Id { get; set;  }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public AccountType(long id, string name) {
            this.Id = id;
            this.Name = name;
        }

        public AccountType() {
        
        }
    }
}
