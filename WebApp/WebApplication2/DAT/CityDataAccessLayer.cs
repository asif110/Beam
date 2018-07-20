using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace Beam.Models
{
    public class CityDataAccessLayer
    {
        string m_sConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();
       
        public IEnumerable<City> GetAllCityControllers(string sCountryCode)
        {
            List<City> lstCityController = null;
            try
            {
                lstCityController = new List<City>();

                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("CitGetAllCitiesFromCountryCode", con);
                    cmd.Parameters.Add(new SqlParameter("@countryCode", sCountryCode));
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        City CityController = new City();

                        CityController.CountryCode = sCountryCode;
                        CityController.PK = Convert.ToInt32(rdr["citPK"]);
                        CityController.NameKey = rdr["citNameKey"].ToString();

                        lstCityController.Add(CityController);
                        
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                
                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
            }
            return lstCityController;
        }
    }
}
