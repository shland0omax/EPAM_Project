using BLL.Entities;

namespace BLL.Interfaces
{
    public interface ICommentRelationService: IService<CommentRelationEntity>
    {
        int GetCommentRelationIdByName(string relationName);
    }
}