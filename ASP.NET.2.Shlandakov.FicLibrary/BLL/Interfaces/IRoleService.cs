using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IRoleService: IService<RoleEntity>
    {
        RoleEntity GetRoleEntityByName(string name);
    }
}