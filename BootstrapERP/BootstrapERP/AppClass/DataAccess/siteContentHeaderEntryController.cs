using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;

namespace BootstrapERP.AppClass.DataAccess
{
    public class siteContentHeaderEntryController : DataAccessBase
    {
        internal void Save(SiteContentHeader objSiteContentHeader, siteApplicationSetup objsiteApplicationSetup, siteContentType objsiteContentType, siteContentCategory objsiteContentCategory, siteContentRelatedTo objsiteContentRelatedTo)
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [siteContentHeader]
               ([ContentID]
               ,[CompanyID]
               ,[BranchID]
               ,[ApplicationID]
               ,[ContentTypeID]
               ,[ContentCategoryID]
               ,[ContentRelatedToID]
               ,[ContentImageURL]  
               ,[ContentRemarks]
               ,[PreparationDate]
               ,[PublishedDate]
               ,[ExpiryDate]
               ,[DataUsed]
               ,[EntryDate]
               ,[EntryUserID])
                VALUES ( " + objSiteContentHeader.ContentID + "" +
                "," + objSiteContentHeader.CompanyID + "" +
                "," + objSiteContentHeader.BranchID + "" +
                "," + objsiteApplicationSetup.ApplicationID + "" +
                "," + objsiteContentType.ContentTypeID + "" +
                "," + objsiteContentCategory.ContentCategoryID + "" +
                "," + objsiteContentRelatedTo.ContentRelatedToID + "" +
                ",'" + objSiteContentHeader.ContentImageURL + "'" +
                ",'" + objSiteContentHeader.ContentRemarks + "'" +
                ",'" + objSiteContentHeader.PreparationDate + "'" +
                ",'" + objSiteContentHeader.PublishedDate + "'" +
                ",'" + objSiteContentHeader.ExpiryDate + "'" +
                ",'" + "A" + "'" +
                "," + "CAST(GETDATE() AS DateTime)" + "" +
                ",'" + objSiteContentHeader.EntryUserName + "'" +
                ");";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

                var storedProcedureComandTextDetail = @"
                INSERT INTO [siteContentDetail]
			    ([ContentID],[ContentDetailID],[ContentDetailTitle],[ContentDetailImage],[ContentDetailDescription]
               ,[DataUsed],[EntryDate],[EntryUserID],[ContentDetailImageURL],[ContentDetailSubTitle],[ContentIntroductoryText]) " +
               " SELECT " + objSiteContentHeader.ContentID + " AS ContentIDHeader, t1.contentID,t1.contentTitle,t1.contentImage,t1.contentDescription,t1.DataUsed,CAST(GETDATE() AS DateTime),t1.EntryUserID,t1.contentImageURL,t1.[contentSubTitle],t1.[introductoryText] " +
               "  FROM siteContentEntryTemp t1 " +
               "  WHERE t1.DataUsed = 'A' AND t1.EntryUserID = '" + objSiteContentHeader.EntryUserName + "';" +
               "  delete from siteContentEntryTemp WHERE [EntryUserID] = '" + objSiteContentHeader.EntryUserName + "';";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
        internal void SaveHeader(SiteContentHeader objSiteContentHeader, siteApplicationSetup objsiteApplicationSetup, siteContentType objsiteContentType, siteContentCategory objsiteContentCategory, siteContentRelatedTo objsiteContentRelatedTo)
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [siteContentHeader]
               ([ContentID]
               ,[CompanyID]
               ,[BranchID]
               ,[ApplicationID]
               ,[ContentTypeID]
               ,[ContentCategoryID]
               ,[ContentRelatedToID]
               ,[ContentImageURL] 
               ,[ContentRemarks]
               ,[PreparationDate]
               ,[PublishedDate]
               ,[ExpiryDate]
               ,[DataUsed]
               ,[EntryDate]
               ,[EntryUserID])
                VALUES ( " + objSiteContentHeader.ContentID + "" +
                "," + objSiteContentHeader.CompanyID + "" +
                "," + objSiteContentHeader.BranchID + "" +
                "," + objsiteApplicationSetup.ApplicationID + "" +
                "," + objsiteContentType.ContentTypeID + "" +
                "," + objsiteContentCategory.ContentCategoryID + "" +
                "," + objsiteContentRelatedTo.ContentRelatedToID + "" +
                ",'" + objSiteContentHeader.ContentImageURL + "'" +
                ",'" + objSiteContentHeader.ContentRemarks + "'" +
                ",'" + objSiteContentHeader.PreparationDate + "'" +
                ",'" + objSiteContentHeader.PublishedDate + "'" +
                ",'" + objSiteContentHeader.ExpiryDate + "'" +
                ",'" + "A" + "'" +
                "," + "CAST(GETDATE() AS DateTime)" + "" +
                ",'" + objSiteContentHeader.EntryUserName + "'" +
                ");";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);


            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        internal bool Checkcontent(BlogContentDetail objBlogContentDetail)
        {
            try
            {
                var storedProcedureComandTextDetail = @"SELECT DISTINCT ContentParentID
                FROM blogContentHeader A WHERE A.DataUsed='A' AND A.CompanyID= " + objBlogContentDetail.CompanyID + ""
                + " AND A.BranchID = " + objBlogContentDetail.BranchID + ""
                + " AND A.ApplicationID= " + objBlogContentDetail.ApplicationID + ""
                + " AND A.ContentCategoryID=" + objBlogContentDetail.ContentCategoryID + ""
                + " AND A.ContentRelatedToID=" + objBlogContentDetail.ContentRelatedToID + ""
                + " AND A.EntryUserID='" + objBlogContentDetail.EntryUserName + "';";
                return clsDataManipulation.DuplicateDataCheckfunction(this.ConnectionString, storedProcedureComandTextDetail);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal int CheckcontentID(BlogContentDetail objBlogContentDetail)
        {
            try
            {
                var storedProcedureComandTextDetail = @"SELECT DISTINCT ContentParentID
                FROM blogContentHeader A WHERE A.DataUsed='A' AND A.CompanyID= " + objBlogContentDetail.CompanyID + ""
                + " AND A.BranchID = " + objBlogContentDetail.BranchID + ""
                + " AND A.ApplicationID= " + objBlogContentDetail.ApplicationID + ""
                + " AND A.ContentCategoryID=" + objBlogContentDetail.ContentCategoryID + ""
                + " AND A.ContentRelatedToID=" + objBlogContentDetail.ContentRelatedToID + ""
                + " AND A.EntryUserID='" + objBlogContentDetail.EntryUserName + "';";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                int contentIDTemp = objclsDataManipulation.GetSingleValue(this.ConnectionString, storedProcedureComandTextDetail);
                return contentIDTemp;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void SaveHeader(BlogContentDetail objBlogContentDetail)
        {
            try
            {
                var storedProcedureComandTextDetail = @"INSERT INTO [blogContentHeader] ([CompanyID],[BranchID]
           ,[ApplicationID],[ContentParentID],[ContentCategoryID],[ContentRelatedToID],[DataUsed],[EntryDate]
            ,[EntryUserID]) VALUES
           (" + objBlogContentDetail.CompanyID + ""
           + "," + objBlogContentDetail.BranchID + ""
           + "," + objBlogContentDetail.ApplicationID + ""
           + ",'" + objBlogContentDetail.ContentParentID + "'"
           + "," + objBlogContentDetail.ContentCategoryID + ""
           + "," + objBlogContentDetail.ContentRelatedToID + ""
           + ",'" + "A" + "'"
           + "," + "CAST(GETDATE() AS DateTime)" + ""
           + ",'" + objBlogContentDetail.EntryUserName + "'"
           + ");";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void SaveDetail(BlogContentDetail objBlogContentDetail, SiteContentDetails objSiteContentDetails)
        {
            try
            {
                var storedProcedureComandTextDetail = @"INSERT INTO [blogContentDetail]([ContentParentID],[ContentID]
           ,[SequenceNo],[ContentDetailTitle],[ContentDetailSubTitle],[ContentDetailImageURL],[ContentIntroductoryText]
           ,[ContentDetailDescription],[AuthorsName],[DataUsed],[EntryDate],[EntryUserID]) VALUES
           ('" + objBlogContentDetail.ContentParentID + "'"
           + ",'" + objBlogContentDetail.ContentID + "'"
           + "," + objBlogContentDetail.SequenceNo + ""
           + ",'" + objBlogContentDetail.ContentDetailTitle + "'"
           + ",'" + objBlogContentDetail.ContentDetailSubTitle + "'"
           + ",'" + objBlogContentDetail.ContentDetailImageURL + "'"
           + ",'" + objBlogContentDetail.ContentIntroductoryText + "'"
           + ",'" + objBlogContentDetail.ContentDetailDescription + "'"
           + ",'" + objBlogContentDetail.AuthorsName + "'"
           + ",'" + objSiteContentDetails.ActionTypeID + "'"
           + "," + "CAST(GETDATE() AS DateTime)" + ""
           + ",'" + objBlogContentDetail.EntryUserName + "'"
           + ");";

                if (objSiteContentDetails.ActionTypeID == "S")
                {
                    storedProcedureComandTextDetail += @"INSERT INTO [blogContentPublishMethod] ([ContentLogID],[ContentID]
           ,[SecquenceNo],[ActionID],[ActionDate]
           ,[EntryDate],[EntryUserID])VALUES (" +
               "'" + objSiteContentDetails.ContentLogID + "'"
               + ",'" + objBlogContentDetail.ContentID + "'"
               + "," + objSiteContentDetails.SecquenceNo + ""
               + ",'" + objSiteContentDetails.ActionTypeID + "'"
               + "," + "CAST(GETDATE() AS DateTime)" + ""
               + "," + "CAST(GETDATE() AS DateTime)" + ""
               + ",'" + objBlogContentDetail.EntryUserName + "'"
               + ");";

                }

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void UpdateDetail(BlogContentDetail objBlogContentDetail, SiteContentDetails objSiteContentDetails)
        {
            try
            {
                var storedProcedureComandTextDetail = @"UPDATE [blogContentDetail] 
                SET [ContentDetailTitle] = '" + objBlogContentDetail.ContentDetailTitle + "'"
                + " ,[ContentDetailSubTitle] = '" + objBlogContentDetail.ContentDetailSubTitle + "'"
                + " ,[ContentDetailImageURL] = '" + objBlogContentDetail.ContentDetailImageURL + "'"
                + " ,[ContentIntroductoryText] = '" + objBlogContentDetail.ContentIntroductoryText + "'"
                + " ,[ContentDetailDescription] = '" + objBlogContentDetail.ContentDetailDescription + "'"
                + " ,[AuthorsName] = '" + objBlogContentDetail.AuthorsName + "'"
                + " ,[DataUsed] = '" + objSiteContentDetails.ActionTypeID + "'"
                + " ,[LastUpdateDate] = CAST(GETDATE() AS DateTime)"
                + " ,[LastUpdateUserID] = '" + objSiteContentDetails.EntryUserName + "'"
             + " WHERE [ContentID] = " + objBlogContentDetail.ContentID + ";";

                if (objSiteContentDetails.ActionTypeID == "S")
                {
                    storedProcedureComandTextDetail += @"INSERT INTO [blogContentPublishMethod] ([ContentLogID],[ContentID]
           ,[SecquenceNo],[ActionID],[ActionDate]
           ,[EntryDate],[EntryUserID])VALUES (" +
               "'" + objSiteContentDetails.ContentLogID + "'"
               + ",'" + objBlogContentDetail.ContentID + "'"
               + "," + objSiteContentDetails.SecquenceNo + ""
               + ",'" + objSiteContentDetails.ActionTypeID + "'"
               + "," + "CAST(GETDATE() AS DateTime)" + ""
               + "," + "CAST(GETDATE() AS DateTime)" + ""
               + ",'" + objSiteContentDetails.EntryUserName + "'"
               + ");";

                }

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void SaveDetail(SiteContentHeader objSiteContentHeader, siteApplicationSetup objsiteApplicationSetup, siteContentType objsiteContentType, siteContentCategory objsiteContentCategory, siteContentRelatedTo objsiteContentRelatedTo)
        {
            try
            {
                var storedProcedureComandTextDetail = @"
                INSERT INTO [siteContentDetail]
			    ([ContentID],[ContentDetailID],[ContentDetailTitle],[ContentDetailImage],[ContentDetailDescription]
               ,[DataUsed],[EntryDate],[EntryUserID],[ContentDetailImageURL],[ContentDetailSubTitle],[ContentIntroductoryText]) " +
               " SELECT " + objSiteContentHeader.ContentID + " AS ContentIDHeader, t1.contentID,t1.contentTitle,t1.contentImage,t1.contentDescription,t1.DataUsed,CAST(GETDATE() AS DateTime),t1.EntryUserID,t1.contentImageURL,t1.[contentSubTitle],t1.[introductoryText] " +
               "  FROM siteContentEntryTemp t1 " +
               "  WHERE t1.DataUsed = 'A' AND t1.EntryUserID = '" + objSiteContentHeader.EntryUserName + "';" +
               "  delete from siteContentEntryTemp WHERE [EntryUserID] = '" + objSiteContentHeader.EntryUserName + "';";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
        internal void SaveDetail(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                var storedProcedureComandTextDetail = @"
                INSERT INTO [siteContentDetail]
			    ([ContentID],[ContentDetailID],[ContentDetailTitle],[ContentDetailImage],[ContentDetailDescription]
               ,[DataUsed],[EntryDate],[EntryUserID],[ContentDetailImageURL],[ContentDetailSubTitle],[ContentIntroductoryText]) " +
               " SELECT " + objSiteContentHeader.ContentID + " AS ContentIDHeader, t1.contentID,t1.contentTitle,t1.contentImage,t1.contentDescription,t1.DataUsed,CAST(GETDATE() AS DateTime),t1.EntryUserID,t1.contentImageURL,t1.[contentSubTitle],t1.[introductoryText] " +
               "  FROM siteContentEntryTemp t1 " +
               "  WHERE t1.DataUsed = 'A' AND t1.EntryUserID = '" + objSiteContentHeader.EntryUserName + "';" +
               "  delete from siteContentEntryTemp WHERE [EntryUserID] = '" + objSiteContentHeader.EntryUserName + "';";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
        internal void DeleteContentHeaderEntry(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                var storedProcedureComandTextDetail = @"delete from siteContentHeader WHERE [ContentID] = " + objSiteContentHeader.ContentID + ";" +
                    " delete from siteContentDetail WHERE [ContentID] = " + objSiteContentHeader.ContentID + ";";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal bool Checkcontent(SiteContentHeader objSiteContentHeader, siteApplicationSetup objsiteApplicationSetup, siteContentType objsiteContentType, siteContentCategory objsiteContentCategory, siteContentRelatedTo objsiteContentRelatedTo)
        {
            try
            {
                var storedProcedureComandTextDetail = @"SELECT DISTINCT [ContentID]
                FROM [siteContentHeader] A WHERE A.CompanyID= " + objSiteContentHeader.CompanyID + " " +
                " AND A.ApplicationID=" + objsiteApplicationSetup.ApplicationID + " AND " +
                " A.ContentTypeID=" + objsiteContentType.ContentTypeID + " AND" +
                " A.ContentCategoryID=" + objsiteContentCategory.ContentCategoryID + "";
                return clsDataManipulation.DuplicateDataCheckfunction(this.ConnectionString, storedProcedureComandTextDetail);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal int CheckcontentID(SiteContentHeader objSiteContentHeader, siteApplicationSetup objsiteApplicationSetup, siteContentType objsiteContentType, siteContentCategory objsiteContentCategory, siteContentRelatedTo objsiteContentRelatedTo)
        {
            try
            {
                var storedProcedureComandTextDetail = @"SELECT DISTINCT [ContentID]
                FROM [siteContentHeader] A WHERE A.CompanyID= " + objSiteContentHeader.CompanyID + " " +
                " AND A.ApplicationID=" + objsiteApplicationSetup.ApplicationID + " AND " +
                " A.ContentTypeID=" + objsiteContentType.ContentTypeID + " AND" +
                " A.ContentCategoryID=" + objsiteContentCategory.ContentCategoryID + "";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                int contentIDTemp = objclsDataManipulation.GetSingleValue(this.ConnectionString, storedProcedureComandTextDetail);
                return contentIDTemp;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void SaveTempData(SiteContentHeader objSiteContentHeader, siteApplicationSetup objsiteApplicationSetup, siteContentType objsiteContentType, siteContentCategory objsiteContentCategory, siteContentRelatedTo objsiteContentRelatedTo)
        {
            try
            {
                var storedProcedureComandTextDetail = @"delete from siteContentHeaderTemp WHERE [EntryUserID] = '" + objSiteContentHeader.EntryUserName + "';" +
                 @" INSERT INTO [siteContentHeaderTemp]
               ( [ApplicationID]
               ,[ContentTypeID]
               ,[ContentCategoryID]
               ,[ContentRelatedToID]
                ,[EntryUserID])
                VALUES(" + objsiteApplicationSetup.ApplicationID + "" +
                "," + objsiteContentType.ContentTypeID + "" +
                "," + objsiteContentCategory.ContentCategoryID + "" +
                "," + objsiteContentRelatedTo.ContentRelatedToID + "" +
                ",'" + objSiteContentHeader.EntryUserName + "'" +
                ");";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        internal DataTable GetContentHeaderTempData(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                DataTable dtCntentHeaderValue = null;
                var storedProcedureComandText = @"SELECT distinct A.[ApplicationID]
                  ,A.[ContentTypeID]
                  ,A.[ContentCategoryID]
                  ,A.[ContentRelatedToID]
	              FROM [siteContentHeaderTemp] A WHERE  A.EntryUserID='" + objSiteContentHeader.EntryUserName + "'";
                dtCntentHeaderValue = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentHeaderValue;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}