using System.Collections.Generic;
using FicLibraryMvcPL.Models.BllModels;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Models
{
    public class NewsPageViewModel
    {
        public NewsViewModel News { get; set; }
        public UserViewModel Author { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}