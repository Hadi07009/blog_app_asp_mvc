using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;

namespace BootstrapERP.AppClass.DataAccess
{
    public class BannerAccess : DataAccessBase
    {
        internal void SaveBanner(SiteBanner objSiteBanner)
        {
            try
            {
                objSiteBanner.BannerID = this.GetMaxBannerID();
                var storedProcedureComandText = @"INSERT INTO [blogBanner]
               ([CompanyID]
               ,[BranchID]
               ,[ApplicationID]
               ,[BannerID]
               ,[BannerTitle]
               ,[BannerImageURL]
               ,[BannerRemarks]
               ,[DataUsed]
               ,[EntryDate]
               ,[EntryUserID]
		       )
                VALUES (" +
               "" + objSiteBanner.CompanyID + "" +
               "," + objSiteBanner.BranchID + "" +
               "," + objSiteBanner.ApplicationID + "" +
               "," + objSiteBanner.BannerID + "" +
               ",'" + objSiteBanner.BannerTitle + "'" +
               ",'" + objSiteBanner.BannerImageURL + "'" +
               ",'" + objSiteBanner.BannerRemarks + "'" +
               ",'I'" +
               ",CAST(GETDATE() AS DateTime)" +
               ",'" + objSiteBanner.EntryUserName + "'" +
               " );";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetAllBanner(SiteBanner objSiteBanner)
        {
            try
            {
                DataTable dtAllBanner = null;
                var storedProcedureComandText = @"
                    SELECT DISTINCT A.BannerID, A.BannerTitle, A.BannerImageURL, A.BannerRemarks, A.EntryDate, A.DataUsed FROM blogBanner A
                    WHERE A.CompanyID= " + objSiteBanner.CompanyID + " AND A.BranchID="+ objSiteBanner .BranchID+ "" +
                    " AND A.ApplicationID="+ objSiteBanner .ApplicationID+ ""+
                    " ORDER BY A.EntryDate DESC; ";

                dtAllBanner = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtAllBanner;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void ActiveInactiveBanner(SiteBanner objSiteBanner)
        {
            try
            {
                var storedProcedureComandText = @"";
                if (objSiteBanner.DataUsedValue == "A")
                {
                    storedProcedureComandText += @"
                UPDATE [blogBanner]
                   SET [DataUsed] = 'I'
                    ,[LastUpdateDate] = CAST(GETDATE() AS DateTime)
                    ,[LastUpdateUserID] = '" + objSiteBanner.EntryUserName + "'" +
                 " WHERE [CompanyID]= "+ objSiteBanner .CompanyID+ " AND [BranchID] ="+ objSiteBanner .BranchID+ "" +
                 " AND [ApplicationID] = "+ objSiteBanner .ApplicationID+ ";";
                    storedProcedureComandText += @"
                UPDATE [blogBanner]
                   SET [DataUsed] = 'A'
                      ,[LastUpdateDate] = CAST(GETDATE() AS DateTime)
                      ,[LastUpdateUserID] = '" + objSiteBanner.EntryUserName + "'" +
                      " WHERE [CompanyID]= " + objSiteBanner.CompanyID + " AND [BranchID] =" + objSiteBanner.BranchID + "" +
                 " AND [ApplicationID] = " + objSiteBanner.ApplicationID + " AND [BannerID] = "+ objSiteBanner .BannerID+ ";";
                    
                }

                if (objSiteBanner.DataUsedValue == "I")
                {
                    storedProcedureComandText += @"
                UPDATE [blogBanner]
                   SET [DataUsed] = 'I'
                      ,[LastUpdateDate] = CAST(GETDATE() AS DateTime)
                      ,[LastUpdateUserID] = '" + objSiteBanner.EntryUserName + "'" +
                      " WHERE [CompanyID]= " + objSiteBanner.CompanyID + " AND [BranchID] =" + objSiteBanner.BranchID + "" +
                 " AND [ApplicationID] = " + objSiteBanner.ApplicationID + " AND [BannerID] = " + objSiteBanner.BannerID + ";";
                }


                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private int GetMaxBannerID()
        {
            try
            {
                int bannerID = 0;
                var storedProcedureComandText = "  SELECT ISNULL( MAX( BannerID),0) +1  as BannerID FROM blogBanner";
                bannerID = clsDataManipulation.GetMaximumValueUsingSQL(this.ConnectionString, storedProcedureComandText);
                return bannerID;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}