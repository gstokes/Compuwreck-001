using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        public ActionResult Details(int id) {
            var page = _pageRepository.GetPageById(id);
            return View(page);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Page page) {
            try {
                if (ModelState.IsValid) {
                    _pageRepository.InsertPage(page);
                    //_shipwreckRepository.Save();
                    return RedirectToAction("Index", "Page");
                }
            }
            catch (DataException /* dex */) {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(page);
        }

       [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id) {
            Page page = _pageRepository.GetPageById(id);
            if (page == null) {
                return HttpNotFound();
            }
            return View(page);
        }

       [HttpPost, ActionName("Edit")]
       [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin")]
       public ActionResult EditPost(int id) {
           var pageToUpdate = _pageRepository.GetPageById(id);
       }
    }
}