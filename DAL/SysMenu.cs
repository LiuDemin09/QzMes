using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{	
	[Serializable()]
    [OrmClassAttribute(TableName = "MES_MASTER.SYS_MENU", IsView = false, PrimaryKeys = "ID",
	PrimaryProperties="ID")]
	public class SysMenu : ICloneable
	{
		#region Member Variables		
		public static SysMenuMeta Meta = new SysMenuMeta();
		#endregion
		
		#region constructor
		public SysMenu()
		{
			///Initialize Child collection objects
		}
        #endregion

        #region Property Variables	
        [OrmPropertyAttribute(IsChild = false, IsPK = true, IsFK = false, IsIdentity = false, IsUnique = true,
        AllowNull = false, ColumnName = "ID", SqlType = "VARCHAR2", Length = 20)]
        public string ID { set; get; }

        [OrmPropertyAttribute(IsChild=false,IsPK= false, IsFK=false,IsIdentity=false,IsUnique=true,
		AllowNull=false,ColumnName="CODE",SqlType="VARCHAR2",Length=20)]
		public string CODE{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="NAME",SqlType="VARCHAR2",Length=100)]
		public string Name{set;get;}

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PAGE_URL", SqlType = "VARCHAR2", Length = 500)]
        public string PageUrl { set; get; }
 
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="LEVEL_NO",SqlType="NUMBER",Length=0)]
		public decimal? LevelNo{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="IS_ROOT",SqlType="NUMBER",Length=0)]
		public decimal? IsRoot{set;get;}
		
		[OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="PARENT_CODE",SqlType="VARCHAR2",Length=20)]
		public string ParentCode{set;get;}

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "BU", SqlType = "VARCHAR2", Length = 10)]
        public string BU { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SITE", SqlType = "VARCHAR2", Length = 10)]
        public string SITE { set; get; }

        [OrmPropertyAttribute(IsChild=false,IsPK=false,IsFK=false,IsIdentity=false,IsUnique=false,
		AllowNull=true,ColumnName="MEMO",SqlType="NVARCHAR2",Length=300)]
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
			SysMenu obj = new SysMenu();
            obj.ID = this.ID;			
			obj.CODE = this.CODE;			
			obj.Name = this.Name;
			obj.PageUrl = this.PageUrl; 
			obj.LevelNo = this.LevelNo;
			obj.IsRoot = this.IsRoot;
			obj.ParentCode = this.ParentCode;
            obj.BU = this.BU;
            obj.SITE = this.SITE;
			obj.MEMO = this.MEMO;
			obj.CreatedDate = this.CreatedDate;
			obj.UpdatedDate = this.UpdatedDate;
			obj.UpdatedBy = this.UpdatedBy;
			
			return obj;
		}
		
		public void CopyTo(SysMenu obj)
		{
            obj.ID = this.ID;
            obj.CODE = this.CODE;
            obj.Name = this.Name;
            obj.PageUrl = this.PageUrl;
            obj.LevelNo = this.LevelNo;
            obj.IsRoot = this.IsRoot;
            obj.ParentCode = this.ParentCode;
            obj.BU = this.BU;
            obj.SITE = this.SITE;
            obj.MEMO = this.MEMO;
            obj.CreatedDate = this.CreatedDate;
            obj.UpdatedDate = this.UpdatedDate;
            obj.UpdatedBy = this.UpdatedBy;
        }
		#endregion
	}
	
	#region "Metadata struct"
    public sealed class SysMenuMeta
    {
        public StringPropertyMeta ID = new StringPropertyMeta("\"ID\"");
        public StringPropertyMeta CODE = new StringPropertyMeta("\"CODE\"");
		public StringPropertyMeta Name = new StringPropertyMeta("\"NAME\"");
		public StringPropertyMeta PageUrl = new StringPropertyMeta("\"PAGE_URL\""); 
		public PropertyMeta LevelNo = new PropertyMeta("\"LEVEL_NO\"");
		public PropertyMeta IsRoot = new PropertyMeta("\"IS_ROOT\"");
		public StringPropertyMeta ParentCode = new StringPropertyMeta("\"PARENT_CODE\"");
        public StringPropertyMeta BU = new StringPropertyMeta("\"PARENT_CODE\"");
        public StringPropertyMeta SITE = new StringPropertyMeta("\"SITE\"");
        public StringPropertyMeta MEMO = new StringPropertyMeta("\"MEMO\"");
		public DatetimePropertyMeta CreatedDate = new DatetimePropertyMeta("\"CREATED_DATE\"");
		public DatetimePropertyMeta UpdatedDate = new DatetimePropertyMeta("\"UPDATED_DATE\"");
		public StringPropertyMeta UpdatedBy = new StringPropertyMeta("\"UPDATED_BY\"");
    }
    #endregion
}

