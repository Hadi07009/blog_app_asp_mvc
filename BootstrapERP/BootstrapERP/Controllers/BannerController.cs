using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class BannerController : Controller
    {
        private SiteBanner _objSiteBanner;
        private BannerAccess _objBannerAccess;
        // GET: Banner
        public ActionResult Index()
        {
            try
            {
                _objSiteBanner = new SiteBanner();
                return View(_objSiteBanner);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string saveBanner, SiteBanner objSiteBanner, HttpPostedFileBase file)
        {
            try
            {
                if (saveBanner != null)
                {
                    objSiteBanner.EntryUserName = LoginUserInformation.UserID;
                    objSiteBanner.CompanyID = LoginUserInformation.CompanyID;
                    objSiteBanner.BranchID = LoginUserInformation.BranchID;
                    objSiteBanner.ApplicationID = LoginUserInformation.ApplicationID;
                    objSiteBanner.BannerTitle = objSiteBanner.BannerTitle.Replace("'", "''");
                    objSiteBanner.BannerRemarks = objSiteBanner.BannerRemarks.Replace("'", "''");

                    #region ImageUpload
                    string fileContent = string.Empty;
                    if (file != null && file.ContentLength > 0)
                    {
                        string relativePath = "~/ImagesUploaded/" + Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath(relativePath);
                        file.SaveAs(physicalPath);
                        objSiteBanner.BannerImageURL = relativePath;
                    }

                    #endregion ImageUpload

                    _objBannerAccess = new BannerAccess();
                    _objBannerAccess.SaveBanner(objSiteBanner);
                }

                return RedirectToAction("Index");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}