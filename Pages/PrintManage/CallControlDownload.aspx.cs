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
public partial class Pages_PrintManage_CallControlDownload : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        downloadfile("SetupNewFuseControls.msi");
    }

    protected void downloadfile(string strname)
    {
        string strfile = string.Empty;
        //string FilePath = System.Environment.CurrentDirectory + "\\DownLoad";
        string FilePath = HttpContext.Current.Request.PhysicalApplicationPath + "\\Download\\";
        //pubMisc.ShowMessage(this.Page, FilePath);
        //return;
        FileInfo fileInfo = new FileInfo(FilePath + strname);
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