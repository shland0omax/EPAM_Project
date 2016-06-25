using System.ComponentModel.DataAnnotations;

namespace FicLibraryMvcPL.Models.AuthModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Длина логина - 5-20 символов")]
        [RegularExpression(@"^[\w.]{5,20}$", ErrorMessage = "Допустимы латинские буквы, цифры, _ и .")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Электронная почта")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Введенный e-mail некорректен")]
        public string Email { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Пароль должен иметь от 6 до 25 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}