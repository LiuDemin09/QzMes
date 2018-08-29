using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_SEQUENCE", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class BasSequence : ICloneable
    {
        #region Member Variables		
        public static BasSequenceMeta Meta = new BasSequenceMeta();
        #endregion

        #region constructor
        public BasSequence()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 20)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SEQ_NAME", SqlType = "VARCHAR2", Length = 20)]
        public string SeqName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAMILY", SqlType = "VARCHAR2", Length = 20)]
        public string FAMILY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DIGITAL_LEN", SqlType = "NUMBER", Length = 0)]
        public decimal? DigitalLen { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DIGITAL_TYPE", SqlType = "VARCHAR2", Length = 10)]
        public string DigitalType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
       AllowNull = true, ColumnName = "DIGITAL_TYPE_MEMO", SqlType = "VARCHAR2", Length = 20)]
        public string DigitalTypeMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "INCREASE_MODE", SqlType = "VARCHAR2", Length = 10)]
        public string IncreaseMode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
       AllowNull = true, ColumnName = "INCREASE_MODE_MEMO", SqlType = "VARCHAR2", Length = 20)]
        public string IncreaseModeMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CURRENT_NO", SqlType = "NUMBER", Length = 0)]
        public decimal? CurrentNo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BU", SqlType = "VARCHAR2", Length = 10)]
        public string BU { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SITE", SqlType = "VARCHAR2", Length = 10)]
        public string SITE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? CreatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? UpdatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_BY", SqlType = "VARCHAR2", Length = 20)]
        public string UpdatedBy { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO", SqlType = "NVARCHAR2", Length = 300)]
        public string MEMO { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            BasSequence obj = new BasSequence();


            obj.ID = this.ID;
            obj.SeqName = this.SeqName;
            obj.FAMILY = this.FAMILY;
            obj.DigitalLen = this.DigitalLen;
            obj.DigitalType = this.DigitalType;
            obj.DigitalTypeMemo = this.DigitalTypeMemo;
            obj.IncreaseMode = this.IncreaseMode;
            obj.IncreaseModeMemo = this.IncreaseModeMemo;
            obj.CurrentNo = this.CurrentNo;
            obj.BU = this.BU;
            obj.SITE = this.SITE;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.MEMO = this.MEMO;

            return obj;
        }

        public void CopyTo(BasSequence obj)
        {
            obj.ID = this.ID;
            obj.SeqName = this.SeqName;
            obj.FAMILY = this.FAMILY;
            obj.DigitalLen = this.DigitalLen;
            obj.DigitalType = this.DigitalType;
            obj.DigitalTypeMemo = this.DigitalTypeMemo;
            obj.IncreaseMode = this.IncreaseMode;
            obj.IncreaseModeMemo = this.IncreaseModeMemo;
            obj.CurrentNo = this.CurrentNo;
            obj.BU = this.BU;
            obj.SITE = this.SITE;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.MEMO = this.MEMO;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasSequenceMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta SeqName = new StringPropertyMeta("\"SEQ_NAME\"");
        public StringPropertyMeta FAMILY = new StringPropertyMeta("\"FAMILY\"");
        public PropertyMeta DigitalLen = new PropertyMeta("\"DIGITAL_LEN\"");
        public StringPropertyMeta DigitalType = new StringPropertyMeta("\"DIGITAL_TYPE\"");
        public StringPropertyMeta DigitalTypeMemo = new StringPropertyMeta("\"DIGITAL_TYPE_MEMO\"");
        public StringPropertyMeta IncreaseMode = new StringPropertyMeta("\"INCREASE_MODE\"");
        public StringPropertyMeta IncreaseModeMemo = new StringPropertyMeta("\"INCREASE_MODE_MEMO\"");
        public PropertyMeta CurrentNo = new PropertyMeta("\"CURRENT_NO\"");
        public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
        public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
    }
    #endregion
}

