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
    public class HomeController : Controller
    {
        private ITextDescriptionService tdService;

        public HomeController(ITextDescriptionService textDescriptionService)
        {
            tdService = textDescriptionService;
        }
        public ActionResult Index()
        {
            var publications = tdService.GetLastTextDescriptionEntitiesWithSkip(5, 0).Select(Mapper.ToView);
            publications = ModelHelper.ReadTextDescriptionsAdditionInfo(publications);
            return View(publications);
        }


        [HttpGet]
        [Authorize]
        public ActionResult Search(string searchString)
        {
            var model = new SearchViewModel();
            if (searchString != null)
            {
                model.Title = searchString;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RunSearch(SearchViewModel model, int pageNumber=1, int pageItemCount=5)
        {
            try
            {
                var result = tdService.Search(Mapper.ToBll(model)).Select(Mapper.ToView);
                result = ModelHelper.ReadTextDescriptionsAdditionInfo(result);
                if (model.SortParameter == SortParameter.Rating)
                    result = result.OrderByDescending(td => td.Rating);
                else if (model.SortParameter == SortParameter.PublicationDate)
                    result = result.OrderByDescending(td => td.PublicationDate);
                return PartialView(result.ToPagedView(pageItemCount, pageNumber));
            }
            catch
            {
                return PartialView(null);
            }
        }
    }
}