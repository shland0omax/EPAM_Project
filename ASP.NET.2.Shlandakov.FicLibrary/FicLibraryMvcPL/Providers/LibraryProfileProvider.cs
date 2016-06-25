using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using BLL.Interfaces;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace FicLibraryMvcPL.Providers
{
    public class LibraryProfileProvider: ProfileProvider
    {
        public IUserService UserService
            => (IUserService)DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService
            => (IRoleService)DependencyResolver.Current.GetService(typeof(IRoleService));

        public IProfileService ProfileService
            => (IProfileService)DependencyResolver.Current.GetService(typeof(IProfileService));
        public override string ApplicationName { get; set; }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            var result = new SettingsPropertyValueCollection();
            if (collection.Count < 1)
                return result;

            var username = (string)context["UserName"];
            if (string.IsNullOrEmpty(username)) return result;

            var profile = ProfileService.GetProfileByUserLogin(username);
            if (profile == null) return result;
            foreach (var spv in from SettingsProperty prop in collection select new SettingsPropertyValue(prop)
            {
                PropertyValue = profile.GetType().GetProperty(prop.Name).GetValue(profile, null)
            })
            {
                result.Add(spv);
            }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            var username = (string)context["UserName"];

            if (string.IsNullOrEmpty(username) || collection.Count < 1)
                return;

            var profile = ProfileService.GetProfileByUserLogin(username);
            if (profile == null) return;
            foreach (SettingsPropertyValue val in collection)
            {
                profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
            }
        }

        #region Not implemented
        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch,
            int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption,
            string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}