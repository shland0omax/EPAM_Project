using BLL.Entities;
using BLL.Interfaces;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class CommentRelationService: Service<CommentRelationEntity, DalCommentRelation>, ICommentRelationService
    {
        private readonly IUnitOfWork uow;

        public CommentRelationService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.CommentRelation)
        {
            uow = unitOfWork;
        }

        public int GetCommentRelationIdByName(string relationName)
        {
            return uow.CommentRelation.GetByPredicate(e => e.RelationName == relationName).Id;
        }
    }
}