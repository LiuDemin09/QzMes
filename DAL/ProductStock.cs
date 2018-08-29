using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.PRODUCT_STOCK", IsView = false, PrimaryKeys = "PSN",
    PrimaryProperties = "PSN")]
    public class ProductStock : ICloneable
    {
        #region Member Variables		
        public static ProductStockMeta Meta = new ProductStockMeta();
        #endregion

        #region constructor
        public ProductStock()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "PSN", SqlType = "VARCHAR2", Length = 40)]
        public string PSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WORK_ORDER", SqlType = "VARCHAR2", Length = 40)]
        public string WorkOrder { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ORDER_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string OrderNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string ProductCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MSN", SqlType = "VARCHAR2", Length = 40)]
        public string MSN { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string BatchNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MANUFACTURE", SqlType = "VARCHAR2", Length = 40)]
        public string MANUFACTURE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STOCK_HOUSE", SqlType = "CHAR", Length = 40)]
        public string StockHouse { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DOCUMENTID", SqlType = "VARCHAR2", Length = 40)]
        public string DOCUMENTID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UNIT", SqlType = "VARCHAR2", Length = 40)]
        public string UNIT { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FROM_BY", SqlType = "VARCHAR2", Length = 40)]
        public string FromBy { set; get; }

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

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWING_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string PartsdrawingCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUALITY_CODE", SqlType = "VARCHAR2", Length = 100)]
        public string QualityCode { set; get; }
        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            ProductStock obj = new ProductStock();

            obj.PSN = this.PSN;

            obj.WorkOrder = this.WorkOrder;
            obj.OrderNumber = this.OrderNumber;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.MSN = this.MSN;
            obj.BatchNumber = this.BatchNumber;
            obj.MANUFACTURE = this.MANUFACTURE;
            obj.StockHouse = this.StockHouse;
            obj.DOCUMENTID = this.DOCUMENTID;
            obj.QUANTITY = this.QUANTITY;
            obj.UNIT = this.UNIT;
            obj.STATUS = this.STATUS;
            obj.FromBy = this.FromBy;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.QualityCode = this.QualityCode;
            return obj;
        }

        public void CopyTo(ProductStock obj)
        {
            obj.PSN = this.PSN;
            obj.WorkOrder = this.WorkOrder;
            obj.OrderNumber = this.OrderNumber;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.MSN = this.MSN;
            obj.BatchNumber = this.BatchNumber;
            obj.MANUFACTURE = this.MANUFACTURE;
            obj.StockHouse = this.StockHouse;
            obj.DOCUMENTID = this.DOCUMENTID;
            obj.QUANTITY = this.QUANTITY;
            obj.UNIT = this.UNIT;
            obj.STATUS = this.STATUS;
            obj.FromBy = this.FromBy;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.QualityCode = this.QualityCode;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class ProductStockMeta
    {
        public StringPropertyMeta PSN = new StringPropertyMeta("\"PSN\"");
        public StringPropertyMeta WorkOrder = new StringPropertyMeta("\"WORK_ORDER\"");
        public StringPropertyMeta OrderNumber = new StringPropertyMeta("\"ORDER_NUMBER\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta ProductCode = new StringPropertyMeta("\"PRODUCT_CODE\"");
        public StringPropertyMeta MSN = new StringPropertyMeta("\"MSN\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public StringPropertyMeta MANUFACTURE = new StringPropertyMeta("\"MANUFACTURE\"");
        public StringPropertyMeta StockHouse = new StringPropertyMeta("\"STOCK_HOUSE\"");
        public StringPropertyMeta DOCUMENTID = new StringPropertyMeta("\"DOCUMENTID\"");
        public PropertyMeta QUANTITY = new PropertyMeta("\"QUANTITY\"");
        public StringPropertyMeta UNIT = new StringPropertyMeta("\"UNIT\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta FromBy = new StringPropertyMeta("\"FROM_BY\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta PartsdrawingCode = new StringPropertyMeta("\"PARTSDRAWING_CODE\"");
        public StringPropertyMeta QualityCode = new StringPropertyMeta("\"QUALITY_CODE\"");
    }
    #endregion
}

