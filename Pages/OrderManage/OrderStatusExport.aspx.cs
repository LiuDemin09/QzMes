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
public partial class Pages_OrderManage_OrderStatusExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string orderno = Request.QueryString["orderno"];
        string partdrawingno = Request.QueryString["partdrawingno"];         
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        IList<OrderDetail> objods = _bal.FindOrderInfo(orderno, partdrawingno);
        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("OrderStatusInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "订单单号,零件图号,订单状态,客户名称,产品名称,订单数量,备注,交付时间,创建人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        string strstatus = string.Empty;
        for (int i = 2; i <= objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 2].OrderNo);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i - 2].PartsdrawingCode);
            cell = row.CreateCell(2);
            switch (objods[i - 2].STATUS)
            {
                case "0":
                    strstatus = "创建";
                    break;
                case "1":
                    strstatus = "发布";
                    break;
                case "2":
                    strstatus = "发货通知";
                    break;
                case "3":
                    strstatus = "关闭";
                    break;
                default:
                    strstatus = "状态异常";
                    break;
            }
            cell.SetCellValue(strstatus);
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 2].CustName);
            cell = row.CreateCell(4);
            cell.SetCellValue(objods[i - 2].ProductName);
            cell = row.CreateCell(5);
            cell.SetCellValue(objods[i - 2].OrderQuantity.ToString());
            cell = row.CreateCell(6);
            cell.SetCellValue(objods[i - 2].BatchNumber);
            cell = row.CreateCell(7);
            cell.SetCellValue(objods[i - 2].OutDate.ToString());
            cell = row.CreateCell(8);
            cell.SetCellValue(objods[i - 2].UpdatedBy);
            cell = row.CreateCell(9);
            cell.SetCellValue(objods[i - 2].UpdatedDate.ToString());
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "OrderStatusInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
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