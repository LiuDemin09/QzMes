using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.UNIT_STATISTICS", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class UnitStatistics : ICloneable
    {
        #region Member Variables		
        public static UnitStatisticsMeta Meta = new UnitStatisticsMeta();
        #endregion

        #region constructor
        public UnitStatistics()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATISTICS_ITEM", SqlType = "VARCHAR2", Length = 40)]
        public string StatisticsItem { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATISTICS_TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string StatisticsType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string StationName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PASS_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? PassQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAIL_QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? FailQuantity { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BYDAY", SqlType = "VARCHAR2", Length = 40)]
        public string BYDAY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BYWEEK", SqlType = "VARCHAR2", Length = 40)]
        public string BYWEEK { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BYMONTH", SqlType = "VARCHAR2", Length = 40)]
        public string BYMONTH { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BYYEAR", SqlType = "VARCHAR2", Length = 40)]
        public string BYYEAR { set; get; }

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
            UnitStatistics obj = new UnitStatistics();

            obj.ID = this.ID;

            obj.StatisticsItem = this.StatisticsItem;
            obj.StatisticsType = this.StatisticsType;
            obj.StationName = this.StationName;
            obj.ProductName = this.ProductName;
            obj.CustName = this.CustName;
            obj.PassQuantity = this.PassQuantity;
            obj.FailQuantity = this.FailQuantity;
            obj.BYDAY = this.BYDAY;
            obj.BYWEEK = this.BYWEEK;
            obj.BYMONTH = this.BYMONTH;
            obj.BYYEAR = this.BYYEAR;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(UnitStatistics obj)
        {
            obj.ID = this.ID;
            obj.StatisticsItem = this.StatisticsItem;
            obj.StatisticsType = this.StatisticsType;
            obj.StationName = this.StationName;
            obj.ProductName = this.ProductName;
            obj.CustName = this.CustName;
            obj.PassQuantity = this.PassQuantity;
            obj.FailQuantity = this.FailQuantity;
            obj.BYDAY = this.BYDAY;
            obj.BYWEEK = this.BYWEEK;
            obj.BYMONTH = this.BYMONTH;
            obj.BYYEAR = this.BYYEAR;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class UnitStatisticsMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta StatisticsItem = new StringPropertyMeta("\"STATISTICS_ITEM\"");
        public StringPropertyMeta StatisticsType = new StringPropertyMeta("\"STATISTICS_TYPE\"");
        public StringPropertyMeta StationName = new StringPropertyMeta("\"STATION_NAME\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public PropertyMeta PassQuantity = new PropertyMeta("\"PASS_QUANTITY\"");
        public PropertyMeta FailQuantity = new PropertyMeta("\"FAIL_QUANTITY\"");
        public StringPropertyMeta BYDAY = new StringPropertyMeta("\"BYDAY\"");
        public StringPropertyMeta BYWEEK = new StringPropertyMeta("\"BYWEEK\"");
        public StringPropertyMeta BYMONTH = new StringPropertyMeta("\"BYMONTH\"");
        public StringPropertyMeta BYYEAR = new StringPropertyMeta("\"BYYEAR\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

