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
    //[Route("api/Login")]
    public class LoginController : ApiController
    {
        // POST: api/Login
        [HttpPost]
        public string Post([FromBody]Login model)
        {
            string returnVal = "failed";
            if (new UserDataAccessLayer().Login(model.Email, model.Password))
            {
                returnVal = "success";
            }
            return returnVal;
        }
    }
}
