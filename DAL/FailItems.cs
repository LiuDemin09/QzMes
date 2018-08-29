using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.FAIL_ITEMS", IsView = false, PrimaryKeys = "FAIL_CODE",
    PrimaryProperties = "FailCode")]
    public class FailItems : ICloneable
    {
        #region Member Variables		
        public static FailItemsMeta Meta = new FailItemsMeta();
        #endregion

        #region constructor
        public FailItems()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "FAIL_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string FailCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAIL_TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string FailType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAIL_MEMO", SqlType = "VARCHAR2", Length = 400)]
        public string FailMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO1", SqlType = "VARCHAR2", Length = 100)]
        public string MEMO1 { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO2", SqlType = "VARCHAR2", Length = 100)]
        public string MEMO2 { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO3", SqlType = "VARCHAR2", Length = 100)]
        public string MEMO3 { set; get; }

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
            FailItems obj = new FailItems();

            obj.FailCode = this.FailCode;

            obj.FailType = this.FailType;
            obj.FailMemo = this.FailMemo;
            obj.MEMO1 = this.MEMO1;
            obj.MEMO2 = this.MEMO2;
            obj.MEMO3 = this.MEMO3;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(FailItems obj)
        {
            obj.FailCode = this.FailCode;
            obj.FailType = this.FailType;
            obj.FailMemo = this.FailMemo;
            obj.MEMO1 = this.MEMO1;
            obj.MEMO2 = this.MEMO2;
            obj.MEMO3 = this.MEMO3;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class FailItemsMeta
    {
        public StringPropertyMeta FailCode = new StringPropertyMeta("\"FAIL_CODE\"");
        public StringPropertyMeta FailType = new StringPropertyMeta("\"FAIL_TYPE\"");
        public StringPropertyMeta FailMemo = new StringPropertyMeta("\"FAIL_MEMO\"");
        public StringPropertyMeta MEMO1 = new StringPropertyMeta("\"MEMO1\"");
        public StringPropertyMeta MEMO2 = new StringPropertyMeta("\"MEMO2\"");
        public StringPropertyMeta MEMO3 = new StringPropertyMeta("\"MEMO3\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

