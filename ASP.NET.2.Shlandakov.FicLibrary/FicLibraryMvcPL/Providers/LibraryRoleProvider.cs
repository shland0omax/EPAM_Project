using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces;
using FicLibraryMvcPL.Mappers;
using FicLibraryMvcPL.Models;

namespace FicLibraryMvcPL.Providers
{
    public class LibraryRoleProvider: RoleProvider
    {
        public override string ApplicationName { get; set; }

        public IUserService UserService
            => (IUserService)DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService
            => (IRoleService)DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string login, string roleName)
        {
            var user = UserService.GetUserByLogin(login);
            if (user == null) return false;
            var role = RoleService.GetRoleEntityByName(roleName);
            return user.RoleId == role.Id;
        }

        public override string[] GetRolesForUser(string username)
        {
            var res = new List<string>();
            var user = UserService.GetUserByLogin(username);
            var role = RoleService.GetEntityById(user.RoleId);
            res.Add(role.Name);
            return res.ToArray();
        }

        public override void CreateRole(string roleName)
        {

            if (RoleService.GetRoleEntityByName(roleName) == null)
            {
                var role = new RoleViewModel
                {
                    Name = roleName
                };
                RoleService.CreateEntity(Mapper.ToBll(role));
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            var role = RoleService.GetRoleEntityByName(roleName);
            if (role == null) return false;
            try
            {
                RoleService.DeleteEntity(role);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override bool RoleExists(string roleName)
        {
            return RoleService.GetRoleEntityByName(roleName) == null;
        }

        #region not implemented
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}