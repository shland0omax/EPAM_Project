using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IRatingService: IService<RatingEntity>
    {
        double GetAverageRatingForUser(int userId);
        double GetAverageRatingForTitle(int titleId);
        RatingEntity GetRateByUserAndTitleId(int userId, int titleId);
        bool CheckIfRatedByUser(int userId, int textDescId);
    }
}