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

        public IHttpActionResult Get(string searchName, int county, string dateStart, string dateEnd) 
        {

            var shipwrecklocationsDto = new List<shipwreckLocationDto>();
            var shipwreckDto = new List<ShipwreckDto>();
            var shipwreckLocationsResultDto = new List<shipwreckLocationDto>();
           
            var locationsList = _locationRepo.GetLocations();
            var shipwrecksList = _shipwreckRepo.ShipwreckMapData(searchName, county, dateStart, dateEnd);

            if (shipwrecksList != null) {
                shipwrecklocationsDto = locationsList.OrderBy(l => l.Shipwreck_FK).ToDtoList();
                shipwreckDto = shipwrecksList.OrderBy(s => s.Shipwreck_id).ToDtoList(); //TODO:: REMOVE TAKE

                foreach (var shipwreck in shipwreckDto){
                    foreach (var location in shipwrecklocationsDto) {
                        if (location.ShipwreckFk == shipwreck.ShipwreckId)
                        {
                            location.ShipwreckName = shipwreck.Name;
                            shipwreckLocationsResultDto.Add(location);
                        }
                    }
                }
            }
            return Ok(shipwreckLocationsResultDto);
        }

        public IHttpActionResult Get(int shipwreckId) {
            var shipwreck = _shipwreckRepo.GetShipwreckById(shipwreckId);
            var locationsList = _locationRepo.GetLocationByShipwreckId(shipwreckId);
            var shipwreckLocationDto = new shipwreckLocationDto();


            if (shipwreck != null)
            {
                shipwreckLocationDto = locationsList.ToDto();
                shipwreckLocationDto.ShipwreckName = shipwreck.Name;
            }
            if (shipwreckLocationDto.Lng == 0d || shipwreckLocationDto.Lng == null)
            {
                shipwreckLocationDto.Lng = -7.849708;
                shipwreckLocationDto.Ltd = 53.8201202;
            }
            return Ok(shipwreckLocationDto);
        }
    }
}