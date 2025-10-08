using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;

namespace BootstrapERP.AppClass.DataAccess
{
    public class BlogContentAccessController : DataAccessBase
    {
        internal DataTable GetblogContentCategory(SiteContentDetails objSiteContentDetails)
        {
            try
            {
                DataTable dtBlogContentCategory = new DataTable();
                var storedProcedureComandText = @"SELECT DISTINCT A.ContentCategoryID, A.ContentCategoryTitle FROM blogContentCategory A
                WHERE A.DataUsed='A' AND A.CompanyID="+ objSiteContentDetails .CompanyID+ "" +
                " AND A.BranchID="+ objSiteContentDetails.BranchID + "" +
                " AND A.ApplicationID="+ objSiteContentDetails .ApplicationID+ "" +
                " ORDER BY A.ContentCategoryTitle";

                dtBlogContentCategory = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtBlogContentCategory;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        //internal DataTable GetblogContentCategory(SiteContentDetails objSiteContentDetails)
        //{
        //    try
        //    {
        //        DataTable dtInformation = new DataTable();
        //        string sqlString = @"SELECT A.UserProfileID,C.CompanyID,C.EmployeeID,D.FullName FROM uUserList A 
        //        INNER JOIN uUserProfile B 
        //        ON A.UserProfileID = B.UserProfileID
        //        INNER JOIN UserSecurityCode C ON B.SecurityCode = C.SecurityCode
        //        INNER JOIN hrEmployeeSetup D ON C.CompanyID = D.CompanyID AND C.EmployeeID = D.EmployeeID 
        //        WHERE C.DataUsed = 'A' AND A.UserName = '" + objUserList.UserName + "' AND B.[Password] = '" + objUserList.UserPassword + "'";
        //        dtInformation = clsDataManipulation.GetData(this.ConnectionString, sqlString);
        //        return dtInformation;

        //    }
        //    catch (Exception msgException)
        //    {

        //        throw msgException;
        //    }
        //}
    }
}