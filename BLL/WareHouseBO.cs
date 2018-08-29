using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freeworks.Common;
using Freeworks.Common.Cryptography;
using Freeworks.ORM.Core;
using System.IO;
using DAL;
using System.Collections;
using Freeworks.Common.Paging;

namespace BLL
{
    public class WareHouseBO : BOBase
    {
        string tplPath = "";
        public WareHouseBO(UserInfo userInfo)
           : base(userInfo)
        {
            //
            // TODO: Add constructor logic here
            //
            //打印模版存放路徑
            tplPath = ConstantsHelper.GetHelper(this.UserSite, this.UserBU).S_LABEL_TPL_PATH + "\\" + this.UserSite + this.UserBU;
            if (!Directory.Exists(tplPath))
            {
                Directory.CreateDirectory(tplPath);
            }
        }

        public string SaveProductInWH(string transferEmp, string delivery, string psn)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                if (string.IsNullOrEmpty(transferEmp) || string.IsNullOrEmpty(psn))
                {
                    return "errorNull";
                    //异常

                }
                IList<UnsurenessProduct> up = DBContext.FindArray<UnsurenessProduct>(trans, UnsurenessProduct.Meta.PSN == psn.Trim() & UnsurenessProduct.Meta.STATUS != "4");
                if (up != null && up.Count > 0)
                {
                    // throw new Exception("该产品属待处理品，未审核通过");
                    return "该产品属待处理品，未审核通过！";//by tony modify 2017-5-22
                }
                //IList<UnsurenessProduct> up2 = DBContext.FindArray<UnsurenessProduct>(trans, UnsurenessProduct.Meta.PSN == psn.Trim() & UnsurenessProduct.Meta.STATUS == "4");
                //IList<TrackingHistory> tHis = null;
                //if (up2 != null && up2.Count > 0)//待处理品已经审核通过
                //{
                //    tHis = DBContext.FindArray<TrackingHistory>(trans, TrackingHistory.Meta.PSN == psn.Trim() & TrackingHistory.Meta.MEMO == "报废");
                //    if (tHis == null || tHis.Count == 0)

                //    {
                //        return "非报废待处理品，不允许入库！";
                //    }
                //}
                TrackingWip wip = DBContext.Find<TrackingWip>(trans, TrackingWip.Meta.PSN == psn.Trim());
                if (wip == null || string.IsNullOrEmpty(wip.WorkOrder) || string.IsNullOrEmpty(wip.MSN))
                {
                    //throw new Exception("该产品未排入生产计划内生产");
                    return "该产品未排入生产计划内生产！";//异常//by tony modify 2017-5-22

                }
                if (string.IsNullOrEmpty(wip.NextStation) || wip.NextStation.Trim() != "INSTOCK")
                {
                    // throw new Exception("该产品未做终检或终检不合格，禁止入库");
                    return "该产品未做终检或终检不合格，禁止入库";
                }
                //if (wip.NextStation.Trim() != "INSTOCK")
                //{
                //    // throw new Exception("该产品未做终检或终检不合格，禁止入库");
                //    return "该产品未做终检或终检不合格，禁止入库";
                //}

                //by tony modify 2017-5-22
                //if (!string.IsNullOrEmpty(wip.NextStation) && !wip.NextStation.Trim().Equals("INSTOCK"))
                //{
                //    return "errorStation";
                //}
                //确定是报废入库还是良品入库
                //if (up2 != null && up2.Count > 0 && tHis != null && tHis.Count > 0)
                if (wip.STATUS=="F")
                {
                    wip.NextStation = "FINISHED";
                    wip.NextStationId = "FINISHED";
                    wip.StationName = "INSCRAP";
                    wip.StationId = "INSCRAP";
                }
                else
                {
                    wip.NextStation = "OUTSTOCK";
                    wip.NextStationId = "OUTSTOCK";
                    wip.StationName = "INSTOCK";
                    wip.StationId = "INSTOCK";
                }

                wip.InStatioonTime = DateTime.Now;
                wip.OutStationTime = DateTime.Now;
                wip.UpdatedBy = this.UserCode;
                wip.UpdatedDate = DateTime.Now;


                WorkOrder wo = DBContext.Find<WorkOrder>(trans, WorkOrder.Meta.WO == wip.WorkOrder);
                MaterialStock mStock = DBContext.Find<MaterialStock>(trans, MaterialStock.Meta.MSN == wip.MSN);
                if (wo == null || mStock == null)
                {
                    throw new Exception("入库失败");//异常
                }
                //保存OR更新ProductStock数据
                ProductStock ps = DBContext.Find<ProductStock>(trans, ProductStock.Meta.PSN == psn.Trim());
                if (ps == null)
                {

                    ps = new ProductStock();
                    ps.PSN = psn.Trim();
                    ps.WorkOrder = wip.WorkOrder;
                    ps.OrderNumber = wo.OrderNumber;
                    ps.ProductName = wip.PartsName;
                    ps.ProductCode = wip.PartsCode;
                    ps.MSN = wip.MSN;
                    ps.BatchNumber = wo.BatchNumber;
                    //ps.MANUFACTURE = delivery;
                    ps.StockHouse = mStock.StockHouse;
                    ps.DOCUMENTID = mStock.DOCUMENTID;
                    ps.QUANTITY = 1;
                    ps.UNIT = mStock.UNIT;
                    ps.QualityCode = wo.QualityCode;
                    //if (up2 != null && up2.Count > 0 && tHis != null && tHis.Count > 0)
                    if (wip.STATUS == "F")
                    {
                        ps.STATUS = "2";
                        ps.MEMO = "报废入库";
                    }
                    else
                    {
                        ps.STATUS = "0";
                        ps.MEMO = "入库";
                    }

                    ps.FromBy = transferEmp;
                    ps.CreatedDate = DateTime.Now;
                    ps.UpdatedBy = this.UserCode;
                    ps.PartsdrawingCode = wip.PartsdrawingCode;//by tony add 2017-6-14

                }
                else
                {
                    //if (up2 != null && up2.Count > 0 && tHis != null && tHis.Count > 0)
                    if (wip.STATUS == "F")
                    {
                        ps.STATUS = "2";
                        ps.MEMO = "报废入库";
                    }
                    else
                    {
                        ps.STATUS = "0";
                        ps.MEMO = "入库";
                    }
                    ps.UpdatedDate = DateTime.Now;
                    ps.UpdatedBy = this.UserCode;

                }
                //保存ProductStockHistory数据
                ProductStockHistory psh = new ProductStockHistory();
                psh.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                psh.PSN = psn.Trim();
                psh.WorkOrder = wip.WorkOrder;
                psh.OrderNumber = wo.OrderNumber;
                psh.ProductName = wip.PartsName;
                psh.ProductCode = wip.PartsCode;
                psh.MSN = wip.MSN;
                psh.BatchNumber = wo.BatchNumber;
                psh.MANUFACTURE = delivery;
                psh.StockHouse = mStock.StockHouse;
                psh.DOCUMENTID = mStock.DOCUMENTID;
                psh.QUANTITY = 1;
                psh.UNIT = mStock.UNIT;
                psh.QualityCode = wo.QualityCode;
                //if (up2 != null && up2.Count > 0 && tHis != null && tHis.Count > 0)
                if (wip.STATUS == "F")
                {
                    psh.STATUS = "2";
                    psh.MEMO = "报废入库";
                }
                else
                {
                    psh.STATUS = "0";
                    psh.MEMO = "入库";
                }
                psh.FromBy = transferEmp;
                psh.CreatedDate = DateTime.Now;
                psh.UpdatedBy = this.UserCode;
                psh.PartsdrawingCode = wip.PartsdrawingCode;
                //更新OrderHead数据
                OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == wo.OrderNumber);
                if (oh == null)
                {
                    throw new Exception("入库失败");//异常
                }
                //if (up2 != null && up2.Count > 0 && tHis != null && tHis.Count > 0)
                if (wip.STATUS == "F")
                {

                }
                else
                {
                    oh.InQuantity = (string.IsNullOrEmpty(oh.InQuantity.ToString())) ? 1 : (++oh.InQuantity);
                }
                oh.UpdatedBy = this.UserCode;
                oh.UpdatedDate = DateTime.Now;

