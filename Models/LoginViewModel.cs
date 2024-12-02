using System.ComponentModel.DataAnnotations;

namespace AdminPanelApp.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        private string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        private string Password { get; set; }

        private bool RememberMe { get; set; }
    }
}
