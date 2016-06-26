using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interfaces;
using FicLibraryMvcPL.Infrastructure;
using FicLibraryMvcPL.Mappers;
using FicLibraryMvcPL.Models;
using FicLibraryMvcPL.Models.BllModels;

namespace FicLibraryMvcPL.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Owner,User")]
    public class TextDescriptionController : Controller
    {
        private ITextDescriptionService tdService;
        private IUserService userService;
        private ITextService textService;

        public TextDescriptionController(ITextDescriptionService tdService, IUserService userService, ITextService textService)
        {
            this.tdService = tdService;
            this.userService = userService;
            this.textService = textService;
        }

        [AllowAnonymous]
        public ActionResult Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error");
            TextDescriptionViewModel textDescription;
            try
            {
                textDescription = Mapper.ToView(tdService.GetEntityById((int) id));
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("Index", "Error");
            }
            if (!textDescription.IsPublished)
                if (!User.Identity.IsAuthenticated ) return RedirectToAction("Index", "Error");
                else if(User.Identity.Name != userService.GetEntityById(textDescription.AuthorId).Login)
                    return RedirectToAction("AccessViolation", "Error");
            textDescription.Texts = textService.GetTitleTextEntitiesWithoutContent(textDescription.Id).Select(Mapper.ToView);
            textDescription = ModelHelper.ReadTextDescriptionAdditionInfo(textDescription);
            textDescription.Comments = ModelHelper.ReadCommentsForModel("TextDescription", (int)id);
            return View(textDescription);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TextDescriptionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            model.AuthorId = userService.GetUserByLogin(User.Identity.Name).Id;
            model.CreationDate = DateTime.Now;
            var newId = tdService.CreateEntityWithIdReturn(Mapper.ToBll(model));
            return RedirectToAction("Index", new {id = newId});
        }

        [HttpGet]
        public ActionResult Edit(int? titleId)
        {
            if (titleId == null) return RedirectToAction("Index", "Error");
            TextDescriptionViewModel td;
            try
            {
                td = Mapper.ToView(tdService.GetEntityById((int) titleId));
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("Index", "Error");
            }
            if (userService.GetUserByLogin(User.Identity.Name).Id != tdService.GetEntityById((int) titleId).AuthorId)
                return RedirectToAction("AccessViolation", "Error");
            return PartialView(td);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TextDescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.LastEditDate = DateTime.Now;
                tdService.UpdateEntity(Mapper.ToBll(model));
                return Json(new {Success = true});
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int titleId)
        {
            var title = tdService.GetEntityById(titleId);
            if (userService.GetUserByLogin(User.Identity.Name).Id != title.AuthorId && !User.IsInRole("Owner"))
                return RedirectToAction("AccessViolation", "Error");
            tdService.DeleteEntity(title);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePublicationState(int textDescId)
        {
            TextDescriptionEntity title;
            try
            {
                title = tdService.GetEntityById(textDescId);
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("Index", "Error");
            }
            if (!ModelHelper.HaveAccessPrivilege(userService.GetEntityById(title.AuthorId).Login, User.Identity.Name))
                return RedirectToAction("AccessViolation", "Error");
            if (title.AuthorId != userService.GetUserByProfileId((int) Profile["Id"]).Id)
                return RedirectToAction("AccessViolation", "Error");
            title.IsPublished = !title.IsPublished;
            title.PublicationDate = title.IsPublished ?  (DateTime?)DateTime.Now : null;
            tdService.UpdateEntity(title);
            return RedirectToAction("Index", new {id = title.Id});
        }
    }
}