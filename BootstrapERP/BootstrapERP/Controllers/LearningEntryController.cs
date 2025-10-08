using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class LearningEntryController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: LearningEntry
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EntryContent()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EntryContent(tblStartLearning objtblStartLearnings)
        {
            try
            {
                return RedirectToAction("EntryContent");
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        //public ActionResult JqueryTreeview()
        //{
        //    var query = InitData().Where(c => c.parent_id == 0).ToList(); 
        //    return View(query);
        //}
        //[HttpPost]
        //public JsonResult FilterNode(int parentid)
        //{
        //    var query = InitData().Where(c => c.parent_id == parentid).ToList(); //according parent id to filter node
        //    return Json(query);
        //}
        //public List<TreeviewModel> InitData()
        //{
        //    List<TreeviewModel> itemlist = new List<TreeviewModel>();
        //    itemlist.Add(new TreeviewModel() { cat_id = 101, parent_id = 0, descript = "Sports" });
        //    itemlist.Add(new TreeviewModel() { cat_id = 102, parent_id = 0, descript = "Fruits" });
        //    itemlist.Add(new TreeviewModel() { cat_id = 103, parent_id = 101, descript = "Football" });
        //    itemlist.Add(new TreeviewModel() { cat_id = 104, parent_id = 101, descript = "Basketball" });
        //    itemlist.Add(new TreeviewModel() { cat_id = 105, parent_id = 102, descript = "Apple" });
        //    itemlist.Add(new TreeviewModel() { cat_id = 106, parent_id = 102, descript = "Orange" });
        //    return itemlist;
        //}
    }
}