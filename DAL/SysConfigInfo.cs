using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{	
	[Serializable()]
	[OrmClassAttribute(TableName= "MES_MASTER.SYS_CONFIG_INFO", IsView=false,PrimaryKeys="ID",
	PrimaryProperties="ID")]
	public class SysConfigInfo : ICloneable
	{
		#region Member Variables		
		public static SysConfigInfoMeta Meta = new SysConfigInfoMeta();
		#endregion
		
		#region constructor
		public SysConfigInfo()
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
		AllowNull=true,ColumnName="KEY_NAME",SqlType="VARCHAR2",Length=10)]
		public string KeyName{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="ITEM_TEXT",SqlType="NVARCHAR2",Length=1800)]
		public string ItemText{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="ITEM_VALUE",SqlType="NVARCHAR2",Length=1800)]
		public string ItemValue{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="MEMO",SqlType="NVARCHAR2",Length=1800)]
		public string MEMO{set;get;}
		
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
			SysConfigInfo obj = new SysConfigInfo();
						
			obj.ID = this.ID;
			
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.KeyName = this.KeyName;
			obj.ItemText = this.ItemText;
			obj.ItemValue = this.ItemValue;
			obj.MEMO = this.MEMO;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
			
			return obj;
		}
		
		public void CopyTo(SysConfigInfo obj)
		{			
			obj.ID = this.ID;
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.KeyName = this.KeyName;
			obj.ItemText = this.ItemText;
			obj.ItemValue = this.ItemValue;
			obj.MEMO = this.MEMO;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
		}
		#endregion
	}
	
	#region "Metadata struct"
    public sealed class SysConfigInfoMeta
    {
		public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
		public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
		public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
		public StringPropertyMeta KeyName = new StringPropertyMeta("\"KEY_NAME\"");
		public StringPropertyMeta ItemText = new StringPropertyMeta("\"ITEM_TEXT\"");
		public StringPropertyMeta ItemValue = new StringPropertyMeta("\"ITEM_VALUE\"");
		public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
		public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
		public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
		public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

