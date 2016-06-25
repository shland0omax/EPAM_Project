using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models
{
    public enum Role
    {
        Owner = 1,
        Admin, Moderator, User, Banned
    }

    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}