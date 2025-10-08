using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Models
{
    public class SiteContentDetails:BranchSetup
    {
        public string ContentTitle { get; set; }
        public string ContentDetailSubTitle { get; set; }
        public string ContentImage { get; set; }
        public string ContentIntroductoryText { get; set; }
        [AllowHtml]
        public string ContentDescription { get; set; }
        public int ContentID { get; set; }
        public string ContentTypeDescription { get; set; }
        public string ContentRelatedToTitle { get; set; }
        public string ContentDetailImageURL { get; set; }
        public DateTime ContentEntryDate { get; set; }
        public DataTable DtContentCategory { get; set; }
        public DataTable DtBlogPostsTopStories { get => dtBlogPostsTopStories; set => dtBlogPostsTopStories = value; }
        public DataTable DtBlogPostsWhatsHot { get => _dtBlogPostsWhatsHot; set => _dtBlogPostsWhatsHot = value; }
        public DataTable DtblogQuote { get => _dtblogQuote; set => _dtblogQuote = value; }
        public string BlogID { get => _blogID; set => _blogID = value; }
        public DataTable DtContentUser { get => _dtContentUser; set => _dtContentUser = value; }
        public int ApplicationID { get => _applicationID; set => _applicationID = value; }
        public string ActionTypeID { get => _actionTypeID; set => _actionTypeID = value; }
        public string ContentLogID { get => _contentLogID; set => _contentLogID = value; }
        public int SecquenceNo { get => _secquenceNo; set => _secquenceNo = value; }
        public string RemarksAction { get => _remarksAction; set => _remarksAction = value; }
        public string QualityTag { get => _qualityTag; set => _qualityTag = value; }
        public string PremiumDate { get => _premiumDate; set => _premiumDate = value; }
        public string AppearsDate { get => _appearsDate; set => _appearsDate = value; }
        public string AuthorsName { get => _authorsName; set => _authorsName = value; }
        
        
        public string FromDate { get => _fromDate; set => _fromDate = value; }
        public string ToDate { get => _toDate; set => _toDate = value; }
        public string ContentCategoryTitle { get => _contentCategoryTitle; set => _contentCategoryTitle = value; }
        public DataTable DtBlogUniqueCategory { get => _dtBlogUniqueCategory; set => _dtBlogUniqueCategory = value; }
        public DataTable DtBlogPremium { get => _dtBlogPremium; set => _dtBlogPremium = value; }
        public string ContentCategoryTitle2 { get => _contentCategoryTitle2; set => _contentCategoryTitle2 = value; }
        public DataTable DtBlogUniqueCategory2 { get => _dtBlogUniqueCategory2; set => _dtBlogUniqueCategory2 = value; }
        public string BannerURL { get => _bannerURL; set => _bannerURL = value; }
        public int ContentCategoryID { get => _contentCategoryID; set => _contentCategoryID = value; }
        public string AuthorsEmailID { get => _authorsEmailID; set => _authorsEmailID = value; }

        private DataTable dtBlogPostsTopStories;
        private DataTable _dtBlogPostsWhatsHot;
        private DataTable _dtBlogUniqueCategory;
        private DataTable _dtBlogUniqueCategory2;
        private DataTable _dtBlogPremium;
        private DataTable _dtblogQuote;
        private string _blogID;
        private DataTable _dtContentUser;
        private int _applicationID;
        private string _actionTypeID;
        private string _contentLogID;
        private int _secquenceNo;
        private string _remarksAction;
        private string _qualityTag;
        private string _premiumDate;
        private string _appearsDate;
        private string _authorsName;
        private string _fromDate;
        private string _toDate;
        private string _contentCategoryTitle;
        private string _contentCategoryTitle2;
        private string _bannerURL;
        private int _contentCategoryID;
        private string _authorsEmailID;

    }
}