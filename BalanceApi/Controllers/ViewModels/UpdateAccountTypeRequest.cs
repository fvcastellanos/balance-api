using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Controllers.ViewModels
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