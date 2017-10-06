using System.ComponentModel.DataAnnotations;

namespace BalanceApi.Model.ViewModels
{
    public class AddAccountRequest
    {
        [Required]
        public long AccountTypeId { get; set; }

        [Required]
        public long ProviderId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountNumber { get; set; }
    }
}