using System;

namespace DAL.DTO.Models
{
    public class DalTextDescription: IDalEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}