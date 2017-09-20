using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using Aersysm.Domain;
using System.Reflection;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using ThoughtWorks.QRCode.Codec;
using System.Security.Cryptography;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using cn.jpush.api.common.resp;
using cn.jpush.api.device;

namespace Services
{
    public static class Common
    {

        // 极光推送
        public static String JPUSH_APP_KEY = "b1ec133146726dd0ea653988";
        public static String JPUSH_MASTER_SECRET = "0db2d8b4b6f59ed9ad82d40c";

        // 容联短信
        public static String CCPREST_ACCOUNT_SID = "8aaf07085d106c7f015d5eab60d1211c";
        public static String CCPREST_ACCOUNT_TOKEN = "6f093df4dc7746a98865e83d4b38971b";
        public static String CCPREST_APP_ID = "8aaf07085d106c7f015d5eab611a2121";


        #region 数据库插入默认时间
        public static DateTime StrToDateTime()
        {

            return Convert.ToDateTime("01/01/1980");
        }
        #endregion

        #region 百度API根据经纬度获取医院列表
        /// <summary>
        /// 百度API根据经纬度获取医院列表
        /// </summary>
        /// <param name="lng">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="Keyworld">关键词（医院、学校）</param>
        /// <param name="radius">搜索半径，米</param>
        /// <returns></returns>
        public static DataTable GetAddress(string lng, string lat, string Keyworld, string radius)
        {

           // string url = @"http://api.map.baidu.com/place/v2/search?query=医院&location=39.915,116.404&radius=5000&output=xml&ak=1gVf0B0NM88MKuj3sVqp1q2WMXWU1gFj";
            string url = @"http://api.map.baidu.com/place/v2/search?query=" + Keyworld + "&location=" + lat + "," + lng + @"&radius="+radius+"&output=xml&ak=1gVf0B0NM88MKuj3sVqp1q2WMXWU1gFj";
           // string url = @"http://api.map.baidu.com/place/v2/search?query=" + Keyworld + "&location=" + lat + "," + lng + @"&radius=" + radius + "&output=xml&ak=1gVf0B0NM88MKuj3sVqp1q2WMXWU1gFj";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            XmlDocument xmlDoc = new XmlDocument();
            string sendData = xmlDoc.InnerXml;
            byte[] byteArray = Encoding.Default.GetBytes(sendData);

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, System.Text.Encoding.GetEncoding("utf-8"));
            string responseXml = reader.ReadToEnd();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(responseXml);
            string status = xml.DocumentElement.SelectSingleNode("status").InnerText;

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            //dt.Columns.Add(new DataColumn("locationlat", typeof(System.String)));
            //dt.Columns.Add(new DataColumn("locationlng", typeof(System.String)));
           // dt.Columns.Add(new DataColumn("Address", typeof(System.String)));
            if (status == "0")
            {
                XmlNodeList nodes = xml.DocumentElement.GetElementsByTagName("result");
                var ddd = nodes;
                if (nodes.Count > 0)
                {
                    XmlNodeList nodess = xml.DocumentElement.GetElementsByTagName("name");

                    for (int i = 0; i < nodess.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Name"] = xml.DocumentElement.GetElementsByTagName("name")[i].InnerText;
                      //  dr["Address"] = xml.DocumentElement.GetElementsByTagName("address")[i].InnerText;
                        dt.Rows.Add(dr);
                    }

                    return dt;
                }
                else
                {
                    return dt;
                }

            }
            else
            {
                return dt;
            }
        }
        #endregion

        #region 根据配置文件获取上传文件路径
        //public static string GetImgPath()
        //{
        //    string pathLocal = "";
        //    pathLocal = ConfigurationManager.AppSettings["UploadQC"].ToString();
        //    return pathLocal;
        //}

        #endregion

