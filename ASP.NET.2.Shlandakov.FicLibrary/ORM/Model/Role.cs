using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    [Table("Role")]
    public partial class Role: IOrmEntity
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }
        
        public virtual ICollection<User> User { get; set; }
    }
}
