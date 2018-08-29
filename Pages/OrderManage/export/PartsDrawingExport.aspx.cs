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
public partial class Pages_OrderManage_PartsDrawingExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        string partsdrawingno = Request.QueryString["partsdrawingno"];
        string custcode = Request.QueryString["custcode"];
        string starttime = Request.QueryString["starttime"];
        string endtime = Request.QueryString["endtime"];
        DateTime dtstart = DateTime.Today.AddDays(-100);
        DateTime dtend = DateTime.Now;
        if (custcode == "-1")
        {
            custcode = "";
        }
        if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
        {
            dtstart = Convert.ToDateTime(starttime);
            dtend = Convert.ToDateTime(endtime);
        }
        
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WsSystem ws = new WsSystem();
        IList<PartsdrawingCode> woobjs = _bal.FindPartsdrawingInfo(partsdrawingno, custcode, starttime, endtime);

        if (woobjs == null || woobjs.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("PartsdrawingInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "零件图号,客户名称,客户代码,产品名称,创建人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        if (woobjs != null)
        {
            for (int i = 2; i <= woobjs.Count + 1; i++)
            {
                row = hssfSheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.SetCellValue(woobjs[i - 2].PartsCode);
                cell = row.CreateCell(1);
                cell.SetCellValue(woobjs[i - 2].CustName);
                cell = row.CreateCell(2);
                cell.SetCellValue(woobjs[i - 2].CustCode);
                cell = row.CreateCell(3);
                cell.SetCellValue(woobjs[i - 2].ProductName);
                cell = row.CreateCell(4);  
                cell.SetCellValue(ws.FindUserNameByCode(woobjs[i - 2].UpdatedBy));               
                cell = row.CreateCell(5);
                cell.SetCellValue(woobjs[i - 2].UpdatedDate == null ? woobjs[i - 2].CreatedDate.ToString() : woobjs[i - 2].UpdatedDate.ToString());

            }
        }
         
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "PartsdrawingInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
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