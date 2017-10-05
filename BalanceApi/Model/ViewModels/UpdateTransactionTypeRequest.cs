using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.ViewModels
{
    public class UpdateTransactionTypeRequest
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public bool Credit { get; set; }
        
    }
}