using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class TreeviewModel
    {
        public int cat_id { get; set; }
        public int parent_id { get; set; }
        public string descript { get; set; }
    }
}