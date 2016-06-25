using System;
using DAL.DTO;
using DAL.DTO.Models;
using ORM;

namespace DAL.Mappers
{
    public static class Mapper
    {
        #region ToDal methods

        public static IDalEntity ToDal(IOrmEntity entity)
        {
            return ToDal((dynamic)entity);
        }

        public static DalUser ToDal(User user)
        {
            return new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                ProfileId = user.ProfileId,
                RoleId = user.RoleId,
                RegisterDate = user.RegisterDate
            };
        }

        public static DalRole ToDal(Role role)
        {          
            return new DalRole
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name
            };
        }


        public static DalComment ToDal(Comment comment)
        {
            return new DalComment
            {
                Id = comment.Id,
                CommentatorId = comment.CommentatorId,
                LastEditDate = comment.LastEditDate,
                PostDate = comment.PostDate,
                Text = comment.Text,
                CommentRelationId = comment.CommentRelationId,
                ObjectId = comment.ObjectId
            };
        }

        public static DalCommentRelation ToDal(CommentRelation relation)
        {
            return new DalCommentRelation()
            {
                Id = relation.Id,
                RelationName = relation.RelationName
            };
        }

        public static DalText ToDal(Text text)
        {
            return new DalText
            {
                Id = text.Id,
                Content = text.Content,
                Subtitle = text.Subtitle,
                OrderInTitle = text.OrderInTitle,
                TitleId = text.TitleId
            };
        }

        public static DalTextDescription ToDal(TextDescription textDescription)
        {
            return new DalTextDescription
            {
                Id = textDescription.Id,
                AuthorId = textDescription.AuthorId,
                CreationDate = textDescription.CreationDate,
                LastEditDate = textDescription.LastEditDate,
                PublicationDate = textDescription.PublicationDate,
                IsPublished = textDescription.IsPublished,
                Title = textDescription.Title
            };
        }

        public static DalProfile ToDal(Profile profile)
        {
            return new DalProfile
            {
                Id = profile.Id,
                About = profile.About,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DayOfBirth = profile.DayOfBirth,
                Mobile = profile.Mobile,
                Avatar = profile.Avatar,
                Sex = profile.Sex
            };
        }

        public static DalRating ToDal(Rating rating)
        {
            return new DalRating()
            {
                Id = rating.Id,
                TextDescId = rating.TextDescId,
                UserId = rating.UserId,
                Value = rating.value
            };
        }

        #endregion

        #region ToOrm methods
        public static IOrmEntity ToOrm(IDalEntity entity)
        {
            return ToOrm((dynamic)entity);
        }

        public static User ToOrm(DalUser dalUser)
        {
            return new User()
            {
                Id = dalUser.Id,
                Login = dalUser.Login,
                Email = dalUser.Email,
                Password = dalUser.Password,
                ProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                RegisterDate = dalUser.RegisterDate
            };
        }

        public static Role ToOrm(DalRole dalRole)
        {
            return new Role
            {
                Id = dalRole.Id,
                Description = dalRole.Description,
                Name = dalRole.Name
            };
        }


        public static Comment ToOrm(DalComment dalComment)
        {
            return new Comment()
            {
                Id = dalComment.Id,
                CommentatorId = dalComment.CommentatorId,
                LastEditDate = dalComment.LastEditDate,
                PostDate = dalComment.PostDate,
                Text = dalComment.Text,
                CommentRelationId = dalComment.CommentRelationId,
                ObjectId = dalComment.ObjectId
            };
        }

        public static CommentRelation ToOrm(DalCommentRelation relation)
        {
            return new CommentRelation()
            {
                Id = relation.Id,
                RelationName = relation.RelationName
            };
        }

        public static Text ToOrm(DalText dalText)
        {
            return new Text()
            {
                Id = dalText.Id,
                Content = dalText.Content,
                Subtitle = dalText.Subtitle,
                TitleId = dalText.TitleId
            };
        }

        public static TextDescription ToOrm(DalTextDescription dalTextDescription)
        {
            return new TextDescription()
            {
                Id = dalTextDescription.Id,
                AuthorId = dalTextDescription.AuthorId,
                CreationDate = dalTextDescription.CreationDate,
                LastEditDate = dalTextDescription.LastEditDate,
                PublicationDate = dalTextDescription.PublicationDate,
                IsPublished = dalTextDescription.IsPublished,
                Title = dalTextDescription.Title
            };
        }

        public static Profile ToOrm(DalProfile dalProfile)
        {
            return new Profile()
            {
                Id = dalProfile.Id,
                About = dalProfile.About,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                DayOfBirth = dalProfile.DayOfBirth,
                Mobile = dalProfile.Mobile,
                Sex = dalProfile.Sex,
                Avatar = dalProfile.Avatar
            };
        }


        public static Rating ToOrm(DalRating rating)
        {
            return new Rating()
            {
                Id = rating.Id,
                TextDescId = rating.TextDescId,
                UserId = rating.UserId,
                value = rating.Value
            };
        }

        #endregion
    }
}