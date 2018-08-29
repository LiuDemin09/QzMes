using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{	
	[Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.SYS_ERROR_LOG", IsView = false, PrimaryKeys = "ID",
	PrimaryProperties="ID")]
	public class SysErrorLog : ICloneable
	{
		#region Member Variables		
		public static SysErrorLogMeta Meta = new SysErrorLogMeta();
		#endregion
		
		#region constructor
		public SysErrorLog()
		{
			///Initialize Child collection objects
		}
		#endregion
		
		#region Property Variables		
		[OrmPropertyAttribute(IsChild=false,IsPK=true,IsFK=false,IsIdentity=false,IsUnique=true,
		AllowNull=false,ColumnName="ID",SqlType="VARCHAR2",Length=20)]
		public string ID{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="SITE",SqlType="VARCHAR2",Length=10)]
		public string SITE{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="BU",SqlType="VARCHAR2",Length=10)]
		public string BU{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="USER_CODE",SqlType="VARCHAR2",Length=20)]
		public string UserCode{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="FUNC_CODE",SqlType="VARCHAR2",Length=20)]
		public string FuncCode{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="LOG_TYPE",SqlType="VARCHAR2",Length=10)]
		public string LogType{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="INFO",SqlType="NVARCHAR2",Length=4000)]
		public string INFO{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="CREATED_DATE",SqlType="DATE",Length=0)]
		public DateTime? CreatedDate{set;get;}
		
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
			SysErrorLog obj = new SysErrorLog();
						
			obj.ID = this.ID;
			
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.UserCode = this.UserCode;
			obj.FuncCode = this.FuncCode;
			obj.LogType = this.LogType;
			obj.INFO = this.INFO;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
			
			return obj;
		}
		
		public void CopyTo(SysErrorLog obj)
		{			
			obj.ID = this.ID;
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.UserCode = this.UserCode;
			obj.FuncCode = this.FuncCode;
			obj.LogType = this.LogType;
			obj.INFO = this.INFO;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
		}
		#endregion
	}
	
	#region "Metadata struct"
    public sealed class SysErrorLogMeta
    {
		public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
		public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
		public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
		public StringPropertyMeta UserCode = new StringPropertyMeta("\"USER_CODE\"");
		public StringPropertyMeta FuncCode = new StringPropertyMeta("\"FUNC_CODE\"");
		public StringPropertyMeta LogType = new StringPropertyMeta("\"LOG_TYPE\"");
		public StringPropertyMeta INFO = new StringPropertyMeta("\"INFO\"");
		public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
		public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
		public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

