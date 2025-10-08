using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;

namespace BootstrapERP.Controllers
{
    public class UserProfileHomeController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: UserProfileHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUserHome()
        {
            try
            {
                var query = _objdbERPSolutionEntities.EmployeeTitles.Select(c => new { c.FieldOfID, c.FieldOfName });
                ViewBag.UserTitle = new SelectList(query.AsEnumerable(), "FieldOfID", "FieldOfName");
                ViewBag.MessagePassword = "";
                return View();
            }
            catch (Exception msgException)
            {

                ViewBag.MessagePassword = msgException.Message;
                return View();
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult CreateUserHome(string uerTitle, uUserProfileTemp objuUserProfileTemp)
        {
            try
            {
                OnlineRegisterController objOnlineRegisterController = new OnlineRegisterController();
                string cEmail = objOnlineRegisterController.CheckUserEmail(objuUserProfileTemp); ;
                if (cEmail == objuUserProfileTemp.UserEmail)
                {
                    var query = _objdbERPSolutionEntities.EmployeeTitles.Select(c => new { c.FieldOfID, c.FieldOfName });
                    ViewBag.uerTitle = new SelectList(query.AsEnumerable(), "FieldOfID", "FieldOfName", uerTitle);
                    ViewBag.MessagePassword = objuUserProfileTemp.UserEmail + " already exist, please sign in.";
                    return View();
                }
                else
                {
                    ViewBag.MessagePassword = "";
                    if (objuUserProfileTemp.CompanyName == null)
                    {
                        objuUserProfileTemp.CompanyName = objuUserProfileTemp.UserName;
                    }

                    if (objuUserProfileTemp.CompanyEmail == null)
                    {
                        objuUserProfileTemp.CompanyEmail = objuUserProfileTemp.UserEmail;
                    }

                    objuUserProfileTemp.UserName = (objuUserProfileTemp.UserName + " " + objuUserProfileTemp.LastName).Trim();

                    objuUserProfileTemp.UserProfileID = Guid.NewGuid();
                    objuUserProfileTemp.DataUsed = "A";
                    var dateQuery = _objdbERPSolutionEntities.Database.SqlQuery<DateTime>("SELECT getdate()");
                    DateTime serverDate = dateQuery.AsEnumerable().First();
                    objuUserProfileTemp.EntryDate = serverDate;

                    _objdbERPSolutionEntities.uUserProfileTemps.Add(objuUserProfileTemp);
                    _objdbERPSolutionEntities.SaveChanges();
                    var securityCodeSQL = _objdbERPSolutionEntities.Database.SqlQuery<string>("SELECT RIGHT(CAST(RAND(CHECKSUM(NEWID())) AS DECIMAL(15, 15)), 5) AS SecurityCode");
                    var securityCode = Convert.ToInt32(securityCodeSQL.AsEnumerable().First().ToString());

                    _objdbERPSolutionEntities.spInitiateSecurityCode(objuUserProfileTemp.UserProfileID, objuUserProfileTemp.UserProfileID, securityCode);

                    this.SendSecurityCodeByMail(1, securityCode, objuUserProfileTemp.UserEmail, objuUserProfileTemp.CompanyEmail);
                    return RedirectToAction("CreateUserHome");
                }

                
                
            }
            catch (Exception msgException)
            {

                ViewBag.MessagePassword = msgException.Message;
                return View();
            }
        }
        public void SendSecurityCodeByMail(int companyID, int securityCode, string userEmail, string companyEmail)
        {
            try
            {
                //objUserSecurityCode.SecurityCode = GetSecurityCode(objUserSecurityCode);
                if (securityCode != 0)
                {
                    MailServiceSetup objMailServiceSetup = new MailServiceSetup();

                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    Thank you for choosing our service. We are aware about privacy and security of our customer data. <br /><br />

                    Your security code is: " + securityCode + "" + "      " +

                  @"<br /><br />
                    Please go to <a href='http://27.147.194.136:82/HomePageRegister/RegisterOnline'>Register</a><br />
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

                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />

                    Thank you for choosing our service.<br /><br />

                    < Mr. user name > has requested the service on behalf of <Company Name>.<br /><br />

                    If you have any query regarding this request, please contact us.<br /><br />

                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address >";
                    objMailServiceSetup.EmailTo = companyEmail;
                    objMailServiceSetup.MailtypeID = "1";
                    objMailServiceSetup.AttachItem = attachDocument;
                    objMailServiceController.eMailSendServiceHTML(companyID, objMailServiceSetup);

                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public void SendSecurityCodeByMailForgotPassword(int companyID, int securityCode, string userEmail, string companyEmail)
        {
            try
            {
                if (securityCode != 0)
                {
                    MailServiceSetup objMailServiceSetup = new MailServiceSetup();

                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />
                    
                    We received a request to reset your password. <br /><br />

                    Enter the following password reset code: " + securityCode + "" + "      " +

                  @"<br /><br />
                    

                    We wish you to experience an excellent journey in using our business solution.<br /><br />


                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address > ";


                    objMailServiceSetup.EmailTo = userEmail;
                    objMailServiceSetup.MailtypeID = "1";
                    ArrayList attachDocument = new ArrayList();
                    objMailServiceSetup.AttachItem = attachDocument;
                    MailServiceController objMailServiceController = new MailServiceController();
                    objMailServiceController.eMailSendServiceHTML(companyID, objMailServiceSetup);

                    objMailServiceSetup.MailBody = @"
                    Dear Sir,<br /><br />

                    Thank you for choosing our service.<br /><br />

                    < Mr. user name > has requested for reset password on behalf of <Company Name>.<br /><br />

                    If you have any query regarding this request, please contact us.<br /><br />

                    Thanking you,<br /><br />

                    The Business Solution Team | Help Line: < Contact No > | < Email address >";
                    objMailServiceSetup.EmailTo = companyEmail;
                    objMailServiceSetup.MailtypeID = "1";
                    objMailServiceSetup.AttachItem = attachDocument;
                    objMailServiceController.eMailSendServiceHTML(companyID, objMailServiceSetup);

                }

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}