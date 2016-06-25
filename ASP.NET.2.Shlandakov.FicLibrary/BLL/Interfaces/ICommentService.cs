using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface ICommentService: IService<CommentEntity>
    {
        IEnumerable<CommentEntity> GetLastCommentsAboutObjectWithSkip(int relationId, int objectId, int number, int skip);
        IEnumerable<CommentEntity> GetCommentsAboutObject(int relationId, int objectId);
        int GetUserCommentsCountByUserId(int userId);
    }
}