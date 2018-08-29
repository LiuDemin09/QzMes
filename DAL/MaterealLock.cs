using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.MATEREAL_LOCK", IsView = false, PrimaryKeys = "MSN",
    PrimaryProperties = "MSN")]
    public class MaterealLock : ICloneable
    {
        #region Member Variables		
        public static MaterealLockMeta Meta = new MaterealLockMeta();
        #endregion

        #region constructor
        public MaterealLock()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "MSN", SqlType = "VARCHAR2", Length = 40)]
        public string MSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MATERIAL_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string MaterialName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MATERIAL_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string MaterialCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string BatchNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STOCK_HOUSE", SqlType = "VARCHAR2", Length = 40)]
        public string StockHouse { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DOCUMENTID", SqlType = "VARCHAR2", Length = 40)]
        public string DOCUMENTID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BAS_QTY", SqlType = "NUMBER", Length = 0)]
        public decimal? BasQty { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UNIT", SqlType = "VARCHAR2", Length = 40)]
        public string UNIT { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "LOCK_REASON", SqlType = "VARCHAR2", Length = 400)]
        public string LockReason { set; get; }

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
            MaterealLock obj = new MaterealLock();

            obj.MSN = this.MSN;

            obj.MaterialName = this.MaterialName;
            obj.MaterialCode = this.MaterialCode;
            obj.BatchNumber = this.BatchNumber;
            obj.CustName = this.CustName;
            obj.StockHouse = this.StockHouse;
            obj.DOCUMENTID = this.DOCUMENTID;
            obj.BasQty = this.BasQty;
            obj.UNIT = this.UNIT;
            obj.LockReason = this.LockReason;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(MaterealLock obj)
        {
            obj.MSN = this.MSN;
            obj.MaterialName = this.MaterialName;
            obj.MaterialCode = this.MaterialCode;
            obj.BatchNumber = this.BatchNumber;
            obj.CustName = this.CustName;
            obj.StockHouse = this.StockHouse;
            obj.DOCUMENTID = this.DOCUMENTID;
            obj.BasQty = this.BasQty;
            obj.UNIT = this.UNIT;
            obj.LockReason = this.LockReason;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class MaterealLockMeta
    {
        public StringPropertyMeta MSN = new StringPropertyMeta("\"MSN\"");
        public StringPropertyMeta MaterialName = new StringPropertyMeta("\"MATERIAL_NAME\"");
        public StringPropertyMeta MaterialCode = new StringPropertyMeta("\"MATERIAL_CODE\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta StockHouse = new StringPropertyMeta("\"STOCK_HOUSE\"");
        public StringPropertyMeta DOCUMENTID = new StringPropertyMeta("\"DOCUMENTID\"");
        public PropertyMeta BasQty = new PropertyMeta("\"BAS_QTY\"");
        public StringPropertyMeta UNIT = new StringPropertyMeta("\"UNIT\"");
        public StringPropertyMeta LockReason = new StringPropertyMeta("\"LOCK_REASON\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

