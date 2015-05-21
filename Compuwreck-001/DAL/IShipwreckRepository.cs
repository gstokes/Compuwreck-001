using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public interface IShipwreckRepository : IDisposable {
        IEnumerable<Shipwreck> GetShipwrecks(string searchString, int? searchCounty, string searchDateStart, string searchDateEnd, string sortOrder);
        Shipwreck GetShipwreckById(int shipwreckId);
        IEnumerable<Shipwreck> ShipwreckMapData(int? shipwreckId);
        void InsertShipwreck(Shipwreck shipwreck);
        void DeleteShipwreck(int shipwreckId);
        void UpdateShipwreck(Shipwreck shipwreck);
        void Save();
    }
}
