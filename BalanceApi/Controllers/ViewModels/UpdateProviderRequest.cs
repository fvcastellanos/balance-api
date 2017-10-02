using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Controllers.ViewModels
{
    public class UpdateProvider
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(2)]
        public string Country { get; set; }
    }
}