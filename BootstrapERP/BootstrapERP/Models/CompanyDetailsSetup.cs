using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class CompanyDetailsSetup : CompanySetup 
    {
        private string _companyName;

        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value == null)
                {
                    throw new Exception(" Company name is required ");

                } _companyName = value;
            }
        }
        private string _companyEmail;

        public string CompanyEmail
        {
            get { return _companyEmail; }
            set { _companyEmail = value; }
        }
        private string _companyMobile;

        public string CompanyMobile
        {
            get { return _companyMobile; }
            set { _companyMobile = value; }
        }
        private byte[] _companyLogo;

        public byte[] CompanyLogo
        {
            get { return _companyLogo; }
            set { _companyLogo = value; }
        }
        private string _companyShortName;

        public string CompanyShortName
        {
            get { return _companyShortName; }
            set { _companyShortName = value; }
        }
        private string _companySlogun;

        public string CompanySlogun
        {
            get { return _companySlogun; }
            set { _companySlogun = value; }
        }
        private string _house;

        public string House
        {
            get { return _house; }
            set { _house = value; }
        }
        private string _road;

        public string Road
        {
            get { return _road; }
            set { _road = value; }
        }
        private string _sector;

        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }
        private string _landmark;

        public string Landmark
        {
            get { return _landmark; }
            set { _landmark = value; }
        }
        private string _contactPersonName;

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set { _contactPersonName = value; }
        }
        private string _contactPersonDesignation;

        public string ContactPersonDesignation
        {
            get { return _contactPersonDesignation; }
            set { _contactPersonDesignation = value; }
        }
        private string _contactPersonContactNumber;

        public string ContactPersonContactNumber
        {
            get { return _contactPersonContactNumber; }
            set { _contactPersonContactNumber = value; }
        }
        private string _alternateContactPersonName;

        public string AlternateContactPersonName
        {
            get { return _alternateContactPersonName; }
            set { _alternateContactPersonName = value; }
        }
        private string _alternateContactPersonDesignation;

        public string AlternateContactPersonDesignation
        {
            get { return _alternateContactPersonDesignation; }
            set { _alternateContactPersonDesignation = value; }
        }
        private string _alternateContactPersonContactNumber;

        public string AlternateContactPersonContactNumber
        {
            get { return _alternateContactPersonContactNumber; }
            set { _alternateContactPersonContactNumber = value; }
        }
        private string _companyPhones;

        public string CompanyPhones
        {
            get { return _companyPhones; }
            set { _companyPhones = value; }
        }
        private string _companyFax;

        public string CompanyFax
        {
            get { return _companyFax; }
            set { _companyFax = value; }
        }
        private string _companyURL;

        public string CompanyURL
        {
            get { return _companyURL; }
            set { _companyURL = value; }
        }
        private string _licenceID;

        public string LicenceID
        {
            get { return _licenceID; }
            set { _licenceID = value; }
        }

        private string _vatNumber;

        public string VatNumber
        {
            get { return _vatNumber; }
            set { _vatNumber = value; }
        }
        
        private string _faceBookID;

        public string FaceBookID
        {
            get { return _faceBookID; }
            set { _faceBookID = value; }
        }
        private string _linkedInID;

        public string LinkedInID
        {
            get { return _linkedInID; }
            set { _linkedInID = value; }
        }
        private string _twitterID;

        public string TwitterID
        {
            get { return _twitterID; }
            set { _twitterID = value; }
        }
        private string _youTubeID;

        public string YouTubeID
        {
            get { return _youTubeID; }
            set { _youTubeID = value; }
        }

        public BusinessType BusinessType
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public OwnershipType OwnershipType
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DataTable DtCompany { get => _dtCompany; set => _dtCompany = value; }
        public string ImageURL { get => _imageURL; set => _imageURL = value; }
        public int BTypeID { get => _bTypeID; set => _bTypeID = value; }
        public int OTypeID { get => _oTypeID; set => _oTypeID = value; }
        public int DistrictID { get => _districtID; set => _districtID = value; }

        private DataTable _dtCompany;
        private string _imageURL;
        private int _bTypeID;
        private int _oTypeID;
        private int _districtID;
    }
}