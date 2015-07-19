using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compuwreck_001.Models;
using Compuwreck_001.DAL;
using PagedList;

namespace Compuwreck_001.Controllers
{
    public class PageController : Controller{

        private const int PageSize = 10;
        private IPageRepository _pageRepository;

        private CompuwreckEntities db = new CompuwreckEntities();

        public PageController() {
            _pageRepository = new PageRepository(new CompuwreckEntities());
        }

        public ActionResult Index(){
            var pages = _pageRepository.GetAllPages();
            return View(pages);
        }
    }
}