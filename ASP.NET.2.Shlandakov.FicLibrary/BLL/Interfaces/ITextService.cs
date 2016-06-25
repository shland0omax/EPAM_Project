using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface ITextService: IService<TextEntity>
    {
        IEnumerable<TextEntity> GetTitleTextEntitiesWithoutContent(int textDescId);
    }
}