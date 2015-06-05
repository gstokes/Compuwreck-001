﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compuwreck_001.DataTransferObjects;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public class LocationRepository : ILocationRepository {
        private readonly CompuwreckEntities _db;

        public LocationRepository(CompuwreckEntities db) {
            _db = db;
        }

        public IEnumerable<Location> GetLocations() {
            var locations = _db.Locations.Where(l => l.LTD != 0).OrderBy(l => l.Shipwreck_FK);
            return locations.ToList();
        }

        public Location GetLocationById(int locationId) {
            var location = _db.Locations.Find(locationId);
            return location;
        }

        public Location GetLocationByShipwreckId(int ShipwreckId) {
            var location = _db.Locations.Single(l => l.Shipwreck_FK == ShipwreckId);
            return location;
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