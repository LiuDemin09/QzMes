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
public partial class Pages_WareHouseManage_ProductExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string workorder = Request.QueryString["workorder"];
        string status = Request.QueryString["status"];
        string productname = Request.QueryString["productname"];
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

        WareHouseBO _bal = BLLFactory.GetBal<WareHouseBO>(userInfo);
        WsSystem ws = new WsSystem();
        IList<ProductStock> objstock = null;
        IList<ProductStockHistory> objhistory = null;
        if (status=="2")
        {
            objstock = _bal.FindProductStockInfo(workorder, productname, batchnumber, starttime, endtime);
        }
        else
        {
            if(status=="3")
            {
                status = "";
            }
            objhistory = _bal.FindProductStockHistory(status, workorder, productname, batchnumber, starttime, endtime);
        }
        if (objstock == null && objhistory.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("ProductInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "产品条码,工单号码,交货单位,单据编号,收货仓库,产品代码,产品名称,单位,数量,批号,保管员,移交人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        if (objstock != null)
        {
            for (int i = 1; i <  objstock.Count + 1; i++)
            {
                row = hssfSheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.SetCellValue(objstock[i - 1].PSN);
                cell = row.CreateCell(1);
                cell.SetCellValue(objstock[i - 1].WorkOrder);
                cell = row.CreateCell(2);
                cell.SetCellValue(objstock[i - 1].MANUFACTURE);
                cell = row.CreateCell(3);
                cell.SetCellValue(objstock[i - 1].DOCUMENTID);
                cell = row.CreateCell(4);
                cell.SetCellValue(objstock[i - 1].StockHouse);
                cell = row.CreateCell(5);
                cell.SetCellValue(objstock[i - 1].ProductCode);
                cell = row.CreateCell(6);
                cell.SetCellValue(objstock[i - 1].ProductName);                
                cell = row.CreateCell(7);
                cell.SetCellValue(objstock[i - 1].UNIT);
                cell = row.CreateCell(8);
                cell.SetCellValue(objstock[i - 1].QUANTITY.ToString());
                cell = row.CreateCell(9);
                cell.SetCellValue(objstock[i - 1].BatchNumber);
                cell = row.CreateCell(10);
                cell.SetCellValue(ws.FindUserNameByCode(objstock[i - 1].UpdatedBy));
                cell = row.CreateCell(11);
                cell.SetCellValue(ws.FindUserNameByCode(objstock[i - 1].FromBy));
                cell = row.CreateCell(12);
                cell.SetCellValue(objstock[i - 1].UpdatedDate == null ? objstock[i - 1].CreatedDate.ToString() : objstock[i - 1].UpdatedDate.ToString());

            }
        }
        else
        {
            for (int i = 1; i < objhistory.Count + 1; i++)
            {
               row = hssfSheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.SetCellValue(objhistory[i - 1].PSN);
                cell = row.CreateCell(1);
                cell.SetCellValue(objhistory[i - 1].WorkOrder);
                cell = row.CreateCell(2);
                cell.SetCellValue(objhistory[i - 1].MANUFACTURE);
                cell = row.CreateCell(3);
                cell.SetCellValue(objhistory[i - 1].DOCUMENTID);
                cell = row.CreateCell(4);
                cell.SetCellValue(objhistory[i - 1].StockHouse);
                cell = row.CreateCell(5);
                cell.SetCellValue(objhistory[i - 1].ProductCode);
                cell = row.CreateCell(6);
                cell.SetCellValue(objhistory[i - 1].ProductName);               
                cell = row.CreateCell(7);
                cell.SetCellValue(objhistory[i - 1].UNIT);
                cell = row.CreateCell(8);
                cell.SetCellValue(objhistory[i - 1].QUANTITY.ToString());
                cell = row.CreateCell(9);
                cell.SetCellValue(objhistory[i - 1].BatchNumber);
                cell = row.CreateCell(10);
                cell.SetCellValue(ws.FindUserNameByCode(objhistory[i - 1].UpdatedBy));
                cell = row.CreateCell(11);
                cell.SetCellValue(ws.FindUserNameByCode(objhistory[i - 1].FromBy));
                cell = row.CreateCell(12);
                cell.SetCellValue(objhistory[i - 1].UpdatedDate == null ? objhistory[i - 1].CreatedDate.ToString() : objhistory[i - 1].UpdatedDate.ToString());

            }
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "ProductInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
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