using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class UserLoginAccess:DataAccessBase
    {
        internal void Save(UserProfile objUserProfile, UserLogin objUserLogin)
        {
            try
            {
                string sql = @"SELECT A.[LoginInfoID] FROM [UserLoginHeader] A WHERE  A.[CompanyID] = "+ objUserLogin.CompanyID + "" +
                    " AND  A.[BranchID] = "+ objUserLogin.BranchID + " AND  A.[ApplicationID] = "+ objUserLogin.ApplicationID + " " +
                    " AND   A.[EntryUserID] = '"+ objUserLogin.EntryUserName + "' AND  A.[UserWiseLoginDate] = '"+ objUserProfile.RegionalTime + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                LoginUserInformation.LoginInfoID = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);

                LoginUserInformation.UniqueSessionCode = objclsDataManipulation.GetAnUniqueidentifierNumber(this.ConnectionString);

                var storedProcedureComandText = @"";
                if (LoginUserInformation.LoginInfoID == "" || LoginUserInformation.LoginInfoID == null)
                {
                    LoginUserInformation.LoginInfoID = LoginUserInformation.UniqueSessionCode;
                    storedProcedureComandText += @"INSERT INTO [UserLoginHeader] ([CompanyID],[BranchID],[ApplicationID],[EntryUserID]" +
                " ,[UserWiseLoginDate],[LoginInfoID]) VALUES (" +
               "" + objUserLogin.CompanyID + "" +
               "," + objUserLogin.BranchID + "" +
               "," + objUserLogin.ApplicationID + "" +
               ",'" + objUserLogin.EntryUserName + "'" +
               ",'" + objUserProfile.RegionalTime + "'" +
               ",'" + LoginUserInformation.LoginInfoID + "'" +
               " );";

                }

                storedProcedureComandText += @" INSERT INTO [UserLoginDetails] ([LoginInfoID],[SessionID],[UserWiseLoginTime],[ServerWiseLoginDate]
           ,[ServerWiseLoginTime],[LoginIP],[BrowserName],[DeviceType],[CountryCode],[CountryName]
           ,[RegionCode],[UserWiseLogOutDate],[UserWiseLogOutTime],[ServerWiseLogOutDate]
           ,[ServerWiseLogOutTime]) VALUES (" +
               "'" + LoginUserInformation.LoginInfoID + "'" +
               ",'" + LoginUserInformation.UniqueSessionCode + "'" +
               ",'" + objUserProfile.RegionalTime + "'" +
               "," + "CAST(GETDATE() AS DateTime)" + "" +
               "," + "CAST(GETDATE() AS DateTime)" + "" +
               ",'" + objUserProfile.DeviceIP + "'" +
               ",'" + objUserProfile.BrowserName + "'" +
               ",'" + objUserProfile.DeviceType + "'" +
               ",'" + objUserProfile.CountryCode + "'" +
               ",'" + objUserProfile.CountryName + "'" +
               ",'" + objUserProfile.LoginRegion + "'" +
               ",'" + objUserProfile.RegionalTime + "'" +
               ",'" + objUserProfile.RegionalTime + "'" +
               "," + "CAST(GETDATE() AS DateTime)" + "" +
               "," + "CAST(GETDATE() AS DateTime)" + "" +
               " );";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void Update(UserProfile objUserProfile)
        {
            try
            {
                var storedProcedureComandText = @"UPDATE [UserLoginDetails]
                   SET [UserWiseLogOutDate] = ISNULL('" + objUserProfile.RegionalTime + "'" + ",UserWiseLogOutDate)"
                   + ",[UserWiseLogOutTime] = ISNULL('" + objUserProfile.RegionalTime + "',UserWiseLogOutTime)"
                   + ",[ServerWiseLogOutDate] = ISNULL(GETDATE(),ServerWiseLogOutDate)"
                  + ",[ServerWiseLogOutTime] = ISNULL(GETDATE(),ServerWiseLogOutTime)"
                  + " WHERE [LoginInfoID] = '"+ objUserProfile.LoginInfoID + "' AND [SessionID] = '" + objUserProfile.UniqueSessionCode + "';";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetLoginRecord(UserLogin objUserLogin)
        {
            try
            {
                DataTable dtLoginData = null;
                var storedProcedureComandText = "";
                storedProcedureComandText = @"SELECT DISTINCT 
                 A.[LoginInfoID],
	             A.[UserWiseLoginDate],
	             B.SessionID,
	             B.UserWiseLoginTime,
	             B.LoginIP,
	             B.BrowserName,
	             B.DeviceType,
	             B.CountryCode,
	             B.CountryName,
	             B.RegionCode,
	             B.[UserWiseLogOutDate],
	             B.[UserWiseLogOutTime]
              FROM [UserLoginHeader] A 
              INNER JOIN [UserLoginDetails] B ON A.LoginInfoID = b.LoginInfoID
              WHERE  A.[CompanyID] = "+ objUserLogin.CompanyID+ "" +
              " AND  A.[BranchID] = "+ objUserLogin.BranchID + " " +
              "AND  A.[ApplicationID] = "+ objUserLogin .ApplicationID+ "" +
              " AND   A.[EntryUserID] = '"+ objUserLogin .EntryUserName + "'" +
              " AND  A.[UserWiseLoginDate] = '"+ objUserLogin .UserLoginDate+ "'" +
              " ORDER BY B.UserWiseLoginTime DESC ;";
                dtLoginData = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtLoginData;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetLoginRecord(UserProfile objUserProfile, UserLogin objUserLogin, string browserName
            , string deviceType, string countryName, string userList)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"SELECT DISTINCT 
                 A.[LoginInfoID],
	             A.[UserWiseLoginDate],
	             B.SessionID,
	             B.UserWiseLoginTime,
	             B.LoginIP,
	             B.BrowserName,
	             B.DeviceType,
	             B.CountryCode,
	             B.CountryName,
	             B.RegionCode,
	             B.[UserWiseLogOutDate],
	             B.[UserWiseLogOutTime],
                CONVERT(DATETIME, CONVERT(CHAR(8), A.[UserWiseLoginDate], 112) + ' ' + CONVERT(CHAR(8), B.UserWiseLoginTime, 108)) as loginDateTime
              FROM [UserLoginHeader] A 
              INNER JOIN [UserLoginDetails] B ON A.LoginInfoID = b.LoginInfoID
              WHERE  A.[CompanyID] = " + objUserLogin.CompanyID + "" +
              " AND  A.[BranchID] = " + objUserLogin.BranchID + " " +
              " AND  A.[ApplicationID] = " + objUserLogin.ApplicationID + "";

                if (browserName != "null")
                {
                    storedProcedureComandText += " AND B.BrowserName = '" + browserName + "'";
                }

                if (deviceType != "null")
                {
                    storedProcedureComandText += " AND B.DeviceType = '" + deviceType + "'";
                }

                if (countryName != "null")
                {
                    storedProcedureComandText += " AND B.CountryCode = '" + countryName + "'";
                }

                if (userList != null)
                {
                    storedProcedureComandText += " AND A.[EntryUserID] ='" + userList + "'";
                }

                if (objUserProfile.FromDate != null && objUserProfile.FromDate != null && objUserProfile.FromDate != "" && objUserProfile.FromDate != "")
                {
                    storedProcedureComandText += " AND A.[UserWiseLoginDate] BETWEEN '" + objUserProfile.FromDate + "' AND '" + objUserProfile.ToDate + "'";
                }

                storedProcedureComandText += " ORDER BY loginDateTime DESC;";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}