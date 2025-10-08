using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BootstrapERP.Models;
using BootstrapERP.AppClass.CommonClass;
using System.Security.Cryptography;
using System.Text;

namespace BootstrapERP.AppClass.DataAccess
{
    public class OnlineRegisterController : DataAccessBase
    {
        private CompanySetupController _objCompanySetupController;
        private CompanyDetailsSetup _objCompanyDetailsSetup;
        private UserProfileOnlineController _objUserProfileOnlineController;
        private UserPermissionController _objUserPermissionController;
        private UserPermission _objUserPermission;
        private CompanySetup _objCompanySetup;
        private string _entryUserName = null;
        private int _companyID;
        private UserProfileOnline objUserProfileOnline;
        internal void Update(UserList objUserList, int tagOnline )
        {
            try
            {
                if (objUserList.UserPassword.ToString().Length < 6)
                {
                    throw new Exception("Passwords are required to be a minimum of 6 characters in length.");

                }

                if (objUserList.UserPassword != objUserList.ConfirmPassword)
                {
                    throw new Exception("The password and confirmation password do not match.");

                }

                if (tagOnline == 1)
                {
                    if (objUserList.SecurityCode != CheckSecurityCodeOnline(objUserList))
                    {
                        throw new Exception("The security code and email do not match.");

                    }

                    this.GetReadyForOnlineUser(objUserList);

                }

                if (tagOnline == 0)
                {
                    if (objUserList.SecurityCode != CheckSecurityCodeRegister(objUserList))
                    {
                        throw new Exception("The security code and email do not match.");

                    }

                }

                OnlineRegisterController objOnlineRegisterController = new OnlineRegisterController();
                objUserList.UserPassword = objOnlineRegisterController.EncodePassword(objUserList.UserPassword);
                var storedProcedureComandText = "exec [uUserRegistrationUpdate] '" +
                                        objUserList.UserPassword + "','" +
                                        objUserList.SecurityCode + "','" +
                                        objUserList.UserName + "'";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal string CheckUserEmail(uUserProfileTemp objuUserProfileTemp)
        {
            try
            {
                var storedProcedureComandTextDetail = @"SELECT DISTINCT A.[Email]
                  FROM [uUserProfile] A
                  WHERE A.[DataUsed]='A' AND A.[Email] = '" + objuUserProfileTemp.UserEmail + "';";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                string userNameTemp = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, storedProcedureComandTextDetail);
                return userNameTemp;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal string CheckUserName(UserList objUserList)
        {
            try
            {
                var storedProcedureComandTextDetail = @"SELECT DISTINCT A.[UserName]
                FROM [uUserList] A WHERE " +
                " A.[UserName] = '" + objUserList.UserName + "';";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                string userNameTemp = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, storedProcedureComandTextDetail);
                return userNameTemp;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void UpdatePassword(UserList objUserList)
        {
            try
            {
                if (objUserList.UserPassword.ToString().Length < 6)
                {
                    throw new Exception("Passwords are required to be a minimum of 6 characters in length.");

                }

                if (objUserList.UserPassword != objUserList.ConfirmPassword)
                {
                    throw new Exception("The password and confirmation password do not match.");

                }


                if (objUserList.SecurityCode != CheckSecurityCode(objUserList))
                {
                    throw new Exception("The security code and email do not match.");

                }


                var storedProcedureComandText = "exec [uUserPasswordUpdate] '" +
                                        objUserList.UserPassword + "','" +
                                        objUserList.SecurityCode + "'";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string CheckSecurityCode(UserList objUserList)
        {
            try
            {
                string targetSecurityCode = null;
                string sql = @"
                SELECT A.SecurityCode FROM UserSecurityCode A
                INNER JOIN uUserProfile B ON A.UserProfileID = B.UserProfileID
                WHERE B.DataUsed = 'A'  AND A.DataUsed = 'A' AND A.SecurityCodeStatus = 0 AND B.Email = '" + objUserList.UserEmail + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetSecurityCode = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetSecurityCode;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void ChangePassword(UserList objUserList)
        {
            try
            {
                if (objUserList.UserPassword.ToString().Length < 6)
                {
                    throw new Exception("Passwords are required to be a minimum of 6 characters in length.");

                }

                if (objUserList.UserPassword != objUserList.ConfirmPassword)
                {
                    throw new Exception("The password and confirmation password do not match.");

                }


                if (objUserList.CurrentPassword != CheckUserCurrentPassword(objUserList))
                {
                    throw new Exception("The current password do not match.");

                }


                var storedProcedureComandText = "exec [uUserPasswordChange] '" +
                                        objUserList.UserPassword + "','" +
                                        objUserList.UserID + "'";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string CheckSecurityCodeOnline(UserList objUserList)
        {
            try
            {
                string targetSecurityCode = null;
                string sql = @"
                SELECT A.SecurityCode FROM UserSecurityCode A
                INNER JOIN uUserProfileTemp B ON A.UserProfileID = B.UserProfileID
                WHERE B.DataUsed = 'A'  AND A.DataUsed = 'A' AND A.SecurityCodeStatus = 0 AND B.UserEmail = '" + objUserList.UserEmail + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetSecurityCode = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetSecurityCode;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string CheckSecurityCodeRegister(UserList objUserList)
        {
            try
            {
                string targetSecurityCode = null;
                string sql = @"
                SELECT A.SecurityCode FROM UserSecurityCode A
                INNER JOIN uUserProfile B ON A.UserProfileID = B.UserProfileID
                WHERE B.DataUsed = 'A'  AND A.DataUsed = 'A' AND A.SecurityCodeStatus = 0 AND B.Email = '" + objUserList.UserEmail + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetSecurityCode = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetSecurityCode;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string CheckUserCurrentPassword(UserList objUserList)
        {
            try
            {
                string targetCurrentPassword = null;
                string sql = @"SELECT A.[Password] FROM uUserProfile A
                INNER JOIN uUserList B ON A.UserProfileID = B.UserProfileID
                WHERE A.DataUsed = 'A' AND A.UserProfileID= '" + objUserList.UserID + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetCurrentPassword = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetCurrentPassword;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void GetReadyForOnlineUser(UserList objUserList)
        {
            try
            {
                CreateCompanyOnline(objUserList);
                objUserList.UserID = _entryUserName;
                ServeClientNode(objUserList);
                _objCompanySetup = new CompanySetup();
                _objCompanySetup.CompanyID = _companyID;
                //AddValuesForRoleSetup(objUserList);
                ServeOrganizationalChart(objUserList, _objCompanySetup);
                ServeUsersUsersInRoles(objUserList, _objCompanySetup);
                ServeUsersInRelatedRoles(objUserList, _objCompanySetup);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void ServeUsersUsersInRoles(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                var storedProcedureComandText = @" INSERT INTO [uUsersInRoles] ([CompanyID],[UserProfileID],[RoleTypeID],[RoleID],[DataUsed],[EntryUserID],[EntryDate]) VALUES(" +
                                                    objCompanySetup.CompanyID + ",'" +
                                                    objUserList.UserID + "','" +
                                                    "1125002" + "'," +
                                                    1 + ",'" +
                                                    "A" + "','" +
                                                    objUserList.UserID + "'," +
                                                    "CAST(GETDATE() AS DateTime)" + ");";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void ServeUsersInRelatedRoles()
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [uUsersInRelatedRoles] ([CompanyID],[UserId],[RoleID],[DataUsed],[EntryUserID],[EntryDate])
                SELECT DISTINCT " + _companyID + " AS CompanyID,D.UserProfileID, A.RelatedToID,'A' AS DataUsed,D.UserProfileID,CAST(GETDATE() AS DateTime) AS EntryDate " +
                " FROM OnlineUsersInRelatedRoles A " +
                " INNER JOIN OnlineServiceRequestDetails B ON A.ServiceID = B.ServiceID " +
                " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID=C.ServiceRequestID " +
                " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                " WHERE A.DataUsed='A' AND B.DataUsed='A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + _entryUserName + "'" +
                " AND NOT EXISTS(SELECT t2.[RoleID] FROM [uUsersInRelatedRoles] t2  WHERE t2.[RoleID] = A.RelatedToID AND t2.CompanyID = " + _companyID + " AND " +
                " t2.UserId = '" + _entryUserName + "');";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void ServeOrganizationalChart()
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [orgOrganizationElements] ([CompanyID],[OrgElementID],[DataUsed],[EntryUserID],[EntryDate])
                  SELECT DISTINCT " + _companyID + " AS CompanyID,A.[EntityTypeID],'A' AS DataUsed,'" + _entryUserName + "' AS EntryUserID,CAST(GETDATE() AS DateTime) " +
                  " FROM [OnlineOrgOrganizationalChartSetup] A " +
                  " INNER JOIN OnlineServiceRequestDetails B ON A.ServiceID = B.ServiceID " +
                  " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID=C.ServiceRequestID " +
                  " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                  " WHERE A.DataUsed='A' AND B.DataUsed='A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + _entryUserName + "'" +
                  " AND NOT EXISTS(SELECT t2.[EntityID] FROM [orgOrganizationalChartSetup] t2  WHERE t2.[EntityID] = A.[EntityID] AND t2.CompanyID = " + _companyID + ");" +
                @"INSERT INTO [orgOrganizationalChartSetup] ([CompanyID],[ParentEntityID],[EntityID],[EntityTypeID],[AddressTag],[AddressID],[EntityName],[ShortName],[DisplayName]
                 ,[GroupEmailAddress],[HeadID],[EntityCategoryID],[EntityOpeningDate],[DataUsed],[EntryUserID],[EntryDate])
                 SELECT DISTINCT " + _companyID + " AS CompanyID,A.[ParentEntityID],A.[EntityID],A.[EntityTypeID],A.[AddressTag],A.[AddressID],A.[EntityName],A.[ShortName],A.[DisplayName],A.[GroupEmailAddress], " +
                 " A.[HeadID],A.[EntityCategoryID],A.[EntityOpeningDate],'A' AS DataUsed,'" + _entryUserName + "' AS EntryUserID,CAST(GETDATE() AS DateTime) " +
                 " FROM [OnlineOrgOrganizationalChartSetup] A " +
                 " INNER JOIN OnlineServiceRequestDetails B ON A.ServiceID = B.ServiceID " +
                 " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID=C.ServiceRequestID " +
                 " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                 " WHERE A.DataUsed='A' AND B.DataUsed='A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + _entryUserName + "'" +
                 " AND NOT EXISTS(SELECT t2.[EntityID] FROM [orgOrganizationalChartSetup] t2  WHERE t2.[EntityID] = A.[EntityID] AND t2.CompanyID = " + _companyID + ");";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }


        internal void GetReadyForAnotherServices(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                ServeClientNode(objUserList, objCompanySetup);
                AddValuesForRoleSetupAnother(objUserList, objCompanySetup);
                this.ServeOrganizationalChart(objUserList, objCompanySetup);
                this.ServeUsersInRelatedRoles(objUserList, objCompanySetup);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void ServeUsersInRelatedRoles(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [uUsersInRelatedRoles] ([CompanyID],[UserId],[RoleID],[DataUsed],[EntryUserID],[EntryDate])
                SELECT DISTINCT " + objCompanySetup.CompanyID + " AS CompanyID,D.UserProfileID, A.RelatedToID,'A' AS DataUsed,D.UserProfileID,CAST(GETDATE() AS DateTime) AS EntryDate " +
                " FROM OnlineUsersInRelatedRoles A " +
                " INNER JOIN OnlineServiceRequestDetails B ON A.ServiceID = B.ServiceID " +
                " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID=C.ServiceRequestID " +
                " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                " WHERE A.DataUsed='A' AND B.DataUsed='A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + objUserList.UserID + "'" +
                " AND NOT EXISTS(SELECT t2.[RoleID] FROM [uUsersInRelatedRoles] t2  WHERE t2.[RoleID] = A.RelatedToID AND t2.CompanyID = " + objCompanySetup.CompanyID + " AND " +
                " t2.UserId = '" + objUserList.UserID + "');";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void ServeOrganizationalChart(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [orgOrganizationElements] ([CompanyID],[OrgElementID],[DataUsed],[EntryUserID],[EntryDate])
                  SELECT DISTINCT " + objCompanySetup.CompanyID + " AS CompanyID,A.[EntityTypeID],'A' AS DataUsed,'" + objUserList.UserID + "' AS EntryUserID,CAST(GETDATE() AS DateTime) " +
                  " FROM [OnlineOrgOrganizationalChartSetup] A " +
                  " INNER JOIN OnlineServiceRequestDetails B ON A.ServiceID = B.ServiceID " +
                  " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID=C.ServiceRequestID " +
                  " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                  " WHERE A.DataUsed='A' AND B.DataUsed='A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + objUserList.UserID + "'" +
                  " AND NOT EXISTS(SELECT t2.[EntityID] FROM [orgOrganizationalChartSetup] t2  WHERE t2.[EntityID] = A.[EntityID] AND t2.CompanyID = " + objCompanySetup.CompanyID + ");" +
                @"INSERT INTO [orgOrganizationalChartSetup] ([CompanyID],[ParentEntityID],[EntityID],[EntityTypeID],[AddressTag],[AddressID],[EntityName],[ShortName],[DisplayName]
                 ,[GroupEmailAddress],[HeadID],[EntityCategoryID],[EntityOpeningDate],[DataUsed],[EntryUserID],[EntryDate])
                 SELECT DISTINCT " + objCompanySetup.CompanyID + " AS CompanyID,A.[ParentEntityID],A.[EntityID],A.[EntityTypeID],A.[AddressTag],A.[AddressID],A.[EntityName],A.[ShortName],A.[DisplayName],A.[GroupEmailAddress], " +
                 " A.[HeadID],A.[EntityCategoryID],A.[EntityOpeningDate],'A' AS DataUsed,'" + objUserList.UserID + "' AS EntryUserID,CAST(GETDATE() AS DateTime) " +
                 " FROM [OnlineOrgOrganizationalChartSetup] A " +
                 " INNER JOIN OnlineServiceRequestDetails B ON A.ServiceID = B.ServiceID " +
                 " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID=C.ServiceRequestID " +
                 " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                 " WHERE A.DataUsed='A' AND B.DataUsed='A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + objUserList.UserID + "'" +
                 " AND NOT EXISTS(SELECT t2.[EntityID] FROM [orgOrganizationalChartSetup] t2  WHERE t2.[EntityID] = A.[EntityID] AND t2.CompanyID = " + objCompanySetup.CompanyID + ");";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }


        private void AddValuesForRoleSetup(UserList objUserList)
        {
            try
            {
                _objUserPermission = new UserPermission();
                _objUserPermissionController = new UserPermissionController();
                _objUserPermission.RoleID = _objUserPermissionController.GetRoleID();
                var storedProcedureComandText = "INSERT INTO [uRoleSetup] ([CompanyID],[RoleID],[RoleTypeID],[RoleName],[DataUsed],[EntryUserID],[EntryDate]) VALUES ( " +
                                                 _companyID + "," +
                                                 _objUserPermission.RoleID + ",'" +
                                                 "1125001" + "','" +
                                                  " " + "','" +
                                                 "A" + "', '" +
                                                 _entryUserName + "'," +
                                                 "CAST(GETDATE() AS DateTime));";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

                var storedProcedureComandTextNodes = @"INSERT INTO [uRoleSetupDetails] ([RoleID],[NodeID],[DataUsed],[EntryUserID],[EntryDate])
                    SELECT DISTINCT " + _objUserPermission.RoleID + " ,A.NodeTypeID,'A','" + _entryUserName + "',CAST(GETDATE() AS DateTime)" +
                    " FROM OnlineServiceRequestNodes A " +
                    " INNER JOIN OnlineServiceRequestDetails B ON A.FeatureDetailsID = B.FeatureDetailsID " +
                    " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID = C.ServiceRequestID " +
                    " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                    " WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserEmail = '" + objUserList.UserEmail + "'";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextNodes);
                AddValuesForUserRole(objUserList, _objUserPermission);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private void AddValuesForRoleSetupAnother(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                _objUserPermission = new UserPermission();
                _objUserPermissionController = new UserPermissionController();
                _objUserPermission.RoleID = _objUserPermissionController.GetRoleID();
                var storedProcedureComandText = "INSERT INTO [uRoleSetup] ([CompanyID],[RoleID],[RoleTypeID],[RoleName],[DataUsed],[EntryUserID],[EntryDate]) VALUES ( " +
                                                 objCompanySetup.CompanyID + "," +
                                                 _objUserPermission.RoleID + ",'" +
                                                 "1125001" + "','" +
                                                  " " + "','" +
                                                 "A" + "', '" +
                                                 objUserList.UserID + "'," +
                                                 "CAST(GETDATE() AS DateTime));";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

                var storedProcedureComandTextNodes = @"INSERT INTO [uRoleSetupDetails] ([RoleID],[NodeID],[DataUsed],[EntryUserID],[EntryDate])
                    SELECT DISTINCT " + _objUserPermission.RoleID + " AS RoleID,A.NodeTypeID,'A' AS DataUsed,'" + objUserList.UserID + "' AS EntryUserID,CAST(GETDATE() AS DateTime) AS EntryDate " +
                " FROM OnlineServiceRequestNodes A " +
                " INNER JOIN OnlineServiceRequestDetails B ON A.FeatureDetailsID = B.FeatureDetailsID " +
                " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID = C.ServiceRequestID " +
                " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                " WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + objUserList.UserID + "'" +
                " AND NOT EXISTS(SELECT DISTINCT t3.NodeID FROM uRoleSetup t2 " +
                " INNER JOIN uRoleSetupDetails t3 ON t2.RoleID = t3.RoleID WHERE t2.DataUsed = 'A' AND t3.DataUsed = 'A' AND t2.CompanyID = " + objCompanySetup.CompanyID + " AND t3.NodeID = A.[NodeTypeID]);";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandTextNodes);
                AddValuesUserRoleAnother(objUserList, _objUserPermission, objCompanySetup);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void AddValuesForUserRole(UserList objUserList, UserPermission objUserPermission)
        {
            try
            {
                _objUserPermission = new UserPermission();
                List<int> listRole = new List<int>();
                listRole.Add(Convert.ToInt32(objUserPermission.RoleID));
                _objUserPermission.roleList = listRole;
                _objUserPermission.RoleType = "1125001";
                EmployeeSetup objEmployeeSetup = new EmployeeSetup();

                UserProfile objUserProfile = new UserProfile();
                UserProfileController objUserProfileController = new UserProfileController();
                objUserProfile.UserIdentifierID = objUserList.UserName;
                objEmployeeSetup.EmployeeID = _entryUserName;
                objEmployeeSetup.CompanyID = _companyID;
                objEmployeeSetup.EntryUserName = _entryUserName;
                _objUserPermissionController = new UserPermissionController();

                List<int> listRelatedUserRole = new List<int>();
                //foreach (ListItem item in ListBoxSelectedRelatedUserRole.Items)
                //{
                //    listRelatedUserRole.Add(Convert.ToInt32(item.Value.ToString()));
                //}
                _objUserPermission.RelatedUserRoleList = listRelatedUserRole;
                _objUserPermissionController.SaveUserRole(objEmployeeSetup, _objUserPermission);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private void AddValuesUserRoleAnother(UserList objUserList, UserPermission objUserPermission, CompanySetup objCompanySetup)
        {
            try
            {
                _objUserPermission = new UserPermission();
                List<int> listRole = new List<int>();
                listRole.Add(Convert.ToInt32(objUserPermission.RoleID));
                _objUserPermission.roleList = listRole;
                _objUserPermission.RoleType = "1125001";
                EmployeeSetup objEmployeeSetup = new EmployeeSetup();

                UserProfile objUserProfile = new UserProfile();
                UserProfileController objUserProfileController = new UserProfileController();
                objUserProfile.UserIdentifierID = objUserList.UserName;
                objEmployeeSetup.EmployeeID = objUserList.UserID;
                objEmployeeSetup.CompanyID = objCompanySetup.CompanyID;
                objEmployeeSetup.EntryUserName = objUserList.UserID;
                _objUserPermissionController = new UserPermissionController();

                List<int> listRelatedUserRole = new List<int>();
                //foreach (ListItem item in ListBoxSelectedRelatedUserRole.Items)
                //{
                //    listRelatedUserRole.Add(Convert.ToInt32(item.Value.ToString()));
                //}
                _objUserPermission.RelatedUserRoleList = listRelatedUserRole;
                _objUserPermissionController.SaveUserRole(objEmployeeSetup, _objUserPermission);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void ServeClientNode(UserList objUserList)
        {
            try
            {
                _companyID = GetUserCompanyIDonline();
                var storedProcedureComandText = @"INSERT INTO [uDefaultNodeList] ([CompanyID],[BranchID],[ActivityID],[ActivityName],[SeqNo],[NodeTypeID],[PNodeTypeID],[FormDescription],[FormName]
                ,[ShowPosition],[DataUsed],[EntryUserID],[EntryDate],[UserID])
                SELECT DISTINCT " + _companyID + " AS CompanyID,1 AS BranchID,A.ActivityID,A.ActivityName,A.SeqNo,A.NodeTypeID,A.PNodeTypeID,A.FormDescription,A.FormName,A.ShowPosition,'A','" + objUserList.UserID + "',CAST(GETDATE() AS DateTime),'" + objUserList.UserName + "'" +
                " FROM OnlineServiceRequestNodes A " +
                " INNER JOIN OnlineServiceRequestDetails B ON A.FeatureDetailsID = B.FeatureDetailsID " +
                " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID = C.ServiceRequestID " +
                " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                " WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserEmail = '" + objUserList.UserEmail + "'";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);


            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private void ServeClientNode(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                var storedProcedureComandText = @"INSERT INTO [uDefaultNodeList] ([CompanyID],[BranchID],[ActivityID],[ActivityName],[SeqNo],[NodeTypeID],[PNodeTypeID],[FormDescription],[FormName]
                ,[ShowPosition],[DataUsed],[EntryUserID],[EntryDate],[UserID])
                SELECT DISTINCT " + objCompanySetup.CompanyID + " AS CompanyID,1 AS BranchID,A.ActivityID,A.ActivityName,A.SeqNo,A.NodeTypeID,A.PNodeTypeID,A.FormDescription,A.FormName,A.ShowPosition,'A' AS DataUsed,'" + objUserList.UserID + "' AS EntryUserID,CAST(GETDATE() AS DateTime) AS EntryDate,'" + objUserList.UserName + "' AS UserID" +
                " FROM OnlineServiceRequestNodes A " +
                " INNER JOIN OnlineServiceRequestDetails B ON A.FeatureDetailsID = B.FeatureDetailsID " +
                " INNER JOIN OnlineServiceRequest C ON B.ServiceRequestID = C.ServiceRequestID " +
                " INNER JOIN uUserProfileTemp D ON C.UserProfileID = D.UserProfileID " +
                " WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed='A' AND D.UserProfileID = '" + objUserList.UserID + "'" +
                " AND NOT EXISTS(SELECT [NodeTypeID] " +
                " FROM [uDefaultNodeList] t2 " +
                " WHERE t2.[NodeTypeID] = A.[NodeTypeID] AND t2.CompanyID = " + objCompanySetup.CompanyID + ");";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);


            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }


        private int GetUserCompanyIDonline()
        {
            try
            {
                CompanySetupController objCompanySetupController = new CompanySetupController();
                UserProfile objUserProfile = new UserProfile();
                objUserProfile.UserProfileID = _entryUserName;
                return objCompanySetupController.GetCompanyID(objUserProfile);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void CreateCompanyOnline(UserList objUserList)
        {
            try
            {
                objUserProfileOnline = new UserProfileOnline();
                _objUserProfileOnlineController = new UserProfileOnlineController();
                objUserProfileOnline.DtUserProfileOnline = _objUserProfileOnlineController.GetUserProfileOnline(objUserList);
                foreach (DataRow rowNo in objUserProfileOnline.DtUserProfileOnline.Rows)
                {
                    UserProfile _objUserProfile = new UserProfile();
                    _objUserProfile.Title = rowNo["UserTitle"].ToString();
                    _objUserProfile.FullName = rowNo["UserName"].ToString();
                    _objUserProfile.Email = rowNo["UserEmail"].ToString();
                    _objUserProfile.UserProfileTypeID = "1";
                    _objUserProfile.DisplayName = rowNo["UserName"].ToString();
                    _objUserProfile.EntryUserName = rowNo["UserProfileID"].ToString();
                    _objUserProfile.UserProfileID = rowNo["UserProfileID"].ToString();
                    _objUserProfile.MobilePIN = rowNo["MobileNumber"].ToString();
                    _objUserProfileOnlineController.Save(_objUserProfile);


                    _objCompanySetupController = new CompanySetupController();
                    _objCompanyDetailsSetup = new CompanyDetailsSetup();
                    _objCompanyDetailsSetup.CountryID = 1;
                    _objCompanyDetailsSetup.CompanyName = rowNo["CompanyName"].ToString();
                    _objCompanyDetailsSetup.CompanyEmail = rowNo["CompanyEmail"].ToString();
                    _objCompanyDetailsSetup.CompanyMobile = null;
                    _objCompanyDetailsSetup.CompanyLogo = null;
                    _objCompanyDetailsSetup.EntryUserName = rowNo["UserProfileID"].ToString();
                    _entryUserName = rowNo["UserProfileID"].ToString();
                    _objCompanySetupController.Save(_objCompanyDetailsSetup);
                }
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            if (originalPassword == "")
            {
                return "";
            }
            else
                return BitConverter.ToString(encodedBytes);
        }
    }
}