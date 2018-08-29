using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.PROGRAM_INFO", IsView = false, PrimaryKeys = "ID",
    PrimaryProperties = "ID")]
    public class ProgramInfo : ICloneable
    {
        #region Member Variables		
        public static ProgramInfoMeta Meta = new ProgramInfoMeta();
        #endregion

        #region constructor
        public ProgramInfo()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 40)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TECHNOLOGY_ID", SqlType = "VARCHAR2", Length = 40)]
        public string TechnologyId { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PARTSDRAWINGNO", SqlType = "VARCHAR2", Length = 100)]
        public string PARTSDRAWINGNO { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string CustName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CUST_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string CustCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string ProductName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PRODUCT_CODE", SqlType = "VARCHAR2", Length = 100)]
        public string ProductCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_TYPE", SqlType = "VARCHAR2", Length = 40)]
        public string MachineType { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACTYPE_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string MactypeCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS", SqlType = "CHAR", Length = 2)]
        public string STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS_MEMO", SqlType = "VARCHAR2", Length = 40)]
        public string StatusMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROCESS_NO", SqlType = "VARCHAR2", Length = 40)]
        public string ProcessNo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROCESS_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProcessName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UNIT_TIME", SqlType = "NUMBER", Length = 0)]
        public decimal? UnitTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROGRAM_ENGINEER", SqlType = "VARCHAR2", Length = 40)]
        public string ProgramEngineer { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROGRAM_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProgramName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROGRAM_FNAME", SqlType = "VARCHAR2", Length = 100)]
        public string ProgramFname { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROGRAM_PATH", SqlType = "VARCHAR2", Length = 400)]
        public string ProgramPath { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TOOL_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string ToolName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "TOOL_PATH", SqlType = "VARCHAR2", Length = 400)]
        public string ToolPath { set; get; }

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

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
       AllowNull = true, ColumnName = "PLAN_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? PlanDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "REAL_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? RealDate { set; get; }
        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            ProgramInfo obj = new ProgramInfo();

            obj.ID = this.ID;

            obj.TechnologyId = this.TechnologyId;
            obj.PARTSDRAWINGNO = this.PARTSDRAWINGNO;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.ProcessNo = this.ProcessNo;
            obj.ProcessName = this.ProcessName;
            obj.UnitTime = this.UnitTime;
            obj.ProgramEngineer = this.ProgramEngineer;
            obj.ProgramName = this.ProgramName;
            obj.ProgramFname = this.ProgramFname;
            obj.ProgramPath = this.ProgramPath;
            obj.ToolName = this.ToolName;
            obj.ToolPath = this.ToolPath;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.RealDate = this.RealDate;
            obj.PlanDate = this.PlanDate;
            return obj;
        }

        public void CopyTo(ProgramInfo obj)
        {
            obj.ID = this.ID;
            obj.TechnologyId = this.TechnologyId;
            obj.PARTSDRAWINGNO = this.PARTSDRAWINGNO;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.MachineType = this.MachineType;
            obj.MactypeCode = this.MactypeCode;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.ProcessNo = this.ProcessNo;
            obj.ProcessName = this.ProcessName;
            obj.UnitTime = this.UnitTime;
            obj.ProgramEngineer = this.ProgramEngineer;
            obj.ProgramName = this.ProgramName;
            obj.ProgramFname = this.ProgramFname;
            obj.ProgramPath = this.ProgramPath;
            obj.ToolName = this.ToolName;
            obj.ToolPath = this.ToolPath;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.RealDate = this.RealDate;
            obj.PlanDate = this.PlanDate;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class ProgramInfoMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta TechnologyId = new StringPropertyMeta("\"TECHNOLOGY_ID\"");
        public StringPropertyMeta PARTSDRAWINGNO = new StringPropertyMeta("\"PARTSDRAWINGNO\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta CustCode = new StringPropertyMeta("\"CUST_CODE\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta ProductCode = new StringPropertyMeta("\"PRODUCT_CODE\"");
        public StringPropertyMeta MachineType = new StringPropertyMeta("\"MACHINE_TYPE\"");
        public StringPropertyMeta MactypeCode = new StringPropertyMeta("\"MACTYPE_CODE\"");
        public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
        public StringPropertyMeta StatusMemo = new StringPropertyMeta("\"STATUS_MEMO\"");
        public StringPropertyMeta ProcessNo = new StringPropertyMeta("\"PROCESS_NO\"");
        public StringPropertyMeta ProcessName = new StringPropertyMeta("\"PROCESS_NAME\"");
        public PropertyMeta UnitTime = new PropertyMeta("\"UNIT_TIME\"");
        public StringPropertyMeta ProgramEngineer = new StringPropertyMeta("\"PROGRAM_ENGINEER\"");
        public StringPropertyMeta ProgramName = new StringPropertyMeta("\"PROGRAM_NAME\"");
        public StringPropertyMeta ProgramFname = new StringPropertyMeta("\"PROGRAM_FNAME\"");
        public StringPropertyMeta ProgramPath = new StringPropertyMeta("\"PROGRAM_PATH\"");
        public StringPropertyMeta ToolName = new StringPropertyMeta("\"TOOL_NAME\"");
        public StringPropertyMeta ToolPath = new StringPropertyMeta("\"TOOL_PATH\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public DatetimePropertyMeta RealDate = new DatetimePropertyMeta("\"REAL_DATE\"");
        public DatetimePropertyMeta PlanDate = new DatetimePropertyMeta("\"PLAN_DATE\"");
    }
    #endregion
}

