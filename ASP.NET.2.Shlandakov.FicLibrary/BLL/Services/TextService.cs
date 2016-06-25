using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class TextService: Service<TextEntity, DalText>, ITextService
    {
        private readonly IUnitOfWork uow;

        public TextService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Texts)
        {
            uow = unitOfWork;
        }

        public IEnumerable<TextEntity> GetTitleTextEntitiesWithoutContent(int textDescId)
        {
            return uow.Texts.GetSortedTextsOfTitleWithoutContent(textDescId).Select(Mapper.ToBll);
        }
    }
}