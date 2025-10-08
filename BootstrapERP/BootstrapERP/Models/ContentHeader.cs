using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Models
{
    public class ContentHeader:BranchSetup
    {
        public int CourseTypeID { get; set; }
        public int CourseCategoryID { get; set; }
        public int LearningPath { get; set; }
        public int CourseProvider { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int CourseTypeTitle { get; set; }
        public int SL { get; set; }
        public int CourseLevelID { get; set; }
    }
}