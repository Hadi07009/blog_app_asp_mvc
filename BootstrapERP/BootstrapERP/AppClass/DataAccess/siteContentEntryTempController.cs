using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class siteContentEntryTempController : DataAccessBase
    {
        internal void Save(string storedProcedureComandText)
        {
            clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
        }

        internal void Update(string storedProcedureComandText)
        {
            clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
        }

        internal void Delete(SiteContentHeader objSiteContentHeader)
        {
            try
            {
                var storedProcedureComandTextDetail = @"delete from siteContentEntryTemp WHERE [EntryUserID] = '" + objSiteContentHeader.EntryUserName + "';";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}