using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;

namespace BootstrapERP.AppClass.DataAccess
{
    public class VMessageAccessController : DataAccessBase
    {
        internal void Save(VisitorsMessage objVisitorsMessage)
        {
            try
            {
                string sql = @"SELECT DISTINCT [VisitorsID]
                FROM [VisitorsMessageHeader] WHERE [CompanyID]="+ objVisitorsMessage .CompanyID+ "" +
                " AND [BranchID]="+ objVisitorsMessage .BranchID+ " AND [ApplicationID]="+ objVisitorsMessage .ApplicationID+ " " +
                " AND [EmailAddress] = '"+ objVisitorsMessage .EmailAddress+ "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                objVisitorsMessage.VisitorsID = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);

                objVisitorsMessage.MessageID = objclsDataManipulation.GetAnUniqueidentifierNumber(this.ConnectionString);

                var storedProcedureComandText = @"";
                if (objVisitorsMessage.VisitorsID == "" || objVisitorsMessage.VisitorsID == null)
                {
                    objVisitorsMessage.VisitorsID = objVisitorsMessage.MessageID;
                    storedProcedureComandText += @"INSERT INTO [VisitorsMessageHeader] ([CompanyID],[BranchID],[ApplicationID],[VisitorsID],[Name],[EmailAddress],[PhoneNumber]) VALUES (" +
               "" + objVisitorsMessage.CompanyID + "" +
               "," + objVisitorsMessage.BranchID + "" +
               "," + objVisitorsMessage.ApplicationID + "" +
               ",'" + objVisitorsMessage.VisitorsID + "'" +
               ",'" + objVisitorsMessage.Name + "'" +
               ",'" + objVisitorsMessage.EmailAddress + "'" +
               ",'" + objVisitorsMessage.PhoneNumber + "'" +
               " );";

                }

                storedProcedureComandText += @" INSERT INTO [VisitorsMessageDetails] ([VisitorsID],[MessageID],[MessageText]
                ,[DataUsed],[EntryDate],[MessageSubject],[MessageTypeID],[MessageCategoryID],[MessageSubCategoryID],[CCEmail]
                ,[MessageChannel])
                VALUES (" +
               "'" + objVisitorsMessage.VisitorsID + "'" +
               ",'" + objVisitorsMessage.MessageID + "'" +
               ",'" + objVisitorsMessage.MessageText + "'" +
               ",'" + "A" + "'" +
               "," + "CAST(GETDATE() AS DateTime)" + "" +
               ",'" + objVisitorsMessage.MessageSubject + "'" +
               ",'" + objVisitorsMessage.MessageTypeID + "'" +
               "," + objVisitorsMessage.MessageCategoryID + "" +
               "," + objVisitorsMessage.MessageSubCategoryID + "" +
               ",'" + objVisitorsMessage.CcEmailAddress + "'" +
               ",'" + objVisitorsMessage.MessageChannel + "'" +
               " );";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
                this.SendEmailToCompany(objVisitorsMessage);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void ReplyMessage(VisitorsMessage objVisitorsMessage)
        {
            try
            {
                var storedProcedureComandText = @"UPDATE [VisitorsMessageDetails]
               SET
              [ReplyMessageText] = ISNULL('" + objVisitorsMessage.ReplyMessageText + "',[ReplyMessageText]) " +
              " ,[DataUsed] = ISNULL('I',[ReplyMessageText]) " +
              " ,[LastUpdateDate] = ISNULL(" + "CAST(GETDATE() AS DateTime)" + ",[LastUpdateDate]) " +
              " ,[LastUpdateUserID] = ISNULL('" + objVisitorsMessage.EntryUserName + "',[LastUpdateUserID]) " +
              " WHERE [VisitorsID] = '"+ objVisitorsMessage .VisitorsID+ "' AND [MessageID] = '"+ objVisitorsMessage .MessageID+ "';";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
                this.SendEmailToVisitor(objVisitorsMessage);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable SearchVisitorsMessage(VisitorsMessage objVisitorsMessage)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"SELECT DISTINCT 
                  A.[VisitorsID]
                  ,A.[Name]
                  ,A.[EmailAddress]
                  ,A.[PhoneNumber]
	              ,B.MessageID
	              ,B.MessageText
	              ,B.EntryDate
                  ,B.[MessageSubject]
                  ,B.[MessageTypeID]
                  ,B.[MessageCategoryID]
                  ,B.[MessageSubCategoryID]
                  ,B.[CCEmail]
                  ,B.[MessageChannel]  
              FROM [VisitorsMessageHeader] A 
              INNER JOIN [VisitorsMessageDetails] B ON A.VisitorsID = B.VisitorsID
              WHERE  B.[MessageChannel] = '" + objVisitorsMessage.MessageChannel + "' AND A.CompanyID = " + objVisitorsMessage.CompanyID + "" +
              " AND A.BranchID = " + objVisitorsMessage.BranchID + " AND A.ApplicationID = " + objVisitorsMessage.ApplicationID + " " +
              "";

                if (objVisitorsMessage.MessageTypeID != "null")
                {
                    storedProcedureComandText += " AND B.[MessageTypeID] = '" + objVisitorsMessage.MessageTypeID + "'";
                }

                storedProcedureComandText += " AND B.[MessageCategoryID] = " + objVisitorsMessage.MessageCategoryID + "";
                storedProcedureComandText += " AND B.[MessageSubCategoryID] = " + objVisitorsMessage.MessageSubCategoryID + "";

                if (objVisitorsMessage.EmailActionType != "null")
                {
                    storedProcedureComandText += " AND B.DataUsed = '" + objVisitorsMessage.EmailActionType + "'";
                }

                if (objVisitorsMessage.FromDate != null && objVisitorsMessage.ToDate != null)
                {
                    storedProcedureComandText += " AND B.EntryDate BETWEEN '" + objVisitorsMessage.FromDate + "' AND '" + objVisitorsMessage.ToDate + "'";
                }


                storedProcedureComandText += " ORDER BY B.EntryDate DESC";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public void SendEmailToVisitor(VisitorsMessage objVisitorsMessage)
        {
            try
            {
                MailServiceSetup objMailServiceSetup = new MailServiceSetup();
                objMailServiceSetup.MailBody = objVisitorsMessage.ReplyMessageText;
                objMailServiceSetup.EmailTo = objVisitorsMessage.EmailAddress;
                objMailServiceSetup.MailtypeID = "3";
                objMailServiceSetup.MailSubject = objVisitorsMessage.MessageSubject;
                objMailServiceSetup.EmailCC = objVisitorsMessage.CcEmailAddress;
                ArrayList attachDocument = new ArrayList();
                objMailServiceSetup.AttachItem = attachDocument;
                MailServiceController objMailServiceController = new MailServiceController();
                objMailServiceController.eMailSendServiceHTML(objVisitorsMessage.CompanyID, objMailServiceSetup);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public void SendEmailToCompany(VisitorsMessage objVisitorsMessage)
        {
            try
            {
                MailServiceSetup objMailServiceSetup = new MailServiceSetup();
                objMailServiceSetup.MailBody = objVisitorsMessage.ReplyMessageText;
                objMailServiceSetup.EmailFrom = objVisitorsMessage.EmailAddress;
                objMailServiceSetup.MailtypeID = "3";
                objMailServiceSetup.MailSubject = objVisitorsMessage.MessageSubject;
                objMailServiceSetup.EmailCC = objVisitorsMessage.CcEmailAddress;
                ArrayList attachDocument = new ArrayList();
                objMailServiceSetup.AttachItem = attachDocument;
                MailServiceController objMailServiceController = new MailServiceController();
                objMailServiceController.eMailHTMLToCompany(objVisitorsMessage.CompanyID, objMailServiceSetup);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetVisitorsMessage(VisitorsMessage visitorsMessage)
        {
            try
            {
                DataTable dtUser = null;
                string sqlString = @"SELECT DISTINCT 
                  A.[VisitorsID]
                  ,A.[Name]
                  ,A.[EmailAddress]
                  ,A.[PhoneNumber]
	              ,B.MessageID
	              ,B.MessageText
	              ,B.EntryDate
                  ,B.[MessageSubject]
                  ,B.[MessageTypeID]
                  ,B.[MessageCategoryID]
                  ,B.[MessageSubCategoryID]
                  ,B.[CCEmail]
                  ,B.[MessageChannel]  
              FROM [VisitorsMessageHeader] A 
              INNER JOIN [VisitorsMessageDetails] B ON A.VisitorsID = B.VisitorsID
              WHERE B.DataUsed = 'A' AND B.[MessageChannel] = '"+ visitorsMessage .MessageChannel+ "' AND A.CompanyID = " + visitorsMessage .CompanyID+ "" +
              " AND A.BranchID = "+ visitorsMessage .BranchID+ " AND A.ApplicationID = "+ visitorsMessage .ApplicationID+ " "+
              " ORDER BY B.EntryDate"+
                "; ";
                dtUser = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtUser;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}