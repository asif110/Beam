using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Beam.Models;
using Beam.DAT;

namespace Beam.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Itinerary")]
    public class ItineraryController : ApiController
    {
        // GET: api/Itinerary
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

                
        // POST: api/Itinerary
        [HttpPost]
        public string Post([FromBody]Itinerary value)
        {
            string returnVal = "failed";
            if (new ItineraryDataAccessLayer().CreateItinerary(value.UserEmail,value.FromCityFK,value.ToCityFK,value.DepartureDateTime.ToUniversalTime(),value.ReturnDateTime.ToUniversalTime(),value.IsDocument,value.IsPackage,value.IsCarpool,value.ModeOfTravel,value.Details))
            {
                returnVal = "success";
            }
            return returnVal;
        }
    }
}
