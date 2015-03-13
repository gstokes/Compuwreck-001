using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public class CountyRepository : ICountyRepository, IDisposable {
        private readonly CompuwreckEntities _db;

        public CountyRepository(CompuwreckEntities db) {
            _db = db;
        }

        public IEnumerable<County> GetCounties() {
            return _db.Counties.OrderBy(c => c.Location).ToList();
        }

        public County GetCountyByID(int id) {
            return _db.Counties.Find(id);
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