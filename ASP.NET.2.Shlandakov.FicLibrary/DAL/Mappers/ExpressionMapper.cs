using System;
using System.Data.Entity;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.DTO.Models;
using ORM;

namespace DAL.Mappers
{
    public static class ExpressionMapper
    {
        public static Expression<Func<Role, DalRole>> ToDal(DbSet<Role> e)
        {
            return x => new DalRole()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            };
        }

        public static Expression<Func<User, DalUser>> ToDal(DbSet<User> temp)
        {
            return user => new DalUser
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

        public static Expression<Func<Comment, DalComment>> ToDal(DbSet<Comment> temp)
        {
            return comment => new DalComment
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

        public static Expression<Func<CommentRelation, DalCommentRelation>> ToDal(DbSet<CommentRelation> temp)
        {
            return relation => new DalCommentRelation()
            {
                Id = relation.Id,
                RelationName = relation.RelationName
            };
        }

        public static Expression<Func<Text, DalText>> ToDal(DbSet<Text> temp)
        {
            return text => new DalText
            {
                Id = text.Id,
                Content = text.Content,
                Subtitle = text.Subtitle,
                OrderInTitle = text.OrderInTitle,
                TitleId = text.TitleId
            };
        }

        public static Expression<Func<TextDescription, DalTextDescription>> ToDal(DbSet<TextDescription> temp)
        {
            return textDescription => new DalTextDescription
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

        public static Expression<Func<Profile, DalProfile>> ToDal(DbSet<Profile> temp)
        {
            return profile => new DalProfile
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

        public static Expression<Func<Rating, DalRating>> ToDal(DbSet<Rating> temp)
        {
            return rating => new DalRating()
            {
                Id = rating.Id,
                TextDescId = rating.TextDescId,
                UserId = rating.UserId,
                Value = rating.value
            };
        }
    } 
}