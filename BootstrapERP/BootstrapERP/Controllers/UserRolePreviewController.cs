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
    public class UserRolePreviewController : Controller
    {
        private UserProfile _objUserProfile;
        private UserPermission _objUserPermission;
        private PermissionController _objPermissionController;
        // GET: UserRolePreview
        public ActionResult Index(string userEmail)
        {
            _objUserProfile = new UserProfile();
            _objUserProfile.Email = userEmail;
            return View(_objUserProfile);
        }

        [HttpPost]
        public JsonResult UserRole(string values)
        {
            try
            {
                string userIdentifierID = null;
                _objUserPermission = new UserPermission();
                var id = values.Split(',');
                int indexid = Array.IndexOf(id, id.Where(x => x.Contains("flagType")).FirstOrDefault());
                List<int> listRole = new List<int>();
                List<string> listRoleType = new List<string>();

                int index = 0;

                foreach (var item in id)
                {
                    if (index == 0)
                    {
                        userIdentifierID = item;
                    }
                    else if (index < indexid && index != 0)
                    {
                        listRoleType.Add(item.ToString());
                    }
                    else if (index > indexid)
                    {
                        int roleID = int.Parse(item);
                        listRole.Add(roleID);
                    }

                    index++;
                }

                _objUserPermission.roleList = listRole;
                _objUserPermission.roleTypeList = listRoleType;
                EmployeeSetup objEmployeeSetup = new EmployeeSetup();
                UserProfileController objUserProfileController = new UserProfileController();
                objEmployeeSetup.EmployeeID = objUserProfileController.GetUserProfileID(userIdentifierID);
                objEmployeeSetup.CompanyID = LoginUserInformation.CompanyID;
                objEmployeeSetup.EntryUserName = LoginUserInformation.UserID;
                _objPermissionController = new PermissionController();
                _objPermissionController.SaveUserRole(objEmployeeSetup, _objUserPermission);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
    }
}