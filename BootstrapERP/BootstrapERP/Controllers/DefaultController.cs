using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        private UserProfile _objUserProfile;
        public ActionResult Index()
        {
            _objUserProfile = new UserProfile();
            LoginUserInformation.LogginUserStatus = "Yes";
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
            return View(_objUserProfile);
        }
    }
}