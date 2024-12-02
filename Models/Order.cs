using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; } // Pending, Paid

        public string OrderStatus { get; set; } // Order Placed, Delivering, Delivered

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
