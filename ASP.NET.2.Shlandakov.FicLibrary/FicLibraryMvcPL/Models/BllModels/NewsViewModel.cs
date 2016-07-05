using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models.BllModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(200)]
        public string Content { get; set; }

        [Required]
        [MinLength(5), MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public DateTime PublicationDate { get; set; }

        public int CreatorId { get; set; }
    }
}