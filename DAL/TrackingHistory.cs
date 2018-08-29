using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.TRACKING_HISTORY", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class TrackingHistory : ICloneable
    {
        #region Member Variables		
        public static TrackingHistoryMeta Meta = new TrackingHistoryMeta();
        #endregion

        #region constructor
        public TrackingHistory()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 80)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = false, ColumnName = "PSN", SqlType = "VARCHAR2", Length = 80)]
        public string PSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MSN", SqlType = "VARCHAR2", Length = 80)]
        public string MSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WORK_ORDER", SqlType = "VARCHAR2", Length = 80)]
        public string WorkOrder { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWING_CODE", SqlType = "VARCHAR2", Length = 200)]
        public string PartsdrawingCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTS_NAME", SqlType = "VARCHAR2", Length = 80)]
        public string PartsName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTS_CODE", SqlType = "VARCHAR2", Length = 80)]
        public string PartsCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 80)]
        public string BatchNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_NAME", SqlType = "VARCHAR2", Length = 80)]
        public string StationName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 4)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "IN_STATION_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? InStationTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "OUT_STATION_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? OutStationTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TASK_TIME", SqlType = "VARCHAR2", Length = 80)]
        public string TaskTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "EXCEPTION", SqlType = "VARCHAR2", Length = 800)]
        public string EXCEPTION { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_TYPE", SqlType = "VARCHAR2", Length = 80)]
        public string MachineType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_NAME", SqlType = "VARCHAR2", Length = 80)]
        public string MachineName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO", SqlType = "VARCHAR2", Length = 100)]
        public string MEMO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? CreatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? UpdatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_BY", SqlType = "VARCHAR2", Length = 80)]
        public string UpdatedBy { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_ID", SqlType = "VARCHAR2", Length = 200)]
        public string StationId { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "NEXT_STATION", SqlType = "VARCHAR2", Length = 200)]
        public string NextStation { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "NEXT_STATION_ID", SqlType = "VARCHAR2", Length = 200)]
        public string NextStationId { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            TrackingHistory obj = new TrackingHistory();

            obj.ID = this.ID;

            obj.PSN = this.PSN;
            obj.MSN = this.MSN;
            obj.WorkOrder = this.WorkOrder;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.PartsName = this.PartsName;
            obj.PartsCode = this.PartsCode;
            obj.BatchNumber = this.BatchNumber;
            obj.StationName = this.StationName;
            obj.QUANTITY = this.QUANTITY;
            obj.STATUS = this.STATUS;
            obj.InStationTime = this.InStationTime;
            obj.OutStationTime = this.OutStationTime;
            obj.TaskTime = this.TaskTime;
            obj.EXCEPTION = this.EXCEPTION;
            obj.MachineType = this.MachineType;
            obj.MachineName = this.MachineName;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.StationId = this.StationId;
            obj.NextStation = this.NextStation;
            obj.NextStationId = this.NextStationId;

            return obj;
        }

        public void CopyTo(TrackingHistory obj)
        {
            obj.ID = this.ID;
            obj.PSN = this.PSN;
            obj.MSN = this.MSN;
            obj.WorkOrder = this.WorkOrder;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.PartsName = this.PartsName;
            obj.PartsCode = this.PartsCode;
            obj.BatchNumber = this.BatchNumber;
            obj.StationName = this.StationName;
            obj.QUANTITY = this.QUANTITY;
            obj.STATUS = this.STATUS;
            obj.InStationTime = this.InStationTime;
            obj.OutStationTime = this.OutStationTime;
            obj.TaskTime = this.TaskTime;
            obj.EXCEPTION = this.EXCEPTION;
            obj.MachineType = this.MachineType;
            obj.MachineName = this.MachineName;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.StationId = this.StationId;
            obj.NextStation = this.NextStation;
            obj.NextStationId = this.NextStationId;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class TrackingHistoryMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta PSN = new StringPropertyMeta("\"PSN\"");
        public StringPropertyMeta MSN = new StringPropertyMeta("\"MSN\"");
        public StringPropertyMeta WorkOrder = new StringPropertyMeta("\"WORK_ORDER\"");
        public StringPropertyMeta PartsdrawingCode = new StringPropertyMeta("\"PARTSDRAWING_CODE\"");
        public StringPropertyMeta PartsName = new StringPropertyMeta("\"PARTS_NAME\"");
        public StringPropertyMeta PartsCode = new StringPropertyMeta("\"PARTS_CODE\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public StringPropertyMeta StationName = new StringPropertyMeta("\"STATION_NAME\"");
        public PropertyMeta QUANTITY = new PropertyMeta("\"QUANTITY\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public DatetimePropertyMeta InStationTime = new DatetimePropertyMeta("\"IN_STATION_TIME\"");
        public DatetimePropertyMeta OutStationTime = new DatetimePropertyMeta("\"OUT_STATION_TIME\"");
        public StringPropertyMeta TaskTime = new StringPropertyMeta("\"TASK_TIME\"");
        public StringPropertyMeta EXCEPTION = new StringPropertyMeta("\"EXCEPTION\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta MachineName = new StringPropertyMeta("\"MACHINE_NAME\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta StationId = new StringPropertyMeta("\"STATION_ID\"");
        public StringPropertyMeta NextStation = new StringPropertyMeta("\"NEXT_STATION\"");
        public StringPropertyMeta NextStationId = new StringPropertyMeta("\"NEXT_STATION_ID\"");
    }
    #endregion
}

