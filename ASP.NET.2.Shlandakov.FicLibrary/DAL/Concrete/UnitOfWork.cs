using System.Data.Entity;
using DAL.DTO.Models;
using DAL.Interface;
using ORM;

namespace DAL.Concrete
{
    public class UnitOfWork: IUnitOfWork
    {
        public FicLibraryDB Context { get; private set; }

        private Repository<DalComment, Comment> comments;
        private Repository<DalCommentRelation, CommentRelation> commentRelations;
        private Repository<DalRating, Rating> ratings;
        private ProfileRepository profiles;
        private TextDescriptionRepository textDescriptions;
        private TextRepository texts;
        private Repository<DalUser, User> users;
        private Repository<DalRole, Role> roles;
        private Repository<DalNews, News> news;


        public UnitOfWork(FicLibraryDB context)
        {
            Context = context;
        }

        public IRepository<DalComment> Comments => comments ?? (comments = new Repository<DalComment, Comment>(Context));
        public IRepository<DalCommentRelation> CommentRelation
            => commentRelations ?? (commentRelations = new Repository<DalCommentRelation, CommentRelation>(Context));
        public IRepository<DalRating> Ratings => ratings ?? (ratings = new Repository<DalRating, Rating>(Context));
        public IProfileRepository Profiles => profiles ?? (profiles = new ProfileRepository(Context));
        public ITextDescriptionRepository TextDescriptions=> textDescriptions ?? (textDescriptions = new TextDescriptionRepository(Context));
        public ITextRepository Texts => texts ?? (texts = new TextRepository(Context));
        public IRepository<DalUser> Users => users ?? (users = new Repository<DalUser, User>(Context));
        public IRepository<DalRole> Roles => roles ?? (roles = new Repository<DalRole, Role>(Context));
        public IRepository<DalNews> News => news ?? (news = new Repository<DalNews, News>(Context)); 

        public void Commit()
        {
            Context?.SaveChanges();
        }
    }
}