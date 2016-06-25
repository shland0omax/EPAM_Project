using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    [Table("TextDescription")]
    public partial class TextDescription: IOrmEntity
    {
        public TextDescription()
        {
            Rating = new HashSet<Rating>();
            Text = new HashSet<Text>();
        }

        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastEditDate { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public bool IsPublished { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PublicationDate { get; set; }
        
        public virtual ICollection<Rating> Rating { get; set; }
        
        public virtual ICollection<Text> Text { get; set; }

        public virtual User User { get; set; }
    }
}
