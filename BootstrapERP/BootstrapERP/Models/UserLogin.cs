using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class UserLogin:TeamSetup
    {
        private int _applicationID;
        private string _UserLoginDate;

        public int ApplicationID { get => _applicationID; set => _applicationID = value; }
        public string UserLoginDate { get => _UserLoginDate; set => _UserLoginDate = value; }
        public string EntryUserID { get => _entryUserID; set => _entryUserID = value; }

        private string _entryUserID;

        
    }
}