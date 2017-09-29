using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Services {
    /// <summary>
    /// AvatarImgHandler 的摘要说明
    /// </summary>
    public class NoticeHandler : IHttpHandler {

        //  RsModel<SaveFile> r = new Services.RsModel<SaveFile>();

        public void ProcessRequest(HttpContext context) {
            try {
                string strNo = "{ \"body\":{ \"Filename\":\"No file\"},\"code\":0,\"msg\":\"\"}";
                context.Response.ContentType = "text/plain";

                if (context.Request.Files.Count == 0) {
                    context.Response.Write(strNo);
                    return;
                }

                HttpPostedFile f1 = context.Request.Files[0];
                string pathLocal = ConfigurationManager.AppSettings["UploadNotice"].ToString();
                string rootPath = HttpRuntime.BinDirectory.ToString();
                string path = rootPath.Remove(rootPath.Length - 4) + pathLocal;
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                string pathOK = path + "\\";
                string fileExtension = Path.GetExtension(f1.FileName).ToLower();  //获取后缀
                string name = DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
                string str = "{ \"body\":{ \"Filename\":\"" + name + "\"},\"code\":0,\"msg\":\"\"}";
                using (var flieStream = new FileStream(pathOK + name, FileMode.Create)) {
                    f1.InputStream.CopyTo(flieStream);
                    context.Response.Write(str);
                }
            } catch (Exception ex) {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}