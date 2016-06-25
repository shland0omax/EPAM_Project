using DAL.DTO.Models;

namespace DAL.Interface
{
    public interface IProfileRepository: IRepository<DalProfile>
    {
        DalProfile GetProfileByLogin(string login);
        int CreateWithGeneratedIdReturn(DalProfile profile);
    }
}