using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.DTO.Models;
using DAL.Mappers;
using DAL.Interface;
using ORM;

namespace DAL.Concrete
{
    public class ProfileRepository: Repository<DalProfile, Profile>, IProfileRepository
    {
        private readonly FicLibraryDB context;

        public ProfileRepository(FicLibraryDB uow): base(uow)
        {
            context = uow;
        }

        public DalProfile GetProfileByLogin(string login)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.Login == login);
            if (user == null) return null;
            var profile = context.Set<Profile>().FirstOrDefault(p => p.Id == user.ProfileId);
            return Mapper.ToDal(profile);
        }

        public int CreateWithGeneratedIdReturn(DalProfile profile)
        {
            var e = context.Profile.Add(Mapper.ToOrm(profile));
            context.SaveChanges();
            return e.Id;
        }
    }
}