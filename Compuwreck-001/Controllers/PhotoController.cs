using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
            if (file != null) {
                //string filename = photo.FileName;
                string FileName = Path.GetFileName(file.FileName);
                string URL = Path.Combine(Server.MapPath("~/shipwreckImages/" + photo.Shipwreck_id), photo.FileName);
                string dirPath = Path.Combine(Server.MapPath("~/shipwreckImages/" + photo.Shipwreck_id));

                if (!Directory.Exists(dirPath)) {
                    Directory.CreateDirectory(dirPath);
                }

                file.SaveAs(URL);

                using (MemoryStream ms = new MemoryStream()) {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            
            if (ModelState.IsValid)
            {
                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Delete(int? id)
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

        // POST: /Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
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
