using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.ROUTE_INFO", IsView = false, PrimaryKeys = "ROUTE_ID",
    PrimaryProperties = "RouteId")]
    public class RouteInfo : ICloneable
    {
        #region Member Variables		
        public static RouteInfoMeta Meta = new RouteInfoMeta();
        #endregion

        #region constructor
        public RouteInfo()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ROUTE_ID", SqlType = "VARCHAR2", Length = 40)]
        public string RouteId { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SITE", SqlType = "VARCHAR2", Length = 40)]
        public string SITE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BU", SqlType = "VARCHAR2", Length = 40)]
        public string BU { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string CustCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ROUTE_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string RouteName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SECTION_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string SectionName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ACTIVE", SqlType = "CHAR", Length = 2)]
        public string ACTIVE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ROUTE_MEMO", SqlType = "VARCHAR2", Length = 100)]
        public string RouteMemo { set; get; }

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
            RouteInfo obj = new RouteInfo();

            obj.RouteId = this.RouteId;

            obj.SITE = this.SITE;
            obj.BU = this.BU;
            obj.CustCode = this.CustCode;
            obj.CustName = this.CustName;
            obj.RouteName = this.RouteName;
            obj.SectionName = this.SectionName;
            obj.ACTIVE = this.ACTIVE;
            obj.RouteMemo = this.RouteMemo;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(RouteInfo obj)
        {
            obj.RouteId = this.RouteId;
            obj.SITE = this.SITE;
            obj.BU = this.BU;
            obj.CustCode = this.CustCode;
            obj.CustName = this.CustName;
            obj.RouteName = this.RouteName;
            obj.SectionName = this.SectionName;
            obj.ACTIVE = this.ACTIVE;
            obj.RouteMemo = this.RouteMemo;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class RouteInfoMeta
    {
        public StringPropertyMeta RouteId = new StringPropertyMeta("\"ROUTE_ID\"");
        public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
        public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
        public StringPropertyMeta CustCode = new StringPropertyMeta("\"CUST_CODE\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta RouteName = new StringPropertyMeta("\"ROUTE_NAME\"");
        public StringPropertyMeta SectionName = new StringPropertyMeta("\"SECTION_NAME\"");
        public StringPropertyMeta ACTIVE = new StringPropertyMeta("\"ACTIVE\"");
        public StringPropertyMeta RouteMemo = new StringPropertyMeta("\"ROUTE_MEMO\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

