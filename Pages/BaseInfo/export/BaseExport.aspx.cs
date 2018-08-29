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
public partial class Pages_BaseInfo_BaseExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        string startTime = Request.QueryString["startTime"];
        string endTime = Request.QueryString["endTime"];
        string baseCode = Request.QueryString["baseName"];
        
        
        BasicBO _bal = BLLFactory.GetBal<BasicBO>(userInfo);
        IList<BasBase> baseInfo = _bal.FindBaseByCode(startTime,endTime,baseCode);
        WsSystem ws = new WsSystem();
        if (baseInfo == null || baseInfo.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("BaseInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "信息编号,信息名称,子信息编码,子信息名称,操作人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        if (baseInfo != null)
        {
            for (int i = 2; i <= baseInfo.Count + 1; i++)
            {
                row = hssfSheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.SetCellValue(baseInfo[i - 2].CODE);
                cell = row.CreateCell(1);
                cell.SetCellValue(baseInfo[i - 2].NAME);
                cell = row.CreateCell(2);
                cell.SetCellValue(baseInfo[i - 2].SubCode);
                cell = row.CreateCell(3);
                cell.SetCellValue(baseInfo[i - 2].SubName);
                cell = row.CreateCell(4);
                cell.SetCellValue(ws.FindUserNameByCode(baseInfo[i - 2].UpdatedBy));               
                cell = row.CreateCell(5);
                cell.SetCellValue(baseInfo[i - 2].UpdatedDate == null ? baseInfo[i - 2].CreatedDate.ToString() : baseInfo[i - 2].UpdatedDate.ToString());

            }
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