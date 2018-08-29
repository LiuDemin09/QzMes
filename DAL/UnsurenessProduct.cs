using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.UNSURENESS_PRODUCT", IsView = false, PrimaryKeys = "PSN",
    PrimaryProperties = "PSN")]
    public class UnsurenessProduct : ICloneable
    {
        #region Member Variables		
        public static UnsurenessProductMeta Meta = new UnsurenessProductMeta();
        #endregion

        #region constructor
        public UnsurenessProduct()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "PSN", SqlType = "VARCHAR2", Length = 40)]
        public string PSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MSN", SqlType = "VARCHAR2", Length = 40)]
        public string MSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WORK_ORDER", SqlType = "VARCHAR2", Length = 40)]
        public string WorkOrder { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAIL_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string FailCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAIL_MEMO", SqlType = "VARCHAR2", Length = 400)]
        public string FailMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string StationName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWING_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string PartsdrawingCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string BatchNumber { set; get; }

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
            UnsurenessProduct obj = new UnsurenessProduct();

            obj.PSN = this.PSN;

            obj.MSN = this.MSN;
            obj.WorkOrder = this.WorkOrder;
            obj.FailCode = this.FailCode;
            obj.FailMemo = this.FailMemo;
            obj.STATUS = this.STATUS;
            obj.StationName = this.StationName;
            obj.QUANTITY = this.QUANTITY;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.ProductName = this.ProductName;
            obj.BatchNumber = this.BatchNumber;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(UnsurenessProduct obj)
        {
            obj.PSN = this.PSN;
            obj.MSN = this.MSN;
            obj.WorkOrder = this.WorkOrder;
            obj.FailCode = this.FailCode;
            obj.FailMemo = this.FailMemo;
            obj.STATUS = this.STATUS;
            obj.StationName = this.StationName;
            obj.QUANTITY = this.QUANTITY;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.ProductName = this.ProductName;
            obj.BatchNumber = this.BatchNumber;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class UnsurenessProductMeta
    {
        public StringPropertyMeta PSN = new StringPropertyMeta("\"PSN\"");
        public StringPropertyMeta MSN = new StringPropertyMeta("\"MSN\"");
        public StringPropertyMeta WorkOrder = new StringPropertyMeta("\"WORK_ORDER\"");
        public StringPropertyMeta FailCode = new StringPropertyMeta("\"FAIL_CODE\"");
        public StringPropertyMeta FailMemo = new StringPropertyMeta("\"FAIL_MEMO\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta StationName = new StringPropertyMeta("\"STATION_NAME\"");
        public PropertyMeta QUANTITY = new PropertyMeta("\"QUANTITY\"");
        public StringPropertyMeta PartsdrawingCode = new StringPropertyMeta("\"PARTSDRAWING_CODE\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

