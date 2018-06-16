using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Beam.Models;


namespace Beam.Controllers
{
    //[Route("api/[controller]")]
    public class CountryController : ApiController
    {
       
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            // return new string[] { "value1.1", "value2.1" };
            return new CountryDataAccessLayer().GetAllCountryControllers();
        }
    }
}