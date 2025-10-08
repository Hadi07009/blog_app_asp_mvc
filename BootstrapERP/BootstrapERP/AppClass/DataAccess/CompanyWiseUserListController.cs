using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPWebApplication.AppClass.CommonClass;
using System.Data;
using BootstrapERP.Models;
using BootstrapERP.AppClass.CommonClass;

namespace BootstrapERP.AppClass.DataAccess
{
    public class CompanyWiseUserListController : DataAccessBase
    {
        public void Save(CompanySetup objCompanySetup, UserList objUserList)
        {
            try
            {
                bool userID = CheckUserID(objCompanySetup, objUserList);
                if (userID == true)
                {
                    var storedProcedureComandText = "INSERT INTO [uCompanyWiseUserList]([CompanyID],[UserNumber],[UserID]) VALUES (" + objCompanySetup.CompanyID + " " +
                " ,(SELECT ISNULL(MAX(A.[UserNumber]),0)+1 AS [UserNumber] FROM [uCompanyWiseUserList] A WHERE A.CompanyID= " + objCompanySetup.CompanyID + "),'" + objUserList.UserID + "')";
                    clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
                }
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private bool CheckUserID(CompanySetup objCompanySetup, UserList objUserList)
        {
            try
            {
                bool userID = true;
                DataTable dtUserID = GetUserID(objCompanySetup, objUserList);
                if (dtUserID.Rows.Count > 0)
                {
                    userID = false;
                }

                return userID;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private DataTable GetUserID(CompanySetup objCompanySetup, UserList objUserList)
        {
            try
            {
                DataTable dtUseID = null;
                var sqlString = @" SELECT CompanyID,UserID FROM uCompanyWiseUserList WHERE CompanyID = " + objCompanySetup.CompanyID + " AND UserID = '" + objUserList.UserID + "' ";
                dtUseID = clsDataManipulation.GetData(this.ConnectionString, sqlString);

                return dtUseID;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}