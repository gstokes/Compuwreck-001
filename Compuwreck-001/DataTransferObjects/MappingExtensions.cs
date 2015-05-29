using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using Compuwreck_001.Models;

namespace Compuwreck_001.DataTransferObjects {
    public static class MappingExtensions {
        public static ShipwreckDto ToDto(this Shipwreck shipwreck) {
            return new ShipwreckDto
            {
                ShipwreckId = shipwreck.Shipwreck_id,
                Name = shipwreck.Name,
                Type = shipwreck.Type,
                Dimensions = shipwreck.Dimensions,
                Tonnage = shipwreck.Tonnage,
                Armament = shipwreck.Armament,
                DateLost = shipwreck.DateLost,
                Locality = shipwreck.Locality,
                PosExtn = shipwreck.PosExtn,
                Cargo = shipwreck.Cargo,
                WindDir = shipwreck.WindDir,
                WindForce = shipwreck.WindForce,
                References = shipwreck.References,
                Remarks = shipwreck.Remarks,
                U_Boat = shipwreck.U_Boat,
                EventFk = shipwreck.Event_FK,
                CountyFk = shipwreck.County_FK
            };
        }

        public static shipwreckLocationDto ToDto(this Location location) {
            return new shipwreckLocationDto
            {
                ShipwreckId = location.Shipwreck.Shipwreck_id,
                LocationId = location.Location_id,
                Ltd = location.LTD,
                Lng = location.LNG,
                GeoLocation = location.GeoLocation,
                ShipwreckFk = location.Shipwreck_FK
            };
        }

        public static List<ShipwreckDto> ToDtoList(this IEnumerable<Shipwreck> shipwrecks) {
            return shipwrecks.Select(r => r.ToDto()).ToList();
        }

        public static List<shipwreckLocationDto> ToDtoList(this IEnumerable<Location> locations) {
            return locations.Select(l => l.ToDto()).ToList();
        }
    }
}