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
    public class QuotationRecordController : Controller
    {
        private QuotationAccess _objQuotationAccess;
        private QuoteAuthor _objQuoteAuthor;
        // GET: QuotationRecord
        public ActionResult Index()
        {
            try
            {
                _objQuoteAuthor = new QuoteAuthor();
                _objQuoteAuthor.CompanyID = LoginUserInformation.CompanyID;
                _objQuoteAuthor.BranchID = LoginUserInformation.BranchID;
                _objQuoteAuthor.ApplicationID = LoginUserInformation.ApplicationID;
                _objQuotationAccess = new QuotationAccess();
                _objQuoteAuthor.DtQuotation = _objQuotationAccess.GetAllQuotation(_objQuoteAuthor);
                return View(_objQuoteAuthor);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
            
        }
    }
}