using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPWebApplication.AppClass.CommonClass
{
    public static class LoginUserInformation
    {
        public static string UserID
        {
            get { return HttpContext.Current.Session["@#$$%@)userID(@^&^&%"].ToString(); }
            set { HttpContext.Current.Session["@#$$%@)userID(@^&^&%"] = value; }
        }
        public static string UserProfileID
        {
            get { return HttpContext.Current.Session["@#$$%@)userProfileID(@^&^&%"].ToString(); }
            set { HttpContext.Current.Session["@#$$%@)userProfileID(@^&^&%"] = value; }

        }
        public static string UserName
        {
            get { return HttpContext.Current.Session["@#$$%@)userName(@^&^&%"].ToString(); }
            set { HttpContext.Current.Session["@#$$%@)userName(@^&^&%"] = value; }
        }
        public static int CompanyID
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["@#$$%@)lastPositionNo(@^&^&%"].ToString()); }
            set { HttpContext.Current.Session["@#$$%@)lastPositionNo(@^&^&%"] = value; }
        }
        public static string EmployeeCode
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)employeeCode(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)employeeCode(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)employeeCode(@^&^&%"] = value; }


            //get { return HttpContext.Current.Session["@#$$%@)employeeCode(@^&^&%"].ToString(); }
            //set { HttpContext.Current.Session["@#$$%@)employeeCode(@^&^&%"] = value; }

        }
        public static string EmployeeFullName
        {
            get { return HttpContext.Current.Session["@#$$%@)employeeFullName(@^&^&%"].ToString(); }
            set { HttpContext.Current.Session["@#$$%@)employeeFullName(@^&^&%"] = value; }

        }
        public static int BranchID
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["@#$$%@)branchID(@^&^&%"].ToString()); }
            set { HttpContext.Current.Session["@#$$%@)branchID(@^&^&%"] = value; }
        }

        public static int ApplicationID
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["@#$$%@)applicationID(@^&^&%"].ToString()); }
            set { HttpContext.Current.Session["@#$$%@)applicationID(@^&^&%"] = value; }
        }

        public static string LogginUserStatus
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)logginUserStatus(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)logginUserStatus(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)logginUserStatus(@^&^&%"] = value; }

        }

        public static string LogginUserIP
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)logginUserIP(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)logginUserIP(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)logginUserIP(@^&^&%"] = value; }

        }

        public static string CountryCode
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)countryCode(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)countryCode(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)countryCode(@^&^&%"] = value; }

        }

        public static string CountryName
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)countryName(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)countryName(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)countryName(@^&^&%"] = value; }

        }

        public static string CityName
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)cityName(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)cityName(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)cityName(@^&^&%"] = value; }

        }

        public static string ZipCode
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)zipCode(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)zipCode(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)zipCode(@^&^&%"] = value; }

        }

        public static string Timezone
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)timezone(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)timezone(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)timezone(@^&^&%"] = value; }

        }

        public static string Region
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)region(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)region(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)region(@^&^&%"] = value; }

        }

        public static string RegionName
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)regionName(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)regionName(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)regionName(@^&^&%"] = value; }

        }

        public static string RegionalTime
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)regionalTime(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)regionalTime(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)regionalTime(@^&^&%"] = value; }

        }

        public static string ComputerName
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)computerName(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)computerName(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)computerName(@^&^&%"] = value; }

        }

        public static string BrowserName
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)browserName(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)browserName(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)browserName(@^&^&%"] = value; }

        }

        public static string DeviceType
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)deviceType(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)deviceType(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)deviceType(@^&^&%"] = value; }

        }

        public static string UniqueSessionCode
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)uniqueSessionCode(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)uniqueSessionCode(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)uniqueSessionCode(@^&^&%"] = value; }

        }

        public static string LoginInfoID
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)loginInfoID(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)loginInfoID(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)loginInfoID(@^&^&%"] = value; }

        }

        public static string UserProfileTypeID
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)userProfileTypeID(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)userProfileTypeID(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)userProfileTypeID(@^&^&%"] = value; }

        }

        public static string UserEmailID
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)userEmailID(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)userEmailID(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)userEmailID(@^&^&%"] = value; }

        }

        public static string UserMobileNo
        {
            get
            {
                if (HttpContext.Current.Session["@#$$%@)userMobileNo(@^&^&%"] == null) { return ""; }

                return HttpContext.Current.Session["@#$$%@)userMobileNo(@^&^&%"].ToString();
            }
            set { HttpContext.Current.Session["@#$$%@)userMobileNo(@^&^&%"] = value; }

        }

    }
}