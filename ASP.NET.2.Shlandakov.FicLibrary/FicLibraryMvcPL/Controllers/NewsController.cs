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
using FicLibraryMvcPL.Models.BllModels;

namespace FicLibraryMvcPL.Controllers
{
    public class NewsController : Controller
    {
        private INewsService newsService;
        private IUserService userService;
        private static readonly int count = 5;

        //public NewsController()
        //{
        //    newsService = DependencyResolver.Current.GetService<INewsService>();
        //    this.userService = DependencyResolver.Current.GetService<IUserService>();
        //}
        public NewsController(INewsService service, IUserService userService)
        {
            newsService = service;
            this.userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error");
            NewsViewModel news;
            try
            {
                news = Mapper.ToView(newsService.GetEntityById(id.Value));
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
            var page = new NewsPageViewModel
            {
                News = news,
                Author = Mapper.ToView(DependencyResolver.Current.GetService<IUserService>().GetEntityById(news.CreatorId)),
                Comments = ModelHelper.ReadCommentsForModel("News", id.Value)
            };
            return View(page);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult MainSlider()
        {
            var news = newsService.GetLastNews(1).ToList();
            if (news.Count != 0)
                return PartialView(Mapper.ToView(news[0]));
            return PartialView();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetNewsSlide(int current, bool next)
        {
            var news = newsService.GetLastNews(NewsController.count).ToList();
            var index = 0;
            if (news.Exists(n => n.Id == current))
            {
                index = news.IndexOf(news.FirstOrDefault(n => n.Id == current)) + (next ? 1 : -1);
            }
            int countTotal = count > news.Count ? news.Count : count;
            index = (index + countTotal)%countTotal;
            var url = Url.Action("Index", "News", new {id = news[index].Id});
            return new JsonResult()
            {
                Data = new
                {
                    Success = true,
                    Image = "data:image/jpeg;base64," + Convert.ToBase64String(news[index].Image),
                    Title = news[index].Title,
                    Id = news[index].Id,
                    Link = url
                },
                MaxJsonLength = Int32.MaxValue
            };
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsPostModel model)
        {
            if (ModelState.IsValid)
            {
                byte[] image = null;
                using (var binaryReader = new BinaryReader(model.Image.InputStream))
                {
                    image = binaryReader.ReadBytes(model.Image.ContentLength);
                }
                var news = new NewsViewModel()
                {
                    Content = model.Content,
                    CreatorId = userService.GetUserByLogin(User.Identity.Name).Id,
                    Image = image,
                    PublicationDate = DateTime.Now,
                    Title = model.Title
                };
                newsService.CreateEntity(Mapper.ToBll(news));
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}