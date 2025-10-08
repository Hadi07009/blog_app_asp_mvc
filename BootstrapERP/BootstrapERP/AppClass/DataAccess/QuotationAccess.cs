using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Models;

namespace BootstrapERP.AppClass.DataAccess
{
    public class QuotationAccess : DataAccessBase
    {
        internal void SaveAuthor(QuoteAuthor objQuoteAuthor)
        {
            try
            {
                objQuoteAuthor.QuoteAuthorID = this.GetMaxAuthorID();
                var storedProcedureComandText = @"INSERT INTO [blogQuoteAuthor]
               ([CompanyID]
               ,[BranchID]
               ,[ApplicationID]
               ,[QuoteAuthorID]
               ,[AuthorTypeID]
               ,[AuthorFullName]
               ,[GenderID]
               ,[ReligionID]
               ,[FamousForID]
               ,[Nationality]
               ,[DateOfBirth]
               ,[DateDied]
               ,[DataUsed]
               ,[EntryDate]
               ,[EntryUserID]
		       )
                VALUES (" +
               "" + objQuoteAuthor.CompanyID + "" +
               "," + objQuoteAuthor.BranchID + "" +
               "," + objQuoteAuthor.ApplicationID + "" +
               "," + objQuoteAuthor.QuoteAuthorID + "" +
               "," + objQuoteAuthor.AuthorTypeID + "" +
               ",'" + objQuoteAuthor.AuthorFullName + "'" +
               "," + objQuoteAuthor.GenderID + "" +
               "," + objQuoteAuthor.ReligionID + "" +
               "," + objQuoteAuthor.FamousForID + "" +
               ",'" + objQuoteAuthor.Nationality + "'" +
               ",'" + objQuoteAuthor.DateOfBirth + "'" +
               ",'" + objQuoteAuthor.DateDied + "'" +
               ",'A'" +
               ",CAST(GETDATE() AS DateTime)" +
               ",'" + objQuoteAuthor.EntryUserName + "'" +
               " );";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
                
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal DataTable GetAllQuotation(QuoteAuthor objQuoteAuthor)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @" SELECT DISTINCT A.QuoteID,A.QuoteDescription,B.AuthorFullName,A.QuoteAuthorID, A.QuoteTypeID,A.QuoteRemarks FROM blogQuote A
                INNER JOIN blogQuoteAuthor B ON A.QuoteAuthorID=B.QuoteAuthorID
                WHERE A.DataUsed='A' AND B.DataUsed='A'
                AND A.CompanyID=" + objQuoteAuthor .CompanyID+ " AND A.BranchID="+ objQuoteAuthor .BranchID+ "" +
                " AND A.ApplicationID="+ objQuoteAuthor .ApplicationID+ ""+
                " ORDER BY B.AuthorFullName";

                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetAllAuthorsQuotation(QuoteAuthor objQuoteAuthors)
        {
            try
            {
                DataTable dtCntentDescription = null;
                var storedProcedureComandText = @" SELECT DISTINCT A.QuoteID,A.QuoteDescription,B.AuthorFullName,A.QuoteAuthorID,A.EntryDate,A.QuoteRemarks FROM blogQuote A
                INNER JOIN blogQuoteAuthor B ON A.QuoteAuthorID=B.QuoteAuthorID WHERE A.DataUsed='A' AND B.DataUsed='A'" +
                " AND B.QuoteAuthorID=" + objQuoteAuthors.QuoteAuthorID + "" +
                " ORDER BY A.EntryDate DESC";
                dtCntentDescription = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);
                return dtCntentDescription;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void SaveQuotation(QuoteAuthor objQuoteAuthor)
        {
            try
            {
                objQuoteAuthor.QuoteID = this.GetMaxQuoteID();
                var storedProcedureComandText = @"INSERT INTO [blogQuote]
               ([CompanyID]
               ,[BranchID]
               ,[ApplicationID]
               ,[QuoteAuthorID]
               ,[QuoteTypeID]
               ,[QuoteDescription]
               ,[QuoteRemarks]
               ,[QuoteID]
               ,[DataUsed]
               ,[EntryDate]
               ,[EntryUserID]
		       )
                VALUES (" +
               "" + objQuoteAuthor.CompanyID + "" +
               "," + objQuoteAuthor.BranchID + "" +
               "," + objQuoteAuthor.ApplicationID + "" +
               "," + objQuoteAuthor.QuoteAuthorID + "" +
               "," + objQuoteAuthor.AuthorTypeID + "" +
               ",'" + objQuoteAuthor.QuoteDescription + "'" +
               ",'" + objQuoteAuthor.QuoteRemarks + "'" +
               "," + objQuoteAuthor.QuoteID + "" +
               ",'A'" +
               ",CAST(GETDATE() AS DateTime)" +
               ",'" + objQuoteAuthor.EntryUserName + "'" +
               " );";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal void UpdateQuotation(QuoteAuthor objQuoteAuthor)
        {
            try
            {
                var storedProcedureComandText = @"UPDATE [blogQuote]
               SET [CompanyID] = " + objQuoteAuthor.CompanyID + "" +
                  ",[BranchID] = " + objQuoteAuthor.BranchID + "" +
                  ",[ApplicationID] = " + objQuoteAuthor.ApplicationID + "" +
                  ",[QuoteAuthorID] = " + objQuoteAuthor.QuoteAuthorID + "" +
                  ",[QuoteTypeID] = " + objQuoteAuthor.AuthorTypeID + "" +
                  ",[QuoteDescription] = '" + objQuoteAuthor.QuoteDescription + "'" +
                  ",[QuoteRemarks] = '" + objQuoteAuthor.QuoteRemarks + "'" +
                  ",[DataUsed] = 'A'" +
                  ",[LastUpdateDate] =  CAST(GETDATE() AS DateTime)" +
                  ",[LastUpdateUserID] =  '" + objQuoteAuthor.EntryUserName + "'" +
             " WHERE [QuoteID] = " + objQuoteAuthor.QuoteID + ";";
                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        private int GetMaxAuthorID()
        {
            try
            {
                int authorID = 0;
                var storedProcedureComandText = "SELECT ISNULL( MAX( QuoteAuthorID),0) +1  as QuoteAuthorID FROM blogQuoteAuthor";
                authorID = clsDataManipulation.GetMaximumValueUsingSQL(this.ConnectionString, storedProcedureComandText);
                return authorID;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        private int GetMaxQuoteID()
        {
            try
            {
                int quoteID = 0;
                var storedProcedureComandText = "SELECT ISNULL( MAX( QuoteID),0) +1  as QuoteID FROM blogQuote";
                quoteID = clsDataManipulation.GetMaximumValueUsingSQL(this.ConnectionString, storedProcedureComandText);
                return quoteID;
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}