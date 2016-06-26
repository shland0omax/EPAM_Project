using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Models.BllModels
{
    public class TextDescriptionViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        [Required]
        [Display(Name = "Заголовок")]
        [MaxLength(200, ErrorMessage = "Длина заголовка превышает допустимую(200 символов)")]
        [MinLength(3, ErrorMessage = "Заголовок слишком короток!")]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public bool IsPublished { get; set; }
        public DateTime? PublicationDate { get; set; }
        public IEnumerable<TextViewModel> Texts { get; set; }
        public UserViewModel Author { get; set; }
        public ProfileViewModel AuthorProfile { get; set; }
        public double Rating { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}