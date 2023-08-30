using System.ComponentModel.DataAnnotations;

namespace products_test.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = "Логин")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
