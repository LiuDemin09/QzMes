using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_BANCI", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class BasBanci : ICloneable
    {
        #region Member Variables		
        public static BasBanciMeta Meta = new BasBanciMeta();
        #endregion

        #region constructor
        public BasBanci()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "NAME", SqlType = "VARCHAR2", Length = 40)]
        public string NAME { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "START_TIME", SqlType = "VARCHAR2", Length = 40)]
        public string StartTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "END_TIME", SqlType = "VARCHAR2", Length = 40)]
        public string EndTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DURATION", SqlType = "NUMBER", Length = 0)]
        public decimal? DURATION { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO", SqlType = "VARCHAR2", Length = 100)]
        public string MEMO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ACTIVE", SqlType = "CHAR", Length = 2)]
        public string ACTIVE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? CreatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? UpdatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_BY", SqlType = "VARCHAR2", Length = 100)]
        public string UpdatedBy { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "START_RESTTIME", SqlType = "VARCHAR2", Length = 40)]
        public string StartResttime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "END_RESTTIME", SqlType = "VARCHAR2", Length = 40)]
        public string EndResttime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DURATION_REST", SqlType = "NUMBER", Length = 0)]
        public decimal? DurationRest { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CODE", SqlType = "VARCHAR2", Length = 40)]
        public string CODE { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            BasBanci obj = new BasBanci();

            obj.ID = this.ID;

            obj.NAME = this.NAME;
            obj.StartTime = this.StartTime;
            obj.EndTime = this.EndTime;
            obj.DURATION = this.DURATION;
            obj.MEMO = this.MEMO;
            obj.ACTIVE = this.ACTIVE;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.StartResttime = this.StartResttime;
            obj.EndResttime = this.EndResttime;
            obj.DurationRest = this.DurationRest;
            obj.CODE = this.CODE;

            return obj;
        }

        public void CopyTo(BasBanci obj)
        {
            obj.ID = this.ID;
            obj.NAME = this.NAME;
            obj.StartTime = this.StartTime;
            obj.EndTime = this.EndTime;
            obj.DURATION = this.DURATION;
            obj.MEMO = this.MEMO;
            obj.ACTIVE = this.ACTIVE;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.StartResttime = this.StartResttime;
            obj.EndResttime = this.EndResttime;
            obj.DurationRest = this.DurationRest;
            obj.CODE = this.CODE;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasBanciMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta NAME = new StringPropertyMeta("\"NAME\"");
        public StringPropertyMeta StartTime = new StringPropertyMeta("\"START_TIME\"");
        public StringPropertyMeta EndTime = new StringPropertyMeta("\"END_TIME\"");
        public PropertyMeta DURATION = new PropertyMeta("\"DURATION\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public StringPropertyMeta ACTIVE = new StringPropertyMeta("\"ACTIVE\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta StartResttime = new StringPropertyMeta("\"START_RESTTIME\"");
        public StringPropertyMeta EndResttime = new StringPropertyMeta("\"END_RESTTIME\"");
        public PropertyMeta DurationRest = new PropertyMeta("\"DURATION_REST\"");
        public StringPropertyMeta CODE = new StringPropertyMeta("\"CODE\"");
    }
    #endregion
}

