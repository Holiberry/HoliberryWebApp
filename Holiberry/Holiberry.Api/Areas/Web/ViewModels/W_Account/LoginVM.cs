using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Areas.Web.ViewModels.W_Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Podaj login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
