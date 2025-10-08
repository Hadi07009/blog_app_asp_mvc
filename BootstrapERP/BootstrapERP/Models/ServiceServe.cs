using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class ServiceServe : ServiceManagement
    {
        private string _serviceList;

        public string ServiceList
        {
            get { return _serviceList; }
            set { _serviceList = value; }
        }
    }
}