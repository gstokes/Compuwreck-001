using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Compuwreck_001.DataTransferObjects {
    public class LocationDto {
        public int ShipwreckId { get; set; }
        public int LocationId { get; set; }
        public string ShipwreckName { get; set; }
        public double? Ltd { get; set; }
        public double? Lng { get; set; }
        public int ShipwreckFk { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }
    }
}