namespace DAL.DTO.Models
{
    public class DalRole: IDalEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}