using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Services;
using FicLibraryMvcPL.Models.AuthModels;
using BLL.Interfaces;
using FicLibraryMvcPL.Providers;

namespace FicLibraryMvcPL.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService service)
        {
            userService = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(userService.GetUserByLogin(model.UserName).Login, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    ViewBag.SuccessMessage =
                        "Авторизация прошла успешно";
                    return PartialView("Success");
                }
                ModelState.AddModelError("", "Неправильный пароль или логин");
            }
            return PartialView(model);
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            var repeatUser = userService.GetUserByEmail(model.Email);
            if (repeatUser != null)
                ModelState.AddModelError("Email", "User with this address already registered.");
            repeatUser = userService.GetUserByLogin(model.UserName);
            if (repeatUser != null)
                ModelState.AddModelError("UserName", "User with such login already registered.");

            if (!ModelState.IsValid) return PartialView(model);
            var membershipUser = ((LibraryMembershipProvider)Membership.Provider)
                .CreateUser(model);

            if (membershipUser != null)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                ViewBag.SuccessMessage =
                    "Вы успешно зарегистрированы в системе";
                return PartialView("Success");
            }
            ModelState.AddModelError("", "Error registration.");
            return PartialView(model);
        }
    }
}