        #region dt写入excel
            /// <summary>
            /// datatable写入excel
            /// </summary>
            /// <param name="source">datatable</param>
            /// <param name="fileName">保存路径</param>
            /// <param name="ht">null</param>
        public static void ExportToExcel(DataTable source, string fileName, Dictionary<string, string> ht)
        {


            StreamWriter excelDoc = new StreamWriter(fileName);
            string startExcelXml = ExcelHeader();
            startExcelXml += "<Styles>\r\n " +
                "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                "\r\n <Protection/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                "ss:Format=\"0.0000\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                "</Styles>\r\n ";
            const string endExcelXml = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            excelDoc.Write(startExcelXml);
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>");
            if (ht != null)
            {
                foreach (string keys in ht.Keys)
                {
                    excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(ht[keys].ToString());
                    excelDoc.Write("</Data></Cell>");
                }
            }
            else
            {
                foreach (DataColumn dc in source.Columns)
                {
                    excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(dc.ColumnName);
                    excelDoc.Write("</Data></Cell>");
                }

            }
            excelDoc.Write("</Row>");
            foreach (DataRow x in source.Rows)
            {
                rowCount++;
                //Excel 最多只能显示64000条数据，在同一个WorkSheet里
                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.Write("</Table>");
                    excelDoc.Write(" </Worksheet>");
                    excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    excelDoc.Write("<Table>");
                }
                excelDoc.Write("<Row>");

                List<string> columns = new List<string>();
                if (ht != null)
                {
                    foreach (string keys in ht.Keys)
                    {
                        columns.Add(keys);
                    }
                }
                else
                {
                    foreach (DataColumn dc in source.Columns)
                    {
                        columns.Add(dc.ColumnName);
                    }
                }
                foreach (string keys in columns)
                {
                    Type rowType = x[keys].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string xmLstring = x[keys].ToString();
                            xmLstring = xmLstring.Trim();
                            xmLstring = System.Security.SecurityElement.Escape(xmLstring);
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            excelDoc.Write(xmLstring);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DateTime":

                            DateTime xmlDate = (DateTime)x[keys];
                            string xmlDatetoString = "";
                            if (xmlDate != DateTime.MinValue)
                            {
                                xmlDatetoString = xmlDate.Year.ToString(CultureInfo.InvariantCulture) +
                                     "-" +
                                     (xmlDate.Month < 10 ? "0" +
                                     xmlDate.Month.ToString(CultureInfo.InvariantCulture) : xmlDate.Month.ToString(CultureInfo.InvariantCulture)) +
                                     "-" +
                                     (xmlDate.Day < 10 ? "0" +
                                     xmlDate.Day.ToString(CultureInfo.InvariantCulture) : xmlDate.Day.ToString(CultureInfo.InvariantCulture)) +
                                     "  " +
                                     (xmlDate.Hour < 10 ? "0" +
                                     xmlDate.Hour.ToString(CultureInfo.InvariantCulture) : xmlDate.Hour.ToString(CultureInfo.InvariantCulture)) +
                                     ":" +
                                     (xmlDate.Minute < 10 ? "0" +
                                     xmlDate.Minute.ToString(CultureInfo.InvariantCulture) : xmlDate.Minute.ToString(CultureInfo.InvariantCulture))
                                     //":" +
                                     //(xmlDate.Second < 10 ? "0" +
                                     //xmlDate.Second.ToString(CultureInfo.InvariantCulture) : xmlDate.Second.ToString(CultureInfo.InvariantCulture)) 
                                     ;
                            }
                            excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"String\">");
                            excelDoc.Write(xmlDatetoString);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            excelDoc.Write(x[keys].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                    "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[keys].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                  "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[keys].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            excelDoc.Write("");
                            excelDoc.Write("</Data></Cell>");
                            break;
                        default:
                            throw (new Exception(rowType + " not handled."));
                    }
                }
                excelDoc.Write("</Row>");

            }
            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            excelDoc.Write(endExcelXml);
            excelDoc.Close();
        }

        private static string ExcelHeader()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\"?>\n");
            sb.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
            sb.Append(
              "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            sb.Append("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
            sb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
            sb.Append("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            sb.Append("xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n");
            sb.Append(
              "<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sb.Append("</DocumentProperties>");
            sb.Append(
              "<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
            sb.Append("<ProtectStructure>False</ProtectStructure>\n");
            sb.Append("<ProtectWindows>False</ProtectWindows>\n");
            sb.Append("</ExcelWorkbook>\n");
            return sb.ToString();
        }
        #endregion

        #region list转dt
        /// <summary>
        /// List转datatable   2017.3.27YM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {

            //创建属性的集合    
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口    

            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例    
                DataRow row = dt.NewRow();
                //给row 赋值    
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable    
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion

        #region json转dt
        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }
        #endregion

        #region DataSet转Json
        /// <summary>
        /// DataSet转Json
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public  static  string ExecutDataSetToJson(DataSet ds)
        {

            string str = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    str = "[{";
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j != ds.Tables[0].Columns.Count - 1)
                        {

                            str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[0][j].ToString() + "\",";
                        }
                        else
                        {
                            str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[0][j].ToString() + "\"}";
                        }
                    }
                    str += "]";
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    str = "[";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        str += "{";
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (j != ds.Tables[0].Columns.Count - 1)
                            {

                                str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[i][j].ToString() + "\",";
                            }
                            else
                            {
                                str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[i][j].ToString() + "\"}";
                            }
                        }
                        if (i != ds.Tables[0].Rows.Count - 1)
                        {
                            str += ",";
                        }
                    }
                    str += "]";
                }
            }
            return str;

        }
        #endregion

        #region 生成二维码
        //生成二维码
        public static  string  SaveCodeFile(string info)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            System.Drawing.Image image = qrCodeEncoder.Encode(info,Encoding.GetEncoding("UTF-8"));
            string filename = info+DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".png";

            string rootPath = HttpRuntime.BinDirectory.ToString();
            string path = rootPath.Remove(rootPath.Length - 4) + "QRCode";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string pathOk = path + "\\";
            string filepath = pathOk + filename;
            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();

            return filename ;
        }
        #endregion

        #region 解码编码
        public static string Encode(string str)
        {
            return  System.Web.HttpUtility.UrlEncode(str, System.Text.Encoding.UTF8);
            //StringBuilder builder = new StringBuilder();
            //foreach (char c in str)
            //{
            //    if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
            //    {
            //        builder.Append(HttpUtility.UrlEncode(c.ToString(), System.Text.Encoding.UTF8).ToUpper());
            //    }
            //    else
            //    {
            //        builder.Append(c);
            //    }
            //}
            //return builder.ToString();
        }

        public static string Decode(string str)
        {
            return System.Web.HttpUtility.UrlDecode(str, System.Text.Encoding.UTF8);
            
        }
        #endregion

        #region md5
        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }
        #endregion

        #region rsa
        //–生成xml格式的密钥生成的密钥会以xml的格式放在你工程bin\Debug目录下。 第二种
        /// <summary>
        /// 生成RSA public key private key
        /// </summary>
        public static void GenerateRSAKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
          //  using (StreamWriter writer = new StreamWriter("PublicKey.xml"))//产生公匙
            using (StreamWriter writer = new StreamWriter(@"D:\Privatekey.xml"))
            {
                writer.WriteLine(rsa.ToXmlString(true));
            }
            using (StreamWriter writer = new StreamWriter(@"D:\PublicKey.xml"))
           // using (StreamWriter writer = new StreamWriter("PublicKey.xml"))
            {
                writer.WriteLine(rsa.ToXmlString(false));
            }
        }
        public static string GetPublickey()
        {
            string publickey;
            using (StreamReader reader = new StreamReader(@"D:\PublicKey.xml"))
            {
                publickey = reader.ReadLine();
            }
            return publickey;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string EncryptRSA(string publickey, string content)
        {
            //string publickey1;
            //using (StreamReader reader = new StreamReader("PublicKey.xml"))
            //{
            //    publickey1 = reader.ReadLine();
            //}
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publickey);
            var cipherBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherBytes);

        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string DecryptRSA(string privatekey, string content)
        {
            string privatekey1;
            using (StreamReader reader = new StreamReader("Privatekey.xml"))
            {
                privatekey1 = reader.ReadLine();
            }
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privatekey1);
            var cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }
        #endregion

        #region DES解密
        /// <summary>  
        /// C# DES解密方法  
        /// </summary>  
        /// <param name="encryptedValue">待解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>解密后的字符串</returns>  
        public static string DESDecrypt(string encryptedValue)
        {
            string key = "12344321";
           // string iv = "88888888";
            using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider{ Key = Encoding.UTF8.GetBytes(key)})
            {
                using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(encryptedValue);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
        #endregion

        #region DES加密
        /// <summary>  
        /// C# DES加密方法  
        /// </summary>  
        /// <param name="encryptedValue">要加密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>加密后的字符串</returns>  
        //public static string DESEncrypt(string originalValue, string key, string iv)
        //{
        //    using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
        //    {
        //        using (ICryptoTransform ct = sa.CreateEncryptor())
        //        {
        //            byte[] by = Encoding.UTF8.GetBytes(originalValue);
        //            using (var ms = new MemoryStream())
        //            {
        //                using (var cs = new CryptoStream(ms, ct,
        //                CryptoStreamMode.Write))
        //                {
        //                    cs.Write(by, 0, by.Length);
        //                    cs.FlushFinalBlock();
        //                }
        //                return Convert.ToBase64String(ms.ToArray());
        //            }
        //        }
        //    }
        //}
        public static string DESEncrypt(string originalValue)
        {
            string key = "12344321";
            //   string iv = "88888888";
            //using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(key) })
            using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key) })
            {
                using (ICryptoTransform ct = sa.CreateEncryptor())
                {
                    byte[] by = Encoding.UTF8.GetBytes(originalValue);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct,
                        CryptoStreamMode.Write))
                        {
                            cs.Write(by, 0, by.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        #endregion

        #region 自写加密
        public static  string GetEnPassword(string str)
        {
            string a= str.TrimStart('0');
            int b = Convert.ToInt32(a);
            int c = b*3-1;
            string  d = c.ToString();
            string dd = d.PadLeft(10, '0');
            return dd;
        }
        #endregion

        #region 自写解密
        public static string GetDePassword(string str)
        {
            string a = str.TrimStart('0');
            int b = Convert.ToInt32(a)+1;
            int c = b / 3;
            string d = c.ToString();
            string dd = d.PadLeft(10,'0');
            return dd;
        }
        #endregion

        #region 环信账号添加前缀
        public static string EMNickName(string NickName)
        {
            return "Buzzly" + NickName;
        }
        public static string EMPassword()
        {
            return "WAJB357";
        }
        public static string EMRegisterId(string RegisterId)
        {
            return "Buzzly" + RegisterId;
        }
        #endregion

        #region 极光推送 根据注册Id别名传
        public static string PushMsgByAliasId(string strMsg, string [] aliasIds,string  deviceId)
        {
            try
            {
              //  JPushClient client = new JPushClient(app_key, master_secret);
                DeviceClient client = new DeviceClient(JPUSH_APP_KEY, JPUSH_MASTER_SECRET);
                //var dd=  client.addDeviceAlias(deviceId, aliasIds[0]);   //添加标识一个设备添加一个注册Id
                JPushClient clientSend = new JPushClient(JPUSH_APP_KEY, JPUSH_MASTER_SECRET);

                string[] registrations = { "170976fa8ab5fc3cda2" };  //根据设备标识
                PushPayload payload = PushObject_registration_Alert(strMsg, registrations);
                var result = clientSend.SendPush(payload);

                //string[] registrations = { "170976fa8ab5fc3cda2" };  //根据设备标识
                //PushPayload payload = PushObject_registration_Alert("aa", registrations);

                //var result = client.SendPush(payload);
                return "0";
            }
            catch(Exception e)
            {
                return "1"+e;
            }
        }

        public static string PushMsgReg(string strMsg, string RegisterId)
        {
            try
            {
                //DeviceClient client = new DeviceClient(app_key, master_secret);
                JPushClient clientSend = new JPushClient(JPUSH_APP_KEY, JPUSH_MASTER_SECRET);

                //var d = client.addDeviceAlias(DeviceRegId, RegisterId);  //给一个设备添加一个标识
                PushPayload payload = PushObject_all_alia_alert(strMsg, RegisterId);
                var result = clientSend.SendPush(payload);
                return "0";
            }
            catch (Exception e)
            {
                return "1" + e;
            }
        }

        public static string PushMsg(string strMsg, string DeviceRegId,string RegisterId)
        {
            try
            {
                DeviceClient client = new DeviceClient(JPUSH_APP_KEY, JPUSH_MASTER_SECRET);
                JPushClient clientSend = new JPushClient(JPUSH_APP_KEY, JPUSH_MASTER_SECRET);

                var d = client.addDeviceAlias(DeviceRegId, RegisterId);  //给一个设备添加一个标识
                PushPayload payload = PushObject_all_alia_alert(strMsg, RegisterId);
                var result = clientSend.SendPush(payload);
                return "0";
            }
            catch (Exception e)
            {
                return "1" + e;
            }
        }
        #endregion

        #region 根据别名推送
        public static PushPayload PushObject_all_alia_alert(string strMsg,string alias)
        {

            PushPayload pushPayload_alias = new PushPayload();
            pushPayload_alias.platform = Platform.android();
            pushPayload_alias.audience = Audience.s_alias(alias);
            pushPayload_alias.notification = new Notification().setAlert(strMsg);
            return pushPayload_alias;
        }
        #endregion

        #region 给
        //public DefaultResult addDeviceAlias(String registrationId, String alias)
        //{
        //    String mobile = null;
        //    HashSet<String> tagsToAdd = null;
        //    HashSet<String> tagsToRemove = null;
        //    return updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        //}
        #endregion

        #region 推送给多个别名：
        public static PushPayload PushObject_alias_Alert(string strPush, string[] aliasId)
        {

           


            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();   //推送平台
            pushPayload.audience = Audience.s_tag(aliasId);
            pushPayload.notification = new Notification().setAlert(strPush);
            return pushPayload;
        }
        #endregion

        public static PushPayload PushObject_registration_Alert(string strPush, string[] registrationId)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();   //推送平台
            pushPayload.audience = Audience.s_registrationId(registrationId);
            pushPayload.notification = new Notification().setAlert(strPush);
            return pushPayload;
        }


        //public bool IsOk(DateTime datetime)
        //{
        //    var data = DateTime.Now.Date.AddDays(-1);
        //    if (datetime.Date == data)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //   // var data = datetime.Date.AddDays(-1);
        //}
    }
}