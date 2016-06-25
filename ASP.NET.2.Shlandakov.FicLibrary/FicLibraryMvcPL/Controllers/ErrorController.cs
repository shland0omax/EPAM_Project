using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FicLibraryMvcPL.Models;

namespace FicLibraryMvcPL.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult Index(string message)
        {
            if (message == null) message = "404 - запрашиваемая страница не найдена!";
            return View(new ErrorViewModel {Message = message});
        }

        public ActionResult Internal()
        {
            var message = "500 - внутренняя ошибка сервера :(";
            return View("Index", new ErrorViewModel {Message = message});
        }

        public ActionResult AccessViolation()
        {
            var message = "403 - у вас недостаточно прав для просмотра этой страницы";
            return View("Index", new ErrorViewModel { Message = message });
        }

        public ActionResult BadRequest()
        {
            return View("Index", new ErrorViewModel { Message = "400 - неверный запрос" });
        }
    }
}