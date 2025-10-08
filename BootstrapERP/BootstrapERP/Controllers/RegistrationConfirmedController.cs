using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class RegistrationConfirmedController : Controller
    {
        private UserProfile _objUserProfile;
        // GET: RegistrationConfirmed
        public ActionResult Index(string userEmail)
        {
            try
            {
                _objUserProfile = new UserProfile();
                UserListController _objUserListController = new UserListController();
                UserList objUserList = new UserList();
                objUserList.UserEmail = userEmail;
                DataTable dtUserInfo = _objUserListController.ShowRegistration(objUserList);
                foreach (DataRow rowNo in dtUserInfo.Rows)
                {
                    _objUserProfile.FullName = rowNo["FullName"].ToString();
                    _objUserProfile.Email = rowNo["Email"].ToString();
                    _objUserProfile.MobilePIN = rowNo["MobilePIN"].ToString() == null ? "" : rowNo["MobilePIN"].ToString();
                    ViewBag.userName = rowNo["UserName"].ToString();
                }

                return View(_objUserProfile);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}