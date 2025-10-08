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
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        private UserProfile _objUserProfile;
        private UserListController _objUserListController;
        private UserProfileController _objUserProfileController;
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        public ActionResult Index()
        {
            try
            {
                _objUserProfile = new UserProfile();
                _objUserListController = new UserListController();
                _objUserProfile.DtUser = _objUserListController.GetAllActiveUser();
                return View(_objUserProfile);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult UserSuspend(string userProfile)
        {
            try
            {
                _objUserProfile = new UserProfile();
                _objUserListController = new UserListController();
                _objUserProfile.EntryUserName = LoginUserInformation.UserID;
                _objUserProfile.UserProfileID = userProfile;
                _objUserListController.SuspendUser(_objUserProfile);
                return RedirectToAction("Index", "UserAccount");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult UserProfile(string userProfile)
        {
            try
            {
                UserProfile objUserProfile = new UserProfile();
                DataTable dtProfile = new DataTable();
                _objUserProfileController = new UserProfileController();

                dtProfile = _objUserProfileController.GetProfileRecord(userProfile);
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
    }
}