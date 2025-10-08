using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.ComponentModel;
using Microsoft.Win32; 

namespace ERPWebApplication.AppClass.CommonClass
{
    /// <summary>
    /// Displays MessageBox messages as a top most window
    /// </summary>
    static public class clsTopMostMessageBox
    {
        /// <summary>
        /// Displays a <see cref="MessageBox"/> but as a TopMost window.
        /// </summary>
        /// <param name="message">The text to appear in the message box.</param>
        /// <param name="title">The title of the message box.</param>
        /// <returns>The button pressed.</returns>
        /// <remarks>This will display with no title and only the OK button.</remarks>
        /// 


        public static void MsgConfirmBox(System.Web.UI.WebControls.Button btn, string strMessage)
        {
            strMessage = strMessage.Replace("'", "\\'");
            btn.Attributes.Add("onclick", "return confirm('" + strMessage + "');");

        }

        public static void MsgConfirmBox(string strMessage)
        {


            strMessage = strMessage.Replace("'", "\\'");
            string script = "<script type= \"text/javascript\">alert('" + strMessage + "'); </script> ";

            System.Web.UI.Page page =System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {

                //page.ClientScript.RegisterClientScriptBlock(typeof (clsStatic), "alert", script);
                System.Web.UI.ScriptManager.RegisterStartupScript(page, typeof(System.Web.UI.Page), "temp", script, false);
            }
        }



        public static void Show(string message)
        {
            //return Show(message, string.Empty, MessageBoxButtons.OK);
            string script = "<script type= \"text/javascript\">alert('" + message + "'); </script> ";

            System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
            System.Web.UI.ScriptManager.RegisterStartupScript(page, typeof(System.Web.UI.Page), "temp", script, false);
            
        }


        


    }
}
