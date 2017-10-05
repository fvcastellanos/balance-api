using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.ViewModels
{
    public class AddAccountType
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}