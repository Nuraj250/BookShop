using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
    }
}
