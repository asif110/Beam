﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Beam.DAT;

namespace Beam.Controllers
{
   // [Route("api/[controller]")]
    public class UserController : ApiController
    {
        

        
        // POST api/user
        [HttpPost]
        public string Post([FromBody] Beam.Models.User userModel)
        {
            string returnVal = "failed";
            if (new UserDataAccessLayer().RegisterUser(userModel.FirstName, userModel.LastName, userModel.Email, userModel.Password, userModel.Phone, userModel.CityFK, userModel.CityTravelTo1FK, userModel.CityTravelTo2FK))
            {
                returnVal = "success";
            }
            return returnVal;
        }
    }
}