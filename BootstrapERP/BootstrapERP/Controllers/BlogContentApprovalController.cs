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
    public class BlogContentApprovalController : Controller
    {
        private SiteContentDetails _objSiteContentDetails;
        private siteApplicationSetup _objsiteApplicationSetup;
        private siteContentType _objsiteContentType;
        private siteContentCategory _objsiteContentCategory;
        private siteContentRelatedTo _objsiteContentRelatedTo;
        private SiteContentAccessController _objSiteContentAccessController;
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: BlogContentApproval
        public ActionResult Index()
        {
            try
            {
                int blogActionDays = 60;
                int contentCategoryID = 1; int relatedToID = 1;
                string viewTitle = "blog";
                _objSiteContentDetails = new SiteContentDetails();
                _objsiteApplicationSetup = new siteApplicationSetup();
                _objsiteContentType = new siteContentType();
                _objsiteContentCategory = new siteContentCategory();
                _objsiteContentRelatedTo = new siteContentRelatedTo();
                _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                _objsiteContentCategory.ContentCategoryID = contentCategoryID;
                _objsiteContentRelatedTo.ContentRelatedToID = relatedToID;
                _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                _objSiteContentAccessController = new SiteContentAccessController();
                DataTable dtContent = _objSiteContentAccessController.ShowdBlogsByDefault(_objsiteApplicationSetup,
                    _objsiteContentCategory, blogActionDays);
                _objSiteContentDetails.DtBlogPostsTopStories = dtContent;
                ViewBag.viewTitle = viewTitle;
                BlogContentDetail objBlogContentDetail = new BlogContentDetail();
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");
                ViewBag.ContentRelatedTo = AddDefaultOption(ViewBag.ContentRelatedTo, "All", null);

                var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Where(a => (a.ActionTypeID != "M")).Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");
                ViewBag.BlogActionType = AddDefaultOption(ViewBag.BlogActionType, "Any", null);

                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string actionSearch, string actionClear, SiteContentDetails objSiteContentDetails,string ContentCategory,string ContentRelatedTo
            ,string BlogActionType)
        {
            try
            {
                if (actionClear != null)
                {
                    return RedirectToAction("Index", "BlogContentApproval");

                }
                else if (actionSearch != null)
                {
                    _objSiteContentDetails = new SiteContentDetails();
                    _objsiteApplicationSetup = new siteApplicationSetup();
                    _objsiteContentType = new siteContentType();
                    _objsiteApplicationSetup.ApplicationID = LoginUserInformation.ApplicationID;
                    _objsiteApplicationSetup.CompanyID = LoginUserInformation.CompanyID;
                    _objsiteApplicationSetup.BranchID = LoginUserInformation.BranchID;
                    _objSiteContentAccessController = new SiteContentAccessController();
                    ContentRelatedTo = ContentRelatedTo == "All" ? "null" : ContentRelatedTo;
                    BlogActionType = BlogActionType == "Any" ? "null" : BlogActionType;

                    _objSiteContentDetails.FromDate = objSiteContentDetails.FromDate == null? string.Empty: Convert.ToDateTime(objSiteContentDetails.FromDate.ToString()).ToShortDateString();
                    _objSiteContentDetails.ToDate = objSiteContentDetails.ToDate == null?string.Empty : Convert.ToDateTime(objSiteContentDetails.ToDate.ToString()).ToShortDateString();

                    DataTable dtContent = _objSiteContentAccessController.GetBlogContentApproval(_objsiteApplicationSetup,
                        ContentCategory, ContentRelatedTo, BlogActionType, objSiteContentDetails);
                    _objSiteContentDetails.DtBlogPostsTopStories = dtContent;


                    var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                    ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle", ContentCategory);

                    int idTemp = Convert.ToInt32(ContentCategory);
                    var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Where(a => a.ContentCategoryID == idTemp).Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle, c.ContentCategoryID });
                    ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle", ContentRelatedTo);
                    ViewBag.ContentRelatedTo = AddDefaultOption(ViewBag.ContentRelatedTo, "All", null);

                    var queryActionType = _objdbERPSolutionEntities.blogActionTypes.Where(a => (a.ActionTypeID != "M")).Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                    ViewBag.BlogActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle", BlogActionType);
                    ViewBag.BlogActionType = AddDefaultOption(ViewBag.BlogActionType, "Any", null);

                    return View(_objSiteContentDetails);
                }
                else
                {
                    return RedirectToAction("Index", "BlogContentApproval");
                }
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        //public ActionResult BlogAction(string contentDetailID)
        //{
        //    return RedirectToAction("Index", "BlogContentApproval");
        //}

        public JsonResult GetContentRelatedTo(string id)
        {
            try
            {
                int idTemp = Convert.ToInt32(id);
                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Where(a => a.ContentCategoryID == idTemp).Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle, c.ContentCategoryID });

                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo, "ContentRelatedToID", "ContentRelatedToTitle");
                ViewBag.ContentRelatedTo = AddDefaultOption(ViewBag.ContentRelatedTo, "All", null);

                return Json(ViewBag.ContentRelatedTo);
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