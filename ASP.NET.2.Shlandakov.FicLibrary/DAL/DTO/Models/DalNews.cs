using System;

namespace DAL.DTO.Models
{
    public class DalNews: IDalEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }

        public DateTime PublicationDate { get; set; }

        public int CreatorId { get; set; }
    }
}