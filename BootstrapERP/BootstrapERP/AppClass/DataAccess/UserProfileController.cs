using BootstrapERP.AppClass.CommonClass;
using BootstrapERP.Controllers;
using BootstrapERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.AppClass.DataAccess
{
    public class UserProfileController : DataAccessBase
    {
        internal void Save(UserProfile objUserProfile)
        {
            try
            {
                objUserProfile.UserIdentifierID = objUserProfile.FullName + ":" + objUserProfile.Email;
                string[] fullName = objUserProfile.FullName.Split(new Char[] { ' ' });
                objUserProfile.FirstName = fullName[0];
                if (fullName.Length >= 2)
                {
                    objUserProfile.MiddleName = fullName[1];
                }

                if (fullName.Length >= 3)
                {
                    objUserProfile.LastName = fullName[2];

                }

                objUserProfile.IsApproved = 1;
                objUserProfile.IsLockedOut = 0;
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                objUserProfile.UserProfileID = objclsDataManipulation.GetAnUniqueidentifierNumber(this.ConnectionString);


                var storedProcedureComandText = "INSERT INTO [uUserProfile] ([UserProfileID],[Password],[Email],[Title],[FirstName],[MiddleName],[LastName],[DisplayName] " +
               " ,[DataUsed],[EntryUserID],[EntryDate],[UserIdentifier],[PasswordSalt],[MobilePIN],[LoweredEmail],[DateOfBirth],[PasswordQuestion],[PasswordAnswer]" +
               " ,[CreateDate] ,[Comment],[IsApproved],[IsLockedOut],[UserProfileTypeID]) VALUES (" +
               "'" + objUserProfile.UserProfileID + "'" +
               ",'111'" +
               ",'" + objUserProfile.Email + "'" +
               ",'" + objUserProfile.Title + "'" +
               ",'" + objUserProfile.FirstName + "'" +
               ",'" + objUserProfile.MiddleName + "'" +
               ",'" + objUserProfile.LastName + "'" +
               ",'" + objUserProfile.DisplayName + "'" +
               ",'A'" +
               ",'" + objUserProfile.EntryUserName + "'" +
               ",CAST(GETDATE() AS DateTime)" +
               ",'" + objUserProfile.UserIdentifierID + "'" +
               ",'" + objUserProfile.PasswordSalt + "'" +
               ",'" + objUserProfile.MobilePIN + "'" +
               ",'" + objUserProfile.LoweredEmail + "'" +
               ",'" + objUserProfile.DateOfBirth + "'" +
               ",'" + objUserProfile.PasswordQuestion + "'" +
               ",'" + objUserProfile.PasswordAnswer + "'" +
               ",'" + objUserProfile.CreateDate + "'" +
               ",'" + objUserProfile.Comment + "'" +
               "," + objUserProfile.IsApproved + "" +
               "," + objUserProfile.IsLockedOut + "" +
               ",'" + objUserProfile.UserProfileTypeID + "'" +
               " );";

                storedProcedureComandText += @"INSERT INTO [uUserList]([UserID],[UserTypeID],[UserProfileID],[UserName],[IsAnonymous],[LastActivityDate]) VALUES (NEWID()
               ,1 " +
               ",'" + objUserProfile.UserProfileID + "'" +
               ",'" + objUserProfile.UserIdentifierID + "'" +
               ",NULL,NULL);";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);
                UserCodeController objUserCodeController = new UserCodeController();
                objUserCodeController.SendSecurityCode(objUserProfile.UserProfileID, objUserProfile.Email);
            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal void Update(UserProfile objUserProfile)
        {
            try
            {
                string[] fullName = objUserProfile.FullName.Split(new Char[] { ' ' });
                objUserProfile.FirstName = fullName[0];
                if (fullName.Length >= 2)
                {
                    objUserProfile.MiddleName = fullName[1];
                }

                if (fullName.Length >= 3)
                {
                    objUserProfile.LastName = fullName[2];
                }

                var storedProcedureComandText = @"UPDATE [uUserProfile]
                   SET [MobilePIN] = ISNULL('" + objUserProfile.MobilePIN + "'" + ",MobilePIN)"
                   + ",[Title] = ISNULL('" + objUserProfile.Title + "',Title)"
                  + ",[FirstName] = ISNULL('" + objUserProfile.FirstName + "',FirstName)"
                  + ",[MiddleName] = ISNULL('" + objUserProfile.MiddleName + "',MiddleName)"
                  + ",[LastName] = ISNULL('" + objUserProfile.LastName + "',LastName)"
                  + ",[DisplayName] = ISNULL('" + objUserProfile.DisplayName + "',DisplayName)"
                  + ",[DateOfBirth] = ISNULL('" + objUserProfile.BirthDate + "',DateOfBirth)"
                  + ",[LastUpdateDate] = ISNULL(GETDATE(),LastUpdateDate)"
                  + ",[LastUpdateUserID] = ISNULL('" + objUserProfile.EntryUserName + "',LastUpdateUserID)"
                  + " WHERE [DataUsed] = 'A' AND [UserProfileID] = '" + objUserProfile.EntryUserName + "'";

                clsDataManipulation.StoredProcedureExecuteNonQuery(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }

        internal string GetUserProfileID(UserProfile objUserProfile)
        {
            try
            {
                string targetUserProfileID = null;
                string sql = @"SELECT DISTINCT UserProfileID FROM [uUserProfile] WHERE DataUsed = 'A' AND IsApproved = 1 AND IsLockedOut = 0 AND UserIdentifier " +
                 "= '" + objUserProfile.UserIdentifierID + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetUserProfileID = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetUserProfileID;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal string GetUserProfileID(string userEmail)
        {
            try
            {
                string targetUserProfileID = null;
                string sql = @"SELECT DISTINCT UserProfileID FROM [uUserProfile] WHERE DataUsed = 'A' AND IsApproved = 1 AND IsLockedOut = 0 AND Email " +
                 "= '" + userEmail + "'";
                clsDataManipulation objclsDataManipulation = new clsDataManipulation();
                targetUserProfileID = objclsDataManipulation.GetSingleValueAsString(this.ConnectionString, sql);
                return targetUserProfileID;

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
        internal DataTable GetProfileRecord(string userID)
        {
            try
            {
                DataTable dtProfileRecord = null;
                var storedProcedureComandText = @"SELECT A.[UserProfileTypeID],A.[Password],A.[PasswordFormat],A.[PasswordSalt],A.[MobilePIN],A.[Email],A.[LoweredEmail],A.[Title],
                (A.[FirstName]+ ' '+A.[MiddleName]+ ' '+A.[LastName]) AS FullName,A.[DisplayName],A.[DateOfBirth],A.[PasswordQuestion],A.[PasswordAnswer],A.[IsApproved],A.[IsLockedOut]
                ,A.[CreateDate],A.[LastLoginDate],A.[LastPasswordChangedDate],A.[LastLockoutDate],A.[FailedPasswordAttemptCount],A.[FailedPasswordAnswerAttemptCount]
                ,A.[Comment],A.[EntryUserID],A.[EntryDate],A.[LastUpdateDate],A.[LastUpdateUserID],A.[NextLockOutDate],A.[SecurityCode],A.[UserIdentifier]
                FROM [uUserProfile] A WHERE A.[DataUsed]='A' AND A.[UserProfileID] = '" + userID + "'";
                return dtProfileRecord = clsDataManipulation.GetData(this.ConnectionString, storedProcedureComandText);

            }
            catch (Exception msgException)
            {

                throw msgException;
            }
        }
    }
}