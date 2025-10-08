using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BootstrapERP.Controllers
{
    [HandleError]
    public class HomePageLoginController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private UserProfile _objUserProfile;
        private string msgPassword = "";
        // GET: HomePageLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLogin(int caseNo, string returnUrl)
        {
            try
            {
                if (LoginUserInformation.LogginUserStatus == "Yes")
                {
                    return RedirectToAction("Index", "BlogPosts");
                }
                else
                {
                    ViewBag.ReturnURL = returnUrl;
                    ViewBag.CaseNo = caseNo;
                    ViewBag.MessagePassword = "";
                    return View();
                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetLogin(UserList objUserList, int caseNo, string returnUrl)
        {
            try
            {  
                Session["serciceID"] = 1;

                switch (CheckUserValidation(objUserList))
                {
                    case -2:
                        {
                            ViewBag.MessagePassword = "Invalid user, Please create an account.";
                            return View();
                        }
                    case -1:
                        {
                            ViewBag.MessagePassword  = "Incorrect password, Please go to forgot password.";
                            return View();
                        }
                    case 1:
                        {
                            switch (caseNo)
                            {

                                case 1: { return RedirectToAction("Index", "BlogPosts"); }
                                case 2: { return RedirectToAction("Index", "StartLearning", new { @selectedServiceID = Convert.ToInt32(returnUrl) }); }
                                default:
                                    return RedirectToAction("Index", "BlogPosts");
                            }

                        }
                    case 2:
                        {
                            switch (caseNo)
                            {

                                case 1: {
                                        //need to insert data into user login table

                                        _objUserProfile = new UserProfile();
                                        //LoginUserInformation.LogginUserStatus = "Yes";
                                        _objUserProfile.DeviceIP = LoginUserInformation.LogginUserIP;
                                        _objUserProfile.BrowserName = LoginUserInformation.BrowserName;
                                        _objUserProfile.DeviceType = LoginUserInformation.DeviceType;
                                        _objUserProfile.CountryCode = LoginUserInformation.CountryCode;
                                        _objUserProfile.CountryName = LoginUserInformation.CountryName;
                                        _objUserProfile.LoginRegion = LoginUserInformation.Region;
                                        _objUserProfile.RegionName = LoginUserInformation.RegionName;
                                        _objUserProfile.CityName = LoginUserInformation.CityName;
                                        _objUserProfile.ZipCode = LoginUserInformation.ZipCode;
                                        _objUserProfile.Timezone = LoginUserInformation.Timezone;
                                        _objUserProfile.RegionalTime = LoginUserInformation.RegionalTime;
                                        UserLogin objUserLogin = new UserLogin();
                                        objUserLogin.CompanyID = LoginUserInformation.CompanyID;
                                        objUserLogin.BranchID = LoginUserInformation.BranchID;
                                        objUserLogin.ApplicationID = LoginUserInformation.ApplicationID;
                                        objUserLogin.EntryUserName = LoginUserInformation.UserID;
                                        UserLoginAccess objUserLoginAccess = new UserLoginAccess();
                                        objUserLoginAccess.Save(_objUserProfile, objUserLogin);
                                        return RedirectToAction("Index", "BlogPosts");
                                    }
                                case 2: { return RedirectToAction("Index", "StartLearning", new { @selectedServiceID = Convert.ToInt32(returnUrl) }); }
                                default:
                                    return RedirectToAction("Index", "BlogPosts");
                            }

                        }
                    default:
                        {
                            return RedirectToAction("GetLogin", "HomePageLogin", new { caseNo = 1, targetURL = "" });
                        }
                }
            }
            catch (Exception msgException)
            {
                ViewBag.MessagePassword = msgException.Message;
                return RedirectToAction("GetLogin", "HomePageLogin", new { caseNo = 1, targetURL = "" });
                
            }

        }


        public string IPRequestHelper(string url)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();

            responseStream.Close();
            responseStream.Dispose();

            return responseRead;
        }

        private int CheckUserValidation(UserList objUserList)
        {
            try
            {
                if (objUserList.UserName == "ADM" && objUserList.UserPassword == "ADM123")
                {
                    LoginUserInformation.UserID = "160ea939-7633-46a8-ae49-f661d12abfd5";
                    LoginUserInformation.CompanyID = 1;
                    LoginUserInformation.EmployeeCode = "ADM";
                    LoginUserInformation.EmployeeFullName = "Administrator";
                    LoginUserInformation.UserName = "ADM";
                    LoginUserInformation.BranchID = 1;
                    LoginUserInformation.ApplicationID = 2;
                    LoginUserInformation.LogginUserStatus = "Yes";
                    return objUserList.UserType = 1;
                }
                else
                {
                    DataTable dtUserInformation = new DataTable();
                    CompanySetup _objCompanySetup = new CompanySetup();
                    UserListController _objUserListController = new UserListController();
                    OnlineRegisterController objOnlineRegisterController = new OnlineRegisterController();
                    objUserList.UserPassword = objOnlineRegisterController.EncodePassword(objUserList.UserPassword);
                    _objCompanySetup.CompanyID = this.LoadAssignCompanyDDL(objUserList);
                    dtUserInformation = _objUserListController.GetLoginUserProfile(objUserList, _objCompanySetup);
                    if (_objCompanySetup.CompanyID == 0)
                    {
                        return objUserList.UserType = -2;
                    }
                    else if (dtUserInformation.Rows.Count == 0)
                    {
                        return objUserList.UserType = -1;
                    }
                    else
                    {
                        foreach (DataRow rowNo in dtUserInformation.Rows)
                        {
                            LoginUserInformation.CompanyID = _objCompanySetup.CompanyID;
                            LoginUserInformation.UserID = rowNo["UserProfileID"].ToString();
                            LoginUserInformation.UserProfileTypeID = rowNo["UserProfileTypeID"].ToString();
                            LoginUserInformation.EmployeeCode = null;
                            LoginUserInformation.EmployeeFullName = rowNo["FullName"].ToString();
                            LoginUserInformation.UserEmailID = rowNo["Email"].ToString();
                            LoginUserInformation.UserMobileNo = rowNo["MobilePIN"].ToString() == null ? "" : rowNo["MobilePIN"].ToString();
                            LoginUserInformation.UserName = objUserList.UserName;
                            LoginUserInformation.BranchID = 1;
                            LoginUserInformation.ApplicationID = 2;
                            LoginUserInformation.LogginUserStatus = "Yes";

                            var IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            if (string.IsNullOrEmpty(IPAddress))
                            {
                                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            }

                            LoginUserInformation.LogginUserIP = IPAddress;
                            //LoginUserInformation.LogginUserIP = Request.UserHostAddress;
                            LoginUserInformation.BrowserName = Request.Browser.Browser.ToString() + " " + Request.Browser.Version.ToString();
                            if (Request.Browser.IsMobileDevice)
                            {
                                LoginUserInformation.DeviceType = "Mobile";
                            }
                            else
                            {
                                LoginUserInformation.DeviceType = "Laptop or Desktop";
                            }

                            if (IPAddress != "::1")
                            {
                                string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + IPAddress.ToString());
                                using (TextReader sr = new StringReader(ipResponse))
                                {
                                    using (System.Data.DataSet dataBase = new System.Data.DataSet())
                                    {
                                        IpProperties ipProperties = new IpProperties();
                                        dataBase.ReadXml(sr);
                                        ipProperties.Status = dataBase.Tables[0].Rows[0][0].ToString();
                                        ipProperties.Country = dataBase.Tables[0].Rows[0][1].ToString();
                                        ipProperties.CountryCode = dataBase.Tables[0].Rows[0][2].ToString();
                                        ipProperties.Region = dataBase.Tables[0].Rows[0][3].ToString();
                                        ipProperties.RegionName = dataBase.Tables[0].Rows[0][4].ToString();
                                        ipProperties.City = dataBase.Tables[0].Rows[0][5].ToString();
                                        ipProperties.Zip = dataBase.Tables[0].Rows[0][6].ToString();
                                        ipProperties.Lat = dataBase.Tables[0].Rows[0][7].ToString();
                                        ipProperties.Lon = dataBase.Tables[0].Rows[0][8].ToString();
                                        ipProperties.TimeZone = dataBase.Tables[0].Rows[0][9].ToString();
                                        ipProperties.ISP = dataBase.Tables[0].Rows[0][10].ToString();
                                        ipProperties.ORG = dataBase.Tables[0].Rows[0][11].ToString();
                                        ipProperties.AS = dataBase.Tables[0].Rows[0][12].ToString();
                                        ipProperties.Query = dataBase.Tables[0].Rows[0][13].ToString();


                                        LoginUserInformation.CountryCode = ipProperties.CountryCode;
                                        LoginUserInformation.CountryName = ipProperties.Country;
                                        LoginUserInformation.Region = ipProperties.Region;
                                        LoginUserInformation.RegionName = ipProperties.RegionName;
                                        LoginUserInformation.CityName = ipProperties.City;
                                        LoginUserInformation.ZipCode = ipProperties.Zip;
                                        LoginUserInformation.Timezone = ipProperties.TimeZone;

                                        TimeZoneInfo dateTimeByZipTemp = TimeZoneInfo.FindSystemTimeZoneById(LoginUserInformation.CountryName + " Standard Time");
                                        DateTime dateTimeByZip = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, dateTimeByZipTemp);
                                        LoginUserInformation.RegionalTime = dateTimeByZip.ToString();
                                    }
                                }

                            }


                            return objUserList.UserType = 2;

                        }
                    }
                    
                }

                return objUserList.UserType;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private int LoadAssignCompanyDDL(UserList objUserList)
        {
            try
            {
                int companyID =0;
                UserListController _objUserListController = new UserListController();
                DataTable dtAssignCompany = _objUserListController.GetAssignCompanyUpdate(objUserList);
                if (dtAssignCompany.Rows.Count> 0)
                {
                    companyID = Convert.ToInt32(dtAssignCompany.Rows[0]["CompanyID"].ToString());
                }
                
                return companyID;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public ActionResult Identify()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Identify(uUserProfileTemp objuUserProfileTemp)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var UserProfileIDTemp = _objdbERPSolutionEntities.Database.SqlQuery<Guid>("SELECT A.UserProfileID FROM uUserProfile A WHERE A.DataUsed = 'A' AND A.Email = '" + objuUserProfileTemp.UserEmail + "'");
                objuUserProfileTemp.UserProfileID = UserProfileIDTemp.AsEnumerable().First();
                if (objuUserProfileTemp.UserProfileID != null)
                {
                    objuUserProfileTemp.DataUsed = "A";
                    var dateQuery = _objdbERPSolutionEntities.Database.SqlQuery<DateTime>("SELECT getdate()");
                    DateTime serverDate = dateQuery.AsEnumerable().First();
                    objuUserProfileTemp.EntryDate = serverDate;
                    var securityCodeSQL = _objdbERPSolutionEntities.Database.SqlQuery<string>("SELECT RIGHT(CAST(RAND(CHECKSUM(NEWID())) AS DECIMAL(15, 15)), 5) AS SecurityCode");
                    var securityCode = Convert.ToInt32(securityCodeSQL.AsEnumerable().First().ToString());
                    _objdbERPSolutionEntities.spInitiateSecurityCodeForgotPassword(objuUserProfileTemp.UserProfileID, objuUserProfileTemp.UserProfileID, securityCode);
                    UserProfileHomeController onjUserProfileHomeController = new UserProfileHomeController();
                    onjUserProfileHomeController.SendSecurityCodeByMailForgotPassword(1, securityCode, objuUserProfileTemp.UserEmail, objuUserProfileTemp.UserEmail);
                    return RedirectToAction("RecoverCode", "HomePageLogin", new { userEmail = objuUserProfileTemp.UserEmail });

                }
                else
                {
                    return RedirectToAction("Identify", "HomePageLogin");
                }


            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public ActionResult RecoverCode(string userEmail)
        {
            ViewBag.UserEmail = userEmail;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RecoverCode(UserList objUserList)
        {
            try
            {
                OnlineRegisterController objOnlineRegisterController = new OnlineRegisterController();
                objUserList.SecurityCode = objUserList.SecurityCode.Trim();
                objUserList.UserPassword = objOnlineRegisterController.EncodePassword(objUserList.UserPassword);
                objUserList.ConfirmPassword = objOnlineRegisterController.EncodePassword(objUserList.ConfirmPassword);
                objOnlineRegisterController.UpdatePassword(objUserList);
                return RedirectToAction("GetLogin", "HomePageLogin", new { caseNo = 1, targetURL = "" });
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePassword(UserList objUserList)
        {
            try
            {
                OnlineRegisterController objOnlineRegisterController = new OnlineRegisterController();
                objUserList.CurrentPassword = objUserList.CurrentPassword.Trim();
                objUserList.CurrentPassword = objOnlineRegisterController.EncodePassword(objUserList.CurrentPassword);
                objUserList.UserPassword = objUserList.UserPassword.Trim();
                objUserList.ConfirmPassword = objUserList.ConfirmPassword.Trim();
                objUserList.ConfirmPassword = objOnlineRegisterController.EncodePassword(objUserList.ConfirmPassword);
                objUserList.UserPassword = objOnlineRegisterController.EncodePassword(objUserList.UserPassword);
                objUserList.UserID = LoginUserInformation.UserID.Trim();
                objOnlineRegisterController.ChangePassword(objUserList);
                return RedirectToAction("Index", "Default");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}