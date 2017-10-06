using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.Views.Request
{
    public class AddTransactionTypeRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public bool Credit { get; set; }
    }
}