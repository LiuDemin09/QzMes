using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{	
	[Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.SYS_ROLE_USER", IsView = false, PrimaryKeys = "ID",
	PrimaryProperties="ID")]
	public class SysRoleUser : ICloneable
	{
		#region Member Variables		
		public static SysRoleUserMeta Meta = new SysRoleUserMeta();
		#endregion
		
		#region constructor
		public SysRoleUser()
		{
			///Initialize Child collection objects
		}
		#endregion
		
		#region Property Variables		
		[OrmPropertyAttribute(IsChild=false,IsPK=true,IsFK=false,IsIdentity=false,IsUnique=true,
		AllowNull=false,ColumnName="ID",SqlType="VARCHAR2",Length=20)]
		public string ID{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="ROLE_ID",SqlType="VARCHAR2",Length=20)]
		public string RoleId{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="USER_CODE",SqlType="VARCHAR2",Length=20)]
		public string UserCode{set;get;}
		
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
			SysRoleUser obj = new SysRoleUser();
						
			obj.ID = this.ID;
			
			obj.RoleId = this.RoleId;
			obj.UserCode = this.UserCode;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
			
			return obj;
		}
		
		public void CopyTo(SysRoleUser obj)
		{			
			obj.ID = this.ID;
			obj.RoleId = this.RoleId;
			obj.UserCode = this.UserCode;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
		}
		#endregion
	}
	
	#region "Metadata struct"
    public sealed class SysRoleUserMeta
    {
		public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
		public StringPropertyMeta RoleId = new StringPropertyMeta("\"ROLE_ID\"");
		public StringPropertyMeta UserCode = new StringPropertyMeta("\"USER_CODE\"");
		public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
		public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
		public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

