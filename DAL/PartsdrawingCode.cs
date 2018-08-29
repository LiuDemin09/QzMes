using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.PARTSDRAWING_CODE", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class PartsdrawingCode : ICloneable
    {
        #region Member Variables		
        public static PartsdrawingCodeMeta Meta = new PartsdrawingCodeMeta();
        #endregion

        #region constructor
        public PartsdrawingCode()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTS_CODE", SqlType = "VARCHAR2", Length = 100)]
        public string PartsCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string CustCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string ProductCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PLAN_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? PlanQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUALITY_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string QualityCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ASK_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? AskQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string BatchNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ASK_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? AskDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ACTIVE", SqlType = "CHAR", Length = 2)]
        public string ACTIVE { set; get; }

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
        AllowNull = true, ColumnName = "UNIT_TIME", SqlType = "NUMBER", Length = 0)]
        public decimal? UnitTime { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            PartsdrawingCode obj = new PartsdrawingCode();

            obj.ID = this.ID;

            obj.PartsCode = this.PartsCode;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.PlanQuantity = this.PlanQuantity;
            obj.QualityCode = this.QualityCode;
            obj.AskQuantity = this.AskQuantity;
            obj.BatchNumber = this.BatchNumber;
            obj.AskDate = this.AskDate;
            obj.ACTIVE = this.ACTIVE;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.UnitTime = this.UnitTime;

            return obj;
        }

        public void CopyTo(PartsdrawingCode obj)
        {
            obj.ID = this.ID;
            obj.PartsCode = this.PartsCode;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.PlanQuantity = this.PlanQuantity;
            obj.QualityCode = this.QualityCode;
            obj.AskQuantity = this.AskQuantity;
            obj.BatchNumber = this.BatchNumber;
            obj.AskDate = this.AskDate;
            obj.ACTIVE = this.ACTIVE;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.UnitTime = this.UnitTime;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class PartsdrawingCodeMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta PartsCode = new StringPropertyMeta("\"PARTS_CODE\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta CustCode = new StringPropertyMeta("\"CUST_CODE\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta ProductCode = new StringPropertyMeta("\"PRODUCT_CODE\"");
        public PropertyMeta PlanQuantity = new PropertyMeta("\"PLAN_QUANTITY\"");
        public StringPropertyMeta QualityCode = new StringPropertyMeta("\"QUALITY_CODE\"");
        public PropertyMeta AskQuantity = new PropertyMeta("\"ASK_QUANTITY\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public DatetimePropertyMeta AskDate = new DatetimePropertyMeta("\"ASK_DATE\"");
        public StringPropertyMeta ACTIVE = new StringPropertyMeta("\"ACTIVE\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public PropertyMeta UnitTime = new PropertyMeta("\"UNIT_TIME\"");
    }
    #endregion
}

