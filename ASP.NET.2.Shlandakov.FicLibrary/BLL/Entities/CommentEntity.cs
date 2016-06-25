using System;
using BLL.Interfaces;

namespace BLL.Entities
{
    public class CommentEntity: IBllEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CommentatorId { get; set; }
        public int CommentRelationId { get; set; }
        public int ObjectId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? LastEditDate { get; set; }
    }
}