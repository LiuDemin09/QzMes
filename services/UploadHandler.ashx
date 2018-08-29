<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
public class UploadHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
           
            //http://www.cnblogs.com/babycool/
            //接收上传后的文件
            HttpPostedFile file = context.Request.Files["Filedata"];
            //其他参数
            //string somekey = context.Request["someKey"];
            //string other = context.Request["someOtherKey"];
            //获取文件的保存路径
            string uploadPath =
                HttpContext.Current.Server.MapPath("..\\Uploads\\materialfile\\");
            //判断上传的文件是否为空
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                //保存文件
                int start = file.FileName.LastIndexOf(".");
                string strFileName = DateTime.Now.ToFileTimeUtc().ToString() + file.FileName.Substring(start);
                HttpCookie cookie = new HttpCookie("CurFilePath");
                cookie.Value = "..\\..\\Uploads\\materialfile\\" + strFileName;
                //cookie.Domain = ".sosuo8.com"; 
                HttpContext.Current.Response.Cookies.Add(cookie); 
                file.SaveAs(uploadPath + strFileName);
                context.Response.Write(cookie.Value);
            }
            else
            {
                context.Response.Write("0");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    } 
    public bool IsReusable {
        get {
            return false;
        }
    } 
}