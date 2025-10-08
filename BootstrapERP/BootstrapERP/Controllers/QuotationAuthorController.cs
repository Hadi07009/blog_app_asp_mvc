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
    public class QuotationAuthorController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private QuotationAccess _objQuotationAccess;
        // GET: QuotationAuthor
        public ActionResult Index()
        {
            try
            {
                var queryAuthorTypeID = _objdbERPSolutionEntities.blogAuthorTypeSetups.Select(c => new { c.AuthorTypeID, c.AuthorTypeTitle });
                ViewBag.AuthorTypeID = new SelectList(queryAuthorTypeID.AsEnumerable(), "AuthorTypeID", "AuthorTypeTitle");
                var queryGenderID = _objdbERPSolutionEntities.GenderSetups.Select(c => new { c.FieldOfID, c.FieldOfName });
                ViewBag.GenderID = new SelectList(queryGenderID.AsEnumerable(), "FieldOfID", "FieldOfName");
                var queryReligionID = _objdbERPSolutionEntities.ReligionSetups.Select(c => new { c.FieldOfID, c.FieldOfName });
                ViewBag.ReligionID = new SelectList(queryReligionID.AsEnumerable(), "FieldOfID", "FieldOfName");
                var queryFamousForID = _objdbERPSolutionEntities.blogFamousForSetups.Select(c => new { c.FamousForID, c.FamousForTitle });
                ViewBag.FamousForID = new SelectList(queryFamousForID.AsEnumerable(), "FamousForID", "FamousForTitle");

                return View();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string saveAuthor, int AuthorTypeID, int GenderID, int ReligionID
            ,int FamousForID, QuoteAuthor objQuoteAuthor)
        {
            try
            {
                if (saveAuthor != null)
                {
                    objQuoteAuthor.EntryUserName = LoginUserInformation.UserID;
                    objQuoteAuthor.CompanyID = LoginUserInformation.CompanyID;
                    objQuoteAuthor.BranchID = LoginUserInformation.BranchID;
                    objQuoteAuthor.ApplicationID = LoginUserInformation.ApplicationID;
                    _objQuotationAccess = new QuotationAccess();
                    _objQuotationAccess.SaveAuthor(objQuoteAuthor);
                }

                return RedirectToAction("Index");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult Quotations(int authorIndex,string authorName)
        {
            try
            {
                QuoteAuthor objQuoteAuthor = new QuoteAuthor();
                _objQuotationAccess = new QuotationAccess();
                objQuoteAuthor.QuoteAuthorID = authorIndex;
                objQuoteAuthor.DtQuotation = _objQuotationAccess.GetAllAuthorsQuotation(objQuoteAuthor);
                ViewBag.authorName = authorName;
                return View(objQuoteAuthor);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

    }
}