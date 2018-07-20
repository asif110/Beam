using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;
using System.Globalization;

namespace BeamApp
{
    class RegisterationController
    {
        [JsonObject(MemberSerialization.OptOut)]
        public class User
        {
            [JsonProperty]
            public string fName;
            [JsonProperty]
            public string lName;
            [JsonProperty]
            public string Email;
            [JsonProperty]
            public string Password;
            [JsonProperty]
            public string Phone;
            public User() { }
            //public User(string email, string password) { Email = email; Password = password; }
        }

        HttpClient httpClient = new HttpClient();

        Uri uri = new Uri("http://beam.gear.host/api/login");

        public ArrayList GetAllCountires()
        {
            ArrayList countryList = new ArrayList();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures))
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                countryList.Add(ri.EnglishName);
            }
            countryList.Sort();
            return countryList;
        }
    }
}
