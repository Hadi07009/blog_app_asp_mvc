using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class SiteContentEntryController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: SiteContentEntry
        //private BlogContentAccessController _objBlogContentAccessController;
        //private SiteContentDetails _objSiteContentDetails;
        public ActionResult Index()
        {
            try
            {
                BlogContentDetail objBlogContentDetail = new BlogContentDetail();
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");
                
                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");

                return View(objBlogContentDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string contentSave, string addtoSubmitList, string ContentCategory, string ContentRelatedTo, BlogContentDetail objBlogContentDetail
            , HttpPostedFileBase file
            )
        {
            try
            {
                objBlogContentDetail.CompanyID = LoginUserInformation.CompanyID;
                objBlogContentDetail.BranchID = LoginUserInformation.BranchID;
                objBlogContentDetail.EntryUserName = LoginUserInformation.UserID;
                objBlogContentDetail.ApplicationID = LoginUserInformation.ApplicationID;

                objBlogContentDetail.ContentCategoryID = Convert.ToInt32(ContentCategory);
                objBlogContentDetail.ContentRelatedToID = Convert.ToInt32(ContentRelatedTo);
                ViewBag.contentIDExists = 0;
                siteContentHeaderEntryController objsiteContentHeaderEntryController = new siteContentHeaderEntryController();
                if (ModelState.IsValid)
                {
                    bool contentAlreadyExists = true;
                    contentAlreadyExists = objsiteContentHeaderEntryController.Checkcontent(objBlogContentDetail);
                    if (contentAlreadyExists)
                    {
                        int contentIDCheck = objsiteContentHeaderEntryController.CheckcontentID(objBlogContentDetail);
                        objBlogContentDetail.ContentParentID = contentIDCheck.ToString();
                    }
                    else
                    {
                        var contentParentIDSQL = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentParentID),0)+1 AS ContentID from [blogContentHeader]");
                        objBlogContentDetail.ContentParentID = contentParentIDSQL.AsEnumerable().First().ToString();
                    objsiteContentHeaderEntryController.SaveHeader(objBlogContentDetail);

                    }


                    var contentIDSQL = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentID),0)+1 AS ContentID from [blogContentDetail]");
                    objBlogContentDetail.ContentID = contentIDSQL.AsEnumerable().First().ToString();
                    var contentSecquenceIDSQL = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( SequenceNo),0)+1 AS ContentID from [blogContentDetail]");
                    objBlogContentDetail.SequenceNo = Convert.ToInt32(contentSecquenceIDSQL.AsEnumerable().First().ToString());
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

                    SiteContentDetails objSiteContentDetails = new SiteContentDetails();

                    if (contentSave != null)
                    {
                        objSiteContentDetails.ActionTypeID = "A";
                        objsiteContentHeaderEntryController.SaveDetail(objBlogContentDetail, objSiteContentDetails);
                    }

                    if (addtoSubmitList != null)
                    {
                        objSiteContentDetails.ActionTypeID = "S";
                        var contentLogIDsql = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentLogID),0)+1 AS ContentLogID from [blogContentPublishMethod]");
                        objSiteContentDetails.ContentLogID = contentLogIDsql.AsEnumerable().First().ToString();
                        var secquenceNosql = _objdbERPSolutionEntities.Database.SqlQuery<int>("  select ISNULL( MAX( A.[SecquenceNo]),0)+1 AS [SecquenceNo] from [blogContentPublishMethod] A WHERE A.ContentID = '" + objBlogContentDetail.ContentID + "';");
                        objSiteContentDetails.SecquenceNo = Convert.ToInt32(secquenceNosql.AsEnumerable().First().ToString());
                        objsiteContentHeaderEntryController.SaveDetail(objBlogContentDetail, objSiteContentDetails);
                    }

                }

                return RedirectToAction("Index", "SiteContentRecord");

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        public ActionResult ContentEntry()
        {
            try
            {
                BlogContentDetail objBlogContentDetail = new BlogContentDetail();
                var queryContentCategory = _objdbERPSolutionEntities.blogContentCategories.Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle });
                ViewBag.ContentCategory = new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle");

                var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle });
                ViewBag.ContentRelatedTo = new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle");

                return View(objBlogContentDetail);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult ContentEntry(string contentSave, string addtoSubmitList, string ContentCategory, string ContentRelatedTo, BlogContentDetail objBlogContentDetail
            , HttpPostedFileBase file
            )
        {
            try
            {
                objBlogContentDetail.CompanyID = LoginUserInformation.CompanyID;
                objBlogContentDetail.BranchID = LoginUserInformation.BranchID;
                objBlogContentDetail.EntryUserName = LoginUserInformation.UserID;
                objBlogContentDetail.ApplicationID = LoginUserInformation.ApplicationID;

                objBlogContentDetail.ContentCategoryID = Convert.ToInt32(ContentCategory);
                objBlogContentDetail.ContentRelatedToID = Convert.ToInt32(ContentRelatedTo);
                ViewBag.contentIDExists = 0;
                siteContentHeaderEntryController objsiteContentHeaderEntryController = new siteContentHeaderEntryController();
                if (ModelState.IsValid)
                {
                    bool contentAlreadyExists = true;
                    contentAlreadyExists = objsiteContentHeaderEntryController.Checkcontent(objBlogContentDetail);
                    if (contentAlreadyExists)
                    {
                        int contentIDCheck = objsiteContentHeaderEntryController.CheckcontentID(objBlogContentDetail);
                        objBlogContentDetail.ContentParentID = contentIDCheck.ToString();
                    }
                    else
                    {
                        var contentParentIDSQL = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentParentID),0)+1 AS ContentID from [blogContentHeader]");
                        objBlogContentDetail.ContentParentID = contentParentIDSQL.AsEnumerable().First().ToString();
                        objsiteContentHeaderEntryController.SaveHeader(objBlogContentDetail);

                    }


                    var contentIDSQL = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentID),0)+1 AS ContentID from [blogContentDetail]");
                    objBlogContentDetail.ContentID = contentIDSQL.AsEnumerable().First().ToString();
                    var contentSecquenceIDSQL = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( SequenceNo),0)+1 AS ContentID from [blogContentDetail]");
                    objBlogContentDetail.SequenceNo = Convert.ToInt32(contentSecquenceIDSQL.AsEnumerable().First().ToString());
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

                    SiteContentDetails objSiteContentDetails = new SiteContentDetails();

                    if (contentSave != null)
                    {
                        objSiteContentDetails.ActionTypeID = "A";
                        objsiteContentHeaderEntryController.SaveDetail(objBlogContentDetail, objSiteContentDetails);
                    }

                    if (addtoSubmitList != null)
                    {
                        objSiteContentDetails.ActionTypeID = "S";
                        var contentLogIDsql = _objdbERPSolutionEntities.Database.SqlQuery<int>("select ISNULL( MAX( ContentLogID),0)+1 AS ContentLogID from [blogContentPublishMethod]");
                        objSiteContentDetails.ContentLogID = contentLogIDsql.AsEnumerable().First().ToString();
                        var secquenceNosql = _objdbERPSolutionEntities.Database.SqlQuery<int>("  select ISNULL( MAX( A.[SecquenceNo]),0)+1 AS [SecquenceNo] from [blogContentPublishMethod] A WHERE A.ContentID = '" + objBlogContentDetail.ContentID + "';");
                        objSiteContentDetails.SecquenceNo = Convert.ToInt32(secquenceNosql.AsEnumerable().First().ToString());
                        objsiteContentHeaderEntryController.SaveDetail(objBlogContentDetail, objSiteContentDetails);
                    }

                }

                return RedirectToAction("Index", "SiteContentRecord");

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        //public ActionResult CreateContent(string ApplicationTitle, string ContentType, string ContentCategory, string ContentRelatedTo)
        //{
        //    try
        //    {
        //        siteContentHeaderEntryController objsiteContentHeaderEntryController = new siteContentHeaderEntryController();
        //        SiteContentHeader objSiteContentHeader = new SiteContentHeader();
        //        objSiteContentHeader.EntryUserName = "417BE38C-005D-4367-8DE6-4F2C70EF7B98";
        //        siteApplicationSetup objsiteApplicationSetup = new siteApplicationSetup();
        //        objsiteApplicationSetup.ApplicationID = Convert.ToInt32(ApplicationTitle);
        //        siteContentType objsiteContentType = new siteContentType();
        //        objsiteContentType.ContentTypeID = Convert.ToInt32(ContentType);
        //        siteContentCategory objsiteContentCategory = new siteContentCategory();
        //        objsiteContentCategory.ContentCategoryID = Convert.ToInt32(ContentCategory);
        //        siteContentRelatedTo objsiteContentRelatedTo = new siteContentRelatedTo();
        //        objsiteContentRelatedTo.ContentRelatedToID = Convert.ToInt32(ContentRelatedTo);
        //        objsiteContentHeaderEntryController.SaveTempData(objSiteContentHeader, objsiteApplicationSetup, objsiteContentType
        //            , objsiteContentCategory, objsiteContentRelatedTo);
        //        return View();
        //    }
        //    catch (Exception msgException)
        //    {

        //        throw msgException;
        //    }
        //}
        public ActionResult CreateContent()
        {
            try
            {

                return View();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateContent(string addtoList, string addtoSubmitList, siteContentEntryTemp objsiteContentEntryTemp, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View();

            #region ImageUpload
            string fileContent = string.Empty;

            if (file != null && file.ContentLength > 0)
            {
                string relativePath = "~/ImagesUploaded/" + Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath(relativePath);
                file.SaveAs(physicalPath);
                objsiteContentEntryTemp.contentImageURL = relativePath;
            }

            #endregion ImageUpload

            string contentTitle = objsiteContentEntryTemp.contentTitle == null ? "" : objsiteContentEntryTemp.contentTitle.Replace("'", "''");
            string contentDescription = objsiteContentEntryTemp.contentDescription == null ? "" : objsiteContentEntryTemp.contentDescription.Replace("'", "''");
            string contentSubTitle = objsiteContentEntryTemp.contentSubTitle == null ? "" : objsiteContentEntryTemp.contentSubTitle.Replace("'", "''");
            string introductoryText = objsiteContentEntryTemp.introductoryText == null ? "" : objsiteContentEntryTemp.introductoryText.Replace("'", "''");

            if (addtoList != null)
            {
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
                return RedirectToAction("Index", "SiteContentEntry", new { dataUse = "A" });
            }
            else if (addtoSubmitList != null)
            {
                SiteContentHeader objSiteContentHeader = new SiteContentHeader();
                siteContentHeaderEntryController objsiteContentHeaderEntryController = new siteContentHeaderEntryController();
                objSiteContentHeader.ContentID = Convert.ToInt32(Session["contentIDHeader"].ToString());
                objSiteContentHeader.CompanyID = LoginUserInformation.CompanyID;
                objSiteContentHeader.BranchID = LoginUserInformation.BranchID;
                objSiteContentHeader.EntryUserName = LoginUserInformation.UserID;

                SiteContentDetails objSiteContentDetails = new SiteContentDetails();
                objSiteContentDetails.BlogID = "";

                var storedProcedureComandText = @"INSERT INTO [siteContentDetail]([ContentID],[ContentDetailID],[ContentDetailTitle],[ContentDetailImageURL],[ContentDetailDescription]
               ,[DataUsed],[EntryDate],[EntryUserID],[ContentDetailSubTitle],[ContentIntroductoryText],[BlogID])
                VALUES ( " + objSiteContentHeader.ContentID + "" +
                "," + objSiteContentDetails.BlogID + "" +
                ",'" + contentTitle + "'" +
                ",'" + objsiteContentEntryTemp.contentImageURL + "'" +
                ",'" + contentDescription + "'" +
                ",'" + "S" + "'" +
                ",'" + "CAST(GETDATE() AS DateTime)" + "'" +
                ",'" + LoginUserInformation.UserID + "'" +
                ",'" + contentSubTitle + "'" +
                ",'" + introductoryText + "'" +
                ",'" + objSiteContentDetails.BlogID + "'" +
                ");";
                siteContentEntryTempController objsiteContentEntryTempController = new siteContentEntryTempController();
                objsiteContentEntryTempController.Save(storedProcedureComandText);

                objsiteContentHeaderEntryController.SaveDetail(objSiteContentHeader);
                return RedirectToAction("Index", "SiteContentEntry", new { dataUse = "A" });
            }
            else
            {
                return RedirectToAction("Index", "SiteContentEntry", new { dataUse = "A" });
            }


        }

        public ActionResult DeleteContent(int id)
        {
            siteContentEntryTemp objsiteContentEntryTemp = _objdbERPSolutionEntities.siteContentEntryTemps.Find(id);
            if (objsiteContentEntryTemp == null)
            {
                return HttpNotFound();
            }
            return View(objsiteContentEntryTemp);
        }

        [HttpPost, ActionName("DeleteContent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siteContentEntryTemp objsiteContentEntryTemp = _objdbERPSolutionEntities.siteContentEntryTemps.Find(id);
            _objdbERPSolutionEntities.siteContentEntryTemps.Remove(objsiteContentEntryTemp);
            _objdbERPSolutionEntities.SaveChanges();
            return RedirectToAction("Index", "SiteContentEntry", new { dataUse = "A" });
            //return RedirectToAction("Index");
        }


        public ActionResult EditContent(int id)
        {
            var contentEdit = (from m in _objdbERPSolutionEntities.siteContentEntryTemps

                               where m.contentID == id

                               select m).First();

            return View(contentEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContent([Bind(Include = "contentID,contentTitle,contentDescription,contentImageURL,contentSubTitle,introductoryText")] siteContentEntryTemp objsiteContentEntryTemp, HttpPostedFileBase uploadFile)
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
                return RedirectToAction("Index", "SiteContentEntry", new { dataUse = "A" });

            }
            return View(objsiteContentEntryTemp);
        }

        public JsonResult GetContentType(string id)
        {
            int idTemp = Convert.ToInt32(id);
            var queryContentType = _objdbERPSolutionEntities.siteContentTypes.Where(a => a.ApplicationID == idTemp).Select(c => new { c.ContentTypeID, c.ContentTypeDescription, c.ApplicationID });
            return Json(new SelectList(queryContentType.AsEnumerable(), "ContentTypeID", "ContentTypeDescription"));
        }

        public JsonResult GetContentCategory(string id)
        {
            int idTemp = Convert.ToInt32(id);
            var queryContentCategory = _objdbERPSolutionEntities.siteContentCategories.Where(a => a.ContentTypeID == idTemp).Select(c => new { c.ContentCategoryID, c.ContentCategoryTitle, c.ContentTypeID });
            return Json(new SelectList(queryContentCategory.AsEnumerable(), "ContentCategoryID", "ContentCategoryTitle"));
        }
        public JsonResult GetContentRelatedTo(string id)
        {
            int idTemp = Convert.ToInt32(id);
            var queryContentRelatedTo = _objdbERPSolutionEntities.blogContentRelatedToes.Where(a => a.ContentCategoryID == idTemp).Select(c => new { c.ContentRelatedToID, c.ContentRelatedToTitle, c.ContentCategoryID });
            return Json(new SelectList(queryContentRelatedTo.AsEnumerable(), "ContentRelatedToID", "ContentRelatedToTitle"));
        }

        #region NotUse
        public ActionResult AddContent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(LRNContentDetail objLRNContentDetail)
        {
            return RedirectToAction("Index", "SiteContentEntry", new { dataUse = "A" });

            //return RedirectToAction("Index");
            //return View("Index");
        }
        #endregion NotUse
    }
}