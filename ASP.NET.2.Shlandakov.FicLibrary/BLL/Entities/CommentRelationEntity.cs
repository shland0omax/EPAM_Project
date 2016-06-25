using BLL.Interfaces;

namespace BLL.Entities
{
    public class CommentRelationEntity: IBllEntity
    {
        public int Id { get; set; }
        public string RelationName { get; set; }
    }
}