using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using FicLibraryMvcPL.Infrastructure;
using FicLibraryMvcPL.Mappers;
using FicLibraryMvcPL.Models;
using FicLibraryMvcPL.Models.IdentityModels;

namespace FicLibraryMvcPL.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;
        private IProfileService profileService;
        private ITextDescriptionService tdService;
        private ICommentService commentService;
        private ICommentRelationService commentRelationService;
        private IRatingService ratingService;

        public UserController(IUserService userService, IProfileService profileService,
            ITextDescriptionService textDescriptionService, ICommentService commentService,
            ICommentRelationService commentRelationService, IRatingService ratingService)
        {
            this.userService = userService;
            this.profileService = profileService;
            this.tdService = textDescriptionService;
            this.commentService = commentService;
            this.commentRelationService = commentRelationService;
            this.ratingService = ratingService;
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            var model = new UserPageViewModel();
            if (id == null)
                return RedirectToAction("Index", "Error"); //redirect to error page
            var profileEntity = profileService.GetEntityById((int)id);
            if (profileEntity != null)
            {
                model.Profile = Mapper.ToView(profileEntity);
                model.User = Mapper.ToView(userService.GetUserByProfileId(model.Profile.Id));
                model.Stats = ModelHelper.ReadUserStats(model.User.Id, true);
                var lastPublications = tdService.GetLastUserTextDescriptionEntitiesWithSkip(model.User.Id, 5, 0).Select(Mapper.ToView).ToList();
                for (int i = 0; i < lastPublications.Count; i++)
                {
                    lastPublications[i].Author = model.User;
                    lastPublications[i].AuthorProfile = model.Profile;
                    lastPublications[i].Rating = ratingService.GetAverageRatingForTitle(lastPublications[i].Id);
                }
                model.Titles = lastPublications;
                var relationId = commentRelationService.GetCommentRelationIdByName("User");
                var lastComments =
                    commentService.GetLastCommentsAboutObjectWithSkip(relationId, model.User.Id, 5, 0).Select(Mapper.ToView).ToList();
                for (var i = 0; i < lastComments.Count; i++)
                {
                    lastComments[i].Sender = Mapper.ToView(userService.GetEntityById(lastComments[i].CommentatorId));
                    lastComments[i].SenderProfile = Mapper.ToView(profileService.GetEntityById(lastComments[i].Sender.ProfileId));
                    lastComments[i].ShortStats = ModelHelper.ReadUserStats(lastComments[i].Sender.Id, false);
                }
                model.Comments = lastComments;
                return View(model);
            }
            return RedirectToAction("Index", "Error"); //redirect to error page
        }

        [HttpPost]
        [Authorize]
        public ActionResult AvatarUpload(HttpPostedFileBase file)
        {
            byte[] fileData;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }
            var profile = profileService.GetProfileByUserLogin(User.Identity.Name);
            profile.Avatar = fileData;
            profileService.UpdateEntity(profile);
            return RedirectToAction("Index", new { profileId = Profile["Id"] });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int profileId)
        {
            if (!ModelHelper.HaveAccessPrivilege(userService.GetUserByProfileId(profileId).Login, User.Identity.Name)) return RedirectToAction("AccessViolation", "Error");
            var profile = Mapper.ToView(profileService.GetEntityById(profileId));
            return View(profile);
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileViewModel model)
        {
            var oldModel = profileService.GetEntityById(model.Id);
            oldModel.About = model.About;
            oldModel.DayOfBirth = model.DayOfBirth;
            oldModel.FirstName = model.FirstName;
            oldModel.LastName = model.LastName;
            oldModel.Mobile = model.Mobile;
            oldModel.Sex = model.Sex;
            profileService.UpdateEntity(oldModel);
            return RedirectToAction("Index", new {id = model.Id});
        }

        public ActionResult Published(int profileId)
        {
            var profile = Mapper.ToView(profileService.GetEntityById(profileId));
            var author = Mapper.ToView(userService.GetUserByProfileId(profileId));
            var publishedList = tdService.GetUsersPublishedTextDesc(author.Id).Select(Mapper.ToView).ToList();
            for (int i = 0; i < publishedList.Count; i++)
            {
                publishedList[i].Author = author;
                publishedList[i].AuthorProfile = profile;
                publishedList[i].Rating = Math.Round(ratingService.GetAverageRatingForTitle(publishedList[i].Id),2);
            }
            ViewBag.AuthorLogin = author.Login;
            return View(publishedList);
        }

        [Authorize]
        public ActionResult Unpublished(int profileId)
        {
            if (profileId != (int) Profile["Id"]) return RedirectToAction("AccessViolation", "Error");
            var profile = Mapper.ToView(profileService.GetEntityById(profileId));
            var author = Mapper.ToView(userService.GetUserByProfileId(profileId));
            var unpublished = tdService.GetUsersUnpublishedTextDesc(author.Id).Select(Mapper.ToView).ToList();
            for (int i = 0; i < unpublished.Count; i++)
            {
                unpublished[i].Author = author;
                unpublished[i].AuthorProfile = profile;
            }
            return View(unpublished);
        }
    }
}