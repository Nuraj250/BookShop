using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private  Guid Id { get; set; }

        [Required]
        [StringLength(200)] // Limit title length
        private  string Title { get; set; }

        [Required]
        [StringLength(100)] // Limit author name length
        private  string Author { get; set; }

        [Required]
        [StringLength(20)] // ISBN typically has a max length of 20
        private  string ISBN { get; set; }

        [StringLength(100)]
        private  string Publisher { get; set; }

        [Required]
        private  int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")] // Precision for price
        private  decimal Price { get; set; }

        [Required]
        [StringLength(50)] // e.g., Available, Out of Stock, Discontinued
        private  string Status { get; set; }

        [DataType(DataType.Date)]
        private  DateTime PublishedDate { get; set; }

        [DataType(DataType.DateTime)]
        private  DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        private  DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        private  string ImagePath { get; set; } 

    }
}
