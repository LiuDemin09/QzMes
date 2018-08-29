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

namespace BLL
{
    public class OrderBO : BOBase
    {
        string tplPath = "";
        public OrderBO(UserInfo userInfo)
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

        public IList<WorkOrder> FindMachineWOInfo()
        {
            string sql = @"SELECT WO.MACHINE_NAME,WO.PRODUCT_NAME,WO.PLAN_QUANTITY,WO.WO,WO.ORDER_NUMBER,                                                                                    ";
            sql = sql + "NVL((select  count(*) from MES_MASTER.TRACKING_HISTORY th where TH.STATION_NAME='CHEXI' and TH.WORK_ORDER=WO.WO and TH.STATUS='P' group by status) ,0) QUANTITY,    ";
            sql = sql + "NVL((select  count(*) from MES_MASTER.TRACKING_HISTORY th where TH.STATION_NAME='CHEXI' and TH.WORK_ORDER=WO.WO and TH.STATUS='F' group by status),0) MATERIAL_QTY  ";
            sql = sql + "FROM MES_MASTER.WORK_ORDER WO   WHERE to_char(wo.updated_date,'yyyy-mm-dd')=to_char(sysdate,'yyyy-mm-dd')                                                           ";
            IList<WorkOrder> ps = DBContext.ExcuteSql(sql).ToBusiObjects<WorkOrder>();

            return ps;

        }

        public DataSet FindOrderCreate()
        {

            string sql = @"SELECT MO.ORDER_NO,MO.PARTSDRAWING_CODE,PC.CUST_NAME,PC.CUST_CODE,PC.PLAN_QUANTITY,                                            ";
            sql = sql + "PC.QUALITY_CODE,PC.ASK_QUANTITY,PC.BATCH_NUMBER,PC.ASK_DATE,MO.UPDATED_BY,MO.CREATED_DATE                                        ";
            sql = sql + "FROM MES_MASTER.ORDER_DETAIL MO LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC                                                        ";
            sql = sql + " ON MO.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE MO.STATUS = '{0}'                                                                   ";
            sql = string.Format(sql, 0);

            return DBContext.ExcuteSql(sql).ToDataSet();
        }

        public DataSet FindUnderPublishOrder(string orderNo, string partsdrawing, string startTime, string endTime)
        {

            string sql = @"SELECT MO.ORDER_NO,MO.PARTSDRAWING_CODE,PC.CUST_NAME,PC.CUST_CODE,PC.PLAN_QUANTITY,                                            ";
            sql = sql + "PC.QUALITY_CODE,PC.ASK_QUANTITY,PC.BATCH_NUMBER,PC.ASK_DATE,MO.UPDATED_BY,MO.CREATED_DATE                                        ";
            sql = sql + "FROM MES_MASTER.ORDER_DETAIL MO LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC                                                        ";
            sql = sql + "ON MO.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE MO.STATUS = '{0}'                                                                    ";
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + " AND MO.ORDER_NO='" + orderNo + "' ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + " AND MO.PARTSDRAWING_CODE='" + partsdrawing + "' ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + " AND MO.UPDATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi')  ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + " AND MO.UPDATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi')  ";
            }
            sql = string.Format(sql, 0);

            return DBContext.ExcuteSql(sql).ToDataSet();
        }

        public string PublishOrder(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return "请选择一个订单进行发布";
            }
            string[] publishCount = obj.Split(';');
            if (publishCount == null || publishCount.Length == 0)
            {
                return "请选择一个订单进行发布";
            }
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                foreach (string key in publishCount)
                {
                    //string[] objs = key.Split(',');
                    //string orderNo = objs[0];
                    //string partsdrawing = objs[1];
                    //OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == orderNo & OrderDetail.Meta.PartsdrawingCode == partsdrawing);
                    //OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == orderNo);
                    OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.ID == key);
                    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    od.STATUS = "1";
                    od.MEMO = "发布";
                    od.UpdatedBy = this.UserCode;
                    od.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<OrderDetail>(trans, od);
                    if (oh != null)
                    {
                        oh.STATUS = "1";
                        oh.MEMO = "发布";
                        oh.UpdatedBy = this.UserCode;
                        oh.UpdatedDate = DateTime.Now;
                        DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                    }
                    OrderHistory oHistory = new OrderHistory();

