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
public partial class Pages_BaseInfo_SequenceExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string squenceName = Request.QueryString["squenceName"];
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WsSystem ws = new WsSystem();
        IList<BasSequence> objods = _bal.FindBasSequence(squenceName);
        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("SquenceQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "序列号名称,产品系列,数字长度,数字类型,增长模式,操作人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        for (int i = 1; i <  objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 1].SeqName);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i - 1].FAMILY);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 1].DigitalLen.ToString());
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 1].DigitalTypeMemo);
            cell = row.CreateCell(4);           
            cell.SetCellValue(objods[i - 1].IncreaseModeMemo);
            cell = row.CreateCell(5);            
            cell.SetCellValue(ws.FindUserNameByCode(objods[i - 1].UpdatedBy));
            cell = row.CreateCell(6);
            cell.SetCellValue(objods[i - 1].CreatedDate.ToString());             
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "SquenceQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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