using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
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
                return RedirectToAction("Index", "Error");
            ProfileEntity profileEntity;
            try
            {
                profileEntity = profileService.GetEntityById((int) id);
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("Index", "Error");
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
            model.Profile = Mapper.ToView(profileEntity);
            model.User = Mapper.ToView(userService.GetUserByProfileId(model.Profile.Id));
            model.Stats = ModelHelper.ReadUserStats(model.User.Id, true);
            var lastPublications = tdService.GetLastUserTextDescriptionEntitiesWithSkip(model.User.Id, 3, 0).Select(Mapper.ToView);
            model.Titles = ModelHelper.ReadTextDescriptionsAdditionInfo(lastPublications);
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AvatarUpload(HttpPostedFileBase file)
        {
            byte[] fileData;
            if (file != null)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileData = binaryReader.ReadBytes(file.ContentLength);
                }
            }
            else fileData = null;
            var profile = profileService.GetProfileByUserLogin(User.Identity.Name);
            profile.Avatar = fileData;
            profileService.UpdateEntity(profile);
            return RedirectToAction("Index", new { id = profile.Id });
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

        public ActionResult Published(int profileId, int pageNumber=1, int pageSize=10)
        {
            try
            {
                var profile = Mapper.ToView(profileService.GetEntityById(profileId));
                var author = Mapper.ToView(userService.GetUserByProfileId(profileId));
                var paged = tdService.GetUsersPublishedTextDesc(author.Id).Select(Mapper.ToView).ToPagedView(pageSize, pageNumber);
                var list = paged.Elements.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Author = author;
                    list[i].AuthorProfile = profile;
                    list[i].Rating = Math.Round(ratingService.GetAverageRatingForTitle(list[i].Id), 2);
                }
                paged.Elements = list;
                ViewBag.AuthorLogin = author.Login;
                return View(paged);
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("Index", "Error");
            }
            catch
            {
                return RedirectToAction("Internal", "Error");
            }
        }

        [Authorize]
        public ActionResult Unpublished(int profileId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (profileId != (int) Profile["Id"]) return RedirectToAction("AccessViolation", "Error");
                var profile = Mapper.ToView(profileService.GetEntityById(profileId));
                var author = Mapper.ToView(userService.GetUserByProfileId(profileId));
                var paged = tdService.GetUsersUnpublishedTextDesc(author.Id)
                    .Select(Mapper.ToView)
                    .ToPagedView(pageSize, pageNumber);
                var unpublished = paged.Elements.ToList();
                for (int i = 0; i < unpublished.Count; i++)
                {
                    unpublished[i].Author = author;
                    unpublished[i].AuthorProfile = profile;
                }
                paged.Elements = unpublished;
                return View(paged);
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("Index", "Error");
            }
            catch
            {
                return RedirectToAction("Internal", "Error");
            }
        }
    }
}