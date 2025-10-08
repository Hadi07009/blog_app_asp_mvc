using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class HeaderNavBar:BranchSetup
    {
        private DataTable _dtNodes;
        private int _showPosition;

        public DataTable DtNodes { get => _dtNodes; set => _dtNodes = value; }
        public int ShowPosition { get => _showPosition; set => _showPosition = value; }
    }
}