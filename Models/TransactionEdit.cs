using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GeneralStoreAPI.Models
{
    public class TransactionEdit
    {
        [Required]
        [DisplayName("Product ID")]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [DisplayName("Date Of Transaction")]
        public DateTime DateOfTransaction { get; }
    }
}