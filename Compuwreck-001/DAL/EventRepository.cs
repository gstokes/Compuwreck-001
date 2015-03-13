using System;
using System.Collections.Generic;
using System.Linq;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public class EventRepository : IEventRepository, IDisposable {
        private readonly CompuwreckEntities _db;

        public EventRepository(CompuwreckEntities db) {
            _db = db;
        }
        public IEnumerable<Event> GetEvents() {
            return _db.Events.ToList();
        }

        public Event GetEventById(int id) {
            return _db.Events.Find(id);
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