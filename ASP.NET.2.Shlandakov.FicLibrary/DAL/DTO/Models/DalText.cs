namespace DAL.DTO.Models
{
    public class DalText: IDalEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Subtitle { get; set; }
        public int OrderInTitle { get; set; }
        public int TitleId { get; set; }
    }
}