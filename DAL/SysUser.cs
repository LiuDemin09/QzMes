using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{	
	[Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.SYS_USER", IsView = false, PrimaryKeys = "USER_CODE",
	PrimaryProperties="UserCode")]
	public class SysUser : ICloneable
	{
		#region Member Variables		
		public static SysUserMeta Meta = new SysUserMeta();
		#endregion
		
		#region constructor
		public SysUser()
		{
			///Initialize Child collection objects
		}
		#endregion
		
		#region Property Variables		
		[OrmPropertyAttribute(IsChild=false,IsPK=true,IsFK=false,IsIdentity=false,IsUnique=true,
		AllowNull=false,ColumnName="USER_CODE",SqlType="VARCHAR2",Length=20)]
		public string UserCode{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="PWD",SqlType="VARCHAR2",Length=30)]
		public string PWD{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="USER_NAME",SqlType="NVARCHAR2",Length=120)]
		public string UserName{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="SITE",SqlType="VARCHAR2",Length=10)]
		public string SITE{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="BU",SqlType="VARCHAR2",Length=10)]
		public string BU{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="DEPT_CODE",SqlType="VARCHAR2",Length=20)]
		public string DeptCode{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="DEPT_NAME",SqlType="NVARCHAR2",Length=600)]
		public string DeptName{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="USER_TYPE",SqlType="NUMBER",Length=0)]
		public decimal? UserType{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="LOGIN_MODE",SqlType="NUMBER",Length=0)]
		public decimal? LoginMode{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="STATUS",SqlType="CHAR",Length=1)]
		public string STATUS{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="QUESTION_TYPE",SqlType="NUMBER",Length=0)]
		public decimal? QuestionType{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="ANSWER",SqlType="VARCHAR2",Length=200)]
		public string ANSWER{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="EMAIL",SqlType="VARCHAR2",Length=100)]
		public string EMAIL{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="TEL",SqlType="VARCHAR2",Length=30)]
		public string TEL{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="MOBILE",SqlType="VARCHAR2",Length=30)]
		public string MOBILE{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="WECHAT",SqlType="VARCHAR2",Length=100)]
		public string WECHAT { set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="CREATED_DATE",SqlType="DATE",Length=0)]
		public DateTime? CreatedDate{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="VALID_DAYS",SqlType="NUMBER",Length=0)]
		public decimal? ValidDays{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="UPDATED_DATE",SqlType="DATE",Length=0)]
		public DateTime? UpdatedDate{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="UPDATED_BY",SqlType="VARCHAR2",Length=20)]
		public string UpdatedBy{set;get;}
		
		#endregion
		#region "Child properties"
		#endregion
		
		#region extension methods
		public Object Clone()
		{
			SysUser obj = new SysUser();
						
			obj.UserCode = this.UserCode;
			
			obj.PWD = this.PWD;
			obj.UserName = this.UserName;
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.DeptCode = this.DeptCode;
			obj.DeptName = this.DeptName;
			obj.UserType = this.UserType;
			obj.LoginMode = this.LoginMode;
			obj.STATUS = this.STATUS;
			obj.QuestionType = this.QuestionType;
			obj.ANSWER = this.ANSWER;
			obj.EMAIL = this.EMAIL;
			obj.TEL = this.TEL;
			obj.MOBILE = this.MOBILE;
			obj.WECHAT = this.WECHAT;
			obj.CreatedDate = this.CreatedDate;
			obj.ValidDays = this.ValidDays;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
			
			return obj;
		}
		
		public void CopyTo(SysUser obj)
		{			
			obj.UserCode = this.UserCode;
			obj.PWD = this.PWD;
			obj.UserName = this.UserName;
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.DeptCode = this.DeptCode;
			obj.DeptName = this.DeptName;
			obj.UserType = this.UserType;
			obj.LoginMode = this.LoginMode;
			obj.STATUS = this.STATUS;
			obj.QuestionType = this.QuestionType;
			obj.ANSWER = this.ANSWER;
			obj.EMAIL = this.EMAIL;
			obj.TEL = this.TEL;
			obj.MOBILE = this.MOBILE;
			obj.WECHAT = this.WECHAT;
			obj.CreatedDate = this.CreatedDate;
			obj.ValidDays = this.ValidDays;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
		}
		#endregion
	}
	
	#region "Metadata struct"
    public sealed class SysUserMeta
    {
		public StringPropertyMeta UserCode = new StringPropertyMeta("\"USER_CODE\"");
		public StringPropertyMeta PWD = new StringPropertyMeta("\"PWD\"");
		public StringPropertyMeta UserName = new StringPropertyMeta("\"USER_NAME\"");
		public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
		public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
		public StringPropertyMeta DeptCode = new StringPropertyMeta("\"DEPT_CODE\"");
		public StringPropertyMeta DeptName = new StringPropertyMeta("\"DEPT_NAME\"");
		public PropertyMeta UserType = new PropertyMeta("\"USER_TYPE\"");
		public PropertyMeta LoginMode = new PropertyMeta("\"LOGIN_MODE\"");
		public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
		public PropertyMeta QuestionType = new PropertyMeta("\"QUESTION_TYPE\"");
		public StringPropertyMeta ANSWER = new StringPropertyMeta("\"ANSWER\"");
		public StringPropertyMeta EMAIL = new StringPropertyMeta("\"EMAIL\"");
		public StringPropertyMeta TEL = new StringPropertyMeta("\"TEL\"");
		public StringPropertyMeta MOBILE = new StringPropertyMeta("\"MOBILE\"");
		public StringPropertyMeta WECHAT = new StringPropertyMeta("\"WECHAT\"");
		public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
		public PropertyMeta ValidDays = new PropertyMeta("\"VALID_DAYS\"");
		public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
		public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

