using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.DTO.Models;
using DAL.Interface;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class TextRepository: Repository<DalText, Text>, ITextRepository
    {
        private readonly FicLibraryDB context;

        public TextRepository(FicLibraryDB uow): base(uow)
        {
            context = uow;
        }

        public IEnumerable<DalText> GetSortedTextsOfTitleWithoutContent(int textDecsId)
        {
            return context.Text.Select(Mapper.ToDal).Select(b => new DalText()
            {
                Id = b.Id,
                OrderInTitle = b.OrderInTitle,
                Subtitle = b.Subtitle,
                TitleId = b.TitleId
            }).Where(e => e.TitleId == textDecsId)
            .OrderBy(e => e.OrderInTitle);
        }
    }
}