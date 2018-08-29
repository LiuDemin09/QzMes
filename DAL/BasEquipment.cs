using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_EQUIPMENT", IsView = false, PrimaryKeys = "CODE",
    PrimaryProperties = "CODE")]
    public class BasEquipment : ICloneable
    {
        #region Member Variables		
        public static BasEquipmentMeta Meta = new BasEquipmentMeta();
        #endregion

        #region constructor
        public BasEquipment()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = false, ColumnName = "CODE", SqlType = "VARCHAR2", Length = 100)]
        public string CODE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "COMPANY", SqlType = "VARCHAR2", Length = 200)]
        public string COMPANY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string MachineName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string MachineType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "AXIS_NUMBER", SqlType = "VARCHAR2", Length = 20)]
        public string AxisNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MODEL", SqlType = "VARCHAR2", Length = 40)]
        public string MODEL { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "POWER", SqlType = "VARCHAR2", Length = 20)]
        public string POWER { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "LOCATION", SqlType = "VARCHAR2", Length = 100)]
        public string LOCATION { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "VARCHAR2", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "OUT_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string OutCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "USE_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? UseDate { set; get; }

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
        AllowNull = true, ColumnName = "UPDATED_BY", SqlType = "VARCHAR2", Length = 20)]
        public string UpdatedBy { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            BasEquipment obj = new BasEquipment();


            obj.CODE = this.CODE;
            obj.COMPANY = this.COMPANY;
            obj.MachineName = this.MachineName;
            obj.MachineType = this.MachineType;
            obj.AxisNumber = this.AxisNumber;
            obj.MODEL = this.MODEL;
            obj.POWER = this.POWER;
            obj.LOCATION = this.LOCATION;
            obj.STATUS = this.STATUS;
            obj.OutCode = this.OutCode;
            obj.UseDate = this.UseDate;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasEquipment obj)
        {
            obj.CODE = this.CODE;
            obj.COMPANY = this.COMPANY;
            obj.MachineName = this.MachineName;
            obj.MachineType = this.MachineType;
            obj.AxisNumber = this.AxisNumber;
            obj.MODEL = this.MODEL;
            obj.POWER = this.POWER;
            obj.LOCATION = this.LOCATION;
            obj.STATUS = this.STATUS;
            obj.OutCode = this.OutCode;
            obj.UseDate = this.UseDate;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasEquipmentMeta
    {
        public StringPropertyMeta CODE = new StringPropertyMeta("\"CODE\"");
        public StringPropertyMeta COMPANY = new StringPropertyMeta("\"COMPANY\"");
        public StringPropertyMeta MachineName = new StringPropertyMeta("\"MACHINE_NAME\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta AxisNumber = new StringPropertyMeta("\"AXIS_NUMBER\"");
        public StringPropertyMeta MODEL = new StringPropertyMeta("\"MODEL\"");
        public StringPropertyMeta POWER = new StringPropertyMeta("\"POWER\"");
        public StringPropertyMeta LOCATION = new StringPropertyMeta("\"LOCATION\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta OutCode = new StringPropertyMeta("\"OUT_CODE\"");
        public DatetimePropertyMeta UseDate = new DatetimePropertyMeta("\"USE_DATE\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

