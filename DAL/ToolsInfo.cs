using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.TOOLS_INFO", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class ToolsInfo : ICloneable
    {
        #region Member Variables		
        public static ToolsInfoMeta Meta = new ToolsInfoMeta();
        #endregion

        #region constructor
        public ToolsInfo()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_TYPE", SqlType = "VARCHAR2", Length = 60)]
        public string MachineType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACTYPE_CODE", SqlType = "VARCHAR2", Length = 60)]
        public string MactypeCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TOOL_NO", SqlType = "VARCHAR2", Length = 40)]
        public string ToolNo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TOOL_TYPE", SqlType = "VARCHAR2", Length = 400)]
        public string ToolType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "EDGE_LENGTH", SqlType = "NUMBER", Length = 0)]
        public decimal? EdgeLength { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TOOL_LENGTH", SqlType = "NUMBER", Length = 0)]
        public decimal? ToolLength { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO1", SqlType = "VARCHAR2", Length = 200)]
        public string MEMO1 { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO2", SqlType = "VARCHAR2", Length = 200)]
        public string MEMO2 { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TOOL_CLASS", SqlType = "VARCHAR2", Length = 200)]
        public string ToolClass { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DIAMETER", SqlType = "NUMBER", Length = 0)]
        public decimal? DIAMETER { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "VISION", SqlType = "CHAR", Length = 2)]
        public string VISION { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ACTIVE", SqlType = "CHAR", Length = 2)]
        public string ACTIVE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MEMO", SqlType = "VARCHAR2", Length = 400)]
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
            ToolsInfo obj = new ToolsInfo();

            obj.ID = this.ID;

            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.ToolNo = this.ToolNo;
            obj.ToolType = this.ToolType;
            obj.EdgeLength = this.EdgeLength;
            obj.ToolLength = this.ToolLength;
            obj.MEMO1 = this.MEMO1;
            obj.MEMO2 = this.MEMO2;
            obj.ToolClass = this.ToolClass;
            obj.DIAMETER = this.DIAMETER;
            obj.VISION = this.VISION;
            obj.ACTIVE = this.ACTIVE;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;

            return obj;
        }

        public void CopyTo(ToolsInfo obj)
        {
            obj.ID = this.ID;
            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.ToolNo = this.ToolNo;
            obj.ToolType = this.ToolType;
            obj.EdgeLength = this.EdgeLength;
            obj.ToolLength = this.ToolLength;
            obj.MEMO1 = this.MEMO1;
            obj.MEMO2 = this.MEMO2;
            obj.ToolClass = this.ToolClass;
            obj.DIAMETER = this.DIAMETER;
            obj.VISION = this.VISION;
            obj.ACTIVE = this.ACTIVE;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class ToolsInfoMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta MactypeCode = new StringPropertyMeta("\"MACTYPE_CODE\"");
        public StringPropertyMeta ToolNo = new StringPropertyMeta("\"TOOL_NO\"");
        public StringPropertyMeta ToolType = new StringPropertyMeta("\"TOOL_TYPE\"");
        public PropertyMeta EdgeLength = new PropertyMeta("\"EDGE_LENGTH\"");
        public PropertyMeta ToolLength = new PropertyMeta("\"TOOL_LENGTH\"");
        public StringPropertyMeta MEMO1 = new StringPropertyMeta("\"MEMO1\"");
        public StringPropertyMeta MEMO2 = new StringPropertyMeta("\"MEMO2\"");
        public StringPropertyMeta ToolClass = new StringPropertyMeta("\"TOOL_CLASS\"");
        public PropertyMeta DIAMETER = new PropertyMeta("\"DIAMETER\"");
        public StringPropertyMeta VISION = new StringPropertyMeta("\"VISION\"");
        public StringPropertyMeta ACTIVE = new StringPropertyMeta("\"ACTIVE\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

