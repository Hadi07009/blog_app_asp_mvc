using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class CourseEntryController : Controller
    {
        // GET: CourseEntry
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateCourse()
        {
            return View();
        }
        }
}