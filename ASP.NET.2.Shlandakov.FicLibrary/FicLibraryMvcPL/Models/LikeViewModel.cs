using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models
{
    public class LikeViewModel
    {
        public int ObjectId { get; set; }
        public int RelationId { get; set; }
        public bool IsLiked { get; set; }
        public int NumberOfLikes { get; set; }
    }
}