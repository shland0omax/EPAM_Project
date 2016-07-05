using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("News")]
    public partial class News: IOrmEntity
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PublicationDate { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }
    }
}