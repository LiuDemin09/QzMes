using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_CODE", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class BasCode : ICloneable
    {
        #region Member Variables		
        public static BasCodeMeta Meta = new BasCodeMeta();
        #endregion

        #region constructor
        public BasCode()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "NAME", SqlType = "VARCHAR2", Length = 40)]
        public string NAME { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string TYPE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PREFIX", SqlType = "VARCHAR2", Length = 20)]
        public string PREFIX { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DATE_FORMAT", SqlType = "VARCHAR2", Length = 40)]
        public string DateFormat { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BIND_SEQUENCE", SqlType = "VARCHAR2", Length = 40)]
        public string BindSequence { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CODE_LEN", SqlType = "NUMBER", Length = 0)]
        public decimal? CodeLen { set; get; }

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
            BasCode obj = new BasCode();

            obj.ID = this.ID;

            obj.NAME = this.NAME;
            obj.TYPE = this.TYPE;
            obj.PREFIX = this.PREFIX;
            obj.DateFormat = this.DateFormat;
            obj.BindSequence = this.BindSequence;
            obj.CodeLen = this.CodeLen;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasCode obj)
        {
            obj.ID = this.ID;
            obj.NAME = this.NAME;
            obj.TYPE = this.TYPE;
            obj.PREFIX = this.PREFIX;
            obj.DateFormat = this.DateFormat;
            obj.BindSequence = this.BindSequence;
            obj.CodeLen = this.CodeLen;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasCodeMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta NAME = new StringPropertyMeta("\"NAME\"");
        public StringPropertyMeta TYPE = new StringPropertyMeta("\"TYPE\"");
        public StringPropertyMeta PREFIX = new StringPropertyMeta("\"PREFIX\"");
        public StringPropertyMeta DateFormat = new StringPropertyMeta("\"DATE_FORMAT\"");
        public StringPropertyMeta BindSequence = new StringPropertyMeta("\"BIND_SEQUENCE\"");
        public PropertyMeta CodeLen = new PropertyMeta("\"CODE_LEN\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

