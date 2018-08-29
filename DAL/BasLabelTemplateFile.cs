using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_LABEL_TEMPLATE_FILE", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class BasLabelTemplateFile : ICloneable
    {
        #region Member Variables
        public static BasLabelTemplateFileMeta Meta = new BasLabelTemplateFileMeta();
        #endregion

        #region constructor
        public BasLabelTemplateFile()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 20)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TPL_ID", SqlType = "VARCHAR2", Length = 100)]
        public string TplId { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FILE_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string FileName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
         AllowNull = true, ColumnName = "FILE_DATA", SqlType = "BLOB", Length = 4000)]
        public object FileData { set; get; }

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
            BasLabelTemplateFile obj = new BasLabelTemplateFile();

            obj.ID = this.ID;
            obj.TplId = this.TplId;
            obj.FileName = this.FileName;
            obj.FileData = this.FileData;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasLabelTemplateFile obj)
        {
            obj.ID = this.ID;
            obj.TplId = this.TplId;
            obj.FileName = this.FileName;
            obj.FileData = this.FileData;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasLabelTemplateFileMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta TplId = new StringPropertyMeta("\"TPL_ID\"");
        public StringPropertyMeta FileName = new StringPropertyMeta("\"FILE_NAME\"");
        public PropertyMeta FileData = new PropertyMeta("\"FILE_DATA\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

