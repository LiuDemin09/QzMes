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
public partial class Pages_WareHouseManage_CartonExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sn = Request.QueryString["sn"];
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
        IList<CartonInfo> objods = _bal.FindCartonInfo(sn,"","", dtstart, dtend);
        WsSystem ws = new WsSystem();
        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("CartonInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "箱号,订单号码,零件图号,质量编号,数量,操作人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        for (int i = 1; i < objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 1].CSN);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i -1].OrderNumber);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 1].PartsdrawingCode);
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 1].QualityCode);
            cell = row.CreateCell(4);
            cell.SetCellValue(objods[i - 1].QUANTITY.ToString());
            cell = row.CreateCell(5);
            cell.SetCellValue(ws.FindUserNameByCode(objods[i - 1].UpdatedBy));
            cell = row.CreateCell(6);
            cell.SetCellValue(objods[i - 1].CreatedDate.ToString());
             
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "CartonInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
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