using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class EmployeeTitleController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: EmployeeTitle
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEmployeeTitle(int companyID)
        {
            try
            {
                return PartialView("", from m in _objdbERPSolutionEntities.EmployeeTitles
                                       where m.CompanyID == companyID && m.DataUsed == "A"
                                       select m);

            }
            catch (Exception msgException)
            {
                return Content(msgException.Message);
            }
            
        }
    }
}