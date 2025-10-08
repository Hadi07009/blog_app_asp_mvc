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
    public class PremierContentController : Controller
    {
        // GET: PremierContent
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private SiteContentDetails _objSiteContentDetails;
        private siteApplicationSetup _objsiteApplicationSetup;
        private siteContentType _objsiteContentType;
        private siteContentCategory _objsiteContentCategory;
        private siteContentRelatedTo _objsiteContentRelatedTo;
        private SiteContentAccessController _objSiteContentAccessController;
        public ActionResult Index(int contentDetailID)
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objSiteContentDetails.ContentID = contentDetailID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContentAdmin(_objSiteContentDetails);
                foreach (DataRow rowNo in dtContent.Rows)
                {
                    int rowIndex = (int)dtContent.Rows.IndexOf(rowNo);
                    if (rowIndex == 0)
                    {
                        _objSiteContentDetails.ContentTitle = rowNo["ContentDetailTitle"].ToString();
                        _objSiteContentDetails.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        _objSiteContentDetails.ContentDescription = rowNo["ContentDetailDescription"].ToString();
                        _objSiteContentDetails.ContentEntryDate = Convert.ToDateTime(rowNo["EntryDate"].ToString());
                        _objSiteContentDetails.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();

                        ViewBag.FullName = rowNo["FullName"].ToString();
                        _objSiteContentDetails.AuthorsEmailID = rowNo["Email"].ToString();
                        _objSiteContentDetails.DtContentCategory = _objSiteContentAccessController.GetBlogCategory(_objSiteContentDetails);
                        ViewBag.ContentCategoryTitle = _objSiteContentAccessController.GetSelectedBlogCategory(_objSiteContentDetails);
                    }
                }

                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string actionPremierContent,
            SiteContentDetails objSiteContentDetails, HttpPostedFileBase file)
        {
            try
            {
                if (actionPremierContent != null)
                {
                    objSiteContentDetails.ActionTypeID = "M";
                    objSiteContentDetails.QualityTag = "M";
                

                    _objSiteContentAccessController = new SiteContentAccessController();
                    var contentLogIDsql = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentLogID),0)+1 AS ContentLogID from [blogContentPublishMethod]");
                    objSiteContentDetails.ContentLogID = contentLogIDsql.AsEnumerable().First().ToString();
                    var secquenceNosql = _objdbERPSolutionEntities.Database.SqlQuery<int>("  select ISNULL( MAX( A.[SecquenceNo]),0)+1 AS [SecquenceNo] from [blogContentPublishMethod] A WHERE A.ContentID = '" + objSiteContentDetails.ContentID + "';");
                    objSiteContentDetails.SecquenceNo = Convert.ToInt32(secquenceNosql.AsEnumerable().First().ToString());
                    objSiteContentDetails.EntryUserName = LoginUserInformation.UserID;
                    objSiteContentDetails.CompanyID = LoginUserInformation.CompanyID;

                    //objSiteContentDetails.ContentDetailImageURL = null;
                    #region ImageUpload
                    string fileContent = string.Empty;
                    if (file != null && file.ContentLength > 0)
                    {
                        string relativePath = "~/ImagesUploaded/" + Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath(relativePath);
                        file.SaveAs(physicalPath);
                        objSiteContentDetails.ContentDetailImageURL = relativePath;
                    }

                    #endregion ImageUpload
                    _objSiteContentAccessController.ActionByAdmin(objSiteContentDetails);
                }

                return RedirectToAction("Index", "PremierList");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}