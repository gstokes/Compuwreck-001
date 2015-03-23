using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Compuwreck_001.Helpers;
using Compuwreck_001.Models;

namespace Compuwreck_001.Controllers
{
    public class PhotoController : Controller
    {
        private CompuwreckEntities db = new CompuwreckEntities();

        // GET: /Photo/
        public ActionResult Index()
        {
            var photos = db.Photos.Include(p => p.Shipwreck);
            return View(photos.ToList());
        }

        // GET: /Photo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: /Photo/Create
        public ActionResult Create(int shipwreckId)
        {
            Photo photoModel = new Photo();
            photoModel.Shipwreck_id = shipwreckId;
            return View(photoModel);
        }

        // POST: /Photo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, Photo photo) {

            photo.FileName = Path.GetFileName(file.FileName);
            photo.URL = Path.Combine(("~/shipwreckImages/" + photo.Shipwreck_id));
            
            int uploadStatus = ImageHelper.UploadImage(file, photo);

            var changedFile = Path.ChangeExtension(photo.FileName, ".jpg");
            photo.FileName = changedFile;
            
            if (uploadStatus == 1)
            {
                if (ModelState.IsValid) {
                    db.Photos.Add(photo);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Shipwreck");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "There was Problem uplaoding, please contact Administrator";
            }

            ViewBag.Shipwreck_id = new SelectList(db.Shipwrecks, "Shipwreck_id", "Name", photo.Shipwreck_id);
            return View(photo);
        }

        // GET: /Photo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Shipwreck_id = new SelectList(db.Shipwrecks, "Shipwreck_id", "Name", photo.Shipwreck_id);
            return View(photo);
        }

        // POST: /Photo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Photo_id,Name,Details,FileName,URL,Source,Shipwreck_id")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Shipwreck_id = new SelectList(db.Shipwrecks, "Shipwreck_id", "Name", photo.Shipwreck_id);
            return View(photo);
        }

        // GET: /Photo/Delete/5
        public ActionResult Delete(int? photoId)
        {
            if (photoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(photoId);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: /Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int photoId)
        {
            Photo photo = db.Photos.Find(photoId);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Edit", "Shipwreck", new {id=photo.Shipwreck_id});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
