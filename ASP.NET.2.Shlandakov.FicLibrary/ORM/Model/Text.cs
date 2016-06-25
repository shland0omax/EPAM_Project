using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    [Table("Text")]
    public partial class Text: IOrmEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [StringLength(200)]
        public string Subtitle { get; set; }

        public int OrderInTitle { get; set; }

        public int TitleId { get; set; }

        public virtual TextDescription TextDescription { get; set; }
    }
}
