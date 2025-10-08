using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class CompanyProfileController : Controller
    {
        // GET: CompanyProfile
        private CompanyDetailsSetup _objCompanyDetailsSetup;
        private CompanySetupController _objCompanySetupController;
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        public ActionResult Index()
        {
            try
            {
                _objCompanyDetailsSetup = new CompanyDetailsSetup();
                _objCompanyDetailsSetup.CompanyID = LoginUserInformation.CompanyID;
                _objCompanySetupController = new CompanySetupController();
                _objCompanyDetailsSetup.DtCompany = _objCompanySetupController.GetCompanyDetails(_objCompanyDetailsSetup);
                return View(_objCompanyDetailsSetup);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult Cedit(int cID)
        {
            try
            {
                _objCompanyDetailsSetup = new CompanyDetailsSetup();
                _objCompanyDetailsSetup.CompanyID = cID;
                _objCompanySetupController = new CompanySetupController();
                _objCompanyDetailsSetup.DtCompany = _objCompanySetupController.GetCompanyDetails(_objCompanyDetailsSetup);

                foreach (DataRow rowNo in _objCompanyDetailsSetup.DtCompany.Rows)
                {
                    _objCompanyDetailsSetup.CompanyName = rowNo["CompanyName"].ToString();
                    _objCompanyDetailsSetup.CompanyEmail = rowNo["CompanyEmail"].ToString()==null?"": rowNo["CompanyEmail"].ToString();
                    _objCompanyDetailsSetup.CompanyMobile = rowNo["CompanyMobile"].ToString() == null?"": rowNo["CompanyMobile"].ToString();
                    _objCompanyDetailsSetup.CompanyShortName = rowNo["CompanyShortName"].ToString() == null ? "" : rowNo["CompanyShortName"].ToString();
                    _objCompanyDetailsSetup.LicenceID = rowNo["LicenceID"].ToString() == null ? "" : rowNo["LicenceID"].ToString();
                    _objCompanyDetailsSetup.VatNumber = rowNo["VATNumber"].ToString() == null ? "" : rowNo["VATNumber"].ToString();
                    _objCompanyDetailsSetup.CompanySlogun = rowNo["CompanySlogun"].ToString() == null ? "" : rowNo["CompanySlogun"].ToString();
                    _objCompanyDetailsSetup.ImageURL = rowNo["CompanyLogoURL"].ToString() == null ? "" : rowNo["CompanyLogoURL"].ToString();
                    
                    _objCompanyDetailsSetup.House = rowNo["House"].ToString() == null ? "" : rowNo["House"].ToString();
                    _objCompanyDetailsSetup.Road = rowNo["Road"].ToString() == null ? "" : rowNo["Road"].ToString();
                    _objCompanyDetailsSetup.Sector = rowNo["Sector"].ToString() == null ? "" : rowNo["Sector"].ToString();
                    _objCompanyDetailsSetup.Landmark = rowNo["Landmark"].ToString() == null ? "" : rowNo["Landmark"].ToString();
                    _objCompanyDetailsSetup.CompanyPhones = rowNo["CompanyPhones"].ToString() == null ? "" : rowNo["CompanyPhones"].ToString();
                    _objCompanyDetailsSetup.CompanyFax = rowNo["CompanyFax"].ToString() == null ? "" : rowNo["CompanyFax"].ToString();
                    _objCompanyDetailsSetup.CompanyURL = rowNo["CompanyURL"].ToString() == null ? "" : rowNo["CompanyURL"].ToString();

                    _objCompanyDetailsSetup.ContactPersonName = rowNo["ContactPersonName"].ToString() == null ? "" : rowNo["ContactPersonName"].ToString();
                    _objCompanyDetailsSetup.ContactPersonContactNumber = rowNo["ContactPersonContactNumber"].ToString() == null ? "" : rowNo["ContactPersonContactNumber"].ToString();

                    _objCompanyDetailsSetup.AlternateContactPersonName = rowNo["AlternateContactPersonName"].ToString() == null ? "" : rowNo["AlternateContactPersonName"].ToString();
                    _objCompanyDetailsSetup.AlternateContactPersonContactNumber = rowNo["AlternateContactPersonContactNumber"].ToString() == null ? "" : rowNo["AlternateContactPersonContactNumber"].ToString();

                    _objCompanyDetailsSetup.FaceBookID = rowNo["FaceBookID"].ToString() == null ? "" : rowNo["FaceBookID"].ToString();
                    _objCompanyDetailsSetup.LinkedInID = rowNo["LinkedInID"].ToString() == null ? "" : rowNo["LinkedInID"].ToString();
                    _objCompanyDetailsSetup.TwitterID = rowNo["TwitterID"].ToString() == null ? "" : rowNo["TwitterID"].ToString();
                    _objCompanyDetailsSetup.YouTubeID = rowNo["YouTubeID"].ToString() == null ? "" : rowNo["YouTubeID"].ToString();

                    var queryBusinessTypey = _objdbERPSolutionEntities.BusinessNatureSetups.Select(c => new { c.FieldOfID, c.FieldOfName });
                    ViewBag.businessTypey = new SelectList(queryBusinessTypey.AsEnumerable(), "FieldOfID", "FieldOfName", rowNo["BusinessTypeID"].ToString());
                    var queryOwnership = _objdbERPSolutionEntities.OwnershipSetups.Select(c => new { c.FieldOfID, c.FieldOfName });
                    ViewBag.bownership = new SelectList(queryOwnership.AsEnumerable(), "FieldOfID", "FieldOfName", rowNo["OwnershipTypeID"].ToString());
                    var queryDistrictView = _objdbERPSolutionEntities.DistrictSetupViews.Select(c => new { c.FieldOfID, c.FieldOfName });
                    ViewBag.districtList = new SelectList(queryDistrictView.AsEnumerable(), "FieldOfID", "FieldOfName", rowNo["DistrictID"].ToString());
                    var queryDesignation = _objdbERPSolutionEntities.orgDesignations.Where(a => (a.CompanyID == LoginUserInformation.CompanyID && a.BranchID == LoginUserInformation.BranchID)).Select(c => new { c.FieldOfID, c.FieldOfName });
                    ViewBag.designationList = new SelectList(queryDesignation.AsEnumerable(), "FieldOfID", "FieldOfName", rowNo["ContactPersonDesignation"].ToString());
                    var queryDaltenative = _objdbERPSolutionEntities.orgDesignations.Where(a => (a.CompanyID == LoginUserInformation.CompanyID && a.BranchID == LoginUserInformation.BranchID)).Select(c => new { c.FieldOfID, c.FieldOfName });
                    ViewBag.dealternative = new SelectList(queryDaltenative.AsEnumerable(), "FieldOfID", "FieldOfName", rowNo["AlternateContactPersonDesignation"].ToString());
                }

                return View(_objCompanyDetailsSetup);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Cedit(string updateCompany, string businessTypey, string bownership, string districtList
            , string designationList, string dealternative, CompanyDetailsSetup objCompanyDetailsSetup
            , string company_slogun, HttpPostedFileBase file)
        {
            objCompanyDetailsSetup.EntryUserName = LoginUserInformation.UserID;
            objCompanyDetailsSetup.BTypeID = Convert.ToInt32(businessTypey);
            objCompanyDetailsSetup.OTypeID = Convert.ToInt32(bownership);
            objCompanyDetailsSetup.DistrictID = Convert.ToInt32(districtList);
            objCompanyDetailsSetup.ContactPersonDesignation = designationList;
            objCompanyDetailsSetup.AlternateContactPersonDesignation = dealternative;
            objCompanyDetailsSetup.CompanySlogun = company_slogun;
            
            #region ImageUpload
            string fileContent = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                string relativePath = "~/ImagesUploaded/" + Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath(relativePath);
                file.SaveAs(physicalPath);
                objCompanyDetailsSetup.ImageURL = relativePath;
            }

            #endregion ImageUpload

            if (updateCompany != null)
            {
                _objCompanySetupController = new CompanySetupController();
                int cidCheck = _objCompanySetupController.CheckComID(objCompanyDetailsSetup);
                int cidCheckTemp = cidCheck == 0 ? objCompanyDetailsSetup.CompanyID : cidCheck;
                if (objCompanyDetailsSetup.CompanyID == cidCheckTemp)
                {
                    _objCompanySetupController.UpdaeCProfile(objCompanyDetailsSetup);
                }
            }

            return RedirectToAction("Index", "CompanyProfile");
        }

    }
}