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
public partial class Pages_BaseInfo_BasInfoDownload : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("BaseInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "信息编号,信息名称,子信息编号,子信息名称,备注";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "BaseInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xls", fileName));
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Response.BinaryWrite(file.GetBuffer());
        Response.Flush();
        Response.End();
    }
}