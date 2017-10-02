using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Controllers.Views.Request
{
    public class AddAccountType
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}