using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    [Table("Rating")]
    public partial class Rating: IOrmEntity
    {
        public int Id { get; set; }

        public int value { get; set; }

        public int UserId { get; set; }

        public int TextDescId { get; set; }

        public virtual TextDescription TextDescription { get; set; }

        public virtual User User { get; set; }
    }
}
