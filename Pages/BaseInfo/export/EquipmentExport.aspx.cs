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
public partial class Pages_BaseInfo_EquipmentExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string equipcode = Request.QueryString["equipcode"];
        string equipname = Request.QueryString["equipname"];
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WsSystem ws = new WsSystem();
        IList<BasEquipment> objods = _bal.FindBasEquipment(equipcode, equipname);//.FindFailItems(failcode,failtype);
        if (objods == null || objods.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("EquipmentQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "设备编号,国别厂家,设备名称,机床类型,轴数,型号,功率,车间位置,状态,出厂编号,启用日期,更新人,时间";
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
            cell.SetCellValue(objods[i - 1].COMPANY);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 1].MachineName);
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 1].MachineType);
            cell = row.CreateCell(4);           
            cell.SetCellValue(objods[i - 1].AxisNumber);
            cell = row.CreateCell(5);
            cell.SetCellValue(objods[i - 1].MODEL);
            cell = row.CreateCell(6);
            cell.SetCellValue(objods[i - 1].POWER);
            cell = row.CreateCell(7);
            cell.SetCellValue(objods[i - 1].LOCATION);
            cell = row.CreateCell(8);
            cell.SetCellValue(objods[i - 1].STATUS);
            cell = row.CreateCell(9);
            cell.SetCellValue(objods[i - 1].OutCode);
            cell = row.CreateCell(10);
            cell.SetCellValue(objods[i - 1].UseDate.ToString());
            cell = row.CreateCell(11);
            cell.SetCellValue(ws.FindUserNameByCode(objods[i - 1].UpdatedBy));
            cell = row.CreateCell(12);
            cell.SetCellValue(objods[i - 1].CreatedDate.ToString());
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "EquipmentQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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