using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public Role RoleId { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}