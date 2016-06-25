using System.Collections.Generic;
using DAL.DTO.Models;

namespace DAL.Interface
{
    public interface ITextRepository: IRepository<DalText>
    {
        IEnumerable<DalText> GetSortedTextsOfTitleWithoutContent(int textDecsId);
    }
}