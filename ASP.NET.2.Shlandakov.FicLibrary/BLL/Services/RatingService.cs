using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class RatingService: Service<RatingEntity, DalRating>, IRatingService
    {
        private readonly IUnitOfWork uow;

        public RatingService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Ratings)
        {
            uow = unitOfWork;
        }

        public double GetAverageRatingForUser(int userId)
        {
            var textDescriptions =
                uow.TextDescriptions.GetManyByPredicate(td => td.IsPublished && td.AuthorId == userId).Select(t => t.Id);
            return
                uow.Ratings.GetManyByPredicate(r => textDescriptions.Contains(r.TextDescId))
                    .Select(rating => rating.Value).DefaultIfEmpty()
                    .Average();
        }

        public double GetAverageRatingForTitle(int titleId)
        {
            return uow.Ratings.GetManyByPredicate(r => r.TextDescId == titleId)
                .Select(rate => rate.Value).DefaultIfEmpty()
                .Average();
        }

        public RatingEntity GetRateByUserAndTitleId(int userId, int titleId)
        {
            return Mapper.ToBll(uow.Ratings.GetByPredicate(r => r.TextDescId == titleId && r.UserId == userId));
        }

        public bool CheckIfRatedByUser(int userId, int textDescId)
        {
            return uow.Ratings.GetByPredicate(r => r.UserId == userId && r.TextDescId == textDescId) != null;
        }
    }
}