using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_LABEL_TEMPLATE", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class BasLabelTemplate : ICloneable
    {
        #region Member Variables
        public static BasLabelTemplateMeta Meta = new BasLabelTemplateMeta();
        #endregion

        #region constructor
        public BasLabelTemplate()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 20)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SITE", SqlType = "VARCHAR2", Length = 10)]
        public string SITE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BU", SqlType = "VARCHAR2", Length = 10)]
        public string BU { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TPL_NAME", SqlType = "VARCHAR2", Length = 20)]
        public string TplName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TPL_TYPE", SqlType = "VARCHAR2", Length = 10)]
        public string TplType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FAMILY", SqlType = "VARCHAR2", Length = 10)]
        public string FAMILY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO", SqlType = "NVARCHAR2", Length = 400)]
        public string MEMO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "NUMBER", Length = 0)]
        public decimal? STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "APPROVE_BY", SqlType = "VARCHAR2", Length = 20)]
        public string ApproveBy { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? UpdatedDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATED_BY", SqlType = "VARCHAR2", Length = 20)]
        public string UpdatedBy { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            BasLabelTemplate obj = new BasLabelTemplate();

            obj.ID = this.ID;

            obj.SITE = this.SITE;
            obj.BU = this.BU;
            obj.TplName = this.TplName;
            obj.TplType = this.TplType;
            obj.FAMILY = this.FAMILY;
            obj.MEMO = this.MEMO;
            obj.STATUS = this.STATUS;
            obj.ApproveBy = this.ApproveBy;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasLabelTemplate obj)
        {
            obj.ID = this.ID;
            obj.SITE = this.SITE;
            obj.BU = this.BU;
            obj.TplName = this.TplName;
            obj.TplType = this.TplType;
            obj.FAMILY = this.FAMILY;
            obj.MEMO = this.MEMO;
            obj.STATUS = this.STATUS;
            obj.ApproveBy = this.ApproveBy;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasLabelTemplateMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
        public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
        public StringPropertyMeta TplName = new StringPropertyMeta("\"TPL_NAME\"");
        public StringPropertyMeta TplType = new StringPropertyMeta("\"TPL_TYPE\"");
        public StringPropertyMeta FAMILY = new StringPropertyMeta("\"FAMILY\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public PropertyMeta STATUS = new PropertyMeta("\"STATUS\"");
        public StringPropertyMeta ApproveBy = new StringPropertyMeta("\"APPROVE_BY\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

