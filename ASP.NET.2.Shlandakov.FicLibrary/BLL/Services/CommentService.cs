using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class CommentService: Service<CommentEntity, DalComment>, ICommentService
    {
        private readonly IUnitOfWork uow;
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Comments)
        {
            uow = unitOfWork;
        }

        public IEnumerable<CommentEntity> GetLastCommentsAboutObjectWithSkip(int relationId, int objectId, int number, int skip)
        {
            return
                uow.Comments.GetManyByPredicate(c => c.CommentRelationId == relationId && c.ObjectId == objectId)
                    .OrderByDescending(e => e.PostDate)
                    .Skip(skip)
                    .Take(number)
                    .Select(Mapper.ToBll);
        }

        public IEnumerable<CommentEntity> GetCommentsAboutObject(int relationId, int objectId)
        {
            return
                uow.Comments.GetManyByPredicate(c => c.CommentRelationId == relationId && c.ObjectId == objectId)
                    .Select(Mapper.ToBll);
        }

        public int GetUserCommentsCountByUserId(int userId)
        {
            return uow.Comments.GetManyByPredicate(c => c.CommentatorId == userId).Count();
        }
    }
}