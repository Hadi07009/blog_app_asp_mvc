using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Models
{
    public class QuoteAuthor: BranchSetup
    {
        private int _quoteAuthorID;
        private int _authorTypeID;
        private string _authorFullName;
        private int _genderID;
        private int _religionID;
        private int _famousForID;
        private string _nationality;
        private DateTime _dateOfBirth;
        private DateTime _dateDied;
        private int _applicationID;

        public int QuoteAuthorID { get => _quoteAuthorID; set => _quoteAuthorID = value; }
        public int AuthorTypeID { get => _authorTypeID; set => _authorTypeID = value; }
        public string AuthorFullName { get => _authorFullName; set => _authorFullName = value; }
        public int GenderID { get => _genderID; set => _genderID = value; }
        public int ReligionID { get => _religionID; set => _religionID = value; }
        public int FamousForID { get => _famousForID; set => _famousForID = value; }
        public string Nationality { get => _nationality; set => _nationality = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public DateTime DateDied { get => _dateDied; set => _dateDied = value; }
        public int QuoteID { get => _quoteID; set => _quoteID = value; }
        public int QuoteTypeID { get => _quoteTypeID; set => _quoteTypeID = value; }
        [AllowHtml]
        public string QuoteDescription { get => _quoteDescription; set => _quoteDescription = value; }
        public int ApplicationID { get => _applicationID; set => _applicationID = value; }
        public DataTable DtQuotation { get => _dtQuotation; set => _dtQuotation = value; }
        [AllowHtml]
        public string QuoteRemarks { get => _quoteRemarks; set => _quoteRemarks = value; }

        private int _quoteID;
        private int _quoteTypeID;
        private string _quoteDescription;
        private string _quoteRemarks;

        private DataTable _dtQuotation;

    }
}