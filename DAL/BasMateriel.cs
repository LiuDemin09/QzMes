using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.BAS_MATERIEL", IsView = false, PrimaryKeys = "CPARTNO",
    PrimaryProperties = "CPARTNO")]
    public class BasMateriel : ICloneable
    {
        #region Member Variables		
        public static BasMaterielMeta Meta = new BasMaterielMeta();
        #endregion

        #region constructor
        public BasMateriel()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "CPARTNO", SqlType = "VARCHAR2", Length = 40)]
        public string CPARTNO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QPARTNO", SqlType = "VARCHAR2", Length = 60)]
        public string QPARTNO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "NAME", SqlType = "VARCHAR2", Length = 100)]
        public string NAME { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUSTOMER", SqlType = "VARCHAR2", Length = 40)]
        public string CUSTOMER { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
       AllowNull = true, ColumnName = "BAS_QTY", SqlType = "NUMBER", Length = 40)]
        public decimal? BasQty { set; get; }

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
            BasMateriel obj = new BasMateriel();

            obj.CPARTNO = this.CPARTNO;

            obj.QPARTNO = this.QPARTNO;
            obj.NAME = this.NAME;
            obj.CUSTOMER = this.CUSTOMER;
            obj.BasQty = this.BasQty;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(BasMateriel obj)
        {
            obj.CPARTNO = this.CPARTNO;
            obj.QPARTNO = this.QPARTNO;
            obj.NAME = this.NAME;
            obj.CUSTOMER = this.CUSTOMER;
            obj.BasQty = this.BasQty;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class BasMaterielMeta
    {
        public StringPropertyMeta CPARTNO = new StringPropertyMeta("\"CPARTNO\"");
        public StringPropertyMeta QPARTNO = new StringPropertyMeta("\"QPARTNO\"");
        public StringPropertyMeta NAME = new StringPropertyMeta("\"NAME\"");
        public StringPropertyMeta CUSTOMER = new StringPropertyMeta("\"CUSTOMER\"");
        public StringPropertyMeta BasQty = new StringPropertyMeta("\"BAS_QTY\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

