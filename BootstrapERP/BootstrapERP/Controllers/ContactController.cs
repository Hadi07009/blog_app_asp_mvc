using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class ContactController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private VisitorsMessage _objVisitorsMessage;
        private VMessageAccessController _objVMessageAccessController;
        public ActionResult Index()
        {
            _objVisitorsMessage = new VisitorsMessage();
            if (LoginUserInformation.LogginUserStatus == "Yes")
            {
                _objVisitorsMessage.Name = LoginUserInformation.EmployeeFullName;
                _objVisitorsMessage.EmailAddress = LoginUserInformation.UserEmailID;
                _objVisitorsMessage.PhoneNumber = LoginUserInformation.UserMobileNo;
            }

            var queryMessageEmailType = _objdbERPSolutionEntities.MessageEmailTypes.Select(c => new { c.MessageTypeID, c.MessageTypeTitle });
            ViewBag.messageEmailType = new SelectList(queryMessageEmailType.AsEnumerable(), "MessageTypeID", "MessageTypeTitle");

            var queryMessageEmailCategorie = _objdbERPSolutionEntities.MessageEmailCategories.Select(c => new { c.MessageCategoryID, c.MessageCategoryTitle });
            ViewBag.messageEmailCategorie = new SelectList(queryMessageEmailCategorie, "MessageCategoryID", "MessageCategoryTitle");

            var queryMessageEmailSubCategorie = _objdbERPSolutionEntities.MessageEmailSubCategories.Select(c => new { c.MessageSubCategoryID, c.MessageSubCategoryTitle });
            ViewBag.messageEmailSubCategorie = new SelectList(queryMessageEmailSubCategorie.AsEnumerable(), "MessageSubCategoryID", "MessageSubCategoryTitle");
            
            return View(_objVisitorsMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(VisitorsMessage objVisitorsMessage, string contact_message, string messageEmailType
            , string messageEmailCategorie, string messageEmailSubCategorie)
        {
            objVisitorsMessage.MessageText = contact_message;
            objVisitorsMessage.CompanyID = 1;
            objVisitorsMessage.BranchID = 1;
            objVisitorsMessage.ApplicationID = 2;
            objVisitorsMessage.MessageChannel = "A";
            objVisitorsMessage.MessageTypeID = messageEmailType;
            objVisitorsMessage.MessageCategoryID = Convert.ToInt32( messageEmailCategorie);
            objVisitorsMessage.MessageSubCategoryID = Convert.ToInt32(messageEmailSubCategorie);
            _objVMessageAccessController = new VMessageAccessController();
            _objVMessageAccessController.Save(objVisitorsMessage);
            return RedirectToAction("Index", "Contact");
        }

        public JsonResult GetMessageEmailSubCategorie(string id)
        {
            try
            {
                int idTemp = Convert.ToInt32(id);
                var queryMessageEmailSubCategorie = _objdbERPSolutionEntities.MessageEmailSubCategories.Where(a => a.MessageCategoryID == idTemp).Select(c => new { c.MessageSubCategoryID, c.MessageSubCategoryTitle, c.MessageCategoryID });

                ViewBag.messageEmailSubCategorie = new SelectList(queryMessageEmailSubCategorie, "MessageSubCategoryID", "MessageSubCategoryTitle");
                

                return Json(ViewBag.messageEmailSubCategorie);
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