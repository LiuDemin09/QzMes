using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_LABEL_TYPE_CONFIG", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class BasLabelTypeConfig : ICloneable
    {
        #region Member Variables
        public static BasLabelTypeConfigMeta Meta = new BasLabelTypeConfigMeta();
        #endregion

        #region constructor
        public BasLabelTypeConfig()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 20)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TPL_TYPE", SqlType = "VARCHAR2", Length = 30)]
        public string TplType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TPL_DESC", SqlType = "VARCHAR2", Length = 30)]
        public string TplDesc { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BIND_FUNC", SqlType = "VARCHAR2", Length = 100)]
        public string BindFunc { set; get; }

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
            BasLabelTypeConfig obj = new BasLabelTypeConfig();

            obj.ID = this.ID;

            obj.TplType = this.TplType;
            obj.TplDesc = this.TplDesc;
            obj.BindFunc = this.BindFunc;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasLabelTypeConfig obj)
        {
            obj.ID = this.ID;
            obj.TplType = this.TplType;
            obj.TplDesc = this.TplDesc;
            obj.BindFunc = this.BindFunc;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasLabelTypeConfigMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta TplType = new StringPropertyMeta("\"TPL_TYPE\"");
        public StringPropertyMeta TplDesc = new StringPropertyMeta("\"TPL_DESC\"");
        public StringPropertyMeta BindFunc = new StringPropertyMeta("\"BIND_FUNC\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

