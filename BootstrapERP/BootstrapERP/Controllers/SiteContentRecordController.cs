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
    public class SiteContentRecordController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();

        private SiteContentDetails _objSiteContentDetails;
        private siteApplicationSetup _objsiteApplicationSetup;
        private siteContentType _objsiteContentType;
        private siteContentCategory _objsiteContentCategory;
        private siteContentRelatedTo _objsiteContentRelatedTo;
        private SiteContentAccessController _objSiteContentAccessController;
        private BlogContentDetail _objBlogContentDetail;
        // GET: SiteContentRecord
        public ActionResult Index()
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objsiteApplicationSetup.EntryUserID = new Guid(LoginUserInformation.UserID);
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup);
                _objSiteContentDetails.DtContentUser = dtContent;
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo, "ContentRelatedToID", "ContentRelatedToTitle");
                ViewBag.ContentRelatedTo = AddDefaultOption(ViewBag.ContentRelatedTo, "All", null);
                

                var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");
                ViewBag.BlogActionType = AddDefaultOption(ViewBag.BlogActionType, "Any", null);
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        private IEnumerable<SelectListItem> AddDefaultOption(IEnumerable<SelectListItem> list, string dataTextField, string selectedValue)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = dataTextField, Value = selectedValue });
            items.AddRange(list);
            return items;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string actionSearch,string actionClear, SiteContentDetails objSiteContentDetails, string ContentCategory, string ContentRelatedTo
            , string BlogActionType)
        {
            try
            {
                if (actionClear != null)
                {
                    return RedirectToAction("Index", "SiteContentRecord");

                }
                else if (actionSearch != null)
                {
                    _objSiteContentDetails = new SiteContentDetails();
                    _objsiteApplicationSetup = new siteApplicationSetup();
                    _objsiteContentType = new siteContentType();
                    _objsiteContentCategory = new siteContentCategory();
                    _objsiteContentRelatedTo = new siteContentRelatedTo();
                    _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                    _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                    _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                    _objsiteApplicationSetup.EntryUserID = new Guid(LoginUserInformation.UserID);
                    _objSiteContentAccessController = new SiteContentAccessController();
                    ContentRelatedTo = ContentRelatedTo == "All" ? "null" : ContentRelatedTo;
                    BlogActionType = BlogActionType == "Any" ? "null" : BlogActionType;
                    DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup
                        , ContentCategory, ContentRelatedTo, BlogActionType, objSiteContentDetails);
                    _objSiteContentDetails.DtContentUser = dtContent;
                    var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                    ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle", ContentCategory);

                    int idTemp = Convert.ToInt32(ContentCategory);
                    var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Where(a => a.ContentCategoryID == idTemp).Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle, c.ContentCategoryID });
                    ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle", ContentRelatedTo);
                    ViewBag.ContentRelatedTo = AddDefaultOption(ViewBag.ContentRelatedTo, "All", null);

                    var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                    ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle", BlogActionType);
                    ViewBag.BlogActionType = AddDefaultOption(ViewBag.BlogActionType, "Any", null);
                    return View(_objSiteContentDetails);
                }
                else
                {
                    return RedirectToAction("Index", "SiteContentRecord");
                }
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult Index_1()
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objsiteApplicationSetup.EntryUserID = new Guid(LoginUserInformation.UserID);
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup);
                _objSiteContentDetails.DtContentUser = dtContent;
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");

                var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult Index_2()
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objsiteApplicationSetup.EntryUserID = new Guid(LoginUserInformation.UserID);
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup);
                _objSiteContentDetails.DtContentUser = dtContent;
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");

                var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult Index_3()
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objsiteApplicationSetup.EntryUserID = new Guid(LoginUserInformation.UserID);
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup);
                _objSiteContentDetails.DtContentUser = dtContent;
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");

                var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult Index_4()
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objsiteApplicationSetup.EntryUserID = new Guid(LoginUserInformation.UserID);
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup);
                _objSiteContentDetails.DtContentUser = dtContent;
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");

                var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult Index_5()
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objsiteApplicationSetup.EntryUserID = new Guid(LoginUserInformation.UserID);
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup);
                _objSiteContentDetails.DtContentUser = dtContent;
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");

                var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
        public ActionResult DeleteContentRecord(int id)
        {
            try
            {
                SiteContentRecordEntryController objSiteContentRecordEntryController = new SiteContentRecordEntryController();
                BlogContentDetail objBlogContentDetail = new BlogContentDetail();
                objBlogContentDetail.ContentID = id.ToString();
                DataTable dtContentHeader = objSiteContentRecordEntryController.GetBlogContent(objBlogContentDetail);
                foreach (DataRow rowNo in dtContentHeader.Rows)
                {
                    var queryBlogCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                    ViewBag.ContentCategory = new SelectList(queryBlogCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle", rowNo["ContentCategoryID"].ToString());

                    var queryaBlogRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                    ViewBag.ContentRelatedTo = new SelectList(queryaBlogRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle", rowNo["ContentRelatedToID"].ToString());
                    objBlogContentDetail.ContentDetailTitle = rowNo["ContentDetailTitle"].ToString() == null ? "" : rowNo["ContentDetailTitle"].ToString();
                    objBlogContentDetail.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString() == null ? "" : rowNo["ContentDetailSubTitle"].ToString();
                    objBlogContentDetail.ContentIntroductoryText = rowNo["ContentIntroductoryText"].ToString() == null ? "" : rowNo["ContentIntroductoryText"].ToString();
                    objBlogContentDetail.ContentDetailDescription = rowNo["ContentDetailDescription"].ToString() == null ? "" : rowNo["ContentDetailDescription"].ToString();

                    ViewBag.ContentImageURL = rowNo["ContentDetailImageURL"].ToString() == string.Empty ? null : rowNo["ContentDetailImageURL"].ToString();
                    Session["ContentImageURL"] = rowNo["ContentDetailImageURL"].ToString();
                }

                return View(objBlogContentDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost, ActionName("DeleteContentRecord")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SiteContentRecordEntryController objSiteContentRecordEntryController = new SiteContentRecordEntryController();
                
                _objBlogContentDetail = new BlogContentDetail();
                _objBlogContentDetail.ContentID = id.ToString();
                _objBlogContentDetail.EntryUserName = LoginUserInformation.UserID;
                objSiteContentRecordEntryController.DeleteBlog(_objBlogContentDetail);
                return RedirectToAction("Index", "SiteContentRecord");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult ShowBlogRecord(int id)
        {
            try
            {
                SiteContentRecordEntryController objSiteContentRecordEntryController = new SiteContentRecordEntryController();
                BlogContentDetail objBlogContentDetail = new BlogContentDetail();
                objBlogContentDetail.ContentID = id.ToString();
                DataTable dtContentHeader = objSiteContentRecordEntryController.GetBlogContent(objBlogContentDetail);
                foreach (DataRow rowNo in dtContentHeader.Rows)
                {
                    var queryBlogCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                    ViewBag.ContentCategory = new SelectList(queryBlogCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle", rowNo["ContentCategoryID"].ToString());

                    var queryaBlogRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                    ViewBag.ContentRelatedTo = new SelectList(queryaBlogRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle", rowNo["ContentRelatedToID"].ToString());
                    objBlogContentDetail.ContentDetailTitle = rowNo["ContentDetailTitle"].ToString() == null ? "" : rowNo["ContentDetailTitle"].ToString();
                    objBlogContentDetail.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString() == null ? "" : rowNo["ContentDetailSubTitle"].ToString();
                    objBlogContentDetail.ContentIntroductoryText = rowNo["ContentIntroductoryText"].ToString() == null ? "" : rowNo["ContentIntroductoryText"].ToString();
                    objBlogContentDetail.ContentDetailDescription = rowNo["ContentDetailDescription"].ToString() == null ? "" : rowNo["ContentDetailDescription"].ToString();

                    ViewBag.ContentImageURL = rowNo["ContentDetailImageURL"].ToString() == string.Empty ? null : rowNo["ContentDetailImageURL"].ToString();
                    Session["ContentImageURL"] = rowNo["ContentDetailImageURL"].ToString();
                }

                return View(objBlogContentDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public ActionResult EditBlogRecord(int id)
        {
            try
            {
                SiteContentRecordEntryController objSiteContentRecordEntryController = new SiteContentRecordEntryController();
                _objBlogContentDetail = new BlogContentDetail();
                _objBlogContentDetail.ContentID = id.ToString();
                DataTable dtContentHeader = objSiteContentRecordEntryController.GetBlogContent(_objBlogContentDetail);
                foreach (DataRow rowNo in dtContentHeader.Rows)
                {
                    var queryBlogCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                    ViewBag.ContentCategory = new SelectList(queryBlogCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle", rowNo["ContentCategoryID"].ToString());

                    var queryaBlogRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                    ViewBag.ContentRelatedTo = new SelectList(queryaBlogRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle", rowNo["ContentRelatedToID"].ToString());
                    _objBlogContentDetail.ContentDetailTitle = rowNo["ContentDetailTitle"].ToString() == null ? "" : rowNo["ContentDetailTitle"].ToString();
                    _objBlogContentDetail.ContentDetailSubTitle = rowNo["ContentDetailSubTitle"].ToString() == null ? "" : rowNo["ContentDetailSubTitle"].ToString();
                    _objBlogContentDetail.ContentIntroductoryText = rowNo["ContentIntroductoryText"].ToString() == null ? "" : rowNo["ContentIntroductoryText"].ToString();
                    _objBlogContentDetail.ContentDetailDescription = rowNo["ContentDetailDescription"].ToString() == null ? "" : rowNo["ContentDetailDescription"].ToString();

                    ViewBag.ContentImageURL = rowNo["ContentDetailImageURL"].ToString() == string.Empty ? null : rowNo["ContentDetailImageURL"].ToString();
                    Session["ContentImageURL"] = rowNo["ContentDetailImageURL"].ToString();
                }

                return View(_objBlogContentDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBlogRecord(string contentSave, string addtoSubmitList, string ContentCategory, string ContentRelatedTo,
            BlogContentDetail objBlogContentDetail,HttpPostedFileBase file)
        {
            _objSiteContentDetails = new SiteContentDetails();
            _objSiteContentDetails.EntryUserName = LoginUserInformation.UserID;
            _objSiteContentDetails.ApplicationID = LoginUserInformation.ApplicationID;
            _objSiteContentDetails.CompanyID = LoginUserInformation.CompanyID;
            _objSiteContentDetails.BranchID = LoginUserInformation.BranchID;

            objBlogContentDetail.ContentDetailImageURL = Session["ContentImageURL"].ToString();


            #region ImageUpload
            string fileContent = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                string relativePath = "~/ImagesUploaded/" + Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath(relativePath);
                file.SaveAs(physicalPath);
                objBlogContentDetail.ContentDetailImageURL = relativePath;
            }

            #endregion ImageUpload


            siteContentHeaderEntryController objsiteContentHeaderEntryController = new siteContentHeaderEntryController();
            if (contentSave != null)
            {
                _objSiteContentDetails.ActionTypeID = "A";
                objsiteContentHeaderEntryController.UpdateDetail(objBlogContentDetail, _objSiteContentDetails);
            }

            if (addtoSubmitList != null)
            {
                _objSiteContentDetails.ActionTypeID = "S";
                var contentLogIDsql = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentLogID),0)+1 AS ContentLogID from [blogContentPublishMethod]");
                _objSiteContentDetails.ContentLogID = contentLogIDsql.AsEnumerable().First().ToString();
                var secquenceNosql = _objdbERPSolutionEntities.Database.SqlQuery<int>("  select ISNULL( MAX( A.[SecquenceNo]),0)+1 AS [SecquenceNo] from [blogContentPublishMethod] A WHERE A.ContentID = '" + objBlogContentDetail.ContentID + "';");
                _objSiteContentDetails.SecquenceNo = Convert.ToInt32(secquenceNosql.AsEnumerable().First().ToString());
                objsiteContentHeaderEntryController.UpdateDetail(objBlogContentDetail, _objSiteContentDetails);
            }

            return RedirectToAction("Index", "SiteContentRecord");
        }
        public ActionResult EditContentRecord(int id, string dataUsed)
        {
            try
            {
                SiteContentRecordEntryController objSiteContentRecordEntryController = new SiteContentRecordEntryController();
                SiteContentHeader objSiteContentHeader = new SiteContentHeader();
                objSiteContentHeader.ContentID = id;

                DataTable dtContentHeader = objSiteContentRecordEntryController.GetContentHeader(objSiteContentHeader);
                foreach (DataRow rowNo in dtContentHeader.Rows)
                {
                    var queryApplication = _objdbERPSolutionEntities.siteApplicationSetups.Select(c => new { c.ApplicationID, c.ApplicationTitle });
                    ViewBag.ApplicationTitle = new SelectList(queryApplication.AsEnumerable(), "ApplicationID", "ApplicationTitle", rowNo["ApplicationID"].ToString());

                    var queryContentType = _objdbERPSolutionEntities.siteContentTypes.Select(c => new { c.ContentTypeID, c.ContentTypeDescription });
                    ViewBag.ContentType = new SelectList(queryContentType.AsEnumerable(), "ContentTypeID", "ContentTypeDescription", rowNo["ContentTypeID"].ToString());

                    var queryContentCategory = _objdbERPSolutionEntities.siteContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                    ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle", rowNo["ContentCategoryID"].ToString());
                    var queryContentRelatedTo = _objdbERPSolutionEntities.siteContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentTitle });
                    ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentTitle", rowNo["ContentRelatedToID"].ToString());

                    ViewBag.ContentImageURL = rowNo["ContentImageURL"].ToString() == string.Empty ? null : rowNo["ContentImageURL"].ToString();
                    Session["ContentImageURL"] = rowNo["ContentImageURL"].ToString();
                }
                //data for temp
                objSiteContentHeader.EntryUserName = LoginUserInformation.UserID;
                if (dataUsed == "I")
                {
                    siteContentEntryTempController objsiteContentEntryTempController = new siteContentEntryTempController();
                    objsiteContentEntryTempController.Delete(objSiteContentHeader);
                    objSiteContentRecordEntryController.ImportContentDetailsTemp(objSiteContentHeader);
                }

                Session["selectedContentID"] = id;
                return View(_objdbERPSolutionEntities.siteContentEntryTemps.Where(x => x.EntryUserID == new Guid(objSiteContentHeader.EntryUserName)).ToList());
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContentRecord(string contentSave, string ApplicationTitle, string ContentType, string ContentCategory, string ContentRelatedTo,
            HttpPostedFileBase file)
        {
            try
            {
                SiteContentHeader objSiteContentHeader = new SiteContentHeader();
                objSiteContentHeader.CompanyID = 1;
                objSiteContentHeader.BranchID = 1;
                objSiteContentHeader.ContentID = Convert.ToInt32(Session["selectedContentID"]);
                objSiteContentHeader.EntryUserName = LoginUserInformation.UserID;
                siteApplicationSetup objsiteApplicationSetup = new siteApplicationSetup();
                objsiteApplicationSetup.ApplicationID = Convert.ToInt32(ApplicationTitle);
                siteContentType objsiteContentType = new siteContentType();
                objsiteContentType.ContentTypeID = Convert.ToInt32(ContentType);
                siteContentCategory objsiteContentCategory = new siteContentCategory();
                objsiteContentCategory.ContentCategoryID = Convert.ToInt32(ContentCategory);
                siteContentRelatedTo objsiteContentRelatedTo = new siteContentRelatedTo();
                objsiteContentRelatedTo.ContentRelatedToID = Convert.ToInt32(ContentRelatedTo);

                objSiteContentHeader.ContentImageURL = Session["ContentImageURL"].ToString();
                #region ImageUpload
                string fileContent = string.Empty;
                if (file != null && file.ContentLength > 0)
                {
                    string relativePath = "~/ImagesUploaded/" + Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath(relativePath);
                    file.SaveAs(physicalPath);
                    objSiteContentHeader.ContentImageURL = relativePath;
                }

                #endregion ImageUpload
                siteContentHeaderEntryController objsiteContentHeaderEntryController = new siteContentHeaderEntryController();
                objsiteContentHeaderEntryController.DeleteContentHeaderEntry(objSiteContentHeader);
                objsiteContentHeaderEntryController.Save(objSiteContentHeader, objsiteApplicationSetup, objsiteContentType
                    , objsiteContentCategory, objsiteContentRelatedTo);
                return RedirectToAction("Index");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult CreateContentRecord()
        {
            ViewBag.selectedContentIDBack = Convert.ToInt32(Session["selectedContentID"]);

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateContentRecord(siteContentEntryTemp objsiteContentEntryTemp, HttpPostedFileBase uploadFile)
        {
            if (!ModelState.IsValid)
                return View();

            #region ImageUpload
            string fileContent = string.Empty;
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                string relativePath = "~/ImagesUploaded/" + Path.GetFileName(uploadFile.FileName);
                string physicalPath = Server.MapPath(relativePath);
                uploadFile.SaveAs(physicalPath);
                objsiteContentEntryTemp.contentImageURL = relativePath;
            }


            #endregion ImageUpload

            string contentDescription = objsiteContentEntryTemp.contentDescription == null ? "" : objsiteContentEntryTemp.contentDescription.Replace("'", "''");
            string contentTitle = objsiteContentEntryTemp.contentTitle == null ? "" : objsiteContentEntryTemp.contentTitle.Replace("'", "''");
            string contentSubTitle = objsiteContentEntryTemp.contentSubTitle == null ? "" : objsiteContentEntryTemp.contentSubTitle.Replace("'", "''");
            string introductoryText = objsiteContentEntryTemp.introductoryText == null ? "" : objsiteContentEntryTemp.introductoryText.Replace("'", "''");

            var storedProcedureComandText = @"INSERT INTO [siteContentEntryTemp]([contentTitle],
           [contentDescription],[DataUsed],[EntryUserID],[contentImageURL],[contentSubTitle],[introductoryText])
            VALUES ( '" + contentTitle + "'" +
            ",'" + contentDescription + "'" +
            ",'" + "A" + "'" +
            ",'" + LoginUserInformation.UserID + "'" +
            ",'" + objsiteContentEntryTemp.contentImageURL + "'" +
            ",'" + contentSubTitle + "'" +
            ",'" + introductoryText + "'" +
            ");";
            siteContentEntryTempController objsiteContentEntryTempController = new siteContentEntryTempController();
            objsiteContentEntryTempController.Save(storedProcedureComandText);
            return RedirectToAction("EditContentRecord", "SiteContentRecord", new { id = Convert.ToInt32(Session["selectedContentID"]), dataUsed = "A" });

            //return View();
        }

        public ActionResult DeleteContentDetail(int id)
        {
            ViewBag.selectedContentIDBack = Convert.ToInt32(Session["selectedContentID"]);
            siteContentEntryTemp objsiteContentEntryTemp = _objdbERPSolutionEntities.siteContentEntryTemps.Find(id);
            if (objsiteContentEntryTemp == null)
            {
                return HttpNotFound();
            }
            return View(objsiteContentEntryTemp);
        }


        [HttpPost, ActionName("DeleteContentDetail")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedDetail(int id)
        {
            siteContentEntryTemp objsiteContentEntryTemp = _objdbERPSolutionEntities.siteContentEntryTemps.Find(id);
            _objdbERPSolutionEntities.siteContentEntryTemps.Remove(objsiteContentEntryTemp);
            _objdbERPSolutionEntities.SaveChanges();
            return RedirectToAction("EditContentRecord", "SiteContentRecord", new { id = Convert.ToInt32(Session["selectedContentID"]), dataUsed = "A" });
        }


        public ActionResult EditContentDetail(int id)
        {
            var contentEdit = (from m in _objdbERPSolutionEntities.siteContentEntryTemps

                               where m.contentID == id

                               select m).First();

            ViewBag.selectedContentIDBack = Convert.ToInt32(Session["selectedContentID"]);
            return View(contentEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContentDetail([Bind(Include = "contentID,contentTitle,contentDescription,contentImageURL,contentSubTitle,introductoryText")] siteContentEntryTemp objsiteContentEntryTemp, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                #region ImageUpload

                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    string relativePath = "~/ImagesUploaded/" + Path.GetFileName(uploadFile.FileName);
                    string physicalPath = Server.MapPath(relativePath);
                    uploadFile.SaveAs(physicalPath);
                    objsiteContentEntryTemp.contentImageURL = relativePath;
                }

                #endregion ImageUpload

                string contentDescription = objsiteContentEntryTemp.contentDescription == null ? "" : objsiteContentEntryTemp.contentDescription.Replace("'", "''");
                string contentTitle = objsiteContentEntryTemp.contentTitle == null ? "" : objsiteContentEntryTemp.contentTitle.Replace("'", "''");
                string contentSubTitle = objsiteContentEntryTemp.contentSubTitle == null ? "" : objsiteContentEntryTemp.contentSubTitle.Replace("'", "''");
                string introductoryText = objsiteContentEntryTemp.introductoryText == null ? "" : objsiteContentEntryTemp.introductoryText.Replace("'", "''");

                var storedProcedureComandText = @"UPDATE [siteContentEntryTemp] SET" +
                " [contentTitle] = '" + contentTitle + "'" +
               " ,[contentDescription] = '" + contentDescription + "'" +
               " ,[EntryUserID] = '" + LoginUserInformation.UserID + "'" +
               " ,[contentImageURL] = '" + objsiteContentEntryTemp.contentImageURL + "'" +
               " ,[contentSubTitle] = '" + contentSubTitle + "'" +
               " ,[introductoryText] = '" + introductoryText + "'" +
               " WHERE contentID = " + objsiteContentEntryTemp.contentID + ";";
                siteContentEntryTempController objsiteContentEntryTempController = new siteContentEntryTempController();
                objsiteContentEntryTempController.Update(storedProcedureComandText);
                return RedirectToAction("EditContentRecord", "SiteContentRecord", new { id = Convert.ToInt32(Session["selectedContentID"]), dataUsed = "A" });
            }
            return View(objsiteContentEntryTemp);
        }


    }
}