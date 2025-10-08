using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class SiteBanner : BranchSetup
    {
        private int _bannerID;
        private string _bannerTitle;
        private string _bannerImageURL;
        private string _bannerRemarks;
        private int _applicationID;
        private DataTable _dtBanner;
        private string _dataUsedValue;

        public int BannerID { get => _bannerID; set => _bannerID = value; }
        public string BannerTitle { get => _bannerTitle; set => _bannerTitle = value; }
        public string BannerImageURL { get => _bannerImageURL; set => _bannerImageURL = value; }
        public string BannerRemarks { get => _bannerRemarks; set => _bannerRemarks = value; }
        public int ApplicationID { get => _applicationID; set => _applicationID = value; }
        public DataTable DtBanner { get => _dtBanner; set => _dtBanner = value; }
        public string DataUsedValue { get => _dataUsedValue; set => _dataUsedValue = value; }
    }
}