                //更新OrderDetail
                OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == wo.OrderNumber
                    & OrderDetail.Meta.PartsdrawingCode == wo.PartsdrawingCode);

                if (od == null)
                {

                    throw new Exception("入库失败");//异常
                }
                //by yajiao modify 2017-6-23
                string sql = "select * from mes_master.product_stock where order_number='" + wo.OrderNumber + "' and partsdrawing_code='" + wo.PartsdrawingCode + "' and (status='0' or status='1')";
                IList<ProductStock> pscount = DBContext.ExcuteSql(sql).ToBusiObjects<ProductStock>();
                //by tony modify 2017-6-14
                // IList<ProductStock> pscount = DBContext.FindArray<ProductStock>(ProductStock.Meta.OrderNumber == wo.OrderNumber & ProductStock.Meta.PartsdrawingCode == wo.PartsdrawingCode&ProductStock.Meta.STATUS=="0");
                //if (up2 != null && up2.Count > 0 && tHis != null && tHis.Count > 0)
                if (wip.STATUS == "F")
                {
                    od.InQuantity = pscount.Count + 1;
                }
                else
                {
                    od.InQuantity = 1;// pscount.Count; //by tony modify 2018-1-12
                }
                //od.InQuantity = (string.IsNullOrEmpty(od.InQuantity.ToString())) ? 1 : (++od.InQuantity);
                //保存OrderHistory数据

                OrderHistory oHistory = new OrderHistory();

                oHistory.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                oHistory.OrderNo = oh.OrderNo;
                oHistory.CustName = oh.CustName;
                oHistory.CustCode = oh.CustCode;
                oHistory.CONTRACT = oh.CONTRACT;
                oHistory.ProductName = od.ProductName;
                oHistory.ProductCode = od.ProductCode;
                oHistory.PartsdrawingCode = od.PartsdrawingCode;
                oHistory.BatchNumber = od.BatchNumber;
                oHistory.OrderQuantity = od.OrderQuantity;
                oHistory.InQuantity = od.InQuantity;
                oHistory.OutQuantity = od.OutQuantity;
                oHistory.OutDate = od.OutDate;
                oHistory.STATUS = od.STATUS;
                oHistory.MEMO = od.MEMO;
                oHistory.CreatedDate = DateTime.Now;
                oHistory.UpdatedBy = this.UserCode;

                TrackingHistory th = new TrackingHistory();

                th.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                th.PSN = wip.PSN;
                th.MSN = wip.MSN;
                th.WorkOrder = wip.WorkOrder;
                th.PartsdrawingCode = wip.PartsdrawingCode;
                th.PartsName = wip.PartsName;
                th.PartsCode = wip.PartsCode;
                th.BatchNumber = wip.BatchNumber;
                th.StationName = wip.StationName;
                th.StationId = wip.StationId;
                th.NextStation = wip.NextStation;
                th.NextStationId = wip.NextStationId;
                th.QUANTITY = wip.QUANTITY;
                th.STATUS = wip.STATUS;
                th.InStationTime = DateTime.Now;
                th.OutStationTime = DateTime.Now;
                th.TaskTime = wip.TaskTime;
                th.MachineType = wo.MachineType;
                th.MachineName = wo.MachineName;
                th.MEMO = wip.MEMO;
                th.CreatedDate = DateTime.Now;
                th.UpdatedBy = this.UserCode;


                RealtimeStatistics rs = new RealtimeStatistics();
                rs.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                rs.PSN = psn.Trim();
                rs.MSN = wip.MSN;
                rs.WorkOrder = wo.WO;
                rs.StationName = wip.StationName;
                rs.MachineName = wo.MachineName;
                rs.MachineType = wo.MachineType;
                rs.STATUS = "P";//入库成功
                rs.QUANTITY = 1;
                rs.OPERATOR = this.UserCode;//by tony modify 2017-5-17
                rs.OrderNumber = wo.OrderNumber;
                rs.ProductName = wo.ProductName;
                rs.ProductCode = wo.ProductCode;
                rs.CustName = mStock.CustName;
                rs.PartsdrawingCode = wip.PartsdrawingCode;
                rs.CreatedDate = DateTime.Now;
                rs.UpdatedBy = this.UserCode;

                //保存所有数据
                DBContext.SaveAndUpdate<ProductStock>(trans, ps);
                DBContext.SaveAndUpdate<ProductStockHistory>(trans, psh);
                DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                DBContext.SaveAndUpdate<OrderDetail>(trans, od);
                DBContext.SaveAndUpdate<OrderHistory>(trans, oHistory);
                DBContext.SaveAndUpdate<TrackingWip>(trans, wip);
                DBContext.SaveAndUpdate<TrackingHistory>(trans, th);
                //DBContext.SaveAndUpdate<RealtimeStatistics>(trans, rs);
                SaveRealtimeStatistics(rs);//by tony modify 2017-5-17
                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "ProductIn");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
            //IDbTransaction trans = DBContext.OpenTrans();
            //try
            //{
            //    if (string.IsNullOrEmpty(transferEmp) || string.IsNullOrEmpty(delivery) || string.IsNullOrEmpty(psn))
            //    {
            //        return "errorNull";
            //        //异常

            //    }
            //    IList<UnsurenessProduct> up = DBContext.FindArray<UnsurenessProduct>(trans, UnsurenessProduct.Meta.PSN == psn & UnsurenessProduct.Meta.STATUS != "4");
            //    if (up != null && up.Count > 0)
            //    {
            //       // throw new Exception("该产品属待处理品，未审核通过");
            //         return "该产品属待处理品，未审核通过！";//by tony modify 2017-5-22
            //    }
            //    TrackingWip wip = DBContext.Find<TrackingWip>(trans, TrackingWip.Meta.PSN == psn);
            //    if (wip == null || string.IsNullOrEmpty(wip.WorkOrder) || string.IsNullOrEmpty(wip.MSN))
            //    {
            //        //throw new Exception("该产品未排入生产计划内生产");
            //        return "该产品未排入生产计划内生产！";//异常//by tony modify 2017-5-22

            //    }
            //    if(wip.NextStation.Trim()!="INSTOCK")
            //    {
            //       // throw new Exception("该产品未做终检或终检不合格，禁止入库");
            //        return "该产品未做终检或终检不合格，禁止入库";
            //    }

            //    //by tony modify 2017-5-22
            //    //if (!string.IsNullOrEmpty(wip.NextStation) && !wip.NextStation.Trim().Equals("INSTOCK"))
            //    //{
            //    //    return "errorStation";
            //    //}
            //    wip.StationName = "INSTOCK";
            //    wip.NextStation = "OUTSTOCK";
            //    wip.InStatioonTime = DateTime.Now;
            //    wip.OutStationTime = DateTime.Now;
            //    wip.UpdatedBy = this.UserCode;
            //    wip.UpdatedDate = DateTime.Now;


            //    WorkOrder wo = DBContext.Find<WorkOrder>(trans, WorkOrder.Meta.WO == wip.WorkOrder);
            //    MaterialStock mStock = DBContext.Find<MaterialStock>(trans, MaterialStock.Meta.MSN == wip.MSN);
            //    if (wo == null || mStock == null)
            //    {
            //        throw new Exception("入库失败");//异常
            //    }
            //    //保存OR更新ProductStock数据
            //    ProductStock ps = DBContext.Find<ProductStock>(trans, ProductStock.Meta.PSN == psn);
            //    if (ps == null)
            //    {

            //        ps = new ProductStock();
            //        ps.PSN = psn;
            //        ps.WorkOrder = wip.WorkOrder;
            //        ps.OrderNumber = wo.OrderNumber;
            //        ps.ProductName = wip.PartsName;
            //        ps.ProductCode = wip.PartsCode;
            //        ps.MSN = wip.MSN;
            //        ps.BatchNumber = wo.BatchNumber;
            //        ps.MANUFACTURE = delivery;
            //        ps.StockHouse = mStock.StockHouse;
            //        ps.DOCUMENTID = mStock.DOCUMENTID;
            //        ps.QUANTITY = 1;
            //        ps.UNIT = mStock.UNIT;
            //        ps.STATUS = "0";
            //        ps.MEMO = "入库";
            //        ps.FromBy = transferEmp;
            //        ps.CreatedDate = DateTime.Now;
            //        ps.UpdatedBy = this.UserCode;
            //        ps.PartsdrawingCode = wip.PartsdrawingCode;//by tony add 2017-6-14

            //    }
            //    else
            //    {
            //        ps.STATUS = "0";
            //        ps.MEMO = "入库";
            //        ps.UpdatedDate = DateTime.Now;
            //        ps.UpdatedBy = this.UserCode;

            //    }
            //    //保存ProductStockHistory数据
            //    ProductStockHistory psh = new ProductStockHistory();
            //    psh.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            //    psh.PSN = psn;
            //    psh.WorkOrder = wip.WorkOrder;
            //    psh.OrderNumber = wo.OrderNumber;
            //    psh.ProductName = wip.PartsName;
            //    psh.ProductCode = wip.PartsCode;
            //    psh.MSN = wip.MSN;
            //    psh.BatchNumber = wo.BatchNumber;
            //    psh.MANUFACTURE = delivery;
            //    psh.StockHouse = mStock.StockHouse;
            //    psh.DOCUMENTID = mStock.DOCUMENTID;
            //    psh.QUANTITY = 1;
            //    psh.UNIT = mStock.UNIT;
            //    psh.STATUS = "0";
            //    psh.MEMO = "入库";
            //    psh.FromBy = transferEmp;
            //    psh.CreatedDate = DateTime.Now;
            //    psh.UpdatedBy = this.UserCode;
            //    psh.PartsdrawingCode = wip.PartsdrawingCode;
            //    //更新OrderHead数据
            //    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == wo.OrderNumber);
            //    if (oh == null)
            //    {
            //        throw new Exception("入库失败");//异常
            //    }
            //    oh.InQuantity = (string.IsNullOrEmpty(oh.InQuantity.ToString())) ? 1 : (++oh.InQuantity);
            //    oh.UpdatedBy = this.UserCode;
            //    oh.UpdatedDate = DateTime.Now;

            //    //更新OrderDetail
            //    OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == wo.OrderNumber
            //        & OrderDetail.Meta.PartsdrawingCode == wo.PartsdrawingCode);

            //    if (od == null)
            //    {

            //        throw new Exception("入库失败");//异常
            //    }
            //    //by tony modify 2017-6-14
            //    IList<ProductStock> pscount = DBContext.FindArray<ProductStock>(ProductStock.Meta.OrderNumber == wo.OrderNumber & ProductStock.Meta.PartsdrawingCode == wo.PartsdrawingCode);
            //    od.InQuantity = pscount.Count;
            //    //od.InQuantity = (string.IsNullOrEmpty(od.InQuantity.ToString())) ? 1 : (++od.InQuantity);
            //    //保存OrderHistory数据

            //    OrderHistory oHistory = new OrderHistory();

            //    oHistory.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            //    oHistory.OrderNo = oh.OrderNo;
            //    oHistory.CustName = oh.CustName;
            //    oHistory.CustCode = oh.CustCode;
            //    oHistory.CONTRACT = oh.CONTRACT;
            //    oHistory.ProductName = od.ProductName;
            //    oHistory.ProductCode = od.ProductCode;
            //    oHistory.PartsdrawingCode = od.PartsdrawingCode;
            //    oHistory.BatchNumber = od.BatchNumber;
            //    oHistory.OrderQuantity = od.OrderQuantity;
            //    oHistory.InQuantity = od.InQuantity;
            //    oHistory.OutQuantity = od.OutQuantity;
            //    oHistory.OutDate = od.OutDate;
            //    oHistory.STATUS = od.STATUS;
            //    oHistory.MEMO = od.MEMO;
            //    oHistory.CreatedDate = DateTime.Now;
            //    oHistory.UpdatedBy = this.UserCode;

            //    TrackingHistory th = new TrackingHistory();

            //    th.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            //    th.PSN = wip.PSN;
            //    th.MSN = wip.MSN;
            //    th.WorkOrder = wip.WorkOrder;
            //    th.PartsdrawingCode = wip.PartsdrawingCode;
            //    th.PartsName = wip.PartsName;
            //    th.PartsCode = wip.PartsCode;
            //    th.BatchNumber = wip.BatchNumber;
            //    th.StationName = wip.StationName;
            //    th.QUANTITY = wip.QUANTITY;
            //    th.STATUS = wip.STATUS;
            //    th.InStationTime = DateTime.Now;
            //    th.OutStationTime = DateTime.Now;
            //    th.TaskTime = wip.TaskTime;
            //    th.MachineType = wo.MachineType;
            //    th.MachineName = wo.MachineName;
            //    th.MEMO = wip.MEMO;
            //    th.CreatedDate = DateTime.Now;
            //    th.UpdatedBy = this.UserCode;


            //    RealtimeStatistics rs = new RealtimeStatistics();
            //    rs.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            //    rs.PSN = psn;
            //    rs.MSN = wip.MSN;
            //    rs.WorkOrder = wo.WO;
            //    rs.StationName = wip.StationName;
            //    rs.MachineName = wo.MachineName;
            //    rs.MachineType = wo.MachineType;
            //    rs.STATUS = "P";//入库成功
            //    rs.QUANTITY = 1;
            //    rs.OPERATOR = this.UserCode;//by tony modify 2017-5-17
            //    rs.OrderNumber = wo.OrderNumber;
            //    rs.ProductName = wo.ProductName;
            //    rs.ProductCode = wo.ProductCode;
            //    rs.CustName = mStock.CustName;
            //    rs.PartsdrawingCode = wip.PartsdrawingCode;
            //    rs.CreatedDate = DateTime.Now;
            //    rs.UpdatedBy = this.UserCode;

            //    //保存所有数据
            //    DBContext.SaveAndUpdate<ProductStock>(trans, ps);
            //    DBContext.SaveAndUpdate<ProductStockHistory>(trans, psh);
            //    DBContext.SaveAndUpdate<OrderHead>(trans, oh);
            //    DBContext.SaveAndUpdate<OrderDetail>(trans, od);
            //    DBContext.SaveAndUpdate<OrderHistory>(trans, oHistory);
            //    DBContext.SaveAndUpdate<TrackingWip>(trans, wip);
            //    DBContext.SaveAndUpdate<TrackingHistory>(trans, th);
            //    //DBContext.SaveAndUpdate<RealtimeStatistics>(trans, rs);
            //    SaveRealtimeStatistics(rs);//by tony modify 2017-5-17
            //    trans.Commit();
            //    return "OK";
            //}
            //catch (Exception ex)
            //{
            //    trans.Rollback();
            //    PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "ProductIn");
            //    throw ex;
            //}
            //finally
            //{
            //    DBContext.ReleaseTrans(trans);
            //}
        }

        public IList<ProductStock> ListTopProductInWH(int showNum)
        {

            string sql = @"SELECT * FROM(SELECT R.*,ROW_NUMBER() OVER(ORDER BY R.CREATED_DATE DESC) RN FROM MES_MASTER.PRODUCT_STOCK R WHERE  STATUS='{0}' ) T WHERE T.RN <= {1}";
            sql = string.Format(sql, "0", showNum);

            IList<ProductStock> ps = DBContext.ExcuteSql(sql).ToBusiObjects<ProductStock>();
            if (ps != null && ps.Count > 0)
            {
                for (int i = 0; i < ps.Count; i++)
                {
                    ps[i].MANUFACTURE = FindNameBySubCode(ps[i].MANUFACTURE) == null ? ps[i].MANUFACTURE : FindNameBySubCode(ps[i].MANUFACTURE).SubName;
                    ps[i].UNIT = FindNameBySubCode(ps[i].UNIT) == null ? ps[i].UNIT : FindNameBySubCode(ps[i].UNIT).SubName;
                    ps[i].StockHouse = FindNameBySubCode(ps[i].StockHouse) == null ? ps[i].StockHouse : FindNameBySubCode(ps[i].StockHouse).SubName;
                }
            }
            return ps;

        }

        public BasBase FindNameBySubCode(string subCode)
        {

            if (string.IsNullOrEmpty(subCode))
            {
                return null;
            }
            BasBase baseName = DBContext.Find<BasBase>(BasBase.Meta.SubCode == subCode.Trim());
            if (baseName == null || string.IsNullOrEmpty(baseName.SubName))
            {
                return null;
            }
            return baseName;

        }

        public BasCustom FindCustNameByCode(string custCode)
        {

            if (string.IsNullOrEmpty(custCode))
            {
                return null;
            }
            BasCustom bc = DBContext.Find<BasCustom>(BasBase.Meta.CODE == custCode.Trim());
            if (bc == null || string.IsNullOrEmpty(bc.NAME))
            {
                return null;
            }
            return bc;

        }

        public SysUser FindUserNameByCode(string userCode)
        {

            if (string.IsNullOrEmpty(userCode))
            {
                return null;
            }
            SysUser bc = DBContext.Find<SysUser>(SysUser.Meta.UserCode == UserCode.Trim());
            if (bc == null || string.IsNullOrEmpty(bc.UserName))
            {
                return null;
            }
            return bc;

        }

        public DataSet FindStandByOutWH()
        {

            string sql = @"SELECT OD.ORDER_NO,WO.WO,OD.PARTSDRAWING_CODE,WO.STATUS,OD.PRODUCT_NAME,OD.OUT_NOTICE_QTY,WO.QUANTITY, WO.QUALITY_CODE,                      ";
            sql = sql + "OD.CREATED_DATE,OD.UPDATED_DATE,WO.BATCH_NUMBER,OD.ORDER_QUANTITY,WO.CHECK_TIME,WO.IN_TIME       ";
            sql = sql + "FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.WORK_ORDER WO                               ";
            sql = sql + "ON OD.ORDER_NO=WO.ORDER_NUMBER AND OD.PARTSDRAWING_CODE=WO.PARTSDRAWING_CODE                     ";
            sql = sql + "WHERE OD.STATUS='{0}'                                                                             ";

            sql = string.Format(sql, 2);

            return DBContext.ExcuteSql(sql).ToDataSet();
        }


        public IList<ProductStock> FindStandByDetail(string workOrder)
        {
            if (string.IsNullOrEmpty(workOrder))
            {
                return null;
            }

            IList<ProductStock> ps = DBContext.FindArray<ProductStock>(ProductStock.Meta.WorkOrder == workOrder & ProductStock.Meta.STATUS == "0");
            if (ps != null && ps.Count > 0)
            {
                for (int i = 0; i < ps.Count; i++)
                {
                    ps[i].MANUFACTURE = FindNameBySubCode(ps[i].MANUFACTURE) == null ? ps[i].MANUFACTURE : FindNameBySubCode(ps[i].MANUFACTURE).SubName;
                    ps[i].StockHouse = FindNameBySubCode(ps[i].StockHouse) == null ? ps[i].StockHouse : FindNameBySubCode(ps[i].StockHouse).SubName;
                    MaterialStock ms = DBContext.Find<MaterialStock>(MaterialStock.Meta.MSN == ps[i].MSN);
                    ps[i].ProductCode = (ms == null) ? "" : ms.MaterialCode;
                    ps[i].ProductName = (ms == null) ? "" : ms.MaterialName;

                }
            }
            return ps;

        }

        public string SaveProductOutWH(string psn)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                if (string.IsNullOrEmpty(psn))
                {

                    return "条码不能为空";
                }
                ProductStock ps = DBContext.Find<ProductStock>(trans, ProductStock.Meta.PSN == psn & ProductStock.Meta.STATUS == "0");
                if (ps == null)
                {
                    return "条码有误，请重新扫描";
                }
                ps.STATUS = "1";
                ps.MEMO = "出库";
                ps.UpdatedBy = this.UserCode;
                ps.UpdatedDate = DateTime.Now;

                ProductStockHistory psh = new ProductStockHistory();
                psh.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                psh.PSN = psn;
                psh.WorkOrder = ps.WorkOrder;
                psh.OrderNumber = ps.OrderNumber;
                psh.ProductName = ps.ProductName;
                psh.ProductCode = ps.ProductCode;
                psh.MSN = ps.MSN;
                psh.BatchNumber = ps.BatchNumber;
                psh.MANUFACTURE = ps.MANUFACTURE;
                psh.StockHouse = ps.StockHouse;
                psh.DOCUMENTID = ps.DOCUMENTID;
                psh.QUANTITY = 1;
                psh.UNIT = ps.UNIT;
                psh.STATUS = ps.STATUS;
                psh.MEMO = ps.MEMO;
                psh.FromBy = ps.FromBy;
                psh.CreatedDate = DateTime.Now;
                psh.UpdatedBy = this.UserCode;
                psh.PartsdrawingCode = ps.PartsdrawingCode;

                OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == ps.OrderNumber);
                if (oh == null)
                {
                    return "出库失败";
                }
                oh.OutQuantity = (oh.OutQuantity == null) ? 1 : ++oh.OutQuantity;
                oh.UpdatedBy = this.UserCode;
                oh.UpdatedDate = DateTime.Now;

                WorkOrder wo = DBContext.Find<WorkOrder>(trans, WorkOrder.Meta.OrderNumber == ps.OrderNumber & WorkOrder.Meta.WO == ps.WorkOrder);
                if (wo == null)
                {
                    return "出库失败";
                }
                OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == ps.OrderNumber & OrderDetail.Meta.PartsdrawingCode == wo.PartsdrawingCode & OrderDetail.Meta.STATUS == "2");
                if (od == null)
                {
                    return "该订单非发货状态";
                }
                //by tony modify 2017-6-14
                IList<ProductStock> pscount = DBContext.FindArray<ProductStock>(ProductStock.Meta.OrderNumber == ps.OrderNumber 
                                                                               & ProductStock.Meta.PartsdrawingCode == wo.PartsdrawingCode
                                                                               &ProductStock.Meta.STATUS=="1");
                od.OutQuantity = pscount.Count+1;
               // od.OutQuantity = (od.OutQuantity == null) ? 1 : ++od.OutQuantity;
                od.ThisOutQty = (od.ThisOutQty == null) ? 1 : ++od.ThisOutQty;
                od.UpdatedDate = DateTime.Now;
                od.UpdatedBy = this.UserCode;

                OrderHistory oHistory = new OrderHistory();
                oHistory.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                oHistory.OrderNo = oh.OrderNo;
                oHistory.CustName = oh.CustName;
                oHistory.CustCode = oh.CustCode;
                oHistory.CONTRACT = oh.CONTRACT;
                oHistory.ProductName = od.ProductName;
                oHistory.ProductCode = od.ProductCode;
                oHistory.PartsdrawingCode = od.PartsdrawingCode;
                oHistory.BatchNumber = od.BatchNumber;
                oHistory.OrderQuantity = od.OrderQuantity;
                oHistory.InQuantity = od.InQuantity;
                oHistory.OutQuantity = od.OutQuantity;
                oHistory.OutDate = od.OutDate;
                oHistory.STATUS = od.STATUS;
                oHistory.MEMO = od.MEMO;
                oHistory.CreatedDate = DateTime.Now;
                oHistory.UpdatedBy = this.UserCode;
                if (od.ThisOutQty > od.OutNoticeQty||od.ThisOutQty==od.OutNoticeQty)//by tony modify 2017-6-14
                {
                    od.ThisOutQty = 0;
                    od.OutNoticeQty = 0;
                    oHistory.ThisOutQty = null;
                    oHistory.OutNoticeQty = null;
                   // if (od.OutQuantity >= od.OutQuantity)
                    if (od.OutQuantity >od.OrderQuantity||od.OutQuantity==od.OrderQuantity)//by tony modify 2017-6-14
                    {
                        od.STATUS = "3";
                        od.MEMO = "关闭";//by tony add 2017-6-12
                        oHistory.STATUS = "3";
                        oHistory.MEMO = "关闭";//by tony add 2017-6-12
                        oh.STATUS = "3";
                        oh.MEMO = "关闭";//by tony add 2017-6-12
                    }
                    else
                    {
                        od.STATUS = "1";
                        od.MEMO = "发布";//by tony add 2017-6-12
                        oHistory.STATUS = "1";
                        oHistory.MEMO = "发布";//by tony add 2017-6-12
                        oh.STATUS = "1";
                        oh.MEMO = "发布";//by tony add 2017-6-12
                    }
                }

                TrackingWip wip = DBContext.Find<TrackingWip>(trans, TrackingWip.Meta.PSN == psn);
                if (wip == null)
                {
                    return "出库失败";//异常

                }
                if (!string.IsNullOrEmpty(wip.NextStation) && !wip.NextStation.Trim().Equals("OUTSTOCK"))
                {
                    return "该条码未非待出库条码";
                }
                wip.StationName = "OUTSTOCK";
                wip.NextStation = "FINISHED";
                wip.InStatioonTime = DateTime.Now;
                wip.OutStationTime = DateTime.Now;
                wip.UpdatedBy = this.UserCode;
                wip.UpdatedDate = DateTime.Now;

                TrackingHistory th = new TrackingHistory();

                th.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                th.PSN = wip.PSN;
                th.MSN = wip.MSN;
                th.WorkOrder = wip.WorkOrder;
                th.PartsdrawingCode = wip.PartsdrawingCode;
                th.PartsName = wip.PartsName;
                th.PartsCode = wip.PartsCode;
                th.BatchNumber = wip.BatchNumber;
                th.StationName = wip.StationName;
                th.QUANTITY = wip.QUANTITY;
                th.STATUS = wip.STATUS;
                th.InStationTime = DateTime.Now;
                th.OutStationTime = DateTime.Now;
                th.TaskTime = wip.TaskTime;
                th.MachineType = wo.MachineType;
                th.MachineName = wo.MachineName;
                th.MEMO = wip.MEMO;
                th.CreatedDate = DateTime.Now;
                th.UpdatedBy = this.UserCode;

                MaterialStock mStock = DBContext.Find<MaterialStock>(trans, MaterialStock.Meta.MSN == wip.MSN);
                if (wo == null || mStock == null)
                {
                    return "出库有误";//异常
                }

                RealtimeStatistics rs = new RealtimeStatistics();
                rs.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                rs.PSN = psn;
                rs.MSN = wip.MSN;
                rs.WorkOrder = wo.WO;
                rs.StationName = wip.StationName;
                rs.MachineName = wo.MachineName;
                rs.MachineType = wo.MachineType;
                rs.STATUS = "P";//入库成功
                rs.QUANTITY = 1;
                rs.OPERATOR = this.UserCode;//by tony modify 2017-5-17
                rs.OrderNumber = wo.OrderNumber;
                rs.ProductName = wo.ProductName;
                rs.ProductCode = wo.ProductCode;
                rs.CustName = mStock.CustName;
                rs.PartsdrawingCode = wip.PartsdrawingCode;
                rs.CreatedDate = DateTime.Now;
                rs.UpdatedBy = this.UserCode;

                //保存所有数据
                DBContext.SaveAndUpdate<ProductStock>(trans, ps);
                DBContext.SaveAndUpdate<ProductStockHistory>(trans, psh);
                DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                DBContext.SaveAndUpdate<OrderDetail>(trans, od);
                DBContext.SaveAndUpdate<OrderHistory>(trans, oHistory);
                DBContext.SaveAndUpdate<TrackingWip>(trans, wip);
                DBContext.SaveAndUpdate<TrackingHistory>(trans, th);
                //DBContext.SaveAndUpdate<RealtimeStatistics>(trans, rs);
                SaveRealtimeStatistics(rs);//by tony modify 2017-5-17
                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "ProductOut");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public IList<ProductStock> FindProductOutWH()
        {
            string sql = @"SELECT * FROM MES_MASTER.PRODUCT_STOCK WHERE ROWNUM<='{0}' AND STATUS='{1}' ORDER BY CREATED_DATE DESC";
            sql = string.Format(sql, BLLConstants.I_OUT_SHOW_NUM, "1");

            IList<ProductStock> ps = DBContext.ExcuteSql(sql).ToBusiObjects<ProductStock>();
            if (ps != null && ps.Count > 0)
            {
                for (int i = 0; i < ps.Count; i++)
                {
                    ps[i].MANUFACTURE = FindNameBySubCode(ps[i].MANUFACTURE) == null ? ps[i].MANUFACTURE : FindNameBySubCode(ps[i].MANUFACTURE).SubName;
                    ps[i].UNIT = FindNameBySubCode(ps[i].UNIT) == null ? ps[i].UNIT : FindNameBySubCode(ps[i].UNIT).SubName;
                    ps[i].StockHouse = FindNameBySubCode(ps[i].StockHouse) == null ? ps[i].StockHouse : FindNameBySubCode(ps[i].StockHouse).SubName;
                }
            }
            return ps;

        }

        public IList<ProductStock> FindProductStockInfo(string workorder, string productname, string batchnumber, string starttime, string endtime)
        {
            ConditionExpress ce = null;

            if (!string.IsNullOrEmpty(workorder))
            {

                ce = (ce & ProductStock.Meta.WorkOrder == workorder);
            }

            if (!string.IsNullOrEmpty(productname))
            {
                ce = (ce & ProductStock.Meta.ProductName == productname);
            }
            if (!string.IsNullOrEmpty(batchnumber))
            {
                ce = (ce & ProductStock.Meta.BatchNumber == batchnumber);
            }

            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                ce = (ce & ProductStock.Meta.UpdatedDate >= Convert.ToDateTime(starttime));
                ce = (ce & ProductStock.Meta.UpdatedDate <= Convert.ToDateTime(endtime));
            }
            //else
            //{
            //    ce = (ce & ProductStock.Meta.UpdatedDate >= DateTime.Now.AddDays(-7));
            //    ce = (ce & ProductStock.Meta.UpdatedDate <= DateTime.Now);
            //}
            if (ce == null)
            {
                return DBContext.LoadArray<ProductStock>();
            }
            return DBContext.FindArray<ProductStock>(ce);
        }

        public IList<ProductStockHistory> FindProductStockHistory(string status, string workorder, string productname, string batchnumber, string starttime, string endtime)
        {
            ConditionExpress ce = null;
            
            if (!string.IsNullOrEmpty(status))
            {
                ce = (ce & ProductStockHistory.Meta.STATUS == status);
            }
            if (!string.IsNullOrEmpty(workorder))
            {

                ce = (ce & ProductStockHistory.Meta.WorkOrder == workorder);
            }

            if (!string.IsNullOrEmpty(productname))
            {
                ce = (ce & ProductStockHistory.Meta.ProductName == productname);
            }
            if (!string.IsNullOrEmpty(batchnumber))
            {
                ce = (ce & ProductStockHistory.Meta.BatchNumber == batchnumber);
            }

            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                ce = (ce & ProductStockHistory.Meta.CreatedDate >= Convert.ToDateTime(starttime));
                ce = (ce & ProductStockHistory.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }
            //else
            //{
            //    ce = (ce & ProductStockHistory.Meta.CreatedDate >= DateTime.Now.AddDays(-7));
            //    ce = (ce & ProductStockHistory.Meta.CreatedDate <= DateTime.Now);
            //}

            return DBContext.FindArray<ProductStockHistory>(ce,ProductStockHistory.Meta.CreatedDate.DESC);
        }


        public PagingResult<ProductStockHistory> FindProductStockHistory(string status, string workorder, string productname, string batchnumber, string starttime, string endtime, string rows, string page)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "0")//入库
                {
                    ce = (ce & (ProductStockHistory.Meta.STATUS == "0"
                        | ProductStockHistory.Meta.STATUS == "1"));
                }
                else if (status == "1" || status == "2")//出库或报废
                {
                    ce = (ce & ProductStockHistory.Meta.STATUS == status);
                }
                else if (status == "3")//库存
                {
                    ce = (ce & ProductStockHistory.Meta.STATUS == "0");
                }
            }
            if (!string.IsNullOrEmpty(workorder))
            {

                //ce = (ce & ProductStockHistory.Meta.WorkOrder == workorder);
                ce = (ce & ProductStockHistory.Meta.WorkOrder.Like(workorder));
            }

            if (!string.IsNullOrEmpty(productname))
            {
                ce = (ce & ProductStockHistory.Meta.ProductName == productname);
            }
            if (!string.IsNullOrEmpty(batchnumber))
            {
                ce = (ce & ProductStockHistory.Meta.BatchNumber == batchnumber);
            }

            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                ce = (ce & ProductStockHistory.Meta.CreatedDate >= Convert.ToDateTime(starttime));
                ce = (ce & ProductStockHistory.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }
            else
            {
                if (string.IsNullOrEmpty(status) && string.IsNullOrEmpty(workorder)
                    && string.IsNullOrEmpty(batchnumber) && string.IsNullOrEmpty(productname))
                {
                    ce = (ce & ProductStockHistory.Meta.CreatedDate >= DateTime.Now.AddDays(-30));
                    ce = (ce & ProductStockHistory.Meta.CreatedDate <= DateTime.Now);
                }
            }

            return DBContext.FindArrayByPaging<ProductStockHistory>(ce, Convert.ToInt32(rows), Convert.ToInt32(page), ProductStockHistory.Meta.ID.DESC);
        }
        //by tony add 2017-5-17
        public void SaveRealtimeStatistics(RealtimeStatistics obj)
        {
            try
            {
                if (DBContext.Exist<RealtimeStatistics>(RealtimeStatistics.Meta.PSN == obj.PSN & RealtimeStatistics.Meta.StationName == obj.StationName))
                {
                    obj.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<RealtimeStatistics>(obj);

                }
                else
                {
                    obj.CreatedDate = DateTime.Now;
                    obj.UpdatedBy = this.UserCode;
                    obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    DBContext.SaveAndUpdate<RealtimeStatistics>(obj);
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveRealtimeStatistics");
                throw ex;
            }

        }

        public IList<BasBase> FindBaseBySubCode(string subCode)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(subCode))
            {
                ce = (BasBase.Meta.SubCode == subCode);
            }


            if (ce == null)
            {
                return DBContext.LoadArray<BasBase>();
            }

            return DBContext.FindArray<BasBase>(ce);

        }

        public IList<WorkOrder> FindOnlineWO(string rows, string page,string workorder="",string partsdrawingno="")
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            string sql = "SELECT * FROM (SELECT * FROM( SELECT WO.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\" FROM MES_MASTER.WORK_ORDER WO WHERE STATUS='{0}' and ( MATERIAL_QTY is  null   or MATERIAL_QTY!=PLAN_QUANTITY ) ";
            if(!string.IsNullOrEmpty(workorder))
            {
                sql += " and wo.wo like '%" + workorder + "%'";
            }
            if(!string.IsNullOrEmpty(partsdrawingno))
            {
                sql += " and wo.partsdrawing_code like '%" + partsdrawingno + "%'";
            }
            sql+= " ) P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2} ";
            //string sql = "SELECT * FROM (SELECT * FROM( SELECT WO.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\" FROM MES_MASTER.WORK_ORDER WO WHERE STATUS='{0}'  ) P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2} ";

            sql = string.Format(sql, 1, first, iRows);
            IList<WorkOrder> list = DBContext.ExcuteSql(sql).ToBusiObjects<WorkOrder>();
            if (list == null || list.Count == 0)
            {
                return null;
            }
            //foreach (WorkOrder wo in list)
            //{
            //    if (wo.MaterialQty != null && wo.MaterialQty == wo.PlanQuantity)
            //    {
            //        list.Remove(wo);
            //    }

            //}
            return list;
        }
        public IList<WorkOrder> FindOnlineWOByKanBan(string rows, string page)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            // string sql = "SELECT * FROM (SELECT * FROM( SELECT WO.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\" FROM MES_MASTER.WORK_ORDER WO WHERE STATUS='{0}' and ( MATERIAL_QTY is  null   or MATERIAL_QTY!=PLAN_QUANTITY ) ) P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2} ";
            string sql = "SELECT * FROM (SELECT * FROM( SELECT WO.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\" FROM MES_MASTER.WORK_ORDER WO WHERE STATUS='{0}'  ) P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2} ";

            sql = string.Format(sql, 1, first, iRows);
            IList<WorkOrder> list = DBContext.ExcuteSql(sql).ToBusiObjects<WorkOrder>();
            if (list == null || list.Count == 0)
            {
                return null;
            }
            //foreach (WorkOrder wo in list)
            //{
            //    if (wo.MaterialQty != null && wo.MaterialQty == wo.PlanQuantity)
            //    {
            //        list.Remove(wo);
            //    }

            //}
            return list;
        }

        public int FindOnlineWOCount()
        {
            string sql = "SELECT count(*) FROM MES_MASTER.WORK_ORDER WHERE STATUS='{0}' and ( MATERIAL_QTY is  null   or MATERIAL_QTY!=PLAN_QUANTITY )";
            sql = string.Format(sql, 1);
            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }

            return iCount;
        }

        public IList<MaterialStock> QueryPrepareInfo(string workorder)
        {
            WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == workorder);
            if (wo != null)
            {
                return DBContext.FindArray<MaterialStock>(MaterialStock.Meta.MaterialName == wo.PartsdrawingCode & (MaterialStock.Meta.STATUS == "0" | MaterialStock.Meta.STATUS == "2"), MaterialStock.Meta.MSN.ASC);
            }
            else
            {
                if (string.IsNullOrEmpty(workorder))
                {
                    //查询工单运行中，且发料数量为0的工单。
                    IList<WorkOrder> wos = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == "1" & WorkOrder.Meta.MaterialQty < 1, WorkOrder.Meta.UpdatedDate.DESC);
                    if (wos.Count > 0)
                    {
                        return DBContext.FindArray<MaterialStock>(MaterialStock.Meta.MaterialName == wos[0].PartsdrawingCode & (MaterialStock.Meta.STATUS == "0" | MaterialStock.Meta.STATUS == "2"), MaterialStock.Meta.MSN.ASC);
                    }
                }
                return null;
            }
        }

        public int FindOrderDetailCount(string status)
        {
            string sql = "SELECT COUNT(*) FROM MES_MASTER.ORDER_DETAIL WHERE STATUS='{0}'  ";
            sql = string.Format(sql, status);
            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }

            return iCount;
        }

        public IList<OrderDetail> FindOrderDetail(string rows, string page, string status)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            string sql = "SELECT * FROM (SELECT * FROM( SELECT OD.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\" FROM MES_MASTER.ORDER_DETAIL OD WHERE STATUS='{0}' ) P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2} ";
            sql = string.Format(sql, status, first, iRows);
            IList<OrderDetail> list = DBContext.ExcuteSql(sql).ToBusiObjects<OrderDetail>();

            return list;
        }

        public int FindReceiveMarCount(string materialCode)
        {
            string sql = "SELECT COUNT(*) FROM MES_MASTER.MATERIAL_STOCK WHERE STATUS='{0}'   ";
            if (!string.IsNullOrEmpty(materialCode))
            {
                sql = sql + " AND MATERIAL_CODE='" + materialCode + "'";
            }
            sql = string.Format(sql, "0");
            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }

            return iCount;
        }

        public IList<MaterialStock> FindReceiveMar(string rows, string page, string materialCode)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            string sql = "SELECT * FROM (SELECT * FROM( SELECT MS.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\" FROM MES_MASTER.MATERIAL_STOCK MS WHERE STATUS='{0}' ";
            if (!string.IsNullOrEmpty(materialCode))
            {
                sql = sql + " AND MATERIAL_CODE='" + materialCode + "'";
            }

            sql = sql + " ) P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2} ";
            sql = string.Format(sql, "0", first, iRows);
            IList<MaterialStock> list = DBContext.ExcuteSql(sql).ToBusiObjects<MaterialStock>();

            return list;
        }

        public int FindPreparedMarCount(string workOrder)
        {
            WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == workOrder);
            string sql = "";
            if (wo != null)
            {
                sql = "SELECT COUNT(*) FROM MES_MASTER.MATERIAL_STOCK WHERE (STATUS='0' OR STATUS='2')  AND MATERIAL_NAME='" + wo.PartsdrawingCode + "' ORDER BY MSN ASC                  ";
                //return DBContext.FindArray<MaterialStock>(MaterialStock.Meta.MaterialName == wo.PartsdrawingCode & (MaterialStock.Meta.STATUS == "0" | MaterialStock.Meta.STATUS == "2"), MaterialStock.Meta.MSN.ASC);
            }
            else
            {
                if (string.IsNullOrEmpty(workOrder))
                {
                    //查询工单运行中，且发料数量为0的工单。
                    IList<WorkOrder> wos = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == "1" & WorkOrder.Meta.MaterialQty < 1, WorkOrder.Meta.UpdatedDate.DESC);
                    if (wos.Count > 0)
                    {
                        sql = "SELECT COUNT(*) FROM MES_MASTER.MATERIAL_STOCK WHERE (STATUS='0' OR STATUS='2') AND MATERIAL_NAME='" + wos[0].PartsdrawingCode + "' ORDER BY MSN ASC                  ";
                        // return DBContext.FindArray<MaterialStock>(MaterialStock.Meta.MaterialName == wos[0].PartsdrawingCode & (MaterialStock.Meta.STATUS == "0" | MaterialStock.Meta.STATUS == "2"), MaterialStock.Meta.MSN.ASC);
                    }
                }

            }
            if (!string.IsNullOrEmpty(sql))
            {
                string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
                int iCount = 0;
                if (!string.IsNullOrEmpty(count))
                {
                    iCount = Convert.ToInt32(count);
                }

                return iCount;
            }
            else
            {
                return 0;
            }


        }

        public IList<MaterialStock> FindPreparedMar(string rows, string page, string workOrder)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == workOrder);
            string sql = "";
            if (wo != null)
            {
                sql = "SELECT * FROM (SELECT * FROM( SELECT MS.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"  FROM MES_MASTER.MATERIAL_STOCK MS WHERE (STATUS='0' OR STATUS='2')  AND MATERIAL_NAME='" + wo.PartsdrawingCode + "' ORDER BY MSN ASC     ) P  WHERE P.\"row_number\" >= {0}) WHERE ROWNUM <= {1}";
                //return DBContext.FindArray<MaterialStock>(MaterialStock.Meta.MaterialName == wo.PartsdrawingCode & (MaterialStock.Meta.STATUS == "0" | MaterialStock.Meta.STATUS == "2"), MaterialStock.Meta.MSN.ASC);
            }
            else
            {
                if (string.IsNullOrEmpty(workOrder))
                {
                    //查询工单运行中，且发料数量为0的工单。
                    IList<WorkOrder> wos = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == "1" & WorkOrder.Meta.MaterialQty < 1, WorkOrder.Meta.UpdatedDate.DESC);
                    if (wos.Count > 0)
                    {
                        sql = "SELECT * FROM (SELECT * FROM( SELECT MS.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"  FROM MES_MASTER.MATERIAL_STOCK MS WHERE (STATUS='0' OR STATUS='2') AND MATERIAL_NAME='" + wos[0].PartsdrawingCode + "' ORDER BY MSN ASC ) P  WHERE P.\"row_number\" >= {0}) WHERE ROWNUM <= {1}";
                        // return DBContext.FindArray<MaterialStock>(MaterialStock.Meta.MaterialName == wos[0].PartsdrawingCode & (MaterialStock.Meta.STATUS == "0" | MaterialStock.Meta.STATUS == "2"), MaterialStock.Meta.MSN.ASC);
                    }
                }

            }
            if (!string.IsNullOrEmpty(sql))
            {
                sql = string.Format(sql, first, iRows);
                IList<MaterialStock> list = DBContext.ExcuteSql(sql).ToBusiObjects<MaterialStock>();

                return list;
            }
            else
            {
                return new List<MaterialStock>();
            }

        }


        public int FindSendMarCount(string workOrder, string status)
        {

            string sql = "SELECT COUNT(*) FROM MES_MASTER.MATERIAL_STOCK_HISTORY WHERE   WORK_ORDER='{0}'   ";
            if (!string.IsNullOrEmpty(status))
            {
                sql = sql + " AND STATUS='" + status + "'                                                        ";
            }
            if (!string.IsNullOrEmpty(workOrder))
            {
                sql = string.Format(sql, workOrder);
            }
            else
            {
                sql = string.Format(sql, "");
            }
            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }

            return iCount;
        }

        public IList<MaterialStockHistory> FindSendMar(string rows, string page, string workOrder, string status)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            string sql = "SELECT * FROM (SELECT * FROM( SELECT MS.*,ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\" FROM MES_MASTER.MATERIAL_STOCK_HISTORY MS WHERE   WORK_ORDER='{0}' ";
            if (!string.IsNullOrEmpty(status))
            {
                sql = sql + " AND STATUS='" + status + "'                                                        ";
            }

            sql = sql + " ) P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2} ";
            if (!string.IsNullOrEmpty(workOrder))
            {
                sql = string.Format(sql, workOrder, first, iRows);
            }
            else
            {
                sql = string.Format(sql, "", first, iRows);
            }
            IList<MaterialStockHistory> list = DBContext.ExcuteSql(sql).ToBusiObjects<MaterialStockHistory>();

            return list;
        }

        public PagingResult<MaterialStockHistory> FindMaterialHistory(string workorder, string status, string msn, string materialCode, string batch, string custName, string startTime, string endTime, string rows, string page)
        {
            string sql = "SELECT * FROM MES_MASTER.MATERIAL_STOCK_HISTORY  WHERE 1=1                                    ";
            if (!string.IsNullOrEmpty(workorder))
            {
                sql = sql + " AND WORK_ORDER='" + workorder + "'                                                         ";
                //ce = (MaterialStockHistory.Meta.WorkOrder == workorder);
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (status.IndexOf('^') != -1)
                {
                    string[] strtemp = status.Split('^');
                    sql = sql + " AND (STATUS='" + strtemp[0] + "' OR STATUS='" + strtemp[1] + "')                         ";
                    //ce = (ce & (MaterialStockHistory.Meta.STATUS == strtemp[0] | MaterialStockHistory.Meta.STATUS == strtemp[1]));
                }
                else
                {
                    sql = sql + " AND STATUS='" + status + "'                                                                 ";
                    //ce = (ce & MaterialStockHistory.Meta.STATUS == status);
                }
            }

            if (!string.IsNullOrEmpty(msn))
            {
                sql = sql + " AND　MSN='" + msn + "'                                                                       ";
                // ce = (ce & MaterialStockHistory.Meta.MSN == msn);
            }

            if (!string.IsNullOrEmpty(materialCode))
            {
                sql = sql + " AND　MATERIAL_NAME='" + materialCode + "'                                                    ";
                //ce = (ce & MaterialStockHistory.Meta.MaterialName == materialCode);
            }
            if (!string.IsNullOrEmpty(custName))
            {
                sql = sql + " AND　CUST_NAME='" + custName + "'                                                            ";
                // ce = (ce & MaterialStockHistory.Meta.CustName == custName);
            }
            if (!string.IsNullOrEmpty(batch))
            {
                sql = sql + " AND　BATCH_NUMBER='" + batch + "'                                                            ";
                // ce = (ce & MaterialStockHistory.Meta.BatchNumber == batch);
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')         ";
                // ce = (ce & MaterialStockHistory.Meta.CreatedDate >= Convert.ToDateTime(startTime));
                // ce = (ce & MaterialStockHistory.Meta.CreatedDate <= Convert.ToDateTime(endTime));
            }
            else if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + " AND CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')         ";
            }
            else
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + DateTime.Now.AddDays(-7) + "','yyyy-mm-dd hh24:mi:ss')         ";
                sql = sql + " AND CREATED_DATE<=  to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')         ";
                // ce = (ce & MaterialStockHistory.Meta.CreatedDate >= DateTime.Now.AddDays(-7));
                //  ce = (ce & MaterialStockHistory.Meta.CreatedDate <= DateTime.Now);
            }
            return DBContext.ExcuteSql(sql).ToPaging<MaterialStockHistory>(Convert.ToInt32(rows), Convert.ToInt32(page), MaterialStockHistory.Meta.ID.DESC);
            // return DBContext.FindArrayByPaging<MaterialStockHistory>(ce,Convert.ToInt32(rows), Convert.ToInt32(page), MaterialStockHistory.Meta.ID.DESC);

        }

        public PagingResult<MaterialStock> FindStockInfo(string status, string materialCode, string batch, string custName, string startTime, string endTime, string rows, string page)
        {
            //ConditionExpress ce = null;
            string sql = "SELECT * FROM MES_MASTER.MATERIAL_STOCK  WHERE 1=1                                                ";

            if (!string.IsNullOrEmpty(status))
            {
                if (status.IndexOf('^') != -1)
                {
                    string[] strtemp = status.Split('^');
                    sql = sql + " AND (STATUS='" + strtemp[0] + "' OR STATUS='" + strtemp[1] + "')                         ";
                    // ce = (ce & (MaterialStock.Meta.STATUS == strtemp[0] | MaterialStock.Meta.STATUS == strtemp[1]));
                }
                else
                {
                    sql = sql + " AND STATUS='" + status + "'                                                              ";
                }
                //ce = (ce & MaterialStock.Meta.STATUS == status);

            }

            if (!string.IsNullOrEmpty(materialCode))
            {
                sql = sql + " AND　MATERIAL_NAME like '%" + materialCode + "%'                                                    ";
            }
            if (!string.IsNullOrEmpty(custName))
            {
                sql = sql + " AND　CUST_NAME='" + custName + "'                                                            ";
            }
            if (!string.IsNullOrEmpty(batch))
            {
                sql = sql + " AND　BATCH_NUMBER like '%" + batch + "%'                                                            ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')         ";
                // ce = (ce & MaterialStockHistory.Meta.CreatedDate >= Convert.ToDateTime(startTime));
                // ce = (ce & MaterialStockHistory.Meta.CreatedDate <= Convert.ToDateTime(endTime));
            }
            else if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + " AND CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')         ";
            }
            else
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + DateTime.Now.AddDays(-7) + "','yyyy-mm-dd hh24:mi:ss')         ";
                sql = sql + " AND CREATED_DATE<=  to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')         ";
                // ce = (ce & MaterialStockHistory.Meta.CreatedDate >= DateTime.Now.AddDays(-7));
                //  ce = (ce & MaterialStockHistory.Meta.CreatedDate <= DateTime.Now);
            }
            return DBContext.ExcuteSql(sql).ToPaging<MaterialStock>(Convert.ToInt32(rows), Convert.ToInt32(page), MaterialStock.Meta.MSN.DESC);
            //return DBContext.FindArrayByPaging<MaterialStock>(ce,Convert.ToInt32(rows),Convert.ToInt32(page),MaterialStock.Meta.MSN.DESC);
        }

        public PagingResult<CartonInfo> FindCartonInfo(string csn, string orderno, string user, DateTime start, DateTime end, string rows, string page)
        {
            try
            {
                ConditionExpress ce = null;
                if (!string.IsNullOrEmpty(csn))
                {
                    ce = (ce & CartonInfo.Meta.CSN == csn);
                }
                if (!string.IsNullOrEmpty(orderno))
                {
                    ce = (ce & CartonInfo.Meta.OrderNumber == orderno);
                }

                if (!string.IsNullOrEmpty(user))
                {
                    ce = (ce & FailItems.Meta.UpdatedBy == user);
                }
                if (start != null && end != null)
                {
                    ce = (ce & CartonInfo.Meta.CreatedDate >= start);
                    ce = (ce & CartonInfo.Meta.CreatedDate <= end);
                }

                return DBContext.FindArrayByPaging<CartonInfo>(ce, Convert.ToInt32(rows), Convert.ToInt32(page), CartonInfo.Meta.CreatedDate.DESC);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "FindCartonInfo");
                throw ex;
            }
        }
        //查找钦纵料号 by tony add 2017-6-19
        public string FindQMaterialByCustMaterial(string cpartcode)
        {

            if (string.IsNullOrEmpty(cpartcode))
            {
                return "";
            }
            BasMateriel bc = DBContext.Find<BasMateriel>(BasMateriel.Meta.CPARTNO == cpartcode.Trim());
            if (bc == null || string.IsNullOrEmpty(bc.QPARTNO))
            {
                return "";
            }
            return bc.QPARTNO;

        }

        public string FindWOByOrderAndPart(string orderNo, string partNo)
        {

            IList<WorkOrder> wo = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.OrderNumber == orderNo & WorkOrder.Meta.PartsdrawingCode == partNo);
            if (wo == null || wo.Count == 0)
            {
                return "ERR";
            }
            return wo[0].WO;
        }

    }

}