                    oHistory.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                    oHistory.OrderNo = od.OrderNo;
                    oHistory.CustName = od.CustName;
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
                    DBContext.SaveAndUpdate<OrderHistory>(trans, oHistory);
                }

                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "PublishOrder");
                return ex.Message;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }



        }

        public IList<OrderDetail> FindOrder(string orderNo, string partsdrawing, string startTime, string endTime)
        {
            string sql = "";
            if (string.IsNullOrEmpty(orderNo) & string.IsNullOrEmpty(partsdrawing) & string.IsNullOrEmpty(startTime) & string.IsNullOrEmpty(endTime))
            {

                sql = "SELECT * FROM MES_MASTER.ORDER_DETAIL  WHERE  to_char(CREATED_DATE,'yyyy-mm-dd')=to_char(sysdate,'yyyy-mm-dd')                  ";
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.ORDER_DETAIL WHERE  ORDER_NO='" + orderNo + "'                                                     ";
                }
                sql = sql + "AND ORDER_NO='" + orderNo + "'                                                                                               ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.ORDER_DETAIL WHERE  PARTSDRAWING_CODE='" + partsdrawing + "'                                       ";
                }
                sql = sql + "AND PARTSDRAWING_CODE='" + partsdrawing + "'                                                                              ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.ORDER_DETAIL WHERE  CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi')               ";
                }
                sql = sql + "AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi')                                                      ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.ORDER_DETAIL WHERE  CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi')                  ";
                }
                sql = sql + "AND CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi')                                                         ";
            }


            return DBContext.ExcuteSql(sql).ToBusiObjects<OrderDetail>(); ;
        }


        public IList<WorkOrder> FindWOOrder(string woNo, string partsdrawing, string startTime, string endTime)
        {
            string sql = "";
            if (string.IsNullOrEmpty(woNo) & string.IsNullOrEmpty(partsdrawing) & string.IsNullOrEmpty(startTime) & string.IsNullOrEmpty(endTime))
            {

                sql = "SELECT * FROM MES_MASTER.WORK_ORDER  WHERE  to_char(CREATED_DATE,'yyyy-mm-dd')=to_char(sysdate,'yyyy-mm-dd')                  ";
            }
            if (!string.IsNullOrEmpty(woNo))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.WORK_ORDER WHERE  WO='" + woNo + "'                                                     ";
                }
                sql = sql + "AND WO='" + woNo + "'                                                                                               ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.WORK_ORDER WHERE  PARTSDRAWING_CODE='" + partsdrawing + "'                                       ";
                }
                sql = sql + "AND PARTSDRAWING_CODE='" + partsdrawing + "'                                                                              ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.WORK_ORDER WHERE  CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi')               ";
                }
                sql = sql + "AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi')                                                      ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT * FROM MES_MASTER.WORK_ORDER WHERE  CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi')                  ";
                }
                sql = sql + "AND CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi')                                                         ";
            }


            return DBContext.ExcuteSql(sql).ToBusiObjects<WorkOrder>(); ;
        }

        public int FindMyOrderCount(string orderNo, string partsdrawing,string productname="")
        {
            string sql = @"SELECT COUNT(*) FROM MES_MASTER.ORDER_DETAIL  WHERE CREATED_BY='{0}'             ";
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND ORDER_NO='" + orderNo + "'                                              ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND PARTSDRAWING_CODE='" + partsdrawing + "'                                ";
            //}
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND ORDER_NO LIKE '%" + orderNo + "%'                                              ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND PARTSDRAWING_CODE LIKE '%" + partsdrawing + "%'                                ";
            }
            if (!string.IsNullOrEmpty(productname))
            {
                sql = sql + "AND PRODUCT_NAME LIKE '%" + productname + "%'                                ";
            }
            sql = string.Format(sql, this.UserCode);
            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }
            return iCount;
        }

        public IList<OrderDetail> FindMyOrder(string orderNo, string partsdrawing, string rows, string page)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            string sql = @"SELECT * FROM (SELECT * FROM(                                                                ";
            //sql = sql + "SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,OD.CUST_NAME,OD.PRODUCT_NAME,                ";
            sql = sql + "SELECT OD.ID, OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,OD.CUST_NAME,OD.PRODUCT_NAME,                ";
            sql = sql + "OD.CONTRACT,OD.ORDER_QUANTITY,OD.BATCH_NUMBER,OD.OUT_DATE,OD.UPDATED_BY,OD.PRODUCT_CODE,OD.CREATED_BY,OD.CREATED_DATE,                       ";//by tony modify 2017-6-5
            sql = sql + "OD.UPDATED_DATE,OD.check_time,OD.MEMO1, ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"                            ";
            sql = sql + "FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC                      ";
            // sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE OD.CREATED_BY='{0}'                            ";
            sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER WHERE OD.CREATED_BY='{0}'                            ";
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND OD.ORDER_NO='" + orderNo + "'                                              ";
            //}
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND OD.ORDER_NO LIKE '%" + orderNo + "%'                                              ";
            }

            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND OD.PARTSDRAWING_CODE like '%" + partsdrawing + "%'                                ";//by tony modify 2017-6-16 支持模糊查询
            }
            sql = sql + ") P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2}    order by created_date desc";

            sql = string.Format(sql, this.UserCode, first, iRows);
            IList<OrderDetail> ods = DBContext.ExcuteSql(sql).ToBusiObjects<OrderDetail>();
            if (ods != null && ods.Count > 0)
            {
                for (int i = 0; i < ods.Count; i++)
                {
                    if (ods[i].STATUS == "0")
                    {
                        ods[i].STATUS = "创建";
                    }
                    else if (ods[i].STATUS == "1")
                    {
                        ods[i].STATUS = "发布";
                    }
                    else if (ods[i].STATUS == "2")
                    {
                        ods[i].STATUS = "发货通知";
                    }
                    else if (ods[i].STATUS == "3")
                    {
                        ods[i].STATUS = "关闭";
                    }
                    else if (ods[i].STATUS == "4")
                    {
                        ods[i].STATUS = "驳回";
                    }

                }
            }

            return ods;
            //int iRows = Convert.ToInt32(rows);
            //int iPage = Convert.ToInt32(page);
            //int first = (iRows * (iPage - 1)) + 1;
            //int last = iRows * iPage;

            //string sql = @"SELECT * FROM (SELECT * FROM(                                                                ";
            ////sql = sql + "SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,OD.CUST_NAME,OD.PRODUCT_NAME,                ";
            //sql = sql + "SELECT OD.ID, OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,OD.CUST_NAME,OD.PRODUCT_NAME,                ";
            //sql = sql + "OD.CONTRACT,OD.ORDER_QUANTITY,PC.BATCH_NUMBER,OD.OUT_DATE,OD.UPDATED_BY,OD.PRODUCT_CODE,OD.CREATED_BY,OD.CREATED_DATE,                       ";//by tony modify 2017-6-5
            //sql = sql + "OD.UPDATED_DATE, ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"                            ";
            //sql = sql + "FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC                      ";
            //// sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE OD.CREATED_BY='{0}'                            ";
            //sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER WHERE OD.CREATED_BY='{0}'                            ";
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND OD.ORDER_NO='" + orderNo + "'                                              ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND OD.PARTSDRAWING_CODE='" + partsdrawing + "'                                ";
            //}
            //sql = sql + ") P  WHERE P.\"row_number\" >= {1}) WHERE ROWNUM <= {2}                                                ";

            //sql = string.Format(sql, this.UserCode, first, last);
            //IList<OrderDetail> ods = DBContext.ExcuteSql(sql).ToBusiObjects<OrderDetail>();
            //if (ods != null && ods.Count > 0)
            //{
            //    for (int i = 0; i < ods.Count; i++)
            //    {
            //        if (ods[i].STATUS == "0")
            //        {
            //            ods[i].STATUS = "创建";
            //        }
            //        else if (ods[i].STATUS == "1")
            //        {
            //            ods[i].STATUS = "发布";
            //        }
            //        else if (ods[i].STATUS == "2")
            //        {
            //            ods[i].STATUS = "发货通知";
            //        }
            //        else if (ods[i].STATUS == "3")
            //        {
            //            ods[i].STATUS = "关闭";
            //        }

            //    }
            //}

            //return ods;
        }

        public IList<OrderDetail> FindMyOrderByNo(string orderNo, string partsdrawing)
        {
            string sql = @"SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,OD.CUST_NAME,OD.PRODUCT_NAME,  ";
            sql = sql + "OD.ORDER_QUANTITY,PC.BATCH_NUMBER,OD.OUT_DATE,OD.UPDATED_BY,OD.UPDATED_DATE        ";
            sql = sql + " FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC         ";
            sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE OD.CREATED_BY='{0}'                    ";
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND OD.ORDER_NO='" + orderNo + "'                                                          ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND OD.PARTSDRAWING_CODE='" + partsdrawing + "'                                                 ";
            }
            sql = string.Format(sql, this.UserCode);
            IList<OrderDetail> ods = DBContext.ExcuteSql(sql).ToBusiObjects<OrderDetail>();
            if (ods != null && ods.Count > 0)
            {
                for (int i = 0; i < ods.Count; i++)
                {
                    if (ods[i].STATUS == "0")
                    {
                        ods[i].STATUS = "创建";
                    }
                    else if (ods[i].STATUS == "1")
                    {
                        ods[i].STATUS = "发布";
                    }
                    else if (ods[i].STATUS == "2")
                    {
                        ods[i].STATUS = "发货通知";
                    }
                    else if (ods[i].STATUS == "3")
                    {
                        ods[i].STATUS = "关闭";
                    }

                }
            }

            return ods;
        }

        public string DelOrderInfo(string orderNo, string contract)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                if (string.IsNullOrEmpty(orderNo) || string.IsNullOrEmpty(contract))
                {
                    return "请选择一条数据进行删除";
                }
                OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == orderNo & OrderDetail.Meta.CONTRACT == contract);
                if (od == null)
                {
                    return "删除数据不存在";
                }
                if (!string.IsNullOrEmpty(od.STATUS) && od.STATUS != "0")
                {
                    return "订单已发布，无法删除";
                }
                if (!string.IsNullOrEmpty(od.CreatedBy) && od.CreatedBy != this.UserCode)
                {
                    return "无权删除该订单";
                }

                OrderDetail odOther = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == orderNo & OrderDetail.Meta.CONTRACT != contract);
                if (odOther == null)
                {
                    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == orderNo);
                    if (oh != null)
                    {
                        DBContext.Remove<OrderHead>(trans, OrderHead.Meta.OrderNo == orderNo);
                    }
                }
                else
                {
                    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == orderNo);
                    if (oh != null)
                    {
                        oh.OrderQuantity = oh.OrderQuantity - od.OrderQuantity;
                        DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                    }
                }
                DBContext.Remove<OrderDetail>(trans, OrderDetail.Meta.ID == od.ID);
                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "DelOrder");
                return ex.Message;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }

        }
        public string DelOrderInfo(string ID)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.ID == ID);
                if (od == null)
                {
                    return "删除数据不存在";
                }
                if (!string.IsNullOrEmpty(od.STATUS) && od.STATUS != "0")
                {
                    return "订单已发布，无法删除";
                }
                if (!string.IsNullOrEmpty(od.CreatedBy) && od.CreatedBy != this.UserCode)
                {
                    return "无权删除该订单";
                }

                OrderDetail odOther = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == od.OrderNo & OrderDetail.Meta.ID != ID);
                if (odOther == null)
                {
                    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    if (oh != null)
                    {
                        DBContext.Remove<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    }
                }
                else
                {
                    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    if (oh != null)
                    {
                        oh.OrderQuantity = oh.OrderQuantity - od.OrderQuantity;
                        DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                    }
                }
                DBContext.Remove<OrderDetail>(trans, OrderDetail.Meta.ID == od.ID);
                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "DelOrder");
                return ex.Message;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }

        }

        public IList<PartsdrawingCode> FindDrawing(string pdcode, string custcode, string starttime, string endtime)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(pdcode))
            {
                ce = (PartsdrawingCode.Meta.PartsCode.Like(pdcode));
            }
            if (!string.IsNullOrEmpty(custcode))
            {
                ce = (ce & PartsdrawingCode.Meta.CustCode == custcode);
            }
            if (!string.IsNullOrEmpty(starttime))
            {
                ce = (ce & PartsdrawingCode.Meta.CreatedDate >= Convert.ToDateTime(starttime));
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                ce = (ce & PartsdrawingCode.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }
            ce = (ce & PartsdrawingCode.Meta.ACTIVE == "1");
            if (ce == null)
            {
                ce = PartsdrawingCode.Meta.CreatedDate >= DateTime.Now.AddDays(-180);
                return DBContext.FindArray<PartsdrawingCode>(ce,PartsdrawingCode.Meta.CreatedDate.DESC);
            }
            return DBContext.FindArray<PartsdrawingCode>(ce,PartsdrawingCode.Meta.CreatedDate.DESC);
        }

        public int FindAllOrderCount(string orderNo, string partsdrawing, string startTime, string endTime, string status = "",string contract="")
        {
            //by tony modify 2017-6-6
            string sql = string.Empty;
            if (string.IsNullOrEmpty(status))
            {
                sql = @"SELECT COUNT(*)  FROM MES_MASTER.ORDER_DETAIL WHERE  1=1 and STATUS in ('0','1','2','3','4')  ";
            }
            else
            {
                sql = @"SELECT COUNT(*)  FROM MES_MASTER.ORDER_DETAIL WHERE  STATUS='" + status + "'";
            }
            //string sql = @"SELECT COUNT(*)  FROM MES_MASTER.ORDER_DETAIL WHERE  STATUS!='3'    
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND ORDER_NO='" + orderNo + "'                                              ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND PARTSDRAWING_CODE='" + partsdrawing + "'                                 ";
            //}
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND ORDER_NO LIKE '%" + orderNo + "%'                                              ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND PARTSDRAWING_CODE LIKE '%" + partsdrawing + "%'                                 ";
            }
            if (!string.IsNullOrEmpty(contract))
            {
                sql = sql + "AND CONTRACT LIKE '%" + contract + "%'                                 ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + "AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')                                   ";
            }
            else
            {
                sql = sql + "AND CREATED_DATE>=  to_date('" + DateTime.Now.AddDays(-30).ToString() + "','yyyy-mm-dd hh24:mi:ss')                                   ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + "AND CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')                                     ";
            }

            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }


            return iCount;
        }

        public DataSet FindAllOrder(string orderNo, string partsdrawing, string startTime, string endTime, string rows, string page, string status = "",string contract="")
        {

            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;



            string sql = @"SELECT * FROM (SELECT * FROM(                                                                                   ";
            //sql = sql + @"SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,PC.CUST_NAME,PC.CUST_CODE,OD.PRODUCT_NAME,PC.QUALITY_CODE,     ";
            sql = sql + @"SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,OD.CUST_NAME,OD.PRODUCT_NAME,     ";
            //sql = sql + "PC.ASK_QUANTITY,OD.ORDER_QUANTITY,OD.BATCH_NUMBER,OD.OUT_DATE,OD.CREATED_BY,OD.CREATED_DATE,   ";
            sql = sql + "OD.ORDER_QUANTITY,OD.BATCH_NUMBER,OD.OUT_DATE,OD.CREATED_BY,OD.CREATED_DATE,OD.MEMO1,   ";
            sql = sql + "OD.IN_QUANTITY,OD.OUT_QUANTITY,(OD.IN_QUANTITY-decode(OD.OUT_QUANTITY,null,0,OD.OUT_QUANTITY)) STOCKQTY,OD.OUT_NOTICE_QTY,OD.THIS_OUT_QTY, OD.CONTRACT ,                    ";//by tony add 2017-6-14
            sql = sql + " ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"   ";
           // sql = sql + " FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC  ";
            sql = sql + " FROM MES_MASTER.ORDER_DETAIL OD ";
            // sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE  STATUS!='3'                                                          ";
            // sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER WHERE  STATUS!='3'";
            //by tony modify 2017-6-6
            if (string.IsNullOrEmpty(status))
            {
                //sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE  WHERE  1=1 and STATUS in ('0','1','2','3','4')  and pc.active='1' ";//by tony modify 2017-8-3
                sql = sql + " WHERE  1=1 and STATUS in ('0','1','2','3','4') ";//by tony modify 2018-2-12
            }
            else
            {
                //sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE  WHERE  STATUS='" + status + "'  and pc.active='1' ";//by tony modify 2017-8-3
                sql = sql + "   WHERE  STATUS='" + status + "'   ";//by tony modify 2018-2-12
            }
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND OD.ORDER_NO='" + orderNo + "'                                                                             ";
            //}
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND OD.ORDER_NO LIKE '%" + orderNo + "%'                                                                             ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND OD.PARTSDRAWING_CODE like '%" + partsdrawing + "%'                                                               ";
            }
            if (!string.IsNullOrEmpty(contract))
            {
                sql = sql + "AND OD.CONTRACT like '%" + contract + "%'                                                               ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + "AND OD.CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')                                   ";
            }
            else
            {
                sql = sql + "AND OD.CREATED_DATE>=  to_date('" + DateTime.Now.AddDays(-30).ToString() + "','yyyy-mm-dd hh24:mi:ss')                                   ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + "AND OD.CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')                                     ";
            }
            //sql = sql + ") P  WHERE P.\"row_number\" >= {0}) WHERE ROWNUM <= {1}     order by created_date desc      ";
            sql = sql + ") P  WHERE P.\"row_number\" >= {0}) WHERE ROWNUM <= {1}     order by status desc      ";
            sql = string.Format(sql, first, iRows);
            DataSet ods = DBContext.ExcuteSql(sql).ToDataSet();
            if (ods != null && ods.Tables != null && ods.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ods.Tables[0].Rows.Count; i++)
                {
                    if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "0")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "创建";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "1")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "发布";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "2")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "发货通知";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "3")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "关闭";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "4")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "驳回";
                    }

                }
            }

            return ods;
            //int iRows = Convert.ToInt32(rows);
            //int iPage = Convert.ToInt32(page);
            //int first = (iRows * (iPage - 1)) + 1;
            //int last = iRows * iPage;
            //string sql = @"SELECT * FROM (SELECT * FROM(                                                                                   ";
            //sql = sql + @"SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,PC.CUST_NAME,PC.CUST_CODE,OD.PRODUCT_NAME,PC.QUALITY_CODE,     ";
            //sql = sql + "PC.ASK_QUANTITY,OD.ORDER_QUANTITY,PC.BATCH_NUMBER,OD.OUT_DATE,OD.CREATED_BY,OD.CREATED_DATE,                      ";
            //sql = sql + "OD.IN_QUANTITY,OD.OUT_QUANTITY,OD.OUT_NOTICE_QTY,OD.THIS_OUT_QTY,                      ";//by tony add 2017-6-14
            //sql = sql + " ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"                                                               ";
            //sql = sql + " FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC                                        ";
            //// sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE  STATUS!='3'                                                          ";
            //// sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER WHERE  STATUS!='3'";
            ////by tony modify 2017-6-6
            //if (string.IsNullOrEmpty(status))
            //{
            //    sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER WHERE  1=1 ";
            //}
            //else
            //{
            //    sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER WHERE  STATUS='" + status + "' ";
            //}
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND OD.ORDER_NO='" + orderNo + "'                                                                             ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND OD.PARTSDRAWING_CODE='" + partsdrawing + "'                                                               ";
            //}
            //if (!string.IsNullOrEmpty(startTime))
            //{
            //    sql = sql + "AND OD.CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')                                   ";
            //}
            //if (!string.IsNullOrEmpty(endTime))
            //{
            //    sql = sql + "AND OD.CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')                                     ";
            //}
            //sql = sql + ") P  WHERE P.\"row_number\" >= {0}) WHERE ROWNUM <= {1}                                                          ";
            //sql = string.Format(sql, first, last);
            //DataSet ods = DBContext.ExcuteSql(sql).ToDataSet();
            //if (ods != null && ods.Tables != null && ods.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ods.Tables[0].Rows.Count; i++)
            //    {
            //        if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "0")
            //        {
            //            ods.Tables[0].Rows[i]["STATUS"] = "创建";
            //        }
            //        else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "1")
            //        {
            //            ods.Tables[0].Rows[i]["STATUS"] = "发布";
            //        }
            //        else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "2")
            //        {
            //            ods.Tables[0].Rows[i]["STATUS"] = "发货通知";
            //        }
            //        else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "3")
            //        {
            //            ods.Tables[0].Rows[i]["STATUS"] = "关闭";
            //        }

            //    }
            //}

            //return ods;
        }

        public int FindAllPublishOrderCount(string orderNo, string partsdrawing, string startTime, string endTime)
        {
            string sql = @"SELECT COUNT(*)  FROM MES_MASTER.ORDER_DETAIL WHERE  STATUS='0'                                                     ";
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND ORDER_NO='" + orderNo + "'                                              ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND PARTSDRAWING_CODE='" + partsdrawing + "'                                 ";
            //}
             

            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND ORDER_NO LIKE '%" + orderNo + "%'                                              ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND PARTSDRAWING_CODE LIKE '%" + partsdrawing + "%'                                 ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + "AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')                                   ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + "AND CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')                                     ";
            }

            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }


            return iCount;
        }

        public DataSet FindAllPublishOrder(string orderNo, string partsdrawing, string startTime, string endTime, string rows, string page)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;



            string sql = @"SELECT * FROM (SELECT * FROM(                                                                                   ";
            // sql = sql + @"SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,PC.CUST_NAME,PC.CUST_CODE,OD.PRODUCT_NAME,PC.QUALITY_CODE,     ";
            sql = sql + @"SELECT OD.ID, OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,PC.CUST_NAME,PC.CUST_CODE,OD.PRODUCT_NAME,PC.QUALITY_CODE,     ";
            sql = sql + "PC.ASK_QUANTITY,OD.ORDER_QUANTITY,PC.BATCH_NUMBER,OD.OUT_DATE,OD.CREATED_BY,OD.CREATED_DATE,                      ";
            sql = sql + " ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"                                                               ";
            sql = sql + " FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC                                        ";
            //sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE  STATUS='0'                                                          ";
            sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER  WHERE  STATUS='0'                                                          ";
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND OD.ORDER_NO='" + orderNo + "'                                                                             ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND OD.PARTSDRAWING_CODE='" + partsdrawing + "'                                                               ";
            //}
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND OD.ORDER_NO LIKE '%" + orderNo + "%'                                                                             ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND OD.PARTSDRAWING_CODE LIKE '%" + partsdrawing + "%'                                                               ";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + "AND OD.CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')                                   ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + "AND OD.CREATED_DATE<=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')                                     ";
            }
            sql = sql + ") P  WHERE P.\"row_number\" >= {0}) WHERE ROWNUM <= {1}   order by CREATED_DATE desc     ";
            sql = string.Format(sql, first, iRows);
            DataSet ods = DBContext.ExcuteSql(sql).ToDataSet();
            if (ods != null && ods.Tables != null && ods.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ods.Tables[0].Rows.Count; i++)
                {
                    if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "0")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "创建";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "1")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "发布";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "2")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "发货通知";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "3")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "关闭";
                    }

                }
            }

            return ods;
        }




        public int FindAllSendOrderCount(string orderNo, string partsdrawing)
        {
            string sql = @"SELECT COUNT(*)  FROM MES_MASTER.ORDER_DETAIL WHERE  STATUS='1'                                                     ";
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND ORDER_NO='" + orderNo + "'                                              ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND PARTSDRAWING_CODE='" + partsdrawing + "'                                 ";
            //}
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND ORDER_NO LIKE '%" + orderNo + "%'                                              ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND PARTSDRAWING_CODE LIKE '%" + partsdrawing + "%'                                 ";
            }
            string count = DBContext.ExcuteSql(sql).ToSingle().ToString();
            int iCount = 0;
            if (!string.IsNullOrEmpty(count))
            {
                iCount = Convert.ToInt32(count);
            }


            return iCount;
        }

        public DataSet FindAllSendOrder(string orderNo, string partsdrawing, string rows, string page)
        {
            int iRows = Convert.ToInt32(rows);
            int iPage = Convert.ToInt32(page);
            int first = (iRows * (iPage - 1)) + 1;
            int last = iRows * iPage;

            string sql = @"SELECT * FROM (SELECT * FROM(                                                                                   ";
            //sql = sql + @"SELECT OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,PC.CUST_NAME,PC.CUST_CODE,OD.PRODUCT_NAME,PC.QUALITY_CODE,     ";
            sql = sql + @"SELECT OD.ID, OD.ORDER_NO,OD.PARTSDRAWING_CODE,OD.STATUS,PC.CUST_NAME,PC.CUST_CODE,OD.PRODUCT_NAME,PC.QUALITY_CODE,     ";
            sql = sql + "OD.OUT_QUANTITY,(OD.IN_QUANTITY-decode(OD.OUT_QUANTITY,null,0,OD.OUT_QUANTITY)) STOCKQTY,OD.OUT_NOTICE_QTY,PC.ASK_QUANTITY,OD.ORDER_QUANTITY,PC.BATCH_NUMBER,OD.OUT_DATE,OD.CREATED_BY,OD.CREATED_DATE,                      ";
            sql = sql + " ROW_NUMBER() OVER(ORDER BY NULL) AS \"row_number\"                                                               ";
            sql = sql + " FROM MES_MASTER.ORDER_DETAIL OD LEFT JOIN MES_MASTER.PARTSDRAWING_CODE PC                                        ";
            // sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE WHERE  STATUS='1'                                                          ";
            sql = sql + "ON OD.PARTSDRAWING_CODE=PC.PARTS_CODE and OD.BATCH_NUMBER=PC.BATCH_NUMBER  WHERE  STATUS='1'                                                          ";
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    sql = sql + "AND OD.ORDER_NO='" + orderNo + "'                                                                             ";
            //}
            //if (!string.IsNullOrEmpty(partsdrawing))
            //{
            //    sql = sql + "AND OD.PARTSDRAWING_CODE='" + partsdrawing + "'                                                               ";
            //}
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql = sql + "AND OD.ORDER_NO LIKE '%" + orderNo + "%'                                                                             ";
            }
            if (!string.IsNullOrEmpty(partsdrawing))
            {
                sql = sql + "AND OD.PARTSDRAWING_CODE LIKE '%" + partsdrawing + "%'                                                               ";
            }
            sql = sql + ") P  WHERE P.\"row_number\" >= {0}) WHERE ROWNUM <= {1}  order by created_date desc                                                         ";
            sql = string.Format(sql, first, iRows);
            DataSet ods = DBContext.ExcuteSql(sql).ToDataSet();
            if (ods != null && ods.Tables != null && ods.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ods.Tables[0].Rows.Count; i++)
                {
                    if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "0")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "创建";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "1")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "发布";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "2")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "发货通知";
                    }
                    else if (ods.Tables[0].Rows[i]["STATUS"].ToString() == "3")
                    {
                        ods.Tables[0].Rows[i]["STATUS"] = "关闭";
                    }

                }
            }

            return ods;
        }

        public string SendOrder(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return "请选择一个订单进行发货";
            }
            string[] publishCount = obj.Split(';');
            if (publishCount == null || publishCount.Length == 0)
            {
                return "请选择一个订单进行发货";
            }
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {

                foreach (string key in publishCount)
                {
                    string[] keyNum = key.Split(',');
                    int ask = Convert.ToInt32(keyNum[1]);
                    int outQty = Convert.ToInt32(keyNum[2]);
                    int qty = Convert.ToInt32(keyNum[3]);
                    DateTime sendTime = DateTime.Now;
                    try
                    {
                        sendTime = Convert.ToDateTime(keyNum[4]);
                    }
                    catch (Exception e)
                    {
                        return "发货时间不能为空且格式必须正确";
                    }
                    if (sendTime <= DateTime.Now)
                    {
                        return "发货时间必须大于当前时间";
                    }
                   // if (ask - outQty < qty)
                    if (ask< qty)
                    {
                        // return "发货通知数量有误:交付数量去掉发货数量不满足当前发货要求";
                        return "发货通知数量有误:库存数量不满足当前发货要求";
                    }
                    //string orderNo = objs[0];
                    //string partsdrawing = objs[1];
                    //OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == orderNo & OrderDetail.Meta.PartsdrawingCode == partsdrawing);
                    //OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == orderNo);
                    OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.ID == keyNum[0]);


                    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    od.STATUS = "2";
                    od.MEMO = "发货通知";
                    od.OutNoticeQty = qty;
                    od.ThisOutTime = sendTime;
                    od.UpdatedBy = this.UserCode;
                    od.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<OrderDetail>(trans, od);
                    if (oh != null)
                    {
                        oh.STATUS = "2";
                        oh.MEMO = "发货通知";
                        oh.UpdatedBy = this.UserCode;
                        oh.UpdatedDate = DateTime.Now;
                        DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                    }
                    OrderHistory oHistory = new OrderHistory();

                    oHistory.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                    oHistory.OrderNo = od.OrderNo;
                    oHistory.CustName = od.CustName;
                    oHistory.CustCode = oh.CustCode;
                    oHistory.CONTRACT = oh.CONTRACT;
                    oHistory.ProductName = od.ProductName;
                    oHistory.ProductCode = od.ProductCode;
                    oHistory.PartsdrawingCode = od.PartsdrawingCode;
                    oHistory.BatchNumber = od.BatchNumber;
                    oHistory.OrderQuantity = od.OrderQuantity;
                    oHistory.InQuantity = od.InQuantity;
                    oHistory.OutQuantity = od.OutQuantity;
                    oHistory.OutNoticeQty = od.OutNoticeQty;
                    oHistory.OutDate = od.OutDate;
                    oHistory.STATUS = od.STATUS;
                    oHistory.MEMO = od.MEMO;
                    oHistory.CreatedDate = DateTime.Now;
                    oHistory.UpdatedBy = this.UserCode;
                    oHistory.ThisOutTime = od.ThisOutTime;
                    DBContext.SaveAndUpdate<OrderHistory>(trans, oHistory);
                }

                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SendOrder");
                return ex.Message;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }

            //if (string.IsNullOrEmpty(obj))
            //{
            //    return "请选择一个订单进行发货";
            //}
            //string[] publishCount = obj.Split(';');
            //if (publishCount == null || publishCount.Length == 0)
            //{
            //    return "请选择一个订单进行发货";
            //}
            //IDbTransaction trans = DBContext.OpenTrans();
            //try
            //{

            //    foreach (string key in publishCount)
            //    {
            //        string[] keyNum = key.Split(',');
            //        int ask = Convert.ToInt32(keyNum[1]);
            //        int outQty = Convert.ToInt32(keyNum[2]);
            //        int qty = Convert.ToInt32(keyNum[3]);
            //        if (ask - outQty < qty)
            //        {
            //            return "发货通知数量有误";
            //        }
            //        //string orderNo = objs[0];
            //        //string partsdrawing = objs[1];
            //        //OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == orderNo & OrderDetail.Meta.PartsdrawingCode == partsdrawing);
            //        //OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == orderNo);
            //        OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.ID == keyNum[0]);


            //        OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
            //        od.STATUS = "2";
            //        od.MEMO = "发货通知";
            //        od.OutNoticeQty = qty;
            //        od.UpdatedBy = this.UserCode;
            //        od.UpdatedDate = DateTime.Now;
            //        DBContext.SaveAndUpdate<OrderDetail>(trans, od);
            //        if (oh != null)
            //        {
            //            oh.STATUS = "2";
            //            oh.MEMO = "发货通知";
            //            oh.UpdatedBy = this.UserCode;
            //            oh.UpdatedDate = DateTime.Now;
            //            DBContext.SaveAndUpdate<OrderHead>(trans, oh);
            //        }
            //        OrderHistory oHistory = new OrderHistory();

            //        oHistory.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            //        oHistory.OrderNo = od.OrderNo;
            //        oHistory.CustName = od.CustName;
            //        oHistory.CustCode = oh.CustCode;
            //        oHistory.CONTRACT = oh.CONTRACT;
            //        oHistory.ProductName = od.ProductName;
            //        oHistory.ProductCode = od.ProductCode;
            //        oHistory.PartsdrawingCode = od.PartsdrawingCode;
            //        oHistory.BatchNumber = od.BatchNumber;
            //        oHistory.OrderQuantity = od.OrderQuantity;
            //        oHistory.InQuantity = od.InQuantity;
            //        oHistory.OutQuantity = od.OutQuantity;
            //        oHistory.OutNoticeQty = od.OutNoticeQty;
            //        oHistory.OutDate = od.OutDate;
            //        oHistory.STATUS = od.STATUS;
            //        oHistory.MEMO = od.MEMO;
            //        oHistory.CreatedDate = DateTime.Now;
            //        oHistory.UpdatedBy = this.UserCode;
            //        DBContext.SaveAndUpdate<OrderHistory>(trans, oHistory);
            //    }

            //    trans.Commit();
            //    return "OK";
            //}
            //catch (Exception ex)
            //{
            //    trans.Rollback();
            //    PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SendOrder");
            //    return ex.Message;
            //}
            //finally
            //{
            //    DBContext.ReleaseTrans(trans);
            //}



        }
        public string RejectOrder(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return "请选择一个订单进行驳回";
            }
            string[] publishCount = obj.Split(';');
            if (publishCount == null || publishCount.Length == 0)
            {
                return "请选择一个订单进行驳回";
            }
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                foreach (string key in publishCount)
                {
                    //string[] objs = key.Split(',');
                    //string orderNo = objs[0];
                    //string partsdrawing = objs[1];
                    //OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.OrderNo == orderNo & OrderDetail.Meta.PartsdrawingCode == partsdrawing);
                    //OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == orderNo);
                    OrderDetail od = DBContext.Find<OrderDetail>(trans, OrderDetail.Meta.ID == key);
                    OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    od.STATUS = "4";
                    od.MEMO = "驳回";
                    od.UpdatedBy = this.UserCode;
                    od.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<OrderDetail>(trans, od);
                    if (oh != null)
                    {
                        oh.STATUS = "4";
                        oh.MEMO = "驳回";
                        oh.UpdatedBy = this.UserCode;
                        oh.UpdatedDate = DateTime.Now;
                        DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                    }
                    OrderHistory oHistory = new OrderHistory();

                    oHistory.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                    oHistory.OrderNo = od.OrderNo;
                    oHistory.CustName = od.CustName;
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
                    DBContext.SaveAndUpdate<OrderHistory>(trans, oHistory);
                }

                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RejectOrder");
                return ex.Message;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


    }

}
