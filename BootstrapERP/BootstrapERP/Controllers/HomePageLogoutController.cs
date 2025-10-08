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
    public class HomePageLogoutController : Controller
    {
        // GET: HomePageLogout
        private UserProfile _objUserProfile;
        public ActionResult Index()
        {
            try
            {
                TimeZoneInfo dateTimeByZipTemp = TimeZoneInfo.FindSystemTimeZoneById(LoginUserInformation.CountryName + " Standard Time");
                DateTime dateTimeByZip = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, dateTimeByZipTemp);
                _objUserProfile = new UserProfile();
                _objUserProfile.RegionalTime = dateTimeByZip.ToString();
                _objUserProfile.UniqueSessionCode = LoginUserInformation.UniqueSessionCode;
                _objUserProfile.LoginInfoID = LoginUserInformation.LoginInfoID;
                UserLoginAccess objUserLoginAccess = new UserLoginAccess();
                objUserLoginAccess.Update(_objUserProfile);
                Session.Abandon();
                return RedirectToAction("Index", "BlogPosts");
            }
            catch (Exception msgException)
            {
                Session.Abandon();
                return RedirectToAction("Index", "BlogPosts");

                //throw msgException;
            }
        }
    }
}