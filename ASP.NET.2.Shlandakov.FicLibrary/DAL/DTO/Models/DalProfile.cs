using System;

namespace DAL.DTO.Models
{
    public class DalProfile: IDalEntity
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