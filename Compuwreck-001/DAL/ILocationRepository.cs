using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public interface ILocationRepository : IDisposable {
        IEnumerable<Location> GetLocations();
        Location GetLocationById(int shipwreckId);
        Location GetLocationByShipwreckId(int shipwreckId);
       
        //void InsertLocation(Shipwreck shipwreck);
        //void DeleteLocation(int shipwreckId);
        //void UpdateLocation(Shipwreck shipwreck);
        //void Save();
    }
}