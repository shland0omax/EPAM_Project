using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class TextDescriptionService: Service<TextDescriptionEntity, DalTextDescription>, ITextDescriptionService
    {
        private readonly IUnitOfWork uow;

        public TextDescriptionService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.TextDescriptions)
        {
            uow = unitOfWork;
        }

        public IEnumerable<TextDescriptionEntity> GetLastUserTextDescriptionEntitiesWithSkip(int userId, int number, int skip)
        {
            return
                uow.TextDescriptions.GetManyByPredicate(e => e.AuthorId == userId && e.IsPublished)
                    .OrderByDescending(e => e.PublicationDate)
                    .Skip(skip)
                    .Take(number)
                    .Select(Mapper.ToBll);
        }

        public int GetUserPublicationsCount(int userId)
        {
            return uow.TextDescriptions.GetManyByPredicate(e => e.AuthorId == userId && e.IsPublished).Count();
        }

        public IEnumerable<TextDescriptionEntity> GetUsersUnpublishedTextDesc(int userId)
        {
            return uow.TextDescriptions.GetUserUnpublishedTextDesc(userId).Select(Mapper.ToBll);
        }

        public IEnumerable<TextDescriptionEntity> GetUsersPublishedTextDesc(int userId)
        {
            return uow.TextDescriptions.GetUserPublishedTextDesc(userId).Select(Mapper.ToBll);
        }

        public int CreateEntityWithIdReturn(TextDescriptionEntity entity)
        {
            return uow.TextDescriptions.CreateWithGeneratedIdReturn(Mapper.ToDal(entity));
        }

        public IEnumerable<TextDescriptionEntity> GetLastTextDescriptionEntitiesWithSkip(int number, int skip)
        {
            return uow.TextDescriptions.GetLastPublishedTextDescWithSkip(number, skip).Select(Mapper.ToBll);
        }

        public IEnumerable<TextDescriptionEntity> Search(SearchEntity entity)
        {
            return uow.TextDescriptions.Search(Mapper.ToDal(entity)).Select(Mapper.ToBll);
        }


    }
}