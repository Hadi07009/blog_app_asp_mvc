using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace BootstrapERP.AppClass.DataAccess
{
    public class TwoColumnTablesController : DataAccessBase
    {
        public void Save(string connectionString, TwoColumnTables objTwoColumnTables)
        {
            try
            {
                var storedProcedureComandTest = "exec [ACT_sysTwoColumnTables] " +
                                        0 + ",'" +
                                        objTwoColumnTables.TableName + "','" +
                                        objTwoColumnTables.EntryMode + "','" +
                                        objTwoColumnTables.RelatedTo + "'," +
                                        objTwoColumnTables.RelatedUserRoleID + ",'" +
                                        objTwoColumnTables.EntrySystem + "','" +
                                        objTwoColumnTables.EntryUserName + "'";
                clsDataManipulation.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public DataTable GetRecord(string connectionString)
        {
            try
            {
                DataTable dtItem = null;
                string sqlString = @"SELECT A.[TableID] ,A.[TableName]
                ,A.[EntryMode]
                ,A.[RelatedTo]
                ,A.[RelatedUserRoleID]
                ,A.[EntrySystem]  
                ,B.RelatedToText
                FROM [sysTwoColumnTables] A
                LEFT JOIN [sysRelatedUserRole] B ON A.RelatedUserRoleID = B.RelatedToID
                 WHERE A.DataUsed = 'A' ORDER BY A.[EntrySystem],A.[TableName],A.[RelatedUserRoleID]";
                dtItem = clsDataManipulation.GetData(connectionString, sqlString);
                return dtItem;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public DataTable GetRecord(string connectionString, TwoColumnTables objTwoColumnTables)
        {
            try
            {
                DataTable dtItem = null;
                string sqlString = @"SELECT [TableID]
                                  ,[TableName]
                                    FROM [sysTwoColumnTables] WHERE DataUsed = 'A' AND EntrySystem = '" + objTwoColumnTables.EntrySystem + "' AND RelatedUserRoleID = " + objTwoColumnTables.RelatedUserRoleID + " ORDER BY [TableName],[RelatedUserRoleID]";
                dtItem = clsDataManipulation.GetData(connectionString, sqlString);
                return dtItem;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
        public DataTable GetRecord(string connectionString, TwoColumnTables objTwoColumnTables, EmployeeSetup objEmployeeSetup)
        {
            try
            {
                DataTable dtItem = null;
                string sqlString = @"SELECT DISTINCT A.[TableID],A.[TableName],A.[RelatedUserRoleID] FROM [sysTwoColumnTables] A INNER JOIN uUsersInRelatedRoles B ON A.RelatedUserRoleID = B.RoleID
                WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND A.EntrySystem = '" + objTwoColumnTables.EntrySystem + "' AND " +
                " B.CompanyID = " + objEmployeeSetup.CompanyID + " AND B.UserId = '" + objEmployeeSetup.EmployeeID + "' ORDER BY A.[TableName],A.[RelatedUserRoleID]";
                dtItem = clsDataManipulation.GetData(connectionString, sqlString);
                return dtItem;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public void Delete(string connectionString, TwoColumnTables objTwoColumnTables)
        {
            try
            {
                string sqlString = "UPDATE sysTwoColumnTables SET DataUsed	= 'I' WHERE TableID = " + objTwoColumnTables.TableID + "";
                clsDataManipulation.StoredProcedureExecuteNonQuery(connectionString, sqlString);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public void Update(string connectionString, TwoColumnTables objTwoColumnTables)
        {
            try
            {
                var storedProcedureComandTest = "exec [ACT_sysTwoColumnTables] " +
                                        objTwoColumnTables.TableID + ",'" +
                                        objTwoColumnTables.TableName + "','" +
                                        objTwoColumnTables.EntryMode + "','" +
                                        objTwoColumnTables.RelatedTo + "'," +
                                        objTwoColumnTables.RelatedUserRoleID + ",'" +
                                        objTwoColumnTables.EntrySystem + "','" +
                                        objTwoColumnTables.EntryUserName + "'";
                clsDataManipulation.StoredProcedureExecuteNonQuery(connectionString, storedProcedureComandTest);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        internal void LoadRelatedUserRoleDDL(DropDownList ddlRelatedUserRoleID)
        {
            try
            {
                ClsDropDownListController.LoadDropDownList(this.ConnectionString, this.SqlForRelatedUserRole(), ddlRelatedUserRoleID, "RelatedToText", "RelatedToID");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void LoadRelatedUserRoleLB(ListBox givenListBoxID, CompanySetup objCompanySetup)
        {
            try
            {
                ClsListBoxController.LoadListBox(this.ConnectionString, this.SqlForRelatedUserRole(objCompanySetup), givenListBoxID, "RelatedToText", "RelatedToID");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private string SqlForRelatedUserRole(CompanySetup objCompanySetup)
        {
            try
            {
                string sqlString = null;
                sqlString = " SELECT [RelatedToID],[RelatedToText]  FROM [sysRelatedUserRole] WHERE CompanyID = " + objCompanySetup.CompanyID + " ORDER BY [RelatedToText]";
                return sqlString;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private string SqlForRelatedUserRole()
        {
            try
            {
                string sqlString = null;
                sqlString = " SELECT A.RelatedToID,A.RelatedToText  FROM sysRelatedUserRoleSetup A WHERE A.DataUsed = 'A' ORDER BY A.RelatedToText";
                return sqlString;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}