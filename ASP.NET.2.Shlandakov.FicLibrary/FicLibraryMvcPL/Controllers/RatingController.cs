using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interfaces;

namespace FicLibraryMvcPL.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Owner,User")]
    public class RatingController : Controller
    {
        private IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        public ActionResult Rate(int value, int titleId, int userId)
        {
            try
            {
                lock (ratingService)
                {
                    if (!ratingService.CheckIfRatedByUser(userId, titleId))
                    {
                        lock (ratingService)
                        {
                            ratingService.CreateEntity(new RatingEntity
                            {
                                TextDescId = titleId,
                                UserId = userId,
                                Value = value
                            });
                        }
                    }
                    else
                    {
                        lock (ratingService)
                        {
                            var entity = ratingService.GetRateByUserAndTitleId(userId, titleId);
                            entity.Value = value;
                            ratingService.UpdateEntity(entity);
                        }
                    }
                    var actual = Math.Round(ratingService.GetAverageRatingForTitle(titleId), 2);
                    return Json(new { ActualValue = actual });
                }
            }
            catch
            {
                return RedirectToAction("Internal", "Error");
            }
        }
    }
}