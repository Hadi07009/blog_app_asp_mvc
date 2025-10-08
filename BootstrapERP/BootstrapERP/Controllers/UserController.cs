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
    public class UserController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private UserProfileController _objUserProfileController;
        // GET: User
        public ActionResult Index()
        {
            try
            {
                var query = _objdbERPSolutionEntities.EmployeeTitles.Select(c => new { c.FieldOfID, c.FieldOfName });
                ViewBag.UserTitle = new SelectList(query.AsEnumerable(), "FieldOfID", "FieldOfName");
                var queryUserType = _objdbERPSolutionEntities.uUserTypeSetups.Select(c => new { c.UserTypeID, c.UserType });
                ViewBag.userType = new SelectList(queryUserType.AsEnumerable(), "UserTypeID", "UserType");
                return View();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string profileSave, string UserTitle, string userType, UserProfile objUserProfile)
        {
            try
            {
                if (profileSave != null)
                {
                    objUserProfile.EntryUserName = LoginUserInformation.UserID;
                    objUserProfile.Title = UserTitle;
                    objUserProfile.UserProfileTypeID = userType;
                    _objUserProfileController = new UserProfileController();
                    _objUserProfileController.Save(objUserProfile);
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}