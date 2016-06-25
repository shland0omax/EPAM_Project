using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models.AuthModels
{
    public class LogOnViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Длина логина - 5-20 символов")]
        [RegularExpression(@"^[\w.]{5,20}$", ErrorMessage = "Допустимы латинские буквы, цифры, _ и .")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(25, ErrorMessage = "Пароль должен иметь от 6 до 25 символов", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Запомнить")]
        public bool RememberMe { get; set; }
    }
}