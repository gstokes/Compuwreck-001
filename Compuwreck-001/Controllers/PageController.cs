using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Compuwreck_001.Helpers;
using Compuwreck_001.Models;
using Compuwreck_001.DAL;
using PagedList;

namespace Compuwreck_001.Controllers
{
    public class PageController : Controller
    {

        private const int PageSize = 10;
        private IPageRepository _pageRepository;

        private CompuwreckEntities db = new CompuwreckEntities();

        public PageController(){
            _pageRepository = new PageRepository(new CompuwreckEntities());
        }

        public ActionResult Index(int? published=1){
            var pages = _pageRepository.GetAllPages(published);
            return View(pages);
        }

        public ActionResult Details(int id){
            var page = _pageRepository.GetPageById(id);
            return View(page);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Create(Page page, HttpPostedFileBase file){
            try
            {
                if (ModelState.IsValid)
                {
                    _pageRepository.InsertPage(page);

                    //*****************

                    Photo photo = new Photo();
                    photo.FileName = Path.GetFileName(file.FileName);
                    photo.URL = Path.Combine(("~/Content/images/page" + page.PostId));

                    int uploadStatus = ImageHelper.UploadImage(file, photo);

                    var changedFile = Path.ChangeExtension(photo.FileName, ".jpg");
                    photo.FileName = changedFile;

                    if (uploadStatus == 1)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Photos.Add(photo);
                            db.SaveChanges();
                        }
                    }

                    //*****************
                    return RedirectToAction("Index", "Page");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }


           

            return View(page);
        }

        [HttpPost]
        public ActionResult AddPhoto(HttpPostedFileBase file, int postId)
        {
            Photo photo = new Photo();
            photo.FileName = Path.GetFileName(file.FileName);
            photo.URL = Path.Combine(("~/Content/images/page"));

            int uploadStatus = ImageHelper.UploadImage(file, photo);

            var changedFile = Path.ChangeExtension(photo.FileName, ".jpg");
            photo.FileName = changedFile;

            if (uploadStatus == 1)
            {
                if (ModelState.IsValid)
                {
                    db.Photos.Add(photo);
                    int id = photo.Photo_id;
                    db.SaveChanges();
                    return RedirectToAction("Edit", "Page", new { id = postId });
                }
            }
            else
            {
                ViewBag.ErrorMessage = "There was Problem uplaoding, please contact Administrator";
            }

            ViewBag.Shipwreck_id = new SelectList(db.Shipwrecks, "Shipwreck_id", "Name", photo.Shipwreck_id);
            return View(photo);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id){
            Page page = _pageRepository.GetPageById(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult EditPost(int id){
            var pageToUpdate = _pageRepository.GetPageById(id);
            if (TryUpdateModel(pageToUpdate, "",
                new string[] { "PageTitle", "Body", "Published", "PublishDate", "UnPublishDate" })) //Any fileds not listed here will not be updateable
            {
                try
                {
                    _pageRepository.UpdatePage(pageToUpdate);
                    _pageRepository.Save();

                    return RedirectToAction("Index");
                }
                catch (Exception /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(pageToUpdate);
        }
    }
}