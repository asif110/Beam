using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
/// <summary>
/// Added to catch the log exception in database
/// Parameters : LogType could be 1 for Error,2 for Information, 3 for Diagnostic
/// Paremeters : logMessage would be any messeage catch in the exception
/// </summary>
namespace Beam.Models
{
    public class ExceptionDataAccessLayer
    {
        string m_sConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

       public enum ExceptionEnum
        {
            None,
            Error = 1,
            Information = 2,
            Diagnostic = 3,
        };


        public void RegisterException(int logType, string logMessage)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("LogInsertLog", con);
                    cmd.Parameters.Add(new SqlParameter("@logType", logType));
                    cmd.Parameters.Add(new SqlParameter("@logMessage", logMessage));
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                //Error log
            }

        }
    }
}