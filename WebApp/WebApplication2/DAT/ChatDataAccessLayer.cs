using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Configuration;

namespace Beam.Models
{
    public class ChatDataAccessLayer
    {
        string m_sConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

        public IEnumerable<Chat> GetAllChat(int RequestId)
        {
            List<Chat> lstChatController = null;
            try
            {
                lstChatController = new List<Chat>();

                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReqGetChat", con);
                    cmd.Parameters.Add(new SqlParameter("@RequestId", RequestId));
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Chat ChatController = new Chat();

                        //ChatController.PK = sPK;
                        ChatController.PK = Convert.ToInt32(rdr["rclPK"]);
                        ChatController.ChatMessage = Convert.ToString(rdr["rclText"] + "");
                        ChatController.IsRead = Convert.ToBoolean(rdr["rclIsRead"]);
                        ChatController.SentDate = Convert.ToDateTime(rdr["rclSentDateTime"]);
                        ChatController.ReceivedDate = Convert.ToDateTime(rdr["rclReceivedTime"]);
                        ChatController.FromId = Convert.ToString(rdr["rclFrom"]);
                        ChatController.ToId = Convert.ToString(rdr["rclTo"]);


                        lstChatController.Add(ChatController);

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
            }
            return lstChatController;
        }




        public bool ChatInsert(Chat Chat)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReqInsertChatLog", con);

                    //ALTER PROCEDURE [dbo].[UsrRegisterUser] @email VARCHAR(254),@pass VARCHAR(10), @phone VARCHAR(10), @fName NCHAR(24), @lName NCHAR(24), 
                    //@cityFK INT, @cityTravelTo1FK INT, @cityTravel2FK INT
                    
                    //cmd.Parameters.Add(new SqlParameter("@ChatPK", Chat.PK));
                    cmd.Parameters.Add(new SqlParameter("@strMessage", Chat.ChatMessage));
                    cmd.Parameters.Add(new SqlParameter("@ChatImage", ""));
                    cmd.Parameters.Add(new SqlParameter("@RequestId", Chat.RequestId));
                    cmd.Parameters.Add(new SqlParameter("@FromId", Convert.ToInt32(Chat.FromId)));
                    cmd.Parameters.Add(new SqlParameter("@ToId", Convert.ToInt32(Chat.ToId)));
                    cmd.Parameters.Add(new SqlParameter("@ChatDate", System.DateTime.Now));


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

        public bool ChatUpdate(Chat Chat)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateChat", con);

                    //ALTER PROCEDURE [dbo].[UsrRegisterUser] @email VARCHAR(254),@pass VARCHAR(10), @phone VARCHAR(10), @fName NCHAR(24), @lName NCHAR(24), 
                    //@cityFK INT, @cityTravelTo1FK INT, @cityTravel2FK INT
                    cmd.Parameters.Add(new SqlParameter("@ChatPK", Chat.PK));
                    cmd.Parameters.Add(new SqlParameter("@ChatMessage", Chat.ChatMessage));
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