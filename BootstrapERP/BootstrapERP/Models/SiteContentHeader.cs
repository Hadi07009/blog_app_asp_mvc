using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class SiteContentHeader:BranchSetup
    {
        public int ContentID { get; set; }
        public byte[] ContentImage { get; set; }
        public string ContentImageURL { get; set; }
        public string ContentRemarks { get; set; }
        public DateTime? PreparationDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        //public string DataUsed { get; set; }


    }
}