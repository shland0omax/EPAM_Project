using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using FicLibraryMvcPL.Infrastructure;
using FicLibraryMvcPL.Models;
using FicLibraryMvcPL.Mappers;

namespace FicLibraryMvcPL.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Owner,User")]
    public class TextController : Controller
    {
        private ITextDescriptionService tdService;
        private ITextService textService;
        private IUserService userService;

        public TextController(ITextDescriptionService tdService, ITextService textService, IUserService userService)
        {
            this.tdService = tdService;
            this.textService = textService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Create(int titleId)
        {
            var td = tdService.GetEntityById(titleId);
            var user = userService.GetUserByLogin(User.Identity.Name);
            if (user.Id != td.AuthorId) return RedirectToAction("AccessViolation", "Error");
            var model = new TextViewModel {TitleId = titleId};
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TextViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.OrderInTitle =
                    textService.GetTitleTextEntitiesWithoutContent(model.TitleId).Select(e => e.OrderInTitle).DefaultIfEmpty().Max() + 1;
                textService.CreateEntity(Mapper.ToBll(model));
                return Json(new { Success = true });
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var text = Mapper.ToView(textService.GetEntityById(id));
            var title = tdService.GetEntityById(text.TitleId);
            if (!ModelHelper.HaveAccessPrivilege(userService.GetEntityById(title.AuthorId).Login, User.Identity.Name))
                return RedirectToAction("AccessViolation", "Error");
            ViewBag.TextDescId = title.Id;
            return View(text);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TextViewModel model)
        {
            if (ModelState.IsValid)
            {
                textService.UpdateEntity(Mapper.ToBll(model));
                return RedirectToAction("Index", new {textId = model.Id});
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var entityToDelete = textService.GetEntityById(id);
            if (entityToDelete == null) return RedirectToAction("BadRequest", "Error");
            var title = tdService.GetEntityById(entityToDelete.TitleId);
            if (!ModelHelper.HaveAccessPrivilege(userService.GetEntityById(title.AuthorId).Login, User.Identity.Name))
                return RedirectToAction("AccessViolation", "Error");
            textService.DeleteEntity(entityToDelete);
            return RedirectToAction("Index", "TextDescription", new {id = title.Id});
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(int id)
        {
            var model = Mapper.ToView(textService.GetEntityById(id));
            if (model == null) return RedirectToAction("Index", "Error");
            var td = tdService.GetEntityById(model.TitleId);
            if (!td.IsPublished)
                if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Error");
                else if (User.Identity.Name != userService.GetEntityById(td.AuthorId).Login)
                    return RedirectToAction("AccessViolation", "Error");
            ViewBag.TitleName = td.Title;
            ViewBag.IsPublished = td.IsPublished;
            model.Comments = ModelHelper.ReadCommentsForModel("Text", id);
            return View(model);
        }
    }
}