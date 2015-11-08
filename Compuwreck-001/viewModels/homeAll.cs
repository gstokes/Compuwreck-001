using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compuwreck_001.Models;

namespace Compuwreck_001.viewModels
{
    public class homeAll
    {
        public Guid Id { get; set; }
        
        public virtual Page Page { get; set; }
        public virtual Shipwreck Shipwreck { get; set; }

        public virtual IEnumerable<Page> Pages { get; set; }
        public virtual ICollection<Shipwreck> Shipwrecks { get; set; }
    }
}