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
public partial class Pages_QualityManage_CustPassRateExport : BasePage
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
        IList<BasCustom> cus = _bal.FindCustName();
        if (cus == null || cus.Count == 0)
        {
            Response.Write("no data");
            return;
        }


        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("CustPassRate");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objhead = "委托单位,产品数量,一类品数量,二类品数量,返工数量,让步数量,废品数量,一类品率,二类品率,返工率,让步率,废品率";
        for (int i = 0; i < objhead.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objhead.Split(',')[i]);            
        }
        for (int i = 1; i < cus.Count + 1; i++)
        {
            int[] allRate = _bal.FindCustQty(month, cus[i-1].NAME);
            int QUANTITY = (allRate[0] + allRate[1]);
            int PassCount = allRate[0];
            int FailCount = allRate[1];
            int ReturnCount = allRate[2];
            int SecondPass = allRate[3];
            int DiscardCount = allRate[4];
            double PassRate = QUANTITY == 0 ? 0 : (Math.Round((double)(PassCount * 100 / QUANTITY), 2));
            double FailRate = QUANTITY == 0 ? 0 : (Math.Round(100 - (double)(PassCount * 100 / QUANTITY), 2));
            double ReturnRate = QUANTITY == 0 ? 0 : (Math.Round((double)(ReturnCount * 100 / QUANTITY), 2));
            double SecPassRate = QUANTITY == 0 ? 0 : (Math.Round((double)(SecondPass * 100 / QUANTITY), 2));
            double DiscardRate = QUANTITY == 0 ? 0 : (Math.Round((double)(DiscardCount * 100 / QUANTITY), 2));
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(cus[i-1].NAME);
            cell = row.CreateCell(1);
            cell.SetCellValue(QUANTITY);
            cell = row.CreateCell(2);
            cell.SetCellValue(PassCount);
            cell = row.CreateCell(3);
            cell.SetCellValue(FailCount);
            cell = row.CreateCell(4);
            cell.SetCellValue(ReturnCount);
            cell = row.CreateCell(5);
            cell.SetCellValue(SecondPass);
            cell = row.CreateCell(6);
            cell.SetCellValue(DiscardCount);
            cell = row.CreateCell(7);
            cell.SetCellValue(PassRate + "%");
            cell = row.CreateCell(8);
            cell.SetCellValue(FailRate + "%");
            cell = row.CreateCell(9);
            cell.SetCellValue(ReturnRate + "%");
            cell = row.CreateCell(10);
            cell.SetCellValue(SecPassRate + "%");
            cell = row.CreateCell(11);
            cell.SetCellValue(DiscardRate + "%");

        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "CustPassRate" + DateTime.Now.ToString("yyyyMMddHHmmss");
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