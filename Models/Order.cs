using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public class Order
    {
        [Key]
        private Guid OrderId { get; set; }

        [Required]
        private Guid CustomerId { get; set; }

        [Required]
        private DateTime Date { get; set; }

        [Required]
        private decimal Price { get; set; }

        [Required]
        private string PaymentMethod { get; set; }

        private string PaymentStatus { get; set; } // Pending, Paid

        private string OrderStatus { get; set; } // Order Placed, Delivering, Delivered

        private virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
