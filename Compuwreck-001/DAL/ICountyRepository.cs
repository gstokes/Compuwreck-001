using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL {
    public interface ICountyRepository : IDisposable {
        IEnumerable<County> GetCounties();
        County GetCountyByID(int countyId);
    }
}
