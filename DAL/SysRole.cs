using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{	
	[Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.SYS_ROLE", IsView = false, PrimaryKeys = "ID",
	PrimaryProperties="ID")]
	public class SysRole : ICloneable
	{
		#region Member Variables		
		public static SysRoleMeta Meta = new SysRoleMeta();
		#endregion
		
		#region constructor
		public SysRole()
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
		AllowNull=true,ColumnName="ROLE_NAME",SqlType="VARCHAR2",Length=20)]
		public string RoleName{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="MEMO",SqlType="VARCHAR2",Length=100)]
		public string MEMO{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="STATUS",SqlType="CHAR",Length=1)]
		public string STATUS{set;get;}
		
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
			SysRole obj = new SysRole();
						
			obj.ID = this.ID;
			
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.RoleName = this.RoleName;
			obj.MEMO = this.MEMO;
			obj.STATUS = this.STATUS;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
			
			return obj;
		}
		
		public void CopyTo(SysRole obj)
		{			
			obj.ID = this.ID;
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.RoleName = this.RoleName;
			obj.MEMO = this.MEMO;
			obj.STATUS = this.STATUS;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
		}
		#endregion
	}
	
	#region "Metadata struct"
    public sealed class SysRoleMeta
    {
		public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
		public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
		public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
		public StringPropertyMeta RoleName = new StringPropertyMeta("\"ROLE_NAME\"");
		public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
		public StringPropertyMeta STATUS = new StringPropertyMeta("\"STATUS\"");
		public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
		public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
		public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

