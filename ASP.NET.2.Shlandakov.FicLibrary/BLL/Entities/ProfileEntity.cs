using System;
using BLL.Interfaces;

namespace BLL.Entities
{
    public class ProfileEntity: IBllEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public string About { get; set; }
        public bool? Sex { get; set; }
        public byte[] Avatar { get; set; }
    }
}