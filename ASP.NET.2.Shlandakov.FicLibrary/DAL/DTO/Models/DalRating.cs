namespace DAL.DTO.Models
{
    public class DalRating : IDalEntity
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int UserId { get; set; }

        public int TextDescId { get; set; }
    }
}