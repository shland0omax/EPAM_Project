using BLL.Entities;
using BLL.Services;

namespace BLL.Interfaces
{
    public interface IProfileService: IService<ProfileEntity>
    {
        ProfileEntity GetProfileByUserLogin(string login);
        int CreateEntityWithIdReturn(ProfileEntity entity);
    }
}