using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.MATERIAL_STOCK_HISTORY", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class MaterialStockHistory : ICloneable
    {
        #region Member Variables		
        public static MaterialStockHistoryMeta Meta = new MaterialStockHistoryMeta();
        #endregion

        #region constructor
        public MaterialStockHistory()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MSN", SqlType = "VARCHAR2", Length = 40)]
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
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UNIT", SqlType = "VARCHAR2", Length = 40)]
        public string UNIT { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS_MEMO", SqlType = "VARCHAR2", Length = 40)]
        public string StatusMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WORK_ORDER", SqlType = "VARCHAR2", Length = 40)]
        public string WorkOrder { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MATERIAL_HANDLER", SqlType = "VARCHAR2", Length = 40)]
        public string MaterialHandler { set; get; }

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
     AllowNull = true, ColumnName = "DESCRIPTION", SqlType = "VARCHAR2", Length = 400)]
        public string Description { set; get; }
        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            MaterialStockHistory obj = new MaterialStockHistory();

            obj.ID = this.ID;

            obj.MSN = this.MSN;
            obj.MaterialName = this.MaterialName;
            obj.MaterialCode = this.MaterialCode;
            obj.BatchNumber = this.BatchNumber;
            obj.CustName = this.CustName;
            obj.StockHouse = this.StockHouse;
            obj.DOCUMENTID = this.DOCUMENTID;
            obj.QUANTITY = this.QUANTITY;
            obj.UNIT = this.UNIT;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.WorkOrder = this.WorkOrder;
            obj.MaterialHandler = this.MaterialHandler;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.Description = this.Description;
            return obj;
        }

        public void CopyTo(MaterialStockHistory obj)
        {
            obj.ID = this.ID;
            obj.MSN = this.MSN;
            obj.MaterialName = this.MaterialName;
            obj.MaterialCode = this.MaterialCode;
            obj.BatchNumber = this.BatchNumber;
            obj.CustName = this.CustName;
            obj.StockHouse = this.StockHouse;
            obj.DOCUMENTID = this.DOCUMENTID;
            obj.QUANTITY = this.QUANTITY;
            obj.UNIT = this.UNIT;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.WorkOrder = this.WorkOrder;
            obj.MaterialHandler = this.MaterialHandler;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.Description = this.Description;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class MaterialStockHistoryMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta MSN = new StringPropertyMeta("\"MSN\"");
        public StringPropertyMeta MaterialName = new StringPropertyMeta("\"MATERIAL_NAME\"");
        public StringPropertyMeta MaterialCode = new StringPropertyMeta("\"MATERIAL_CODE\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta StockHouse = new StringPropertyMeta("\"STOCK_HOUSE\"");
        public StringPropertyMeta DOCUMENTID = new StringPropertyMeta("\"DOCUMENTID\"");
        public PropertyMeta QUANTITY = new PropertyMeta("\"QUANTITY\"");
        public StringPropertyMeta UNIT = new StringPropertyMeta("\"UNIT\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta StatusMemo = new StringPropertyMeta("\"STATUS_MEMO\"");
        public StringPropertyMeta WorkOrder = new StringPropertyMeta("\"WORK_ORDER\"");
        public StringPropertyMeta MaterialHandler = new StringPropertyMeta("\"MATERIAL_HANDLER\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta Description = new StringPropertyMeta("\"DESCRIPTION\"");
    }
    #endregion
}

