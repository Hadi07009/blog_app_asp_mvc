using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;

namespace BootstrapERP.AppClass.DataAccess
{
    public class StandardOrgElementsController : DataAccessBase
    {
        internal void LoadStandardOrgElements(ListBox listBoxStandardOrgElements)
        {
            try
            {
                ClsListBoxController.LoadListBox(this.ConnectionString, this.SqlGetStandardOrgElements(), listBoxStandardOrgElements, "OrgElementName", "OrgElementID");
            }
            catch (Exception msgException)
            {
                
                throw msgException;
            }
        }

        internal DataTable GetStandardOrgElements()
        {
            try
            {
                DataTable dtStandardOrgElements = new DataTable();
                string sqlString = this.SqlGetStandardOrgElements();
                dtStandardOrgElements = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtStandardOrgElements;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string SqlGetStandardOrgElements()
        {
            try
            {
                string sqlString = null;
                sqlString = @"SELECT [OrgElementID]
                              ,[OrgElementName]
                          FROM [orgStandardOrgElements] 
                          WHERE [DataUsed] = 'A' ORDER BY [HierarchyID]";
                return sqlString;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}