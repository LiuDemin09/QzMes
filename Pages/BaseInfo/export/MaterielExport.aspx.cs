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
public partial class Pages_BaseInfo_MaterielExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string qmateriel = Request.QueryString["qmateriel"];
        string cmateriel = Request.QueryString["cmateriel"];
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WsSystem ws = new WsSystem();
        IList<BasMateriel> objods = _bal.FindBasMateriel(cmateriel, qmateriel, "");//.FindFailItems(failcode,failtype);
        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("MaterielQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "钦纵料号,客户料号,料号名称,基板数,客户名称,备注,操作人,时间";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        for (int i = 1; i <  objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 1].QPARTNO);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i - 1].CPARTNO);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 1].NAME);
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 1].BasQty.ToString());
            cell = row.CreateCell(4);           
            cell.SetCellValue(objods[i - 1].CUSTOMER);
            cell = row.CreateCell(5);
            cell.SetCellValue(objods[i - 1].MEMO);             
            cell = row.CreateCell(6);
            cell.SetCellValue(ws.FindUserNameByCode(objods[i - 1].UpdatedBy));
            cell = row.CreateCell(7);
            cell.SetCellValue(objods[i - 1].CreatedDate.ToString());
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "MaterielQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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