using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.ROUTE_DETAILS", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class RouteDetails : ICloneable
    {
        #region Member Variables		
        public static RouteDetailsMeta Meta = new RouteDetailsMeta();
        #endregion

        #region constructor
        public RouteDetails()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ROUTE_ID", SqlType = "VARCHAR2", Length = 40)]
        public string RouteId { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ROUTE_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string RouteName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATION_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string StationName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string MachineType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACTYPE_CODE", SqlType = "VARCHAR2", Length = 60)]
        public string MactypeCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "NEXT_STATION", SqlType = "VARCHAR2", Length = 40)]
        public string NextStation { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAIL_NEXT_STATION", SqlType = "VARCHAR2", Length = 40)]
        public string FailNextStation { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FLAG", SqlType = "NUMBER", Length = 0)]
        public decimal? FLAG { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ORDER_ID", SqlType = "NUMBER", Length = 0)]
        public decimal? OrderId { set; get; }

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
       AllowNull = true, ColumnName = "STATION_ID", SqlType = "VARCHAR2", Length = 100)]
        public string StationId { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            RouteDetails obj = new RouteDetails();

            obj.ID = this.ID;

            obj.RouteId = this.RouteId;
            obj.RouteName = this.RouteName;
            obj.StationName = this.StationName;
            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.NextStation = this.NextStation;
            obj.FailNextStation = this.FailNextStation;
            obj.FLAG = this.FLAG;
            obj.OrderId = this.OrderId;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.StationId = this.StationId;
            return obj;
        }

        public void CopyTo(RouteDetails obj)
        {
            obj.ID = this.ID;
            obj.RouteId = this.RouteId;
            obj.RouteName = this.RouteName;
            obj.StationName = this.StationName;
            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.NextStation = this.NextStation;
            obj.FailNextStation = this.FailNextStation;
            obj.FLAG = this.FLAG;
            obj.OrderId = this.OrderId;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.StationId = this.StationId;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class RouteDetailsMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta RouteId = new StringPropertyMeta("\"ROUTE_ID\"");
        public StringPropertyMeta RouteName = new StringPropertyMeta("\"ROUTE_NAME\"");
        public StringPropertyMeta StationName = new StringPropertyMeta("\"STATION_NAME\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta MactypeCode = new StringPropertyMeta("\"MACTYPE_CODE\"");
        public StringPropertyMeta NextStation = new StringPropertyMeta("\"NEXT_STATION\"");
        public StringPropertyMeta FailNextStation = new StringPropertyMeta("\"FAIL_NEXT_STATION\"");
        public PropertyMeta FLAG = new PropertyMeta("\"FLAG\"");
        public PropertyMeta OrderId = new PropertyMeta("\"ORDER_ID\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta StationID = new StringPropertyMeta("\"STATION_ID\"");
    }
    #endregion
}

