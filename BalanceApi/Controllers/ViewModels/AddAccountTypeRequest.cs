using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Controllers.ViewModels
{
    public class AddAccountType
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}