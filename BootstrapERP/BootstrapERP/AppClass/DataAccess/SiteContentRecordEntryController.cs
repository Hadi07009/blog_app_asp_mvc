using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;

namespace BootstrapERP.AppClass.DataAccess
{
    public class SiteContentRecordEntryController : DataAccessBase
    {
        internal void DeleteRecord(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                var storedProcedureComandText = @"delete from [siteContentHeader] WHERE ContentID = " + objSiteContentHeader.ContentID + ";" +
                " delete from [siteContentDetail] WHERE ContentID = " + objSiteContentHeader.ContentID + ";";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void DeleteBlog(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                var storedProcedureComandText = @"delete from [siteContentHeader] WHERE ContentID = " + objSiteContentHeader.ContentID + ";" +
                " delete from [siteContentDetail] WHERE ContentID = " + objSiteContentHeader.ContentID + ";";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void ImportContentDetailsTemp(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                var storedProcedureComandTextDetail = @"" +
                    "INSERT INTO [siteContentEntryTemp]([contentTitle],[contentDescription],[DataUsed],[EntryUserID],[contentParentID],[contentImageURL],[contentSubTitle],[introductoryText])" +
                 " SELECT t1.ContentDetailTitle,t1.ContentDetailDescription,t1.DataUsed,t1.EntryUserID,t1.ContentID,t1.ContentDetailImageURL,t1.[ContentDetailSubTitle],t1.[ContentIntroductoryText]" +
                 " FROM siteContentDetail t1" +
                 " WHERE NOT EXISTS(SELECT contentParentID" +
                 " FROM[siteContentEntryTemp] t2" +
                 " WHERE t2.contentParentID = t1.ContentID) AND t1.DataUsed = 'A' AND t1.EntryUserID = '" + objSiteContentHeader.EntryUserName + "'" +
                 " AND t1.ContentID = " + objSiteContentHeader.ContentID + ";";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetContentHeader(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                DataTable dtCntentHeaderValue = null;
                var storedProcedureComandText = @"SELECT A.[ApplicationID]
                  ,A.[ContentTypeID]
                  ,A.[ContentCategoryID]
                  ,A.[ContentRelatedToID]
                  ,A.[ContentImageURL]  
	              FROM [siteContentHeader] A WHERE A.[DataUsed] = 'A' AND A.ContentID=" + objSiteContentHeader.ContentID + "";
                dtCntentHeaderValue = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentHeaderValue;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContent(BlogContentDetail objBlogContentDetail)
        {
            try
            {
                DataTable dtCntentHeaderValue = null;
                var storedProcedureComandText = @"SELECT DISTINCT A.ContentDetailTitle,A.ContentDetailSubTitle,A.ContentDetailImageURL,A.ContentIntroductoryText
                ,A.ContentDetailDescription,A.AuthorsName,B.ContentCategoryID,B.ContentRelatedToID
                 FROM blogContentDetail A
                 INNER JOIN blogContentHeader B
                 ON A.ContentParentID = B.ContentParentID
                WHERE A.ContentID = '"+ objBlogContentDetail.ContentID + "';";
                dtCntentHeaderValue = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentHeaderValue;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void DeleteBlog(BlogContentDetail objBlogContentDetail)
        {
            try
            {
                var storedProcedureComandText = @"UPDATE [blogContentDetail]
               SET [DataUsed] = 'I'
	            ,[LastUpdateDate] = CAST(GETDATE() AS DateTime)
	            ,[LastUpdateUserID] = '" + objBlogContentDetail.EntryUserName + "'"
             + " WHERE [ContentID] = '" + objBlogContentDetail.ContentID + "';";
                
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}