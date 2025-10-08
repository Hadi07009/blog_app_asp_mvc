using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class ResendSecurityCodeController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: ResendSecurityCode
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ResendCode()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResendCode(uUserProfileTemp objuUserProfileTemp)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var UserProfileIDTemp = _objdbERPSolutionEntities.Database.SqlQuery<Guid>("SELECT A.UserProfileID FROM uUserProfileTemp A WHERE A.DataUsed = 'A' AND A.UserEmail = '" + objuUserProfileTemp.UserEmail + "'");
                objuUserProfileTemp.UserProfileID = UserProfileIDTemp.AsEnumerable().First();
                objuUserProfileTemp.DataUsed = "A";
                var dateQuery = _objdbERPSolutionEntities.Database.SqlQuery<DateTime>("SELECT getdate()");
                DateTime serverDate = dateQuery.AsEnumerable().First();
                objuUserProfileTemp.EntryDate = serverDate;
                var securityCodeSQL = _objdbERPSolutionEntities.Database.SqlQuery<string>("SELECT RIGHT(CAST(RAND(CHECKSUM(NEWID())) AS DECIMAL(15, 15)), 5) AS SecurityCode");
                var securityCode = Convert.ToInt32(securityCodeSQL.AsEnumerable().First().ToString());
                _objdbERPSolutionEntities.spInitiateSecurityCode(objuUserProfileTemp.UserProfileID, objuUserProfileTemp.UserProfileID, securityCode);
                UserProfileHomeController onjUserProfileHomeController = new UserProfileHomeController();
                onjUserProfileHomeController.SendSecurityCodeByMail(1, securityCode, objuUserProfileTemp.UserEmail, objuUserProfileTemp.UserEmail);
                return RedirectToAction("ResendCode");

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}