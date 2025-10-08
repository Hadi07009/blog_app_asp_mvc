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
    public class ProfileController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private UserProfileController _objUserProfileController;
        // GET: Profile
        public ActionResult Index()
        {
            try
            {
                UserProfile objUserProfile = new UserProfile();
                DataTable dtProfile = new DataTable();
                _objUserProfileController = new UserProfileController();

                dtProfile = _objUserProfileController.GetProfileRecord(LoginUserInformation.UserID);
                foreach (DataRow rowNo in dtProfile.Rows)
                {
                    var query = _objdbERPSolutionEntities.EmployeeTitles.Select(c => new { c.FieldOfID, c.FieldOfName });
                    ViewBag.UserTitleID = new SelectList(query.AsEnumerable(), "FieldOfID", "FieldOfName", rowNo["Title"].ToString());
                    objUserProfile.FullName = rowNo["FullName"].ToString();
                    objUserProfile.DisplayName = rowNo["DisplayName"].ToString();
                    objUserProfile.MobilePIN = rowNo["MobilePIN"].ToString();
                    objUserProfile.BirthDate = Convert.ToDateTime(rowNo["DateOfBirth"].ToString()).ToShortDateString();
                    objUserProfile.DateOfBirth = Convert.ToDateTime(rowNo["DateOfBirth"].ToString());
                }

                return View(objUserProfile);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string profileUpdate, string UserTitleID, UserProfile objUserProfile)
        {
            try
            {
                objUserProfile.EntryUserName = LoginUserInformation.UserID;
                objUserProfile.Title = UserTitleID;
                _objUserProfileController = new UserProfileController();
                _objUserProfileController.Update(objUserProfile);
                return RedirectToAction("Index");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}