using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Models
{
    public class BlogContentDetail : BlogContentHeader
    {
        private string _contentID;
        private int _sequenceNo;
        private string _contentDetailTitle;
        private string _contentDetailSubTitle;
        private string _contentDetailImageURL;
        private string _contentIntroductoryText;
        private string _contentDetailDescription;
        private string _authorsName;

        public string ContentID { get => _contentID; set => _contentID = value; }
        public int SequenceNo { get => _sequenceNo; set => _sequenceNo = value; }
        public string ContentDetailTitle { get => _contentDetailTitle; set => _contentDetailTitle = value; }
        public string ContentDetailSubTitle { get => _contentDetailSubTitle; set => _contentDetailSubTitle = value; }
        public string ContentDetailImageURL { get => _contentDetailImageURL; set => _contentDetailImageURL = value; }
        public string ContentIntroductoryText { get => _contentIntroductoryText; set => _contentIntroductoryText = value; }
        [AllowHtml]
        public string ContentDetailDescription { get => _contentDetailDescription; set => _contentDetailDescription = value; }
        public string AuthorsName { get => _authorsName; set => _authorsName = value; }
        public string ActionType { get => _actionType; set => _actionType = value; }

        private string _actionType;
    }
}