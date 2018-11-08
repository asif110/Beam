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
    public class RequestDataAccessLayer
    {
        string m_sConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        ExceptionDataAccessLayer ExcData = new ExceptionDataAccessLayer();

        public IEnumerable<Request> GetAllRequestControllers(int sPK)
        {
            List<Request> lstRequestController = null;
            try
            {
                lstRequestController = new List<Request>();

                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReqGetRequest", con);
                    cmd.Parameters.Add(new SqlParameter("@reqPK", sPK));
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Request RequestController = new Request();

                        RequestController.PK = sPK;
                        RequestController.PK = Convert.ToInt32(rdr["reqPK"]);
                        RequestController.RequestTypeFK = Convert.ToInt32(rdr["reqRequestTypeFK"]);
                        RequestController.FromCityFK = Convert.ToInt32(rdr["reqFromCityFK"]);
                        RequestController.UserName = Convert.ToString(rdr["usrFirstName"]);
                        RequestController.UserName = RequestController.UserName + " " + Convert.ToString(rdr["usrLastName"]);
                        RequestController.ToCityFK = Convert.ToInt32(rdr["reqToCityFK"]);
                        RequestController.DateTimeUtc = Convert.ToDateTime(rdr["reqDateTimeUtc"]);
                        RequestController.IsUrgent = Convert.ToBoolean(rdr["reqIsUrgent"]);
                        RequestController.FlexibilityDays = Convert.ToInt32(rdr["reqFlexibilityDays"]);
                        RequestController.Subject = rdr["reqToCityFK"].ToString();
                        RequestController.ItemDescription = rdr["reqItemDescription"].ToString();
                        RequestController.Image = Convert.ToString(rdr["reqImage"]);
                        RequestController.Options = Convert.ToInt32(rdr["reqOptions"]);
                        RequestController.ShareOnFacebook = Convert.ToBoolean(rdr["reqShareOnFacebook"]);
                        RequestController.AccompanyInfoFK = Convert.ToInt32(rdr["reqaAccompanyInfoFK"]);
                        RequestController.PackageInfoFK = Convert.ToInt32(rdr["reqPackageInfoFK"]);
                        RequestController.IsForwardingAllowed = Convert.ToBoolean(rdr["reqIsForwardingAllowed"]);
                        RequestController.Status = Convert.ToInt32(rdr["reqStatus"]);
                        RequestController.WillingToPay = Convert.ToInt32(rdr["reqWillingToPay"]);
                        if (RequestController.Status == 0)
                        { RequestController.StatusDescription = "undecided"; }
                        else if (RequestController.Status == 1)
                        { RequestController.StatusDescription = "Accepted by taker "; }
                        else
                        {
                            RequestController.StatusDescription = "Declined by taker";
                        }
                        RequestController.FromCitystr = Convert.ToString(rdr["CityFromName"]);
                        RequestController.ToCitystr = Convert.ToString(rdr["CityToName"]);

                        lstRequestController.Add(RequestController);

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
            }
            return lstRequestController;
        }

        public IEnumerable<Request> GetAllRequestReceivedControllers(int sPK, int resUserPK,string reqType)
        {
            List<Request> lstRequestController = null;
            string spName = "";
            if (reqType=="received")
            {
                spName = "ReqGetRequestReceived";
            }
            else
            {
                spName = "ReqGetRequestSent";
            }
            //ReqGetRequestReceived
            try
            {
                lstRequestController = new List<Request>();

                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(spName, con);
                    cmd.Parameters.Add(new SqlParameter("@reqPK", sPK));
                    cmd.Parameters.Add(new SqlParameter("@resUserFK", resUserPK));
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Request RequestController = new Request();

                        //RequestController.PK = sPK;
                        RequestController.PK = Convert.ToInt32(rdr["reqPK"]);
                        RequestController.ReqDescritption = Convert.ToString(rdr["rtpDescritption"]);
                        RequestController.UserName = Convert.ToString(rdr["usrFirstName"]);
                        RequestController.UserName = RequestController.UserName + " " + Convert.ToString(rdr["usrLastName"]);
                        RequestController.RequestTypeFK = Convert.ToInt32(rdr["reqRequestTypeFK"]);
                        RequestController.FromCityFK = Convert.ToInt32(rdr["reqFromCityFK"]);
                        RequestController.ToCityFK = Convert.ToInt32(rdr["reqToCityFK"]);
                        RequestController.DateTimeUtc = Convert.ToDateTime(rdr["reqDateTimeUtc"]);
                        RequestController.IsUrgent = Convert.ToBoolean(rdr["reqIsUrgent"]);
                        RequestController.FlexibilityDays = Convert.ToInt32(rdr["reqFlexibilityDays"]);
                        RequestController.Subject = rdr["reqSubject"].ToString();
                        RequestController.ItemDescription = rdr["reqItemDescription"].ToString();
                        RequestController.Image = Convert.ToString(rdr["reqImage"]);
                        RequestController.Options = Convert.ToInt32(rdr["reqOptions"]);
                        RequestController.ShareOnFacebook = Convert.ToBoolean(rdr["reqShareOnFacebook"]);
                        RequestController.AccompanyInfoFK = Convert.ToInt32(rdr["reqaAccompanyInfoFK"]);
                        RequestController.PackageInfoFK = Convert.ToInt32(rdr["reqPackageInfoFK"]);
                        RequestController.IsForwardingAllowed = Convert.ToBoolean(rdr["reqIsForwardingAllowed"]);
                        RequestController.Status = Convert.ToInt32(rdr["reqStatus"]);
                        RequestController.WillingToPay = Convert.ToInt32(rdr["reqWillingToPay"]);
                        RequestController.ReqSubject = Convert.ToString(rdr["reqSubject"]);
                        if (RequestController.Status == 0)
                        { RequestController.StatusDescription = "undecided"; }
                        else if (RequestController.Status == 1)
                        { RequestController.StatusDescription = "Accepted by taker "; }
                        else
                        {
                            RequestController.StatusDescription = "Declined by taker";
                        }
                        RequestController.FromCitystr = Convert.ToString(rdr["CityFromName"]);
                        RequestController.ToCitystr = Convert.ToString(rdr["CityToName"]);

                        lstRequestController.Add(RequestController);
                        
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                ExcData.RegisterException((int)ExceptionDataAccessLayer.ExceptionEnum.Error, ex.Message);
            }
            return lstRequestController;
        }


        public bool RequestInsert(Request request)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReqInsertRequest", con);

                    //ALTER PROCEDURE [dbo].[UsrRegisterUser] @email VARCHAR(254),@pass VARCHAR(10), @phone VARCHAR(10), @fName NCHAR(24), @lName NCHAR(24), 
                    //@cityFK INT, @cityTravelTo1FK INT, @cityTravel2FK INT
                    //cmd.Parameters.Add(new SqlParameter("@reqPK", request.PK));
                    cmd.Parameters.Add(new SqlParameter("@reqUserFK", request.UserFK));
                    cmd.Parameters.Add(new SqlParameter("@reqRequestTypeFK", request.RequestTypeFK));
                    cmd.Parameters.Add(new SqlParameter("@reqFromCityFK", request.FromCityFK));
                    cmd.Parameters.Add(new SqlParameter("@reqToCityFK", request.ToCityFK));
                    cmd.Parameters.Add(new SqlParameter("@reqDateTimeUtc", request.DateTimeUtc));
                    cmd.Parameters.Add(new SqlParameter("@reqIsUrgent", request.IsUrgent));
                    cmd.Parameters.Add(new SqlParameter("@reqFlexibilityDays", request.FlexibilityDays));
                    cmd.Parameters.Add(new SqlParameter("@reqSubject", request.Subject));
                    cmd.Parameters.Add(new SqlParameter("@reqItemDescription", request.ItemDescription));
                    cmd.Parameters.Add(new SqlParameter("@reqImage", request.Image));
                    cmd.Parameters.Add(new SqlParameter("@reqOptions", request.Options));
                    cmd.Parameters.Add(new SqlParameter("@reqShareOnFacebook", request.ShareOnFacebook));
                    cmd.Parameters.Add(new SqlParameter("@reqaAccompanyInfoFK", request.AccompanyInfoFK));
                    cmd.Parameters.Add(new SqlParameter("@reqPackageInfoFK", request.PackageInfoFK));
                    cmd.Parameters.Add(new SqlParameter("@reqIsForwardingAllowed", request.IsForwardingAllowed));
                    cmd.Parameters.Add(new SqlParameter("@reqStatus", request.Status));
                    cmd.Parameters.Add(new SqlParameter("@reqWillingToPay", request.WillingToPay));

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

        public bool RequestUpdate(Request request)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReqUpdateRequest", con);

                    //ALTER PROCEDURE [dbo].[UsrRegisterUser] @email VARCHAR(254),@pass VARCHAR(10), @phone VARCHAR(10), @fName NCHAR(24), @lName NCHAR(24), 
                    //@cityFK INT, @cityTravelTo1FK INT, @cityTravel2FK INT
                    cmd.Parameters.Add(new SqlParameter("@reqPK", request.PK));
                    cmd.Parameters.Add(new SqlParameter("@reqUserFK", request.UserFK));
                    cmd.Parameters.Add(new SqlParameter("@reqRequestTypeFK", request.RequestTypeFK));
                    cmd.Parameters.Add(new SqlParameter("@reqFromCityFK", request.FromCityFK));
                    cmd.Parameters.Add(new SqlParameter("@reqToCityFK", request.ToCityFK));
                    cmd.Parameters.Add(new SqlParameter("@reqDateTimeUtc", request.DateTimeUtc));
                    cmd.Parameters.Add(new SqlParameter("@reqIsUrgent", request.IsUrgent));
                    cmd.Parameters.Add(new SqlParameter("@reqFlexibilityDays", request.FlexibilityDays));
                    cmd.Parameters.Add(new SqlParameter("@reqSubject", request.Subject));
                    cmd.Parameters.Add(new SqlParameter("@reqItemDescription", request.ItemDescription));
                    cmd.Parameters.Add(new SqlParameter("@reqImage", request.Image));
                    cmd.Parameters.Add(new SqlParameter("@reqOptions", request.Options));
                    cmd.Parameters.Add(new SqlParameter("@reqShareOnFacebook", request.ShareOnFacebook));
                    cmd.Parameters.Add(new SqlParameter("@reqaAccompanyInfoFK", request.AccompanyInfoFK));
                    cmd.Parameters.Add(new SqlParameter("@reqPackageInfoFK", request.PackageInfoFK));
                    cmd.Parameters.Add(new SqlParameter("@reqIsForwardingAllowed", request.IsForwardingAllowed));
                    cmd.Parameters.Add(new SqlParameter("@reqStatus", request.Status));
                    cmd.Parameters.Add(new SqlParameter("@reqWillingToPay", request.WillingToPay));

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

        public bool RequestStatusUpdate(Request request)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReqUpdateRequestStatus", con);

                    //ALTER PROCEDURE [dbo].[UsrRegisterUser] @email VARCHAR(254),@pass VARCHAR(10), @phone VARCHAR(10), @fName NCHAR(24), @lName NCHAR(24), 
                    //@cityFK INT, @cityTravelTo1FK INT, @cityTravel2FK INT
                    cmd.Parameters.Add(new SqlParameter("@reqPK", request.PK));
                    
                    cmd.Parameters.Add(new SqlParameter("@reqStatus", request.Status));

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

        // DELETE api/values/5
        public bool RequestDelete(int sPK)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(m_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReqDeleteRequest", con);
                    cmd.Parameters.Add(new SqlParameter("@reqPK", sPK));

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