using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class HeaderNavBarPermission : DataAccessBase
    {
        internal DataTable GetUserNodes(HeaderNavBar objHeaderNavBar)
        {
            try
            {
                string sqlString = @";WITH parents AS (SELECT D.[NodeTypeID], D.[ActivityName], D.[ControllerTitle], D.[ActionTitle],D.[PNodeTypeID] FROM uUsersInRoles A
                INNER JOIN uRoleSetup B ON A.RoleID = B.RoleID AND A.RoleTypeID = B.RoleTypeID
                INNER JOIN uRoleSetupDetails C ON B.RoleID = C.RoleID
                INNER JOIN [uDefaultNodeList] D ON C.NodeID = D.NodeTypeID
                WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed = 'A'  
				AND A.UserProfileID = '"+ objHeaderNavBar.EntryUserName + "'  AND D.ShowPosition = "+ objHeaderNavBar.ShowPosition + ""+
				" UNION ALL"+
				" SELECT t.[NodeTypeID], t.[ActivityName], t.[ControllerTitle], t.[ActionTitle],t.[PNodeTypeID] FROM parents "+
				" INNER JOIN [uDefaultNodeList] t ON t.[NodeTypeID] =  parents.[PNodeTypeID] "+
				" )"+
                " SELECT distinct * FROM parents;";
                var dtNodes = clsDataManipulation.GetData(this.ConnectionString, sqlString);
                return dtNodes;

                //string sqlString = @"SELECT DISTINCT D.[NodeTypeID], D.[ActivityName], D.[ControllerTitle], D.[ActionTitle],D.[PNodeTypeID] FROM uUsersInRoles A
                //INNER JOIN uRoleSetup B ON A.RoleID = B.RoleID AND A.RoleTypeID = B.RoleTypeID
                //INNER JOIN uRoleSetupDetails C ON B.RoleID = C.RoleID
                //INNER JOIN [uDefaultNodeList] D ON C.NodeID = D.NodeTypeID
                //WHERE A.DataUsed = 'A' AND B.DataUsed = 'A' AND C.DataUsed = 'A' AND D.DataUsed = 'A' AND " +
                //" A.CompanyID = " + objHeaderNavBar.CompanyID + "" +
                //" AND D.CompanyID = " + objHeaderNavBar.CompanyID + "" +
                //" AND A.UserProfileID = '" + objHeaderNavBar.EntryUserName + "' " +
                //" AND D.ShowPosition = " + objHeaderNavBar.ShowPosition + ";";

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}