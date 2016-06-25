using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Entities;
using BLL.Interfaces;
using FicLibraryMvcPL.Mappers;
using FicLibraryMvcPL.Models;
using FicLibraryMvcPL.Models.AuthModels;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Providers
{
    public class LibraryMembershipProvider: MembershipProvider
    {
        #region Properties
        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }

        public IUserService UserService
            => (IUserService)DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService
            => (IRoleService)DependencyResolver.Current.GetService(typeof(IRoleService));

        public IProfileService ProfileService
            => (IProfileService)DependencyResolver.Current.GetService(typeof(IProfileService));
        #endregion

        public MembershipUser CreateUser(RegisterViewModel registerModel)
        {
            if (GetUser(registerModel.UserName, false) != null || GetUser(registerModel.Email) != null)
                return null;
            try
            {
                var user = new UserEntity()
                {
                    Email = registerModel.Email,
                    Password = Crypto.HashPassword(registerModel.Password),
                    Login = registerModel.UserName,
                    RegisterDate = DateTime.Now.Date
                };


                var role = RoleService.GetRoleEntityByName("User");
                if (role != null)
                {
                    user.RoleId = role.Id;
                }

                var profile = new ProfileEntity()
                {
                    About = null,
                    DayOfBirth = null,
                    FirstName = null,
                    LastName = null,
                    Mobile = null
                };

                var bllProfileId = ProfileService.CreateEntityWithIdReturn(profile);
                user.ProfileId = bllProfileId;
                UserService.CreateEntity(user);
                return GetUser(registerModel.Email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = UserService.GetUserByLogin(username);
            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                return true;
            return false;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            var user = UserService.GetUserByLogin(username);
            if (user == null) return false;
            if (deleteAllRelatedData)
            {
                //smth about deleting related data
            }
            try
            {
                UserService.DeleteEntity(user);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = UserService.GetUserByLogin(username);

            if (user == null) return null;

            var memberUser = new MembershipUser("LibraryMembershipProvider", user.Login,
                null, user.Email, null, null,
                false, false, DateTime.MinValue, 
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public MembershipUser GetUser(string email)
        {
            var user = UserService.GetUserByEmail(email);
            if (user == null) return null;
            var memberUser = new MembershipUser("LibraryMembershipProvider", user.Login,
                null, user.Email, null, null,
                false, false, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);
            return memberUser;
        }

        #region Not implemented
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }



        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}