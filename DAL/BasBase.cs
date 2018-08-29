using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_BASE", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class BasBase : ICloneable
    {
        #region Member Variables		
        public static BasBaseMeta Meta = new BasBaseMeta();
        #endregion

        #region constructor
        public BasBase()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CODE", SqlType = "VARCHAR2", Length = 40)]
        public string CODE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "NAME", SqlType = "VARCHAR2", Length = 100)]
        public string NAME { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SUB_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string SubCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SUB_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string SubName { set; get; }

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
            BasBase obj = new BasBase();

            obj.ID = this.ID;

            obj.CODE = this.CODE;
            obj.NAME = this.NAME;
            obj.SubCode = this.SubCode;
            obj.SubName = this.SubName;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasBase obj)
        {
            obj.ID = this.ID;
            obj.CODE = this.CODE;
            obj.NAME = this.NAME;
            obj.SubCode = this.SubCode;
            obj.SubName = this.SubName;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasBaseMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta CODE = new StringPropertyMeta("\"CODE\"");
        public StringPropertyMeta NAME = new StringPropertyMeta("\"NAME\"");
        public StringPropertyMeta SubCode = new StringPropertyMeta("\"SUB_CODE\"");
        public StringPropertyMeta SubName = new StringPropertyMeta("\"SUB_NAME\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

