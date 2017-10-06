using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.Views.Request
{
    public class AddAccountType
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}