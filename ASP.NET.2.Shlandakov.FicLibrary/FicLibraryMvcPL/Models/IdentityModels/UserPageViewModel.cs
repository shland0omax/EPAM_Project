using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FicLibraryMvcPL.Models.BllModels;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Models
{
    public class UserPageViewModel
    {
        public UserViewModel User { get; set; }
        public ProfileViewModel Profile { get; set; }
        public UserStatsViewModel Stats { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public IEnumerable<TextDescriptionViewModel> Titles { get; set; }
    }
}