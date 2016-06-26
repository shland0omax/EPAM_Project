using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models
{
    public class TextViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [Required]
        [Display(Name = "Подзаголовок")]
        [MaxLength(200, ErrorMessage = "Длина заголовка превышает допустимую(200 символов)"), MinLength(3, ErrorMessage = "Заголовок слишком короток!")]
        public string Subtitle { get; set; }
        public int OrderInTitle { get; set; }
        public int TitleId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}