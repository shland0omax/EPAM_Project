using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models
{
    public class NewsPostModel
    {
        [Required]
        [MinLength(200)]
        public string Content { get; set; }

        [Required]
        [MinLength(5), MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public HttpPostedFileBase Image { get; set; }
    }
}