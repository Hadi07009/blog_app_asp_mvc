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
    public class BlogPostsController : Controller
    {
        private SiteContentDetails _objSiteContentDetails;
        private siteApplicationSetup _objsiteApplicationSetup;
        private siteContentType _objsiteContentType;
        private siteContentCategory _objsiteContentCategory;
        private siteContentRelatedTo _objsiteContentRelatedTo;
        private SiteContentAccessController _objSiteContentAccessController;
        // GET: BlogPosts
        public ActionResult Index()
        {
            try
            {
                if (LoginUserInformation.LogginUserStatus == "Yes")
                {
                    ViewBag.LogginUserStatus = "Yes";

                }
                else
                {
                    ViewBag.LogginUserStatus = "";
                }

                LoginUserInformation.CompanyID = 1; LoginUserInformation.BranchID = 1;
                int applicationID = 2; int contentCategoryID = 2; int relatedToID = 1;
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
                _objSiteContentDetails.BannerURL = _objSiteContentAccessController.GetSiteBanner(_objsiteApplicationSetup);

                DataTable dtContentPremium = _objSiteContentAccessController.GetBlogContentPremium(_objsiteApplicationSetup,
                    _objsiteContentCategory);
                _objSiteContentDetails.DtBlogPremium = dtContentPremium;
                DataTable dtContentTopStories = _objSiteContentAccessController.GetBlogContentTopStories(_objsiteApplicationSetup,
                    _objsiteContentCategory);
                _objSiteContentDetails.DtBlogPostsTopStories = dtContentTopStories;
                DataTable dtContentUnique = _objSiteContentAccessController.GetBlogContent(_objsiteApplicationSetup,
                    _objsiteContentCategory);
                _objSiteContentDetails.DtBlogUniqueCategory = dtContentUnique;
                _objsiteContentCategory.ContentCategoryID = 1;
                DataTable dtContentUnique2 = _objSiteContentAccessController.GetBlogContent(_objsiteApplicationSetup,
                    _objsiteContentCategory);
                _objSiteContentDetails.DtBlogUniqueCategory2 = dtContentUnique2;
                DataTable dtContentWhatsHot = _objSiteContentAccessController.GetBlogContentWhatsHot(_objsiteApplicationSetup,
                    _objsiteContentCategory);
                _objSiteContentDetails.DtBlogPostsWhatsHot = dtContentWhatsHot;

                int quoteType = 0;
                DataTable dtblog = _objSiteContentAccessController.GetBlogQuote(_objsiteApplicationSetup
                    , _objsiteContentCategory, quoteType);
                _objSiteContentDetails.DtblogQuote = dtblog;
                ViewBag.viewTitle = viewTitle;

                _objSiteContentDetails.DtContentCategory = _objSiteContentAccessController.GetBlogCategory();
                foreach (DataRow rowNo in _objSiteContentDetails.DtContentCategory.Rows)
                {
                    int rowIndex = (int)_objSiteContentDetails.DtContentCategory.Rows.IndexOf(rowNo);
                    if (rowIndex == 0)
                    {
                        //ViewBag.ContentCategoryTitle1 = rowNo["ContentCategoryTitle"].ToString();
                        //ViewBag.ContentCategoryID1 = rowNo["ContentCategoryID"].ToString();
                        Session["ContentCategoryTitle1"] = rowNo["ContentCategoryTitle"].ToString();
                        Session["ContentCategoryID1"] = rowNo["ContentCategoryID"].ToString();
                        _objSiteContentDetails.ContentCategoryTitle2 = rowNo["ContentCategoryTitle"].ToString().ToUpper();
                    }
                    else if (rowIndex == 1)
                    {
                        //ViewBag.ContentCategoryTitle2 = rowNo["ContentCategoryTitle"].ToString();
                        //ViewBag.ContentCategoryID2 = rowNo["ContentCategoryID"].ToString();
                        Session["ContentCategoryTitle2"] = rowNo["ContentCategoryTitle"].ToString();
                        Session["ContentCategoryID2"] = rowNo["ContentCategoryID"].ToString();
                        _objSiteContentDetails.ContentCategoryTitle = rowNo["ContentCategoryTitle"].ToString().ToUpper();

                    }
                    else if (rowIndex == 2)
                    {
                        ViewBag.ContentCategoryTitle3 = rowNo["ContentCategoryTitle"].ToString();
                        ViewBag.ContentCategoryID3 = rowNo["ContentCategoryID"].ToString();

                    }
                    else if (rowIndex == 3)
                    {
                        ViewBag.ContentCategoryTitle4 = rowNo["ContentCategoryTitle"].ToString();
                        ViewBag.ContentCategoryID4 = rowNo["ContentCategoryID"].ToString();

                    }
                    else if (rowIndex == 4)
                    {
                        ViewBag.ContentCategoryTitle5 = rowNo["ContentCategoryTitle"].ToString();
                        ViewBag.ContentCategoryID5 = rowNo["ContentCategoryID"].ToString();

                    }
                }

                return View(_objSiteContentDetails);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
    }
}