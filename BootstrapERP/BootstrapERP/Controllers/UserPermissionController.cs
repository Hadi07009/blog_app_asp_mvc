using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BootstrapERP.Controllers
{
    public class UserPermissionController : Controller
    {
        // GET: UserPermission
        private HeaderNavBar _objHeaderNavBar;
        private HeaderNavBarPermission _objHeaderNavBarPermission;
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private UserPermission _objUserPermission;
        private CompanySetup _objCompanySetup;
        private PermissionController _objPermissionController;
        private AddNode _objAddNode;
        List<int> listNode = new List<int>();

        public ActionResult Index()
        {
            //using (dbERPSolutionEntities context = new dbERPSolutionEntities())
            //{   
            //    var plist = context.suDefaultNodeLists.Where(p => p.PNodeTypeID == 111).Select(a => new
            //    {
            //        a.NodeTypeID,
            //        a.ActivityName
            //    }).ToList();
            //    ViewBag.plist = plist;
            //}
            //GetHierarchy();
            var queryRoleType = _objdbERPSolutionEntities.uRoleTypeSetups.Select(c => new { c.RoleTypeID, c.RoleTypeTitle });
            ViewBag.BlogRoleType = new SelectList(queryRoleType.AsEnumerable(), "RoleTypeID", "RoleTypeTitle");
            _objPermissionController = new PermissionController();
            _objCompanySetup = new CompanySetup();
            _objCompanySetup.CompanyID = LoginUserInformation.CompanyID;
            _objAddNode = new AddNode();
            DataTable dtNodes = _objPermissionController.GetRoleDetails(_objCompanySetup);
            _objAddNode.DtRoleDetails = dtNodes;
            return View(_objAddNode);
        }

        public ActionResult Nodes(string id,string roleTypeID, string roleName)
        {
            try
            {
                Session["selectedRole"] = id;
                //using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                //{
                //    var plist = context.suDefaultNodeLists.Where(p => p.PNodeTypeID == 111).Select(a => new
                //    {
                //        a.NodeTypeID,
                //        a.ActivityName
                //    }).ToList();
                //    ViewBag.plist = plist;
                //}
                //GetExistingNodes();
                var queryRoleType = _objdbERPSolutionEntities.uRoleTypeSetups.Select(c => new { c.RoleTypeID, c.RoleTypeTitle });
                ViewBag.BlogRoleType = new SelectList(queryRoleType.AsEnumerable(), "RoleTypeID", "RoleTypeTitle", roleTypeID);
                _objAddNode = new AddNode();
                _objAddNode.NodeName = roleName;
                _objAddNode.RoleID = Convert.ToInt32(id);
                

                _objPermissionController = new PermissionController();
                _objCompanySetup = new CompanySetup();
                _objCompanySetup.CompanyID = LoginUserInformation.CompanyID;
                DataTable dtNodes = _objPermissionController.GetRoleList(_objCompanySetup, _objAddNode);
                _objAddNode.DtRoleDetails = dtNodes;
                return View(_objAddNode);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public JsonResult GetExistingNodes()
        {
            List<uDefaultNodeList> hdList;
            List<HierarchyViewModel> records;
            using (dbERPSolutionEntities context = new dbERPSolutionEntities())
            {
                hdList = context.uDefaultNodeLists.ToList();
                int indexTemp = 0;
                foreach (var nodeID in hdList)
                {
                    string IdTemp = nodeID.NodeTypeID.ToString();
                    _objPermissionController = new PermissionController();
                    int selectedRoleId = Convert.ToInt32(Session["selectedRole"].ToString());
                    string nodeCheckValueTemp = _objPermissionController.CheckNodeID(selectedRoleId, IdTemp);
                    hdList[indexTemp].NodeCheckValue = nodeCheckValueTemp;
                    indexTemp++;
                }

                records = hdList.Where(l => l.PNodeTypeID == 111)
                    .Select(l => new HierarchyViewModel
                    {
                        Id = l.NodeTypeID,
                        text = l.ActivityName,
                        perentId = l.PNodeTypeID,
                        nodeCheckValue = l.NodeCheckValue,
                        children = GetChildren(hdList, l.NodeTypeID)
                    }).ToList();
            }

            return this.Json(records, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Nodes(string values)
        {
            _objUserPermission = new UserPermission();
            _objCompanySetup = new CompanySetup();
            _objCompanySetup.CompanyID = LoginUserInformation.CompanyID;
            _objCompanySetup.EntryUserName = LoginUserInformation.UserID;

            int index = 0;
            var id = values.Split(',');
            foreach (var item in id)
            {
                if (index == 0)
                {
                    _objUserPermission.RoleType = item;
                }
                else if (index == 1)
                {
                    _objUserPermission.RoleName = item;
                }
                else if (index == 2)
                {
                    _objUserPermission.RoleID = Convert.ToInt32( item);
                }
                else
                {
                    int ID = int.Parse(item);
                    listNode.Add(ID);
                }

                index++;
            }

            _objUserPermission.nodeList = listNode;


            _objPermissionController = new PermissionController();
            _objPermissionController.UpdateRoleData(_objCompanySetup, _objUserPermission);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            
        }


        [HttpPost]
        public JsonResult Index(string values)
        {
            _objUserPermission = new UserPermission();
            _objCompanySetup = new CompanySetup();
            _objCompanySetup.CompanyID = LoginUserInformation.CompanyID;
            _objCompanySetup.EntryUserName = LoginUserInformation.UserID;

            int index = 0;
            var id = values.Split(',');
            foreach (var item in id)
            {
                if (index == 0)
                {
                    _objUserPermission.RoleType = item;
                }
                else if (index == 1)
                {
                    _objUserPermission.RoleName = item;
                }
                else
                {
                    int ID = int.Parse(item);
                    listNode.Add(ID);
                }

                index++;
            }

            _objUserPermission.nodeList = listNode;

            
            _objPermissionController = new PermissionController();
            _objPermissionController.SaveRoleData(_objCompanySetup, _objUserPermission);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult Tree(string checkedNodes, string uncheckedNodes)
        {
            TempData["message"] = String.Format(CultureInfo.InvariantCulture,
                                                "c=[{0}] | u=[{1}]",
                                                checkedNodes, uncheckedNodes);
            return RedirectToAction("Index");
        }

        public JsonResult GetHierarchy()
        {
            List<uDefaultNodeList> hdList;
            List<HierarchyViewModel> records;
            using (dbERPSolutionEntities context = new dbERPSolutionEntities())
            {
                hdList = context.uDefaultNodeLists.ToList();

                records = hdList.Where(l => l.PNodeTypeID == 111)
                    .Select(l => new HierarchyViewModel
                    {
                        Id = l.NodeTypeID,
                        text = l.ActivityName,
                        perentId = l.PNodeTypeID,
                        children = GetChildren(hdList, l.NodeTypeID)
                    }).ToList();
            }

            return this.Json(records, JsonRequestBehavior.AllowGet);
        }


        private List<HierarchyViewModel> GetChildren(List<uDefaultNodeList> hdList, int parentId)
        {
            return hdList.Where(l => l.PNodeTypeID == parentId)
                .Select(l => new HierarchyViewModel
                {
                    Id = l.NodeTypeID,
                    text = l.ActivityName,
                    perentId = l.PNodeTypeID,
                    nodeCheckValue=l.NodeCheckValue,
                    children = GetChildren(hdList, l.NodeTypeID)
                }).ToList();
        }

        [HttpPost]
        public JsonResult ChangeNodePosition(int id, int parentId)
        {
            using (dbERPSolutionEntities context = new dbERPSolutionEntities())
            {
                var Hd = context.uDefaultNodeLists.First(l => l.NodeTypeID == id);
                Hd.PNodeTypeID = parentId;
                context.SaveChanges();
            }
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewNode(AddNode model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (dbERPSolutionEntities db = new dbERPSolutionEntities())
                    {
                        uDefaultNodeList hierarchyDetail = new uDefaultNodeList()
                        {
                            ActivityName = model.NodeName,
                            PNodeTypeID = model.ParentName.GetValueOrDefault(),
                        };

                        db.uDefaultNodeLists.Add(hierarchyDetail);
                        db.SaveChanges();
                    }
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteNode(string values)
        {
            try
            {
                using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                {
                    var id = values.Split(',');
                    foreach (var item in id)
                    {
                        int ID = int.Parse(item);
                        context.uDefaultNodeLists.RemoveRange(context.uDefaultNodeLists.Where(x => x.NodeTypeID == ID).ToList());
                        context.SaveChanges();
                    }

                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
                
            }

        }
    }
}