using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class NodeListController:DataAccessBase
    {
        public void Save(NodeList objNodeList)
        {
            try
            {
                //if (objNodeList.ParentNodeTypeID.ToString().Length == 6)
                //{
                //    throw new Exception(" please select parent node correctly.");

                //}


                objNodeList.NodeTypeID = Convert.ToInt32(objNodeList.CompanyID.ToString() + objNodeList.BranchID.ToString());
                if (objNodeList.ActivityID == 4)
                {
                    objNodeList.NodeTypeID = Convert.ToInt32(objNodeList.NodeTypeID.ToString() + GetSeqNoForForm());
                }
                else
                {
                    objNodeList.NodeTypeID = Convert.ToInt32(objNodeList.NodeTypeID.ToString() + GetSeqNoForModule());
                    objNodeList.ShowPosition = objNodeList.ShowPosition == -1 ? 1 : objNodeList.ShowPosition;//need to be check

                }

                var storedProcedureComandText = "INSERT INTO [suDefaultNodeList] ([CompanyID],[BranchID],[ActivityID],[ActivityName],[SeqNo],[NodeTypeID],[PNodeTypeID]" +
                                            " ,[ControllerTitle],[ActionTitle],[ShowPosition],[DataUsed],[EntryUserID],[EntryDate]) VALUES ( " +
                                                 objNodeList.CompanyID + "," +
                                                 objNodeList.BranchID + ", " +
                                                 objNodeList.ActivityID + ", '" +
                                                 objNodeList.ActivityName + "', " +
                                                 objNodeList.SeqNo + ", " +
                                                 objNodeList.NodeTypeID + ", " +
                                                 objNodeList.ParentNodeTypeID + ", '" +
                                                 objNodeList.ControllerTitle + "', '" +
                                                 objNodeList.ActionTitle + "', " +
                                                 objNodeList.ShowPosition + ",'" +
                                                 "A" + "', '" +
                                                 objNodeList.EntryUserName + "'," +
                                                 "CAST(GETDATE() AS DateTime));";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public int GetSeqNo()
        {
            try
            {
                int seqNo = 0;
                var storedProcedureComandText = "SELECT ISNULL( MAX( [SeqNo]),0) +1  FROM [suDefaultNodeList]";
                seqNo = clsDataManipulation.GetMaximumValueUsingSQL(this.ConnectionString, storedProcedureComandText);
                return seqNo;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string GetSeqNoForForm()
        {
            try
            {
                string seqNo = null;
                var storedProcedureComandText = @" DECLARE @RefNumber VARCHAR(10),@countData INT
                                              SET @countData = ( SELECT ISNULL( MAX( [SeqNo]),0) +1  FROM [suDefaultNodeList])
                                              SET @RefNumber=STUFF('0000',5-LEN(@countData),20,@countData)
                                              SELECT @RefNumber";
                var dtSeq = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                foreach (DataRow item in dtSeq.Rows)
                {
                    seqNo = item.ItemArray[0].ToString();

                }
                return seqNo;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        public string GetSeqNoForModule()
        {
            try
            {
                string seqNo = null;
                var storedProcedureComandText = @" DECLARE @RefNumber VARCHAR(10),@countData INT
                                              SET @countData = ( SELECT ISNULL( MAX( [SeqNo]),0) +1  FROM [suDefaultNodeList])
                                              SET @RefNumber=STUFF('000',4-LEN(@countData),20,@countData)
                                              SELECT @RefNumber";
                var dtSeq = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                foreach (DataRow item in dtSeq.Rows)
                {
                    seqNo = item.ItemArray[0].ToString();

                }
                return seqNo;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public DataTable GetNodeDetails(NodeList objNodeList)
        {
            try
            {
                DataTable dtNodeValue = null;
                var storedProcedureComandText = @"SELECT A.[ActivityID]
                ,A.[ActivityName]   
	            ,A.[ShowPosition]         
                ,A.[ControllerTitle]    
                ,A.[ActionTitle]
                ,[PNodeTypeID]
	            FROM [suDefaultNodeList] A WHERE A.[CompanyID] = " + objNodeList .CompanyID+ " AND"+ 
                  " A.[BranchID] = "+ objNodeList .BranchID+ "  AND A.[NodeTypeID] = "+ objNodeList.NodeTypeID+ " ";
                dtNodeValue = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtNodeValue;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        public void Update( NodeList objNodeList)
        {
            try
            {
                if (objNodeList.ShowPosition == -1)
                {
                    objNodeList.ShowPosition = 0;

                }


                var storedProcedureComandText = "UPDATE [suDefaultNodeList] " +
                                               " SET " +
                                               " [ActivityName] = ISNULL('" + objNodeList.ActivityName + "',ActivityName) " +
                                                 " ,[ControllerTitle] = ISNULL('" + objNodeList.ControllerTitle + "',ControllerTitle) " +
                                                 " ,[ActionTitle] = ISNULL('" + objNodeList.ActionTitle + "',ActionTitle) " +
                                                 " ,[ShowPosition] = ISNULL(" + objNodeList.ShowPosition + ",ShowPosition) " +
                                                 " ,[LastUpdateDate] = CAST(GETDATE() AS DateTime) " +
                                                 " ,[LastUpdateUserID] = '"+ objNodeList .EntryUserName+ "'" +
                                                 " WHERE [NodeTypeID] = " + objNodeList.NodeTypeID + "";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }

        }
    }
}