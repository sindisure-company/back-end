using System.ComponentModel.DataAnnotations;

namespace ApiSindisure.Domain.ViewModel.Login
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
