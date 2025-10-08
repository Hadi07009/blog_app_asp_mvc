using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class HierarchyViewModel
    {
        public int Id { get; set; }
        public string text { get; set; }
        public int? perentId { get; set; }
        public virtual List<HierarchyViewModel> children { get; set; }
        public string nodeCheckValue { get; set; }
    }
}