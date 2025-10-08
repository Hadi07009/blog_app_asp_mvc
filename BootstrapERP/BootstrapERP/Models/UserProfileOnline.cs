using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class UserProfileOnline :UserProfile
    {
        private int _serviceRequestID;

        public int ServiceRequestID
        {
            get { return _serviceRequestID; }
            set { _serviceRequestID = value; }
        }
        private DateTime _requestDate;

        public DateTime RequestDate
        {
            get { return _requestDate; }
            set { _requestDate = value; }
        }
        private string _rquestTime;

        public string RquestTime
        {
            get { return _rquestTime; }
            set { _rquestTime = value; }
        }
        private int _specialDiscountRate;

        public int SpecialDiscountRate
        {
            get { return _specialDiscountRate; }
            set { _specialDiscountRate = value; }
        }
        private DataTable _dtSelectedService;

        public DataTable DtSelectedService
        {
            get { return _dtSelectedService; }
            set { _dtSelectedService = value; }
        }
        private DataTable _dtUserProfileOnline;

        public DataTable DtUserProfileOnline
        {
            get { return _dtUserProfileOnline; }
            set { _dtUserProfileOnline = value; }
        }

    }
}