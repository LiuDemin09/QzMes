using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    [Serializable()]
    [OrmClassAttribute(TableName = "BASEDATA.SYS_USER", IsView = false, PrimaryKeys = "USER_CODE",
    PrimaryProperties = "UserCode")]
    public class SysUserPub : ICloneable
    {
        #region Member Variables		
        public static SysUserMeta Meta = new SysUserMeta();
        #endregion

        #region constructor
        public SysUserPub()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "USER_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string UserCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = false, ColumnName = "USER_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string UserName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "LOGIN_NAME", SqlType = "VARCHAR2", Length = 40)]
        public string LoginName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PASSWORD", SqlType = "VARCHAR2", Length = 200)]
        public string PASSWORD { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DEPT_CODE", SqlType = "VARCHAR2", Length = 40)]
        public string DeptCode { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "DEPT_NAME", SqlType = "VARCHAR2", Length = 100)]
        public string DeptName { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "PHONE", SqlType = "VARCHAR2", Length = 100)]
        public string PHONE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "EMAIL", SqlType = "VARCHAR2", Length = 100)]
        public string EMAIL { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "WECHAT", SqlType = "VARCHAR2", Length = 40)]
        public string WECHAT { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ACTIVE", SqlType = "CHAR", Length = 2)]
        public string ACTIVE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATE_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? CreateTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CREATE_BY", SqlType = "VARCHAR2", Length = 40)]
        public string CreateBy { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "UPDATE_TIME", SqlType = "DATE", Length = 0)]
        public DateTime? UpdateTime { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "IP_ADDRESS", SqlType = "VARCHAR2", Length = 40)]
        public string IpAddress { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "ROLE", SqlType = "VARCHAR2", Length = 40)]
        public string ROLE { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "CHANGEPWD_FLAG", SqlType = "CHAR", Length = 2)]
        public string ChangepwdFlag { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "MACHINE_ID", SqlType = "VARCHAR2", Length = 100)]
        public string MachineId { set; get; }

        [OrmPropertyAttribute(IsChild = false, IsPK = false, IsFK = false, IsIdentity = false, IsUnique = false,
        AllowNull = true, ColumnName = "SYSTEM_DEFINE", SqlType = "VARCHAR2", Length = 100)]
        public string SystemDefine { set; get; }

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            SysUserPub obj = new SysUserPub();


            obj.UserCode = this.UserCode;
            obj.UserName = this.UserName;
            obj.LoginName = this.LoginName;
            obj.PASSWORD = this.PASSWORD;
            obj.DeptCode = this.DeptCode;
            obj.DeptName = this.DeptName;
            obj.PHONE = this.PHONE;
            obj.EMAIL = this.EMAIL;
            obj.WECHAT = this.WECHAT;
            obj.ACTIVE = this.ACTIVE;
            obj.CreateTime = this.CreateTime;
            obj.CreateBy = this.CreateBy;
            obj.UpdateTime = this.UpdateTime;
            obj.IpAddress = this.IpAddress;
            obj.ROLE = this.ROLE;
            obj.ChangepwdFlag = this.ChangepwdFlag;
            obj.MachineId = this.MachineId;
            obj.SystemDefine = this.SystemDefine;

            return obj;
        }

        public void CopyTo(SysUserPub obj)
        {
            obj.UserCode = this.UserCode;
            obj.UserName = this.UserName;
            obj.LoginName = this.LoginName;
            obj.PASSWORD = this.PASSWORD;
            obj.DeptCode = this.DeptCode;
            obj.DeptName = this.DeptName;
            obj.PHONE = this.PHONE;
            obj.EMAIL = this.EMAIL;
            obj.WECHAT = this.WECHAT;
            obj.ACTIVE = this.ACTIVE;
            obj.CreateTime = this.CreateTime;
            obj.CreateBy = this.CreateBy;
            obj.UpdateTime = this.UpdateTime;
            obj.IpAddress = this.IpAddress;
            obj.ROLE = this.ROLE;
            obj.ChangepwdFlag = this.ChangepwdFlag;
            obj.MachineId = this.MachineId;
            obj.SystemDefine = this.SystemDefine;
        }
        #endregion
    }

    #region "Metadata struct"
    public sealed class SysUserPubMeta
    {
        public StringPropertyMeta UserCode = new StringPropertyMeta("\"USER_CODE\"");
        public StringPropertyMeta UserName = new StringPropertyMeta("\"USER_NAME\"");
        public StringPropertyMeta LoginName = new StringPropertyMeta("\"LOGIN_NAME\"");
        public StringPropertyMeta PASSWORD = new StringPropertyMeta("\"PASSWORD\"");
        public StringPropertyMeta DeptCode = new StringPropertyMeta("\"DEPT_CODE\"");
        public StringPropertyMeta DeptName = new StringPropertyMeta("\"DEPT_NAME\"");
        public StringPropertyMeta PHONE = new StringPropertyMeta("\"PHONE\"");
        public StringPropertyMeta EMAIL = new StringPropertyMeta("\"EMAIL\"");
        public StringPropertyMeta WECHAT = new StringPropertyMeta("\"WECHAT\"");
        public StringPropertyMeta ACTIVE = new StringPropertyMeta("\"ACTIVE\"");
        public DatetimePropertyMeta CreateTime = new DatetimePropertyMeta("\"CREATE_TIME\"");
        public StringPropertyMeta CreateBy = new StringPropertyMeta("\"CREATE_BY\"");
        public DatetimePropertyMeta UpdateTime = new DatetimePropertyMeta("\"UPDATE_TIME\"");
        public StringPropertyMeta IpAddress = new StringPropertyMeta("\"IP_ADDRESS\"");
        public StringPropertyMeta ROLE = new StringPropertyMeta("\"ROLE\"");
        public StringPropertyMeta ChangepwdFlag = new StringPropertyMeta("\"CHANGEPWD_FLAG\"");
        public StringPropertyMeta MachineId = new StringPropertyMeta("\"MACHINE_ID\"");
        public StringPropertyMeta SystemDefine = new StringPropertyMeta("\"SYSTEM_DEFINE\"");
    }
    #endregion
}

