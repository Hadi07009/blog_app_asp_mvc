using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class SearchArticleController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private SiteContentDetails _objSiteContentDetails;
        private siteApplicationSetup _objsiteApplicationSetup;
        private SiteContentAccessController _objSiteContentAccessController;
        // GET: SearchArticle
        public ActionResult Index()
        {
            try
            {
                _objSiteContentDetails = new SiteContentDetails();
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo, "ContentRelatedToID", "ContentRelatedToTitle");
                ViewBag.ContentRelatedTo = AddDefaultOption(ViewBag.ContentRelatedTo, "All", null);
                _objSiteContentDetails.DtBlogPostsTopStories = null;
                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string actionSearch, string actionClear
            , SiteContentDetails objSiteContentDetails, string ContentCategory, string ContentRelatedTo)
        {
            try
            {
                if (actionClear != null)
                {
                    return RedirectToAction("Index", "SearchArticle");

                }
                else if (actionSearch != null)
                {
                    _objSiteContentDetails = new SiteContentDetails();
                    _objsiteApplicationSetup = new siteApplicationSetup();
                    _objsiteApplicationSetup.CompanyID = 1;
                    _objsiteApplicationSetup.BranchID = 1;
                    _objSiteContentAccessController = new SiteContentAccessController();
                    ContentRelatedTo = ContentRelatedTo == "All" ? "null" : ContentRelatedTo;
                    DataTable dtContent = _objSiteContentAccessController.GetBlogContentUser(_objsiteApplicationSetup
                        , ContentCategory, ContentRelatedTo, objSiteContentDetails);
                    _objSiteContentDetails.DtBlogPostsTopStories = dtContent;

                    var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                    ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle", ContentCategory);

                    int idTemp = Convert.ToInt32(ContentCategory);
                    var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Where(a => a.ContentCategoryID == idTemp).Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle, c.ContentCategoryID });
                    ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle", ContentRelatedTo);
                    ViewBag.ContentRelatedTo = AddDefaultOption(ViewBag.ContentRelatedTo, "All", null);
                    return View(_objSiteContentDetails);
                }
                else
                {
                    return RedirectToAction("Index", "SearchArticle");
                }
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
        
    }
}