using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.DTO.Models;
using DAL.Interface;
using ORM;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class TextDescriptionRepository: Repository<DalTextDescription, TextDescription>, ITextDescriptionRepository
    {
        private readonly FicLibraryDB context;

        public TextDescriptionRepository(FicLibraryDB uow): base(uow)
        {
            context = uow;
        }

        public int CreateWithGeneratedIdReturn(DalTextDescription td)
        {
            var e = context.TextDescription.Add(Mapper.ToOrm(td));
            context.SaveChanges();
            return e.Id;
        }

        IEnumerable<DalTextDescription> ITextDescriptionRepository.GetUserPublishedTextDesc(int userId)
        {
            var user = context.User.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return user.TextDescription.Select(Mapper.ToDal).Where(text => text.IsPublished);
            }
            throw new NullReferenceException(nameof(user));
        }

        public IEnumerable<DalTextDescription> GetUserUnpublishedTextDesc(int userId)
        {
            var user = context.User.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return user.TextDescription.Select(Mapper.ToDal).Where(text => !text.IsPublished);
            }
            throw new NullReferenceException(nameof(user));
        }

        public IEnumerable<DalTextDescription> GetLastPublishedTextDescWithSkip(int number, int skip)
        {
            return
                context.TextDescription.Where(td => td.IsPublished == true)
                    .OrderByDescending(td => td.PublicationDate)
                    .Skip(skip)
                    .Take(number).Select(Mapper.ToDal);
        }

        public IEnumerable<DalTextDescription> Search(DalSearch model)
        {
            if (model == null) throw new NullReferenceException(nameof(model));
            if (model.MinRating != null && model.MinRating < 0 || model.MinRating > 5) throw new ArgumentException(nameof(model.MinRating));
            if (model.MaxRating != null && model.MaxRating < 0 || model.MaxRating > 5) throw new ArgumentException(nameof(model.MaxRating));
            if (model.MaxRating != null && model.MaxRating != null && model.MinRating > model.MaxRating)
                throw new ArgumentException(nameof(model.MaxRating));
            var result = context.TextDescription.Where(td => td.IsPublished);
            if (model.Title != null && model.Title.Trim() != String.Empty)
            {
                if (model.SearchSubtitles)
                    result =
                        result.Where(
                            td =>
                                td.Title.ToLower().Contains(model.Title.Trim().ToLower()) ||
                                td.Text.Any(text => text.Subtitle.ToLower().Contains(model.Title.Trim().ToLower())));
                else
                    result = result.Where(td => td.Title.ToLower().Contains(model.Title.Trim().ToLower()));
            }
            if (model.PublisherLogin != null && model.PublisherLogin.Trim() != String.Empty)
                result = result.Where(td => td.User.Login.ToLower().Contains(model.PublisherLogin.ToLower()));
            if (model.PublicationDateStart != null)
                result =
                    result.Where(td => td.PublicationDate > model.PublicationDateStart);
            if (model.PublicationDateFinish != null)
                result = result.Where(td => td.PublicationDate < model.PublicationDateFinish);
            if (model.MinRating != null && model.MinRating != 0) model.IncludeUnrated = false;
            if (model.MinRating != null)
                result = result.Where(td => td.Rating.Select(r => r.value).DefaultIfEmpty().Average() >= model.MinRating);
            if (model.MaxRating != null)
                result = result.Where(td => td.Rating.Select(r => r.value).DefaultIfEmpty().Average() <= model.MaxRating);
            if (!model.IncludeUnrated)
                result = result.Where(td => td.Rating.Count != 0);
            return result.Select(Mapper.ToDal);
        }
    }
}