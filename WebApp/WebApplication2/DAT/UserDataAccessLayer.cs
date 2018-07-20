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
    public class UserDataAccessLayer
    {
        string m_sConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

        public bool RegisterUser(string firstName, string lastName, string email, string pass,string phone, int cityFK, int cityTravelTo1FK, int cityTravelTo2FK)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UsrRegisterUser", con);

                    //ALTER PROCEDURE [dbo].[UsrRegisterUser] @email VARCHAR(254),@pass VARCHAR(10), @phone VARCHAR(10), @fName NCHAR(24), @lName NCHAR(24), 
                    //@cityFK INT, @cityTravelTo1FK INT, @cityTravel2FK INT

                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@pass", pass));
                    cmd.Parameters.Add(new SqlParameter("@phone", pass));
                    cmd.Parameters.Add(new SqlParameter("@fName", firstName));
                    cmd.Parameters.Add(new SqlParameter("@lName", lastName));
                    cmd.Parameters.Add(new SqlParameter("@cityFK", cityFK));
                    cmd.Parameters.Add(new SqlParameter("@cityTravelTo1FK", cityTravelTo1FK));
                    cmd.Parameters.Add(new SqlParameter("@cityTravelTo2FK", cityTravelTo2FK));

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);

                return false;
            }
            return true;
        }

        public bool Login(string email,string password)
        {
            bool success = false;
            try
            {
               using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UsrUserLogin", con);

                    cmd.Parameters.Add(new SqlParameter("@usrEmail", email));
                    cmd.Parameters.Add(new SqlParameter("@usrPassword", password));

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(rdr);

                    success = Convert.ToBoolean(table.Rows[0][0]);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
                return false;
            }
            return success;
        }
    }
}
