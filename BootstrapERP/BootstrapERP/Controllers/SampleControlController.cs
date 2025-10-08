using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class SampleControlController : Controller
    {
        // GET: SampleControl
        public ActionResult Index()
        {
            Session["serciceID"] = 1;
            return View(Session["serciceID"]);
        }
    }
}