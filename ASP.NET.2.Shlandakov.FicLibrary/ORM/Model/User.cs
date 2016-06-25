using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    [Table("User")]
    public partial class User: IOrmEntity
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Rating = new HashSet<Rating>();
            TextDescription = new HashSet<TextDescription>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(70)]
        public string Email { get; set; }

        public int ProfileId { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegisterDate { get; set; }

        public int RoleId { get; set; }
        
        public virtual ICollection<Comment> Comment { get; set; }
        
        public virtual Profile Profile { get; set; }
        
        public virtual ICollection<Rating> Rating { get; set; }

        public virtual Role Role { get; set; }
        
        public virtual ICollection<TextDescription> TextDescription { get; set; }
    }
}
