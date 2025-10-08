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
    public class SiteContentDisplayController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private SiteContentDetails _objSiteContentDetails;
        private siteApplicationSetup _objsiteApplicationSetup;
        private siteContentType _objsiteContentType;
        private siteContentCategory _objsiteContentCategory;
        private siteContentRelatedTo _objsiteContentRelatedTo;
        private SiteContentAccessController _objSiteContentAccessController;
        // GET: SiteContentDisplay
        public ActionResult Index(int applicationID, int contentTypeID, int contentCategoryID, int relatedToID,string viewTitle)
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = applicationID;
                _objsiteContentType.ContentTypeID = contentTypeID;
                _objsiteContentCategory.ContentCategoryID = contentCategoryID;
                _objsiteContentRelatedTo.ContentRelatedToID = relatedToID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetSiteContent(_objsiteApplicationSetup,
                    _objsiteContentType, _objsiteContentCategory, _objsiteContentRelatedTo);
                foreach (DataRow rowNo in dtContent.Rows)
                {
                    int rowIndex = (int)dtContent.Rows.IndexOf(rowNo);
                    if (rowIndex == 0)
                    {
                        _objSiteContentDetails.ContentTitle = rowNo["ContentDetailTitle"].ToString();
                        _objSiteContentDetails.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        _objSiteContentDetails.ContentDescription = rowNo["ContentDetailDescription"].ToString();
                        _objSiteContentDetails.ContentTypeDescription = rowNo["ContentTypeDescription"].ToString();
                        _objSiteContentDetails.ContentRelatedToTitle = rowNo["ContentTitle"].ToString();
                        _objSiteContentDetails.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                        ViewBag.ContentImageURL = rowNo["ContentImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentImageURL"].ToString();
                    }
                    else if (rowIndex == 1)
                    {
                        ViewBag.ContentDetailDescription = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                }

                ViewBag.viewTitle = viewTitle;
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult AboutBlog(  )
        {
            try
            {
                string viewTitle = "About";
                LoginUserInformation.CompanyID = 1; LoginUserInformation.BranchID = 1;
                int applicationID = 2; int contentCategoryID = 2; int relatedToID = 1;
                int contentTypeID = 1;

                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = applicationID;
                _objsiteContentType.ContentTypeID = contentTypeID;
                _objsiteContentCategory.ContentCategoryID = contentCategoryID;
                _objsiteContentRelatedTo.ContentRelatedToID = relatedToID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetSiteContent(_objsiteApplicationSetup,
                    _objsiteContentType, _objsiteContentCategory, _objsiteContentRelatedTo);
                foreach (DataRow rowNo in dtContent.Rows)
                {
                    int rowIndex = (int)dtContent.Rows.IndexOf(rowNo);
                    if (rowIndex == 0)
                    {
                        _objSiteContentDetails.ContentTitle = rowNo["ContentDetailTitle"].ToString();
                        _objSiteContentDetails.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        _objSiteContentDetails.ContentDescription = rowNo["ContentDetailDescription"].ToString();
                        _objSiteContentDetails.ContentTypeDescription = rowNo["ContentTypeDescription"].ToString();
                        _objSiteContentDetails.ContentRelatedToTitle = rowNo["ContentTitle"].ToString();
                        _objSiteContentDetails.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                        ViewBag.ContentImageURL = rowNo["ContentImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentImageURL"].ToString();
                    }
                    else if (rowIndex == 1)
                    {
                        ViewBag.ContentDetailDescription = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                }

                ViewBag.viewTitle = viewTitle;
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public ActionResult PortfolioDesign(int applicationID, int contentTypeID, int contentCategoryID, int relatedToID, string viewTitle)
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = applicationID;
                _objsiteContentType.ContentTypeID = contentTypeID;
                _objsiteContentCategory.ContentCategoryID = contentCategoryID;
                _objsiteContentRelatedTo.ContentRelatedToID = relatedToID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetSiteContent(_objsiteApplicationSetup,
                    _objsiteContentType, _objsiteContentCategory, _objsiteContentRelatedTo);
                foreach (DataRow rowNo in dtContent.Rows)
                {
                    int rowIndex = (int)dtContent.Rows.IndexOf(rowNo);
                    if (rowIndex == 0)
                    {
                        _objSiteContentDetails.ContentTitle = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailID1 = rowNo["ContentDetailID"].ToString();
                        _objSiteContentDetails.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        _objSiteContentDetails.ContentIntroductoryText = rowNo["ContentIntroductoryText"].ToString();
                        _objSiteContentDetails.ContentDescription = rowNo["ContentDetailDescription"].ToString();
                        _objSiteContentDetails.ContentTypeDescription = rowNo["ContentTypeDescription"].ToString();
                        _objSiteContentDetails.ContentRelatedToTitle = rowNo["ContentTitle"].ToString();
                        _objSiteContentDetails.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                        ViewBag.ContentImageURL = rowNo["ContentImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentImageURL"].ToString();
                    }
                    else if (rowIndex == 1)
                    {
                        ViewBag.ContentDetailID2 = rowNo["ContentDetailID"].ToString();
                        ViewBag.ContentDetailDescription = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                    else if (rowIndex == 2)
                    {
                        ViewBag.ContentDetailID3 = rowNo["ContentDetailID"].ToString();
                        ViewBag.ContentDetailDescription3 = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle3 = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle3 = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText3 = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.ContentDetailImageURL3 = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                    else if (rowIndex == 3)
                    {
                        ViewBag.ContentDetailID4 = rowNo["ContentDetailID"].ToString();
                        ViewBag.ContentDetailDescription4 = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle4 = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle4 = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText4 = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.ContentDetailImageURL4 = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                    else if (rowIndex == 4)
                    {
                        ViewBag.ContentDetailID5 = rowNo["ContentDetailID"].ToString();
                        ViewBag.ContentDetailDescription5 = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle5 = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle5 = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText5 = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.ContentDetailImageURL5 = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                }

                ViewBag.viewTitle = viewTitle;
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
        public ActionResult PortfolioDescription(int contentDetailID )
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objSiteContentDetails.ContentID = contentDetailID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetSiteContent(_objSiteContentDetails);
                foreach (DataRow rowNo in dtContent.Rows)
                {
                    int rowIndex = (int)dtContent.Rows.IndexOf(rowNo);
                    if (rowIndex == 0)
                    {
                        _objSiteContentDetails.ContentTitle = rowNo["ContentDetailTitle"].ToString();
                        _objSiteContentDetails.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        _objSiteContentDetails.ContentDescription = rowNo["ContentDetailDescription"].ToString();
                        _objSiteContentDetails.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                        
                    }
                }

                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }


        public ActionResult BlogDesign(int applicationID, int contentTypeID, int contentCategoryID, int relatedToID, string viewTitle)
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = applicationID;
                _objsiteContentType.ContentTypeID = contentTypeID;
                _objsiteContentCategory.ContentCategoryID = contentCategoryID;
                _objsiteContentRelatedTo.ContentRelatedToID = relatedToID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetSiteContent(_objsiteApplicationSetup,
                    _objsiteContentType, _objsiteContentCategory);
                foreach (DataRow rowNo in dtContent.Rows)
                {
                    int rowIndex = (int)dtContent.Rows.IndexOf(rowNo);
                    if (rowIndex == 0)
                    {
                        _objSiteContentDetails.ContentTitle = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailID1 = rowNo["ContentDetailID"].ToString();
                        ViewBag.FullName1 = rowNo["FullName"].ToString();
                        _objSiteContentDetails.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        _objSiteContentDetails.ContentIntroductoryText = rowNo["ContentIntroductoryText"].ToString();
                        _objSiteContentDetails.ContentDescription = rowNo["ContentDetailDescription"].ToString();
                        _objSiteContentDetails.ContentTypeDescription = rowNo["ContentTypeDescription"].ToString();
                        _objSiteContentDetails.ContentRelatedToTitle = rowNo["ContentTitle"].ToString();
                        _objSiteContentDetails.ContentEntryDate = Convert.ToDateTime(rowNo["EntryDate"].ToString());
                        _objSiteContentDetails.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                        ViewBag.ContentImageURL = rowNo["ContentImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentImageURL"].ToString();
                    }
                    else if (rowIndex == 1)
                    {
                        ViewBag.ContentDetailID2 = rowNo["ContentDetailID"].ToString();
                        ViewBag.FullName2 = rowNo["FullName"].ToString();
                        ViewBag.ContentDetailDescription = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.EntryDate2 = Convert.ToDateTime(rowNo["EntryDate"].ToString());
                        ViewBag.ContentDetailImageURL = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                    else if (rowIndex == 2)
                    {
                        ViewBag.ContentDetailID3 = rowNo["ContentDetailID"].ToString();
                        ViewBag.FullName3 = rowNo["FullName"].ToString();
                        ViewBag.ContentDetailDescription3 = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle3 = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle3 = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText3 = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.EntryDate3 = Convert.ToDateTime(rowNo["EntryDate"].ToString());
                        ViewBag.ContentDetailImageURL3 = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                    else if (rowIndex == 3)
                    {
                        ViewBag.ContentDetailID4 = rowNo["ContentDetailID"].ToString();
                        ViewBag.FullName4 = rowNo["FullName"].ToString();
                        ViewBag.ContentDetailDescription4 = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle4 = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle4 = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText4 = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.EntryDate4 = Convert.ToDateTime(rowNo["EntryDate"].ToString());
                        ViewBag.ContentDetailImageURL4 = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                    else if (rowIndex == 4)
                    {
                        ViewBag.ContentDetailID5 = rowNo["ContentDetailID"].ToString();
                        ViewBag.FullName5 = rowNo["FullName"].ToString();
                        ViewBag.ContentDetailDescription5 = rowNo["ContentDetailDescription"].ToString();
                        ViewBag.ContentDetailTitle5 = rowNo["ContentDetailTitle"].ToString();
                        ViewBag.ContentDetailSubTitle5 = rowNo["ContentDetailSubTitle"].ToString();
                        ViewBag.ContentIntroductoryText5 = rowNo["ContentIntroductoryText"].ToString();
                        ViewBag.EntryDate5 = Convert.ToDateTime(rowNo["EntryDate"].ToString());
                        ViewBag.ContentDetailImageURL5 = rowNo["ContentDetailImageURL"].ToString() == "" ? "~/ImagesUploaded/750x500.png" : rowNo["ContentDetailImageURL"].ToString();
                    }
                }

                ViewBag.viewTitle = viewTitle;
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult BlogCategory(int contentCategoryID, string cTitle)
        {
            try
            {
                LoginUserInformation.CompanyID = 1; LoginUserInformation.BranchID = 1;
                int applicationID = 2; int relatedToID = 1;
                string viewTitle = "blog";
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = applicationID;
                _objsiteContentCategory.ContentCategoryID = contentCategoryID;
                _objsiteContentRelatedTo.ContentRelatedToID = relatedToID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContent(_objsiteApplicationSetup,
                    _objsiteContentCategory);
                _objSiteContentDetails.DtBlogPostsTopStories = dtContent;
                ViewBag.viewTitle = viewTitle;
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public ActionResult BlogDescription(int contentDetailID, string ctitle)
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objSiteContentDetails.ContentID = contentDetailID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContent(_objSiteContentDetails);
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
                        _objSiteContentDetails.DtContentCategory = _objSiteContentAccessController.GetBlogCategory();
                        ViewBag.ContentCategoryTitle = _objSiteContentAccessController.GetSelectedBlogCategory(_objSiteContentDetails);

                        _objSiteContentDetails.ContentCategoryID= Convert.ToInt32( rowNo["ContentCategoryID"].ToString());
                        LoginUserInformation.CompanyID = 1; LoginUserInformation.BranchID = 1;
                        LoginUserInformation.ApplicationID = 2;
                        _objsiteApplicationSetup = new siteApplicationSetup();
                        _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                        _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                        _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                        DataTable dtContentUnique = _objSiteContentAccessController.GetRelatedContent(_objsiteApplicationSetup,
                    _objSiteContentDetails);
                        _objSiteContentDetails.DtBlogUniqueCategory = dtContentUnique;
                    }
                }

                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public ActionResult BlogDescriptionAdmin(int contentDetailID)
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
        public ActionResult BlogDescriptionAdmin(string actionCancel,string actionReturn, string actionPublish,
            SiteContentDetails objSiteContentDetails, HttpPostedFileBase file)
        {
            try
            {
                if (actionPublish != null)
                {
                    objSiteContentDetails.ActionTypeID = "P";
                    objSiteContentDetails.QualityTag = "N";

                }

                if (actionReturn != null)
                {
                    objSiteContentDetails.ActionTypeID = "B";
                }
                
                if(actionCancel != null)
                {
                    objSiteContentDetails.ActionTypeID = "C";
                }

                _objSiteContentAccessController = new SiteContentAccessController();
                var contentLogIDsql = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentLogID),0)+1 AS ContentLogID from [blogContentPublishMethod]");
                objSiteContentDetails.ContentLogID = contentLogIDsql.AsEnumerable().First().ToString();
                var secquenceNosql = _objdbERPSolutionEntities.Database.SqlQuery<int>("  select ISNULL( MAX( A.[SecquenceNo]),0)+1 AS [SecquenceNo] from [blogContentPublishMethod] A WHERE A.ContentID = '"+ objSiteContentDetails .ContentID+ "';");
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
                return RedirectToAction("Index", "BlogContentApproval");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult BlogSuspend(int contentDetailID)
        {
            try
            {
                SiteContentDetails objSiteContentDetails = new SiteContentDetails();
                objSiteContentDetails.ContentID = contentDetailID;
                objSiteContentDetails.ActionTypeID = "H";
                _objSiteContentAccessController = new SiteContentAccessController();
                var contentLogIDsql = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentLogID),0)+1 AS ContentLogID from [blogContentPublishMethod]");
                objSiteContentDetails.ContentLogID = contentLogIDsql.AsEnumerable().First().ToString();
                var secquenceNosql = _objdbERPSolutionEntities.Database.SqlQuery<int>("  select ISNULL( MAX( A.[SecquenceNo]),0)+1 AS [SecquenceNo] from [blogContentPublishMethod] A WHERE A.ContentID = '" + objSiteContentDetails.ContentID + "';");
                objSiteContentDetails.SecquenceNo = Convert.ToInt32(secquenceNosql.AsEnumerable().First().ToString());
                objSiteContentDetails.EntryUserName = LoginUserInformation.UserID;

                _objSiteContentAccessController.SuspendByAdmin(objSiteContentDetails);
                return RedirectToAction("Index", "BlogContentApproval");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult BlogNotPremier(int contentDetailID)
        {
            try
            {
                SiteContentDetails objSiteContentDetails = new SiteContentDetails();
                objSiteContentDetails.ContentID = contentDetailID;
                objSiteContentDetails.ActionTypeID = "P";
                objSiteContentDetails.QualityTag = "N";
                _objSiteContentAccessController = new SiteContentAccessController();
                var contentLogIDsql = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentLogID),0)+1 AS ContentLogID from [blogContentPublishMethod]");
                objSiteContentDetails.ContentLogID = contentLogIDsql.AsEnumerable().First().ToString();
                var secquenceNosql = _objdbERPSolutionEntities.Database.SqlQuery<int>("  select ISNULL( MAX( A.[SecquenceNo]),0)+1 AS [SecquenceNo] from [blogContentPublishMethod] A WHERE A.ContentID = '" + objSiteContentDetails.ContentID + "';");
                objSiteContentDetails.SecquenceNo = Convert.ToInt32(secquenceNosql.AsEnumerable().First().ToString());
                objSiteContentDetails.EntryUserName = LoginUserInformation.UserID;

                _objSiteContentAccessController.SuspendByAdmin(objSiteContentDetails);
                return RedirectToAction("Index", "PremierList");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult BlogDescriptionPremier(int contentDetailID)
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
        public ActionResult BlogDescriptionPremier(string actionPremier,
            SiteContentDetails objSiteContentDetails, HttpPostedFileBase file)
        {
            try
            {
                if (actionPremier != null)
                {
                    objSiteContentDetails.ActionTypeID = "M";
                    objSiteContentDetails.QualityTag = "M";
                }

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
                return RedirectToAction("Index", "PremierList");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}