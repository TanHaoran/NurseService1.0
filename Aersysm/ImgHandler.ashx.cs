using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Services
{
    /// <summary>
    /// ImgHandler 的摘要说明
    /// </summary>
    public class ImgHandler : IHttpHandler
    {

        public  void  ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                if (context.Request.Files.Count == 0)
                {
                    context.Response.Write("No file");
                    return;
                }

                HttpPostedFile f1 = context.Request.Files[0];
                string rootPath = HttpRuntime.BinDirectory.ToString();
                string path = rootPath.Remove(rootPath.Length - 4) + "FX";  // 加上FX
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string pathOK = path + "\\";

                string fileExtension = Path.GetExtension(f1.FileName).ToLower();

                string name = f1.FileName.Substring(0, f1.FileName.LastIndexOf(".")) + "split" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;

                using (var flieStream = new FileStream(pathOK + name, FileMode.Create))
                {
                    f1.InputStream.CopyTo(flieStream);
                    context.Response.Write(name);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);

            }
            return;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}