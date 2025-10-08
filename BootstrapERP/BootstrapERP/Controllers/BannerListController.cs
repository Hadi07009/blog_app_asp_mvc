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
    public class BannerListController : Controller
    {
        private SiteBanner _objSiteBanner;
        private BannerAccess _objBannerAccess;
        // GET: BannerList
        public ActionResult Index()
        {
            try
            {
                _objSiteBanner = new SiteBanner();
                _objSiteBanner.CompanyID = LoginUserInformation.CompanyID;
                _objSiteBanner.BranchID = LoginUserInformation.BranchID;
                _objSiteBanner.ApplicationID = LoginUserInformation.ApplicationID;
                _objBannerAccess = new BannerAccess();
                _objSiteBanner.DtBanner = _objBannerAccess.GetAllBanner(_objSiteBanner);
                return View(_objSiteBanner);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        
        public ActionResult BannerSelect(int selectedBannerID, string selectionType)
        {
            try
            {
                _objSiteBanner = new SiteBanner();
                _objSiteBanner.DataUsedValue = selectionType;
                _objSiteBanner.BannerID = selectedBannerID;
                _objBannerAccess = new BannerAccess();
                _objSiteBanner.CompanyID = LoginUserInformation.CompanyID;
                _objSiteBanner.BranchID = LoginUserInformation.BranchID;
                _objSiteBanner.ApplicationID = LoginUserInformation.ApplicationID;
                _objSiteBanner.EntryUserName = LoginUserInformation.UserID;
                _objBannerAccess.ActiveInactiveBanner(_objSiteBanner);
                return RedirectToAction("Index", "BannerList");
                
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
    }
}