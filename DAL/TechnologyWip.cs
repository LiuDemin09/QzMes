using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.TECHNOLOGY_WIP", IsView = false, PrimaryKeys = "PARTSDRAWINGNO",
    PrimaryProperties = "PARTSDRAWINGNO")]
    public class TechnologyWip : ICloneable
    {
        #region Member Variables		
        public static TechnologyWipMeta Meta = new TechnologyWipMeta();
        #endregion

        #region constructor
        public TechnologyWip()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
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
        AllowNull = true, ColumnName = "STATUS", SqlType = "NUMBER", Length = 0)]
        public decimal? STATUS { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "STATUS_MEMO", SqlType = "CHAR", Length = 40)]
        public string StatusMemo { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROCESS_ENGINEER", SqlType = "VARCHAR2", Length = 40)]
        public string ProcessEngineer { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROCESS_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProcessName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROGRAM_ENGINEER", SqlType = "VARCHAR2", Length = 40)]
        public string ProgramEngineer { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROGRAM_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string ProgramName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROCESS_FNAME", SqlType = "VARCHAR2", Length = 100)]
        public string ProcessFname { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PROCESS_PATH", SqlType = "VARCHAR2", Length = 400)]
        public string ProcessPath { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PLAN_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? PlanDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "REAL_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? RealDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
       AllowNull = true, ColumnName = "DEVPLAN_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? DevplanDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DEVREAL_DATE", SqlType = "DATE", Length = 0)]
        public DateTime? DevrealDate { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "QZPARTSDRAWING", SqlType = "VARCHAR2", Length = 200)]
        public string QZPARTSDRAWING { set; get; }

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
            TechnologyWip obj = new TechnologyWip();
             
            obj.PARTSDRAWINGNO = this.PARTSDRAWINGNO;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.ProcessEngineer = this.ProcessEngineer;
            obj.ProcessName = this.ProcessName;
            obj.ProgramEngineer = this.ProgramEngineer;
            obj.ProgramName = this.ProgramName;
            obj.ProcessFname = this.ProcessFname;
            obj.ProcessPath = this.ProcessPath;
            obj.PlanDate = this.PlanDate;
            obj.RealDate = this.RealDate;
            obj.QZPARTSDRAWING = this.QZPARTSDRAWING;
            obj.ACTIVE = this.ACTIVE;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.DevplanDate = this.DevplanDate;
            obj.DevrealDate = this.DevrealDate;
            return obj;
        }

        public void CopyTo(TechnologyWip obj)
        { 
            obj.PARTSDRAWINGNO = this.PARTSDRAWINGNO;
            obj.CustName = this.CustName;
            obj.CustCode = this.CustCode;
            obj.ProductName = this.ProductName;
            obj.ProductCode = this.ProductCode;
            obj.STATUS = this.STATUS;
            obj.StatusMemo = this.StatusMemo;
            obj.ProcessEngineer = this.ProcessEngineer;
            obj.ProcessName = this.ProcessName;
            obj.ProgramEngineer = this.ProgramEngineer;
            obj.ProgramName = this.ProgramName;
            obj.ProcessFname = this.ProcessFname;
            obj.ProcessPath = this.ProcessPath;
            obj.PlanDate = this.PlanDate;
            obj.RealDate = this.RealDate;
            obj.QZPARTSDRAWING = this.QZPARTSDRAWING;
            obj.ACTIVE = this.ACTIVE;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
            obj.DevplanDate = this.DevplanDate;
            obj.DevrealDate = this.DevrealDate;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class TechnologyWipMeta
    { 
        public StringPropertyMeta PARTSDRAWINGNO = new StringPropertyMeta("\"PARTSDRAWINGNO\"");
        public StringPropertyMeta CustName = new StringPropertyMeta("\"CUST_NAME\"");
        public StringPropertyMeta CustCode = new StringPropertyMeta("\"CUST_CODE\"");
        public StringPropertyMeta ProductName = new StringPropertyMeta("\"PRODUCT_NAME\"");
        public StringPropertyMeta ProductCode = new StringPropertyMeta("\"PRODUCT_CODE\"");
        public PropertyMeta STATUS = new PropertyMeta("\"STATUS\"");
        public StringPropertyMeta StatusMemo = new StringPropertyMeta("\"STATUS_MEMO\"");
        public StringPropertyMeta ProcessEngineer = new StringPropertyMeta("\"PROCESS_ENGINEER\"");
        public StringPropertyMeta ProcessName = new StringPropertyMeta("\"PROCESS_NAME\"");
        public StringPropertyMeta ProgramEngineer = new StringPropertyMeta("\"PROGRAM_ENGINEER\"");
        public StringPropertyMeta ProgramName = new StringPropertyMeta("\"PROGRAM_NAME\"");
        public StringPropertyMeta ProcessFname = new StringPropertyMeta("\"PROCESS_FNAME\"");
        public StringPropertyMeta ProcessPath = new StringPropertyMeta("\"PROCESS_PATH\"");
        public DatetimePropertyMeta PlanDate = new DatetimePropertyMeta("\"PLAN_DATE\"");
        public DatetimePropertyMeta RealDate = new DatetimePropertyMeta("\"REAL_DATE\"");
        public StringPropertyMeta QZPARTSDRAWING = new StringPropertyMeta("\"QZPARTSDRAWING\"");
        public StringPropertyMeta ACTIVE = new StringPropertyMeta("\"ACTIVE\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
        public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
        public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
        public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
        public DatetimePropertyMeta DevplanDate = new DatetimePropertyMeta("\"DEVPLAN_DATE\"");
        public DatetimePropertyMeta DevrealDate = new DatetimePropertyMeta("\"DEVREAL_DATE\"");
    }
    #endregion
}

