using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compuwreck_001.Models;
using Compuwreck_001.viewModels;
using Compuwreck_001.DAL;

namespace Compuwreck_001.Controllers {
    public class HomeController : Controller {


        private readonly IPageRepository _pageRepository;

        private CompuwreckEntities db = new CompuwreckEntities();

        public HomeController(){
            _pageRepository = new PageRepository(new CompuwreckEntities());
        }

        public ActionResult Index() {
            var homeAllView = new homeAll();
            //homeAllView.Shipwreck = _shipwreckRepository.GetShipwreckById(123);
            homeAllView.Pages = _pageRepository.GetTop5Pages();
            return View(homeAllView);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}