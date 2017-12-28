using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.Views.Request
{
    public class TransactionTypeRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public bool Credit { get; set; }
    }
}