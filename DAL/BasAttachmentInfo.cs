using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_ATTACHMENT_INFO", IsView = false, PrimaryKeys = "FILE_NO",
    PrimaryProperties = "FileNo")]
    public class BasAttachmentInfo : ICloneable
    {
        #region Member Variables		
        public static BasAttachmentInfoMeta Meta = new BasAttachmentInfoMeta();
        #endregion

        #region constructor
        public BasAttachmentInfo()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "FILE_NO", SqlType = "VARCHAR2", Length = 20)]
        public string FileNo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FILE_TYPE", SqlType = "NUMBER", Length = 0)]
        public decimal? FileType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FRIENDLY_KEY", SqlType = "VARCHAR2", Length = 20)]
        public string FriendlyKey { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FILE_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string FileName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ORIGINAL_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string OriginalName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "EXTENSION_NAME", SqlType = "VARCHAR2", Length = 10)]
        public string ExtensionName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "FULL_PATH", SqlType = "VARCHAR2", Length = 200)]
        public string FullPath { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PHYSICAL_PATH", SqlType = "VARCHAR2", Length = 200)]
        public string PhysicalPath { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DELETE_FLAG", SqlType = "NUMBER", Length = 0)]
        public decimal? DeleteFlag { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATED_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? CreatedDate { set; get; }

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
            BasAttachmentInfo obj = new BasAttachmentInfo();

            obj.FileNo = this.FileNo;

            obj.FileType = this.FileType;
            obj.FriendlyKey = this.FriendlyKey;
            obj.FileName = this.FileName;
            obj.OriginalName = this.OriginalName;
            obj.ExtensionName = this.ExtensionName;
            obj.FullPath = this.FullPath;
            obj.PhysicalPath = this.PhysicalPath;
            obj.DeleteFlag = this.DeleteFlag;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasAttachmentInfo obj)
        {
            obj.FileNo = this.FileNo;
            obj.FileType = this.FileType;
            obj.FriendlyKey = this.FriendlyKey;
            obj.FileName = this.FileName;
            obj.OriginalName = this.OriginalName;
            obj.ExtensionName = this.ExtensionName;
            obj.FullPath = this.FullPath;
            obj.PhysicalPath = this.PhysicalPath;
            obj.DeleteFlag = this.DeleteFlag;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasAttachmentInfoMeta
    {
        public StringPropertyMeta FileNo = new StringPropertyMeta("\"FILE_NO\"");
        public PropertyMeta FileType = new PropertyMeta("\"FILE_TYPE\"");
        public StringPropertyMeta FriendlyKey = new StringPropertyMeta("\"FRIENDLY_KEY\"");
        public StringPropertyMeta FileName = new StringPropertyMeta("\"FILE_NAME\"");
        public StringPropertyMeta OriginalName = new StringPropertyMeta("\"ORIGINAL_NAME\"");
        public StringPropertyMeta ExtensionName = new StringPropertyMeta("\"EXTENSION_NAME\"");
        public StringPropertyMeta FullPath = new StringPropertyMeta("\"FULL_PATH\"");
        public StringPropertyMeta PhysicalPath = new StringPropertyMeta("\"PHYSICAL_PATH\"");
        public PropertyMeta DeleteFlag = new PropertyMeta("\"DELETE_FLAG\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

