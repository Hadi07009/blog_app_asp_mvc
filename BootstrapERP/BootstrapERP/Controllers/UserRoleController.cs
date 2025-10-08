using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class UserRoleController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private UserPermission _objUserPermission;
        private PermissionController _objPermissionController;
        // GET: UserRole
        public ActionResult Index()
        {
            try
            {
                var queryRoleType = _objdbERPSolutionEntities.uRoleTypeSetups.Select(c => new { c.RoleTypeID, c.RoleTypeTitle });
                ViewBag.BlogRoleType = new SelectList(queryRoleType.AsEnumerable(), "RoleTypeID", "RoleTypeTitle");
                ViewBag.BlogRoleType = AddDefaultOption(ViewBag.BlogRoleType, "Select", null);
                return View();

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        public JsonResult Index(string values)
        {
            try
            {
                string userIdentifierID = null;
                _objUserPermission = new UserPermission();
                var id = values.Split(',');
                int indexid = Array.IndexOf(id, id.Where(x => x.Contains("flagType")).FirstOrDefault());
                List<int> listRole = new List<int>();
                List<string> listRoleType = new List<string>();

                int index = 0;
                
                foreach (var item in id)
                {
                    if (index == 0)
                    {
                        userIdentifierID = item;
                    }
                    else if (index < indexid && index != 0)
                    {
                        listRoleType.Add(item.ToString());
                    }
                    else if (index > indexid)
                    {
                        int roleID = int.Parse(item);
                        listRole.Add(roleID);
                    }

                    index++;
                }

                _objUserPermission.roleList = listRole;
                _objUserPermission.roleTypeList = listRoleType;
                EmployeeSetup objEmployeeSetup = new EmployeeSetup();
                UserProfileController objUserProfileController = new UserProfileController();
                objEmployeeSetup.EmployeeID = objUserProfileController.GetUserProfileID(userIdentifierID);
                objEmployeeSetup.CompanyID = LoginUserInformation.CompanyID;
                objEmployeeSetup.EntryUserName = LoginUserInformation.UserID;
                _objPermissionController = new PermissionController();
                _objPermissionController.SaveUserRole(objEmployeeSetup, _objUserPermission);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }

        private IEnumerable<SelectListItem> AddDefaultOption(IEnumerable<SelectListItem> list, string dataTextField, string selectedValue)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = dataTextField, Value = selectedValue });
            items.AddRange(list);
            return items;
        }

        [HttpPost]
        public ActionResult GetHierarchy(string Message)
        {
            try
            {
                List<uRoleSetup> hdList;
                List<HierarchyViewModel> records;
                using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                {
                    hdList = context.uRoleSetups.Where(u => u.RoleTypeID == Message).ToList();

                    int indexTemp = 0;
                    foreach (var nodeID in hdList)
                    {
                        hdList[indexTemp].PRoleID = 111;
                        indexTemp++;
                    }

                    records = hdList.Where(l => l.PRoleID == 111)
                        .Select(l => new HierarchyViewModel
                        {
                            Id = l.RoleID,
                            text = l.RoleName,
                            perentId = l.PRoleID
                        }).ToList();


                }

                return Json(records, JsonRequestBehavior.DenyGet);


            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        public ActionResult GetAllRoles(string Message)
        {
            try
            {
                List<uRoleSetup> hdList;
                List<HierarchyViewModel> records;
                using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                {
                    //hdList = context.uRoleSetups.Where(u => u.RoleTypeID == Message).ToList();
                    hdList = context.uRoleSetups.ToList();

                    int indexTemp = 0;
                    foreach (var nodeID in hdList)
                    {
                        hdList[indexTemp].PRoleID = 111;

                        string IdTemp = nodeID.RoleID.ToString();
                        _objPermissionController = new PermissionController();
                        string nodeCheckValueTemp = _objPermissionController.CheckUserRole(Message, IdTemp);
                        hdList[indexTemp].NodeCheckValue = nodeCheckValueTemp;

                        indexTemp++;
                    }

                    records = hdList.Where(l => l.PRoleID == 111)
                        .Select(l => new HierarchyViewModel
                        {
                            Id = l.RoleID,
                            text = l.RoleName,
                            nodeCheckValue = l.NodeCheckValue,
                            perentId = l.PRoleID
                        }).ToList();


                }

                return Json(records, JsonRequestBehavior.DenyGet);


            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        public JsonResult CheckUserEmail(string Message)
        {
            try
            {
                _objPermissionController = new PermissionController(); 
                int records = _objPermissionController.CheckEmail(Message);
                return Json(records, JsonRequestBehavior.DenyGet);
                
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [HttpPost]
        public ActionResult GetAllRoleType(string Message)
        {
            try
            {
                List<uRoleTypeSetup> hdList;
                List<HierarchyViewModel> records;
                using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                {
                    //hdList = context.uRoleSetups.Where(u => u.RoleTypeID == Message).ToList();
                    hdList = context.uRoleTypeSetups.ToList();

                    int indexTemp = 0;
                    foreach (var nodeID in hdList)
                    {
                        hdList[indexTemp].PRoleTypeID = 111;

                        string IdTemp = nodeID.RoleTypeID.ToString();
                        _objPermissionController = new PermissionController();
                        string nodeCheckValueTemp = _objPermissionController.CheckUserRoleType(Message, IdTemp);
                        hdList[indexTemp].NodeCheckValue = nodeCheckValueTemp;

                        indexTemp++;
                    }

                    records = hdList.Where(l => l.PRoleTypeID == 111)
                        .Select(l => new HierarchyViewModel
                        {
                            Id = Convert.ToInt32( l.RoleTypeID),
                            text = l.RoleTypeTitle,
                            nodeCheckValue = l.NodeCheckValue,
                            perentId = l.PRoleTypeID
                        }).ToList();
                }

                return Json(records, JsonRequestBehavior.DenyGet);


            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult GetHierarchyAll()
        {
            try
            {
                List<uRoleSetup> hdList;
                List<HierarchyViewModel> records;
                using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                {
                    hdList = context.uRoleSetups.ToList();

                    records = hdList.Where(l => l.PRoleID == null)
                        .Select(l => new HierarchyViewModel
                        {
                            Id = l.RoleID,
                            text = l.RoleName,
                            perentId = l.PRoleID
                        }).ToList();
                }

                return this.Json(records, JsonRequestBehavior.AllowGet);


                //List<uRoleSetup> hdList;
                //List<HierarchyViewModel> records;
                //using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                //{
                //    hdList = context.uRoleSetups.Where(u => u.RoleTypeID == Message).ToList();

                //    int indexTemp = 0;
                //    foreach (var nodeID in hdList)
                //    {
                //        hdList[indexTemp].PRoleID = 111;
                //        indexTemp++;
                //    }

                //    records = hdList.Where(l => l.PRoleID == 111)
                //        .Select(l => new HierarchyViewModel
                //        {
                //            Id = l.RoleID,
                //            text = l.RoleName,
                //            perentId = l.PRoleID
                //            ,
                //            children = GetChildren(hdList, l.RoleID)
                //        }).ToList();


                //}

                //return Json(records, JsonRequestBehavior.AllowGet);


            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private List<HierarchyViewModel> GetChildren(List<uRoleSetup> hdList, int parentId)
        {
            try
            {
                return hdList.Where(l => l.PRoleID == parentId)
                .Select(l => new HierarchyViewModel
                {
                    Id = l.RoleID,
                    text = l.RoleName,
                    perentId = l.PRoleID,
                    nodeCheckValue = l.NodeCheckValue,
                    children = GetChildren(hdList, l.RoleID)
                }).ToList();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}