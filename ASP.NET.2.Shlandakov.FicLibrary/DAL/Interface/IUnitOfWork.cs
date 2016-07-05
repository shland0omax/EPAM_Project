using DAL.DTO.Models;

namespace DAL.Interface
{
    public interface IUnitOfWork
    {
        IRepository<DalComment> Comments { get; }
        IRepository<DalCommentRelation> CommentRelation { get; }
        IProfileRepository Profiles { get; }
        ITextDescriptionRepository TextDescriptions { get; }
        ITextRepository Texts { get; }
        IRepository<DalUser> Users { get; }
        IRepository<DalRole> Roles { get; }
        IRepository<DalRating> Ratings { get; }
        IRepository<DalNews> News { get; }


        void Commit();
        //Rollback
    }
}