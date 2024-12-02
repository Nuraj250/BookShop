using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public  class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public  string Name { get; set; }

        [Required]
        [StringLength(100)]
        public  string Email { get; set; }

        [Required]
        public  string Password { get; set; }

        [StringLength(15)]
        public  string PhoneNumber { get; set; }

        public  string Role { get; set; }

        // Path to the user image
        public  string ImagePath { get; set; }

        public  DateTime CreatedAt { get; set; }
        public  DateTime UpdatedAt { get; set; }
    }

}
