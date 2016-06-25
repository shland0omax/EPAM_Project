using BLL.Interfaces;

namespace BLL.Entities
{
    public class TextEntity: IBllEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Subtitle { get; set; }
        public int OrderInTitle { get; set; }
        public int TitleId { get; set; }
    }
}