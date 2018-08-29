using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.SYS_MIDSVC_CONFIG", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class SysMidsvcConfig : ICloneable
    {
        #region Member Variables
        public static SysMidsvcConfigMeta Meta = new SysMidsvcConfigMeta();
        #endregion

        #region constructor
        public SysMidsvcConfig()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 20)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BU", SqlType = "VARCHAR2", Length = 10)]
        public string BU { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SITE", SqlType = "VARCHAR2", Length = 10)]
        public string SITE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "NUMBER", Length = 0)]
        public decimal? STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MID_SVC_TYPE", SqlType = "VARCHAR2", Length = 20)]
        public string MidSvcType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MID_SVC_URL", SqlType = "VARCHAR2", Length = 100)]
        public string MidSvcUrl { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MID_SVC_PORT", SqlType = "VARCHAR2", Length = 10)]
        public string MidSvcPort { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CONN_COUNT", SqlType = "NUMBER", Length = 0)]
        public decimal? ConnCount { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? CreatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? UpdatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_BY", SqlType = "VARCHAR2", Length = 20)]
        public string UpdatedBy { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "POSITION", SqlType = "NVARCHAR2", Length = 200)]
        public string POSITION { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            SysMidsvcConfig obj = new SysMidsvcConfig();

            obj.ID = this.ID;

            obj.BU = this.BU;
            obj.SITE = this.SITE;
            obj.STATUS = this.STATUS;
            obj.MidSvcType = this.MidSvcType;
            obj.MidSvcUrl = this.MidSvcUrl;
            obj.MidSvcPort = this.MidSvcPort;
            obj.ConnCount = this.ConnCount;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.POSITION = this.POSITION;

            return obj;
        }

        public void CopyTo(SysMidsvcConfig obj)
        {
            obj.ID = this.ID;
            obj.BU = this.BU;
            obj.SITE = this.SITE;
            obj.STATUS = this.STATUS;
            obj.MidSvcType = this.MidSvcType;
            obj.MidSvcUrl = this.MidSvcUrl;
            obj.MidSvcPort = this.MidSvcPort;
            obj.ConnCount = this.ConnCount;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.POSITION = this.POSITION;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class SysMidsvcConfigMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
        public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
        public PropertyMeta STATUS = new PropertyMeta("\"STATUS\"");
        public StringPropertyMeta MidSvcType = new StringPropertyMeta("\"MID_SVC_TYPE\"");
        public StringPropertyMeta MidSvcUrl = new StringPropertyMeta("\"MID_SVC_URL\"");
        public StringPropertyMeta MidSvcPort = new StringPropertyMeta("\"MID_SVC_PORT\"");
        public PropertyMeta ConnCount = new PropertyMeta("\"CONN_COUNT\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public StringPropertyMeta POSITION = new StringPropertyMeta("\"POSITION\"");
    }
    #endregion
}

