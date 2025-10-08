using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class UserSessionController : Controller
    {
        // GET: UserSession

        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();

        private UserProfile _objUserProfile;
        private UserLoginAccess _objUserLoginAccess;
        private UserLogin _objUserLogin;
        public ActionResult Index()
        {
            try
            {
                BlogContentDetail objBlogContentDetail = new BlogContentDetail();
                var queryContentCategory = _objdbERPSolutionEntities.BrowserNameSetups.Select(m => new { m.BrowserName, m.BrowserID })
                .AsEnumerable().GroupBy(x => x.BrowserName).Select(x => x.First()).ToList();
                ViewBag.BrowserName = new SelectList(queryContentCategory.AsEnumerable(), "BrowserID", "BrowserName");

                if (LoginUserInformation.UserProfileTypeID == "1")
                {
                    var queryUser = _objdbERPSolutionEntities.AllUsers.Select(m => new { m.Email, m.UserProfileID })
                .AsEnumerable().GroupBy(x => x.Email).Select(x => x.First()).ToList();
                    ViewBag.UserList = new SelectList(queryUser.AsEnumerable(), "UserProfileID", "Email",LoginUserInformation.UserID);
                }
                else
                {
                    var queryUser = _objdbERPSolutionEntities.AllUsers.Where(a => (a.UserProfileID.ToString() == LoginUserInformation.UserID)).Select(m => new { m.Email, m.UserProfileID })
                .AsEnumerable().GroupBy(x => x.Email).Select(x => x.First()).ToList();
                    ViewBag.UserList = new SelectList(queryUser.AsEnumerable(), "UserProfileID", "Email", LoginUserInformation.UserID);

                }

                var queryDeviceType = _objdbERPSolutionEntities.DeviceTypeSetups.Select(m => new { m.DeviceType, m.DeviceTypeID })
                .AsEnumerable().GroupBy(x => x.DeviceType).Select(x => x.First()).ToList();
                ViewBag.DeviceType = new SelectList(queryDeviceType.AsEnumerable(), "DeviceTypeID", "DeviceType");
                 

                var queryCountryName = _objdbERPSolutionEntities.CountryNameSetups.Select(m => new { m.CountryName, m.CountryCode })
                .AsEnumerable().GroupBy(x => x.CountryName).Select(x => x.First()).ToList();
                ViewBag.CountryName = new SelectList(queryCountryName.AsEnumerable(), "CountryCode", "CountryName");

                _objUserProfile = new UserProfile();
                _objUserLoginAccess = new UserLoginAccess();
                _objUserLogin = new UserLogin();
                _objUserLogin.ApplicationID = LoginUserInformation.ApplicationID;
                _objUserLogin.CompanyID = LoginUserInformation.CompanyID;
                _objUserLogin.BranchID = LoginUserInformation.BranchID;
                
                _objUserLogin.UserLoginDate = LoginUserInformation.RegionalTime;
                _objUserLogin.EntryUserName = LoginUserInformation.UserID;
                _objUserProfile.DtLoginRecord = _objUserLoginAccess.GetLoginRecord(_objUserLogin);
                return View(_objUserProfile);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string actionSearch, string actionClear, UserProfile objUserProfile, string BrowserName, string DeviceType
            , string CountryName, string UserList)
        {
            try
            {
                if (actionClear != null)
                {
                    return RedirectToAction("Index", "UserSession");

                }
                else if (actionSearch != null)
                {
                    _objUserLoginAccess = new UserLoginAccess();
                    _objUserLogin = new UserLogin();
                    _objUserLogin.ApplicationID = LoginUserInformation.ApplicationID;
                    _objUserLogin.CompanyID = LoginUserInformation.CompanyID;
                    _objUserLogin.BranchID = LoginUserInformation.BranchID;

                    _objUserLogin.EntryUserName = LoginUserInformation.UserID;

                    objUserProfile.FromDate = objUserProfile.FromDate == null ? string.Empty : Convert.ToDateTime(objUserProfile.FromDate.ToString()).ToShortDateString();
                    objUserProfile.ToDate = objUserProfile.ToDate == null ? string.Empty : Convert.ToDateTime(objUserProfile.ToDate.ToString()).ToShortDateString();


                    objUserProfile.DtLoginRecord = _objUserLoginAccess.GetLoginRecord(objUserProfile,_objUserLogin,BrowserName
                        ,DeviceType,CountryName, UserList);

                    var queryContentCategory = _objdbERPSolutionEntities.BrowserNameSetups.Select(m => new { m.BrowserName, m.BrowserID })
                .AsEnumerable().GroupBy(x => x.BrowserName).Select(x => x.First()).ToList();
                    ViewBag.BrowserName = new SelectList(queryContentCategory.AsEnumerable(), "BrowserID", "BrowserName", BrowserName);

                    var queryDeviceType = _objdbERPSolutionEntities.DeviceTypeSetups.Select(m => new { m.DeviceType, m.DeviceTypeID })
                    .AsEnumerable().GroupBy(x => x.DeviceType).Select(x => x.First()).ToList();
                    ViewBag.DeviceType = new SelectList(queryDeviceType.AsEnumerable(), "DeviceTypeID", "DeviceType",DeviceType);


                    var queryCountryName = _objdbERPSolutionEntities.CountryNameSetups.Select(m => new { m.CountryName, m.CountryCode })
                    .AsEnumerable().GroupBy(x => x.CountryName).Select(x => x.First()).ToList();
                    ViewBag.CountryName = new SelectList(queryCountryName.AsEnumerable(), "CountryCode", "CountryName",CountryName);
                    
                    if (LoginUserInformation.UserProfileTypeID == "1")
                    {
                        var queryUser = _objdbERPSolutionEntities.AllUsers.Select(m => new { m.Email, m.UserProfileID })
                    .AsEnumerable().GroupBy(x => x.Email).Select(x => x.First()).ToList();
                        ViewBag.UserList = new SelectList(queryUser.AsEnumerable(), "UserProfileID", "Email", UserList);
                    }
                    else
                    {
                        var queryUser = _objdbERPSolutionEntities.AllUsers.Where(a => (a.UserProfileID.ToString() == LoginUserInformation.UserID)).Select(m => new { m.Email, m.UserProfileID })
                    .AsEnumerable().GroupBy(x => x.Email).Select(x => x.First()).ToList();
                        ViewBag.UserList = new SelectList(queryUser.AsEnumerable(), "UserProfileID", "Email", UserList);

                    }
                    return View(objUserProfile);
                }
                else
                {
                    return RedirectToAction("Index", "UserSession");
                }
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private IEnumerable<SelectListItem> AddDefaultOption(IEnumerable<SelectListItem> list, string dataTextField, string selectedValue)
        {
            try
            {
                var items = new List<SelectListItem>();
                items.Add(new SelectListItem() { Text = dataTextField, Value = selectedValue });
                items.AddRange(list);
                return items;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}