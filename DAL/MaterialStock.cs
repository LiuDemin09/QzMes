﻿using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.MATERIAL_STOCK", IsView = false, PrimaryKeys = "MSN",
    PrimaryProperties = "MSN")]
    public class MaterialStock : ICloneable
    {
        #region Member Variables		
        public static MaterialStockMeta Meta = new MaterialStockMeta();
        #endregion

        #region constructor
        public MaterialStock()
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
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

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
            MaterialStock obj = new MaterialStock();

            obj.MSN = this.MSN;

            obj.MaterialName = this.MaterialName;
            obj.MaterialCode = this.MaterialCode;
            obj.BatchNumber = this.BatchNumber;
            obj.CustName = this.CustName;
            obj.StockHouse = this.StockHouse;
            obj.DOCUMENTID = this.DOCUMENTID;
            obj.BasQty = this.BasQty;
            obj.UNIT = this.UNIT;
            obj.STATUS = this.STATUS;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.Description = this.Description;
            return obj;
        }

        public void CopyTo(MaterialStock obj)
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
            obj.STATUS = this.STATUS;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.Description = this.Description;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class MaterialStockMeta
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
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta Description = new StringPropertyMeta("\"DESCRIPTION\"");
    }
    #endregion
}

