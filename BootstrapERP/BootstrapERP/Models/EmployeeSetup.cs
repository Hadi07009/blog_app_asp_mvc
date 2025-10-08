using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class EmployeeSetup : TeamSetup
    {
        private string _employeeID;

        public string EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }
        private DataTable dtEmployee;

        public DataTable DtEmployee
        {
            get { return dtEmployee; }
            set { dtEmployee = value; }
        }
    }
}