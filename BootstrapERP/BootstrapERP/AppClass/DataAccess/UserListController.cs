using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BootstrapERP.AppClass.DataAccess
{
    public class UserListController : DataAccessBase
    {
        internal void Update(UserList objUserList)
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

                //if (CheckUserNameWithCompany(objUserList) > 0)
                //{
                //    throw new Exception("User name already exist ");

                //}

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

        internal DataTable ShowRegistration(UserList objUserList)
        {
            try
            {
                DataTable dtInformation = new DataTable();
                string sqlString = @"SELECT DISTINCT A.UserProfileID,(B.FirstName+' '+B.MiddleName+' '+ B.LastName) AS FullName, B.UserProfileTypeID,B.[MobilePIN],B.[Email], A.UserName FROM uUserList A
                INNER JOIN [uUserProfile] B ON A.UserProfileID = B.UserProfileID
                WHERE B.DataUsed = 'A' AND B.Email = '" + objUserList.UserEmail + "';";
                dtInformation = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtInformation;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void SuspendUser(UserProfile objUserProfile)
        {
            try
            {
                var storedProcedureComandText = @"UPDATE [uUserProfile]
                   SET 
                      [DataUsed] = ISNULL('" + "I" + "',[DataUsed])"
                      + " ,[LastUpdateDate] = ISNULL(GETDATE(),[LastUpdateDate])"
                      + " ,[LastUpdateUserID] = ISNULL('" + objUserProfile.EntryUserName + "',[LastUpdateUserID])"
                      + " WHERE [UserProfileID] = '" + objUserProfile.UserProfileID + "';";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetAllActiveUser()
        {
            try
            {
                DataTable dtUser = null;
                string sqlString = @" SELECT A.UserIdentifier,A.EntryDate,A.UserProfileID, A.Email FROM [uUserProfile] A WHERE A.DataUsed = 'A' AND A.[Password] != '111'  
                ORDER BY A.UserIdentifier";
                dtUser = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtUser;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private int CheckUserName(UserList objUserList)
        {
            try
            {
                int countUserProfileId = 0;
                string sqlString = "SELECT ISNULL( COUNT( UserProfileID),0) FROM uUserList WHERE UserName = '" + objUserList.UserName + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                countUserProfileId = objclsDataManipulation.GetSingleValue(this.ConnectionString, sqlString);
                return countUserProfileId;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private int CheckUserNameWithCompany(UserList objUserList)
        {
            try
            {
                int countUserProfileId = 0;
                string sqlString = "  SELECT ISNULL( COUNT( A.UserProfileID),0) FROM uUserList A INNER JOIN uUserProfile B ON A.UserProfileID = B.UserProfileID " +
                   " INNER JOIN UserSecurityCode C ON B.SecurityCode = C.SecurityCode WHERE B.DataUsed = 'A' AND C.DataUsed = 'A' AND " +
                   " C.CompanyID =(SELECT CompanyID FROM UserSecurityCode WHERE SecurityCode = " + objUserList.SecurityCode + ") AND A.UserName = '" + objUserList.UserName + "' ";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                countUserProfileId = objclsDataManipulation.GetSingleValue(this.ConnectionString, sqlString);
                return countUserProfileId;

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
                WHERE B.DataUsed = 'A' AND B.IsApproved = 1 AND B.IsLockedOut = 0 AND A.DataUsed = 'A' AND A.SecurityCodeStatus = 0 AND B.Email = '" + objUserList.UserEmail + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetSecurityCode = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetSecurityCode;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }



        internal DataTable GetLoginUserInformation(UserList objUserList)
        {
            try
            {
                DataTable dtInformation = new DataTable();
                string sqlString = @"SELECT A.UserProfileID,C.CompanyID,C.EmployeeID,D.FullName FROM uUserList A 
                INNER JOIN uUserProfile B 
                ON A.UserProfileID = B.UserProfileID
                INNER JOIN UserSecurityCode C ON B.SecurityCode = C.SecurityCode
                INNER JOIN hrEmployeeSetup D ON C.CompanyID = D.CompanyID AND C.EmployeeID = D.EmployeeID 
                WHERE C.DataUsed = 'A' AND A.UserName = '" + objUserList.UserName + "' AND B.[Password] = '" + objUserList.UserPassword + "'";
                dtInformation = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtInformation;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetLoginUserInformation(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                DataTable dtInformation = new DataTable();
                string sqlString = @"SELECT A.UserProfileID,C.CompanyID,C.EmployeeID,D.FullName FROM uUserList A 
                INNER JOIN uUserProfile B 
                ON A.UserProfileID = B.UserProfileID
                INNER JOIN UserSecurityCode C ON B.SecurityCode = C.SecurityCode
                INNER JOIN hrEmployeeSetup D ON C.CompanyID = D.CompanyID AND C.EmployeeID = D.EmployeeID 
                WHERE C.DataUsed = 'A' AND A.UserName = '" + objUserList.UserName + "' AND B.[Password] = '" + objUserList.UserPassword + "' AND C.CompanyID = " + objCompanySetup.CompanyID + "";
                dtInformation = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtInformation;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetLoginUserProfile(UserList objUserList, CompanySetup objCompanySetup)
        {
            try
            {
                DataTable dtInformation = new DataTable();
                string sqlString = @"SELECT DISTINCT A.UserProfileID,(B.FirstName+' '+B.MiddleName+' '+ B.LastName) AS FullName, B.UserProfileTypeID,B.[MobilePIN],B.[Email] FROM uUserList A
                INNER JOIN [uUserProfile] B ON A.UserProfileID = B.UserProfileID
                INNER JOIN uUsersInRoles C ON A.UserProfileID = C.UserProfileID
                WHERE B.DataUsed = 'A'" +
                " AND ( A.UserName = '" + objUserList.UserName + "' OR B.Email = '" + objUserList.UserName + "' OR B.MobilePIN = '" + objUserList.UserName + "') " +
                " AND B.[Password]='" + objUserList.UserPassword + "' AND C.CompanyID=" + objCompanySetup.CompanyID + "";
                dtInformation = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtInformation;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetAssignCompany(UserList objUserList)
        {
            try
            {
                DataTable dtUserCompanyList = new DataTable();
                string sqlString = @"SELECT DISTINCT E.CompanyID,E.CompanyName FROM uUserList A 
                                INNER JOIN uUserProfile B ON A.UserProfileID = B.UserProfileID
                                INNER JOIN UserSecurityCode C ON B.SecurityCode = C.SecurityCode
                                INNER JOIN uUsersInRoles D ON C.EmployeeID = D.UserId
                                INNER JOIN [comCompanySetup] E ON D.CompanyID = E.CompanyID
                WHERE B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed = 'A' AND E.DataUsed = 'A' AND A.UserName = '" + objUserList.UserName + "'";
                dtUserCompanyList = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtUserCompanyList;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetAssignCompanyUpdate(UserList objUserList)
        {
            try
            {
                DataTable dtUserCompanyList = new DataTable();
                string sqlString = @"SELECT DISTINCT D.CompanyID,D.CompanyName FROM uUserList A 
                 INNER JOIN uUserProfile B ON A.UserProfileID = B.UserProfileID 
                 INNER JOIN uUsersInRoles C ON B.UserProfileID = C.UserProfileID
                 INNER JOIN [comCompanySetup] D ON C.CompanyID = D.CompanyID
                 WHERE B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed = 'A' "+
                " AND ( A.UserName = '" + objUserList.UserName + "' OR B.Email = '" + objUserList.UserName + "' OR B.MobilePIN = '" + objUserList.UserName + "')";
                dtUserCompanyList = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtUserCompanyList;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void LoadCompanyDDL(DataTable dtAssignCompany, DropDownList ddlCompany)
        {
            try
            {
                ClsDropDownListController.LoadDropDownListFromDataTable(dtAssignCompany, ddlCompany, "CompanyName", "CompanyID");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal string GetUseID(UserProfile objUserProfile)
        {
            try
            {
                string targetUseID = null;
                string sql = @"SELECT UserID FROM uUserList WHERE UserProfileID = '" + objUserProfile.UserProfileID + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetUseID = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetUseID;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void CheckUser(UserList _objUserList)
        {
            try
            {
                _objUserList.IsUser = this.CheckUserName(_objUserList);
                if (_objUserList.IsUser == 0)
                {
                    throw new Exception("Wrong user name. Try again.");

                }
                else
                {
                    throw new Exception("Access is denied.");
                }

            }
            catch (Exception msgException)
            {
                
                throw msgException;
            }
        }
    }
}