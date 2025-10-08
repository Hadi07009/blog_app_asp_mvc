using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class HomePageRegisterController : Controller
    {
        // GET: HomePageRegister
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterOnline()
        {
            ViewBag.MessagePassword = "";
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterOnline(UserList objUserList)
        {
            try
            {
                OnlineRegisterController objOnlineRegisterController = new OnlineRegisterController();
                string checkUserName = objOnlineRegisterController.CheckUserName(objUserList);
                if (checkUserName == objUserList.UserName)
                {
                    ViewBag.MessagePassword = objUserList.UserName + " already exist";
                    return View();
                }
                else
                {
                    ViewBag.MessagePassword = "";
                    objUserList.SecurityCode = objUserList.SecurityCode.Trim();
                    int tagOnline = 1;
                    objUserList.UserPassword = objOnlineRegisterController.EncodePassword(objUserList.UserPassword);
                    objOnlineRegisterController.Update(objUserList, tagOnline);
                    return RedirectToAction("Index", "RegistrationConfirmed", new { userEmail = objUserList.UserEmail });
                    
                }
            }
            catch (Exception msgException)
            {

                ViewBag.MessagePassword = msgException.Message;
                return View();
            }
        }

        public ActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(UserList objUserList)
        {
            try
            {
                OnlineRegisterController objOnlineRegisterController = new OnlineRegisterController();
                objUserList.SecurityCode = objUserList.SecurityCode.Trim();
                int tagOnline = 0;
                objOnlineRegisterController.Update(objUserList, tagOnline);
                return RedirectToAction("UserRegister");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

    }
}