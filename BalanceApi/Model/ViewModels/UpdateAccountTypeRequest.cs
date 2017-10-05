using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.ViewModels
{
    public class UpdateAccountType
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}