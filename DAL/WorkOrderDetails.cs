using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.WORK_ORDER_DETAILS", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class WorkOrderDetails : ICloneable
    {
        #region Member Variables		
        public static WorkOrderDetailsMeta Meta = new WorkOrderDetailsMeta();
        #endregion

        #region constructor
        public WorkOrderDetails()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = false, ColumnName = "WO", SqlType = "VARCHAR2", Length = 40)]
        public string WO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ORDER_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string OrderNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWING_CODE", SqlType = "VARCHAR2", Length = 100)]
        public string PartsdrawingCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS_MEMO", SqlType = "VARCHAR2", Length = 40)]
        public string StatusMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string MachineType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACTYPE_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string MactypeCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string MachineCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string MachineName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string ProductCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "START_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? StartTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "END_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? EndTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 60)]
        public string BatchNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PLAN_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? PlanQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MATERIAL_QTY", SqlType = "NUMBER", Length = 0)]
        public decimal? MaterialQty { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CHECK_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? CheckTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "INSTOCK_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? InstockTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WORKER", SqlType = "VARCHAR2", Length = 40)]
        public string WORKER { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WORKER_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string WorkerName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 60)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BASE_QTY", SqlType = "NUMBER", Length = 0)]
        public decimal? BaseQty { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUALITY_CODE", SqlType = "VARCHAR2", Length = 400)]
        public string QualityCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UNIT_TIME", SqlType = "NUMBER", Length = 0)]
        public decimal? UnitTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CLASS", SqlType = "VARCHAR2", Length = 100)]
        public string CLASS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ROUTE_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string RouteCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string StationName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO", SqlType = "VARCHAR2", Length = 200)]
        public string MEMO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? CreatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? UpdatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_BY", SqlType = "VARCHAR2", Length = 40)]
        public string UpdatedBy { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            WorkOrderDetails obj = new WorkOrderDetails();

            obj.ID = this.ID;

            obj.WO = this.WO;
            obj.OrderNumber = this.OrderNumber;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.MachineCode = this.MachineCode;
            obj.MachineName = this.MachineName;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.StartTime = this.StartTime;
            obj.EndTime = this.EndTime;
            obj.BatchNumber = this.BatchNumber;
            obj.PlanQuantity = this.PlanQuantity;
            obj.QUANTITY = this.QUANTITY;
            obj.MaterialQty = this.MaterialQty;
            obj.CheckTime = this.CheckTime;
            obj.InstockTime = this.InstockTime;
            obj.WORKER = this.WORKER;
            obj.WorkerName = this.WorkerName;
            obj.CustName = this.CustName;
            obj.BaseQty = this.BaseQty;
            obj.QualityCode = this.QualityCode;
            obj.UnitTime = this.UnitTime;
            obj.CLASS = this.CLASS;
            obj.RouteCode = this.RouteCode;
            obj.StationName = this.StationName;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(WorkOrderDetails obj)
        {
            obj.ID = this.ID;
            obj.WO = this.WO;
            obj.OrderNumber = this.OrderNumber;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.MachineCode = this.MachineCode;
            obj.MachineName = this.MachineName;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.StartTime = this.StartTime;
            obj.EndTime = this.EndTime;
            obj.BatchNumber = this.BatchNumber;
            obj.PlanQuantity = this.PlanQuantity;
            obj.QUANTITY = this.QUANTITY;
            obj.MaterialQty = this.MaterialQty;
            obj.CheckTime = this.CheckTime;
            obj.InstockTime = this.InstockTime;
            obj.WORKER = this.WORKER;
            obj.WorkerName = this.WorkerName;
            obj.CustName = this.CustName;
            obj.BaseQty = this.BaseQty;
            obj.QualityCode = this.QualityCode;
            obj.UnitTime = this.UnitTime;
            obj.CLASS = this.CLASS;
            obj.RouteCode = this.RouteCode;
            obj.StationName = this.StationName;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class WorkOrderDetailsMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta WO = new StringPropertyMeta("\"WO\"");
        public StringPropertyMeta OrderNumber = new StringPropertyMeta("\"ORDER_NUMBER\"");
        public StringPropertyMeta PartsdrawingCode = new StringPropertyMeta("\"PARTSDRAWING_CODE\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta StatusMemo = new StringPropertyMeta("\"STATUS_MEMO\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta MactypeCode = new StringPropertyMeta("\"MACTYPE_CODE\"");
        public StringPropertyMeta MachineCode = new StringPropertyMeta("\"MACHINE_CODE\"");
        public StringPropertyMeta MachineName = new StringPropertyMeta("\"MACHINE_NAME\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta ProductCode = new StringPropertyMeta("\"PRODUCT_CODE\"");
        public DatetimePropertyMeta StartTime = new DatetimePropertyMeta("\"START_TIME\"");
        public DatetimePropertyMeta EndTime = new DatetimePropertyMeta("\"END_TIME\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public PropertyMeta PlanQuantity = new PropertyMeta("\"PLAN_QUANTITY\"");
        public PropertyMeta QUANTITY = new PropertyMeta("\"QUANTITY\"");
        public PropertyMeta MaterialQty = new PropertyMeta("\"MATERIAL_QTY\"");
        public DatetimePropertyMeta CheckTime = new DatetimePropertyMeta("\"CHECK_TIME\"");
        public DatetimePropertyMeta InstockTime = new DatetimePropertyMeta("\"INSTOCK_TIME\"");
        public StringPropertyMeta WORKER = new StringPropertyMeta("\"WORKER\"");
        public StringPropertyMeta WorkerName = new StringPropertyMeta("\"WORKER_NAME\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public PropertyMeta BaseQty = new PropertyMeta("\"BASE_QTY\"");
        public StringPropertyMeta QualityCode = new StringPropertyMeta("\"QUALITY_CODE\"");
        public PropertyMeta UnitTime = new PropertyMeta("\"UNIT_TIME\"");
        public StringPropertyMeta CLASS = new StringPropertyMeta("\"CLASS\"");
        public StringPropertyMeta RouteCode = new StringPropertyMeta("\"ROUTE_CODE\"");
        public StringPropertyMeta StationName = new StringPropertyMeta("\"STATION_NAME\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

