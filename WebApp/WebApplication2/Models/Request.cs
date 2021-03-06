﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using System.Web.Http;


namespace Beam.Models
{
    [Route("api/[Model]")]
    public class Request
    {
        ////to get data
        public int UserFK { get; set; }

        //data retrieved
        public int PK { get; set; }
        public int RequestTypeFK { get; set; }
        public int FromCityFK { get; set; }
        public int ToCityFK { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public Boolean IsUrgent { get; set; }
        public int FlexibilityDays { get; set; }
        public string Subject { get; set; }
        public string ItemDescription { get; set; }
        public string Image { get; set; }
        public int Options { get; set; }
        public Boolean ShareOnFacebook { get; set; }
        public Nullable<int> AccompanyInfoFK { get; set; }
        public Nullable<int> PackageInfoFK { get; set; } //TODO remove
        public Boolean IsForwardingAllowed { get; set; }
        public int Status { get; set; }
        public int WillingToPay { get; set; }
        public string ReqDescritption { get; set; }
        public string reqCreatedUserFK { get; set; }
        public string ReceivedUserFK { get; set; }
        public string StatusDescription { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ReqSubject { get; set; }
        public Boolean IsStatusChange { get; set; }
        public string FromCitystr { get; set; }
        public string ToCitystr { get; set; }
        public string FlightInformation { get; set; }

    }
}