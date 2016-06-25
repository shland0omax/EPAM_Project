using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace FicLibraryMvcPL.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Owner")]
    public class AdminController : Controller
    {
        private IUserService userService { get; set; }

        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserRole(int userId, string editorLogin, int role)
        {
            var user = userService.GetEntityById(userId);
            var editor = userService.GetUserByLogin(editorLogin);
            if(user.RoleId <= editor.RoleId) throw new ArgumentException("Недостаточно прав для изменения статуса пользователя");
            if(role < 2 && role > 5) throw new ArgumentException("Ошибка роли - такой роли не существует");
            user.RoleId = role;
            userService.UpdateEntity(user);
            return Json(new { Success = true});
        }
    }
}