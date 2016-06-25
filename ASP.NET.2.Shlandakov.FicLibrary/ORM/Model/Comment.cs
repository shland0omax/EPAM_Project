using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    [Table("Comment")]
    public partial class Comment : IOrmEntity
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int CommentatorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PostDate { get; set; }

        public int ObjectId { get; set; }

        public int CommentRelationId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastEditDate { get; set; }

        public virtual CommentRelation CommentRelation { get; set; }

        public virtual User User { get; set; }
    }
}
