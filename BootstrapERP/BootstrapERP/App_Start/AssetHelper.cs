using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace BootstrapERP.App_Start
{
    public static class AssetHelper
    {
        public static string VersionedContent(string path)
        {
            var filePath = HttpContext.Current.Server.MapPath(path);

            if (File.Exists(filePath))
            {
                var version = File.GetLastWriteTime(filePath).Ticks;
                return path + "?v=" + version;
            }

            return path;
        }
    }
}