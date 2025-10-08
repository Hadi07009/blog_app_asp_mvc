using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class StartLearningController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: StartLearning
        public ActionResult Index(int selectedServiceID)
        {
            try
            {
                Session["serciceID"] = selectedServiceID;
                return View();
            }
            catch (Exception msgException)
            {

                return Content(msgException.Message);
            }
        }

        public ActionResult NodeDetails(int id,int parentID)
        {
            try
            {
                ViewBag.Current = id;
                tblStartLearning objtblStartLearning = _objdbERPSolutionEntities.tblStartLearnings.Find(Convert.ToInt32(id), parentID);
                if (objtblStartLearning == null)
                {
                    return HttpNotFound();
                }
                return View(objtblStartLearning);
            }
            catch (Exception msgException)
            {

                return Content(msgException.Message);
            }
        }
        
        [HttpGet]
        public ActionResult _MainMenu(int selectedService)
        {
            try
            {
                return PartialView("", from m in _objdbERPSolutionEntities.tblStartLearnings
                                       where m.PNodeTypeID == selectedService
                                       select m);
                
            }
            catch (Exception msgException)
            {
                return Content(msgException.Message);
            }

        }
    }
}