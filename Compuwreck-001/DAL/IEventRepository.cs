using System;
using System.Collections.Generic;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public interface IEventRepository : IDisposable {
        IEnumerable<Event> GetEvents();
        Event GetEventById(int countyId);
    }
}