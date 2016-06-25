using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL.Entities;
using FicLibraryMvcPL.Models.BllModels;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ваш комментарий")]
        [MinLength(5, ErrorMessage = "Длина комментария не должна быть меньше 5 символов!")]
        public string Text { get; set; }
        public int CommentatorId { get; set; }
        public int CommentRelationId { get; set; }
        public int ObjectId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public UserViewModel Sender { get; set; }
        public ProfileViewModel SenderProfile { get; set; }
        public UserStatsViewModel ShortStats { get; set; }

        //public LikeViewModel CommentLikes { get; set; }
    }
}