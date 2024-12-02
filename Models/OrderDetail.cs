using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public class OrderDetail
    {
        [Key]
        private Guid Id { get; set; }

        [Required]
        private Guid OrderId { get; set; }

        [Required]
        private Guid ProductId { get; set; }

        [Required]
        private int Quantity { get; set; }

        [Required]
        private decimal Price { get; set; }

        private virtual Order Order { get; set; }
    }
}
