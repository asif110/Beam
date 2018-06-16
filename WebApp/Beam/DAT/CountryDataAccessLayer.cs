using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;


namespace Beam.Models
{
    public class CountryDataAccessLayer
    {
        string m_sConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        public IEnumerable<Country> GetAllCountryControllers()
        {
            List<Country> lstCountryController = new List<Country>();

            using (SqlConnection con = new SqlConnection(m_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("CtyGetAllCountries", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
               SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Country CountryController = new Country();

                    CountryController.PK = Convert.ToInt32(rdr["ctyPK"]);
                    CountryController.NameKey = rdr["ctyNameKey"].ToString();

                    lstCountryController.Add(CountryController);
                }
                con.Close();
            }
            return lstCountryController;
        }
    }
}
