//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Compuwreck_001.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        public int Location_id { get; set; }
        public int Shipwreck_FK { get; set; }
        public Nullable<double> LTD { get; set; }
        public Nullable<double> LNG { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }
    
        public virtual Shipwreck Shipwreck { get; set; }
    }
}
