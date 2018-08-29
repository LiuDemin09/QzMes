using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.CARTON_TEMP", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class CartonTemp : ICloneable
    {
        #region Member Variables		
        public static CartonTempMeta Meta = new CartonTempMeta();
        #endregion

        #region constructor
        public CartonTemp()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = false, ColumnName = "IP", SqlType = "VARCHAR2", Length = 40)]
        public string IP { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWING_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string PartsdrawingCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUALITY_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string QualityCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QUANTITY", SqlType = "NUMBER", Length = 0)]
        public decimal? QUANTITY { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }
        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
       AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProductName { set; get; }
        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BATCH_NUMBER", SqlType = "VARCHAR2", Length = 40)]
        public string BatchNumber { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string TYPE { set; get; }
        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            CartonTemp obj = new CartonTemp();

            obj.ID = this.ID;

            obj.IP = this.IP;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.QualityCode = this.QualityCode;
            obj.QUANTITY = this.QUANTITY;
            obj.ProductName = this.ProductName;
            obj.BatchNumber = this.BatchNumber;
            obj.TYPE = this.TYPE;

            return obj;
        }

        public void CopyTo(CartonTemp obj)
        {
            obj.IP = this.IP;
            obj.PartsdrawingCode = this.PartsdrawingCode;
            obj.QualityCode = this.QualityCode;
            obj.QUANTITY = this.QUANTITY;
            obj.ID = this.ID;
            obj.ProductName = this.ProductName;
            obj.BatchNumber = this.BatchNumber;
            obj.TYPE = this.TYPE;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class CartonTempMeta
    {
        public StringPropertyMeta IP = new StringPropertyMeta("\"IP\"");
        public StringPropertyMeta PartsdrawingCode = new StringPropertyMeta("\"PARTSDRAWING_CODE\"");
        public StringPropertyMeta QualityCode = new StringPropertyMeta("\"QUALITY_CODE\"");
        public PropertyMeta QUANTITY = new PropertyMeta("\"QUANTITY\"");
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta BatchNumber = new StringPropertyMeta("\"BATCH_NUMBER\"");
        public StringPropertyMeta TYPE = new StringPropertyMeta("\"TYPE\"");
    }
    #endregion
}

