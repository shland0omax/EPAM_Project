using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService: IService<UserEntity>
    {
        UserEntity GetUserByLogin(string login);
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserByProfileId(int profileId);
    }
}