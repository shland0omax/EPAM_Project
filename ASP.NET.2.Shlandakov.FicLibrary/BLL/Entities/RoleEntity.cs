using BLL.Interfaces;

namespace BLL.Entities
{
    public class RoleEntity: IBllEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}