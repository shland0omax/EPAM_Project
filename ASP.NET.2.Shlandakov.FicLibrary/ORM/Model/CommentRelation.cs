using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{

    [Table("CommentRelation")]
    public partial class CommentRelation : IOrmEntity
    {
        public CommentRelation()
        {
            Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RelationName { get; set; }
        
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
