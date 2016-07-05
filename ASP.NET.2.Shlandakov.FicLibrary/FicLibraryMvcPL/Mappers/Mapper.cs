using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using FicLibraryMvcPL.Models;
using FicLibraryMvcPL.Models.BllModels;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Mappers
{
    public static class Mapper
    {
        #region ToView methods
        public static UserViewModel ToView(UserEntity user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return new UserViewModel()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                ProfileId = user.ProfileId,
                RoleId = (Role)user.RoleId,
                RegisterDate = user.RegisterDate
            };
        }

        public static RoleViewModel ToView(RoleEntity role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            return new RoleViewModel
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name
            };
        }

        public static CommentViewModel ToView(CommentEntity comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            return new CommentViewModel()
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

        public static TextViewModel ToView(TextEntity text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            return new TextViewModel()
            {
                Id = text.Id,
                Content = text.Content,
                TitleId = text.TitleId,
                OrderInTitle = text.OrderInTitle,
                Subtitle = text.Subtitle
            };
        }

        public static TextDescriptionViewModel ToView(TextDescriptionEntity textDescription)
        {
            if (textDescription == null) throw new ArgumentNullException(nameof(textDescription));
            return new TextDescriptionViewModel()
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

        public static ProfileViewModel ToView(ProfileEntity profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            return new ProfileViewModel()
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

        public static NewsViewModel ToView(NewsEntity news)
        {
            if (news == null) throw new ArgumentNullException(nameof(news));
            return new NewsViewModel()
            {
                Id = news.Id,
                Content = news.Content,
                Image = news.Image,
                CreatorId = news.CreatorId,
                PublicationDate = news.PublicationDate,
                Title = news.Title
            };
        }

        public static PagedDataViewModel<T> ToPagedView<T>(this
            IEnumerable<T> elements, int elemsPerPage, int pageNumber) where T: class 
        {
            if (elements == null) throw new ArgumentNullException(nameof(elements));
            return new PagedDataViewModel<T>
            {
                PageInfo = new PageInfo()
                {
                    PageNumber = pageNumber,
                    PageSize = elemsPerPage,
                    TotalItems = elements.Count()
                },
                Elements = elements.Skip(elemsPerPage*(pageNumber - 1)).Take(elemsPerPage)
            };
        }

        #endregion

        #region ToBll methods

        public static UserEntity ToBll(UserViewModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return new UserEntity()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                ProfileId = user.ProfileId,
                RoleId = (int)user.RoleId,
                RegisterDate = user.RegisterDate
            };
        }

        public static RoleEntity ToBll(RoleViewModel role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            return new RoleEntity()
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name
            };
        }

        public static CommentEntity ToBll(CommentViewModel comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            return new CommentEntity()
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

        public static TextEntity ToBll(TextViewModel text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            return new TextEntity()
            {
                Id = text.Id,
                Content = text.Content,
                TitleId = text.TitleId,
                OrderInTitle = text.OrderInTitle,
                Subtitle = text.Subtitle
            };
        }

        public static TextDescriptionEntity ToBll(TextDescriptionViewModel textDescription)
        {
            if (textDescription == null) throw new ArgumentNullException(nameof(textDescription));
            return new TextDescriptionEntity()
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

        public static ProfileEntity ToBll(ProfileViewModel profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            return new ProfileEntity()
            {
                Id = profile.Id,
                About = profile.About,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DayOfBirth = profile.DayOfBirth,
                Mobile = profile.Mobile,
                Sex = profile.Sex,
                Avatar = profile.Avatar
            };
        }

        public static SearchEntity ToBll(SearchViewModel search)
        {
            if (search == null) throw new ArgumentNullException(nameof(search));
            return new SearchEntity
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

        public static NewsEntity ToBll(NewsViewModel news)
        {
            if (news == null) throw new ArgumentNullException(nameof(news));
            return new NewsEntity()
            {
                Id = news.Id,
                Content = news.Content,
                Image = news.Image,
                CreatorId = news.CreatorId,
                PublicationDate = news.PublicationDate,
                Title = news.Title
            };
        }

        #endregion
    }
}