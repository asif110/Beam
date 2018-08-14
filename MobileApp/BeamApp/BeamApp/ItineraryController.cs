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
    class ItineraryController : Controller
    {
        public class Itinerary
        {
            [JsonProperty]
            public string UserEmail { get; set; }
            [JsonProperty]
            public int FromCityFK { get; set; }
            [JsonProperty]
            public int ToCityFK { get; set; }
            [JsonProperty]
            public DateTime DepartureDateTime { get; set; }
            [JsonProperty]
            public DateTime ReturnDateTime { get; set; }
            [JsonProperty]
            public bool IsDocument { get; set; }
            [JsonProperty]
            public bool IsPackage { get; set; }
            [JsonProperty]
            public bool IsCarpool { get; set; }
            [JsonProperty]
            public int ModeOfTravel { get; set; }
            [JsonProperty]
            public string Details { get; set; }
        }
        public ItineraryController()
        {
            m_uri = new Uri("http://beam.gear.host/api/Itinerary");
            m_httpClient = new HttpClient();
        }
        public async Task<bool> CreateItinerary(string userEmail, int fromCityFK, int toCityFK, DateTime departureDateTime, DateTime returnDateTime, bool isDocument, bool isPackage, bool isCarpool, int modeOfTravel, string details)
        {
            try
            {
                Itinerary itinerary = new Itinerary()
                {
                    UserEmail = userEmail,
                    FromCityFK = fromCityFK,
                    ToCityFK = toCityFK,
                    DepartureDateTime = departureDateTime,
                    ReturnDateTime = returnDateTime,
                    IsDocument = isDocument,
                    IsPackage = isPackage,
                    IsCarpool = isCarpool,
                    ModeOfTravel = modeOfTravel,
                    Details = details
                };
                string json = JsonConvert.SerializeObject(itinerary, Formatting.Indented);
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
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
