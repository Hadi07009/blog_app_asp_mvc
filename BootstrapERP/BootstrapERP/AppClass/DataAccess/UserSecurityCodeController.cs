using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Controllers;
using BootstrapERP.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class UserSecurityCodeController:DataAccessBase
    {
        //internal void LoadUserForSecurityCode(CompanySetup objCompanySetup, GridView GridViewUsers)
        //{
        //    try
        //    {
        //        DataTable dtUser = GetUserForSecurityCode(objCompanySetup);
        //        GridViewUsers.DataSource = null;
        //        GridViewUsers.DataBind();
        //        if (dtUser.Rows.Count > 0)
        //        {
        //            GridViewUsers.DataSource = dtUser;
        //            GridViewUsers.DataBind();

        //        }

        //    }
        //    catch (Exception msgException)
        //    {

        //        throw msgException;
        //    }
        //}

        private DataTable GetUserForSecurityCode(CompanySetup objCompanySetup)
        {
            try
            {
                DataTable dtUser = null;
                string sqlString = @"SELECT A.EmployeeID,A.FullName,A.EntryDate,NULL AS DateOfCode FROM [hrEmployeeSetup] A WHERE NOT EXISTS ( SELECT B.EmployeeID FROM UserSecurityCode B 
                WHERE A.CompanyID = B.CompanyID AND A.EmployeeID = B.EmployeeID ) AND A.UserPermission = 1 AND A.Email != '' AND A.CompanyID = " + objCompanySetup.CompanyID + "";
                sqlString += @" UNION
                SELECT A.EmployeeID,A.FullName,A.EntryDate AS EntryDate ,B.EntryDate AS DateOfCode FROM [hrEmployeeSetup] A 
                INNER JOIN UserSecurityCode B ON A.CompanyID = B.CompanyID AND A.EmployeeID = B.EmployeeID WHERE 
                B.SecurityCodeStatus = 0 AND B.DataUsed = 'A' AND A.CompanyID = " + objCompanySetup.CompanyID + "";
                sqlString += @" ORDER BY A.EntryDate";
                dtUser = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtUser;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void SendSecurityCode(CompanySetup objCompanySetup, UserSecurityCode objUserSecurityCode, UserProfile objUserProfile, CompanyDetailsSetup objCompanyDetailsSetup)
        {
            try
            {
                Save(objCompanySetup, objUserSecurityCode);
                SendSecurityCodeByMail(objCompanySetup, objUserSecurityCode, objUserProfile, objCompanyDetailsSetup);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void SendSecurityCode(CompanySetup objCompanySetup, UserSecurityCode objUserSecurityCode, UserProfile objUserProfile)
        {
            try
            {
                Save(objCompanySetup, objUserSecurityCode);
                UserProfileHomeController objUserProfileHomeController = new UserProfileHomeController();
                objUserSecurityCode.SecurityCode = GetSecurityCode(objUserSecurityCode);
                this.SendSecurityCodeByMail(1, objUserSecurityCode.SecurityCode, objUserProfile.Email);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public void SendSecurityCodeByMail(int companyID, int securityCode, string userEmail)
        {
            try
            {
                if (securityCode != 0)
                {
                    MailServiceSetup objMailServiceSetup = new MailServiceSetup();

                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    Thank you for choosing our service. We are aware about privacy and security of our customer data. <br /><br />

                    Your security code is: " + securityCode + "" + "      " +

                  @"<br /><br />
                    Please go to <a href='http://27.147.194.136:82/HomePageRegister/UserRegister'>Register</a><br />
                    Click Register at the right top corner and enter the security code along with a unique user name, your desired password and email address.<br /><br />

                    We wish you to experience an excellent journey in using our business solution.<br /><br />


                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address > ";


                    objMailServiceSetup.EmailTo = userEmail;
                    objMailServiceSetup.MailtypeID = "1";
                    ArrayList attachDocument = new ArrayList();
                    objMailServiceSetup.AttachItem = attachDocument;
                    MailServiceController objMailServiceController = new MailServiceController();
                    objMailServiceController.eMailSendServiceHTML(companyID, objMailServiceSetup);

                    //objMailServiceSetup.MailBody = @"
                    //Dear Sir,<br /><br />

                    //Thank you for choosing our service.<br /><br />

                    //< Mr. user name > has requested the service on behalf of <Company Name>.<br /><br />

                    //If you have any query regarding this request, please contact us.<br /><br />

                    //Thanking you,<br /><br />

                    //The Business Solution Team | Help Line: < Contact No > | < Email address >";
                    //objMailServiceSetup.EmailTo = companyEmail;
                    //objMailServiceSetup.MailtypeID = "1";
                    //objMailServiceSetup.AttachItem = attachDocument;
                    //objMailServiceController.eMailSendServiceHTML(companyID, objMailServiceSetup);

                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void SendSecurityCodeByMail(CompanySetup objCompanySetup, UserSecurityCode objUserSecurityCode, UserProfile objUserProfile, CompanyDetailsSetup objCompanyDetailsSetup)
        {
            try
            {
                objUserSecurityCode.SecurityCode = GetSecurityCode(objUserSecurityCode);
                if (objUserSecurityCode.SecurityCode != 0)
                {
                    MailServiceSetup objMailServiceSetup = new MailServiceSetup();

                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    Thank you for choosing our service. We are aware about privacy and security of our customer data. <br /><br />

                    Your security code is: " + objUserSecurityCode.SecurityCode + "" + "      " +

                  @"<br /><br />
                    Please go to <a href='http://27.147.194.138/erp/HomePageLogoForm.aspx'>Register</a><br />
                    Click Register at the right top corner and enter the security code along with a unique user name, your desired password and email address.<br /><br />

                    We wish you to experience an excellent journey in using our business solution.<br /><br />


                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address > ";


                    objMailServiceSetup.EmailTo = objUserProfile.Email;
                    objMailServiceSetup.MailtypeID = "1";
                    ArrayList attachDocument = new ArrayList();
                    objMailServiceSetup.AttachItem = attachDocument;
                    MailServiceController objMailServiceController = new MailServiceController();
                    //objMailServiceController.eMailSendServiceHTML(objCompanySetup, objMailServiceSetup);

                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />

                    Thank you for choosing our service.<br /><br />

                    < Mr. user name > has requested the service on behalf of <Company Name>.<br /><br />

                    If you have any query regarding this request, please contact us.<br /><br />

                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address >";
                    objMailServiceSetup.EmailTo = objCompanyDetailsSetup.CompanyEmail;
                    objMailServiceSetup.MailtypeID = "1";
                    objMailServiceSetup.AttachItem = attachDocument;
                    //objMailServiceController.eMailSendServiceHTML(objCompanySetup, objMailServiceSetup);

                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private void SendSecurityCodeByMail(CompanySetup objCompanySetup, UserSecurityCode objUserSecurityCode, UserProfile objUserProfile)
        {
            try
            {
                objUserSecurityCode.SecurityCode = GetSecurityCode(objUserSecurityCode);
                if (objUserSecurityCode.SecurityCode != 0)
                {
                    MailServiceSetup objMailServiceSetup = new MailServiceSetup();
                    //objMailServiceSetup.MailBody = "Your security code is: " + objUserSecurityCode.SecurityCode + "";
                    objMailServiceSetup.MailBody = @"Dear Sir,

                                                    Thank you for choosing our service. We are aware about privacy and security of our customer data. 
                                                    Your security code is: " + objUserSecurityCode.SecurityCode + "" +

                                                  @"Please go to < URL >, Click Register at the right top corner and enter the security code along with a unique user name, your desired password and email address.

                                                    We wish you to experience an excellent journey in using our business solution.


                                                    Thanking you,

                                                    The Business Solution Team | Help Line: < Contact No > | < Email address >";
                    objMailServiceSetup.EmailTo = objUserProfile.Email;
                    objMailServiceSetup.MailtypeID = "1";
                    ArrayList attachDocument = new ArrayList();
                    objMailServiceSetup.AttachItem = attachDocument;
                    MailServiceController objMailServiceController = new MailServiceController();
                    //objMailServiceController.eMailSendService(objCompanySetup, objMailServiceSetup);
                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private int GetSecurityCode(UserSecurityCode objUserSecurityCode)
        {
            try
            {
                string sql = "SELECT [SecurityCode] FROM [UserSecurityCode] WHERE DataUsed = 'A' AND SecurityCodeStatus = 0 AND UserProfileID = '" + objUserSecurityCode.UserKnownID + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                objUserSecurityCode.SecurityCode = objclsDataManipulation.GetSingleValue(this.ConnectionString, sql);
                return objUserSecurityCode.SecurityCode;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private void Save(CompanySetup objCompanySetup, UserSecurityCode objUserSecurityCode)
        {
            try
            {
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                objUserSecurityCode.SecurityCode = Convert.ToInt32(objclsDataManipulation.GetAnUniqueidentifierSecurityCode(this.ConnectionString));
                var storedProcedureComandText = "exec [spInitiateSecurityCode] " +
                                         "'" + objUserSecurityCode.UserKnownID + "','" +
                                        objCompanySetup.EntryUserName + "'," +
                                        objUserSecurityCode.SecurityCode + "";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        //internal void LoadUserForSecurityCode(GridView GridViewUsers)
        //{
        //    try
        //    {
        //        DataTable dtUser = GetUserForSecurityCode();
        //        GridViewUsers.DataSource = null;
        //        GridViewUsers.DataBind();
        //        if (dtUser.Rows.Count > 0)
        //        {
        //            GridViewUsers.DataSource = dtUser;
        //            GridViewUsers.DataBind();

        //        }

        //    }
        //    catch (Exception msgException)
        //    {

        //        throw msgException;
        //    }
        //}

        public DataTable GetUserForSecurityCode()
        {
            try
            {
                DataTable dtUser = null;
                string sqlString = @"SELECT A.UserProfileID, (A.FirstName +' '+ A.MiddleName+' ' +A.LastName) AS FullName,A.Email, A.EntryDate, NULL AS DateOfCode 
                FROM [uUserProfile] A WHERE NOT EXISTS ( SELECT B.UserProfileID FROM UserSecurityCode B WHERE B.UserProfileID = A.UserProfileID  )
                AND A.DataUsed = 'A' AND A.SecurityCode IS NULL OR A.SecurityCode = ''
                 UNION
                 SELECT A.UserProfileID, (A.FirstName +' '+ A.MiddleName+' ' +A.LastName) AS FullName,A.Email, A.EntryDate, B.EntryDate AS DateOfCode 
                 FROM [uUserProfile] A  INNER JOIN UserSecurityCode B ON A.UserProfileID = B.UserProfileID WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND A.SecurityCode 
                 IS NULL OR A.SecurityCode = '' AND B.SecurityCodeStatus = 0 ";
                sqlString += @" ORDER BY A.EntryDate";
                dtUser = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtUser;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}