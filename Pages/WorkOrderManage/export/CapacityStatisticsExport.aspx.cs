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
public partial class Pages_WorkOrderManage_CapacityStatisticsExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string workorder = Request.QueryString["workorder"];
        string status = Request.QueryString["status"];
        string partsdrawingno = Request.QueryString["partsdrawingno"];
        string order = Request.QueryString["order"];
        string starttime = Request.QueryString["starttime"];
        string endtime = Request.QueryString["endtime"];
        DateTime dtstart = DateTime.Today.AddDays(-100);
        DateTime dtend = DateTime.Now;
        if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
        {
            dtstart = Convert.ToDateTime(starttime);
            dtend = Convert.ToDateTime(endtime);
        }
        WorkOrder wo = new WorkOrder();
        wo.WO = workorder;
        wo.STATUS = status=="4"?"":status;
        wo.PartsdrawingCode = partsdrawingno;
        wo.OrderNumber = order;
        wo.StartTime = dtstart;
        wo.EndTime = dtend;
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        IList<WorkOrder> woobjs = _bal.FindWorkOrderInfo(wo);
        WsSystem ws = new WsSystem();
        if (woobjs == null || woobjs.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("CapacityStatistics");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "工单单号,订单单号,零件图号,工单状态,机床类型,机床名称,负责人员,产品名称,计划开始,计划结束,批次,计划数量,生产数量,计划检验,计划入库";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        if (woobjs != null)
        {
            for (int i = 2; i <= woobjs.Count + 1; i++)
            {
                row = hssfSheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.SetCellValue(woobjs[i - 2].WO);
                cell = row.CreateCell(1);
                cell.SetCellValue(woobjs[i - 2].OrderNumber);
                cell = row.CreateCell(2);
                cell.SetCellValue(woobjs[i - 2].PartsdrawingCode);
                cell = row.CreateCell(3);
                cell.SetCellValue(woobjs[i - 2].MEMO);
                cell = row.CreateCell(4);
                cell.SetCellValue(woobjs[i - 2].MachineType);
                cell = row.CreateCell(5);
                cell.SetCellValue(woobjs[i - 2].MachineName);
                cell = row.CreateCell(6);
                cell.SetCellValue(woobjs[i - 2].WorkerName);
                cell = row.CreateCell(7);
                cell.SetCellValue(woobjs[i - 2].ProductName);
                cell = row.CreateCell(8);
                cell.SetCellValue(woobjs[i - 2].StartTime.ToString());
                cell = row.CreateCell(9);
                cell.SetCellValue(woobjs[i - 2].EndTime.ToString());
                cell = row.CreateCell(10);
                cell.SetCellValue(woobjs[i - 2].BatchNumber);
                cell = row.CreateCell(11);
                cell.SetCellValue(woobjs[i - 2].PlanQuantity.ToString());
                cell = row.CreateCell(12);
                cell.SetCellValue(woobjs[i - 2].QUANTITY.ToString());
                cell = row.CreateCell(13);
                cell.SetCellValue(woobjs[i - 2].CheckTime.ToString());
                cell = row.CreateCell(14);
                cell.SetCellValue(woobjs[i - 2].InTime.ToString());
                cell = row.CreateCell(15);
                cell.SetCellValue(ws.FindUserNameByCode(woobjs[i - 2].UpdatedBy));
                cell = row.CreateCell(16);
                cell.SetCellValue(woobjs[i - 2].UpdatedDate == null ? woobjs[i - 2].CreatedDate.ToString() : woobjs[i - 2].UpdatedDate.ToString());

            }
        }
         
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "CapacityStatistics" + DateTime.Now.ToString("yyyyMMddHHmmss");
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