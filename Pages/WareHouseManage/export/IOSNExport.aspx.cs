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
using BLL;
public partial class Pages_WareHouseManage_IOSNExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string materialname = Request.QueryString["materialname"];
        string custname = Request.QueryString["custname"];
        string batchnumber = Request.QueryString["batchnumber"];
        string starttime = Request.QueryString["starttime"];
        string endtime = Request.QueryString["endtime"];
        DateTime dtstart = DateTime.Today.AddDays(-7);
        DateTime dtend = DateTime.Now;
        if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
        {
            dtstart = Convert.ToDateTime(starttime);
            dtend = Convert.ToDateTime(endtime);
        }

        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        IList<MaterialStock> objods = _bal.FindStockInfo("0^2", materialname, custname, starttime, endtime, batchnumber);

        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("MaterialInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "来料条码,物料代码,物料名称,批号,数量,单据编号,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        for (int i = 1; i <  objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 1].MSN);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i - 1].MaterialCode);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 1].MaterialName);
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 1].BatchNumber);
            cell = row.CreateCell(4);
            cell.SetCellValue(objods[i - 1].BasQty.ToString());
            cell = row.CreateCell(5);
            cell.SetCellValue(objods[i - 1].DOCUMENTID);
            cell = row.CreateCell(6);
            cell.SetCellValue(objods[i - 1].CreatedDate.ToString());
            
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "MaterialInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
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