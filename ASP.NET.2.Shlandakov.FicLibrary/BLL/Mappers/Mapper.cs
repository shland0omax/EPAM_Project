using System;
using BLL.Entities;
using BLL.Interfaces;
using DAL.DTO;
using DAL.DTO.Models;

namespace BLL.Mappers
{
    public static class Mapper
    {
        #region ToDal methods

        public static IDalEntity ToDal(IBllEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return ToDal((dynamic)entity);
        }

        public static DalUser ToDal(UserEntity user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return new DalUser()
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

        public static DalRole ToDal(RoleEntity role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            return new DalRole()
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name
            };
        }

        public static DalComment ToDal(CommentEntity comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            return new DalComment()
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

        public static DalCommentRelation ToDal(CommentRelationEntity cre)
        {
            if (cre == null) throw new ArgumentNullException(nameof(cre));
            return new DalCommentRelation()
            {
                Id = cre.Id,
                RelationName = cre.RelationName
            };
        }

        public static DalText ToDal(TextEntity text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            return new DalText()
            {
                Id = text.Id,
                Content = text.Content,
                TitleId = text.TitleId,
                OrderInTitle = text.OrderInTitle,
                Subtitle = text.Subtitle
            };
        }

        public static DalTextDescription ToDal(TextDescriptionEntity textDescription)
        {
            if (textDescription == null) throw new ArgumentNullException(nameof(textDescription));
            return new DalTextDescription()
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

        public static DalProfile ToDal(ProfileEntity profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            return new DalProfile()
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

        public static DalRating ToDal(RatingEntity rating)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));
            return new DalRating()
            {
                Id= rating.Id,
                UserId = rating.UserId,
                TextDescId = rating.TextDescId,
                Value = rating.Value
            };
        }

        public static DalSearch ToDal(SearchEntity search)
        {
            if (search == null) throw new ArgumentNullException(nameof(search));
            return new DalSearch()
            {
                Title = search.Title,
                SearchSubtitles = search.SearchSubtitles,
                PublisherLogin = search.PublisherLogin,
                PublicationDateStart = search.PublicationDateStart,
                PublicationDateFinish = search.PublicationDateFinish,
                MinRating = search.MinRating,
                MaxRating = search.MaxRating,
                IncludeUnrated = search.IncludeUnrated
            };
        }

        #endregion

        #region ToBll methods
        public static IBllEntity ToBll(IDalEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return ToBll((dynamic)entity);
        }

        public static UserEntity ToBll(DalUser dalUser)
        {
            if (dalUser == null) throw new ArgumentNullException(nameof(dalUser));
            return new UserEntity()
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

        public static RoleEntity ToBll(DalRole dalRole)
        {
            if (dalRole == null) throw new ArgumentNullException(nameof(dalRole));
            return new RoleEntity()
            {
                Id = dalRole.Id,
                Description = dalRole.Description,
                Name = dalRole.Name
            };
        }

        public static CommentEntity ToBll(DalComment dalComment)
        {
            if (dalComment == null) throw new ArgumentNullException(nameof(dalComment));
            return new CommentEntity()
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

        public static CommentRelationEntity ToBll(DalCommentRelation dcr)
        {
            if (dcr == null) throw new ArgumentNullException(nameof(dcr));
            return new CommentRelationEntity()
            {
                Id = dcr.Id,
                RelationName = dcr.RelationName
            };
        }

        public static TextEntity ToBll(DalText dalText)
        {
            if (dalText == null) throw new ArgumentNullException(nameof(dalText));
            return new TextEntity()
            {
                Id = dalText.Id,
                Content = dalText.Content,
                TitleId = dalText.TitleId,
                OrderInTitle = dalText.OrderInTitle,
                Subtitle = dalText.Subtitle
            };
        }

        public static TextDescriptionEntity ToBll(DalTextDescription dalTextDescription)
        {
            if (dalTextDescription == null) throw new ArgumentNullException(nameof(dalTextDescription));
            return new TextDescriptionEntity()
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

        public static ProfileEntity ToBll(DalProfile dalProfile)
        {
            if (dalProfile == null) throw new ArgumentNullException(nameof(dalProfile));
            return new ProfileEntity()
            {
                Id = dalProfile.Id,
                About = dalProfile.About,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                DayOfBirth = dalProfile.DayOfBirth,
                Mobile = dalProfile.Mobile,
                Avatar = dalProfile.Avatar,
                Sex = dalProfile.Sex
            };
        }

        public static RatingEntity ToBll(DalRating rating)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));
            return new RatingEntity()
            {
                Id = rating.Id,
                UserId = rating.UserId,
                TextDescId = rating.TextDescId,
                Value = rating.Value
            };
        }
        #endregion
    }
}