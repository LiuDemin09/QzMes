using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using NPOI;
using NPOI.HSSF;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using DAL;
public partial class Pages_PrintManage_TemplateDownload : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string TemplateType = Request.QueryString["TemplateType"];
         
        WsSystem ws = new WsSystem();
        PrintSet obj = ws.QueryPrintSetPath(TemplateType);
        if (obj != null)
        {
            string name = string.Empty;
            if(obj.MEMO== "箱号模板")
            {
                name = "csn.lab";
            }
            if (obj.MEMO == "条码模板")
            {
                name = "psn.lab";
            }
            else
            {
                name = "msn.lab";
            }
            downloadfile(obj.TemplatePath,name);
        }
    }

    protected void downloadfile(string path,string strname)
    {
        string strfile = string.Empty;
        //string FilePath = System.Environment.CurrentDirectory + "\\DownLoad";
        string FilePath = WebHelper.GetAttachmentTempPath() + "\\" + path;
       // string FilePath = path;// HttpContext.Current.Request.PhysicalApplicationPath + "\\Download\\";
        //pubMisc.ShowMessage(this.Page, FilePath);
        //return;
        FileInfo fileInfo = new FileInfo(FilePath);
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + strname);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Response.WriteFile(fileInfo.FullName);
        Response.Flush();
        Response.End(); 
    }
}