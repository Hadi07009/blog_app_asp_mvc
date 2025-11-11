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
    public class SiteContentAccessController : DataAccessBase
    {
        internal DataTable GetSiteContent(siteApplicationSetup objsiteApplicationSetup,
            siteContentType objsiteContentType, siteContentCategory objsiteContentCategory,
            siteContentRelatedTo objsiteContentRelatedTo)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"
                select B.ContentDetailDescription,C.ContentTypeDescription,D.ContentTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentDetailID,B.ContentIntroductoryText,B.EntryDate from [siteContentHeader] A
                INNER JOIN siteContentDetail B ON A.ContentID=B.ContentID
                INNER JOIN siteContentType C ON A.ContentTypeID = C.ContentTypeID
                INNER JOIN siteContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                WHERE A.DataUsed='A' AND B.DataUsed='A' AND A.CompanyID=1 AND A.BranchID=1
                AND A.ApplicationID=" + objsiteApplicationSetup.ApplicationID + " AND " +
                " A.ContentTypeID=" + objsiteContentType.ContentTypeID + " AND" +
                " A.ContentCategoryID=" + objsiteContentCategory.ContentCategoryID + " AND" +
                " A.ContentRelatedToID=" + objsiteContentRelatedTo.ContentRelatedToID + "";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetSiteContent(siteApplicationSetup objsiteApplicationSetup,
            siteContentType objsiteContentType, siteContentCategory objsiteContentCategory)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objsiteContentCategory.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"
                     select B.ContentDetailDescription,C.ContentTypeDescription,D.ContentTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentDetailID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from [siteContentHeader] A
                     INNER JOIN siteContentDetail B ON A.ContentID=B.ContentID
                     INNER JOIN siteContentType C ON A.ContentTypeID = C.ContentTypeID
                     INNER JOIN siteContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='A' AND A.CompanyID=1 AND A.BranchID=1
                     AND A.ApplicationID=" + objsiteApplicationSetup.ApplicationID + " AND " +
                     " A.ContentTypeID=" + objsiteContentType.ContentTypeID + " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"
                      select B.ContentDetailDescription,C.ContentTypeDescription,D.ContentTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentDetailID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from [siteContentHeader] A
                      INNER JOIN siteContentDetail B ON A.ContentID=B.ContentID
                      INNER JOIN siteContentType C ON A.ContentTypeID = C.ContentTypeID
                      INNER JOIN siteContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                      INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID 
                      WHERE A.DataUsed='A' AND B.DataUsed='A' AND A.CompanyID=1 AND A.BranchID=1
                      AND A.ApplicationID=" + objsiteApplicationSetup.ApplicationID + " AND " +
                     " A.ContentTypeID=" + objsiteContentType.ContentTypeID + " AND" +
                     " A.ContentCategoryID=" + objsiteContentCategory.ContentCategoryID + " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal string GetSiteBanner(siteApplicationSetup objsiteApplicationSetup)
        {
            try
            {
                string siteBannerURL = null;
                string sql = @"SELECT DISTINCT A.BannerImageURL FROM blogBanner A
                WHERE A.DataUsed = 'A' AND A.CompanyID="+ objsiteApplicationSetup .CompanyID+ "" +
                " AND A.BranchID="+ objsiteApplicationSetup .BranchID+ " AND " +
                " A.ApplicationID="+ objsiteApplicationSetup .ApplicationID+ ";";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                siteBannerURL = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return siteBannerURL;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContentUser(siteApplicationSetup objsiteApplicationSetup, string contentCategory, string contentRelatedTo, SiteContentDetails objSiteContentDetails)
        {
            try
            {
                //new
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"select DISTINCT B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle
                 ,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                  INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                 INNER JOIN blogContentPublishMethod F ON B.ContentID = F.ContentID
                  INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                  INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                  INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                  WHERE A.DataUsed='A' AND B.DataUsed='P' AND F.ActionID='P' 
                  AND A.CompanyID= 1 AND A.BranchID=1 AND A.ApplicationID= 2 ";

                if (contentCategory != "null")
                {
                    int contentCategoryTemp = Convert.ToInt32(contentCategory);
                    storedProcedureComandText += " AND A.ContentCategoryID = " + contentCategoryTemp + "";
                }

                if (contentRelatedTo != "null")
                {
                    int contentRelatedToTemp = Convert.ToInt32(contentRelatedTo);
                    storedProcedureComandText += " AND A.ContentRelatedToID = " + contentRelatedToTemp + "";
                }


                if (objSiteContentDetails.FromDate != null && objSiteContentDetails.FromDate != null)
                {
                    storedProcedureComandText += " AND B.LastUpdateDate BETWEEN '" + objSiteContentDetails.FromDate + "' AND '" + objSiteContentDetails.ToDate + "'";
                }

                if (objSiteContentDetails.AuthorsName != null)
                {
                    storedProcedureComandText += " AND B.AuthorsName='" + objSiteContentDetails.AuthorsName + "'";
                }

                if (objSiteContentDetails.ContentTitle != null)
                {
                    storedProcedureComandText += " AND B.ContentDetailTitle='" + objSiteContentDetails.ContentTitle + "'";
                }

                storedProcedureComandText += " ORDER BY C.ContentCategoryTitle,D.ContentRelatedToTitle, B.EntryDate DESC";


                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContentWhatsHot(siteApplicationSetup objsiteApplicationSetup, siteContentCategory objsiteContentCategory)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objsiteContentCategory.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"
                    select DISTINCT TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"
                    select DISTINCT TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
					 INNER JOIN blogContentPublishMethod F ON B.ContentID = F.ContentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND F.ActionID='P' AND F.QualityTag = 'N' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " AND A.ContentCategoryID != " + objsiteContentCategory.ContentCategoryID + "" +
                     " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContentTopStories(siteApplicationSetup objsiteApplicationSetup, siteContentCategory objsiteContentCategory)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objsiteContentCategory.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"
                    select TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"
                    select TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " AND A.ContentCategoryID != " + objsiteContentCategory.ContentCategoryID + "" +
                     " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContentPremium(siteApplicationSetup objsiteApplicationSetup, siteContentCategory objsiteContentCategory)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objsiteContentCategory.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"
                    select DISTINCT TOP 7 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='M' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"
                    select DISTINCT TOP 7 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='M' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContentUser(siteApplicationSetup objsiteApplicationSetup, string contentCategory, string contentRelatedTo, string blogActionType, SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"select DISTINCT C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailTitle,F.ActionTypeTitle,B.SequenceNo,B.ContentDetailDescription,B.ContentDetailImageURL,A.ContentImageURL,
                        B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,G.Remarks  
                        from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
					 INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID  
					 INNER JOIN blogActionType F ON B.DataUsed = F.ActionTypeID  
                     LEFT JOIN blogContentPublishMethod G ON B.ContentID=G.ContentID AND B.DataUsed=G.ActionID
                     WHERE
                     A.CompanyID= " + objsiteApplicationSetup.CompanyID + "" +
                     " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " AND B.EntryUserID = '" + objsiteApplicationSetup.EntryUserID + "' ";
                     
                if (contentCategory != "null")
                {
                    int contentCategoryTemp = Convert.ToInt32(contentCategory);
                    storedProcedureComandText += " AND A.ContentCategoryID = " + contentCategoryTemp + "";
                }

                if (contentRelatedTo != "null")
                {
                    int contentRelatedToTemp = Convert.ToInt32(contentRelatedTo);
                    storedProcedureComandText += " AND A.ContentRelatedToID = " + contentRelatedToTemp + "";
                }

                if (blogActionType != "null")
                {
                    storedProcedureComandText += " AND B.DataUsed = '" + blogActionType + "'";
                }

                if (objSiteContentDetails.FromDate != null && objSiteContentDetails.FromDate != null)
                {
                    storedProcedureComandText += " AND B.LastUpdateDate BETWEEN '" + objSiteContentDetails.FromDate + "' AND '" + objSiteContentDetails.ToDate + "'";
                }

                if (objSiteContentDetails.AuthorsName != null)
                {
                    storedProcedureComandText += " AND B.AuthorsName='" + objSiteContentDetails.AuthorsName + "'";
                }

                storedProcedureComandText += " ORDER BY C.ContentCategoryTitle,D.ContentRelatedToTitle,F.ActionTypeTitle, B.EntryDate DESC";
                

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContentApproval(siteApplicationSetup objsiteApplicationSetup, string contentCategory, 
            string contentRelatedTo, string blogActionType, SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"select B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,B.DataUsed,F.ActionTypeTitle 
                     from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID					 
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     LEFT JOIN blogActionType F ON B.DataUsed = F.ActionTypeID
                     WHERE A.DataUsed='A'  AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + "" +
                     " AND A.BranchID= " + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "";

                if (contentCategory != "null")
                {
                    int contentCategoryTemp = Convert.ToInt32(contentCategory);
                    storedProcedureComandText += " AND A.ContentCategoryID = " + contentCategoryTemp + "" ;
                }

                if (contentRelatedTo != "null")
                {
                    int contentRelatedToTemp = Convert.ToInt32(contentRelatedTo);
                    storedProcedureComandText += " AND A.ContentRelatedToID = " + contentRelatedToTemp + "";
                }

                if (blogActionType != "null")
                {
                    storedProcedureComandText += " AND B.DataUsed = '" + blogActionType + "'";
                }

                if (objSiteContentDetails.FromDate != null && objSiteContentDetails.FromDate != null)
                {
                    storedProcedureComandText += " AND B.LastUpdateDate BETWEEN '" + objSiteContentDetails.FromDate + "' AND '" + objSiteContentDetails.ToDate + "'";
                }

                if (objSiteContentDetails.AuthorsName != null)
                {
                    storedProcedureComandText += " AND B.AuthorsName='"+ objSiteContentDetails.AuthorsName + "'";
                }

                storedProcedureComandText += " ORDER BY C.ContentCategoryTitle,D.ContentRelatedToTitle,F.ActionTypeTitle, B.EntryDate DESC";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogForPremier(siteApplicationSetup objsiteApplicationSetup, string contentCategory,
            string contentRelatedTo, string blogActionType, SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"select B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,B.DataUsed,F.ActionTypeTitle 
                     from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID					 
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     LEFT JOIN blogActionType F ON B.DataUsed = F.ActionTypeID
                     WHERE A.DataUsed='A'  AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + "" +
                     " AND A.BranchID= " + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "";

                if (contentCategory != "null")
                {
                    int contentCategoryTemp = Convert.ToInt32(contentCategory);
                    storedProcedureComandText += " AND A.ContentCategoryID = " + contentCategoryTemp + "";
                }

                if (contentRelatedTo != "null")
                {
                    int contentRelatedToTemp = Convert.ToInt32(contentRelatedTo);
                    storedProcedureComandText += " AND A.ContentRelatedToID = " + contentRelatedToTemp + "";
                }

                if (blogActionType != "null")
                {
                    storedProcedureComandText += " AND B.DataUsed = '" + blogActionType + "'";
                }

                if (objSiteContentDetails.FromDate != null && objSiteContentDetails.FromDate != null)
                {
                    storedProcedureComandText += " AND B.LastUpdateDate BETWEEN '" + objSiteContentDetails.FromDate + "' AND '" + objSiteContentDetails.ToDate + "'";
                }

                if (objSiteContentDetails.AuthorsName != null)
                {
                    storedProcedureComandText += " AND B.AuthorsName='" + objSiteContentDetails.AuthorsName + "'";
                }

                storedProcedureComandText += " ORDER BY C.ContentCategoryTitle,D.ContentRelatedToTitle,F.ActionTypeTitle, B.EntryDate DESC";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }



        internal DataTable GetBlogActionType(siteApplicationSetup objsiteApplicationSetup)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                storedProcedureComandText = @"SELECT A.[ActionTypeID]
                  ,A.[ActionTypeTitle]
	              FROM [blogActionType] A WHERE A.[DataUsed] = 'A' AND  A.[CompanyID] = "+ objsiteApplicationSetup .CompanyID+ "" +
               " AND A.[BranchID]= "+ objsiteApplicationSetup .BranchID+ "" +
               " AND A.[ApplicationID]="+ objsiteApplicationSetup .ApplicationID+ ""+
	           " ORDER BY A.ActionTypeTitle ";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetRelatedContent(siteApplicationSetup objsiteApplicationSetup
            , SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objSiteContentDetails.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"
                    select TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " AND B.ContentID NOT IN('"+ objSiteContentDetails .ContentID+ "')" + 
                     " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"
                    select TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " AND A.ContentCategoryID = " + objSiteContentDetails.ContentCategoryID + "" +
                     " AND B.ContentID NOT IN('" + objSiteContentDetails.ContentID + "')" +
                     " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable ShowdBlogsByDefault(siteApplicationSetup objsiteApplicationSetup
            , siteContentCategory objsiteContentCategory,int blogActionDays)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objsiteContentCategory.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"select B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,B.DataUsed,F.ActionTypeTitle from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID					 
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID   
                     LEFT JOIN blogActionType F ON B.DataUsed = F.ActionTypeID
                     WHERE A.DataUsed='A' AND B.DataUsed='S' AND B.EntryDate >= DATEADD(d,-60,GETDATE())  AND 
					  A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"select B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,B.DataUsed,F.ActionTypeTitle from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID					 
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     LEFT JOIN blogActionType F ON B.DataUsed = F.ActionTypeID
                     WHERE A.DataUsed='A' AND B.DataUsed='S' AND B.EntryDate >= DATEADD(d,-60,GETDATE())  AND
					  A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                    " AND A.ContentCategoryID = " + objsiteContentCategory.ContentCategoryID + "" +
                    " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable ShowdBlogsForPremier(siteApplicationSetup objsiteApplicationSetup
            , siteContentCategory objsiteContentCategory, int blogActionDays)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objsiteContentCategory.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"select B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,B.DataUsed,F.ActionTypeTitle from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID					 
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID   
                     LEFT JOIN blogActionType F ON B.DataUsed = F.ActionTypeID
                     WHERE A.DataUsed='A' AND B.DataUsed IN ('P','M') AND B.EntryDate >= DATEADD(d,-60,GETDATE())  AND 
					  A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"select B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,B.DataUsed,F.ActionTypeTitle from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID					 
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     LEFT JOIN blogActionType F ON B.DataUsed = F.ActionTypeID
                     WHERE A.DataUsed='A' AND B.DataUsed IN ('P','M') AND B.EntryDate >= DATEADD(d,-60,GETDATE())  AND
					  A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                    " AND A.ContentCategoryID = " + objsiteContentCategory.ContentCategoryID + "" +
                    " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetBlogContent(siteApplicationSetup objsiteApplicationSetup, siteContentCategory objsiteContentCategory)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = "";
                if (objsiteContentCategory.ContentCategoryID == 0)
                {
                    storedProcedureComandText = @"
                    select TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY B.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"
                    select TOP 9 B.ContentDetailDescription,C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailImageURL,A.ContentImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
                     INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID    
                     WHERE A.DataUsed='A' AND B.DataUsed='P' AND A.CompanyID= " + objsiteApplicationSetup.CompanyID + " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " AND A.ContentCategoryID = " + objsiteContentCategory.ContentCategoryID + "" +
                     " ORDER BY B.EntryDate DESC";
                }

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetBlogContentUser(siteApplicationSetup objsiteApplicationSetup)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"select DISTINCT C.ContentCategoryTitle,D.ContentRelatedToTitle,B.ContentDetailTitle,F.ActionTypeTitle,B.SequenceNo,B.ContentDetailDescription,B.ContentDetailImageURL,A.ContentImageURL,
                        B.ContentDetailSubTitle,B.ContentID,B.ContentIntroductoryText,B.EntryDate, (E.FirstName+' ' + E.MiddleName+' '+ E.LastName) AS FullName,G.Remarks  
                        from blogContentHeader A
                     INNER JOIN blogContentDetail B ON A.ContentParentID=B.ContentParentID
					 INNER JOIN blogContentCategory C ON A.ContentCategoryID = C.ContentCategoryID
                     INNER JOIN blogContentRelatedTo D ON A.ContentRelatedToID=D.ContentRelatedToID
                     INNER JOIN uUserProfile E ON B.EntryUserID = E.UserProfileID  
					 INNER JOIN blogActionType F ON B.DataUsed = F.ActionTypeID  
                     LEFT JOIN blogContentPublishMethod G ON B.ContentID=G.ContentID AND B.DataUsed=G.ActionID
                     WHERE
                     A.CompanyID= " + objsiteApplicationSetup.CompanyID + "" +
                     " AND A.BranchID=" + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID= " + objsiteApplicationSetup.ApplicationID + "" +
                     " AND B.EntryUserID = '" + objsiteApplicationSetup.EntryUserID + "' " +
                     " ORDER BY C.ContentCategoryTitle,D.ContentRelatedToTitle,B.EntryDate,B.SequenceNo DESC";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetBlogQuote(siteApplicationSetup objsiteApplicationSetup, siteContentCategory objsiteContentCategory, int quoteTypeID)
        {
            try
            {
                DataTable dtBlogQuote = null;
                var storedProcedureComandText = "";
                if (quoteTypeID == 0)
                {
                    storedProcedureComandText = @"
                     SELECT TOP 3 A.QuoteID, A.QuoteDescription, A.EntryDate,B.AuthorFullName,B.QuoteAuthorID FROM blogQuote A  
                     INNER JOIN blogQuoteAuthor B ON A.QuoteAuthorID = B.QuoteAuthorID 
                     WHERE A.DataUsed = 'A' AND A.[CompanyID]= " + objsiteApplicationSetup.CompanyID + " " +
                     " AND A.[BranchID] = " + objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID = " + objsiteApplicationSetup.ApplicationID + "" +
                     " ORDER BY A.EntryDate DESC";
                }
                else
                {
                    storedProcedureComandText = @"
                     SELECT TOP 3 A.QuoteID, A.QuoteDescription, A.EntryDate,B.AuthorFullName,B.QuoteAuthorID FROM blogQuote A  
                     INNER JOIN blogQuoteAuthor B ON A.QuoteAuthorID = B.QuoteAuthorID 
                     WHERE A.DataUsed = 'A' AND A.[CompanyID]= "+ objsiteApplicationSetup.CompanyID + " " +
                     " AND A.[BranchID] = "+ objsiteApplicationSetup.BranchID + "" +
                     " AND A.ApplicationID = "+ objsiteApplicationSetup.ApplicationID + "" +
                     " AND A.QuoteTypeID = " + quoteTypeID + "" +
                     " ORDER BY A.EntryDate DESC";
                }

                dtBlogQuote = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtBlogQuote;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetSiteContent(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"
                select B.ContentDetailDescription,B.ContentDetailImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentIntroductoryText,B.EntryDate, (C.FirstName+' ' + C.MiddleName+' '+ C.LastName) AS FullName from siteContentDetail B
                INNER JOIN uUserProfile C ON B.EntryUserID = C.UserProfileID    
                WHERE B.DataUsed='A' 
                AND B.ContentDetailID=" + objSiteContentDetails.ContentID + "";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetBlogContent(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"
                select DISTINCT B.ContentDetailDescription,B.ContentDetailImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentIntroductoryText,B.EntryDate, (C.FirstName+' ' + C.MiddleName+' '+ C.LastName) AS FullName, D.ContentCategoryID  from blogContentDetail B
                INNER JOIN uUserProfile C ON B.EntryUserID = C.UserProfileID  
                INNER JOIN blogContentHeader D ON B.ContentParentID = D.ContentParentID
                WHERE 
                B.ContentID=" + objSiteContentDetails.ContentID + "";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetBlogContentAdmin(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"
                select DISTINCT B.ContentDetailDescription,B.ContentDetailImageURL,B.ContentDetailTitle,B.ContentDetailSubTitle,B.ContentIntroductoryText,B.EntryDate, (C.FirstName+' ' + C.MiddleName+' '+ C.LastName) AS FullName, C.Email from blogContentDetail B
                INNER JOIN uUserProfile C ON B.EntryUserID = C.UserProfileID    
                WHERE B.ContentID=" + objSiteContentDetails.ContentID + "";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetBlogCategory(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"select C.ContentCategoryID,C.ContentCategoryTitle from  siteContentDetail A 
                    INNER JOIN [siteContentHeader] B ON A.ContentID = B.ContentID
                    INNER JOIN siteContentCategory C ON B.ContentTypeID=C.ContentTypeID
                    WHERE A.ContentDetailID =" + objSiteContentDetails.ContentID + "";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetBlogCategory()
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"SELECT DISTINCT A.ContentCategoryID,A.ContentCategoryTitle FROM blogContentCategory A
                WHERE A.DataUsed = 'A' AND A.CompanyID=1 AND A.BranchID=1 AND A.ApplicationID=2 ORDER BY A.ContentCategoryTitle";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal string GetSelectedBlogCategory(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                string contentCategoryTitle = null;
                var storedProcedureComandText = @"select C.ContentCategoryTitle from  blogContentDetail A 
                    INNER JOIN blogContentHeader B ON A.ContentParentID = B.ContentParentID
                    INNER JOIN blogContentCategory C ON B.ContentCategoryID=C.ContentCategoryID
                    WHERE A.ContentID =" + objSiteContentDetails.ContentID + "";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                contentCategoryTitle = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, storedProcedureComandText);
                return contentCategoryTitle;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void ActionByAdmin(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                var storedProcedureComandText = @"UPDATE [blogContentDetail]
                   SET 
                      [DataUsed] = ISNULL('"+ objSiteContentDetails .ActionTypeID+ "',[DataUsed])"
	                  + " ,[LastUpdateDate] = ISNULL(GETDATE(),[LastUpdateDate])"
                      + " ,[LastUpdateUserID] = ISNULL('" + objSiteContentDetails.EntryUserName + "',[LastUpdateUserID])"
                      + " ,[ContentDetailImageURL] = ISNULL('" + objSiteContentDetails .ContentDetailImageURL+ "',[ContentDetailImageURL])"
                 + " WHERE [ContentID] = '"+ objSiteContentDetails .ContentID+ "';";

                storedProcedureComandText += @"INSERT INTO [blogContentPublishMethod] ([ContentLogID],[ContentID]
           ,[SecquenceNo],[ActionID],[ActionDate],[Remarks],[PreviousContent],[QualityTag],[PremiumDate],[AppearsDate]
           ,[EntryDate],[EntryUserID])VALUES (" +
               "'" + objSiteContentDetails.ContentLogID + "'"
               +",'" + objSiteContentDetails.ContentID + "'"
               +"," + objSiteContentDetails.SecquenceNo + ""
               +",'" + objSiteContentDetails.ActionTypeID + "'"
               +"," + "CAST(GETDATE() AS DateTime)" + ""
               +",'" + objSiteContentDetails.RemarksAction + "'"
               +",'" + objSiteContentDetails.ContentDescription + "'"
               +",'" + objSiteContentDetails.QualityTag + "'"
               + ",'" + objSiteContentDetails.PremiumDate + "'"
               + ",'" + objSiteContentDetails.AppearsDate + "'"
               +"," + "CAST(GETDATE() AS DateTime)" + ""
               +",'" + objSiteContentDetails.EntryUserName + "'"
               +");";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
                this.SendEmailToAuthors(objSiteContentDetails);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public void SendEmailToAuthors(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                MailServiceSetup objMailServiceSetup = new MailServiceSetup();
                objMailServiceSetup.MailBody = "";
                if (objSiteContentDetails.ActionTypeID == "P")
                {
                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    Thank you for choosing our service, Your article has published.<br /><br />

                    <br /><br />

                    We wish you to experience an excellent journey in using our business solution.<br /><br />


                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address > ";

                }
                else if (objSiteContentDetails.ActionTypeID == "B")
                {
                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    Thank you for choosing our service, Your article has returned.<br /><br />

                    <br /><br />

                    We wish you to experience an excellent journey in using our business solution.<br /><br />


                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address > ";

                }
                else if (objSiteContentDetails.ActionTypeID == "C")
                {
                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    Thank you for choosing our service, Your article has canceled.<br /><br />

                    <br /><br />

                    We wish you to experience an excellent journey in using our business solution.<br /><br />


                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address > ";

                }
                else if (objSiteContentDetails.ActionTypeID == "M" && objSiteContentDetails.QualityTag == "M")
                {
                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    Thank you for choosing our service, Your article has selected as premier article.<br /><br />

                    <br /><br />

                    We wish you to experience an excellent journey in using our business solution.<br /><br />


                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address > ";

                }
                else
                {
                    objMailServiceSetup.MailBody = "";
                }

                objMailServiceSetup.EmailTo = objSiteContentDetails.AuthorsEmailID;
                objMailServiceSetup.MailtypeID = "2";
                ArrayList attachDocument = new ArrayList();
                objMailServiceSetup.AttachItem = attachDocument;
                MailServiceController objMailServiceController = new MailServiceController();
                objMailServiceController.eMailSendServiceHTML(objSiteContentDetails.CompanyID, objMailServiceSetup);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void SuspendByAdmin(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                var storedProcedureComandText = @"UPDATE [blogContentDetail]
                   SET 
                      [DataUsed] = ISNULL('" + objSiteContentDetails.ActionTypeID + "',[DataUsed])"
                      + " ,[LastUpdateDate] = ISNULL(GETDATE(),[LastUpdateDate])"
                      + " ,[LastUpdateUserID] = ISNULL('" + objSiteContentDetails.EntryUserName + "',[LastUpdateUserID])"
                      + " WHERE [ContentID] = '" + objSiteContentDetails.ContentID + "';";

                storedProcedureComandText += @"INSERT INTO [blogContentPublishMethod] ([ContentLogID],[ContentID]
           ,[SecquenceNo],[ActionID],[ActionDate],[Remarks],[PreviousContent],[QualityTag],[PremiumDate],[AppearsDate]
           ,[EntryDate],[EntryUserID])VALUES (" +
               "'" + objSiteContentDetails.ContentLogID + "'"
               + ",'" + objSiteContentDetails.ContentID + "'"
               + "," + objSiteContentDetails.SecquenceNo + ""
               + ",'" + objSiteContentDetails.ActionTypeID + "'"
               + "," + "CAST(GETDATE() AS DateTime)" + ""
               + ",'" + objSiteContentDetails.RemarksAction + "'"
               + ",'" + objSiteContentDetails.ContentDescription + "'"
               + ",'" + objSiteContentDetails.QualityTag + "'"
               + ",'" + objSiteContentDetails.PremiumDate + "'"
               + ",'" + objSiteContentDetails.AppearsDate + "'"
               + "," + "CAST(GETDATE() AS DateTime)" + ""
               + ",'" + objSiteContentDetails.EntryUserName + "'"
               + ");";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}