using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    [Table("Profile")]
    public partial class Profile: IOrmEntity
    {
        public Profile()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(14)]
        public string Mobile { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DayOfBirth { get; set; }

        public string About { get; set; }

        public bool? Sex { get; set; }

        public byte[] Avatar { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
