using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Controllers.Views
{
    public class AddAccountType
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}