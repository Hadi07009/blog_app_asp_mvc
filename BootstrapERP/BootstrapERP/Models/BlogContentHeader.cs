using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class BlogContentHeader : BranchSetup
    {
        private int _applicationID;
        private string _contentParentID;
        private int _contentCategoryID;
        private int _contentRelatedToID;

        public int ApplicationID { get => _applicationID; set => _applicationID = value; }
        public string ContentParentID { get => _contentParentID; set => _contentParentID = value; }
        public int ContentCategoryID { get => _contentCategoryID; set => _contentCategoryID = value; }
        public int ContentRelatedToID { get => _contentRelatedToID; set => _contentRelatedToID = value; }
    }
}