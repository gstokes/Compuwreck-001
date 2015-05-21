using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Compuwreck_001.DataTransferObjects;
using Compuwreck_001.DAL;
using Compuwreck_001.Models;

namespace Compuwreck_001.Controllers.Api
{
    public class ShipwreckController : ApiController 
    {
        private readonly IShipwreckRepository _shipwreckRepo;
        private readonly ILocationRepository _locationRepo;
        private CompuwreckEntities db = new CompuwreckEntities();

        public ShipwreckController() {
            _shipwreckRepo = new ShipwreckRepository(new CompuwreckEntities());
            _locationRepo = new LocationRepository(new CompuwreckEntities());
        }

        public ShipwreckController(IShipwreckRepository shipwreckRepository) {
            _shipwreckRepo = shipwreckRepository;

        }

        public IHttpActionResult Get() 
        {
            int? shipwreckId = null;

            var locationsDto = new List<LocationDto>();
            var locationsDto002 = new List<LocationDto>();
            var shipwreckDto = new List<ShipwreckDto>();
            var locationsList = _locationRepo.GetLocations();
            var shipwrecksList = _shipwreckRepo.ShipwreckMapData(shipwreckId);

            locationsDto = locationsList.OrderBy(l => l.Shipwreck_FK).ToDtoList();
            shipwreckDto = shipwrecksList.OrderBy(s => s.Shipwreck_id).ToDtoList(); //TODO:: REMOVE TAKE

            foreach (var shipwreck in shipwreckDto){
                foreach (var location in locationsDto){
                    if (location.ShipwreckFk == shipwreck.ShipwreckId)
                    {
                        location.ShipwreckName = shipwreck.Name;
                    }
                }
            }

            foreach (var location in locationsDto)
            {
                if (location.Lng != 0d && location.Lng != null)
                {
                    locationsDto002.Add(location);
                }
            }

            return Ok(locationsDto002);


            //var shipwrecks = new List<ShipwreckDto>();
            //var shipwrecksList = _shipwreckRepo.ShipwreckMapData(shipwreckId);

            //shipwrecks = shipwrecksList.OrderBy(s => s.DateLost).ToDtoList();
            //return Ok(shipwrecks);
        }
    }
}