using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public class ShipwreckRepository : IShipwreckRepository{
        private readonly CompuwreckEntities _db;

        public ShipwreckRepository(CompuwreckEntities db) {
            _db = db;
        }

        public IEnumerable<Shipwreck> GetShipwrecks(string searchString, int? searchCounty, string searchDateStart, string searchDateEnd, string sortOrder) 
        {
            var shipwrecks = _db.Shipwrecks.Include(s => s.County).Include(s => s.Event);
            

                // Keyword filter
                if (!String.IsNullOrEmpty(searchString))
                {
                    shipwrecks = shipwrecks.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
                }

                // County filter
                if (searchCounty != null)
                {
                    shipwrecks = shipwrecks.Where(s => s.County_FK == searchCounty);
                }

                // Date filter TODO: Catch partial date search
                if (!String.IsNullOrEmpty(searchDateEnd) && !String.IsNullOrEmpty(searchDateStart))
                {
                    DateTime dtStart = Convert.ToDateTime(searchDateStart); //TODO: DateTimetype
                    DateTime dtEnd = Convert.ToDateTime(searchDateEnd);
                    shipwrecks = shipwrecks.Where(s => s.DateLost >= dtStart && s.DateLost <= dtEnd);
                }

                if (String.IsNullOrEmpty(searchDateEnd) && !String.IsNullOrEmpty(searchDateStart))
                {
                    DateTime dt = Convert.ToDateTime(searchDateStart); //TODO: DateTimetype
                    shipwrecks = shipwrecks.Where(s => s.DateLost == dt);
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        shipwrecks = shipwrecks.OrderBy(s => s.Name);
                        break;
                    case "Date":
                        shipwrecks = shipwrecks.OrderBy(s => s.DateLost);
                        break;
                    case "date_desc":
                        shipwrecks = shipwrecks.OrderByDescending(s => s.DateLost);
                        break;
                    default:
                        shipwrecks = shipwrecks.OrderBy(s => s.Name);
                        break;
                }
            return shipwrecks.ToList();
        }

        public IEnumerable<Shipwreck> ShipwreckMapData(string searchName, int? searchCounty, string searchDateStart, string searchDateEnd) {
            var results = new List<Shipwreck>();
            results = _db.Shipwrecks.OrderBy(s => s.Name).ToList();
            
            // Filter on name or part of
            if (searchName != null)
            {
                results =
                    _db.Shipwrecks.Where(s => s.Name.ToLower().Contains(searchName.ToLower()))
                        .OrderBy(s => s.Name)
                        .ToList();
            }

            // Filter on County id
            if (searchCounty != 0)
            {
                results = results.Where(s => s.County_FK == searchCounty).ToList();
            }

            // Filter on Dates TODO: Catch partial date search
            if (!String.IsNullOrEmpty(searchDateEnd) && !String.IsNullOrEmpty(searchDateStart)) {
                DateTime dtStart = Convert.ToDateTime(searchDateStart); //TODO: DateTimetype
                DateTime dtEnd = Convert.ToDateTime(searchDateEnd);
                results = results.Where(s => s.DateLost >= dtStart && s.DateLost <= dtEnd).ToList();
            }

            if (String.IsNullOrEmpty(searchDateEnd) && !String.IsNullOrEmpty(searchDateStart)) {
                DateTime dt = Convert.ToDateTime(searchDateStart); //TODO: DateTimetype
                results = results.Where(s => s.DateLost == dt).ToList();
            }

            return results;
        }

        public Shipwreck GetShipwreckById(int id) {
            var shipwreck = _db.Shipwrecks.Find(id);
            return shipwreck;
        }

        public void InsertShipwreck(Shipwreck shipwreck) {
            _db.Shipwrecks.Add(shipwreck);
            _db.SaveChanges();
        }

        public void DeleteShipwreck(int shipwreckId) {
            Shipwreck shipwreck = _db.Shipwrecks.Find(shipwreckId);
            _db.Shipwrecks.Remove(shipwreck);
        }

        public void UpdateShipwreck(Shipwreck shipwreck) {
            _db.Entry(shipwreck).State = EntityState.Modified;
        }

        public void Save() {
            _db.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!this._disposed) {
                if (disposing) {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}