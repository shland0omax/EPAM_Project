using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfaces;
using FicLibraryMvcPL.Mappers;
using FicLibraryMvcPL.Models;
using FicLibraryMvcPL.Models.BllModels;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Infrastructure
{
    public static class ModelHelper
    {
        public static UserStatsViewModel ReadUserStats(int userId, bool fullStats)
        {
            var userStats = new UserStatsViewModel();
            var user = DependencyResolver.Current.GetService<IUserService>().GetEntityById(userId);
            if (user != null)
            {
                userStats.DaysInService = (DateTime.Now - user.RegisterDate).Days;
                userStats.TotalComments = DependencyResolver.Current.GetService<ICommentService>().GetUserCommentsCountByUserId(userId);
                if (fullStats)
                {
                    userStats.TotalPublications =
                        DependencyResolver.Current.GetService<ITextDescriptionService>()
                            .GetUserPublicationsCount(userId);
                    if (userStats.TotalPublications != 0)
                        userStats.AverageRating =
                            DependencyResolver.Current.GetService<IRatingService>().GetAverageRatingForUser(userId);
                }
            }
            return userStats;
        }

        public static IEnumerable<CommentViewModel> ReadCommentsForModel(string relation, int modelId)
        {
            var relationId = DependencyResolver.Current.GetService<ICommentRelationService>().GetCommentRelationIdByName(relation);
            var comments = DependencyResolver.Current.GetService<ICommentService>()
                .GetCommentsAboutObject(relationId, modelId).Select(Mapper.ToView).ToList();
            var userService = DependencyResolver.Current.GetService<IUserService>();
            var profileService = DependencyResolver.Current.GetService<IProfileService>();
            for (int i = 0; i < comments.Count; i++)
            {
                comments[i].Sender = Mapper.ToView(userService.GetEntityById(comments[i].CommentatorId));
                comments[i].SenderProfile = Mapper.ToView(profileService.GetEntityById(comments[i].Sender.ProfileId));
                comments[i].ShortStats = ReadUserStats(comments[i].Sender.Id, false);
            }
            return comments;
        }

        public static IEnumerable<TextDescriptionViewModel> ReadTextDescriptionsAdditionInfo(
            IEnumerable<TextDescriptionViewModel> td)
        {
            if (td == null) throw new ArgumentNullException(nameof(td));
            var list = td.ToList();
            var userService = DependencyResolver.Current.GetService<IUserService>();
            var profileService = DependencyResolver.Current.GetService<IProfileService>();
            var ratingService = DependencyResolver.Current.GetService<IRatingService>();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Author = Mapper.ToView(userService.GetEntityById(list[i].AuthorId));
                list[i].AuthorProfile = Mapper.ToView(profileService.GetEntityById(list[i].Author.ProfileId));
                list[i].Rating = Math.Round(ratingService.GetAverageRatingForTitle(list[i].Id), 2);
            }
            return list;
        }

        public static TextDescriptionViewModel ReadTextDescriptionAdditionInfo(TextDescriptionViewModel td)
        {
            if (td == null) throw new ArgumentNullException(nameof(td));
            td.Author = Mapper.ToView(DependencyResolver.Current.GetService<IUserService>().GetEntityById(td.AuthorId));
            td.AuthorProfile = Mapper.ToView(DependencyResolver.Current.GetService<IProfileService>().GetEntityById(td.Author.ProfileId));
            td.Rating = Math.Round(DependencyResolver.Current.GetService<IRatingService>().GetAverageRatingForTitle(td.Id), 2);
            return td;
        }

        public static bool HaveAccessPrivilege(string userName, string editorName)
        {
            if (editorName == null) return false;
            if (string.Equals(userName, editorName, StringComparison.CurrentCultureIgnoreCase)) return true;
            var userService = DependencyResolver.Current.GetService<IUserService>();
            var user = userService.GetUserByLogin(userName);
            var editor = userService.GetUserByLogin(editorName);
            if (editor.RoleId > 3) return false;
            return user.RoleId > editor.RoleId;
        }
    }
}