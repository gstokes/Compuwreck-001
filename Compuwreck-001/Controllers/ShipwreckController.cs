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
    public class ShipwreckController : Controller
    {

        private const int PageSize = 10;
        private readonly IShipwreckRepository _shipwreckRepository;
        private readonly ICountyRepository _countyRepository;
        private readonly IEventRepository _eventRepository;

        private CompuwreckEntities db = new CompuwreckEntities();

        public ShipwreckController() {
            _shipwreckRepository = new ShipwreckRepository(new CompuwreckEntities());
            _countyRepository = new CountyRepository(new CompuwreckEntities());
            _eventRepository = new EventRepository(new CompuwreckEntities());
        }

        public ShipwreckController(IShipwreckRepository shipwreckRepository, ICountyRepository countyRepository, IEventRepository eventRepository) {
            _shipwreckRepository = shipwreckRepository;
            _countyRepository = countyRepository;
            _eventRepository = eventRepository;
        }

        // GET: /Shipwreck/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? currentCounty, int? searchCounty, string currentDateStart, string searchDateStart, string currentDateEnd, string searchDateEnd, int? page)
        {
            // Set sort parmeters
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.searchCounty = new SelectList(_countyRepository.GetCounties(), "County_id", "Location");

            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }

            if (searchCounty != null) {
                page = 1;
            }
            else {
                searchCounty = currentCounty;
            }

            if (searchDateStart != null) {
                page = 1;
            }
            else {
                searchDateStart = currentDateStart;
            }

            if (searchDateEnd != null) {
                page = 1;
            }
            else {
                searchDateEnd = currentDateEnd;
            }

            ViewBag.currentFilter = searchString;
            ViewBag.currentCounty = searchCounty;
            ViewBag.currentDateStart = searchDateStart;
            ViewBag.currentDateEnd = searchDateEnd;

            //var shipwrecks = db.Shipwrecks.Include(s => s.County).Include(s => s.Event);

            var shipwrecks = _shipwreckRepository.GetShipwrecks(searchString, searchCounty, searchDateStart, searchDateEnd, sortOrder);

            int pageNumber = (page ?? 1);
            return View(shipwrecks.ToPagedList(pageNumber, PageSize));
        }

        // GET: /Shipwreck/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            Shipwreck shipwreck = _shipwreckRepository.GetShipwreckById(id);
            //if (shipwreck == null)
            //{
            //    return HttpNotFound();
            //}
            return View(shipwreck);
        }

        // GET: /Shipwreck/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.County_FK = new SelectList(_countyRepository.GetCounties(), "County_id", "Location");
            ViewBag.Event_FK = new SelectList(_eventRepository.GetEvents(), "Event_id", "Event1");
            return View();
        }

        // POST: /Shipwreck/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Shipwreck shipwreck) {
            try {
                if (ModelState.IsValid) {
                    _shipwreckRepository.InsertShipwreck(shipwreck);
                    //_shipwreckRepository.Save();
                    return RedirectToAction("Index");
                }

                ViewBag.County_FK = new SelectList(_countyRepository.GetCounties(), "County_id", "Location", shipwreck.County_FK);
                ViewBag.Event_FK = new SelectList(_eventRepository.GetEvents(), "Event_id", "Event1", shipwreck.Event_FK);

            }

            catch (DataException /* dex */) {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(shipwreck);

        }

        // GET: /Shipwreck/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Shipwreck shipwreck = _shipwreckRepository.GetShipwreckById(id);
            if (shipwreck == null) {
                return HttpNotFound();
            }
            ViewBag.County_FK = new SelectList(_countyRepository.GetCounties(), "County_id", "Location", shipwreck.County_FK);
            ViewBag.Event_FK = new SelectList(_eventRepository.GetEvents(), "Event_id", "Event1", shipwreck.Event_FK);
            return View(shipwreck);
        }

        // POST: /Shipwreck/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPost(int id) {

            //if (id == null) {
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var shipwreckToUpdate = _shipwreckRepository.GetShipwreckById(id);
            if (TryUpdateModel(shipwreckToUpdate, "",
                new string[] { "Name", "DateLost", "Type" })) //Any fileds not listed here will not be updateable
            {
                try {
                    _shipwreckRepository.UpdateShipwreck(shipwreckToUpdate);
                    _shipwreckRepository.Save();

                    return RedirectToAction("Index");
                }
                catch (Exception /* dex */) {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.County_FK = new SelectList(_countyRepository.GetCounties(), "County_id", "Location", shipwreckToUpdate.County_FK);
            ViewBag.Event_FK = new SelectList(_eventRepository.GetEvents(), "Event_id", "Event1", shipwreckToUpdate.Event_FK);
            return View(shipwreckToUpdate);
        }

        // GET: /Shipwreck/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, bool? saveChangesError = false) {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            if (saveChangesError.GetValueOrDefault()) {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Shipwreck shipwreck = _shipwreckRepository.GetShipwreckById(id);
            if (shipwreck == null) {
                return HttpNotFound();
            }
            return View(shipwreck);
        }

        // POST: /Shipwreck/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            try {
                Shipwreck shipwreck = _shipwreckRepository.GetShipwreckById(id);
                _shipwreckRepository.DeleteShipwreck(id);
                _shipwreckRepository.Save();

            }
            catch (DataException/* dex */) {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

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
