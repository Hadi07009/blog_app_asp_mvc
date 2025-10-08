using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Models
{
    public class LRNContentDetail
    {
        [AllowHtml]
        [Display(Name = "Message")]
        public string ContentDescription { get; set; }
    }
}