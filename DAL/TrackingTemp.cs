using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.TRACKING_TEMP", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class TrackingTemp : ICloneable
    {
        #region Member Variables		
        public static TrackingTempMeta Meta = new TrackingTempMeta();
        #endregion

        #region constructor
        public TrackingTemp()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 80)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PSN", SqlType = "VARCHAR2", Length = 80)]
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
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "VARCHAR2", Length = 80)]
        public string QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 210)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STEP", SqlType = "VARCHAR2", Length = 80)]
        public string STEP { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "IN_STATION_TIME", SqlType = "VARCHAR2", Length = 800)]
        public string InStationTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "OUT_STATION_TIME", SqlType = "VARCHAR2", Length = 800)]
        public string OutStationTime { set; get; }

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
        AllowNull = true, ColumnName = "NEXT_EMP", SqlType = "VARCHAR2", Length = 80)]
        public string NextEmp { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_ID", SqlType = "VARCHAR2", Length = 200)]
        public string StationId { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            TrackingTemp obj = new TrackingTemp();

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
            obj.STEP = this.STEP;
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
            obj.NextEmp = this.NextEmp;
            obj.StationId = this.StationId;

            return obj;
        }

        public void CopyTo(TrackingTemp obj)
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
            obj.STEP = this.STEP;
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
            obj.NextEmp = this.NextEmp;
            obj.StationId = this.StationId;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class TrackingTempMeta
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
        public StringPropertyMeta QUANTITY = new StringPropertyMeta("\"QUANTITY\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta STEP = new StringPropertyMeta("\"STEP\"");
        public StringPropertyMeta InStationTime = new StringPropertyMeta("\"IN_STATION_TIME\"");
        public StringPropertyMeta OutStationTime = new StringPropertyMeta("\"OUT_STATION_TIME\"");
        public StringPropertyMeta TaskTime = new StringPropertyMeta("\"TASK_TIME\"");
        public StringPropertyMeta EXCEPTION = new StringPropertyMeta("\"EXCEPTION\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta MachineName = new StringPropertyMeta("\"MACHINE_NAME\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta NextEmp = new StringPropertyMeta("\"NEXT_EMP\"");
        public StringPropertyMeta StationId = new StringPropertyMeta("\"STATION_ID\"");
    }
    #endregion
}

