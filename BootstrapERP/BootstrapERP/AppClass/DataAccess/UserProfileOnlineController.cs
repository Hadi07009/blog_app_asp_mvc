using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



namespace BootstrapERP.AppClass.DataAccess
{
    public class UserProfileOnlineController : DataAccessBase
    {
        internal void SaveUserProfileOnline(UserProfileOnline objUserProfileOnline, CompanyDetailsSetup objCompanyDetailsSetup)
        {
            try
            {
                bool checkUserEmail = true;
                checkUserEmail = CountUserEmail(objUserProfileOnline);
                if (checkUserEmail == false)
                {
                    throw new Exception(clsMessages.GExist);

                }

                var storedProcedureComandText = @"INSERT INTO [uUserProfileTemp]
               ([UserProfileID]
               ,[CompanyName]
               ,[CompanyEmail]
               ,[UserEmail]
               ,[UserTitle]
               ,[UserName]
               ,[DataUsed]
               ,[EntryDate])
                    VALUES
                   ('" + objUserProfileOnline.UserProfileID + "'"
                      + ",'" + objCompanyDetailsSetup.CompanyName + "'"
                      + ",'" + objCompanyDetailsSetup.CompanyEmail + "'"
                      + ",'" + objUserProfileOnline.Email + "'"
                      + ",'" + objUserProfileOnline.Title + "'"
                      + ",'" + objUserProfileOnline.FullName + "'"
                      + ",'" + "A" + "'"
                      + "," + "CAST(GETDATE() AS DateTime)" + ""
                      + ");";
                objUserProfileOnline.ServiceRequestID = this.GetServiceRequestID();
                storedProcedureComandText += SqlSaveOnlineServiceRequest(objUserProfileOnline);
                storedProcedureComandText += SqlSaveOnlineServiceRequestDetails(objUserProfileOnline);
                storedProcedureComandText += SqlSaveOnlineServiceRequestNodes(objUserProfileOnline);
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void SaveUserAnotherServices(UserProfileOnline objUserProfileOnline)
        {
            try
            {
                var storedProcedureComandText = @"";
                objUserProfileOnline.ServiceRequestID = this.GetServiceRequestID();
                storedProcedureComandText += SqlSaveOnlineServiceRequest(objUserProfileOnline);
                storedProcedureComandText += SqlSaveOnlineServiceRequestDetails(objUserProfileOnline);
                storedProcedureComandText += SqlSaveOnlineAnotherServiceRequestNodes(objUserProfileOnline);
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private bool CountUserEmail(UserProfileOnline objUserProfileOnline)
        {
            try
            {
                bool emailCount = true;
                string sql = @"SELECT COUNT(A.UserProfileID) AS UserProfileID FROM uUserProfileTemp A WHERE A.DataUsed='A' AND A.UserEmail = '" + objUserProfileOnline.Email + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                if (objclsDataManipulation.GetSingleValue(this.ConnectionString, sql) != 0)
                {
                    emailCount = false;
                }

                return emailCount;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public string CreateUserProfileIDOnline()
        {
            clsDataManipulation objclsDataManipulation = new clsDataManipulation();
            return objclsDataManipulation.GetAnUniqueidentifierNumber(this.ConnectionString);
        }

        //public void SendSecurityCodeOnline(UserProfileOnline objUserProfileOnline, CompanyDetailsSetup objCompanyDetailsSetup)
        //{
        //    try
        //    {
        //        UserSecurityCode objUserSecurityCode = new UserSecurityCode();
        //        objUserSecurityCode.UserKnownID = objUserProfileOnline.UserProfileID;
        //        UserProfile objUserProfile = new UserProfile();
        //        objUserProfile.Email = objUserProfileOnline.Email;
        //        CompanySetup objCompanySetup = new CompanySetup();
        //        objCompanySetup.CompanyID = 1;
        //        objCompanySetup.EntryUserName = objUserProfileOnline.UserProfileID;
        //        UserSecurityCodeController objUserSecurityCodeController = new UserSecurityCodeController();
        //        objUserSecurityCodeController.SendSecurityCode(objCompanySetup, objUserSecurityCode, objUserProfile, objCompanyDetailsSetup);
        //    }
        //    catch (Exception msgException)
        //    {

        //        throw msgException;
        //    }
        //}

        private string SqlSaveOnlineServiceRequestNodes(UserProfileOnline objUserProfileOnline)
        {
            try
            {
                ServiceServe objServiceServe = new ServiceServe();
                foreach (DataRow rowNo in objUserProfileOnline.DtSelectedService.Rows)
                {
                    objServiceServe.ServiceList += rowNo["colServiceID"].ToString() + ",";
                }

                objServiceServe.ServiceList = objServiceServe.ServiceList.TrimEnd(',');
                var storedProcedureComandTextNodes = @"INSERT INTO [OnlineServiceRequestNodes] ([FeatureDetailsID],[ActivityID],[ActivityName],[SeqNo],[NodeTypeID],[PNodeTypeID],[FormDescription],[FormName]
               ,[ShowPosition],[DataUsed],[EntryDate])
		       SELECT DISTINCT " + objUserProfileOnline.ServiceRequestID + " AS FeatureDetailsID,C.ActivityID,C.ActivityName,C.SeqNo,A.NodeTypeID,C.PNodeTypeID,C.FormDescription,C.FormName,C.ShowPosition,C.DataUsed,CAST(GETDATE() AS DateTime) AS EntryDate " +
               " FROM sysServiceDetails A INNER JOIN sysServiceHeader B ON A.ServiceInformatioID = B.ServiceInformatioID " +
               " INNER JOIN sysProductOwnerNodeList C ON A.NodeTypeID = C.NodeTypeID WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed='A' " +
               " AND B.ServiceID IN (" + objServiceServe.ServiceList + ") ORDER BY A.NodeTypeID";
                return storedProcedureComandTextNodes;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private string SqlSaveOnlineAnotherServiceRequestNodes(UserProfileOnline objUserProfileOnline)
        {
            try
            {
                ServiceServe objServiceServe = new ServiceServe();
                foreach (DataRow rowNo in objUserProfileOnline.DtSelectedService.Rows)
                {
                    objServiceServe.ServiceList += rowNo["colServiceID"].ToString() + ",";
                }

                objServiceServe.ServiceList = objServiceServe.ServiceList.TrimEnd(',');
                var storedProcedureComandTextNodes = @"INSERT INTO [OnlineServiceRequestNodes] ([FeatureDetailsID],[ActivityID],[ActivityName],[SeqNo],[NodeTypeID],[PNodeTypeID],[FormDescription],[FormName]
               ,[ShowPosition],[DataUsed],[EntryDate])
		       SELECT DISTINCT " + objUserProfileOnline.ServiceRequestID + " AS FeatureDetailsID,C.ActivityID,C.ActivityName,C.SeqNo,A.NodeTypeID,C.PNodeTypeID,C.FormDescription,C.FormName,C.ShowPosition,C.DataUsed,CAST(GETDATE() AS DateTime) AS EntryDate " +
               " FROM sysServiceDetails A INNER JOIN sysServiceHeader B ON A.ServiceInformatioID = B.ServiceInformatioID " +
               " INNER JOIN sysProductOwnerNodeList C ON A.NodeTypeID = C.NodeTypeID WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed='A' " +
               " AND B.ServiceID IN (" + objServiceServe.ServiceList + ") ORDER BY A.NodeTypeID";
                return storedProcedureComandTextNodes;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private string SqlSaveOnlineServiceRequestDetails(UserProfileOnline objUserProfileOnline)
        {
            try
            {
                string storedProcedureComandTextDetails = null;
                if (objUserProfileOnline.DtSelectedService != null)
                {
                    foreach (DataRow rowNo in objUserProfileOnline.DtSelectedService.Rows)
                    {
                        ServiceManagement objServiceManagement = new ServiceManagement();
                        objServiceManagement.ServiceID = Convert.ToInt32(rowNo["colServiceID"].ToString());
                        objServiceManagement.ServiceValue = Convert.ToDouble(rowNo["colServiceValue"].ToString());
                        var storedProcedureComandTextDetailsTemp = @"INSERT INTO [OnlineServiceRequestDetails]
                       ([ServiceRequestID]
                       ,[ServiceID]
                       ,[ActualValue]
                       ,[DiscountRate]
                       ,[FeatureDetailsID]
                       ,[DataUsed]
                           )
                     VALUES
                   (" + objUserProfileOnline.ServiceRequestID + ""
                       + "," + objServiceManagement.ServiceID + ""
                       + "," + objServiceManagement.ServiceValue + ""
                       + "," + 0 + ""
                       + "," + objUserProfileOnline.ServiceRequestID + ""
                       + ",'" + "A" + "'"
                       + ");";
                        storedProcedureComandTextDetails += storedProcedureComandTextDetailsTemp;

                    }
                }

                return storedProcedureComandTextDetails;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private string SqlSaveOnlineServiceRequest(UserProfileOnline objUserProfileOnline)
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [OnlineServiceRequest]
               ([UserProfileID]
               ,[ServiceRequestID]
               ,[RequestDate]
               ,[RequestTime]
               ,[SpecialDiscountRate]
               ,[DataUsed])
                    VALUES
                   ('" + objUserProfileOnline.UserProfileID + "'"
                      + "," + objUserProfileOnline.ServiceRequestID + ""
                      + "," + "CAST(GETDATE() AS Date)" + ""
                      + "," + "CAST(GETDATE() AS Time)" + ""
                      + "," + 0 + ""
                      + ",'" + "A" + "'"
                      + ");";
                return storedProcedureComandText;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private int GetServiceRequestID()
        {
            try
            {
                int serviceRequestID = 0;
                var storedProcedureComandText = "SELECT ISNULL( MAX( ServiceRequestID),0) + 1 AS ServiceRequestID FROM OnlineServiceRequest";
                serviceRequestID = clsDataManipulation.GetMaximumValueUsingSQL(this.ConnectionString, storedProcedureComandText);
                return serviceRequestID;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetUserProfileOnline(UserList objUserList)
        {
            try
            {
                DataTable dtUserProfileOnline = new DataTable();
                string sqlString = @"SELECT A.UserProfileID,A.CompanyName,A.CompanyEmail,A.UserName,A.UserEmail,A.UserTitle,A.[MobileNumber] FROM uUserProfileTemp A WHERE A.DataUsed = 'A' AND A.UserEmail = '" + objUserList.UserEmail + "'";
                dtUserProfileOnline = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtUserProfileOnline;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void Save(UserProfile objUserProfile)
        {
            try
            {
                objUserProfile.UserIdentifierID = objUserProfile.FullName + ":" + objUserProfile.Email;
                string[] fullName = objUserProfile.FullName.Split(new Char[] { ' ' });
                objUserProfile.FirstName = fullName[0];
                if (fullName.Length >= 2)
                {
                    objUserProfile.MiddleName = fullName[1];
                }

                if (fullName.Length >= 3)
                {
                    objUserProfile.LastName = fullName[2];

                }

                objUserProfile.IsApproved = 1;
                objUserProfile.IsLockedOut = 0;

                var storedProcedureComandText = "INSERT INTO [uUserProfile] ([UserProfileID],[Password],[Email],[Title],[FirstName],[MiddleName],[LastName],[DisplayName] " +
               " ,[DataUsed],[EntryUserID],[EntryDate],[UserIdentifier],[PasswordSalt],[MobilePIN],[LoweredEmail],[DateOfBirth],[PasswordQuestion],[PasswordAnswer]" +
               " ,[CreateDate] ,[Comment],[IsApproved],[IsLockedOut],[UserProfileTypeID]) VALUES (" +
               "'" + objUserProfile.UserProfileID + "'" +
               ",'111'" +
               ",'" + objUserProfile.Email + "'" +
               ",'" + objUserProfile.Title + "'" +
               ",'" + objUserProfile.FirstName + "'" +
               ",'" + objUserProfile.MiddleName + "'" +
               ",'" + objUserProfile.LastName + "'" +
               ",'" + objUserProfile.DisplayName + "'" +
               ",'A'" +
               ",'" + objUserProfile.EntryUserName + "'" +
               ",CAST(GETDATE() AS DateTime)" +
               ",'" + objUserProfile.UserIdentifierID + "'" +
               ",'" + objUserProfile.PasswordSalt + "'" +
               ",'" + objUserProfile.MobilePIN + "'" +
               ",'" + objUserProfile.LoweredEmail + "'" +
               ",'" + objUserProfile.DateOfBirth + "'" +
               ",'" + objUserProfile.PasswordQuestion + "'" +
               ",'" + objUserProfile.PasswordAnswer + "'" +
               ",'" + objUserProfile.CreateDate + "'" +
               ",'" + objUserProfile.Comment + "'" +
               "," + objUserProfile.IsApproved + "" +
               "," + objUserProfile.IsLockedOut + "" +
               ",'" + objUserProfile.UserProfileTypeID + "'" +
               " );";

                storedProcedureComandText += @"INSERT INTO [uUserList]([UserID],[UserTypeID],[UserProfileID],[UserName],[IsAnonymous],[LastActivityDate]) VALUES (NEWID()
               ,1 " +
               ",'" + objUserProfile.UserProfileID + "'" +
               ",'" + objUserProfile.UserIdentifierID + "'" +
               ",NULL,NULL);";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}