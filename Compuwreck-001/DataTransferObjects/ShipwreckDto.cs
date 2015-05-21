using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Compuwreck_001.DataTransferObjects {
    public class ShipwreckDto {
        public int ShipwreckId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Dimensions { get; set; }
        public string Tonnage { get; set; }
        public string Armament { get; set; }
        public DateTime? DateLost { get; set; }
        public string DateExtn { get; set; }
        public string Locality { get; set; }
        public string PosExtn { get; set; }
        public string Cargo { get; set; }
        public string WindDir { get; set; }
        public string WindForce { get; set; }
        public string References { get; set; }
        public string Remarks { get; set; }
        public string U_Boat { get; set; }
        public int? EventFk { get; set; }
        public int? CountyFk { get; set; }
    }
}