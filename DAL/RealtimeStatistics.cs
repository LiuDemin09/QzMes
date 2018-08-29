using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.REALTIME_STATISTICS", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class RealtimeStatistics : ICloneable
    {
        #region Member Variables		
        public static RealtimeStatisticsMeta Meta = new RealtimeStatisticsMeta();
        #endregion

        #region constructor
        public RealtimeStatistics()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PSN", SqlType = "VARCHAR2", Length = 40)]
        public string PSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MSN", SqlType = "VARCHAR2", Length = 40)]
        public string MSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WORK_ORDER", SqlType = "VARCHAR2", Length = 40)]
        public string WorkOrder { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string StationName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string MachineType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string MachineName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "OPERATOR", SqlType = "VARCHAR2", Length = 40)]
        public string OPERATOR { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ORDER_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string OrderNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string ProductCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWING_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string PartsdrawingCode { set; get; }

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
            RealtimeStatistics obj = new RealtimeStatistics();

            obj.ID = this.ID;

            obj.PSN = this.PSN;
            obj.MSN = this.MSN;
            obj.WorkOrder = this.WorkOrder;
            obj.StationName = this.StationName;
            obj.MachineType = this.MachineType;
            obj.MachineName = this.MachineName;
            obj.STATUS = this.STATUS;
            obj.QUANTITY = this.QUANTITY;
            obj.OPERATOR = this.OPERATOR;
            obj.OrderNumber = this.OrderNumber;
            obj.ProductCode = this.ProductCode;
            obj.ProductName = this.ProductName;
            obj.CustName = this.CustName;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(RealtimeStatistics obj)
        {
            obj.ID = this.ID;
            obj.PSN = this.PSN;
            obj.MSN = this.MSN;
            obj.WorkOrder = this.WorkOrder;
            obj.StationName = this.StationName;
            obj.MachineType = this.MachineType;
            obj.MachineName = this.MachineName;
            obj.STATUS = this.STATUS;
            obj.QUANTITY = this.QUANTITY;
            obj.OPERATOR = this.OPERATOR;
            obj.OrderNumber = this.OrderNumber;
            obj.ProductCode = this.ProductCode;
            obj.ProductName = this.ProductName;
            obj.CustName = this.CustName;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class RealtimeStatisticsMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta PSN = new StringPropertyMeta("\"PSN\"");
        public StringPropertyMeta MSN = new StringPropertyMeta("\"MSN\"");
        public StringPropertyMeta WorkOrder = new StringPropertyMeta("\"WORK_ORDER\"");
        public StringPropertyMeta StationName = new StringPropertyMeta("\"STATION_NAME\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta MachineName = new StringPropertyMeta("\"MACHINE_NAME\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public PropertyMeta QUANTITY = new PropertyMeta("\"QUANTITY\"");
        public StringPropertyMeta OPERATOR = new StringPropertyMeta("\"OPERATOR\"");
        public StringPropertyMeta OrderNumber = new StringPropertyMeta("\"ORDER_NUMBER\"");
        public StringPropertyMeta ProductCode = new StringPropertyMeta("\"PRODUCT_CODE\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta PartsdrawingCode = new StringPropertyMeta("\"PARTSDRAWING_CODE\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

