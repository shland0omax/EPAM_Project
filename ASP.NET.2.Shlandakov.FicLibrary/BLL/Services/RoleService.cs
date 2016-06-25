using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class RoleService: Service<RoleEntity, DalRole>, IRoleService
    {
        private readonly IUnitOfWork uow;

        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Roles)
        {
            uow = unitOfWork;
        }

        public RoleEntity GetRoleEntityByName(string name)
        {
            var role = uow.Roles.GetByPredicate(r => r.Name == name);
            return role == null? null : Mapper.ToBll(role);
        }
    }
}