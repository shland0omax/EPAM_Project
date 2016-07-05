using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Helpers;

namespace ORM
{
    public class DbInitializer : CreateDatabaseIfNotExists<FicLibraryDB>
    {
        protected override void Seed(FicLibraryDB context)
        {
            Role r1 = new Role { Id = 1, Name = "Owner", Description = "Site owner. Can do anything."};
            Role r2 = new Role { Id = 2, Name = "Admin", Description = "Admin" };
            Role r3 = new Role { Id = 3, Name = "Moderator", Description = "Moderator. Usually keeps an eye on comments and publications" };
            Role r4 = new Role { Id = 4, Name = "User", Description = "User. Just user" };
            Role r5 = new Role { Id = 5, Name = "Banned", Description = ":(" };

            context.Role.Add(r1);
            context.Role.Add(r2);
            context.Role.Add(r3);
            context.Role.Add(r4);
            context.Role.Add(r5);

            Profile ownerProfile = new Profile {Id = 1};
            Profile adminProfile = new Profile { Id = 2 };
            Profile moderatorProfile = new Profile { Id = 3 };

            context.Profile.Add(ownerProfile);
            context.Profile.Add(adminProfile);
            context.Profile.Add(moderatorProfile);

            User owner = new User { Id = 1, Login = "max0__o", Password = Crypto.HashPassword("Toshiro96"), RoleId = r1.Id, RegisterDate = DateTime.Now, Email = "shland@list.ru", ProfileId = ownerProfile.Id};
            User admin = new User { Id = 2, Login = "smilencer", Password = Crypto.HashPassword("adslm101a"), RoleId = r2.Id, RegisterDate = DateTime.Now, Email = "smilencer@gmail.com", ProfileId = adminProfile.Id };
            User moderator = new User { Id = 3, Login = "zioba", Password = Crypto.HashPassword("123456"), RoleId = r3.Id, RegisterDate = DateTime.Now, Email = "zioba@gmail.com", ProfileId = moderatorProfile.Id };

            context.User.Add(owner);
            context.User.Add(admin);
            context.User.Add(moderator);

            CommentRelation td = new CommentRelation { RelationName = "Text"};
            CommentRelation text = new CommentRelation { RelationName = "TextDescription" };
            CommentRelation user = new CommentRelation {RelationName = "User"};
            CommentRelation news = new CommentRelation { RelationName = "News"};

            context.CommentRelation.Add(td);
            context.CommentRelation.Add(text);
            context.CommentRelation.Add(user);
            context.CommentRelation.Add(news);

            base.Seed(context);
        }
    }
}