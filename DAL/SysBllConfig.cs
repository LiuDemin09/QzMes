using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{	
	[Serializable()]
	[OrmClassAttribute(TableName= "MES_MASTER.SYS_BLL_CONFIG", IsView=false,PrimaryKeys="ID",
	PrimaryProperties="ID")]
	public class SysBllConfig : ICloneable
	{
		#region Member Variables		
		public static SysBllConfigMeta Meta = new SysBllConfigMeta();
		#endregion
		
		#region constructor
		public SysBllConfig()
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
		AllowNull=true,ColumnName="BASE_NAME",SqlType="VARCHAR2",Length=100)]
		public string BaseName{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="BLL_NAME",SqlType="VARCHAR2",Length=100)]
		public string BllName{set;get;}
		
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
			SysBllConfig obj = new SysBllConfig();
						
			obj.ID = this.ID;
			
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.BaseName = this.BaseName;
			obj.BllName = this.BllName;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
			
			return obj;
		}
		
		public void CopyTo(SysBllConfig obj)
		{			
			obj.ID = this.ID;
			obj.SITE = this.SITE;
			obj.BU = this.BU;
			obj.BaseName = this.BaseName;
			obj.BllName = this.BllName;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
		}
		#endregion
	}
	
	#region "Metadata struct"
    public sealed class SysBllConfigMeta
    {
		public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
		public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
		public StringPropertyMeta BU = new StringPropertyMeta("\"BU\"");
		public StringPropertyMeta BaseName = new StringPropertyMeta("\"BASE_NAME\"");
		public StringPropertyMeta BllName = new StringPropertyMeta("\"BLL_NAME\"");
		public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
		public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
		public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

