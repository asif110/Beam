using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BeamApp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class User
    {
        [JsonProperty]
        public string Email;
        [JsonProperty]
        public string Password;
        public User() { }
        //public User(string email, string password) { Email = email; Password = password; }
    }
    class LoginController
    {
      
        HttpClient httpClient = new HttpClient();

        Uri uri = new Uri("http://beam.gear.host/api/login");

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                User user = new User()
                {
                    Email = email,
                    Password = password
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
                    else //success
                    {

                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
