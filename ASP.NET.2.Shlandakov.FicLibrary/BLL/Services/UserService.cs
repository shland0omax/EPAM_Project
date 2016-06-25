using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class UserService: Service<UserEntity, DalUser>, IUserService
    {
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Users)
        {
            uow = unitOfWork;
        }

        public UserEntity GetUserByLogin(string login)
        {
            var user = uow.Users.GetByPredicate(u => u.Login.ToLower() == login.ToLower());
            return user == null ? null : Mapper.ToBll(user);
        }

        public UserEntity GetUserByEmail(string email)
        {
            var user = uow.Users.GetByPredicate(u => u.Email.ToLower() == email.ToLower());
            return user == null ? null : Mapper.ToBll(user);
        }

        public UserEntity GetUserByProfileId(int profileId)
        {
            var user = uow.Users.GetByPredicate(u => u.ProfileId == profileId);
            return user == null ? null : Mapper.ToBll(user);
        }
    }
}