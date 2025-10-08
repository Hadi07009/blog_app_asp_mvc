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
    public class ExamCenterController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        // GET: ExamCenter
        public ActionResult Index()
        {
            var query = _objdbERPSolutionEntities.lrnCourseTypes.Select(c => new { c.CourseTypeID, c.CourseTypeTitle });
            ViewBag.CourseTypeTitle = new SelectList(query.AsEnumerable(), "CourseTypeID", "CourseTypeTitle");
            var queryCourseCategories = _objdbERPSolutionEntities.lrnCourseCategories.Select(c => new { c.CourseCategoryID, c.CourseCategoryTitle });
            ViewBag.CourseCategoryTitle = new SelectList(queryCourseCategories.AsEnumerable(), "CourseCategoryID", "CourseCategoryTitle");
            var queryLearningPaths = _objdbERPSolutionEntities.lrnLearningPaths.Select(c => new { c.LearningPathID, c.LearningPathTitle });
            ViewBag.LearningPathTitle = new SelectList(queryLearningPaths.AsEnumerable(), "LearningPathID", "LearningPathTitle");
            var queryProviderNames = _objdbERPSolutionEntities.lrnProviderNames.Select(c => new { c.ProviderID, c.ProviderName });
            ViewBag.ProviderName = new SelectList(queryProviderNames.AsEnumerable(), "ProviderID", "ProviderName");
            var queryCourseLevel = _objdbERPSolutionEntities.lrnCourseLevels.Select(c => new { c.CourseLevelID, c.CourseLevelTitle });
            ViewBag.CourseLevel = new SelectList(queryCourseLevel.AsEnumerable(), "CourseLevelID", "CourseLevelTitle");

            LearningHubBase objLearningHubBase = new LearningHubBase();
            IEnumerable<ContentHeader> objContentHeader = objLearningHubBase.GetContentHeaderRecord().AsEnumerable().Select(x => new ContentHeader()
            {
                CourseID = x.Field<int>("CourseID"),
                CourseName = x.Field<string>("CourseName")
            }).ToList();
            return View(objContentHeader);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string searchButton, string advancedSearchButton, string contact_search, string CourseTypeTitle, string CourseCategoryIDS, string LearningPathS
            , string CourseProviderS, string CourseLevelS)
        {
            var query = _objdbERPSolutionEntities.lrnCourseTypes.Select(c => new { c.CourseTypeID, c.CourseTypeTitle });
            ViewBag.CourseTypeTitle = new SelectList(query.AsEnumerable(), "CourseTypeID", "CourseTypeTitle");
            var queryCourseCategories = _objdbERPSolutionEntities.lrnCourseCategories.Select(c => new { c.CourseCategoryID, c.CourseCategoryTitle });
            ViewBag.CourseCategoryTitle = new SelectList(queryCourseCategories.AsEnumerable(), "CourseCategoryID", "CourseCategoryTitle");
            var queryLearningPaths = _objdbERPSolutionEntities.lrnLearningPaths.Select(c => new { c.LearningPathID, c.LearningPathTitle });
            ViewBag.LearningPathTitle = new SelectList(queryLearningPaths.AsEnumerable(), "LearningPathID", "LearningPathTitle");
            var queryProviderNames = _objdbERPSolutionEntities.lrnProviderNames.Select(c => new { c.ProviderID, c.ProviderName });
            ViewBag.ProviderName = new SelectList(queryProviderNames.AsEnumerable(), "ProviderID", "ProviderName");
            var queryCourseLevel = _objdbERPSolutionEntities.lrnCourseLevels.Select(c => new { c.CourseLevelID, c.CourseLevelTitle });
            ViewBag.CourseLevel = new SelectList(queryCourseLevel.AsEnumerable(), "CourseLevelID", "CourseLevelTitle");

            if (advancedSearchButton != null)
            {
                ContentHeader objContentHeaderVaue = new ContentHeader();
                objContentHeaderVaue.CourseTypeID = Convert.ToInt32(CourseTypeTitle);
                objContentHeaderVaue.CourseCategoryID = Convert.ToInt32(CourseCategoryIDS);
                objContentHeaderVaue.LearningPath = Convert.ToInt32(LearningPathS);
                objContentHeaderVaue.CourseProvider = Convert.ToInt32(CourseProviderS);
                objContentHeaderVaue.CourseLevelID = Convert.ToInt32(CourseLevelS);
                LearningHubBase objLearningHubBase = new LearningHubBase();
                IEnumerable<ContentHeader> objContentHeader = objLearningHubBase.GetContentHeaderRecord(objContentHeaderVaue).AsEnumerable().Select(x => new ContentHeader()
                {
                    CourseID = x.Field<int>("CourseID"),
                    CourseName = x.Field<string>("CourseName")
                }).ToList();
                return View(objContentHeader);
            }
            else if (searchButton != null)
            {
                ContentHeader objContentHeaderVaue = new ContentHeader();
                objContentHeaderVaue.CourseName = contact_search;
                LearningHubBase objLearningHubBase = new LearningHubBase();
                IEnumerable<ContentHeader> objContentHeader = objLearningHubBase.GetContentHeaderRecordByName(objContentHeaderVaue).AsEnumerable().Select(x => new ContentHeader()
                {
                    CourseID = x.Field<int>("CourseID"),
                    CourseName = x.Field<string>("CourseName")
                }).ToList();
                return View(objContentHeader);
            }
            else
            {
                return View();
            }

        }
    }
}