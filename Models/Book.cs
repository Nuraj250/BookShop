using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)] // Limit title length
        public string Title { get; set; }

        [Required]
        [StringLength(100)] // Limit author name length
        public string Author { get; set; }

        [Required]
        [StringLength(20)] // ISBN typically has a max length of 20
        public string ISBN { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")] // Precision for price
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)] // e.g., Available, Out of Stock, Discontinued
        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string ImagePath { get; set; } 

    }
}
