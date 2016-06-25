using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using FicLibraryMvcPL.Infrastructure;
using FicLibraryMvcPL.Mappers;
using FicLibraryMvcPL.Models;

namespace FicLibraryMvcPL.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Owner,User")]
    public class CommentController : Controller
    {
        private ICommentService commentService;
        private ICommentRelationService crService;
        private IUserService userService;

        public CommentController(ICommentRelationService commentRelationService, ICommentService commentService, IUserService userService)
        {
            this.commentService = commentService;
            crService = commentRelationService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Create(string relation, int objectId, int userId)
        {
            var relationId = crService.GetCommentRelationIdByName(relation);
            var model = new CommentViewModel { ObjectId = objectId, CommentRelationId = relationId, CommentatorId = userId};
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel model)
        {
            if ((int)Profile["Id"] != model.CommentatorId) ModelState.AddModelError("", "Попытка отправить сообщение от лица другого пользователя");
            if (ModelState.IsValid)
            {
                model.PostDate = DateTime.Now;
                commentService.CreateEntity(Mapper.ToBll(model));
                return Json(new { Success = true});
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Mapper.ToView(commentService.GetEntityById(id));
            if (!ModelHelper.HaveAccessPrivilege(userService.GetEntityById(model.CommentatorId).Login, User.Identity.Name))
                return RedirectToAction("AccessViolation", "Error");
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.LastEditDate = DateTime.Now;
                commentService.UpdateEntity(Mapper.ToBll(model));
                return Json(new { Success = true });
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var entity = commentService.GetEntityById(id);
            if (entity != null)
            {
                commentService.DeleteEntity(entity);
                return Json(new { Success = true });
            }
            return RedirectToAction("BadRequest", "Error");
        }
    }
}