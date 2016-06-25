using System.Collections.Generic;
using DAL.DTO;
using DAL.DTO.Models;

namespace DAL.Interface
{
    public interface ITextDescriptionRepository: IRepository<DalTextDescription>
    {
        int CreateWithGeneratedIdReturn(DalTextDescription td);
        IEnumerable<DalTextDescription> GetUserPublishedTextDesc(int userId);
        IEnumerable<DalTextDescription> GetUserUnpublishedTextDesc(int userId);
        IEnumerable<DalTextDescription> GetLastPublishedTextDescWithSkip(int number, int skip);
        IEnumerable<DalTextDescription> Search(DalSearch model);
    }
}