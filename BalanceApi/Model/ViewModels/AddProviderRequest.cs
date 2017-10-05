using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.ViewModels
{
    public class AddProvider
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(2)]
        public string Country { get; set; }
    }
}