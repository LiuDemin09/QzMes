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
public partial class Pages_BaseInfo_CustExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string startTime = Request.QueryString["startTime"];
        string endTime = Request.QueryString["endTime"];
        string custCode = Request.QueryString["custCode"];
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WsSystem ws = new WsSystem();
        IList<BasCustom> objods = _bal.FindBasCustom(custCode, "");//.FindBasCode("", codename);//.FindFailItems(failcode,failtype);
        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("CustQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "客户代码,客户名称,地址,运输方式,收货地点,联系人,电话,传真,开票名称,操作人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        for (int i = 1; i <  objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 1].CODE);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i - 1].NAME);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 1].ADDRESS);
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 1].TransType);
            cell = row.CreateCell(4);           
            cell.SetCellValue(objods[i - 1].ReceiveArea);
            cell = row.CreateCell(5);
            cell.SetCellValue(objods[i - 1].CONTACT);
            cell = row.CreateCell(6);
            cell.SetCellValue(objods[i - 1].MOBILE);
            cell = row.CreateCell(7);
            cell.SetCellValue(objods[i - 1].FAX);
            cell = row.CreateCell(8);
            cell.SetCellValue(objods[i - 1].InvoiceName);
            cell = row.CreateCell(9);             
            cell.SetCellValue(ws.FindUserNameByCode(objods[i - 1].UpdatedBy));
            cell = row.CreateCell(10);
            cell.SetCellValue(objods[i - 1].CreatedDate.ToString());             
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "CustQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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