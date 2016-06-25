using System;
using System.ComponentModel.DataAnnotations;

namespace FicLibraryMvcPL.Models.IdentityModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        [Display(Name="Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Телефон")]
        public string Mobile { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? DayOfBirth { get; set; }
        [Display(Name = "Пару слов о себе")]
        public string About { get; set; }
        [Display(Name = "Пол")]
        public bool? Sex { get; set; }
        public byte[] Avatar { get; set; }
    }
}