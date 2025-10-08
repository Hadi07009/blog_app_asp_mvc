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
    public class HeaderNavBarController : Controller
    {
        // GET: HeaderNavBar
        private HeaderNavBar _objHeaderNavBar;
        private HeaderNavBarPermission _objHeaderNavBarPermission;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult __HeaderNavBar()
        {
            try
            {
                _objHeaderNavBar = new HeaderNavBar();
                _objHeaderNavBar.ShowPosition = 1;

                if (LoginUserInformation.LogginUserStatus == "Yes")
                {
                    ViewBag.LogginUserStatus = "Yes";
                    _objHeaderNavBar.CompanyID = LoginUserInformation.CompanyID;
                    _objHeaderNavBar.EntryUserName = LoginUserInformation.UserID;
                    _objHeaderNavBarPermission = new HeaderNavBarPermission();
                    _objHeaderNavBar.DtNodes = _objHeaderNavBarPermission.GetUserNodes(_objHeaderNavBar);
                }
                else
                {
                    ViewBag.LogginUserStatus = "";
                }


                return PartialView("__HeaderNavBar", _objHeaderNavBar);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
            
        }
    }
}