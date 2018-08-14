using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace BeamApp
{
    class RegisterationController
    {
        [JsonObject(MemberSerialization.OptOut)]
        public class User
        {
            [JsonProperty]
            public string FirstName { get; set; }
            [JsonProperty]
            public string LastName { get; set; }
            [JsonProperty]
            public string Email { get; set; }
            [JsonProperty]
            public string Phone { get; set; }
            [JsonProperty]
            public string Password { get; set; }
            [JsonProperty]
            public int CityFK { get; set; }
            [JsonProperty]
            public int CityTravelTo1FK { get; set; }
            [JsonProperty]
            public int CityTravelTo2FK { get; set; }


            public User() { }
            //public User(string email, string password) { Email = email; Password = password; }
        }

       

        HttpClient httpClient = new HttpClient();

        Uri uri = new Uri("http://beam.gear.host/api/user");
        Uri cityUri = new Uri("http://beam.gear.host/api/city");

      
        public async Task<bool> RegisterUser(string firstName, string lastName, string email, string pass, string phone, int cityFK, int cityTravelTo1FK, int cityTravelTo2FK)
        {
            try
            { 
            User user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = pass,
                Phone = phone,
                CityFK = cityFK,
                CityTravelTo1FK = cityTravelTo1FK,
                CityTravelTo2FK = cityTravelTo2FK
            };
                string json = JsonConvert.SerializeObject(user, Formatting.Indented);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    string res = await response.Content.ReadAsStringAsync();
                    if (res.Contains("failed"))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
