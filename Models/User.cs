using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public  class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private  Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        private  string Name { get; set; }

        [Required]
        [StringLength(100)]
        private  string Email { get; set; }

        [Required]
        private  string Password { get; set; }

        [StringLength(15)]
        private  string PhoneNumber { get; set; }

        private  string Role { get; set; }

        // Path to the user image
        private  string ImagePath { get; set; }

        private  DateTime CreatedAt { get; set; }
        private  DateTime UpdatedAt { get; set; }
    }

}
