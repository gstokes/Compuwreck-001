using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compuwreck_001.DAL;
using Compuwreck_001.Models;

namespace Compuwreck_001.Controllers
{
    public class MapController : Controller
    {
        private readonly ICountyRepository _countyRepository;
        private CompuwreckEntities db = new CompuwreckEntities();
        //
        // GET: /Map/
        public MapController() {
            _countyRepository = new CountyRepository(new CompuwreckEntities());
        }

        public MapController(ICountyRepository countyRepository) {
            _countyRepository = countyRepository;
        }

        public ActionResult Index()
        {
            ViewBag.searchCounty = new SelectList(_countyRepository.GetCounties(), "County_id", "Location");
            return View();
        }
	}
}