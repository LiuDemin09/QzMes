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
public partial class Pages_QualityManage_PassRateQueryExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string month = Request.QueryString["month"];
        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        IList<RealtimeStatistics> machines = _bal.findMachineCode();
        List<YieldInfo> bs = new List<YieldInfo>();

        for (int i = 0; i < machines.Count; i++)
        {

            YieldInfo bbtemp = new YieldInfo();
            bbtemp.WO = machines[i].MachineName;


            int[] fails = _bal.FindMachineYield(machines[i].MachineName,month);
            bbtemp.QUANTITY = (fails[0] + fails[1]);
            bbtemp.PassCount = fails[0];
            bbtemp.FailCount = fails[1];
            bbtemp.ReturnCount = fails[2];
            bbtemp.SecondPass = fails[3];
            bbtemp.DiscardCount = fails[4];
            bbtemp.PassRate = (Math.Round((double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.FailRate = (Math.Round(100 - (double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.ReturnRate = (Math.Round((double)(bbtemp.ReturnCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.SecPassRate = (Math.Round((double)(bbtemp.SecondPass * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.DiscardRate = (Math.Round((double)(bbtemp.DiscardCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bs.Add(bbtemp);

        }


        if (bs == null || bs.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("PassRateQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objhead = "机床名称,产品数量,一类品数量,二类品数量,返工数量,让步数量,废品数量,一类品率,二类品率,返工率,让步率,废品率";
        for (int i = 0; i < objhead.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objhead.Split(',')[i]);
        }
        for (int j = 0; j < bs.Count; j++)
        {
            row = hssfSheet.CreateRow(j+1);
            cell = row.CreateCell(0);
            cell.SetCellValue(bs[j].WO);
            cell = row.CreateCell(1);
            cell.SetCellValue(bs[j].QUANTITY);
            cell = row.CreateCell(2);
            cell.SetCellValue(bs[j].PassCount);
            cell = row.CreateCell(3);
            cell.SetCellValue(bs[j].FailCount);
            cell = row.CreateCell(4);
            cell.SetCellValue(bs[j].ReturnCount);
            cell = row.CreateCell(5);
            cell.SetCellValue(bs[j].SecondPass);
            cell = row.CreateCell(6);
            cell.SetCellValue(bs[j].DiscardCount);
            cell = row.CreateCell(7);
            cell.SetCellValue(bs[j].PassRate);
            cell = row.CreateCell(8);
            cell.SetCellValue(bs[j].FailRate);
            cell = row.CreateCell(9);
            cell.SetCellValue(bs[j].ReturnRate);
            cell = row.CreateCell(10);
            cell.SetCellValue(bs[j].SecPassRate);
            cell = row.CreateCell(11);
            cell.SetCellValue(bs[j].DiscardRate);

        }

        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "PassRateQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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