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
    public class VMessageReplyController : Controller
    {
        // GET: VMessageReply
        private VisitorsMessage _visitorsMessage;
        private VMessageAccessController _objVMessageAccessController;
        public ActionResult Index(string name, string emailAddress, string entryDate
            ,string vmessage, string visitorsID, string messageID, string messageSubjectTest, string ccEmailAddress)
        {
            try
            {
                _visitorsMessage = new VisitorsMessage();
                _visitorsMessage.VisitorsID = visitorsID;
                _visitorsMessage.MessageID = messageID;
                _visitorsMessage.Name = name;
                _visitorsMessage.EmailAddress = emailAddress;
                _visitorsMessage.EntryDate = entryDate;
                _visitorsMessage.MessageText = vmessage;
                _visitorsMessage.MessageSubject = messageSubjectTest;
                _visitorsMessage.CcEmailAddress = ccEmailAddress;
                return View(_visitorsMessage);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(VisitorsMessage objVisitorsMessage, string contact_message)
        {
            try
            {
                _objVMessageAccessController = new VMessageAccessController();
                objVisitorsMessage.CompanyID = LoginUserInformation.CompanyID;
                objVisitorsMessage.BranchID = LoginUserInformation.BranchID;
                objVisitorsMessage.ApplicationID = LoginUserInformation.ApplicationID;
                objVisitorsMessage.EntryUserName = LoginUserInformation.UserID;
                objVisitorsMessage.ReplyMessageText = contact_message;
                _objVMessageAccessController.ReplyMessage(objVisitorsMessage);
                return RedirectToAction("Index", "VMessage");
                //return View(_visitorsMessage);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}