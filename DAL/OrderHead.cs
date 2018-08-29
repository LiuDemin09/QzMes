using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.ORDER_HEAD", IsView = false, PrimaryKeys = "ORDER_NO",
    PrimaryProperties = "OrderNo")]
    public class OrderHead : ICloneable
    {
        #region Member Variables		
        public static OrderHeadMeta Meta = new OrderHeadMeta();
        #endregion

        #region constructor
        public OrderHead()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ORDER_NO", SqlType = "VARCHAR2", Length = 40)]
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
        AllowNull = true, ColumnName = "ORDER_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? OrderQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "IN_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? InQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "OUT_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? OutQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO", SqlType = "VARCHAR2", Length = 100)]
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
            OrderHead obj = new OrderHead();

            obj.OrderNo = this.OrderNo;

            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.CONTRACT = this.CONTRACT;
            obj.OrderQuantity = this.OrderQuantity;
            obj.InQuantity = this.InQuantity;
            obj.OutQuantity = this.OutQuantity;
            obj.STATUS = this.STATUS;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(OrderHead obj)
        {
            obj.OrderNo = this.OrderNo;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.CONTRACT = this.CONTRACT;
            obj.OrderQuantity = this.OrderQuantity;
            obj.InQuantity = this.InQuantity;
            obj.OutQuantity = this.OutQuantity;
            obj.STATUS = this.STATUS;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class OrderHeadMeta
    {
        public StringPropertyMeta OrderNo = new StringPropertyMeta("\"ORDER_NO\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta CustCode = new StringPropertyMeta("\"CUST_CODE\"");
        public StringPropertyMeta CONTRACT = new StringPropertyMeta("\"CONTRACT\"");
        public PropertyMeta OrderQuantity = new PropertyMeta("\"ORDER_QUANTITY\"");
        public PropertyMeta InQuantity = new PropertyMeta("\"IN_QUANTITY\"");
        public PropertyMeta OutQuantity = new PropertyMeta("\"OUT_QUANTITY\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

