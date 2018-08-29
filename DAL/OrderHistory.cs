using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.ORDER_HISTORY", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class OrderHistory : ICloneable
    {
        #region Member Variables		
        public static OrderHistoryMeta Meta = new OrderHistoryMeta();
        #endregion

        #region constructor
        public OrderHistory()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ORDER_NO", SqlType = "VARCHAR2", Length = 40)]
        public string OrderNo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string CustCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CONTRACT", SqlType = "VARCHAR2", Length = 40)]
        public string CONTRACT { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string ProductCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWING_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string PartsdrawingCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string BatchNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ORDER_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? OrderQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "IN_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? InQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "OUT_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? OutQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
       AllowNull = true, ColumnName = "OUT_NOTICE_QTY", SqlType = "NUMBER", Length = 0)]
        public decimal? OutNoticeQty { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
      AllowNull = true, ColumnName = "THIS_OUT_QTY", SqlType = "NUMBER", Length = 0)]
        public decimal? ThisOutQty { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "THIS_OUT_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? ThisOutTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "OUT_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? OutDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS_MEMO", SqlType = "VARCHAR2", Length = 40)]
        public string StatusMemo { set; get; }

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
            OrderHistory obj = new OrderHistory();

            obj.ID = this.ID;

            obj.OrderNo = this.OrderNo;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.CONTRACT = this.CONTRACT;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.BatchNumber = this.BatchNumber;
            obj.OrderQuantity = this.OrderQuantity;
            obj.InQuantity = this.InQuantity;
            obj.OutQuantity = this.OutQuantity;
            obj.OutNoticeQty = this.OutNoticeQty;
            obj.ThisOutQty = this.ThisOutQty;
            obj.ThisOutTime = this.ThisOutTime;
            obj.OutDate = this.OutDate;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(OrderHistory obj)
        {
            obj.ID = this.ID;
            obj.OrderNo = this.OrderNo;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.CONTRACT = this.CONTRACT;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.BatchNumber = this.BatchNumber;
            obj.OrderQuantity = this.OrderQuantity;
            obj.InQuantity = this.InQuantity;
            obj.OutQuantity = this.OutQuantity;
            obj.OutNoticeQty = this.OutNoticeQty;
            obj.ThisOutQty = this.ThisOutQty;
            obj.ThisOutTime = this.ThisOutTime;
            obj.OutDate = this.OutDate;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class OrderHistoryMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta OrderNo = new StringPropertyMeta("\"ORDER_NO\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta CustCode = new StringPropertyMeta("\"CUST_CODE\"");
        public StringPropertyMeta CONTRACT = new StringPropertyMeta("\"CONTRACT\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta ProductCode = new StringPropertyMeta("\"PRODUCT_CODE\"");
        public StringPropertyMeta PartsdrawingCode = new StringPropertyMeta("\"PARTSDRAWING_CODE\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public PropertyMeta OrderQuantity = new PropertyMeta("\"ORDER_QUANTITY\"");
        public PropertyMeta InQuantity = new PropertyMeta("\"IN_QUANTITY\"");
        public PropertyMeta OutQuantity = new PropertyMeta("\"OUT_QUANTITY\"");
        public PropertyMeta OutNoticeQty = new PropertyMeta("\"OUT_NOTICE_QTY\"");
        public PropertyMeta ThisOutQty = new PropertyMeta("\"THIS_OUT_QTY\"");
        public PropertyMeta ThisOutTime = new PropertyMeta("\"THIS_OUT_TIME\"");
        public DatetimePropertyMeta OutDate = new DatetimePropertyMeta("\"OUT_DATE\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta StatusMemo = new StringPropertyMeta("\"STATUS_MEMO\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

