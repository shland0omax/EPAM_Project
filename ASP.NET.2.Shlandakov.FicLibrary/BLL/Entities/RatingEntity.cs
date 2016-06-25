using BLL.Interfaces;

namespace BLL.Entities
{
    public class RatingEntity: IBllEntity
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int UserId { get; set; }

        public int TextDescId { get; set; }
    }
}