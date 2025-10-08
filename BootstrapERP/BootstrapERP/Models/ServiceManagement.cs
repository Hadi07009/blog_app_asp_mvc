using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class ServiceManagement : CompanySetup
    {
        private int _serviceID;

        public int ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }
        private string _serviceName;

        public string ServiceName
        {
            get { return _serviceName; }
            set
            {
                if (value == null)
                {
                    throw new Exception(" Service name is required ");
                } _serviceName = value;
            }
        }
        private string _serviceDescription;

        public string ServiceDescription
        {
            get { return _serviceDescription; }
            set { _serviceDescription = value; }
        }
        private int _serviceValueID;

        public int ServiceValueID
        {
            get { return _serviceValueID; }
            set { _serviceValueID = value; }
        }
        private int _billingFrequencyType;

        public int BillingFrequencyType
        {
            get { return _billingFrequencyType; }
            set { _billingFrequencyType = value; }
        }
        private int _paymentType;

        public int PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }
        private DateTime _serviceValueStartDate;

        public DateTime ServiceValueStartDate
        {
            get { return _serviceValueStartDate; }
            set { _serviceValueStartDate = value; }
        }
        private int _vatRate;

        public int VatRate
        {
            get { return _vatRate; }
            set { _vatRate = value; }
        }
        private int _vatCalculationProcessID;

        public int VatCalculationProcessID
        {
            get { return _vatCalculationProcessID; }
            set { _vatCalculationProcessID = value; }
        }
        private int _serviceCategoryTypeID;

        public int ServiceCategoryTypeID
        {
            get { return _serviceCategoryTypeID; }
            set {
                if (value == -1)
                {
                   throw new Exception(" Service category is required "); 
                } _serviceCategoryTypeID = value;
            }
        }
        private double _serviceValue;

        public double ServiceValue
        {
            get { return _serviceValue; }
            set { _serviceValue = value; }
        }
        private DataTable _dtServices;

        public DataTable DtServices
        {
            get { return _dtServices; }
            set { _dtServices = value; }
        }
        private int _serviceInformatioID;

        public int ServiceInformatioID
        {
            get { return _serviceInformatioID; }
            set { _serviceInformatioID = value; }
        }
        private int _packageID;

        public int PackageID
        {
            get { return _packageID; }
            set { _packageID = value; }
        }
        private DataTable _dtServiceDescription;

        public DataTable DtServiceDescription
        {
            get { return _dtServiceDescription; }
            set { _dtServiceDescription = value; }
        }
        private int _numberOfUser;

        public int NumberOfUser
        {
            get { return _numberOfUser; }
            set { _numberOfUser = value; }
        }
        private byte[] _serviceLogo;

        public byte[] ServiceLogo
        {
            get { return _serviceLogo; }
            set { _serviceLogo = value; }
        }

    }
}