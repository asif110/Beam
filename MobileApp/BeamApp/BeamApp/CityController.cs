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
    class CityController : Controller
    {
       public CityController()
        {
            m_uri = new Uri("http://beam.gear.host/api/city");
        }

        public class Country
        {
            [JsonProperty]
            public string CountryCode { get; set; }
        }

        public class City
        {
            [JsonProperty]
            public string CountryCode { get; set; }
            [JsonProperty]
            public int PK { get; set; }
            [JsonProperty]
            public string NameKey { get; set; }

        }
        public static IEnumerable<CountryName> GetCountries()
        {
            return from ri in
                       from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                       select new RegionInfo(ci.LCID)
                   group ri by ri.TwoLetterISORegionName into g
                   orderby g.First().DisplayName
                   //where g.Key.Length == 2
                   select new CountryName
                   {
                       CountryId = g.Key,
                       Title = g.First().DisplayName
                   };
        }
        public class CountryName
        {
            public string CountryId { get; set; }
            public string Title { get; set; }
        }

        public async Task<List<City>> GetCities(string countryCode)
        {
            Country city = new Country() { CountryCode = countryCode };
            string json = JsonConvert.SerializeObject(city, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await m_httpClient.PostAsync(m_uri, content);
            string jsonResponse;
            //var obj = "";
            List<City> retList = new List<City>();
            if (response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync();
                jsonResponse = await response.Content.ReadAsStringAsync();
                retList = JsonConvert.DeserializeObject<List<City>>(jsonResponse);
            }

            return retList;
        }

    }
}
