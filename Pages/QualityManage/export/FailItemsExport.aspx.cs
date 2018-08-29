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
public partial class Pages_QualityManage_FailItemsExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string failcode = Request.QueryString["failcode"];
        string failtype = Request.QueryString["failtype"];
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WsSystem ws = new WsSystem();
        if (failtype=="-1")
        {
            failtype = "";
        }
        IList<FailItems> objods = _bal.FindFailItems(failcode,failtype);
        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("FailItemsQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "不良项,不良类型,不良描述,创建人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        for (int i = 1; i <  objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 1].FailCode);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i - 1].FailType);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 1].FailMemo);
            cell = row.CreateCell(3);
            cell.SetCellValue(ws.FindUserNameByCode(objods[i - 1].UpdatedBy));
            cell = row.CreateCell(4);           
            cell.SetCellValue(objods[i - 1].CreatedDate.ToString());
           
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "FailItemsQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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