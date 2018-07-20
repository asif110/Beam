using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Beam.Models;

namespace Beam.DAT
{
    public class ItineraryDataAccessLayer
    {
        string m_sConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        
        ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

        public bool CreateItinerary(string userEmail,int fromCityFK, int toCityFK, DateTime departureDateTime, DateTime returnDateTime, bool isDocument, bool isPackage, bool isCarpool, int modeOfTravel, string details)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UsrCreateItinerary", con);

                    //ALTER PROCEDURE [dbo].[UsrRegisterUser] @email VARCHAR(254),@pass VARCHAR(10), @phone VARCHAR(10), @fName NCHAR(24), @lName NCHAR(24), 
                    //@cityFK INT, @cityTravelTo1FK INT, @cityTravel2FK INT

                    cmd.Parameters.Add(new SqlParameter("@userEmail", userEmail));
                    cmd.Parameters.Add(new SqlParameter("@fromCityPK", fromCityFK));
                    cmd.Parameters.Add(new SqlParameter("@toCityPK", toCityFK));
                    cmd.Parameters.Add(new SqlParameter("@departureDateTime", departureDateTime));
                    cmd.Parameters.Add(new SqlParameter("@returnDateTime", returnDateTime));
                    cmd.Parameters.Add(new SqlParameter("@isDocument", isDocument));
                    cmd.Parameters.Add(new SqlParameter("@isPackage", isPackage));
                    cmd.Parameters.Add(new SqlParameter("@isCarpool", isCarpool));
                    cmd.Parameters.Add(new SqlParameter("@modeOfTravel", modeOfTravel));
                    cmd.Parameters.Add(new SqlParameter("@details", details));
                    
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
                return false;
            }
            return true;
        }
    }
}
