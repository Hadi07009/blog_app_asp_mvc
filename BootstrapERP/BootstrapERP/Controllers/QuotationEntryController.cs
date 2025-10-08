using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class QuotationEntryController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private QuotationAccess _objQuotationAccess;
        // GET: QuotationEntry
        public ActionResult Index()
        {
            try
            {
                var queryQuoteAuthorID = _objdbERPSolutionEntities.blogQuoteAuthors.Select(c => new { c.QuoteAuthorID, c.AuthorFullName });
                ViewBag.QuoteAuthorID = new SelectList(queryQuoteAuthorID.AsEnumerable(), "QuoteAuthorID", "AuthorFullName");
                var queryAuthorTypeID = _objdbERPSolutionEntities.blogAuthorTypeSetups.Select(c => new { c.AuthorTypeID, c.AuthorTypeTitle });
                ViewBag.AuthorTypeID = new SelectList(queryAuthorTypeID.AsEnumerable(), "AuthorTypeID", "AuthorTypeTitle");
                return View();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string saveQuotation, int AuthorTypeID, int QuoteAuthorID
            , QuoteAuthor objQuoteAuthor)
        {
            try
            {
                if (saveQuotation != null)
                {
                    objQuoteAuthor.EntryUserName = LoginUserInformation.UserID;
                    objQuoteAuthor.CompanyID = LoginUserInformation.CompanyID;
                    objQuoteAuthor.BranchID = LoginUserInformation.BranchID;
                    objQuoteAuthor.ApplicationID = LoginUserInformation.ApplicationID;
                    objQuoteAuthor.QuoteDescription = objQuoteAuthor.QuoteDescription.Replace("'", "''");
                    objQuoteAuthor.QuoteRemarks = objQuoteAuthor.QuoteRemarks.Replace("'", "''");
                    _objQuotationAccess = new QuotationAccess();
                    _objQuotationAccess.SaveQuotation(objQuoteAuthor);
                }

                return RedirectToAction("Index");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult Edit(int quotationIndex, int quoteAuthorIndex, int quoteTypeIndex
            , string quotations, string remarks)
        {
            try
            {
                var queryQuoteAuthorID = _objdbERPSolutionEntities.blogQuoteAuthors.Select(c => new { c.QuoteAuthorID, c.AuthorFullName });
                ViewBag.QuoteAuthorID = new SelectList(queryQuoteAuthorID.AsEnumerable(), "QuoteAuthorID", "AuthorFullName", quoteAuthorIndex);
                var queryAuthorTypeID = _objdbERPSolutionEntities.blogAuthorTypeSetups.Select(c => new { c.AuthorTypeID, c.AuthorTypeTitle });
                ViewBag.AuthorTypeID = new SelectList(queryAuthorTypeID.AsEnumerable(), "AuthorTypeID", "AuthorTypeTitle", quoteTypeIndex);
                QuoteAuthor objQuoteAuthor = new QuoteAuthor();
                objQuoteAuthor.QuoteID = quotationIndex;
                objQuoteAuthor.QuoteDescription = quotations;
                objQuoteAuthor.QuoteRemarks = remarks;
                return View(objQuoteAuthor);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string updateQuotation, int AuthorTypeID, int QuoteAuthorID
            , QuoteAuthor objQuoteAuthor)
        {
            try
            {
                if (updateQuotation != null)
                {
                    objQuoteAuthor.EntryUserName = LoginUserInformation.UserID;
                    objQuoteAuthor.CompanyID = LoginUserInformation.CompanyID;
                    objQuoteAuthor.BranchID = LoginUserInformation.BranchID;
                    objQuoteAuthor.ApplicationID = LoginUserInformation.ApplicationID;
                    objQuoteAuthor.QuoteDescription = objQuoteAuthor.QuoteDescription.Replace("'", "''");
                    objQuoteAuthor.QuoteRemarks = objQuoteAuthor.QuoteRemarks.Replace("'", "''");
                    _objQuotationAccess = new QuotationAccess();
                    _objQuotationAccess.UpdateQuotation(objQuoteAuthor);
                }

                return RedirectToAction("Index", "QuotationRecord");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}