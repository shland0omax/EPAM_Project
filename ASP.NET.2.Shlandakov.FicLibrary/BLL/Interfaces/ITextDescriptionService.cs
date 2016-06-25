using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface ITextDescriptionService: IService<TextDescriptionEntity>
    {
        IEnumerable<TextDescriptionEntity> GetLastUserTextDescriptionEntitiesWithSkip(int userId, int number, int skip);
        int GetUserPublicationsCount(int userId);
        IEnumerable<TextDescriptionEntity> GetUsersUnpublishedTextDesc(int userId);
        IEnumerable<TextDescriptionEntity> GetUsersPublishedTextDesc(int userId);
        int CreateEntityWithIdReturn(TextDescriptionEntity entity);
        IEnumerable<TextDescriptionEntity> GetLastTextDescriptionEntitiesWithSkip(int number, int skip);
        IEnumerable<TextDescriptionEntity> Search(SearchEntity entity);
    }
}