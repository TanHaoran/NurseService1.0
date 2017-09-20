using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Services
{
    /// <summary>
    /// QCImgHandler 的摘要说明
    /// </summary>
    public class QCImgHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string strNo = "{ \"body\":{ \"Filename\":\"No file\"},\"code\":0,\"msg\":\"\"}";
                context.Response.ContentType = "text/plain";
                if (context.Request.Files.Count == 0)
                {
                    context.Response.Write(strNo);
                    return;
                }
                HttpPostedFile f1 = context.Request.Files[0];
                string pathLocal = ConfigurationManager.AppSettings["UploadQC"].ToString();
                string rootPath = HttpRuntime.BinDirectory.ToString();
                string path = rootPath.Remove(rootPath.Length - 4) + pathLocal;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //string pathOK = path + "\\";
                //string fileExtension = Path.GetExtension(f1.FileName).ToLower();
                //string name = f1.FileName.Substring(0, f1.FileName.LastIndexOf(".")) + "time" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                //string str = "{ \"body\":{ \"Filename\":\"" + name + "\"},\"code\":0,\"msg\":\"\"}";
                //using (var flieStream = new FileStream(pathOK + name, FileMode.Create))
                //{
                //    f1.InputStream.CopyTo(flieStream);
                //    context.Response.Write(str);
                //}
                string pathOK = path + "\\";
                string fileExtension = Path.GetExtension(f1.FileName).ToLower();  //获取后缀
                string name = DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                string str = "{ \"body\":{ \"Filename\":\"" + name + "\"},\"code\":0,\"msg\":\"\"}";
                using (var flieStream = new FileStream(pathOK + name, FileMode.Create))
                {
                    f1.InputStream.CopyTo(flieStream);
                    context.Response.Write(str);
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