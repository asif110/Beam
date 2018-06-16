using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Beam.Models
{
   public class CityController : ApiController
    {
        
        
        // POST api/city
        [HttpPost]
        public IEnumerable<City> Post([FromBody] Beam.Models.City cityModel)
        {
            return new CityDataAccessLayer().GetAllCityControllers(cityModel.CountryCode);
        }
    }
}
