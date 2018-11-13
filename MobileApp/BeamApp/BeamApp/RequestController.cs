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
    [JsonObject(MemberSerialization.OptOut)]
    public class RequestModel
    {
        ////to get data
        public int UserFK { get; set; }

        //data retrieved
        //public int PK { get; set; }
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
        public Nullable<int> PackageInfoFK { get; set; }
        public Boolean IsForwardingAllowed { get; set; }
        public int Status { get; set; }
        public int WillingToPay { get; set; }
        public string FlightInformation { get; set; }
    };
    class RequestController : Controller
    {
        public RequestController()
        {
            m_httpClient = new HttpClient();

            m_uri = new Uri("http://beam.gear.host/api/request");
        }

        public async Task<bool> CreateRequest(Request.RequestType requestType, int fromCityFK, int toCityFK, DateTime dateTime, bool isUrgent, 
            int flexibility, string subject,string itemDescription,string image,int options, bool shareOnFacebook, int willingToPay, string flightInformation)
        {
            try
            {
                RequestModel request = new RequestModel()
                {
                    UserFK = App.UserPK,
                    RequestTypeFK = (int)requestType,
                    FromCityFK = fromCityFK,
                    ToCityFK = toCityFK,
                    DateTimeUtc = dateTime,
                    IsUrgent = isUrgent,
                    FlexibilityDays = flexibility,
                    Subject = subject,
                    ItemDescription = itemDescription,
                    Image = image,
                    Options = options,
                    PackageInfoFK = null,
                    AccompanyInfoFK = null,
                    WillingToPay = willingToPay,
                    FlightInformation = flightInformation 
                };

                string json = JsonConvert.SerializeObject(request, Formatting.Indented);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await m_httpClient.PostAsync(m_uri, content);
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

            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }


}
