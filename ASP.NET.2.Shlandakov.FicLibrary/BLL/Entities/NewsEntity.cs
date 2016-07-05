using System;
using BLL.Interfaces;

namespace BLL.Entities
{
    public class NewsEntity : IBllEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }

        public DateTime PublicationDate { get; set; }

        public int CreatorId { get; set; }
    }
}