using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class PermissionController :DataAccessBase
    {
        internal void SaveRoleData(CompanySetup objCompanySetup, UserPermission objUserPermission)
        {
            try
            {
                if (objCompanySetup.CompanyID == -1)
                {
                    throw new Exception(" Company is required ");

                }

                objUserPermission.RoleID = GetRoleID();
                var storedProcedureComandText = "INSERT INTO [uRoleSetup] ([CompanyID],[RoleID],[RoleTypeID],[RoleName],[DataUsed],[EntryUserID],[EntryDate]) VALUES ( " +
                                                 objCompanySetup.CompanyID + "," +
                                                 objUserPermission.RoleID + ",'" +
                                                 objUserPermission.RoleType + "','" +
                                                 objUserPermission.RoleName + "','" +
                                                 "A" + "', '" +
                                                 objCompanySetup.EntryUserName + "'," +
                                                 "CAST(GETDATE() AS DateTime));";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

                foreach (var itemNo in objUserPermission.nodeList)
                {
                    objUserPermission.NodeID = Convert.ToInt32(itemNo.ToString());
                    var storedProcedureComandTextNode = "INSERT INTO [uRoleSetupDetails] ([RoleID],[NodeID],[DataUsed],[EntryUserID],[EntryDate]) VALUES ( " +
                                                 objUserPermission.RoleID + "," +
                                                 objUserPermission.NodeID + ",'" +
                                                 "A" + "', '" +
                                                 objCompanySetup.EntryUserName + "'," +
                                                 "CAST(GETDATE() AS DateTime));";
                    clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextNode);

                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void UpdateRoleData(CompanySetup objCompanySetup, UserPermission objUserPermission)
        {
            try
            {
                if (objCompanySetup.CompanyID == -1)
                {
                    throw new Exception(" Company is required ");

                }

                string sqlForUpdate = @"UPDATE [uRoleSetup] SET [RoleName] = '" + objUserPermission.RoleName + "',[LastUpdateDate] = CAST(GETDATE() AS DateTime)," +
                                "[LastUpdateUserID] = '" + objCompanySetup.EntryUserName + "'"+
                                " ,[RoleTypeID] = '" + objUserPermission.RoleType + "'"+
                                " WHERE [CompanyID] = " + objCompanySetup.CompanyID + " AND " +
                                " [RoleID] = " + objUserPermission.RoleID + "; " +
                                 " DELETE FROM uRoleSetupDetails WHERE RoleID = " + objUserPermission.RoleID + "";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, sqlForUpdate);

                foreach (var itemNo in objUserPermission.nodeList)
                {
                    objUserPermission.NodeID = Convert.ToInt32(itemNo.ToString());
                    var storedProcedureComandTextNode = "INSERT INTO [uRoleSetupDetails] ([RoleID],[NodeID],[DataUsed],[EntryUserID],[EntryDate]) VALUES ( " +
                                                 objUserPermission.RoleID + "," +
                                                 objUserPermission.NodeID + ",'" +
                                                 "A" + "', '" +
                                                 objCompanySetup.EntryUserName + "'," +
                                                 "CAST(GETDATE() AS DateTime));";
                    clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextNode);
                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetRoleList(CompanySetup objCompanySetup, AddNode objAddNode)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"SELECT DISTINCT A.NodeID
                FROM uRoleSetupDetails A
                INNER JOIN uRoleSetup Z ON A.RoleID=Z.RoleID
			    WHERE A.DataUsed='A' AND Z.DataUsed='A' AND "+
                " Z.CompanyID="+ objCompanySetup.CompanyID + " AND A.RoleID="+ objAddNode .RoleID+ ""+
			    " ORDER BY A.NodeID ;";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetRoleDetails(CompanySetup objCompanySetup)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @"SELECT DISTINCT Y.RoleTypeTitle,Z.RoleTypeID,Z.RoleName,ST2.RoleID, 
                SUBSTRING(
                    (
                        SELECT ','+B.ActivityName  AS [text()]
                        FROM uRoleSetupDetails ST1
			            INNER JOIN uDefaultNodeList B ON ST1.NodeID = B.NodeTypeID
			            WHERE ST1.RoleID = ST2.RoleID AND ST1.NodeID NOT IN('11001') AND ST1.DataUsed='A'
			            ORDER BY ST1.RoleID
                        FOR XML PATH ('')
                    ), 2, 1000) [ActivityList]
            FROM uRoleSetupDetails ST2
            INNER JOIN uRoleSetup Z ON ST2.RoleID=Z.RoleID
            INNER JOIN uRoleTypeSetup Y ON Z.RoleTypeID = Y.RoleTypeID
            WHERE ST2.DataUsed='A' AND Z.CompanyID = " + objCompanySetup .CompanyID+ ";";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public int GetRoleID()
        {
            try
            {
                UserPermission objUserPermission = new UserPermission();
                var storedProcedureComandText = "SELECT ISNULL( MAX( RoleID ),0) +1 FROM [uRoleSetup]";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                return objUserPermission.RoleID = objclsDataManipulation.GetSingleValue(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string CheckNodeID(int roleID,string nodeID)
        {
            try
            {
                string nodeIDTemp = "";
                var sqlString = @" SELECT NodeID FROM uRoleSetupDetails WHERE DataUsed = 'A' AND RoleID ="+ roleID + " " +
                    " AND NodeID='"+ nodeID + "'";
                DataTable dtNode = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                if (dtNode.Rows.Count > 0)
                {
                    nodeIDTemp = "true";
                }

                return nodeIDTemp;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public string CheckUserRoleType(string userEmail, string nodeID)
        {
            try
            {
                string nodeIDTemp = "";
                var sqlString = @" SELECT A.RoleTypeID FROM uUsersInRoles A
                INNER JOIN uUserProfile B ON A.UserProfileID=B.UserProfileID
                WHERE B.Email='"+ userEmail + "' AND A.RoleTypeID='"+ nodeID + "';";
                DataTable dtNode = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                if (dtNode.Rows.Count > 0)
                {
                    nodeIDTemp = "true";
                }

                return nodeIDTemp;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public string CheckUserRole(string userEmail, string nodeID)
        {
            try
            {
                string nodeIDTemp = "";
                var sqlString = @" SELECT A.RoleID FROM uUsersInRoles A
                INNER JOIN uUserProfile B ON A.UserProfileID=B.UserProfileID
                WHERE B.Email='" + userEmail + "' AND A.RoleID='" + nodeID + "';";
                DataTable dtNode = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                if (dtNode.Rows.Count > 0)
                {
                    nodeIDTemp = "true";
                }

                return nodeIDTemp;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public bool CheckRoleByType(string roleTypeID, int roleID)
        {
            try
            {
                bool nodeIDTemp = false;
                var sqlString = @" SELECT A.RoleID FROM uRoleSetup A
                WHERE A.DataUsed='A' AND A.RoleTypeID='"+ roleTypeID + "' AND A.RoleID="+ roleID + ";";
                DataTable dtNode = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                if (dtNode.Rows.Count > 0)
                {
                    nodeIDTemp = true;
                }

                return nodeIDTemp;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public int CheckEmail(string userEmailID)
        {
            try
            {
                int emailIDTemp = 0;
                var sqlString = @" SELECT A.Email FROM uUserProfile A WHERE A.DataUsed='A' AND A.Email='"+ userEmailID + "';";
                DataTable dtEmail = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                if (dtEmail.Rows.Count > 0)
                {
                    emailIDTemp = 1;
                }

                return emailIDTemp;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void SaveUserRole(EmployeeSetup objEmployeeSetup, UserPermission objUserPermission)
        {
            try
            {
                string sqlForDelete = @" DELETE FROM uUsersInRoles WHERE UserProfileID = '"+ objEmployeeSetup.EmployeeID + "'; ";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, sqlForDelete);

                string storedProcedureComandTextNode = null;
                foreach (var itemRoleType in objUserPermission.roleTypeList)
                {
                    objUserPermission.RoleType = itemRoleType;

                    foreach (var itemNo in objUserPermission.roleList)
                    {
                        objUserPermission.RoleID = itemNo;
                        bool checkRoleID = false;
                        checkRoleID = CheckRoleByType(objUserPermission.RoleType, objUserPermission.RoleID);
                        if (checkRoleID == true)
                        {
                            storedProcedureComandTextNode += @" INSERT INTO [uUsersInRoles] ([CompanyID],[UserProfileID],[RoleTypeID],[RoleID],[DataUsed],[EntryUserID],[EntryDate]) VALUES(" +
                                                        objEmployeeSetup.CompanyID + ",'" +
                                                        objEmployeeSetup.EmployeeID + "','" +
                                                        objUserPermission.RoleType + "'," +
                                                        objUserPermission.RoleID + ",'" +
                                                        "A" + "','" +
                                                        objEmployeeSetup.EntryUserName + "'," +
                                                        "CAST(GETDATE() AS DateTime)" + ");";

                        }
                    }

                }

                
                

                if (storedProcedureComandTextNode != null)
                {
                    //foreach (var itemNo in objUserPermission.RelatedUserRoleList)
                    //{
                    //    TwoColumnTables objTwoColumnTables = new TwoColumnTables();
                    //    objTwoColumnTables.RelatedUserRoleID = itemNo;
                    //    storedProcedureComandTextNode += @"INSERT INTO [uUsersInRelatedRoles] ([CompanyID],[UserId],[RoleID],[DataUsed],[EntryUserID],[EntryDate]) VALUES(" +
                    //    objEmployeeSetup.CompanyID + ",'" +
                    //    objEmployeeSetup.EmployeeID + "'," +
                    //    objTwoColumnTables.RelatedUserRoleID + ",'" +
                    //    "A" + "','" +
                    //    objEmployeeSetup.EntryUserName + "'," +
                    //    "CAST(GETDATE() AS DateTime)" + ");";
                    //}

                    clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextNode);


                    CompanySetup objCompanySetup = new CompanySetup();
                    objCompanySetup.CompanyID = objEmployeeSetup.CompanyID;
                    UserList objUserList = new UserList();
                    UserListController objUserListController = new UserListController();
                    UserProfile objUserProfile = new UserProfile();
                    objUserProfile.UserProfileID = objEmployeeSetup.EmployeeID;
                    objUserList.UserID = objUserListController.GetUseID(objUserProfile);
                    CompanyWiseUserListController objCompanyWiseUserListController = new CompanyWiseUserListController();
                    objCompanyWiseUserListController.Save(objCompanySetup, objUserList);
                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

    }
}