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
    public class VMessageController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private VisitorsMessage _visitorsMessage;
        private VMessageAccessController _objVMessageAccessController;
        // GET: VMessage
        public ActionResult Index()
        {
            try
            {
                _visitorsMessage = new VisitorsMessage();
                _visitorsMessage.CompanyID = LoginUserInformation.CompanyID;
                _visitorsMessage.BranchID = LoginUserInformation.BranchID;
                _visitorsMessage.ApplicationID = LoginUserInformation.ApplicationID;
                _visitorsMessage.MessageChannel = "A";
                _objVMessageAccessController = new VMessageAccessController();
                _visitorsMessage.DtMessages = _objVMessageAccessController.GetVisitorsMessage(_visitorsMessage);

                var queryMessageEmailType = _objdbERPSolutionEntities.MessageEmailTypes.Select(c => new { c.MessageTypeID, c.MessageTypeTitle });
                ViewBag.messageEmailType = new SelectList(queryMessageEmailType.AsEnumerable(), "MessageTypeID", "MessageTypeTitle");

                var queryMessageEmailCategorie = _objdbERPSolutionEntities.MessageEmailCategories.Select(c => new { c.MessageCategoryID, c.MessageCategoryTitle });
                ViewBag.messageEmailCategorie = new SelectList(queryMessageEmailCategorie, "MessageCategoryID", "MessageCategoryTitle");

                var queryMessageEmailSubCategorie = _objdbERPSolutionEntities.MessageEmailSubCategories.Select(c => new { c.MessageSubCategoryID, c.MessageSubCategoryTitle });
                ViewBag.messageEmailSubCategorie = new SelectList(queryMessageEmailSubCategorie.AsEnumerable(), "MessageSubCategoryID", "MessageSubCategoryTitle");

                var queryActionType = _objdbERPSolutionEntities.MessageEmailActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                ViewBag.emailActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle");

                return View(_visitorsMessage);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string actionSearch, string actionClear, VisitorsMessage objVisitorsMessage
            , string messageEmailType, string messageEmailCategorie, string messageEmailSubCategorie, string emailActionType)
        {
            try
            {
                if (actionClear != null)
                {
                    return RedirectToAction("Index", "VMessage");

                }
                else if (actionSearch != null)
                {
                    _objVMessageAccessController = new VMessageAccessController();

                    objVisitorsMessage.ApplicationID = LoginUserInformation.ApplicationID;
                    objVisitorsMessage.CompanyID = LoginUserInformation.CompanyID;
                    objVisitorsMessage.BranchID = LoginUserInformation.BranchID;
                    objVisitorsMessage.MessageTypeID = messageEmailType;
                    objVisitorsMessage.MessageCategoryID = Convert.ToInt32( messageEmailCategorie);
                    objVisitorsMessage.MessageSubCategoryID = Convert.ToInt32( messageEmailSubCategorie);
                    objVisitorsMessage.EmailActionType = emailActionType;
                    objVisitorsMessage.MessageChannel = "A";

                    objVisitorsMessage.FromDate = objVisitorsMessage.FromDate == null ? string.Empty : Convert.ToDateTime(objVisitorsMessage.FromDate.ToString()).ToShortDateString();
                    objVisitorsMessage.ToDate = objVisitorsMessage.ToDate == null ? string.Empty : Convert.ToDateTime(objVisitorsMessage.ToDate.ToString()).ToShortDateString();

                    DataTable dtMessage = _objVMessageAccessController.SearchVisitorsMessage(objVisitorsMessage); ;
                    objVisitorsMessage.DtMessages = dtMessage;

                    var queryMessageEmailType = _objdbERPSolutionEntities.MessageEmailTypes.Select(c => new { c.MessageTypeID, c.MessageTypeTitle });
                    ViewBag.messageEmailType = new SelectList(queryMessageEmailType.AsEnumerable(), "MessageTypeID", "MessageTypeTitle", messageEmailType);

                    var queryMessageEmailCategorie = _objdbERPSolutionEntities.MessageEmailCategories.Select(c => new { c.MessageCategoryID, c.MessageCategoryTitle });
                    ViewBag.messageEmailCategorie = new SelectList(queryMessageEmailCategorie, "MessageCategoryID", "MessageCategoryTitle", messageEmailCategorie);

                    var queryMessageEmailSubCategorie = _objdbERPSolutionEntities.MessageEmailSubCategories.Select(c => new { c.MessageSubCategoryID, c.MessageSubCategoryTitle });
                    ViewBag.messageEmailSubCategorie = new SelectList(queryMessageEmailSubCategorie.AsEnumerable(), "MessageSubCategoryID", "MessageSubCategoryTitle", messageEmailSubCategorie);

                    var queryActionType = _objdbERPSolutionEntities.MessageEmailActionTypes.Select(c => new { c.ActionTypeID, c.ActionTypeTitle });
                    ViewBag.emailActionType = new SelectList(queryActionType.AsEnumerable(), "ActionTypeID", "ActionTypeTitle", emailActionType);



                    return View(objVisitorsMessage);
                }
                else
                {
                    return RedirectToAction("Index", "VMessage");
                }
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}