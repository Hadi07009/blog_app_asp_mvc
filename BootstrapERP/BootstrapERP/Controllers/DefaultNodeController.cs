using BootstrapERP.AppClass.DataAccess;
using BootstrapERP.Models;
using ERPWebApplication.AppClass.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapERP.Controllers
{
    public class DefaultNodeController : Controller
    {
        private dbERPSolutionEntities _objdbERPSolutionEntities = new dbERPSolutionEntities();
        private CompanySetup _objCompanySetup;
        private PermissionController _objPermissionController;
        private AddNode _objAddNode;
        List<int> listNode = new List<int>();
        private NodeListController _objNodeListController;
        // GET: DefaultNode
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult NewNode(string id)
        {
            try
            {
                var idTemp = id.Split(',');
                var selectedNode = "";
                foreach (var item in idTemp)
                {
                    selectedNode = item;
                }

                var defaultNodeLists = _objdbERPSolutionEntities.suDefaultNodeLists.Select(c => new { c.NodeTypeID, c.ActivityName });
                ViewBag.DefaultNode = new SelectList(defaultNodeLists.AsEnumerable(), "NodeTypeID", "ActivityName", selectedNode);
                var nodeTypes = _objdbERPSolutionEntities.uNodeTypes.Select(c => new { c.NodeTypeID, c.NodeTypeTitle });
                ViewBag.NodeTypes = new SelectList(nodeTypes.AsEnumerable(), "NodeTypeID", "NodeTypeTitle");
                var nodePositions = _objdbERPSolutionEntities.uNodePositions.Select(c => new { c.NodePositionID, c.NodePositionTitle });
                ViewBag.NodePositions = new SelectList(nodePositions.AsEnumerable(), "NodePositionID", "NodePositionTitle");

                return View();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult NewNode(string saveNewNode, string DefaultNode, string NodeTypes,
            string NodePositions, NodeList objNodeList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (saveNewNode != null)
                    {
                        _objNodeListController = new NodeListController();
                        objNodeList.ActivityID = Convert.ToInt32(NodeTypes);
                        objNodeList.BranchID = LoginUserInformation.BranchID;
                        objNodeList.CompanyID = LoginUserInformation.CompanyID;
                        objNodeList.ShowPosition = Convert.ToInt32(NodePositions);
                        objNodeList.ParentNodeTypeID = DefaultNode == string.Empty ? 0 : Convert.ToInt32(DefaultNode);
                        objNodeList.SeqNo = _objNodeListController.GetSeqNo();
                        objNodeList.EntryUserName = LoginUserInformation.UserID;
                        _objNodeListController.Save(objNodeList);
                    }
                }

                return RedirectToAction("Index", "DefaultNode");

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public ActionResult SubsistNode(string id)
        {
            try
            {
                NodeList _objNodeList = new NodeList();
                //_objNodeList.NodeTypeID = Convert.ToInt32(id);
                var idTemp = id.Split(',');
                foreach (var item in idTemp)
                {
                    _objNodeList.NodeTypeID = Convert.ToInt32(item);
                }

                _objNodeList.CompanyID = LoginUserInformation.CompanyID;
                _objNodeList.BranchID = LoginUserInformation.BranchID;
                _objNodeListController = new NodeListController();
                
                DataTable dtNodeDetails = _objNodeListController.GetNodeDetails( _objNodeList);
                foreach (DataRow row in dtNodeDetails.Rows)
                {
                    _objNodeList.ActivityName = row["ActivityName"].ToString() == null ? string.Empty : row["ActivityName"].ToString();
                    _objNodeList.ControllerTitle = row["ControllerTitle"].ToString() == "" ? null : row["ControllerTitle"].ToString();
                    _objNodeList.ActionTitle = row["ActionTitle"].ToString() == "" ? null : row["ActionTitle"].ToString();
                    int formPosition = row["ShowPosition"].ToString() == "0" ? -1 : Convert.ToInt32(row["ShowPosition"].ToString());
                    var ParentNodeTypeID = row["PNodeTypeID"].ToString() == "0" ? null : row["PNodeTypeID"].ToString();

                    var defaultNodeLists = _objdbERPSolutionEntities.suDefaultNodeLists.Select(c => new { c.NodeTypeID, c.ActivityName });
                    ViewBag.DefaultNode = new SelectList(defaultNodeLists.AsEnumerable(), "NodeTypeID", "ActivityName", ParentNodeTypeID);
                    
                    var nodePositions = _objdbERPSolutionEntities.uNodePositions.Select(c => new { c.NodePositionID, c.NodePositionTitle });
                    ViewBag.NodePositions = new SelectList(nodePositions.AsEnumerable(), "NodePositionID", "NodePositionTitle", formPosition);
                }

                return View(_objNodeList);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult SubsistNode(string updateNode, string NodePositions, NodeList objNodeList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (updateNode != null)
                    {
                        _objNodeListController = new NodeListController();
                        objNodeList.BranchID = LoginUserInformation.BranchID;
                        objNodeList.CompanyID = LoginUserInformation.CompanyID;
                        objNodeList.ShowPosition = Convert.ToInt32(NodePositions);
                        objNodeList.SeqNo = _objNodeListController.GetSeqNo();
                        objNodeList.EntryUserName = LoginUserInformation.UserID;
                        _objNodeListController.Update(objNodeList);
                    }
                }

                return RedirectToAction("Index", "DefaultNode");

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public JsonResult GetHierarchy()
        {
            try
            {
                List<suDefaultNodeList> hdList;
                List<HierarchyViewModel> records;
                using (dbERPSolutionEntities context = new dbERPSolutionEntities())
                {
                    hdList = context.suDefaultNodeLists.ToList();

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
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private List<HierarchyViewModel> GetChildren(List<suDefaultNodeList> hdList, int parentId)
        {
            try
            {
                return hdList.Where(l => l.PNodeTypeID == parentId)
                .Select(l => new HierarchyViewModel
                {
                    Id = l.NodeTypeID,
                    text = l.ActivityName,
                    perentId = l.PNodeTypeID,
                    nodeCheckValue = l.NodeCheckValue,
                    children = GetChildren(hdList, l.NodeTypeID)
                }).ToList();
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}