using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class ProfileService: Service<ProfileEntity, DalProfile>, IProfileService
    {
        private readonly IUnitOfWork uow;

        public ProfileService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Profiles)
        {
            uow = unitOfWork;
        }

        public ProfileEntity GetProfileByUserLogin(string login)
        {
            var profile = uow.Profiles.GetById(uow.Users.GetByPredicate(u => u.Login == login).ProfileId);
            return profile == null ? null: Mapper.ToBll(profile);
        }

        public int CreateEntityWithIdReturn(ProfileEntity entity)
        {
            return uow.Profiles.CreateWithGeneratedIdReturn(Mapper.ToDal(entity));
        }
    }
}