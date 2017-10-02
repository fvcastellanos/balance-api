using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Controllers.ViewModels
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