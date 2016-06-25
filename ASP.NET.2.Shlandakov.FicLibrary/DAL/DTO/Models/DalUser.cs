using System;

namespace DAL.DTO.Models
{
    public class DalUser : IDalEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProfileId { get; set; }
        public int RoleId { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}