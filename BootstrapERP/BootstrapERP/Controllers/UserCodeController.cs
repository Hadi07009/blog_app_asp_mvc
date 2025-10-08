using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class UserCodeController : Controller
    {
        private UserSecurityCode _objUserSecurityCode;
        private UserSecurityCodeController _objUserSecurityCodeController;
        private CompanySetup _objCompanySetup;
        // GET: UserCode
        public ActionResult Index()
        {
            try
            {
                _objUserSecurityCodeController = new UserSecurityCodeController();
                _objUserSecurityCode = new UserSecurityCode();
                _objUserSecurityCode.DtUser = _objUserSecurityCodeController.GetUserForSecurityCode();
                return View(_objUserSecurityCode);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public ActionResult SendCode(string userProfile, string userEmail)
        {
            try
            {
                SendSecurityCode(userProfile, userEmail);
                return RedirectToAction("Index", "UserCode");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public void SendSecurityCode(string userProfile, string userEmail)
        {
            try
            {
                _objUserSecurityCode = new UserSecurityCode();
                _objUserSecurityCode.UserKnownID = userProfile;
                UserProfile objUserProfile = new UserProfile();
                objUserProfile.Email = userEmail;
                _objCompanySetup = new CompanySetup();
                _objCompanySetup.CompanyID = LoginUserInformation.CompanyID;
                _objCompanySetup.EntryUserName = LoginUserInformation.UserID;
                _objUserSecurityCodeController = new UserSecurityCodeController();
                _objUserSecurityCodeController.SendSecurityCode(_objCompanySetup, _objUserSecurityCode, objUserProfile);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}