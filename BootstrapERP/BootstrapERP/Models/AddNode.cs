using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class AddNode
    {
        [Required(ErrorMessage = "Node type is required.")]
        public string NodeTypeRbtn { get; set; }

        [Required(ErrorMessage = "Node Name is required.")]
        public string NodeName { get; set; }

        //requiredif is conditional attributes which is used to validate Input type based on some condition
        
        public int? ParentName { get; set; }
        public DataTable DtRoleDetails { get => _dtRoleDetails; set => _dtRoleDetails = value; }
        public int RoleID { get => _roleID; set => _roleID = value; }

        private DataTable _dtRoleDetails;
        private int _roleID;
    }
}