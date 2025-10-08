using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class DepartmentSetup : BranchSetup
    {
        private int _departmentID;

        public int DepartmentID
        {
            get { return _departmentID; }
            set { _departmentID = value; }
        }
    }
}