using BootstrapERP.AppClass.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class UserList
    {
        OnlineRegisterController _objOnlineRegisterController;
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set {
                if (value == null)
                {
                    throw new Exception("User name is required ");
                    
                } _userName = value;
            }
        }
        private string _userPassword;

        public string UserPassword
        {
            get {
                return _userPassword; }
            set {
                if (value == null)
                {
                    throw new Exception("Password is required ");
                    
                }_userPassword = value;


            }
        }
        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set {
                if (value == null)
                {
                    throw new Exception("Confirm password is required ");

                }_confirmPassword = value;

            }
        }
        
        private string _securityCode;

        public string SecurityCode
        {
            get { return _securityCode; }
            set {
                if (value == null)
                {
                    throw new Exception("Security code is required ");
                    
                } _securityCode = value;
            }
        }



        public int UserType { get; set; }
        private string _userEmail;

        public string UserEmail
        {
            get { return _userEmail; }
            set {
                if (value == null)
                {
                    throw new Exception("Email is required ");
                    
                } _userEmail = value;
            }
        }

        private string _userID;

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private int _isUser;

        public int IsUser
        {
            get { return _isUser; }
            set { _isUser = value; }
        }

        private string _currentPassword;
        public string CurrentPassword { get => _currentPassword; set => _currentPassword = value; }
    }
}