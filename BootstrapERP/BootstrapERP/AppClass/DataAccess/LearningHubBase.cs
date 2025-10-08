using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class LearningHubBase : DataAccessBase
    {
        internal DataTable GetContentHeaderRecord()
        {
            try
            {
                DataTable dtContentHeader = null;
                var storedProcedureComandText = @"SELECT [CourseID],[CourseName],[WritingStatus] FROM [lrnContentHeader] A
                WHERE A.[DataUsed] = 'A' AND A.[BranchID]=1 AND A.[CompanyID]=1
                ORDER BY A.CourseName";
                dtContentHeader = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtContentHeader;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetContentHeaderRecord(ContentHeader objContentHeaderSearch)
        {
            try
            {
                DataTable dtContentHeader = null;
                var storedProcedureComandText = @"SELECT [CourseID],[CourseName],[WritingStatus] FROM [lrnContentHeader] A
                WHERE A.[DataUsed] = 'A' AND A.[BranchID]=1 AND A.[CompanyID]=1 AND A.CourseTypeID = " + objContentHeaderSearch.CourseTypeID + "" +
                " AND A.CourseCategoryID = " + objContentHeaderSearch.CourseCategoryID + "" +
                " AND A.LearningPath = " + objContentHeaderSearch.LearningPath + "" +
                " AND A.CourseProviderName = " + objContentHeaderSearch.CourseProvider + "" +
                " AND A.CourseLevelID = " + objContentHeaderSearch.CourseLevelID + "" +
                " ORDER BY A.CourseName";
                dtContentHeader = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtContentHeader;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetContentHeaderRecordByName(ContentHeader objContentHeaderSearch)
        {
            try
            {
                DataTable dtContentHeader = null;
                var storedProcedureComandText = @"SELECT [CourseID],[CourseName],[WritingStatus] FROM [lrnContentHeader] A
                WHERE A.[DataUsed] = 'A' AND A.[BranchID]=1 AND A.[CompanyID]=1 AND A.CourseName LIKE  '%" + objContentHeaderSearch.CourseName + "%'" +
                " ORDER BY A.CourseName";
                dtContentHeader = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtContentHeader;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}