using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freeworks.Common;
using Freeworks.Common.Cryptography;
using Freeworks.ORM.Core;
using System.IO;
using DAL;
using System.Collections;

namespace BLL
{
    public class SystemBO : BOBase
    {
        string tplPath = "";
        public SystemBO(UserInfo userInfo)
           : base(userInfo)
        {
            //
            // TODO: Add constructor logic here
            //
            //打印模版存放路徑
            tplPath = ConstantsHelper.GetHelper(this.UserSite, this.UserBU).S_LABEL_TPL_PATH + "\\" + this.UserSite + this.UserBU;
            if (!Directory.Exists(tplPath))
            {
                Directory.CreateDirectory(tplPath);
            }
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public String Encrypt(String original)
        {
            return CryptographyManager.Instance.SymmetricEncrpyt(original, CryptographyManager.DEFAULT_KEY);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public String Decrypt(String dist)
        {
            return CryptographyManager.Instance.SymmetricDecrpyt(dist, CryptographyManager.DEFAULT_KEY);
        }

        public IList<SysUser> FindUsers(string userCode, string userName)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(userCode))
            {
                ce = (SysUser.Meta.UserCode == userCode);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                ce =( ce & SysUser.Meta.UserName.Like(userName));
            }

            //if (!string.IsNullOrEmpty(deptCode))
            //{
            //    ce = ce & (SysUser.Meta.DeptCode.Like(deptCode));
            //}

            // ce = ce & SysUser.Meta.SITE == this.UserSite;

            if (ce == null)
            {
                return DBContext.LoadArray<SysUser>();
            }

            return DBContext.FindArray<SysUser>(ce);

        }

        public IList<SysUser> FindPublicUsers(string userCode, string userName)
        {
            string sql = @"select user_code,user_name,dept_name,phone mobile from basedata.sys_user where user_code not in(
                                select user_code from mes_master.sys_user) ";
            if (!string.IsNullOrEmpty(userCode))
            {
                sql += "and user_code='" + userCode + "'";
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sql += "and user_name='" + userName + "'";
            }
            sql += " order by user_code";
            //string sql = "SELECT * FROM MES_MASTER.SYS_USER WHERE USER_CODE IN (SELECT USER_CODE FROM MES_MASTER.SYS_ROLE_USER WHERE ROLE_ID = '{0}')";
            //sql = string.Format(sql, roleId);
            return DBContext.ExcuteSql(sql).ToBusiObjects<SysUser>();

        }

        public SysUser FindUserByCode(string userCode)
        {
            return DBContext.Find<SysUser>(SysUser.Meta.UserCode == userCode);
        }
        public IList<SysUser> FindPublicUserByCode(string userCode)
        {
            string sql = @"select user_code,user_name,dept_name,phone mobile from basedata.sys_user where user_code not in(
                                select user_code from mes_master.sys_user) ";
            if (!string.IsNullOrEmpty(userCode))
            {
                sql += "and user_code='" + userCode + "'";
            }


            //string sql = "SELECT * FROM MES_MASTER.SYS_USER WHERE USER_CODE IN (SELECT USER_CODE FROM MES_MASTER.SYS_ROLE_USER WHERE ROLE_ID = '{0}')";
            //sql = string.Format(sql, roleId);
            return DBContext.ExcuteSql(sql).ToBusiObjects<SysUser>();

        }
        public void SaveUser(SysUser user)
        {
            user.UserCode = user.UserCode.ToUpper();
            if (!DBContext.Exist<SysUser>(SysUser.Meta.UserCode == user.UserCode))
            {
                user.CreatedDate = DateTime.Now;
                user.PWD = CryptographyManager.Instance.SymmetricEncrpyt(BLLConstants.DEAULT_PWD, CryptographyManager.DEFAULT_KEY);
            }
            user.UpdatedBy = this.UserCode;
            user.UpdatedDate = DateTime.Now;
            //user.BU = this.UserBU;
            //user.SITE = this.UserSite;

            DBContext.SaveAndUpdate<SysUser>(user);
        }

        public void RemoveUser(string userCode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<SysRoleUser>(trans, SysRoleUser.Meta.UserCode == userCode);
                DBContext.Remove<SysUser>(trans, userCode);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveUser");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


        public void UpdateRoleUsers(string roleId, string[] userCodes)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                foreach (string userCode in userCodes)
                {

                    SysRoleUser ru = DBContext.Find<SysRoleUser>(trans, SysRoleUser.Meta.RoleId == roleId & SysRoleUser.Meta.UserCode == userCode);
                    if (ru == null)
                    {
                        ru = new SysRoleUser();
                        ru.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                    }
                    ru.RoleId = roleId;
                    ru.UserCode = userCode;

                    ru.UpdatedBy = this.UserCode;
                    ru.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<SysRoleUser>(trans, ru);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateUserRole");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public void UpdateUserRole(string[] items, string userCode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                //篩選出Site，Bu下的ROLE_ID
                //SysRole ssql = DBContext.Find<SysRole>(trans, SysRole.Meta.SITE == this.UserSite & SysRole.Meta.BU == this.UserBU);

                //string ssql = @"SELECT ID FROM FMGR.SYS_ROLE  WHERE SITE = '{0}' AND BU = '{1}'";
                //ssql = string.Format(ssql, this.UserSite, this.UserBU);
                //DBContext.ExcuteSql(trans, ssql);
                //刪除
                string dsql = @"DELETE  FROM MES_MASTER.SYS_ROLE_USER WHERE ROLE_ID IN (SELECT ID FROM MES_MASTER.SYS_ROLE   ) AND USER_CODE = '{0}'";
                dsql = string.Format(dsql, userCode);
                DBContext.ExcuteSql(trans, dsql).ToNonQuery(); ;
                // DBContext.Remove<SysRoleUser>(trans, SysRoleUser.Meta.UserCode == userCode & SysRoleUser.Meta.RoleId == ssql.ID);
                if (items != null && items.Length > 0)
                {
                    foreach (string item in items)
                    {
                        SysRoleUser ru = new SysRoleUser();
                        ru.RoleId = item;
                        ru.UserCode = userCode;
                        ru.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                        ru.UpdatedBy = this.UserCode;
                        ru.UpdatedDate = DateTime.Now;
                        DBContext.SaveAndUpdate<SysRoleUser>(trans, ru);
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateUserRole");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public IList<SysRole> ListRole()
        {
            return DBContext.FindArray<SysRole>(SysRole.Meta.STATUS == 1);
        }

        public IList<SysRole> ListActivedRole()
        {
            return DBContext.FindArray<SysRole>(SysRole.Meta.STATUS == 1);
        }

        public void SaveRole(SysRole role)
        {
            if (string.IsNullOrEmpty(role.ID))
            {
                role.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                role.STATUS = "1";
                role.CreatedDate = DateTime.Now;
            }
            role.UpdatedBy = this.UserCode;
            role.UpdatedDate = DateTime.Now;
            DBContext.SaveAndUpdate<SysRole>(role);
        }

        public void RemoveRole(string id)
        {
            if (DBContext.Exist<SysRoleUser>(SysRoleUser.Meta.RoleId == id))
            {
                throw new BusiException("不能删除角色，因为存在角色下的用户，请先移除用户后删除.");
            }

            if (DBContext.Exist<SysRoleMenu>(SysRoleMenu.Meta.RoleId == id))
            {
                throw new BusiException("不能删除角色，因为存在角色下的菜单，请先移除菜单后删除.");
            }

            DBContext.Remove<SysRole>(id);

        }

        public IList<SysUser> ListUsersByRole(string roleId)
        {
            string sql = "SELECT * FROM MES_MASTER.SYS_USER WHERE USER_CODE IN (SELECT USER_CODE FROM MES_MASTER.SYS_ROLE_USER WHERE ROLE_ID = '{0}')";
            // sql = ConstantsHelper.GetHelper(this.UserSite, this.UserBU).GetSqlText(this.GetType().Name, "ListUsersByRole", "SEL_USERS_BY_ROLE");
            sql = string.Format(sql, roleId);
            return DBContext.ExcuteSql(string.Format(sql, roleId)).ToBusiObjects<SysUser>();
        }

        public bool ExistRoleMenu(string roleId, string menuCode)
        {
            return DBContext.Exist<SysRoleMenu>(SysRoleMenu.Meta.RoleId == roleId &
                SysRoleMenu.Meta.MenuCode == menuCode);
        }

        public SysMenu FindMenuByCode(string code)
        {
            SysMenu vm = DBContext.Find<SysMenu>(SysMenu.Meta.CODE == code);

            return vm;
        }

        public SysMenu FindMenuByUrl(string url)
        {
            SysMenu vm = DBContext.Find<SysMenu>(SysMenu.Meta.PageUrl == url);
            return vm;
        }


        public TextValueInfo[] ListParentMenu()
        {
            IList<SysMenu> menus = DBContext.FindArray<SysMenu>(SysMenu.Meta.LevelNo == 1 | SysMenu.Meta.LevelNo == 0, SysMenu.Meta.CODE.ASC);

            TextValueInfo[] vts = new TextValueInfo[menus.Count + 1];
            for (var i = 0; i < menus.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = menus[i].CODE;
                vts[i].Text = menus[i].Name;
            }


            vts[menus.Count] = new TextValueInfo();
            vts[menus.Count].Value = "000000";
            vts[menus.Count].Text = "Root Node";

            return vts;
        }

        /// <summary>
        /// 取得用戶主菜單
        /// </summary>
        /// <returns></returns>
        public IList<SysMenu> GetMenus()
        {
            return DBContext.FindArray<SysMenu>(SysMenu.Meta.LevelNo == 1, SysMenu.Meta.CODE.ASC);

            //IDbTransaction trans = DBContext.OpenTrans();
            //try
            //{

            //   // string dsql = @"DELETE  FROM PUBLIBLX.SYS_ROLE_USER WHERE ROLE_ID IN (SELECT ID FROM PUBLIBLX.SYS_ROLE  WHERE SITE = '{0}' AND BU = '{1}') AND USER_CODE = '{2}'";
            //    string dsql = @"SELECT* FROM MES_MASTER.SYS_MENU WHERE LEVEL_NO = 1   ORDER BY CODE";
            //    dsql = string.Format(dsql, this.UserSite, this.UserBU, userCode);
            //    DBContext.ExcuteSql(trans, dsql).ToNonQuery(); ;
            //    // DBContext.Remove<SysRoleUser>(trans, SysRoleUser.Meta.UserCode == userCode & SysRoleUser.Meta.RoleId == ssql.ID);
            //    if (items != null && items.Length > 0)
            //    {
            //        foreach (string item in items)
            //        {
            //            SysRoleUser ru = new SysRoleUser();
            //            ru.RoleId = item;
            //            ru.UserCode = userCode;
            //            ru.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            //            ru.UpdatedBy = this.UserCode;
            //            ru.UpdatedDate = DateTime.Now;
            //            DBContext.SaveAndUpdate<SysRoleUser>(trans, ru);
            //        }
            //    }
            //    trans.Commit();
            //}
            //catch (Exception ex)
            //{
            //    trans.Rollback();
            //    PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateUserRole");
            //    throw ex;
            //}
            //finally
            //{
            //    DBContext.ReleaseTrans(trans);
            //}
        }
        /// <summary>
        /// 取得用戶權限子級菜單
        /// </summary>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        public IList<SysMenu> GetSubMenus(string menuCode)
        {
            IList<SysMenu> lstMenu = null;
            if (this.IsAdmin)
            {
                IDbTransaction trans = DBContext.OpenTrans();
                try
                {
                    string sql = "SELECT* FROM MES_MASTER.SYS_MENU WHERE PARENT_CODE = '{0}' ORDER BY CODE";
                    sql = string.Format(sql, menuCode);
                    lstMenu = DBContext.ExcuteSql(trans, sql).ToBusiObjects<SysMenu>();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "GetSubMenus");
                    throw ex;
                }
                finally
                {
                    DBContext.ReleaseTrans(trans);
                }

            }
            else
            {
                lstMenu = SpCaller.GetCaller(DBContext).CallGetSubmenus(this.UserCode, menuCode).ToBusiObjects<SysMenu>();
            }

            return lstMenu;
        }


        /// <summary>
        /// 列出需要認證的菜單
        /// </summary>
        /// <returns></returns>
        public IList<SysMenu> ListAuthorizabledMenus()
        {
            IList<SysMenu> lstMenu = null;

            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                string sql = " SELECT* FROM MES_MASTER.SYS_MENU t   WHERE t.LEVEL_NO = 1 ORDER BY t.Code";
                lstMenu = DBContext.ExcuteSql(trans, sql).ToBusiObjects<SysMenu>();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "ListAuthorizabledMenus");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }


            return lstMenu;
        }

        /// <summary>
        /// 列出需要認證的子菜單
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        public IList<SysMenu> ListAuthorizabledSubFuncs(string funcCode)
        {
            //if (IsAdmin)
            //{
            return DBContext.FindArray<SysMenu>(SysMenu.Meta.ParentCode == funcCode, SysMenu.Meta.CODE.ASC);
            //}
            //else
            //{
            //    return SpCaller.GetCaller(DBContext).CallGetAuthSubmenus(this.UserCode, funcCode).ToBusiObjects<SysMenu>();
            //}
        }


        /// <summary>
        /// 判定用戶權限
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="userCode"></param>
        /// <param name="permName"></param>
        /// <returns></returns>
        public bool ChkPermPrivilege(string menuCode, string userCode, string permName)
        {
            //object ret = SpCaller.GetCaller(DBContext).CallChkPermPrivilege(menuCode, userCode, permName).ExecuteOutParameters()["P_RET"];
            //return ret.ToString() == "1" ? true : false;
            return false;
        }


        /// <summary>
        /// 檢查Menu權限
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool ChkMenuPrivilege(string menuCode, string userCode)
        {
            object ret = SpCaller.GetCaller(DBContext).CallChkMenuPrivilege(menuCode, userCode).ExecuteOutParameters()["P_RET"];
            return ret.ToString() == "1" ? true : false;
        }

        /// <summary>
        /// 取得所有菜單信息
        /// </summary>
        /// <returns></returns>
        public IList<SysMenu> LoadAllMenuInfo()
        {
            // return DBContext.FindArray<SysMenu>(SysMenu.Meta.ID !=null);
            return DBContext.LoadArray<SysMenu>();
        }

        public IList<SysMenu> LoadMenuAll()
        {
            return DBContext.LoadArray<SysMenu>();
        }

        public void RemoveMenu(string code)
        {
            if (DBContext.Exist<SysRoleMenu>(SysRoleMenu.Meta.MenuCode == code))
            {
                throw new BusiException("不能删除菜单，因为存在角色在用此菜单，请先解除绑定再删除.");
            }

            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<SysMenu>(trans, SysMenu.Meta.CODE == code);

                DBContext.Remove<SysMenu>(trans, code);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveMenu");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public void SaveMenu(SysMenu vmenu)
        {
            if (string.IsNullOrEmpty(vmenu.CODE))
            {
                vmenu.CODE = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            }
             
            IDbTransaction trans = DBContext.OpenTrans();
            
            if (vmenu.ParentCode == "000000")
            {
                vmenu.IsRoot = 1;
                vmenu.LevelNo = 1;
            }
            else
            {
                vmenu.IsRoot = 0;
                vmenu.LevelNo = 2;
            }           
            try
            {
                if (DBContext.Exist<SysMenu>(SysMenu.Meta.CODE == vmenu.CODE))
                {
                    SysMenu sm = DBContext.Find<SysMenu>(SysMenu.Meta.CODE == vmenu.CODE);
                    vmenu.ID = sm.ID;
                    vmenu.UpdatedBy = this.UserCode;
                    vmenu.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<SysMenu>(trans, vmenu);
                    trans.Commit();
                    return;
                }
                SysMenu func = new SysMenu();
                func.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                func.CODE = vmenu.CODE;
                func.Name = vmenu.Name;
                func.PageUrl = vmenu.PageUrl;
                func.IsRoot = vmenu.IsRoot;
                func.LevelNo = vmenu.LevelNo;
                func.MEMO = vmenu.MEMO;
                func.ParentCode = vmenu.ParentCode;
                func.CreatedDate = DateTime.Now;
                func.UpdatedBy = this.UserCode;
                func.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<SysMenu>(trans, func);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveMenu");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public bool ExistRoleUser(string userCode, string roleId)
        {
            return DBContext.Exist<SysRoleUser>(SysRoleUser.Meta.UserCode == userCode
                & SysRoleUser.Meta.RoleId == roleId);
        }
        public void UpdateRolePerm(string[] items, string roleId)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                //Clear history data
                var rms = DBContext.FindArray<SysRoleMenu>(SysRoleMenu.Meta.RoleId == roleId);
                //foreach (SysRoleMenu rm in rms)
                //{
                //    DBContext.Remove<SysRolePerm>(trans, SysRolePerm.Meta.RoleMenuId == rm.ID);
                //}
                DBContext.Remove<SysRoleMenu>(trans, SysRoleMenu.Meta.RoleId == roleId);
                //Update the new data
               // string[] menus = items.Where(c => c.StartsWith("+")).ToArray();
                foreach (string m in items)
                {
                    SysRoleMenu rm = new SysRoleMenu();
                    rm.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                    rm.RoleId = roleId;
                    rm.MenuCode = m.Replace("+", "");
                    rm.UpdatedBy = this.UserCode;
                    rm.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<SysRoleMenu>(trans, rm);

                    //var ps = items.Where(c => c.StartsWith(rm.MenuCode)).ToArray();
                    //foreach (string p in ps)
                    //{
                    //    SysRolePerm rp = new SysRolePerm();
                    //    rp.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
                    //    rp.PermId = p.Split('+')[1];
                    //    rp.RoleMenuId = rm.ID;
                    //    rp.UpdatedBy = this.UserCode;
                    //    rp.UpdatedDate = DateTime.Now;

                    //    DBContext.SaveAndUpdate<SysRolePerm>(trans, rp);
                    //}
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateRolePerm");
                throw ex;
            }
        }

        /// <summary>
        /// 修改密碼
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void UpdatePassword(String oldPassword, String newPassword)
        {
            if (String.IsNullOrEmpty(this.UserCode))
            {
                throw new BusiException("会话超时");
            }
            if (!DBContext.Exist<SysUser>(SysUser.Meta.UserCode == this.UserCode & SysUser.Meta.PWD == CryptographyManager.Instance.SymmetricEncrpyt(oldPassword, CryptographyManager.DEFAULT_KEY)))
            {
                throw new BusiException("用户原密码错误.", "100001");
            }
            SysUser user = new SysUser();
            user.UserCode = this.UserCode;
            user.PWD = CryptographyManager.Instance.SymmetricEncrpyt(newPassword, CryptographyManager.DEFAULT_KEY);
            DBContext.SaveAndUpdate<SysUser>(user);

        }

        public IList<BasBase> FindBaseInfo(string Code, string Name)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(Code))
            {
                ce = (BasBase.Meta.CODE == Code);
            }

            if (!string.IsNullOrEmpty(Name))
            {
                ce = (ce & BasBase.Meta.NAME.Like(Name));
            }

            //if (!string.IsNullOrEmpty(deptCode))
            //{
            //    ce = ce & (SysUser.Meta.DeptCode.Like(deptCode));
            //}

            // ce = ce & SysUser.Meta.SITE == this.UserSite;

            if (ce == null)
            {
                return DBContext.LoadArray<BasBase>();
            }

            return DBContext.FindArray<BasBase>(ce);

        }
        public IList<BasBase> FindBaseBySubCode(string subCode)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(subCode))
            {
                ce = (BasBase.Meta.SubCode == subCode);
            }


            if (ce == null)
            {
                return DBContext.LoadArray<BasBase>();
            }

            return DBContext.FindArray<BasBase>(ce);

        }
       
        public void SaveBaseInfo(BasBase bbase)
        {
            try
            {
                if((bbase.CODE==null||bbase.CODE==string.Empty)&&bbase.ID==null)
                {
                    if(bbase.SubCode!=null)
                    {
                        bbase.CreatedDate = DateTime.Now;
                        bbase.UpdatedBy = this.UserCode;
                        bbase.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        bbase.CODE = bbase.SubCode;
                        bbase.NAME = bbase.SubName;
                        bbase.SubCode = null;
                        bbase.SubName = null;
                        DBContext.SaveAndUpdate<BasBase>(bbase);
                    }
                    return;
                }
                if (!DBContext.Exist<BasBase>(BasBase.Meta.SubCode == bbase.SubCode))
                {
                    bbase.CreatedDate = DateTime.Now;
                    bbase.UpdatedBy = this.UserCode;
                    bbase.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    DBContext.SaveAndUpdate<BasBase>(bbase);
                }
                else
                {
                    if (bbase.ID != null)
                    {
                        bbase.UpdatedDate = DateTime.Now;
                        bbase.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<BasBase>(bbase);
                    }
                    else
                    {
                        throw new Exception("子编码" + bbase.SubCode + "重复");
                    }
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveBaseInfo");
                throw ex;
            }
        }

        public void RemoveBaseInfo(string Code)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<BasBase>(trans, BasBase.Meta.ID == Code);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveBaseInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


        public void UpdateBaseInfo(string basId, BasBase bbase)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                BasBase ru = DBContext.Find<BasBase>(trans, BasBase.Meta.ID == basId);
                ru.CODE = bbase.CODE;
                ru.NAME = bbase.NAME;
                ru.SubCode = bbase.SubCode;
                ru.SubName = bbase.SubName;
                ru.MEMO = bbase.MEMO;
                ru.UpdatedBy = this.UserCode;
                ru.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<BasBase>(trans, ru);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateBaseInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<BasCode> FindBasCode(string ID, string Name)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(ID))
            {
                ce = (BasCode.Meta.ID == ID);
            }

            if (!string.IsNullOrEmpty(Name))
            {
                ce = (ce & BasCode.Meta.NAME.Like(Name));
            }

            if (ce == null)
            {
                return DBContext.LoadArray<BasCode>();
            }

            return DBContext.FindArray<BasCode>(ce);

        }
        public void SaveBasCode(BasCode bcode)
        {
            try
            {
                if (!DBContext.Exist<BasCode>(BasCode.Meta.ID == bcode.ID))
                {
                    bcode.CreatedDate = DateTime.Now;
                    bcode.UpdatedBy = this.UserCode;
                    bcode.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();                   
                }
                else
                {
                    bcode.UpdatedDate = DateTime.Now;
                    bcode.UpdatedBy = this.UserCode;
                }
                DBContext.SaveAndUpdate<BasCode>(bcode);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveBasCode");
                throw ex;
            }
        }

        public void RemoveBasCode(string ID)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<BasCode>(trans, BasCode.Meta.ID == ID);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveBasCode");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


        public void UpdateBasCode(string basId, BasCode bcode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                BasCode ru = DBContext.Find<BasCode>(trans, BasCode.Meta.ID == basId);
                ru.NAME = bcode.NAME;
                ru.TYPE = bcode.TYPE;
                ru.PREFIX = bcode.PREFIX;
                ru.DateFormat = bcode.DateFormat;
                ru.BindSequence = bcode.BindSequence;
                ru.CodeLen = bcode.CodeLen;
                ru.MEMO = bcode.MEMO;
                ru.UpdatedBy = this.UserCode;
                ru.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<BasCode>(trans, ru);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateBasCode");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<BasCustom> FindBasCustom(string code, string Name)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(code))
            {
                ce = (BasCustom.Meta.CODE == code);
            }

            if (!string.IsNullOrEmpty(Name))
            {
                ce =  (ce &BasCustom.Meta.NAME.Like(Name));
            }

            if (ce == null)
            {
                return DBContext.LoadArray<BasCustom>();
            }

            return DBContext.FindArray<BasCustom>(ce);

        }
        public void SaveBasCustom(BasCustom bcode)
        {
            try
            {
                if (!DBContext.Exist<BasCustom>(BasCustom.Meta.CODE == bcode.CODE))
                {
                    bcode.CreatedDate = DateTime.Now;
                    bcode.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<BasCustom>(bcode);
                }
                else
                {
                    throw new Exception("编码重复");
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveBasCustom");
                throw ex;
            }
        }

        public void RemoveBasCustom(string code, string name)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<BasCustom>(trans, BasCustom.Meta.CODE == code & BasCustom.Meta.NAME == name);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveBasCustom");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


        public void UpdateBasCustom(string code, BasCustom bcode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                BasCustom ru = DBContext.Find<BasCustom>(trans, BasCustom.Meta.CODE == code);
                ru.NAME = bcode.NAME;
                ru.ADDRESS = bcode.ADDRESS;
                ru.TransType = bcode.TransType;
                ru.ReceiveArea = bcode.ReceiveArea;
                ru.CONTACT = bcode.CONTACT;
                ru.MOBILE = bcode.MOBILE;
                ru.FAX = bcode.FAX;
                ru.InvoiceName = bcode.InvoiceName;
                ru.InvoiceNumber = bcode.InvoiceNumber;
                ru.MEMO = bcode.MEMO;
                ru.UpdatedBy = this.UserCode;
                ru.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<BasCustom>(trans, ru);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateBasCustom");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<BasEquipment> FindBasEquipment(string code, string Name)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(code))
            {
                ce = (BasEquipment.Meta.CODE == code);
            }

            if (!string.IsNullOrEmpty(Name))
            {
                ce = (ce & BasEquipment.Meta.MachineName.Like(Name));
            }

            if (ce == null)
            {
                return DBContext.LoadArray<BasEquipment>();
            }

            return DBContext.FindArray<BasEquipment>(ce);

        }
        public void SaveBasEquipment(BasEquipment bequip)
        {
            try
            {
                if (!DBContext.Exist<BasEquipment>(BasEquipment.Meta.CODE == bequip.CODE))
                {
                    bequip.CreatedDate = DateTime.Now;
                    bequip.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<BasEquipment>(bequip);
                }
                else
                {
                    if (bequip.CODE != null)
                    {
                        bequip.UpdatedDate = DateTime.Now;
                        bequip.UpdatedBy = this.UserCode;                        
                        DBContext.SaveAndUpdate<BasEquipment>(bequip);
                    }
                    else
                    {
                        throw new Exception("设备编码" + bequip.CODE + "重复");
                    }
                    //throw new Exception("编码重复");
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveBasEquipment");
                throw ex;
            }
        }

        public void RemoveBasEquipment(string code)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<BasEquipment>(trans, BasEquipment.Meta.CODE == code);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveBasEquipment");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


        public void UpdateBasEquipment(string code, BasEquipment bcode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                BasEquipment ru = DBContext.Find<BasEquipment>(trans, BasEquipment.Meta.CODE == code);
                ru.COMPANY = bcode.COMPANY;
                ru.MachineName = bcode.MachineName;
                ru.MachineType = bcode.MachineType;
                ru.AxisNumber = bcode.AxisNumber;
                ru.MODEL = bcode.MODEL;
                ru.POWER = bcode.POWER;
                ru.LOCATION = bcode.LOCATION;
                ru.STATUS = bcode.STATUS;
                ru.OutCode = bcode.OutCode;
                ru.UseDate = bcode.UseDate;
                ru.MEMO = bcode.MEMO;
                ru.UpdatedBy = this.UserCode;
                ru.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<BasEquipment>(trans, ru);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateBasEquipment");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<BasMateriel> FindBasMateriel(string cpartno, string qpartno, string Name)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(cpartno))
            {
                ce = (BasMateriel.Meta.CPARTNO == cpartno);
            }
            if (!string.IsNullOrEmpty(qpartno))
            {
                ce = (BasMateriel.Meta.QPARTNO == qpartno);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                ce =( ce & BasMateriel.Meta.NAME.Like(Name));
            }

            if (ce == null)
            {
                return DBContext.LoadArray<BasMateriel>();
            }

            return DBContext.FindArray<BasMateriel>(ce);

        }
        public void SaveBasMateriel(BasMateriel bcode)
        {
            try
            {
                if (!DBContext.Exist<BasMateriel>(BasMateriel.Meta.CPARTNO == bcode.CPARTNO))
                {
                    bcode.CreatedDate = DateTime.Now;
                    bcode.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<BasMateriel>(bcode);
                }
                else
                {
                    bcode.UpdatedDate = DateTime.Now;
                    bcode.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<BasMateriel>(bcode);
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveBasMateriel");
                throw ex;
            }
        }

        public void RemoveBasMateriel(string cpartno, string qpartno)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<BasMateriel>(trans, BasMateriel.Meta.CPARTNO == cpartno & BasMateriel.Meta.QPARTNO == qpartno);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveBasMateriel");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


        public void UpdateBasMateriel(string cpart, BasMateriel bcode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                BasMateriel ru = DBContext.Find<BasMateriel>(trans, BasMateriel.Meta.CPARTNO == cpart);
                ru.QPARTNO = bcode.QPARTNO;
                ru.NAME = bcode.NAME;
                ru.CUSTOMER = bcode.CUSTOMER;
                ru.MEMO = bcode.MEMO;
                ru.UpdatedBy = this.UserCode;
                ru.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<BasMateriel>(trans, ru);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateBasMateriel");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public IList<BasSequence> FindBasSequence(string Name)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(Name))
            {
                ce = (BasSequence.Meta.SeqName == Name);
            }

            if (ce == null)
            {
                return DBContext.LoadArray<BasSequence>();
            }

            return DBContext.FindArray<BasSequence>(ce);

        }
        public void SaveBasSequence(BasSequence bseq)
        {
            try
            {
                if (!DBContext.Exist<BasSequence>(BasSequence.Meta.SeqName == bseq.SeqName))
                {
                    bseq.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    bseq.CreatedDate = DateTime.Now;
                    bseq.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<BasSequence>(bseq);
                }
                else
                {
                    if(bseq.ID != null)
                    {
                        bseq.UpdatedDate = DateTime.Now;
                        bseq.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<BasSequence>(bseq);
                    }
                    else
                    {
                        throw new Exception("序列号" + bseq.SeqName + "重复");
                    }
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveBasSequence");
                throw ex;
            }
        }

        public void RemoveBasSequence(string ID)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<BasSequence>(trans, BasSequence.Meta.ID == ID);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveBasSequence");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }


        public void UpdateBasSequence(string ID, BasSequence bcode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                BasSequence ru = DBContext.Find<BasSequence>(trans, BasSequence.Meta.ID == ID);
                ru.SeqName = bcode.SeqName;
                ru.FAMILY = bcode.FAMILY;
                ru.DigitalLen = bcode.DigitalLen;
                ru.DigitalType = bcode.DigitalType;
                ru.IncreaseMode = bcode.IncreaseMode;
                ru.CurrentNo = bcode.CurrentNo;
                ru.BU = bcode.BU;
                ru.SITE = bcode.SITE;
                ru.MEMO = bcode.MEMO;
                ru.UpdatedBy = this.UserCode;
                ru.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<BasSequence>(trans, ru);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateBasSequence");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public TextValueInfo[] ListBindSeqNo()
        {
            IList<BasSequence> bseqs = DBContext.LoadArray<BasSequence>();

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].ID;
                vts[i].Text = bseqs[i].SeqName;
            }


            //vts[menus.Count] = new TextValueInfo();
            //vts[menus.Count].Value = "000000";
            //vts[menus.Count].Text = "Root Node";

            return vts;
        }

        public TextValueInfo[] ListBindCustName()
        {
            IList<BasCustom> bseqs = DBContext.LoadArray<BasCustom>();

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                //vts[i].Value = bseqs[i].CODE;
                vts[i].Value = bseqs[i].NAME;
                vts[i].Text = bseqs[i].NAME;
            }

            return vts;
        }

        public TextValueInfo[] ListBindTemplateType()
        {
            IList<BasBase> objs = DBContext.FindArray<BasBase>(BasBase.Meta.CODE=="QZB17040008");

            TextValueInfo[] vts = new TextValueInfo[objs.Count];
            for (var i = 0; i < objs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = objs[i].SubCode;
                vts[i].Text = objs[i].SubName;
            }
            return vts;
        }

        public TextValueInfo[] ListBindUnsurenessRes()
        {
            IList<BasBase> objs = DBContext.FindArray<BasBase>(BasBase.Meta.CODE == "QZB17040010");

            TextValueInfo[] vts = new TextValueInfo[objs.Count];
            for (var i = 0; i < objs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = objs[i].SubCode;
                vts[i].Text = objs[i].SubName;
            }
            return vts;
        }

        //public void SaveOrderInfo(OrderDetail obj)
        //{
        //    try
        //    {
        //        bool bsum = false;
        //        //保存订单身  
        //        if (string.IsNullOrEmpty(obj.ID))
        //        {
        //            obj.STATUS = "0";//订单状态（0：创建；1：发布；2：发货通知；3：关闭）
        //            obj.MEMO = "创建";
        //            obj.CreatedDate = DateTime.Now;
        //            obj.UpdatedBy = this.UserCode;
        //            obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
        //            DBContext.SaveAndUpdate<OrderDetail>(obj);
        //            bsum = true;
        //        }
        //        else
        //        {
        //            obj.UpdatedDate = DateTime.Now;
        //            obj.UpdatedBy = this.UserCode;
        //            DBContext.SaveAndUpdate<OrderDetail>(obj);
        //        }
        //        //保存订单头
        //        if (!DBContext.Exist<OrderHead>(OrderHead.Meta.OrderNo == obj.OrderNo))
        //        {
        //            OrderHead oh = new OrderHead();
        //            oh.OrderNo = obj.OrderNo;
        //            oh.CONTRACT = obj.CONTRACT;
        //            oh.CustName = obj.CustName;
        //            oh.OrderQuantity = obj.OrderQuantity;
        //            oh.STATUS = "0";
        //            oh.MEMO = "创建";
        //            oh.CreatedDate = DateTime.Now;
        //            oh.UpdatedBy = this.UserCode;
        //            DBContext.SaveAndUpdate<OrderHead>(oh);
        //        }
        //        else
        //        {
        //            if (bsum)
        //            {
        //                OrderHead oh = DBContext.Find<OrderHead>(OrderHead.Meta.OrderNo == obj.OrderNo);
        //                oh.OrderQuantity += obj.OrderQuantity;
        //                oh.UpdatedBy = this.UserCode;
        //                oh.UpdatedDate = DateTime.Now;
        //                DBContext.SaveAndUpdate<OrderHead>(oh);
        //            }
        //            //throw new Exception("订单号重复");
        //        }

        //        //保存历史记录
        //        OrderHistory ohistory = new OrderHistory();
        //        ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
        //        ohistory.OrderNo = obj.OrderNo;
        //        ohistory.CONTRACT = obj.CONTRACT;
        //        ohistory.CustName = obj.CustName;
        //        ohistory.BatchNumber = obj.BatchNumber;
        //        ohistory.OrderQuantity = obj.OrderQuantity;
        //        ohistory.PartsdrawingCode = obj.PartsdrawingCode;
        //        ohistory.ProductName = obj.ProductName;
        //        ohistory.STATUS = obj.STATUS;
        //        ohistory.MEMO = obj.MEMO;
        //        ohistory.OutDate = obj.OutDate;
        //        ohistory.CreatedDate = DateTime.Now;
        //        ohistory.UpdatedBy = this.UserCode;

        //        DBContext.SaveAndUpdate<OrderHistory>(ohistory);
        //    }
        //    catch (Exception ex)
        //    {
        //        PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveOrderInfo");
        //        throw ex;
        //    }
        //}
        public string SaveOrderInfo(OrderDetail obj)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                if (obj == null || string.IsNullOrEmpty(obj.OrderNo) || string.IsNullOrEmpty(obj.CustName)
                   // || string.IsNullOrEmpty(obj.CONTRACT) || string.IsNullOrEmpty(obj.ProductCode)
                    //|| string.IsNullOrEmpty(obj.ProductCode)
                    || string.IsNullOrEmpty(obj.ProductName) || string.IsNullOrEmpty(obj.PartsdrawingCode)
                    || obj.OutDate == null)
                {
                    return "请检查输入数据是否输入完全";
                }
                if(string.IsNullOrEmpty(obj.BatchNumber))
                {
                    obj.BatchNumber = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();//如果备注为空，则自动补充
                }
                PartsdrawingCode pc = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.PartsCode == obj.PartsdrawingCode);
                if (pc == null)
                {
                    return "图号不存在，请先维护图号";
                }
                try
                {
                    string sq = obj.OrderQuantity.ToString();
                    foreach (char ch in sq)
                    {
                        if (ch < '0' || ch > '9')
                            return "订单数量格式不正确,应为整数"; 
                    }
                    int quantity = Convert.ToInt32(obj.OrderQuantity);
                }
                catch (Exception ex)
                {
                    return "订单数量格式不正确";
                }
                if(DateTime.Compare(DateTime.Now,(DateTime)obj.OutDate)>0)
                {
                    return "交付时间应大于当前时间";
                }
                OrderHead oh = DBContext.Find<OrderHead>(OrderHead.Meta.OrderNo == obj.OrderNo);
                //OrderDetail od = DBContext.Find<OrderDetail>(OrderDetail.Meta.OrderNo == obj.OrderNo & OrderDetail.Meta.CONTRACT == obj.CONTRACT);
                //by tony modify 2017-6-5
                OrderDetail od = null;
                if (!string.IsNullOrEmpty(obj.ID))
                {
                    od = DBContext.Find<OrderDetail>(OrderDetail.Meta.ID == obj.ID);
                    
                    //od = DBContext.Find<OrderDetail>(OrderDetail.Meta.OrderNo == obj.OrderNo
                    //& OrderDetail.Meta.PartsdrawingCode == obj.PartsdrawingCode & OrderDetail.Meta.BatchNumber == obj.BatchNumber);
                }
                if (oh == null)
                {
                    oh = new OrderHead();
                    oh.OrderNo = obj.OrderNo;
                    oh.CONTRACT = obj.CONTRACT;
                    oh.CustName = obj.CustName;
                    oh.OrderQuantity = obj.OrderQuantity;
                    oh.STATUS = "0";
                    oh.MEMO = "创建";
                    oh.CreatedDate = DateTime.Now;
                    oh.UpdatedBy = this.UserCode;
                }
                else
                {
                    if (od == null)
                    {
                        if(DBContext.Exist<OrderDetail>(OrderDetail.Meta.PartsdrawingCode==obj.PartsdrawingCode&OrderDetail.Meta.BatchNumber==obj.BatchNumber))
                        {
                            return "已有该图号批号下的订单存在，请更改批号或图号！";
                        }
                        oh.OrderQuantity += obj.OrderQuantity;
                        oh.UpdatedBy = this.UserCode;
                        oh.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        oh.OrderQuantity = oh.OrderQuantity - od.OrderQuantity + obj.OrderQuantity;
                        oh.UpdatedBy = this.UserCode;
                        oh.UpdatedDate = DateTime.Now;
                    }
                }
                if (od == null)
                {
                    od = obj;
                    od.STATUS = "0";
                    od.MEMO = "创建";
                    od.CreatedDate = DateTime.Now;
                    od.CreatedBy = this.UserCode;
                    od.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                }
                else
                {
                    od.CONTRACT = obj.CONTRACT;
                    od.CustName = obj.CustName;
                    od.ProductCode = obj.ProductCode;
                    od.ProductName = obj.ProductName;
                    od.PartsdrawingCode = obj.PartsdrawingCode;
                    od.BatchNumber = obj.BatchNumber;
                    od.OrderQuantity = obj.OrderQuantity;
                    od.OutDate = obj.OutDate;
                    od.CheckTime = obj.CheckTime;
                    od.UpdatedDate = DateTime.Now;
                    od.UpdatedBy = this.UserCode;
                    od.MEMO = obj.MEMO;
                }
                OrderHistory ohistory = new OrderHistory();
                ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                ohistory.OrderNo = obj.OrderNo;
                ohistory.CONTRACT = obj.CONTRACT;
                ohistory.CustName = obj.CustName;
                ohistory.BatchNumber = obj.BatchNumber;
                ohistory.OrderQuantity = obj.OrderQuantity;
                ohistory.PartsdrawingCode = obj.PartsdrawingCode;
                ohistory.ProductName = obj.ProductName;
                ohistory.STATUS = obj.STATUS;
                ohistory.MEMO = obj.MEMO;
                ohistory.OutDate = obj.OutDate;
                ohistory.CreatedDate = DateTime.Now;
                ohistory.UpdatedBy = this.UserCode;

                DBContext.SaveAndUpdate<OrderHead>(trans, oh);
                DBContext.SaveAndUpdate<OrderDetail>(trans, od);
                DBContext.SaveAndUpdate<OrderHistory>(trans, ohistory);
                trans.Commit();
                return "OK";

                //bool bsum = false;
                ////保存订单身  
                //if (string.IsNullOrEmpty(obj.ID))
                //{
                //    obj.STATUS = "0";//订单状态（0：创建；1：发布；2：发货通知；3：关闭）
                //    obj.MEMO = "创建";
                //    obj.CreatedDate = DateTime.Now;
                //    obj.UpdatedBy = this.UserCode;
                //    obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                //    DBContext.SaveAndUpdate<OrderDetail>(obj);
                //    bsum = true;
                //}
                //else
                //{
                //    obj.UpdatedDate = DateTime.Now;
                //    obj.UpdatedBy = this.UserCode;
                //    DBContext.SaveAndUpdate<OrderDetail>(obj);
                //}
                ////保存订单头
                //if (!DBContext.Exist<OrderHead>(OrderHead.Meta.OrderNo == obj.OrderNo))
                //{
                //    OrderHead oh = new OrderHead();
                //    oh.OrderNo = obj.OrderNo;
                //    oh.CONTRACT = obj.CONTRACT;
                //    oh.CustName = obj.CustName;
                //    oh.OrderQuantity = obj.OrderQuantity;
                //    oh.STATUS = "0";
                //    oh.MEMO = "创建";
                //    oh.CreatedDate = DateTime.Now;
                //    oh.UpdatedBy = this.UserCode;
                //    DBContext.SaveAndUpdate<OrderHead>(oh);
                //}
                //else
                //{
                //    if (bsum)
                //    {
                //        OrderHead oh = DBContext.Find<OrderHead>(OrderHead.Meta.OrderNo == obj.OrderNo);
                //        oh.OrderQuantity += obj.OrderQuantity;
                //        oh.UpdatedBy = this.UserCode;
                //        oh.UpdatedDate = DateTime.Now;
                //        DBContext.SaveAndUpdate<OrderHead>(oh);
                //    }
                //    //throw new Exception("订单号重复");
                //}

                ////保存历史记录
                //OrderHistory ohistory = new OrderHistory();
                //ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                //ohistory.OrderNo = obj.OrderNo;
                //ohistory.CONTRACT = obj.CONTRACT;
                //ohistory.CustName = obj.CustName;
                //ohistory.BatchNumber = obj.BatchNumber;
                //ohistory.OrderQuantity = obj.OrderQuantity;
                //ohistory.PartsdrawingCode = obj.PartsdrawingCode;
                //ohistory.ProductName = obj.ProductName;
                //ohistory.STATUS = obj.STATUS;
                //ohistory.MEMO = obj.MEMO;
                //ohistory.OutDate = obj.OutDate;
                //ohistory.CreatedDate = DateTime.Now;
                //ohistory.UpdatedBy = this.UserCode;

                //DBContext.SaveAndUpdate<OrderHistory>(ohistory);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveOrderInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }

        }
        public void RemoveOrderInfo(string ID)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                OrderDetail od = DBContext.Find<OrderDetail>(OrderDetail.Meta.ID == ID);
                if (od != null)
                {
                    IList<OrderDetail> lod = DBContext.FindArray<OrderDetail>(OrderDetail.Meta.OrderNo == od.OrderNo);
                    if (lod.Count == 1)
                    {
                        DBContext.Remove<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    }
                }
                DBContext.Remove<OrderDetail>(trans, OrderDetail.Meta.ID == ID);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveOrderInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<OrderDetail> FindOrderInfo(string orderno, string parsdrawingno,string status="",string starttime="",string endtime="",string id="")
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(orderno))
            {
                ce = (OrderDetail.Meta.OrderNo.Like(orderno));
            }
            if (!string.IsNullOrEmpty(parsdrawingno))
            {
                ce = (ce&OrderDetail.Meta.PartsdrawingCode.Like(parsdrawingno));
            }
            if (!string.IsNullOrEmpty(status))
            {
                ce = (ce & OrderDetail.Meta.STATUS == status);
            }
            if (!string.IsNullOrEmpty(id))
            {
                ce = (ce & OrderDetail.Meta.ID == id);
            }
            if (!string.IsNullOrEmpty(starttime)&&!string.IsNullOrEmpty(endtime))
            {
                if(DateTime.Compare(Convert.ToDateTime(starttime), Convert.ToDateTime(endtime))>0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & OrderDetail.Meta.CreatedDate >= Convert.ToDateTime(starttime));
                ce = (ce & OrderDetail.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }

            if (ce == null)
            {
                return DBContext.LoadArray<OrderDetail>();
            }

            return DBContext.FindArray<OrderDetail>(ce);

        }
        
        public string SaveInWareHouseInfo(MaterialStock obj)
        {
            try
            {
                if (!DBContext.Exist<BasMateriel>(BasMateriel.Meta.NAME == obj.MaterialName & BasMateriel.Meta.QPARTNO == obj.MaterialCode))
                {
                    return "ERR";

                }
                if (!DBContext.Exist<MaterialStock>(MaterialStock.Meta.MSN == obj.MSN))
                {
                    obj.STATUS = "0";//物料状态（0：入库；1：正常出库；2：退料；3：补料出库；4：报废）
                    obj.MEMO = "入库";
                    obj.CreatedDate = DateTime.Now;
                    obj.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<MaterialStock>(obj);
                }
                else
                {
                    obj.STATUS = "0";//物料状态（0：入库；1：正常出库；2：退料；3：补料出库；4：报废）
                    obj.MEMO = "入库";
                    obj.UpdatedBy = this.UserCode;
                    obj.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<MaterialStock>(obj);

                    //throw new Exception("订单号重复");
                }

                //保存历史记录
                MaterialStockHistory ohistory = new MaterialStockHistory();
                ohistory.MSN = obj.MSN;
                ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                ohistory.MaterialCode = obj.MaterialCode;
                ohistory.MaterialName = obj.MaterialName;
                ohistory.MEMO = obj.MEMO;
                ohistory.UNIT = obj.UNIT;
                ohistory.UpdatedBy = obj.UpdatedBy;
                ohistory.UpdatedDate = obj.UpdatedDate == null ? obj.CreatedDate : obj.UpdatedDate;
                ohistory.CreatedDate = DateTime.Now;
                ohistory.CustName = obj.CustName;
                ohistory.BatchNumber = obj.BatchNumber;
                ohistory.DOCUMENTID = obj.DOCUMENTID;
                ohistory.STATUS = obj.STATUS;
                ohistory.QUANTITY = obj.BasQty;
                ohistory.StockHouse = obj.StockHouse;
                ohistory.Description = obj.Description;
                ohistory.StatusMemo = "入库";

                DBContext.SaveAndUpdate<MaterialStockHistory>(ohistory);
                return "OK";
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveInWareHouseInfo");
                throw ex;
            }
            //try
            //{
            //    if (!DBContext.Exist<MaterialStock>(MaterialStock.Meta.MSN == obj.MSN))
            //    {
            //        obj.STATUS = "0";//物料状态（0：入库；1：正常出库；2：退料；3：补料出库；4：报废）
            //        obj.MEMO = "入库";
            //        obj.CreatedDate = DateTime.Now;
            //        obj.UpdatedBy = this.UserCode;
            //        DBContext.SaveAndUpdate<MaterialStock>(obj);
            //    }
            //    else
            //    {
            //        obj.STATUS = "0";//物料状态（0：入库；1：正常出库；2：退料；3：补料出库；4：报废）
            //        obj.MEMO = "入库";
            //        obj.UpdatedBy = this.UserCode;
            //        obj.UpdatedDate = DateTime.Now;
            //        DBContext.SaveAndUpdate<MaterialStock>(obj);

            //        //throw new Exception("订单号重复");
            //    }

            //    //保存历史记录
            //    MaterialStockHistory ohistory = new MaterialStockHistory();
            //    ohistory.MSN = obj.MSN;
            //    ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
            //    ohistory.MaterialCode = obj.MaterialCode;
            //    ohistory.MaterialName = obj.MaterialName;
            //    ohistory.MEMO = obj.MEMO;
            //    ohistory.UNIT = obj.UNIT;
            //    ohistory.UpdatedBy = obj.UpdatedBy;
            //    ohistory.UpdatedDate = obj.UpdatedDate == null ? obj.CreatedDate : obj.UpdatedDate;
            //    ohistory.CreatedDate = DateTime.Now;
            //    ohistory.CustName = obj.CustName;
            //    ohistory.BatchNumber = obj.BatchNumber;
            //    ohistory.DOCUMENTID = obj.DOCUMENTID;
            //    ohistory.STATUS = obj.STATUS;
            //    ohistory.QUANTITY = obj.BasQty;
            //    ohistory.StockHouse = obj.StockHouse;
            //    ohistory.StatusMemo = "入库";

            //    DBContext.SaveAndUpdate<MaterialStockHistory>(ohistory);
            //}
            //catch (Exception ex)
            //{
            //    PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveInWareHouseInfo");
            //    throw ex;
            //}
        }

        public void RemoveInWareHouseInfo(string msn)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                MaterialStock ms = DBContext.Find<MaterialStock>(MaterialStock.Meta.MSN == msn);
                if(ms!=null)
                {
                    if(ms.STATUS.Trim()=="0")
                    {
                        DBContext.Remove<MaterialStock>(trans, MaterialStock.Meta.MSN == msn);
                        trans.Commit();
                    }
                    else
                    {
                        throw new Exception("该料已发出，禁止删除");
                    }
                }
                else
                {
                    throw new Exception("该料不存在，禁止失败");
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveInWareHouseInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<MaterialStock> FindInWareHouseInfo(string msn, string materialno)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(msn))
            {
                ce = (MaterialStock.Meta.MSN == msn);
            }
            if (!string.IsNullOrEmpty(materialno))
            {
                ce = (MaterialStock.Meta.MaterialCode == materialno);
            }
            if (ce == null)
            {
                return DBContext.LoadArray<MaterialStock>();
            }
            return DBContext.FindArray<MaterialStock>(ce);
        }
        public TextValueInfo[] ListBindReceiveHouse()
        {
            IList<BasBase> bseqs = DBContext.FindArray<BasBase>(BasBase.Meta.CODE == "QZB17040007");

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].SubCode;
                vts[i].Text = bseqs[i].SubName;
            }

            return vts;
        }
        public TextValueInfo[] ListBindUnit()
        {
            IList<BasBase> bseqs = DBContext.FindArray<BasBase>(BasBase.Meta.CODE == "QZB17040006");

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].SubCode;
                vts[i].Text = bseqs[i].SubName;
            }

            return vts;
        }

        /// <summary>
        /// 獲取該工站的服務地址列表
        /// </summary>
        /// <param name="macOrIP">客戶端工站IP</param>
        /// <returns></returns>
        public IList<SysMidsvcConfig> GetMidsvcList()
        {
            return DBContext.FindArray<SysMidsvcConfig>(SysMidsvcConfig.Meta.SITE == this.UserSite
                & SysMidsvcConfig.Meta.BU == this.UserBU, SysMidsvcConfig.Meta.ConnCount.ASC);
        }
        /// <summary>
        /// 從數據庫中下載模版文件到服務器目錄
        /// </summary>
        /// <param name="tplId">模版ID</param>
        /// <returns>模版在服務器路徑</returns>
        public string DownloadLabelTpl(string tplId)
        {
            BasLabelTemplate tpl = DBContext.Find<BasLabelTemplate>(BasLabelTemplate.Meta.ID == tplId);
            if (tpl != null)
            {
                string timeStamp = ((DateTime)tpl.UpdatedDate).ToString("yyyyMMddHHmmSS");
                string filePath = tplPath + "\\" + tpl.FAMILY;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //判定是否已經下載了文件，如果已下載就直接返回路徑
                string fileName = filePath + "\\" + tpl.TplName + "_" + timeStamp + ".lab";
                if (File.Exists(fileName))
                {
                    return fileName;
                }
                //如果文件已經更新，先刪除舊文件
                DirectoryInfo di = new DirectoryInfo(filePath);
                foreach (FileInfo fi in di.GetFiles(tpl.TplName + "*.lab"))
                {
                    fi.Delete();
                }
                //從數據庫中下載到服務器
                //System.Data.IDataReader rd = DBContext.ExcuteSql(ConstantsHelper.GetHelper(this.UserSite, this.UserBU).GetSqlText(BLLConstants.Route_BO_NAME, "DownloadLabelTpl", "SEL_LABEL_TPL_DATA"))
                System.Data.IDataReader rd = DBContext.ExcuteSql("SELECT * FROM FMGR.BAS_LABEL_TEMPLATE_FILE WHERE TPL_ID = :tplId")
                    .AddInParameter("tplId", System.Data.DbType.String, tplId)
                    .ToDataRead();
                // Writes the BLOB to a file (*.bmp).
                FileStream stream;
                // Streams the BLOB to the FileStream object.
                BinaryWriter writer;
                // Size of the BLOB buffer.
                int bufferSize = 512;
                // The BLOB byte[] buffer to be filled by GetBytes.
                byte[] outByte = new byte[bufferSize];
                // The bytes returned from GetBytes.
                long retval;
                // The starting position in the BLOB output.
                long startIndex = 0;
                if (rd.Read())
                {
                    startIndex = 0;
                    // Create a file to hold the output.
                    stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new BinaryWriter(stream);

                    // Read bytes into outByte[] and retain the number of bytes returned.
                    retval = rd.GetBytes(3, startIndex, outByte, 0, bufferSize);

                    // Continue while there are bytes beyond the size of the buffer.
                    while (retval == bufferSize)
                    {
                        writer.Write(outByte);
                        writer.Flush();

                        // Reposition start index to end of last buffer and fill buffer.
                        startIndex += bufferSize;
                        retval = rd.GetBytes(3, startIndex, outByte, 0, bufferSize);
                    }
                    if (retval > 0)
                    {
                        // Write the remaining buffer.
                        writer.Write(outByte, 0, (int)retval - 1);
                    }
                    writer.Flush();

                    // Close the output file.
                    writer.Close();
                    stream.Close();
                    //byte[] blob = new Byte[(rd.GetBytes(0, 0, null, 0, int.MaxValue))];
                    //rd.GetBytes(0, 0, blob, 0, blob.Length);                    
                    //rd.Close();
                    //File.WriteAllBytes(fileName, blob);
                    return fileName;
                }
            }

            return "";
        }
        /// <summary>
        /// 取得打印模版參數值
        /// </summary>
        /// <param name="labelType"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IList<TextValueInfo> ListLabelParams(string labelType, string pid)
        {
            BasLabelTypeConfig ltf = DBContext.Find<BasLabelTypeConfig>(BasLabelTypeConfig.Meta.TplType == labelType);
            if (ltf == null)
            {
                throw new BusiException("模版類型沒有綁定參數設置。");
            }

            IDictionary rets = SpCaller.GetCaller(DBContext).CallGetLabelParams(ltf.BindFunc, pid).ExecuteOutParameters();
            SPMessage ret = new SPMessage();
            ret.Result = (string)rets["P_RET"];
            ret.Message = (string)rets["P_MSG"];
            IList<TextValueInfo> ls = new List<TextValueInfo>();
            if (ret.Result == "OK")
            {
                string[] kvs = ret.Message.Split('^');

                foreach (string kv in kvs)
                {
                    string[] tvis = kv.Split('=');
                    ls.Add(new TextValueInfo() { Text = tvis[0], Value = tvis[1] });
                }

                return ls;
            }
            else
            {
                throw new BusiException(ret.Message);
            }
        }

        /// <summary>
        /// 得到指定模版類型變量參數
        /// </summary>
        /// <param name="data"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public SPMessage GetLabelParameters(string data, string labelId)
        {
            IDictionary rets = SpCaller.GetCaller(DBContext).CallGetLabelParameters(data, labelId).ExecuteOutParameters();
            SPMessage ret = new SPMessage();
            ret.Result = (string)rets["P_RET"];
            ret.Message = (string)rets["P_MSG"];
            return ret;
        }

        public TextValueInfo[] ListBindWorkOrder(string status)
        {
            IList<WorkOrder> bseqs = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == status,WorkOrder.Meta.UpdatedDate.DESC);

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].WO;
                vts[i].Text = bseqs[i].WO;
            }

            return vts;
        }

        /// <summary>
        /// 獲取备料信息
        /// </summary>
        /// <param name="macOrIP">客戶端工站IP</param>
        /// <returns></returns>
        public IList<MaterialStock> QueryBackupInfo(string workorder)
        {
            WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == workorder);
            if (wo != null)
            {
                return DBContext.FindArray<MaterialStock>( MaterialStock.Meta.MaterialName == wo.PartsdrawingCode&(MaterialStock.Meta.STATUS=="0"|MaterialStock.Meta.STATUS=="2"),MaterialStock.Meta.MSN.ASC);
            }
            else
            {
                if(string.IsNullOrEmpty(workorder))
                {
                    //查询工单运行中，且发料数量为0的工单。
                    IList<WorkOrder> wos = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == "1" & WorkOrder.Meta.MaterialQty < 1, WorkOrder.Meta.UpdatedDate.DESC);
                    if(wos.Count>0)
                    {
                        return DBContext.FindArray<MaterialStock>(MaterialStock.Meta.MaterialName == wos[0].PartsdrawingCode & (MaterialStock.Meta.STATUS == "0" | MaterialStock.Meta.STATUS == "2"), MaterialStock.Meta.MSN.ASC);
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 保存发料信息
        /// </summary>
        /// <param name="obj"></param>
        public string SaveOutWareHouseInfo(MaterialStockHistory obj)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {

                //判断是否超发
                IList<WorkOrder> wo = FindWorkOrderInfo(obj.WorkOrder);
                if (wo != null)
                {
                    if (wo[0].MaterialQty > wo[0].PlanQuantity || wo[0].MaterialQty == wo[0].PlanQuantity)
                    {
                        //临时添加
                        //return "OK";
                        throw new Exception("工单发料已满，禁止超发！");
                    }
                }
                else
                {
                    throw new Exception("无此工单!");
                }
                //IList<MaterialStockHistory> mhs = QueryOutHistoryInfo(obj.WorkOrder);
                //更新库存状态
                MaterialStock ms = DBContext.Find<MaterialStock>(MaterialStock.Meta.MSN == obj.MSN);
                //临时添加
                //if (!string.IsNullOrEmpty(ms.STATUS) && ms.STATUS == "1")
                //{
                //    return "OK";
                //}
                if (!string.IsNullOrEmpty(ms.STATUS) && ms.STATUS != "0" && ms.STATUS != "2")
                {
                    throw new Exception("条码有误!");
                }
                ms.STATUS = "1";//物料状态（0：入库；1：正常出库；2：退料；3：补料出库；4：报废）
                ms.MEMO = "正常出库";
                ms.UpdatedBy = this.UserCode;
                ms.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<MaterialStock>(trans, ms);


                //保存历史记录
                MaterialStockHistory ohistory = new MaterialStockHistory();
                ohistory.MSN = ms.MSN;
                ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                ohistory.MaterialCode = ms.MaterialCode;
                ohistory.MaterialName = ms.MaterialName;
                ohistory.MEMO = ms.MEMO;
                ohistory.UNIT = ms.UNIT;
                ohistory.STATUS = "1";
                ohistory.UpdatedBy = this.UserCode;
                ohistory.CreatedDate = DateTime.Now;
                ohistory.CustName = ms.CustName;
                ohistory.BatchNumber = ms.BatchNumber;
                ohistory.DOCUMENTID = ms.DOCUMENTID;
                ohistory.WorkOrder = obj.WorkOrder;
                ohistory.MaterialHandler = obj.MaterialHandler;
                ohistory.StatusMemo = "正常出库";
                ohistory.QUANTITY = ms.BasQty;
                ohistory.StockHouse = obj.StockHouse;
                DBContext.SaveAndUpdate<MaterialStockHistory>(trans, ohistory);

                //更新工单发料数量
                if (wo[0].MaterialQty == null)
                {
                    wo[0].MaterialQty = 1;
                }
                else
                {
                    wo[0].MaterialQty = wo[0].MaterialQty + 1;
                }
                DBContext.SaveAndUpdate<WorkOrder>(trans, wo[0]);
                //根据工单查找BASE_QTY
                WorkOrder workOrder = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == obj.WorkOrder);
                int qty = 1;
                if (workOrder != null & workOrder.BaseQty > 0)
                {
                    qty = Convert.ToInt32(workOrder.BaseQty);
                }
                //循环生成PSN并保存WIP及History
                string[] psns = new string[qty];
                string sql = "select count(0) from MES_MASTER.TRACKING_WIP where MSN='" + obj.MSN + "'";
                int num = Convert.ToInt32(DBContext.ExcuteSql(sql).ToDataSet().Tables[0].Rows[0][0].ToString());
                for (int i = 0; i < qty; i++)
                {

                    num = num + 1;
                    psns[i] = obj.MSN + num.ToString().PadLeft(2, '0');


                    if (workOrder != null)
                    {
                        TrackingWip tw = new TrackingWip();
                        tw.PSN = psns[i];
                        tw.MSN = obj.MSN;
                        tw.WorkOrder = workOrder.WO;
                        tw.PartsdrawingCode = workOrder.PartsdrawingCode;
                        tw.PartsName = workOrder.ProductName;
                        tw.PartsCode = workOrder.ProductCode;
                        tw.BatchNumber = workOrder.BatchNumber;
                        tw.StationName = "PRINT";
                        tw.StationId = "PRINT";//by tony 2018-7-25
                        string[] nextstations = GetNextStation(tw.WorkOrder, tw.StationId);//by tony 2018-7-25
                        tw.NextStation = nextstations[0];//by tony 2018-7-25
                        tw.NextStationId = nextstations[1];//by tony 2018-7-25
                        tw.QUANTITY = 1;
                        tw.STATUS = "P";
                        tw.InStatioonTime = DateTime.Now;
                        tw.OutStationTime = DateTime.Now;
                        tw.CreatedDate = DateTime.Now;
                        tw.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<TrackingWip>(trans, tw);

                        TrackingHistory th = new TrackingHistory();
                        th.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        th.PSN = psns[i];
                        th.MSN = obj.MSN;
                        th.WorkOrder = workOrder.WO;
                        th.PartsdrawingCode = workOrder.PartsdrawingCode;
                        th.PartsName = workOrder.ProductName;
                        th.PartsCode = workOrder.ProductCode;
                        th.BatchNumber = workOrder.BatchNumber;
                        th.StationName = "PRINT";
                        th.StationId = "PRINT";//by tony 2018-7-25
                        th.NextStation = nextstations[0];//by tony 2018-7-25
                        th.NextStationId = nextstations[1];//by tony 2018-7-25
                        th.QUANTITY = 1;
                        th.STATUS = "P";
                        th.InStationTime = DateTime.Now;
                        th.OutStationTime = DateTime.Now;
                        th.CreatedDate = DateTime.Now;
                        th.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<TrackingHistory>(trans, th);

                    }
                    else
                    {
                        throw new Exception("工单号码不存在");
                    }

                }
                //返回PSN
                trans.Commit();
                return string.Join(",", psns);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveOutWareHouseInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
            

        }
        /// <summary>
        /// 保存补料信息
        /// </summary>
        /// <param name="obj"></param>
        public string SaveReSendMaterialInfo(MaterialStockHistory obj)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {                
                IList<WorkOrder> wo = FindWorkOrderInfo(obj.WorkOrder);
                if (wo != null)
                {
                    //IList<MaterialStockHistory> mhs = QueryOutHistoryInfo(obj.WorkOrder);
                    //更新库存状态
                    MaterialStock ms = DBContext.Find<MaterialStock>(MaterialStock.Meta.MSN == obj.MSN);
                    ms.STATUS = "3";//物料状态（0：入库；1：正常出库；2：退料；3：补料出库；4：报废）
                    ms.MEMO = "补料出库";
                    ms.UpdatedBy = this.UserCode;
                    ms.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<MaterialStock>(ms);


                    //保存历史记录
                    MaterialStockHistory ohistory = new MaterialStockHistory();
                    ohistory.MSN = ms.MSN;
                    ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    ohistory.MaterialCode = ms.MaterialCode;
                    ohistory.MaterialName = ms.MaterialName;
                    ohistory.MEMO = ms.MEMO;
                    ohistory.UNIT = ms.UNIT;
                    ohistory.STATUS = "3";
                    ohistory.UpdatedBy = this.UserCode;
                    ohistory.CreatedDate = DateTime.Now;
                    ohistory.CustName = ms.CustName;
                    ohistory.BatchNumber = ms.BatchNumber;
                    ohistory.DOCUMENTID = ms.DOCUMENTID;
                    ohistory.WorkOrder = obj.WorkOrder;
                    ohistory.MaterialHandler = obj.MaterialHandler;
                    ohistory.StatusMemo = "补料出库";
                    ohistory.QUANTITY = ms.BasQty;
                    DBContext.SaveAndUpdate<MaterialStockHistory>(ohistory);

                    //更新工单发料数量
                    wo[0].MaterialQty = wo[0].MaterialQty + 1;
                    DBContext.SaveAndUpdate<WorkOrder>(wo[0]);

                    //根据工单查找BASE_QTY
                    WorkOrder workOrder = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == obj.WorkOrder);
                    int qty = 1;
                    if (workOrder != null & workOrder.BaseQty > 0)
                    {
                        qty = Convert.ToInt32(workOrder.BaseQty);
                    }
                    //循环生成PSN并保存WIP及History
                    string[] psns = new string[qty];
                    string sql = "select count(0) from MES_MASTER.TRACKING_WIP where MSN='" + obj.MSN + "'";
                    int num = Convert.ToInt32(DBContext.ExcuteSql(sql).ToDataSet().Tables[0].Rows[0][0].ToString());
                    for (int i = 0; i < qty; i++)
                    {

                        num = num + 1;
                        psns[i] = obj.MSN + num.ToString().PadLeft(2, '0');


                        if (workOrder != null)
                        {
                            TrackingWip tw = new TrackingWip();
                            tw.PSN = psns[i];
                            tw.MSN = obj.MSN;
                            tw.WorkOrder = workOrder.WO;
                            tw.PartsdrawingCode = workOrder.PartsdrawingCode;
                            tw.PartsName = workOrder.ProductName;
                            tw.PartsCode = workOrder.ProductCode;
                            tw.BatchNumber = workOrder.BatchNumber;
                            tw.StationName = "PRINT";
                            tw.QUANTITY = 1;
                            tw.STATUS = "P";
                            tw.InStatioonTime = DateTime.Now;
                            tw.OutStationTime = DateTime.Now;
                            tw.CreatedDate = DateTime.Now;
                            tw.UpdatedBy = this.UserCode;
                            DBContext.SaveAndUpdate<TrackingWip>(trans, tw);

                            TrackingHistory th = new TrackingHistory();
                            th.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                            th.PSN = psns[i];
                            th.MSN = obj.MSN;
                            th.WorkOrder = workOrder.WO;
                            th.PartsdrawingCode = workOrder.PartsdrawingCode;
                            th.PartsName = workOrder.ProductName;
                            th.PartsCode = workOrder.ProductCode;
                            th.BatchNumber = workOrder.BatchNumber;
                            th.StationName = "PRINT";
                            th.QUANTITY = 1;
                            th.STATUS = "P";
                            th.InStationTime = DateTime.Now;
                            th.OutStationTime = DateTime.Now;
                            th.CreatedDate = DateTime.Now;
                            th.UpdatedBy = this.UserCode;
                            DBContext.SaveAndUpdate<TrackingHistory>(trans, th);

                        }
                        else
                        {
                            throw new Exception("工单号码不存在");
                        }

                    }
                    //返回PSN
                    trans.Commit();
                    return string.Join(",", psns);
                }
                else
                {
                    throw new Exception("无此工单!");
                }                
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveReSendMaterialInfo");
                throw ex;
            }
        }
        /// <summary>
        /// 保存退料信息
        /// </summary>
        /// <param name="obj"></param>
        public void SaveReturnMaterialInfo(string MSN)
        {
            try
            {
                MaterialStockHistory msh = DBContext.Find<MaterialStockHistory>(MaterialStockHistory.Meta.MSN == MSN & (MaterialStockHistory.Meta.STATUS == "1" | MaterialStockHistory.Meta.STATUS == "3"));
                
                if (msh != null)
                {
                    //更新库存状态
                    MaterialStock ms = DBContext.Find<MaterialStock>(MaterialStock.Meta.MSN == MSN);
                    ms.STATUS = "2";//物料状态（0：入库；1：正常出库；2：退料；3：补料出库；4：报废）
                    ms.MEMO = "退料";
                    ms.UpdatedBy = this.UserCode;
                    ms.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<MaterialStock>(ms);


                    //保存历史记录
                    MaterialStockHistory ohistory = new MaterialStockHistory();
                    ohistory.MSN = ms.MSN;
                    ohistory.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    ohistory.MaterialCode = ms.MaterialCode;
                    ohistory.MaterialName = ms.MaterialName;
                    ohistory.MEMO = ms.MEMO;
                    ohistory.UNIT = ms.UNIT;
                    ohistory.STATUS = "2";
                    ohistory.UpdatedBy = this.UserCode;
                    ohistory.CreatedDate = DateTime.Now;
                    ohistory.CustName = ms.CustName;
                    ohistory.BatchNumber = ms.BatchNumber;
                    ohistory.DOCUMENTID = ms.DOCUMENTID;
                    ohistory.WorkOrder = msh.WorkOrder;
                    ohistory.StatusMemo = "退料";
                    ohistory.QUANTITY = ms.BasQty;
                    ohistory.StockHouse = ms.StockHouse;
                    DBContext.SaveAndUpdate<MaterialStockHistory>(ohistory);

                    //更新工单发料数量
                    IList<WorkOrder> wo = FindWorkOrderInfo(msh.WorkOrder);
                    if (wo[0].MaterialQty > 0)
                    {
                        wo[0].MaterialQty = wo[0].MaterialQty - 1;
                    }
                    DBContext.SaveAndUpdate<WorkOrder>(wo[0]);

                   
                }
                else
                {
                    throw new Exception("无此工单!");
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveReSendMaterialInfo");
                throw ex;
            }
        }
        /// <summary>
        /// 獲取领料记录信息
        /// </summary>
        /// <param name="workorder">领料工单</param>
        /// <returns></returns>
        public IList<MaterialStockHistory> QueryOutHistoryInfo(string workorder)
        {           
           return DBContext.FindArray<MaterialStockHistory>(MaterialStockHistory.Meta.WorkOrder == workorder, MaterialStockHistory.Meta.UpdatedDate.DESC);
            
        }
        /// <summary>
        /// 獲取领料记录信息
        /// </summary>
        /// <param name="workorder">领料工单</param>
        /// <returns></returns>
        public IList<MaterialStockHistory> QueryOutHistoryInfo(string workorder,string status)
        {
            return DBContext.FindArray<MaterialStockHistory>(MaterialStockHistory.Meta.WorkOrder == workorder&MaterialStockHistory.Meta.STATUS==status, MaterialStockHistory.Meta.UpdatedDate.DESC);

        }

        /// <summary>
        /// 查询领料记录信息
        /// </summary>
        /// <param name="workorder">领料工单</param>
        /// <returns></returns>
        public IList<MaterialStockHistory> FindMaterialHistory(string workorder,string status,string MSN,string materialname,string custname,string starttime,string endtime,string batchnumber="")
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
                ce = (MaterialStockHistory.Meta.WorkOrder == workorder);
            }

            if (!string.IsNullOrEmpty(status))
            {
                if(status.IndexOf('^')!=-1)
                {
                    string[] strtemp = status.Split('^');
                    ce = (ce & (MaterialStockHistory.Meta.STATUS == strtemp [0]| MaterialStockHistory.Meta.STATUS == strtemp[1]));
                }
                ce =  (ce &MaterialStockHistory.Meta.STATUS==status);
            }

            if (!string.IsNullOrEmpty(MSN))
            {
                ce = (ce & MaterialStockHistory.Meta.MSN == MSN);
            }

            if (!string.IsNullOrEmpty(materialname))
            {
                ce = (ce & MaterialStockHistory.Meta.MaterialName == materialname);
            }
            if (!string.IsNullOrEmpty(custname))
            {
                ce = (ce & MaterialStockHistory.Meta.CustName == custname);
            }
            if (!string.IsNullOrEmpty(batchnumber))
            {
                ce = (ce & MaterialStockHistory.Meta.BatchNumber == batchnumber);
            }
            if (!string.IsNullOrEmpty(starttime)&& !string.IsNullOrEmpty(endtime))
            {
                if (DateTime.Compare(Convert.ToDateTime(starttime), Convert.ToDateTime(endtime)) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & MaterialStockHistory.Meta.CreatedDate >= Convert.ToDateTime(starttime));
                ce =( ce & MaterialStockHistory.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }
            else
            {
                ce = (ce & MaterialStockHistory.Meta.CreatedDate >= DateTime.Now.AddDays(-7));
                ce = (ce & MaterialStockHistory.Meta.CreatedDate <= DateTime.Now);
            }          

            return DBContext.FindArray<MaterialStockHistory>(ce);

        }

        public void SaveLabelReprint(ReprintLog obj)
        {
            try
            {
                obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                obj.CreatedDate = DateTime.Now;
                obj.UpdatedBy = this.UserCode;
                obj.ReprintBy = this.UserCode;
                DBContext.SaveAndUpdate<ReprintLog>(obj); 
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveLabelReprint");
                throw ex;
            }
        }

        public IList<ReprintLog> FindReprintLogInfo(string sn)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(sn))
            {
                ce = (ReprintLog.Meta.SN == sn);
            }
             
            if (ce == null)
            {
                return DBContext.LoadArray<ReprintLog>();
            }
            return DBContext.FindArray<ReprintLog>(ce);
        }

        public string SavePartsDrawing(PartsdrawingCode obj)
        {
            try
            {
                //string sq = obj.PlanQuantity.ToString();
                //foreach (char ch in sq)
                //{
                //    if (ch < '0' || ch > '9')
                //        return "投产数量格式不正确,应为整数";
                //}
                //  sq = obj.AskQuantity.ToString();
                //foreach (char ch in sq)
                //{
                //    if (ch < '0' || ch > '9')
                //        return "交付数量格式不正确,应为整数";
                //}
                //if (DateTime.Compare(DateTime.Now, (DateTime)obj.AskDate) > 0)
                //{
                //    return "交付时间应大于当前时间";
                //}
                //by tony modify 20170731
                if (string.IsNullOrEmpty(obj.ID))
                {
                    PartsdrawingCode pc = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.PartsCode == obj.PartsCode&PartsdrawingCode.Meta.ACTIVE==1);
                    if (pc == null)
                    {
                        obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        obj.CreatedDate = DateTime.Now;
                        //给技术部保存一份图号信息
                        TechnologyWip techinfo = new TechnologyWip();
                        //techinfo.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        techinfo.PARTSDRAWINGNO = obj.PartsCode;
                        techinfo.CustCode = obj.CustCode;
                        techinfo.CustName = obj.CustName;
                        techinfo.ProductCode = obj.ProductCode;
                        techinfo.ProductName = obj.ProductName;
                        techinfo.STATUS = 0;
                        techinfo.StatusMemo = "新建任务";
                        techinfo.UpdatedBy = this.UserCode;
                        techinfo.CreatedDate = System.DateTime.Now;
                        SaveTechnologyInfo(techinfo);
                       // DBContext.SaveAndUpdate<TechnologyInfo>(techinfo);
                    }
                    else
                    {
                        return "图号已经存在";
                        //obj.ID = pc.ID;
                        //if(( obj.UnitTime<0||obj.UnitTime==0)&&pc.UnitTime>0)
                        //{
                        //    obj.UnitTime = pc.UnitTime;
                        //}
                        //obj.UpdatedDate = DateTime.Now;
                    }
                }
                else
                {
                    if(string.IsNullOrEmpty(obj.ProductCode))
                    {
                        PartsdrawingCode pdc = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.ID == obj.ID);
                        if(pdc!=null)
                        {
                            obj.ProductCode = pdc.ProductCode;
                            obj.ProductName = pdc.ProductName;
                        }
                    }
                    obj.UpdatedDate = DateTime.Now;
                }
                //if(string.IsNullOrEmpty(obj.BatchNumber))
                //{
                //    obj.BatchNumber = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                //}               
                
                obj.UpdatedBy = this.UserCode;
                obj.ACTIVE = "1";
                DBContext.SaveAndUpdate<PartsdrawingCode>(obj);
                return "OK";
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SavePartsDrawing");
                throw ex;
            }
            //try
            //{
            //    string sq = obj.PlanQuantity.ToString();
            //    foreach (char ch in sq)
            //    {
            //        if (ch < '0' || ch > '9')
            //            return "投产数量格式不正确,应为整数";
            //    }
            //      sq = obj.AskQuantity.ToString();
            //    foreach (char ch in sq)
            //    {
            //        if (ch < '0' || ch > '9')
            //            return "交付数量格式不正确,应为整数";
            //    }
            //    if (DateTime.Compare(DateTime.Now, (DateTime)obj.AskDate) > 0)
            //    {
            //        return "交付时间应大于当前时间";
            //    }
            //    obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString(); 
            //    if(string.IsNullOrEmpty(obj.BatchNumber))
            //    {
            //        obj.BatchNumber = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
            //    }               
            //    obj.CreatedDate = DateTime.Now;
            //    obj.UpdatedBy = this.UserCode;
            //    obj.ACTIVE = "1";                    
            //    DBContext.SaveAndUpdate<PartsdrawingCode>(obj);
            //    return "OK";
            //}
            //catch (Exception ex)
            //{
            //    PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SavePartsDrawing");
            //    throw ex;
            //}

        }

        public IList<PartsdrawingCode> FindPartsdrawingInfo(string pdcode,string custcode="",string starttime="",string endtime="")
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(pdcode))
            {
                ce = (PartsdrawingCode.Meta.PartsCode == pdcode);
            }
            if (!string.IsNullOrEmpty(custcode))
            {
                ce = (ce&PartsdrawingCode.Meta.CustCode == custcode);
            }
            if (!string.IsNullOrEmpty(starttime)&&!string.IsNullOrEmpty(endtime))
            {
                if (DateTime.Compare(Convert.ToDateTime(starttime), Convert.ToDateTime(endtime)) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce&PartsdrawingCode.Meta.CreatedDate>= Convert.ToDateTime(starttime));
                ce = (ce & PartsdrawingCode.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }

            if (ce == null)
            {
                return DBContext.LoadArray<PartsdrawingCode>();
            }
            return DBContext.FindArray<PartsdrawingCode>(ce);
        }

        public void RemovePartsdrawingNo(string ID)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                PartsdrawingCode od = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.ID == ID);
                if (od != null)
                {                    
                    DBContext.Remove<PartsdrawingCode>(trans, PartsdrawingCode.Meta.ID == ID);                    
                }               
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemovePartsdrawingInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public TextValueInfo[] ListBindProductName()
        {
            IList<BasBase> bseqs = DBContext.FindArray<BasBase>(BasBase.Meta.CODE=="QZB17050013");

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].SubCode;
                vts[i].Text = bseqs[i].SubName;
            }
            return vts;
        }

        public void SaveWorkOrderInfo(WorkOrderDetails obj)
        {
            try
            {
                     WorkOrder wotemp = new WorkOrder();
                    wotemp.BaseQty = obj.BaseQty;
                    wotemp.BatchNumber = obj.BatchNumber;
                    wotemp.CheckTime = obj.CheckTime;
                    wotemp.CLASS = obj.CLASS;
                    wotemp.CreatedDate = DateTime.Now;
                    wotemp.CustName = obj.CustName;
                    wotemp.EndTime = obj.EndTime;
                    wotemp.MachineName = obj.MachineName;
                    wotemp.MachineType = obj.MachineType;
                    wotemp.MaterialQty = obj.MaterialQty;
                    wotemp.MEMO = obj.MEMO;
                    wotemp.OrderNumber = obj.OrderNumber;
                    wotemp.PartsdrawingCode = obj.PartsdrawingCode;
                    wotemp.PlanQuantity = obj.PlanQuantity;
                    wotemp.ProductCode = obj.ProductCode;
                    wotemp.ProductName = obj.ProductName;
                    wotemp.QualityCode = obj.QualityCode;
                    wotemp.QUANTITY = obj.QUANTITY;
                    wotemp.StartTime = obj.StartTime;
                    wotemp.InTime = obj.InstockTime;
                    wotemp.STATUS = obj.STATUS;
                    wotemp.UnitTime = obj.UnitTime;
                    wotemp.UpdatedBy = this.UserCode;
                    wotemp.UpdatedDate = DateTime.Now;
                    wotemp.WO = obj.WO;
                    wotemp.WORKER = obj.WORKER;
                    wotemp.WorkerName = obj.WorkerName;
                    DBContext.SaveAndUpdate<WorkOrder>(wotemp); 
                   
                WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == obj.WO);
                if (wo== null)
                {
                    obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    DBContext.SaveAndUpdate<WorkOrderDetails>(obj);
                }else
                {
                    WorkOrderDetails wodetail = DBContext.Find<WorkOrderDetails>(WorkOrderDetails.Meta.WO == obj.WO
                        &WorkOrderDetails.Meta.StationName==obj.StationName&WorkOrderDetails.Meta.RouteCode==obj.RouteCode);

                    if (wodetail == null)
                    {
                        obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        obj.UpdatedBy = this.UserCode;
                        obj.UpdatedDate = DateTime.Now;
                        DBContext.SaveAndUpdate<WorkOrderDetails>(obj);
                    }
                    else
                    {
                        obj.ID = wodetail.ID;
                        obj.UpdatedBy = this.UserCode;
                        obj.UpdatedDate = DateTime.Now;
                        DBContext.SaveAndUpdate<WorkOrderDetails>(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveWorkOrderInfo");
                throw ex;
            }
        }
        public void UpdateWorkOrderInfo(string id,string status)
        {
            try
            {
                WorkOrderDetails wo = DBContext.Find<WorkOrderDetails>(WorkOrderDetails.Meta.ID == id);
                if (wo != null)
                {
                    if (status == "1")
                    {
                        if (wo.STATUS == "0" || wo.STATUS == "2")
                        {
                            wo.STATUS = status;
                            wo.StatusMemo = "运行";
                        }
                        else
                        {
                            throw new BusiException("不能运行工单，因为它正在运行或已经关闭.");
                        }
                    }
                    else
                    {
                        wo.STATUS = status;//状态（0：创建；1：运行；2：暂停；3：关闭；）
                        switch(status)
                        {
                            case "0":
                                wo.StatusMemo = "创建";
                                break;
                            case "1":
                                wo.StatusMemo = "运行";
                                break;
                            case "2":
                                wo.StatusMemo = "暂停";
                                break;
                            case "3":
                                wo.StatusMemo = "关闭";
                                break;
                        }
                    }
                    wo.UpdatedDate = DateTime.Now;
                    wo.UpdatedBy = this.UserCode;
                    
                }
                DBContext.SaveAndUpdate<WorkOrderDetails>(wo);
                //只要有一个工单下的工序是运行状态，工单就是运行状态
                //工单下的工序全部关闭，工单才关闭
                IList<WorkOrderDetails> wotemp = DBContext.FindArray<WorkOrderDetails>(WorkOrderDetails.Meta.WO == wo.WO);
                string temp_status = string.Empty;
                string temp_statusmemo = string.Empty;
                if (wotemp != null && wotemp.Count > 0)
                {
                    for (int k = 0; k < wotemp.Count;k++)
                    {
                        if(wotemp[k].STATUS=="1")
                        {
                            temp_status = "1";
                            temp_statusmemo = "运行";
                            break;
                        }
                    }
                    if(temp_status!="1")
                    {
                        temp_status = "3";
                        temp_statusmemo = "关闭";
                    }
                }
                WorkOrder wo_temp = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == wo.WO);
                if (wo_temp != null)
                {
                    wo_temp.STATUS = temp_status;
                    wo_temp.MEMO = temp_statusmemo;
                    wo_temp.UpdatedDate = DateTime.Now;
                    wo_temp.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<WorkOrder>(wo_temp);
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateWorkOrderInfo");
                throw ex;
            }
        }

        public void RemoveWorkOrder(string id)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                WorkOrderDetails wo = DBContext.Find<WorkOrderDetails>(WorkOrderDetails.Meta.ID == id);
                if (wo != null)
                {
                    if (wo.STATUS == "0" || wo.STATUS == "3")//状态（0：创建；1：运行；2：暂停；3：关闭；）
                    {
                        DBContext.Remove<WorkOrderDetails>(trans, WorkOrderDetails.Meta.ID == id);
                    }
                    else
                    {
                        throw new BusiException("不能删除工单，因为它在运行或暂停状态，请关闭后删除.");
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveWorkOrder");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<WorkOrder> FindWorkOrderInfo(string workorder)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
               // ce = (WorkOrder.Meta.WO == workorder);
                ce = (WorkOrder.Meta.WO.Like(workorder));
            }

            if (ce == null)
            {
                return DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS!="5");//5为无效工单
            }
            return DBContext.FindArray<WorkOrder>(ce,WorkOrder.Meta.UpdatedDate.DESC);
        }
        public IList<WorkOrderDetails> FindWorkOrderDetailsInfo(string workorder,string stationid="")
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            { 
                ce = (WorkOrderDetails.Meta.WO.Like(workorder));
            }
            if (!string.IsNullOrEmpty(stationid))
            { 
                ce = ce&(WorkOrderDetails.Meta.RouteCode.Like(stationid));
            }
            if (ce == null)
            {
                return DBContext.FindArray<WorkOrderDetails>(WorkOrderDetails.Meta.STATUS != "5");//5为无效工单
            }
            return DBContext.FindArray<WorkOrderDetails>(ce, WorkOrderDetails.Meta.UpdatedDate.DESC);
        }
        public IList<WorkOrder> FindWorkOrderInfo(WorkOrder workorder)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder.WO))
            {
                //ce = (WorkOrder.Meta.WO == workorder.WO);
                ce = (WorkOrder.Meta.WO.Like(workorder.WO));
            }
            if(!string.IsNullOrEmpty(workorder.OrderNumber))
            {
               // ce = (ce & WorkOrder.Meta.OrderNumber == workorder.OrderNumber);
                ce = (ce & WorkOrder.Meta.OrderNumber.Like(workorder.OrderNumber));
            }
            if(!string.IsNullOrEmpty(workorder.PartsdrawingCode))
            {
                //ce = (ce & WorkOrder.Meta.PartsdrawingCode == workorder.PartsdrawingCode);
                ce = (ce & WorkOrder.Meta.PartsdrawingCode.Like(workorder.PartsdrawingCode));
            }
            if(!string.IsNullOrEmpty(workorder.STATUS))
            {
                ce = (ce & WorkOrder.Meta.STATUS == workorder.STATUS);
            }
            if (!string.IsNullOrEmpty(workorder.CustName))
            {
                ce = (ce & WorkOrder.Meta.CustName == workorder.CustName);
            }
            if(!string.IsNullOrEmpty(workorder.WORKER))
            {
                ce = (ce & WorkOrder.Meta.WORKER == workorder.WORKER);
            }
            if(!string.IsNullOrEmpty(workorder.MachineName))
            {
                ce = (ce & WorkOrder.Meta.MachineName == workorder.MachineName);
            }
            if (null!=workorder.StartTime&&null!=workorder.EndTime)
            {
                if (DateTime.Compare((DateTime)workorder.StartTime,(DateTime) workorder.EndTime) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & WorkOrder.Meta.CreatedDate >= workorder.StartTime);
                ce = (ce & WorkOrder.Meta.CreatedDate <= workorder.EndTime);
            }
            if (ce == null)
            {
                ce =  WorkOrder.Meta.CreatedDate >= DateTime.Now.AddDays(-30);
                return DBContext.FindArray<WorkOrder>(ce,WorkOrder.Meta.CreatedDate.DESC);
            }
            return DBContext.FindArray<WorkOrder>(ce,WorkOrder.Meta.CreatedDate.DESC);
        }
        public IList<WorkOrderDetails> FindWorkOrderDetailsInfo(WorkOrderDetails workorder)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder.WO))
            { 
                ce = (WorkOrderDetails.Meta.WO.Like(workorder.WO));
            }
            if (!string.IsNullOrEmpty(workorder.OrderNumber))
            { 
                ce = (ce & WorkOrderDetails.Meta.OrderNumber.Like(workorder.OrderNumber));
            }
            if (!string.IsNullOrEmpty(workorder.PartsdrawingCode))
            { 
                ce = (ce & WorkOrderDetails.Meta.PartsdrawingCode.Like(workorder.PartsdrawingCode));
            }
            if (!string.IsNullOrEmpty(workorder.STATUS))
            {
                ce = (ce & WorkOrderDetails.Meta.STATUS == workorder.STATUS);
            }
            if (!string.IsNullOrEmpty(workorder.CustName))
            {
                ce = (ce & WorkOrderDetails.Meta.CustName == workorder.CustName);
            }
            if (!string.IsNullOrEmpty(workorder.WORKER))
            {
                ce = (ce & WorkOrderDetails.Meta.WORKER == workorder.WORKER);
            }
            if (!string.IsNullOrEmpty(workorder.MachineName))
            {
                ce = (ce & WorkOrderDetails.Meta.MachineName == workorder.MachineName);
            }
            if (null != workorder.StartTime && null != workorder.EndTime)
            {
                if (DateTime.Compare((DateTime)workorder.StartTime, (DateTime)workorder.EndTime) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & WorkOrderDetails.Meta.CreatedDate >= workorder.StartTime);
                ce = (ce & WorkOrderDetails.Meta.CreatedDate <= workorder.EndTime);
            }
            if (ce == null)
            {
                ce = WorkOrderDetails.Meta.CreatedDate >= DateTime.Now.AddDays(-30);
                return DBContext.FindArray<WorkOrderDetails>(ce, WorkOrderDetails.Meta.CreatedDate.DESC);
            }
            return DBContext.FindArray<WorkOrderDetails>(ce, WorkOrderDetails.Meta.CreatedDate.DESC);
        }

        public int FindWorkOrderCount(string workorder,string stationname,string stationid="")
        {
            string sql = "select distinct(psn) from tracking_history where work_order='"
                + workorder + "' and station_id='" + stationid + "'";
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            if(ds!=null&&ds.Tables[0].Rows.Count>0)
            {
                return ds.Tables[0].Rows.Count;
            }
            else
            {
                return 0;
            }
            //ConditionExpress ce = null;
            //if (!string.IsNullOrEmpty(workorder))
            //{
            //    ce = (TrackingWip.Meta.WorkOrder == workorder);
            //}
            //if (!string.IsNullOrEmpty(stationname))
            //{
            //    ce = (ce & TrackingWip.Meta.StationName != stationname);
            //}
            //if (!string.IsNullOrEmpty(stationid))
            //{
            //    ce = (ce & TrackingWip.Meta.StationId != stationid);
            //}
            //if (ce == null)
            //{
            //    return DBContext.LoadArray<TrackingWip>().Count;
            //}
            //return DBContext.FindArray<TrackingWip>(ce).Count;
        }

        public IList<WorkOrder> FindWorkOrderByPartsdrawingCode(string partsdrawingcode)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(partsdrawingcode))
            {
                ce = (WorkOrder.Meta.PartsdrawingCode == partsdrawingcode);
            }

            if (ce == null)
            {
                return DBContext.LoadArray<WorkOrder>();
            }
            return DBContext.FindArray<WorkOrder>(ce,WorkOrder.Meta.CreatedDate.DESC);
        }

        public IList<WorkOrder> FindWorkOrderByStatus(string status,bool isAssign,string drawingcode="" )
        {
            IList<WorkOrder> bseqs = null;
            if (isAssign)
            {
                if (string.IsNullOrEmpty(drawingcode))
                {
                    bseqs = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == status, WorkOrder.Meta.StartTime.ASC);
                }
                else
                {
                    bseqs = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == status & WorkOrder.Meta.PartsdrawingCode.Like(drawingcode), WorkOrder.Meta.UpdatedDate.DESC);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(drawingcode))
                {
                    bseqs = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == status & (WorkOrder.Meta.WORKER == null | WorkOrder.Meta.WORKER == ""), WorkOrder.Meta.UpdatedDate.DESC);
                }
                else
                {
                    bseqs = DBContext.FindArray<WorkOrder>(WorkOrder.Meta.STATUS == status & (WorkOrder.Meta.WORKER == null | WorkOrder.Meta.WORKER == "")&WorkOrder.Meta.PartsdrawingCode.Like(drawingcode), WorkOrder.Meta.UpdatedDate.DESC);

                }
            }
            return bseqs;            
        }
        public TextValueInfo[] ListBindOrderNo()
        {
            IList<OrderDetail> bseqs = DBContext.FindArray<OrderDetail>(OrderDetail.Meta.STATUS == "1");//订单状态（0：创建；1：发布；2：发货通知；3：关闭）

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].OrderNo;
                vts[i].Text = bseqs[i].OrderNo;
            }
            return vts;
        }
        public TextValueInfo[] ListBindPartsDrawingCode()
        {
            IList<PartsdrawingCode> bseqs = DBContext.FindArray<PartsdrawingCode>(PartsdrawingCode.Meta.ACTIVE == "1");

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].PartsCode;
                vts[i].Text = bseqs[i].PartsCode;
            }
            return vts;
        }

        public TextValueInfo[] ListBindPartsDrawingCodeBindOrder()
        {
            //查询已经绑定订单的未关闭的零件图号
          string  sql = "SELECT distinct(partsdrawing_code) FROM MES_MASTER.ORDER_DETAIL WHERE STATUS='1' OR STATUS='2' ORDER BY partsdrawing_code ASC                  ";
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int rows = ds.Tables[0].Rows.Count;
                TextValueInfo[] vts = new TextValueInfo[rows];
                for (var i = 0; i < rows; i++)
                {
                    vts[i] = new TextValueInfo();
                    vts[i].Value = ds.Tables[0].Rows[i][0].ToString();
                    vts[i].Text = ds.Tables[0].Rows[i][0].ToString();
                }
                return vts;
            }
            else
            {
                return null;
            }
        }

        public TextValueInfo[] ListBindPartsDrawingCodebyOrder(string order)
        {
            IList<OrderDetail> bseqs = DBContext.FindArray<OrderDetail>(OrderDetail.Meta.OrderNo == order);

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                if (!DBContext.Exist<WorkOrder>(WorkOrder.Meta.PartsdrawingCode == bseqs[i].PartsdrawingCode))
                {
                    vts[i] = new TextValueInfo();
                    vts[i].Value = bseqs[i].PartsdrawingCode;
                    vts[i].Text = bseqs[i].PartsdrawingCode+"|"+bseqs[i].BatchNumber;
                }
            }
            return vts;
        }

        public TextValueInfo[] ListBindOrderbyPartsDrawingCode(string PartsDrawingCode)
        {
            IList<OrderDetail> bseqs = DBContext.FindArray<OrderDetail>(OrderDetail.Meta.PartsdrawingCode == PartsDrawingCode);

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                 
                    vts[i] = new TextValueInfo();
                    vts[i].Value = bseqs[i].OrderNo;
                    vts[i].Text = bseqs[i].OrderNo;
                
            }
            return vts;
        }


        public TextValueInfo[] ListBindMachineType()
        {
            IList<BasBase> bseqs = DBContext.FindArray<BasBase>(BasBase.Meta.CODE == "QZB17040009");

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].SubCode;
                vts[i].Text = bseqs[i].SubName;
            }
            return vts;
        }

        public TextValueInfo[] ListBindMachines(string code)
        {
            IList<BasBase> bseqs = DBContext.FindArray<BasBase>(BasBase.Meta.CODE == code|BasBase.Meta.NAME==code);

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].SubCode;
                vts[i].Text = bseqs[i].SubName;
            }
            return vts;
        }

        public IList<MaterialStock> FindBackupInfo(string workorder)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
                WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == workorder);               
               if(wo!=null)
                {
                    //ce = (MaterialStock.Meta.MaterialName == wo.PartsdrawingCode&MaterialStock.Meta.BatchNumber==wo.BatchNumber);
                    ce = (MaterialStock.Meta.MaterialName == wo.PartsdrawingCode);
                }
            }
            else
            {
                return null;
            } 
            return DBContext.FindArray<MaterialStock>(ce);
        }
        public TextValueInfo[] ListBindUserByOperators()
        {
            IList<SysRoleUser> bseqs = DBContext.FindArray<SysRoleUser>(SysRoleUser.Meta.RoleId == "1000000006");//操作工角色为1000000006

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                SysUser su = FindUserByCode(bseqs[i].UserCode);
                vts[i] = new TextValueInfo();
                vts[i].Value = su.UserCode;
                vts[i].Text = su.UserName;
            }
            return vts;
        }

        public void SaveWorkOrderAssign(string workorder,string operater,string operatercode,string station)
        {
            try
            {
                if(string.IsNullOrEmpty(station))
                {
                    throw new Exception("工序不能为空");
                }
                string[] stationinfo = station.Split('|');
                WorkOrderDetails obj = DBContext.Find<WorkOrderDetails>(WorkOrderDetails.Meta.WO == workorder
                    &WorkOrderDetails.Meta.StationName==stationinfo[0]&WorkOrderDetails.Meta.RouteCode==stationinfo[1]);
                obj.WORKER = operatercode;
                obj.WorkerName = operater;
                obj.UpdatedDate = DateTime.Now;
                obj.UpdatedBy = this.UserCode;
                DBContext.SaveAndUpdate<WorkOrderDetails>(obj);
                WorkOrder obj2 = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == workorder);
                obj2.WORKER = operatercode;
                obj2.WorkerName = operater;
                obj2.UpdatedDate = DateTime.Now;
                obj2.UpdatedBy = this.UserCode;
                DBContext.SaveAndUpdate<WorkOrder>(obj2);
                //WorkOrder obj = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == workorder);
                //obj.WORKER = operatercode;
                //obj.WorkerName = operater;
                //obj.UpdatedDate = DateTime.Now;
                //obj.UpdatedBy = this.UserCode;                
                //DBContext.SaveAndUpdate<WorkOrder>(obj);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveWorkOrderAssign");
                throw ex;
            }
        }

        public string GetPSNByMSN(string MSN)
        {
            try
            {
                //产生规则：先在tracking_wip表中查找已经绑定的MSN记录，在记录上加1即可.
                string sql = "select count(0) from MES_MASTER.TRACKING_WIP where MSN='" + MSN + "'";
                int num = Convert.ToInt32(DBContext.ExcuteSql(sql).ToDataSet().Tables[0].Rows[0][0].ToString());
                num = num + 1;
                string PSN = MSN + num.ToString().PadLeft(2, '0');
                IList<MaterialStockHistory> msh = DBContext.FindArray<MaterialStockHistory>(MaterialStockHistory.Meta.MSN == MSN & MaterialStockHistory.Meta.STATUS == "1");
                if (msh.Count > 0)
                {
                    WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == msh[0].WorkOrder);
                    if(wo!=null)
                    {
                        TrackingWip tw = new TrackingWip();
                        tw.PSN = PSN;
                        tw.MSN = MSN;
                        tw.WorkOrder = wo.WO;
                        tw.PartsdrawingCode = wo.PartsdrawingCode;
                        tw.PartsName = wo.ProductName;
                        tw.PartsCode = wo.ProductCode;
                        tw.BatchNumber = wo.BatchNumber;
                        tw.StationName = "PRINT";
                        tw.QUANTITY = 1;
                        tw.STATUS = "P";
                        tw.InStatioonTime = DateTime.Now;
                        tw.OutStationTime = DateTime.Now;
                        tw.CreatedDate = DateTime.Now;
                        tw.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<TrackingWip>(tw);

                        TrackingHistory th = new TrackingHistory();
                        th.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        th.PSN = PSN;
                        th.MSN = MSN;
                        th.WorkOrder = wo.WO;
                        th.PartsdrawingCode = wo.PartsdrawingCode;
                        th.PartsName = wo.ProductName;
                        th.PartsCode = wo.ProductCode;
                        th.BatchNumber = wo.BatchNumber;
                        th.StationName = "PRINT";
                        th.QUANTITY = 1;
                        th.STATUS = "P";
                        th.InStationTime = DateTime.Now;
                        th.OutStationTime = DateTime.Now;
                        th.CreatedDate = DateTime.Now;
                        th.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<TrackingHistory>(th);
                        return PSN;
                    }
                    else
                    {
                        throw new Exception("该来料条码未出库");
                    }
                }
                else
                {
                    throw new Exception("来料条码不存在");
                }
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public IList<TrackingWip> FindPSNPrint(string PSN)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(PSN))
            {
                ce = (TrackingWip.Meta.PSN == PSN);
            }

            if (ce == null)
            {
                return DBContext.FindArray<TrackingWip>(TrackingWip.Meta.StationName=="PRINT");
            }
            return DBContext.FindArray<TrackingWip>(ce);
        }

        public IList<WorkOrder> FindLatheTask(string worker)
        {
            ConditionExpress ce = (WorkOrder.Meta.STATUS == "1");//运行

            if (!string.IsNullOrEmpty(worker))
            {
                //ce = (ce & WorkOrder.Meta.WORKER == worker);
                ce = (ce & WorkOrder.Meta.WORKER.Like(worker));
            }
            else
            {
                ce = (ce & WorkOrder.Meta.WORKER.Like(this.UserCode));
            }

            return DBContext.FindArray<WorkOrder>(ce);
        }
        public IList<TrackingHistory> FindLatheTaskHistory(string worker)
        {
            ConditionExpress ce = null;

            if (!string.IsNullOrEmpty(worker))
            {
                ce = (ce & TrackingHistory.Meta.UpdatedBy == worker);
            }
            else
            {
                ce = (ce & TrackingHistory.Meta.UpdatedBy == this.UserCode);
            }
            DateTime dtstart = DateTime.Now.Date;
            DateTime dtend = DateTime.Now;
             
            ce = (ce & TrackingHistory.Meta.CreatedDate >= dtstart);
            ce = (ce & TrackingHistory.Meta.CreatedDate <= dtend);
            
            return DBContext.FindArray<TrackingHistory>(ce,TrackingHistory.Meta.CreatedDate.DESC);
        }

        public TrackingTemp FindTrackingTemp(string ID)
        {
            ConditionExpress ce = null;

            if (!string.IsNullOrEmpty(ID))
            {
                ce = (ce & TrackingTemp.Meta.ID == ID);
            }
            else
            {
                return null;
            }
            
            return DBContext.Find<TrackingTemp>(ce);
        }

        public TrackingTemp LoadTrackingTemp(string station)
        {
            ConditionExpress ce = null;

            if (!string.IsNullOrEmpty(station))
            {
                ce = (ce & TrackingTemp.Meta.StationName == station&(TrackingTemp.Meta.UpdatedBy==this.UserCode|TrackingTemp.Meta.NextEmp==this.UserCode));
            }
            else
            {
                return null;
            }

            return DBContext.Find<TrackingTemp>(ce);
        }

        public string SaveTrackingTemp(TrackingTemp tt)
        {
            try
            {
                if (string.IsNullOrEmpty(tt.ID))
                {
                    tt.CreatedDate = DateTime.Now;
                    tt.UpdatedBy = this.UserCode;
                    tt.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    DBContext.SaveAndUpdate<TrackingTemp>(tt);
                }
                else
                {
                    tt.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<TrackingTemp>(tt);
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveTrackingTemp");
                throw ex;
            }
            return tt.ID;
        }

        public void RemoveTrackingTemp(string ID)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                DBContext.Remove<TrackingTemp>(trans, TrackingTemp.Meta.ID == ID);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveTrackingTempe");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public IList<TrackingHistory> FindSNTrackingInfo(string SN)
        {
            ConditionExpress ce = null;

            if (!string.IsNullOrEmpty(SN))
            {
                ce = (ce & (TrackingHistory.Meta.PSN == SN | TrackingHistory.Meta.MSN==SN));
            }
            else
            {
                ce = (ce & MaterialStock.Meta.CreatedDate >= DateTime.Now.AddDays(-7));
                ce = (ce & MaterialStock.Meta.CreatedDate <= DateTime.Now);
            }

            return DBContext.FindArray<TrackingHistory>(ce,TrackingHistory.Meta.CreatedDate.ASC);
        }

        public void SaveTrackingHistory(TrackingHistory obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ID))
                {
                    obj.CreatedDate = DateTime.Now;
                    obj.UpdatedBy = this.UserCode;
                    obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    DBContext.SaveAndUpdate<TrackingHistory>(obj);
                }
                else
                {
                    obj.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<TrackingHistory>(obj);
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveTrackingHistory");
                throw ex;
            }
            
        }
        public void SaveTrackingWip(TrackingWip obj)
        {
            try
            {
                obj.UpdatedBy = this.UserCode;
                obj.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<TrackingWip>(obj);               
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveTrackingWip");
                throw ex;
            }

        }
        public IList<TrackingWip> FindTrackingWip(string SN)
        {
            string strpsn = string.Empty;
            string sql = "select * from tracking_wip where 1=1 ";

            if (!string.IsNullOrEmpty(SN))
            {
                if (SN.IndexOf(',') != -1)
                {
                    string[] psns = SN.Split(',');
                    for (int t = 0; t < psns.Length; t++)
                    {
                        if (t == 0)
                        {
                            strpsn = "'" + psns[t] + "'";
                        }
                        else
                        {
                            strpsn += ",'" + psns[t] + "'";
                        }
                    }
                }
                else
                {
                    strpsn = "'" + SN + "'";
                }
                sql += " and PSN in(" + strpsn + ")";
            }
            IList<TrackingWip> itws = DBContext.ExcuteSql(sql).ToBusiObjects<TrackingWip>();//.FindArray<TrackingWip>(ce);
                                                                                            //ConditionExpress ce = null;

            //if (!string.IsNullOrEmpty(SN))
            //{
            //    ce = (ce & TrackingWip.Meta.PSN == SN);
            //}

            //IList<TrackingWip> itws = DBContext.FindArray<TrackingWip>(ce);
            if (itws.Count < 1)
            {
                return DBContext.FindArray<TrackingWip>(TrackingWip.Meta.MSN == SN);
            }
            return itws;
        }

        public IList<WorkOrder> FindQCTask()
        {
            ConditionExpress ce = (WorkOrder.Meta.STATUS == "1");
            return DBContext.FindArray<WorkOrder>(ce);
        }

        public IList<TrackingHistory> FindQCCheckHistory()
        {
            ConditionExpress ce = null;             
            ce = (ce & TrackingHistory.Meta.UpdatedBy == this.UserCode);            
            DateTime dtstart = DateTime.Now.Date;
            DateTime dtend = DateTime.Now;
            ce = (ce & TrackingHistory.Meta.CreatedDate >= dtstart);
            ce = (ce & TrackingHistory.Meta.CreatedDate <= dtend);
            return DBContext.FindArray<TrackingHistory>(ce, TrackingHistory.Meta.CreatedDate.DESC);
        }

        public IList<FailItems> FindFailItems(string failcode,string failtype,string failname)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(failcode))
            {
                ce = (ce & FailItems.Meta.FailCode == failcode);
            }
            if (!string.IsNullOrEmpty(failtype))
            {
                ce = (ce & FailItems.Meta.FailType == failtype);
            }
            if (!string.IsNullOrEmpty(failname))
            {
                ce = (ce & FailItems.Meta.FailMemo == failname);
            }
           
            if(ce==null)
            {
                return DBContext.LoadArray<FailItems>();
            }
            return DBContext.FindArray<FailItems>(ce);
        }

        public void SaveUnsurenessProduct(UnsurenessProduct obj)
        {
            try
            {
                obj.UpdatedBy = this.UserCode;
                obj.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<UnsurenessProduct>(obj);

                UnsurenessHistory up = new UnsurenessHistory();
                up.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                up.PSN = obj.PSN;
                up.MSN = obj.MSN;
                up.WorkOrder = obj.WorkOrder;
                up.FailCode = obj.FailCode;
                up.FailMemo = obj.FailMemo;
                up.STATUS = obj.STATUS;
                up.MEMO = obj.MEMO;
                up.StationName =obj.StationName;
                up.QUANTITY = obj.QUANTITY;
                up.PartsdrawingCode = obj.PartsdrawingCode;
                up.ProductName = obj.ProductName;
                up.BatchNumber = obj.BatchNumber;
                up.CreatedDate = DateTime.Now;
                SaveUnsurenessHistory(up);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveUnsurenessProduct");
                throw ex;
            }

        }

        public void SaveUnsurenessHistory(UnsurenessHistory obj)
        {
            try
            {
                obj.UpdatedBy = this.UserCode;
                obj.UpdatedDate = DateTime.Now;
                DBContext.SaveAndUpdate<UnsurenessHistory>(obj);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveUnsurenessHistory");
                throw ex;
            }

        }
        public void SaveUnsurenessProduct(string psn,string failcode,string failmemo)
        {
            try
            {
                IList<TrackingWip> tw = FindTrackingWip(psn);
                if (tw.Count > 0)
                {
                    UnsurenessProduct uproduct = new UnsurenessProduct();
                    uproduct.PSN = psn;
                    uproduct.MSN = tw[0].MSN;
                    uproduct.WorkOrder = tw[0].WorkOrder;
                    uproduct.FailCode = failcode;
                    uproduct.FailMemo = failmemo;
                    uproduct.STATUS = "0";
                    uproduct.MEMO = "待处理";
                    uproduct.StationName = tw[0].StationName;
                    uproduct.QUANTITY = tw[0].QUANTITY;
                    uproduct.PartsdrawingCode = tw[0].PartsdrawingCode;
                    uproduct.ProductName = tw[0].PartsName;
                    uproduct.BatchNumber = tw[0].BatchNumber;
                    uproduct.UpdatedBy = this.UserCode;
                    uproduct.CreatedDate = DateTime.Now;
                    uproduct.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<UnsurenessProduct>(uproduct);

                    UnsurenessHistory up = new UnsurenessHistory();
                    up.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    up.PSN = uproduct.PSN;
                    up.MSN = uproduct.MSN;
                    up.WorkOrder = uproduct.WorkOrder;
                    up.FailCode = uproduct.FailCode;
                    up.FailMemo = uproduct.FailMemo;
                    up.STATUS = uproduct.STATUS;
                    up.MEMO = uproduct.MEMO;
                    up.StationName = uproduct.StationName;
                    up.QUANTITY = uproduct.QUANTITY;
                    up.PartsdrawingCode = uproduct.PartsdrawingCode;
                    up.ProductName = uproduct.ProductName;
                    up.BatchNumber = uproduct.BatchNumber;
                    up.CreatedDate = DateTime.Now;
                    SaveUnsurenessHistory(up);
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveUnsurenessProduct");
                throw ex;
            }

        }

        public IList<UnsurenessProduct> FindUnsurenessProduct(string psn)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(psn))
            {
                ce = (ce & UnsurenessProduct.Meta.PSN == psn);
            }
            //if (!string.IsNullOrEmpty(failtype))
            //{
            //    ce = (ce & FailItems.Meta.FailType == failtype);
            //}
            //if (!string.IsNullOrEmpty(failname))
            //{
            //    ce = (ce & FailItems.Meta.FailMemo == failname);
            //}

            if (ce == null)
            {
                return DBContext.LoadArray<UnsurenessProduct>();
            }
            return DBContext.FindArray<UnsurenessProduct>(ce);
        }

        public IList<UnsurenessProduct> FindUnsurenessProductOut(string psn)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(psn))
            {
                ce = (ce & UnsurenessProduct.Meta.PSN == psn);
            }
             
            //if (!string.IsNullOrEmpty(failname))
            //{
            //    ce = (ce & FailItems.Meta.FailMemo == failname);
            //}

            if (ce == null)
            {
                //状态（0：待处理；1：返工；2：让步接收；3：报废；4：已审核）
                return DBContext.FindArray<UnsurenessProduct>(UnsurenessProduct.Meta.STATUS=="0"| UnsurenessProduct.Meta.STATUS=="1"| UnsurenessProduct.Meta.STATUS=="2"| UnsurenessProduct.Meta.STATUS=="3");
            }
            return DBContext.FindArray<UnsurenessProduct>(ce);
        }
        public IList<UnsurenessProduct> FindCheckUnsurenessProduct(string psn)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(psn))
            {
                ce = (ce & UnsurenessProduct.Meta.PSN == psn);
            }
             
            if (ce == null)
            {
                //状态（0：待处理；1：返工；2：让步接收；3：报废；4：已审核）
                return DBContext.FindArray<UnsurenessProduct>(UnsurenessProduct.Meta.STATUS == "1" | UnsurenessProduct.Meta.STATUS == "2" | UnsurenessProduct.Meta.STATUS == "3");
            }
            return DBContext.FindArray<UnsurenessProduct>(ce);
        }

        public IList<PrintSet> FindPrintSet(string templatetype)
        {
            ConditionExpress ce = null;

            if (!string.IsNullOrEmpty(templatetype))
            {
                ce = (ce & PrintSet.Meta.TemplateType == templatetype);
            }
            
            if(ce==null)
            {
                return DBContext.LoadArray<PrintSet>();
            }
            IList<PrintSet> itws = DBContext.FindArray<PrintSet>(ce);             
            return itws;
        }

        public void SavePrintSet(string templatetype, string templatecode, string isactive)
        {
            try
            {
                if(isactive=="0")
                {
                    PrintSet obj = DBContext.Find<PrintSet>(PrintSet.Meta.ID == templatecode);
                    if(obj!=null)
                    {
                        obj.ACTIVE = isactive;
                        DBContext.SaveAndUpdate<PrintSet>(obj);
                    }
                    else
                    {
                        throw new Exception("无此模板");
                    }
                }
                else
                {
                     IList<PrintSet> objs = FindPrintSet(templatetype);
                    foreach(PrintSet o in objs)
                    {
                        if(o.ID==templatecode)
                        {
                            o.ACTIVE = "1";
                        }
                        else
                        {
                            o.ACTIVE = "0";
                        }
                        DBContext.SaveAndUpdate<PrintSet>(o);
                    }
                }               
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SavePrintSet");
                throw ex;
            }

        }

        public void RemovePrintSet(string templatecode)
        {
            try
            {
                PrintSet obj = DBContext.Find<PrintSet>(PrintSet.Meta.ID == templatecode);
                if (obj != null)
                {
                    if (obj.ACTIVE == "1")
                    {
                        throw new Exception("此模板当前为激活状态，禁止删除");
                    }
                    else
                    {
                        DBContext.Remove<PrintSet>(obj);
                    }

                }
                else
                {
                    throw new Exception("无此模板");
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemovePrintSet");
                throw ex;
            }

        }

        public void SavePrintLabelTemplate(PrintSet obj)
        {
            try
            {
                if(string.IsNullOrEmpty(obj.ID))
                {
                    obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    obj.CreatedDate = DateTime.Now;
                    obj.UpdatedBy = this.UserCode;

                }
                 
                DBContext.SaveAndUpdate<PrintSet>(obj);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SavePrintLabelTemplate");
                throw ex;
            }

        }

        public void SaveCartonTemp(CartonTemp obj)
        {
            try
            {
                obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                DBContext.SaveAndUpdate<CartonTemp>(obj);               
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveCartonTemp");
                throw ex;
            }
        }

        //public void RemoveCartonTemp(string ip)
        //{
        //    try
        //    {
        //        DBContext.Remove<CartonTemp>(CartonTemp.Meta.IP == ip);
        //    }
        //    catch (Exception ex)
        //    {
        //        PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveCartonTemp");
        //        throw ex;
        //    }
        //}
        public void RemoveCartonTemp(string ip, string type)
        {
            try
            {
                DBContext.Remove<CartonTemp>(CartonTemp.Meta.IP == ip & CartonTemp.Meta.TYPE == type);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveCartonTemp");
                throw ex;
            }
        }
        public IList<CartonTemp> FindCartonTemp(string ip)
        {
            try
            {
               return DBContext.FindArray<CartonTemp>(CartonTemp.Meta.IP == ip);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "FindCartonTemp");
                throw ex;
            }
        }

        //public void SaveCartonInfo(string ip,string carton)
        //{
        //    try
        //    {
        //        IList<CartonTemp> objs = FindCartonTemp(ip);
        //        if(objs.Count>0)
        //        {
        //            //保存箱号
        //            foreach(CartonTemp ct in objs)
        //            {
        //                CartonInfo ci = new CartonInfo();
        //                ci.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
        //                ci.PartsdrawingCode = ct.PartsdrawingCode;
        //                ci.QualityCode = ct.QualityCode;
        //                ci.QUANTITY = ct.QUANTITY;
        //                ci.CSN = carton;
        //                IList<WorkOrder> wo = FindWorkOrderByPartsdrawingCode(ct.PartsdrawingCode);
        //                if(wo.Count>0)
        //                {
        //                    ci.OrderNumber = wo[0].OrderNumber;
        //                }
        //                ci.CreatedDate = DateTime.Now;
        //                ci.UpdatedBy = this.UserCode;
        //                DBContext.SaveAndUpdate<CartonInfo>(ci);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveCartonInfo");
        //        throw ex;
        //    }
        //}
        public void SaveCartonInfo(string ip, string carton, string type)
        {
            try
            {
                IList<CartonTemp> objs = FindCartonTemp(ip, type);
                if (objs.Count > 0)
                {
                    //保存箱号
                    foreach (CartonTemp ct in objs)
                    {
                        CartonInfo ci = new CartonInfo();
                        ci.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        ci.PartsdrawingCode = ct.PartsdrawingCode;
                        ci.QualityCode = ct.QualityCode;
                        ci.QUANTITY = ct.QUANTITY;
                        ci.CSN = carton;
                        ci.ProductName = ct.ProductName;
                        ci.BatchNumber = ct.BatchNumber;
                        IList<WorkOrder> wo = FindWorkOrderByPartsdrawingCode(ct.PartsdrawingCode);
                        if (wo.Count > 0)
                        {
                            ci.OrderNumber = wo[0].OrderNumber;
                        }
                        ci.CreatedDate = DateTime.Now;
                        ci.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<CartonInfo>(ci);
                    }

                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveCartonInfo");
                throw ex;
            }
        }
        public IList<CartonInfo> FindCartonInfo(string csn,string orderno,string user,DateTime start,DateTime end)
        {
            try
            {
                ConditionExpress ce = null;
                if (!string.IsNullOrEmpty(csn))
                {
                    ce = (ce & CartonInfo.Meta.CSN == csn);
                }
                if (!string.IsNullOrEmpty(orderno))
                {
                    ce = (ce & CartonInfo.Meta.OrderNumber == orderno);
                }

                if (!string.IsNullOrEmpty(user))
                {
                    ce = (ce & FailItems.Meta.UpdatedBy == user);
                }
                if(start!=null&&end!=null)
                {
                    ce = (ce & CartonInfo.Meta.CreatedDate >= start);
                    ce = (ce & CartonInfo.Meta.CreatedDate <= end);
                }
                if(ce==null)
                {
                    return DBContext.LoadArray<CartonInfo>();
                }
                return DBContext.FindArray<CartonInfo>(ce, CartonInfo.Meta.CreatedDate.DESC);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "FindCartonInfo");
                throw ex;
            }
        }
        /// <summary>
        /// yajiao
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TextValueInfo[] ListBaseByCode(string code)
        {
            IList<BasBase> bases = DBContext.FindArray<BasBase>(BasBase.Meta.CODE == code);

            TextValueInfo[] vts = new TextValueInfo[bases.Count];
            if (bases != null && bases.Count > 0)
            {
                for (var i = 0; i < bases.Count; i++)
                {
                    vts[i] = new TextValueInfo();
                    vts[i].Value = bases[i].SubCode;
                    vts[i].Text = bases[i].SubName;
                }
            }

            return vts;
        }
        public IList<MaterialStock> FindStockInfo(string status, string materialname, string custname, string starttime, string endtime, string batchnumber)
        {
            ConditionExpress ce = null;
           
            if (!string.IsNullOrEmpty(status))
            {
                if (status.IndexOf('^') != -1)
                {
                    string[] strtemp = status.Split('^');
                    ce = (ce & (MaterialStock.Meta.STATUS == strtemp[0] | MaterialStock.Meta.STATUS == strtemp[1]));
                }
                ce = (ce & MaterialStock.Meta.STATUS == status);
            }
          
            if (!string.IsNullOrEmpty(materialname))
            {
                ce = (ce & MaterialStock.Meta.MaterialName == materialname);
            }
            if (!string.IsNullOrEmpty(custname))
            {
                ce = (ce & MaterialStock.Meta.CustName == custname);
            }
            if (!string.IsNullOrEmpty(batchnumber))
            {
                ce = (ce & MaterialStock.Meta.BatchNumber == batchnumber);
            }
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                ce = (ce & MaterialStock.Meta.CreatedDate >= Convert.ToDateTime(starttime));
                ce = (ce & MaterialStock.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }
            else
            {
                ce = (ce & MaterialStock.Meta.CreatedDate >= DateTime.Now.AddDays(-7));
                ce = (ce & MaterialStock.Meta.CreatedDate <= DateTime.Now);
            }

            return DBContext.FindArray<MaterialStock>(ce);
        }

        public void SaveFailItems(FailItems objs)
        {
            try
            {                
                if (!DBContext.Exist<FailItems>(FailItems.Meta.FailCode == objs.FailCode))
                {
                    objs.CreatedDate = DateTime.Now;
                    
                }
                else
                {
                    objs.UpdatedDate = DateTime.Now;
                }
                objs.UpdatedBy = this.UserCode;
                DBContext.SaveAndUpdate<FailItems>(objs);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveFailItems");
                throw ex;
            }
        }
        public void RemoveFailItems(string failcode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                FailItems od = DBContext.Find<FailItems>(FailItems.Meta.FailCode == failcode);
                if (od != null)
                {
                    DBContext.Remove<FailItems>(trans, FailItems.Meta.FailCode == failcode);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveFailItems");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        public IList<FailItems> FindFailItems(string failcode, string failtype)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(failcode))
            {
                ce = (FailItems.Meta.FailCode == failcode);
            }

            if (!string.IsNullOrEmpty(failtype))
            {
                ce = (ce & FailItems.Meta.MEMO1==failtype);
            }
            if (ce == null)
            {
                return DBContext.LoadArray<FailItems>();
            }

            return DBContext.FindArray<FailItems>(ce);

        }

        public void SaveCheckUnsureness(string psn,string result)
        {
            try
            {
                UnsurenessProduct up = DBContext.Find<UnsurenessProduct>(UnsurenessProduct.Meta.PSN == psn);
                if(up!=null)
                {
                    if(result=="1")
                    {
                        //保存历史记录
                        IList<TrackingWip> tw = FindTrackingWip(psn);
                        if (tw.Count > 0)
                        {
                            //保存检查历史记录
                            TrackingHistory th = new TrackingHistory();
                            th.PSN = tw[0].PSN;
                            th.MSN = tw[0].MSN;
                            th.WorkOrder = tw[0].WorkOrder;
                            th.PartsdrawingCode = tw[0].PartsdrawingCode;
                            th.PartsName = tw[0].PartsName;
                            th.PartsCode = tw[0].PartsCode;
                            th.BatchNumber = tw[0].BatchNumber;
                            th.StationName = "CheckUnsureness";
                            th.StationId = "CheckUnsureness";
                            th.QUANTITY = 1;
                            th.STATUS = "P";
                            th.MEMO = up.MEMO;
                            th.InStationTime = up.CreatedDate;
                            th.OutStationTime = up.UpdatedDate;
                            IList<WorkOrder> wo = FindWorkOrderInfo(tw[0].WorkOrder);
                            th.MachineName = wo[0].MachineName;
                            th.MachineType = wo[0].MachineType;
                            th.CreatedDate = DateTime.Now;
                            th.UpdatedBy = this.UserCode;
                            if (up.STATUS == "1")
                            {
                                th.NextStation = tw[0].StationName;
                                th.NextStationId = tw[0].StationId;
                            }
                            else if (up.STATUS == "2")//让步接收
                            {
                                th.NextStation = tw[0].NextStation;
                                th.NextStationId = tw[0].NextStationId;
                            }
                            else if (up.STATUS == "3")//报废
                            {
                                th.NextStation = "INSTOCK";
                                th.NextStationId = "INSTOCK";
                            }
                            SaveTrackingHistory(th);
                        }
                        else
                        {
                            throw new Exception("无此条码");
                        }


                        //先检查处理结果是不是让步接收，如果是，则修改route_wip表，保证能直接入库
                        if (up.STATUS == "1")//返工
                        { 
                            tw[0].NextStation = tw[0].StationName;
                            tw[0].NextStationId = tw[0].StationId;
                            tw[0].InStatioonTime = up.CreatedDate;
                            tw[0].OutStationTime = DateTime.Now;
                            tw[0].UpdatedDate = DateTime.Now;
                            SaveTrackingWip(tw[0]);
                        }
                        else if (up.STATUS=="2")//让步接收
                        {
                            //if (tw[0].StationName == "QC")
                            //{
                            //    tw[0].NextStation = "INSTOCK";
                            //}
                            tw[0].STATUS = "P";
                            tw[0].InStatioonTime = up.CreatedDate;
                            tw[0].OutStationTime = DateTime.Now;
                            tw[0].UpdatedDate = DateTime.Now;                            
                            SaveTrackingWip(tw[0]);
                            //保存历史记录
                            //TrackingHistory th = new TrackingHistory();
                            //th.PSN = tw[0].PSN;
                            //th.MSN = tw[0].MSN;
                            //th.WorkOrder = tw[0].WorkOrder;
                            //th.PartsdrawingCode = tw[0].PartsdrawingCode;
                            //th.PartsName = tw[0].PartsName;
                            //th.PartsCode = tw[0].PartsCode;
                            //th.BatchNumber = tw[0].BatchNumber;
                            //th.StationName = tw[0].StationName;
                            //th.StationId = tw[0].StationId;
                            //th.NextStation = tw[0].NextStation;
                            //th.NextStationId = tw[0].NextStationId;
                            //th.QUANTITY = 1;
                            //th.STATUS = "P";                            
                            //th.InStationTime = up.CreatedDate;
                            //th.OutStationTime = DateTime.Now;
                            //IList<WorkOrder> wo = FindWorkOrderInfo(tw[0].WorkOrder);
                            //th.MachineName = wo[0].MachineName;
                            //th.MachineType = wo[0].MachineType;
                            //th.CreatedDate = DateTime.Now;
                            //th.UpdatedBy = this.UserCode;
                            //SaveTrackingHistory(th);

                            //保存到实时统计表
                            IList<WorkOrderDetails> wo = FindWorkOrderDetailsInfo(tw[0].WorkOrder,tw[0].StationId);
                            RealtimeStatistics rs = new RealtimeStatistics();
                            rs.PSN = tw[0].PSN;
                            rs.MSN = tw[0].MSN;
                            rs.WorkOrder = tw[0].WorkOrder;
                            rs.StationName = tw[0].StationName;
                            //rs.MachineType = tw[0].MachineType;
                            //rs.MachineName = tw[0].MachineName;
                            rs.STATUS = tw[0].STATUS;
                            rs.QUANTITY = tw[0].QUANTITY;
                            rs.OPERATOR = tw[0].UpdatedBy;
                            if (wo.Count > 0)
                            {
                                rs.OrderNumber = wo[0].OrderNumber;
                            }
                            rs.ProductName = tw[0].PartsName;
                            rs.ProductCode = tw[0].PartsCode;
                            IList<PartsdrawingCode> pc = FindPartsdrawingInfo(tw[0].PartsdrawingCode);
                            if (pc.Count > 0)
                            {
                                rs.CustName = pc[0].CustName;
                            }
                            rs.PartsdrawingCode = tw[0].PartsdrawingCode;
                           SaveRealtimeStatistics(rs);

                        }
                        else if(up.STATUS=="3")//报废
                        {
                            tw[0].NextStation = "INSTOCK";
                            tw[0].NextStationId = "INSTOCK";
                            tw[0].InStatioonTime = up.CreatedDate;
                            tw[0].OutStationTime = DateTime.Now;
                            tw[0].UpdatedDate = DateTime.Now;
                            SaveTrackingWip(tw[0]);
                        }
                        up.STATUS = "4";
                        up.MEMO = "已审核";
                        up.UpdatedBy = this.UserCode;
                        up.UpdatedDate = DateTime.Now;
                        DBContext.SaveAndUpdate<UnsurenessProduct>(up);

                        UnsurenessHistory uh = new UnsurenessHistory();
                        uh.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                        uh.PSN = up.PSN;
                        uh.MSN = up.MSN;
                        uh.MEMO = up.MEMO;
                        uh.PartsdrawingCode = up.PartsdrawingCode;
                        uh.ProductName = up.ProductName;
                        uh.QUANTITY = up.QUANTITY;
                        uh.StationName = up.StationName;
                        uh.STATUS = up.STATUS;
                        uh.WorkOrder = up.WorkOrder;
                        uh.UpdatedBy = up.UpdatedBy;
                        uh.CreatedDate = DateTime.Now;
                        uh.BatchNumber = up.BatchNumber;
                        uh.FailCode = up.FailCode;
                        uh.FailMemo = up.FailMemo;
                        DBContext.SaveAndUpdate<UnsurenessHistory>(uh);
                    }
                }
                 
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveCheckUnsureness");
                throw ex;
            }
        }
        public void SaveRealtimeStatistics(RealtimeStatistics obj)
        {
            try
            {
                if (DBContext.Exist<RealtimeStatistics>(RealtimeStatistics.Meta.PSN==obj.PSN&RealtimeStatistics.Meta.StationName==obj.StationName))
                {
                    RealtimeStatistics rs = DBContext.Find<RealtimeStatistics>(RealtimeStatistics.Meta.PSN == obj.PSN & RealtimeStatistics.Meta.StationName == obj.StationName);
                    obj.ID = rs.ID;
                    obj.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<RealtimeStatistics>(obj);
                    
                }
                else
                {
                    obj.CreatedDate = DateTime.Now;
                    obj.UpdatedBy = this.UserCode;
                    obj.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    DBContext.SaveAndUpdate<RealtimeStatistics>(obj);
                }
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveRealtimeStatistics");
                throw ex;
            }

        }

        public int FindFailCountbyWorkOrder(string workorder,string partsdrawing)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
                ce = (UnsurenessProduct.Meta.WorkOrder == workorder);
            }

            if (!string.IsNullOrEmpty(partsdrawing))
            {
                ce = (UnsurenessProduct.Meta.PartsdrawingCode == partsdrawing);
            }

            if (ce == null)
            {
                return DBContext.LoadArray<UnsurenessProduct>().Count;
            }
            return DBContext.FindArray<UnsurenessProduct>(ce).Count;
        }

        public IList<UnsurenessHistory> FindUnsurenessHistory(UnsurenessHistory uh)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(uh.PSN))
            {
                ce = (ce & UnsurenessHistory.Meta.PSN == uh.PSN);
            }
            if (!string.IsNullOrEmpty(uh.WorkOrder))
            {
               // ce = (UnsurenessHistory.Meta.WorkOrder == uh.WorkOrder);
                ce = (UnsurenessHistory.Meta.WorkOrder.Like(uh.WorkOrder));
            }
            if (!string.IsNullOrEmpty(uh.PartsdrawingCode))
            {
               // ce = (ce & UnsurenessHistory.Meta.PartsdrawingCode == uh.PartsdrawingCode);
                ce = (ce & UnsurenessHistory.Meta.PartsdrawingCode.Like(uh.PartsdrawingCode));
            }
            if (!string.IsNullOrEmpty(uh.STATUS))
            {
                ce = (ce & UnsurenessHistory.Meta.STATUS == uh.STATUS);
            }
            if (null!=uh.CreatedDate&&null!=uh.UpdatedDate)
            {
                if (DateTime.Compare((DateTime)uh.CreatedDate, (DateTime)uh.UpdatedDate) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & UnsurenessHistory.Meta.CreatedDate >= uh.CreatedDate);
                ce = (ce & UnsurenessHistory.Meta.CreatedDate <= uh.UpdatedDate);
            }
            
            if (ce == null)
            {
                return DBContext.LoadArray<UnsurenessHistory>();
            }
            return DBContext.FindArray<UnsurenessHistory>(ce);
        }

        public int[] FindYieldCountInfo(string workorder, string partsdrawing)
        {
            ConditionExpress ce = null;
            IList<UnsurenessProduct> uplist = null;
            int[] res = new int[4];//0:不良数量；1：返工数量；2：让步数量：3：废品数量
            if (!string.IsNullOrEmpty(workorder))
            {
                ce = (UnsurenessProduct.Meta.WorkOrder == workorder);
            }

            if (!string.IsNullOrEmpty(partsdrawing))
            {
                ce = (ce & UnsurenessProduct.Meta.PartsdrawingCode == partsdrawing);
            }

            if (ce == null)
            {
                uplist = DBContext.LoadArray<UnsurenessProduct>();
            }
            else
            {
                uplist = DBContext.FindArray<UnsurenessProduct>(ce);
            }
            res[0] = uplist.Count;
            IList<UnsurenessHistory> uhlist = DBContext.FindArray<UnsurenessHistory>(UnsurenessHistory.Meta.WorkOrder == workorder & UnsurenessHistory.Meta.STATUS == "1");
            if(uhlist==null)
            {
                res[1] = 0;
            }
            else
            {
                res[1] = uhlist.Count;
            }

            uhlist = DBContext.FindArray<UnsurenessHistory>(UnsurenessHistory.Meta.WorkOrder == workorder & UnsurenessHistory.Meta.STATUS == "2");
            if (uhlist == null)
            {
                res[2] = 0;
            }
            else
            {
                res[2] = uhlist.Count;
            }
            uhlist = DBContext.FindArray<UnsurenessHistory>(UnsurenessHistory.Meta.WorkOrder == workorder & UnsurenessHistory.Meta.STATUS == "3");
            if (uhlist == null)
            {
                res[3] = 0;
            }
            else
            {
                res[3] = uhlist.Count;
            }

            return res;
        }
        public int FindCountFromRealtimeStatistics(RealtimeStatistics rs)
        {
            string sql = @"SELECT count(distinct(psn)) from MES_MASTER.realtime_statistics where 1=1  ";             
           
            if (!string.IsNullOrEmpty(rs.WorkOrder))
            {
                sql +=" and work_order= '"+ rs.WorkOrder+"'";
            }

            if (!string.IsNullOrEmpty(rs.PartsdrawingCode))
            {
                sql += " and partsdrawing_code= '" + rs.PartsdrawingCode + "'";
            }
            if (!string.IsNullOrEmpty(rs.STATUS))
            {
                sql += " and status= '" + rs.STATUS + "'";
            }
            if (!string.IsNullOrEmpty(rs.StationName))
            {
                sql += " and station_name= '" + rs.StationName + "'";
            }
            
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }

        /// <summary>
        /// yajiao
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ListCustCodeByName(string name)
        {
            IList<BasCustom> bases = DBContext.FindArray<BasCustom>(BasBase.Meta.NAME == name);

             
            if (bases != null && bases.Count > 0)
            {
                return bases[0].CODE;
            }

            return "";
        }
		public TextValueInfo[] ListBaseName()
        {
            IList<BasBase> menus = DBContext.ExcuteSql("SELECT DISTINCT(CODE),NAME FROM MES_MASTER.BAS_BASE ").ToBusiObjects<BasBase>();

            TextValueInfo[] vts = new TextValueInfo[menus.Count];
            for (var i = 0; i < menus.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = menus[i].CODE;
                vts[i].Text = menus[i].NAME;
            }

            return vts;
        }

        public bool CheckUnSurenessOut(string psn)
        {
            IList<UnsurenessProduct> up = DBContext.FindArray<UnsurenessProduct>(UnsurenessProduct.Meta.PSN == psn & UnsurenessProduct.Meta.STATUS != "4");
            if (up != null && up.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public TextValueInfo[] ListPartCode()
        {
            IList<PartsdrawingCode> bseqs = DBContext.FindArray<PartsdrawingCode>(PartsdrawingCode.Meta.ACTIVE == "1", PartsdrawingCode.Meta.CreatedDate.DESC);

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].PartsCode;
                vts[i].Text = bseqs[i].PartsCode;
            }
            return vts;
        }

        public string GetcustByPartCode(string partCode)
        {
            if (string.IsNullOrEmpty(partCode))
            {
                return "";
            }
            PartsdrawingCode pc = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.PartsCode == partCode);
            if (pc != null && !string.IsNullOrEmpty(pc.CustCode))
            {
                return pc.CustCode;
            }
            else
            {
                return "";
            }

        }
        public string GetProductByPartCode(string partCode)
        {
            if (string.IsNullOrEmpty(partCode))
            {
                return "";
            }
            PartsdrawingCode pc = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.PartsCode == partCode);
            if (pc != null && !string.IsNullOrEmpty(pc.ProductCode))
            {
                return pc.ProductCode;
            }
            else
            {
                return "";
            }

        }

        public IList<MaterialStock> FindMSNByDocumentID(string documentid)
        {
            if(string.IsNullOrEmpty(documentid))
            {
                return null;
            }
            IList<MaterialStock> mss = DBContext.FindArray<MaterialStock>(MaterialStock.Meta.DOCUMENTID == documentid & MaterialStock.Meta.STATUS == "0");
            return mss;
        }

        public TextValueInfo[] ListMaterialCode()
        {
            IList<BasMateriel> bseqs = DBContext.LoadArray<BasMateriel>();

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].QPARTNO;
                vts[i].Text = bseqs[i].QPARTNO;
            }
            return vts;
        }
        public string GetCMaterialByQMaterialCode(string qmaterialcode)
        {
            if (string.IsNullOrEmpty(qmaterialcode))
            {
                return "";
            }
            BasMateriel bm = DBContext.Find<BasMateriel>(BasMateriel.Meta.QPARTNO == qmaterialcode);
            if (bm != null && !string.IsNullOrEmpty(bm.CPARTNO))
            {
                return bm.CPARTNO;
            }
            else
            {
                return "";
            }

        }

        public TextValueInfo[] ListMaterialCodeName()
        {
            string sql = "SELECT * FROM MES_MASTER.BAS_MATERIEL ORDER BY NAME ";
            IList<BasMateriel> bseqs = DBContext.ExcuteSql(sql).ToBusiObjects<BasMateriel>(); ;
            //IList<BasMateriel> bseqs = DBContext.LoadArray<BasMateriel>();

            TextValueInfo[] vts = new TextValueInfo[bseqs.Count];
            for (var i = 0; i < bseqs.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = bseqs[i].QPARTNO;
                vts[i].Text = bseqs[i].NAME;
            }
            return vts;
        }
        public IList<CartonTemp> FindCartonTemp(string ip, string type)
        {
            try
            {
                return DBContext.FindArray<CartonTemp>(CartonTemp.Meta.IP == ip & CartonTemp.Meta.TYPE == type);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "FindCartonTemp");
                throw ex;
            }
        }
        /// <summary>
        /// 查询当天产出
        /// </summary>
        /// <returns></returns>
        public int QueryCurOutQty()
        {
            int total = 0;
            string sql = @"select sum(quantity) count from realtime_statistics where station_name='CHEXI' and created_date>TO_DATE(TO_CHAR(sysdate,'YYYY/MM/DD'),'YYYY/MM/DD')";
             
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string s = ds.Tables[0].Rows[0][0].ToString();
                total = Convert.ToInt32(s==string.Empty?"0":s);
            }
            return total;

        }
        /// <summary>
        /// 根据机床名称查询机床产出，工单和图号
        /// </summary>
        /// <returns></returns>
        public string[] QueryCurMachineInfo(string machinename)
        {
            //查产出，查工单，查图号，查计划产出比
            string[] res = new string[7];
            string sql = @"select WO,PARTSDRAWING_CODE,PLAN_QUANTITY,QUANTITY,WORKER,WORKER_NAME from work_order where machine_name like '%" + machinename + "%' and status='1' order by UPDATED_DATE desc"; 
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for(int i=0;i<1;i++)
                {
                    if(string.IsNullOrEmpty(res[0]))
                    {
                        res[0] = ds.Tables[0].Rows[i]["WO"].ToString();
                    }
                    else
                    {
                        res[0] += "," + ds.Tables[0].Rows[i]["WO"].ToString();
                    }

                    if (string.IsNullOrEmpty(res[1]))
                    {
                        res[1] = ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"].ToString();
                    }
                    else
                    {
                        res[1] += "," + ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"].ToString();
                    }

                    if (string.IsNullOrEmpty(res[2]))
                    {
                        res[2] = ds.Tables[0].Rows[i]["PLAN_QUANTITY"].ToString();
                    }
                    else
                    {
                        res[2] += "," + ds.Tables[0].Rows[i]["PLAN_QUANTITY"].ToString();
                    }

                    if (string.IsNullOrEmpty(res[3]))
                    {
                        res[3] = ds.Tables[0].Rows[i]["QUANTITY"].ToString();
                    }
                    else
                    {
                        res[3] += "," + ds.Tables[0].Rows[i]["QUANTITY"].ToString();
                    }

                    if (string.IsNullOrEmpty(res[4]))
                    {
                        res[4] = ds.Tables[0].Rows[i]["WORKER"].ToString();
                    }
                    else
                    {
                        res[4] += "," + ds.Tables[0].Rows[i]["WORKER"].ToString();
                    }

                    if (string.IsNullOrEmpty(res[5]))
                    {
                        res[5] = ds.Tables[0].Rows[i]["WORKER_NAME"].ToString();
                    }
                    else
                    {
                        res[5] += "," + ds.Tables[0].Rows[i]["WORKER_NAME"].ToString();
                    }
                    
                                      
                }               
            }
            sql = "select count(0) from realtime_statistics where station_name='CHEXI' and machine_name='" + machinename + "'";// and created_date>TO_DATE(TO_CHAR(sysdate,'YYYY/MM/DD'),'YYYY/MM/DD')";
            DataSet dstemp = DBContext.ExcuteSql(sql).ToDataSet();
            res[6] = dstemp.Tables[0].Rows[0][0].ToString();
            return res;

        }

        public int[] FindYield(string iMonth)
        {


            IList<RealtimeStatistics> uplist = null;
            IList<RealtimeStatistics> uplistF = null;
            int[] res = new int[5];//0:良品数量；1:不良数量；2：返工数量；3：让步数量：4：废品数量
            string sql = "";
            string sqlF = "";

            sql = "select distinct(psn) from mes_master.realtime_statistics where to_char(created_date,'YYYY-MM') ='" + iMonth + "' ";
            sqlF = "select distinct(psn) from mes_master.realtime_statistics where status='F' and to_char(created_date,'YYYY-MM') ='" + iMonth + "'";

            uplist = DBContext.ExcuteSql(sql).ToBusiObjects<RealtimeStatistics>();
            uplistF = DBContext.ExcuteSql(sqlF).ToBusiObjects<RealtimeStatistics>();


            res[0] = (uplist == null ? 0 : uplist.Count) - (uplistF == null ? 0 : uplistF.Count);
            res[1] = (uplistF == null ? 0 : uplistF.Count);
            IList<UnsurenessHistory> uhlist1 = null;
            IList<UnsurenessHistory> uhlist2 = null;
            IList<UnsurenessHistory> uhlist3 = null;
            if (uplistF != null && uplistF.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (RealtimeStatistics ff in uplistF)
                {
                    list.Add("'" + ff.PSN + "'");
                }
                string inSql = string.Join(",", list);
                string sql1 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='1'";
                uhlist1 = DBContext.ExcuteSql(sql1).ToBusiObjects<UnsurenessHistory>();
                string sql2 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='2'";
                uhlist2 = DBContext.ExcuteSql(sql2).ToBusiObjects<UnsurenessHistory>();
                string sql3 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='3'";
                uhlist3 = DBContext.ExcuteSql(sql3).ToBusiObjects<UnsurenessHistory>();
            }
            res[2] = (uhlist1 == null ? 0 : uhlist1.Count);
            res[3] = (uhlist2 == null ? 0 : uhlist2.Count);
            res[4] = (uhlist3 == null ? 0 : uhlist3.Count);

            return res;
        }




        public DataSet FindCustYield(string type, string month)
        {
            DataSet ds = null;
            if (type == "passed")
            {
                string sql = "select * from mes_master.realtime_statistics where status='P' and to_char(created_date,'YYYY-MM') ='" + month + "'";

                IList<RealtimeStatistics> uplist = DBContext.ExcuteSql(sql).ToBusiObjects<RealtimeStatistics>();

                int passCount = uplist == null ? 0 : uplist.Count;

                string sqlDe = "select cust_name, count(*) count from mes_master.realtime_statistics where status='P' and to_char(created_date,'YYYY-MM') ='" + month + "' group by cust_name";
                ds = DBContext.ExcuteSql(sqlDe).ToDataSet();
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["COUNT"] = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[i]["COUNT"]) / passCount), 2) * 100;
                    }
                }
            }
            else
            {

                string sqlF = "select psn from mes_master.realtime_statistics where status='F'";
                IList<RealtimeStatistics> uplist = DBContext.ExcuteSql(sqlF).ToBusiObjects<RealtimeStatistics>();

                int failCount = uplist == null ? 0 : uplist.Count;

                if (type == "returned")
                {
                    string sqlDe = "select cust_name, count(*) count from mes_master.realtime_statistics where status='F' and to_char(created_date,'YYYY-MM') ='" + month + "'  and psn in (select psn from mes_master.unsureness_history where status='1') group by cust_name";
                    ds = DBContext.ExcuteSql(sqlDe).ToDataSet();
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["COUNT"] = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[i]["COUNT"]) / failCount), 2) * 100;
                        }
                    }
                }
                else if (type == "secpass")
                {
                    string sqlDe = "select cust_name, count(*) count from mes_master.realtime_statistics where status='F' and to_char(created_date,'YYYY-MM') ='" + month + "'  and psn in (select psn from mes_master.unsureness_history where status='2') group by cust_name";
                    ds = DBContext.ExcuteSql(sqlDe).ToDataSet();
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["COUNT"] = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[i]["COUNT"]) / failCount), 2) * 100;
                        }
                    }
                }
                else if (type == "discard")
                {
                    string sqlDe = "select cust_name, count(*) count from mes_master.realtime_statistics where status='F' and to_char(created_date,'YYYY-MM') ='" + month + "'  and psn in (select psn from mes_master.unsureness_history where status='3') group by cust_name";
                    ds = DBContext.ExcuteSql(sqlDe).ToDataSet();
                    if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["COUNT"] = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[i]["COUNT"]) / failCount), 2) * 100;
                        }
                    }
                }


            }

            return ds;
        }

        public int[] FindMachineYield(string machineName, string month)
        {


            IList<RealtimeStatistics> uplist = null;
            IList<RealtimeStatistics> uplistF = null;
            int[] res = new int[5];//0:良品数量；1:不良数量；2：返工数量；3：让步数量：4：废品数量
            if (!string.IsNullOrEmpty(machineName))
            {
                string sql = "select distinct(psn) from mes_master.realtime_statistics where machine_name='" + machineName + "' and to_char(created_date,'YYYY-MM') ='" + month + "'";
                string sqlF = "select distinct(psn) from mes_master.realtime_statistics where machine_name='" + machineName + "' and status='F' and to_char(created_date,'YYYY-MM') ='" + month + "'";
                uplist = DBContext.ExcuteSql(sql).ToBusiObjects<RealtimeStatistics>();
                uplistF = DBContext.ExcuteSql(sqlF).ToBusiObjects<RealtimeStatistics>();
            }



            res[0] = (uplist == null ? 0 : uplist.Count) - (uplistF == null ? 0 : uplistF.Count);
            res[1] = (uplistF == null ? 0 : uplistF.Count);
            IList<UnsurenessHistory> uhlist1 = null;
            IList<UnsurenessHistory> uhlist2 = null;
            IList<UnsurenessHistory> uhlist3 = null;
            if (uplistF != null && uplistF.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (RealtimeStatistics ff in uplistF)
                {
                    list.Add("'" + ff.PSN + "'");
                }
                string inSql = string.Join(",", list);
                string sql1 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='1'";
                uhlist1 = DBContext.ExcuteSql(sql1).ToBusiObjects<UnsurenessHistory>();
                string sql2 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='2'";
                uhlist2 = DBContext.ExcuteSql(sql2).ToBusiObjects<UnsurenessHistory>();
                string sql3 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='3'";
                uhlist3 = DBContext.ExcuteSql(sql3).ToBusiObjects<UnsurenessHistory>();
            }
            res[2] = (uhlist1 == null ? 0 : uhlist1.Count);
            res[3] = (uhlist2 == null ? 0 : uhlist2.Count);
            res[4] = (uhlist3 == null ? 0 : uhlist3.Count);

            return res;
        }


        public IList<RealtimeStatistics> findMachineCode()
        {
            try
            {
                string sql = "SELECT MACHINE_NAME FROM MES_MASTER.REALTIME_STATISTICS GROUP BY MACHINE_NAME";
                return DBContext.ExcuteSql(sql).ToBusiObjects<RealtimeStatistics>();
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "findMachineCode");
                throw ex;
            }
        }


        public IList<BasCustom> FindCustName()
        {
            return DBContext.LoadArray<BasCustom>();

        }

        public int[] FindCustQty(string month, string custName)
        {


            IList<RealtimeStatistics> uplist = null;
            IList<RealtimeStatistics> uplistF = null;
            int[] res = new int[5];//0:良品数量；1:不良数量；2：返工数量；3：让步数量：4：废品数量
            string sql = "";
            string sqlF = "";

            sql = "select distinct(psn) from mes_master.realtime_statistics where to_char(created_date,'YYYY-MM') ='" + month + "' and cust_name='" + custName + "' ";
            sqlF = "select distinct(psn) from mes_master.realtime_statistics where status='F' and to_char(created_date,'YYYY-MM') ='" + month + "'  and cust_name='" + custName + "'";

            uplist = DBContext.ExcuteSql(sql).ToBusiObjects<RealtimeStatistics>();
            uplistF = DBContext.ExcuteSql(sqlF).ToBusiObjects<RealtimeStatistics>();


            res[0] = (uplist == null ? 0 : uplist.Count) - (uplistF == null ? 0 : uplistF.Count);
            res[1] = (uplistF == null ? 0 : uplistF.Count);
            IList<UnsurenessHistory> uhlist1 = null;
            IList<UnsurenessHistory> uhlist2 = null;
            IList<UnsurenessHistory> uhlist3 = null;
            if (uplistF != null && uplistF.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (RealtimeStatistics ff in uplistF)
                {
                    list.Add("'" + ff.PSN + "'");
                }
                string inSql = string.Join(",", list);
                string sql1 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='1'";
                uhlist1 = DBContext.ExcuteSql(sql1).ToBusiObjects<UnsurenessHistory>();
                string sql2 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='2'";
                uhlist2 = DBContext.ExcuteSql(sql2).ToBusiObjects<UnsurenessHistory>();
                string sql3 = "select * from mes_master.unsureness_history where psn in (" + inSql + ") and status='3'";
                uhlist3 = DBContext.ExcuteSql(sql3).ToBusiObjects<UnsurenessHistory>();
            }
            res[2] = (uhlist1 == null ? 0 : uhlist1.Count);
            res[3] = (uhlist2 == null ? 0 : uhlist2.Count);
            res[4] = (uhlist3 == null ? 0 : uhlist3.Count);

            return res;
        }
        public IList<FailItems> FindFailType()
        {
            string sqlType = "SELECT FAIL_TYPE FROM MES_MASTER.FAIL_ITEMS GROUP BY FAIL_TYPE ";
            return DBContext.ExcuteSql(sqlType).ToBusiObjects<FailItems>();

        }

        public DataSet FindUPByFail(string failType)
        {
            string sqlDetail = "SELECT COUNT(*) COUNT ,TRUNC(CREATED_DATE, 'DD') CREATED_DATE  FROM MES_MASTER.UNSURENESS_PRODUCT WHERE     ";
            sqlDetail = sqlDetail + "FAIL_CODE IN (SELECT FAIL_CODE FROM MES_MASTER.FAIL_ITEMS WHERE FAIL_TYPE='" + failType + "' )         ";
            sqlDetail = sqlDetail + "GROUP BY TRUNC(CREATED_DATE, 'DD') ORDER BY  TRUNC(CREATED_DATE, 'DD')                     ";
            return DBContext.ExcuteSql(sqlDetail).ToDataSet();

        }

        public void RemovePartsDrawingInfo(string id)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                PartsdrawingCode pdc = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.ID == id.Trim());
                if(pdc!=null)
                {
                    pdc.ACTIVE = "0";
                    DBContext.SaveAndUpdate<PartsdrawingCode>(pdc);
                }
                //DBContext.Remove<PartsdrawingCode>(trans, BasMateriel.Meta.CPARTNO == cpartno & BasMateriel.Meta.QPARTNO == qpartno);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemovePartsDrawingInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public void SaveBanCi(BasBanci bbanci)
        {
            try
            {
                //计算时长
                TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(bbanci.StartTime).Ticks); //获取当前时间的刻度数
                TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(bbanci.EndTime).Ticks);
                TimeSpan tstemp = ts2.Subtract(ts1);
                float fduration = tstemp.Hours + (float)(tstemp.Minutes) / 60;
                if(fduration>0)
                {
                    TimeSpan ts = ts2.Subtract(ts1).Duration();//时间差的绝对值 
                    bbanci.DURATION = (decimal)(ts.Hours + (float)(ts.Minutes) / 60);
                }
                else
                {
                    TimeSpan tsmiddletime1 = new TimeSpan(Convert.ToDateTime("23:59:59").Ticks);
                    TimeSpan tsmiddletime2 = new TimeSpan(Convert.ToDateTime("00:00:00").Ticks);
                    TimeSpan tssub1 = tsmiddletime1.Subtract(ts1).Duration();//时间差的绝对值 
                    TimeSpan tssub2 = ts2.Subtract(tsmiddletime2).Duration();//时间差的绝对值 
                    bbanci.DURATION = (decimal)(tssub1.Hours + (float)(tssub1.Minutes) / 60 + (float)(tssub1.Seconds) / 3600 + tssub2.Hours + (float)(tssub2.Minutes) / 60 + (float)(tssub2.Seconds) / 3600);
                }
                //计算休息时长
                // String spanTime = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒";
                TimeSpan ts3 = new TimeSpan(Convert.ToDateTime(bbanci.StartResttime).Ticks); //获取当前时间的刻度数
                TimeSpan ts4 = new TimeSpan(Convert.ToDateTime(bbanci.EndResttime).Ticks);
                tstemp = ts4.Subtract(ts3);
                fduration = tstemp.Hours + (float)(tstemp.Minutes) / 60;
                if (fduration < 0)
                {
                    TimeSpan tsmiddletime1 = new TimeSpan(Convert.ToDateTime("23:59:59").Ticks);
                    TimeSpan tsmiddletime2 = new TimeSpan(Convert.ToDateTime("00:00:00").Ticks);
                    TimeSpan tssub1 = tsmiddletime1.Subtract(ts3).Duration();//时间差的绝对值 
                    TimeSpan tssub2 = ts4.Subtract(tsmiddletime2).Duration();//时间差的绝对值 
                    bbanci.DurationRest = (decimal)(tssub1.Hours + (float)(tssub1.Minutes) / 60 + (float)(tssub1.Seconds) / 3600 + tssub2.Hours + (float)(tssub2.Minutes) / 60 + (float)(tssub2.Seconds) / 3600);                    
                }
                else
                {
                    TimeSpan tsrest = ts4.Subtract(ts3).Duration();//时间差的绝对值 
                    bbanci.DurationRest = (decimal)(tsrest.Hours + (float)(tsrest.Minutes) / 60);
                }
                  
                if (!DBContext.Exist<BasBanci>(BasBanci.Meta.ID == bbanci.ID))
                {
                    bbanci.CreatedDate = DateTime.Now;
                    bbanci.UpdatedBy = this.UserCode;
                    bbanci.ACTIVE = "1";
                    bbanci.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                }
                else
                {
                    bbanci.UpdatedDate = DateTime.Now;
                    bbanci.UpdatedBy = this.UserCode;
                }
                DBContext.SaveAndUpdate<BasBanci>(bbanci);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveBanCi");
                throw ex;
            }
        }
        public void RemoveBanCi(string ID)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                BasBanci bbc = DBContext.Find<BasBanci>(BasBanci.Meta.ID == ID);
                bbc.ACTIVE = "0";
                DBContext.SaveAndUpdate<BasBanci>(trans, bbc);
               // DBContext.Remove<BasBanci>(trans, BasBanci.Meta.ID == ID);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveBanCi");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public IList<WorkOrder> FindWONew(string workorder, string status
            , string partNo, string cust
            , string worker,string orderno)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
                ce = (WorkOrder.Meta.WO.Like(workorder));
            }
            if (!string.IsNullOrEmpty(status) && status != "4")
            {
                ce = (ce & WorkOrder.Meta.STATUS == status);
            }
            if (!string.IsNullOrEmpty(partNo))
            {
                ce = (ce & WorkOrder.Meta.PartsdrawingCode.Like(partNo));
            }
            if (!string.IsNullOrEmpty(cust))
            {
                ce = (ce & WorkOrder.Meta.CustName == cust);
            }
            if (!string.IsNullOrEmpty(worker))
            {
                ce = (ce & WorkOrder.Meta.WorkerName.Like(worker));
            }
            if (!string.IsNullOrEmpty(orderno))
            {
                ce = (ce & WorkOrder.Meta.OrderNumber.Like(orderno));
            }

            if (ce == null)
            {
                ce = WorkOrder.Meta.CreatedDate > DateTime.Now.AddDays(-30) & WorkOrder.Meta.STATUS != "5";
                return DBContext.FindArray<WorkOrder>(ce, WorkOrder.Meta.CreatedDate.DESC);//5为无效工单
            }
            //if (ce==null)
            //{
            //    return DBContext.LoadArray<WorkOrder>();
            //}
            return DBContext.FindArray<WorkOrder>(ce,WorkOrder.Meta.CreatedDate.DESC);
        }
        public TextValueInfo[] ListBindBanCi()
        {
            string sql = string.Empty;            
            sql = @"SELECT distinct(name)  FROM MES_MASTER.BAS_BANCI WHERE  ACTIVE='1'  ";
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            TextValueInfo[] vts = new TextValueInfo[ds.Tables[0].Rows.Count];
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = ds.Tables[0].Rows[i][0].ToString();
                vts[i].Text = ds.Tables[0].Rows[i][0].ToString();
            }
            return vts;
        }

       
         
        public string GetUnitTimebyPartsDrawingCode(string PartsDrawingCode)
        {
            string res = "0";
            PartsdrawingCode pc = DBContext.Find<PartsdrawingCode>(PartsdrawingCode.Meta.PartsCode == PartsDrawingCode&PartsdrawingCode.Meta.ACTIVE=="1");
            if (pc != null)
            {
                res = pc.UnitTime.ToString();
            }
            return res;
        }

        /// <summary>
        /// 根据生产开始时间计算生产结束时间
        /// </summary>
        /// <param name="taskTime">工时</param>
        /// <param name="shift">班次名称</param>
        /// <param name="qty">数量</param>
        /// <param name="startTime">开始排班时间</param>
        /// <returns>生产结束时间</returns>
         //计算计划生产结束时间
        
        public string GetPlanEndTime(string startTime, string shift, string taskTime, string qty)
        {
            //判断信息是否完整
            if (string.IsNullOrEmpty(taskTime) || string.IsNullOrEmpty(shift) || string.IsNullOrEmpty(qty) || string.IsNullOrEmpty(startTime))
            {
                throw new Exception("信息不完整");
            }

            double dTackTime = 0;
            int iQty = 0;
            DateTime dStart;
            try
            {
                dTackTime = Convert.ToDouble(taskTime);
                iQty = Convert.ToInt32(qty);
                dStart = Convert.ToDateTime(startTime);

            }
            catch
            {
                throw new Exception("数据格式有误");
            }
            if (dStart < DateTime.Now)
            {
                throw new Exception("开始排班时间不能小于当前时间");
            }
            //多个班次并行
            string[] shifts = shift.Split(',');
            for (int i = 0; i < shifts.Length; i++)
            {
                shifts[i] = "'" + shifts[i] + "'";
            }
            shift = string.Join(",", shifts);
            string sql = "select * from mes_master.bas_banci where name in (" + shift + ") and active='1'";

            //根据班次名称查找所有班次信息
            IList<BasBanci> shiftInfo = DBContext.ExcuteSql(sql).ToBusiObjects<BasBanci>();
            if (shiftInfo == null || shiftInfo.Count == 0)
            {
                throw new Exception("班次信息不存在");
            }
            DateTime StartTime = default(DateTime);
            DateTime StartResttime = default(DateTime);
            DateTime EndTime = default(DateTime);
            DateTime EndResttime = default(DateTime);
            BasBanci banciBe = new BasBanci();
            for (int i = 0; i < shiftInfo.Count; i++)
            {
                if (string.IsNullOrEmpty(shiftInfo[i].StartTime) || string.IsNullOrEmpty(shiftInfo[i].StartResttime)
                || string.IsNullOrEmpty(shiftInfo[i].EndTime) || string.IsNullOrEmpty(shiftInfo[i].EndResttime))
                {
                    throw new Exception("班次信息不完整");
                }

                try
                {
                    StartTime = DateTime.ParseExact(shiftInfo[i].StartTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                    StartResttime = DateTime.ParseExact(shiftInfo[i].StartResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                    EndTime = DateTime.ParseExact(shiftInfo[i].EndTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                    EndResttime = DateTime.ParseExact(shiftInfo[i].EndResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);

                }
                catch
                {
                    throw new Exception("班次信息格式不正确");
                }
                //跨天的情况，拆分成0:00之前和0:00之后两部分
                if (EndTime < StartTime)
                {
                    double BeforeHour = ((Convert.ToDateTime(StartTime.AddDays(1).ToShortDateString().ToString())) - StartTime).TotalHours;
                    //24点前半场班次信息
                    banciBe.NAME = shiftInfo[i].NAME;
                    banciBe.StartTime = shiftInfo[i].StartTime;
                    banciBe.EndTime = "23:59";
                    banciBe.DURATION = Convert.ToDecimal(BeforeHour);

                    //休息之前都在前半场
                    if (StartResttime < Convert.ToDateTime(StartTime.AddDays(1).ToShortDateString().ToString()) && StartResttime > StartTime && EndResttime <= Convert.ToDateTime(StartTime.AddDays(1).ToShortDateString().ToString()))
                    {
                        banciBe.StartResttime = shiftInfo[i].StartResttime;
                        banciBe.EndResttime = shiftInfo[i].EndResttime;
                        banciBe.DurationRest = shiftInfo[i].DurationRest;
                    }
                    //休息时间都在后半场
                    else if (StartResttime >= Convert.ToDateTime(StartTime.ToShortDateString().ToString()) && EndResttime >= Convert.ToDateTime(StartTime.ToShortDateString().ToString()))
                    {
                        banciBe.StartResttime = "23:59";
                        banciBe.EndResttime = "23:59";
                        banciBe.DurationRest = 0;
                    }
                    //休息时间前半场开始后半场结束
                    else
                    {
                        banciBe.StartResttime = shiftInfo[i].StartResttime;
                        banciBe.EndResttime = "23:59";
                        banciBe.DurationRest = Convert.ToDecimal(((Convert.ToDateTime(StartTime.AddDays(1).ToShortDateString().ToString())) - StartResttime).TotalHours);

                    }

                    //0点后半场班次信息
                    shiftInfo[i].StartTime = "00:00";
                    shiftInfo[i].DURATION = Convert.ToDecimal((EndTime - (Convert.ToDateTime(StartTime.ToShortDateString().ToString()))).TotalHours);

                    //休息时间在前半场
                    if (StartResttime < Convert.ToDateTime(StartTime.AddDays(1).ToShortDateString().ToString()) && StartResttime > StartTime && EndResttime <= Convert.ToDateTime(StartTime.AddDays(1).ToShortDateString().ToString()))
                    {
                        shiftInfo[i].StartResttime = "00:00";
                        shiftInfo[i].EndResttime = "00:00";
                        shiftInfo[i].DurationRest = 0;
                    }
                    //休息时间在后半场
                    else if (StartResttime >= Convert.ToDateTime(StartTime.ToShortDateString().ToString()) && EndResttime >= Convert.ToDateTime(StartTime.ToShortDateString().ToString()))
                    {
                        //不作修改
                    }
                    else
                    {
                        shiftInfo[i].StartResttime = "00:00";
                        //shiftInfo[i].EndResttime = "24:00";
                        shiftInfo[i].DurationRest = Convert.ToDecimal((EndResttime - (Convert.ToDateTime(StartTime.ToShortDateString().ToString()))).TotalHours);

                    }
                }

            }
            shiftInfo.Add(banciBe);
            //将切割后的班次按时间进行排序
            var sortedList = shiftInfo.OrderBy(a => a.StartTime).ThenBy(b => b.StartTime);
            shiftInfo = sortedList.ToList(); //这个时候会排序

            double TotalHour = dTackTime * iQty;
            double DaysTotal = 0;
            double FirstHour = 0;
            int j = 0;
            for (int i = 0; i < shiftInfo.Count; i++)
            {
                try
                {
                    //by tony add 2017-8-8
                    if(string.IsNullOrEmpty(shiftInfo[i].ACTIVE))
                    {
                        continue;
                    }
                    StartTime = DateTime.ParseExact(shiftInfo[i].StartTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                    StartResttime = DateTime.ParseExact(shiftInfo[i].StartResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                    EndTime = DateTime.ParseExact(shiftInfo[i].EndTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                    EndResttime = DateTime.ParseExact(shiftInfo[i].EndResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                    if (shiftInfo[i].StartTime == "23:59")
                    {
                        StartTime = StartTime.AddMinutes(1);
                    }
                    if (shiftInfo[i].StartResttime == "23:59")
                    {
                        StartResttime = StartResttime.AddMinutes(1);
                    }
                    if (shiftInfo[i].EndResttime == "23:59")
                    {
                        EndResttime = EndResttime.AddMinutes(1);
                    }
                    if (shiftInfo[i].EndTime == "23:59")
                    {
                        EndTime = EndTime.AddMinutes(1);
                    }

                }
                catch
                {
                    throw new Exception("班次信息格式不正确");
                }
                //排班日期距当前日期天数
                int start2Today = Convert.ToInt32(Math.Floor((Convert.ToDateTime(dStart.ToShortDateString().ToString()) -
                    (Convert.ToDateTime(DateTime.Now.ToShortDateString().ToString()))).TotalDays));
                if (start2Today > 0)
                {

                    StartTime = StartTime.AddDays(start2Today);
                    StartResttime = StartResttime.AddDays(start2Today);
                    EndTime = EndTime.AddDays(start2Today);
                    EndResttime = EndResttime.AddDays(start2Today);
                }
                string week = dStart.DayOfWeek.ToString();
                //判断开始排班时间是否为周末或节假日

                if (week == "Saturday" || week == "Sunday")
                {
                    throw new Exception("开始排班时间不能为休息日");
                }
                // DateTime startHour = DateTime.ParseExact(dStart.GetDateTimeFormats('t')[0].ToString(), "HH:mm", System.Globalization.CultureInfo.CurrentCulture);//排班开始时间
                if (dStart >= StartResttime && dStart < EndResttime)
                {
                    throw new Exception("开始排班时间不能为休息时间");
                }
                //累计计算一天的所有工时数
                DaysTotal = DaysTotal + Convert.ToDouble(shiftInfo[i].DURATION - shiftInfo[i].DurationRest);
                //判断开始时间是否在当前班次的时间范围内
                if (dStart < StartTime || dStart >= EndTime)
                {
                    continue;
                }
                j++;
                //找到开始时间对应的班次信息
                if (j == 1)
                {
                    //上半场开始
                    if (dStart < StartResttime)
                    {
                        FirstHour = (StartResttime - dStart + (EndTime - EndResttime)).TotalHours;
                    }
                    //下半场开始
                    else if (dStart >= EndResttime)
                    {
                        FirstHour = (EndTime - dStart).TotalHours;
                    }
                    //休息时间开始
                    else
                    {
                        throw new Exception("开始时间不能是休息时间");
                    }
                }
                //开始时间对应班次之后的所有班次
                else
                {
                    //累计计算第一天的所有工时数
                    FirstHour = FirstHour + Convert.ToDouble(shiftInfo[0].DURATION - shiftInfo[0].DurationRest);
                    //第一天可以结束工作的处理
                    if (FirstHour > TotalHour)
                    {
                        double diffH = FirstHour - TotalHour;
                        if (diffH < (EndTime - EndResttime).TotalHours)
                        {
                            return EndTime.AddHours(-diffH).ToString();
                        }
                        else
                        {
                            return StartResttime.AddHours(-(diffH - (EndTime - EndResttime).TotalHours)).ToString();
                        }
                    }
                    else if (FirstHour == TotalHour)
                    {
                        return EndTime.ToString() ;
                    }
                }
            }
            //需要的完整天数
            double days = (TotalHour - FirstHour) / DaysTotal;

            if (days > 1)
            {
                dStart = Convert.ToDateTime(dStart.ToShortDateString().ToString());
            }
            DateTime diffDays = dateDiff(dStart, Convert.ToInt32(Math.Floor(days))).AddDays(1);
            //最后一天需要的小时数
            double lastTime = (days - (Convert.ToInt32(Math.Floor(days)))) * DaysTotal;

            if (lastTime == 0)
            {
                //return Convert.ToDateTime(diffDays.AddDays(-1).ToShortDateString().ToString() + " " + EndTime.ToLongTimeString().ToString());
                return diffDays.AddDays(-1).ToShortDateString().ToString() + " " + EndTime.ToLongTimeString().ToString();
            }
            else
            {
                //最后一个完整天数刚好是周五的情况
                if (diffDays.DayOfWeek.ToString() == "Saturday")
                {
                    diffDays = diffDays.AddDays(2);
                }

                double last = 0;
                int i = 0;
                for (; i < shiftInfo.Count; i++)
                {

                    try
                    {
                        //by tony add 2017-8-8
                        if (string.IsNullOrEmpty(shiftInfo[i].ACTIVE))
                        {
                            continue;
                        }
                        StartTime = DateTime.ParseExact(shiftInfo[i].StartTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                        StartResttime = DateTime.ParseExact(shiftInfo[i].StartResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                        EndTime = DateTime.ParseExact(shiftInfo[i].EndTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                        EndResttime = DateTime.ParseExact(shiftInfo[i].EndResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                        if (shiftInfo[i].StartTime == "23:59")
                        {
                            StartTime = StartTime.AddMinutes(1);
                        }
                        if (shiftInfo[i].StartResttime == "23:59")
                        {
                            StartResttime = StartResttime.AddMinutes(1);
                        }
                        if (shiftInfo[i].EndResttime == "23:59")
                        {
                            EndResttime = EndResttime.AddMinutes(1);
                        }
                        if (shiftInfo[i].EndTime == "23:59")
                        {
                            EndTime = EndTime.AddMinutes(1);
                        }
                    }
                    catch
                    {
                        throw new Exception("班次信息格式不正确");
                    }
                    //排班日期距当前日期天数
                    int start2Today = Convert.ToInt32(Math.Floor((Convert.ToDateTime(dStart.ToShortDateString().ToString()) -
                        (Convert.ToDateTime(DateTime.Now.ToShortDateString().ToString()))).TotalDays));
                    if (start2Today > 0)
                    {
                        StartTime = StartTime.AddDays(start2Today);
                        StartResttime = StartResttime.AddDays(start2Today);
                        EndTime = EndTime.AddDays(start2Today);
                        EndResttime = EndResttime.AddDays(start2Today);
                    }
                    //找到最后结束时间所在班次
                    if (last + Convert.ToDouble(shiftInfo[i].DURATION - shiftInfo[i].DurationRest) >= lastTime)
                    {
                        //本班次所需的小时数
                        double BanCiHour = lastTime - last;
                        //上半场可以完成
                        if (BanCiHour <= (StartResttime - StartTime).TotalHours)
                        {
                            //return Convert.ToDateTime(diffDays.ToShortDateString().ToString() + " " + StartTime.AddHours(BanCiHour).ToLongTimeString().ToString());
                            return diffDays.ToShortDateString().ToString() + " " + StartTime.AddHours(BanCiHour).ToLongTimeString().ToString();
                        }
                        else
                        {
                            //return Convert.ToDateTime(diffDays.ToShortDateString().ToString() + " " + EndResttime.AddHours(BanCiHour - (StartResttime - StartTime).TotalHours).ToLongTimeString().ToString());
                            return diffDays.ToShortDateString().ToString() + " " + EndResttime.AddHours(BanCiHour - (StartResttime - StartTime).TotalHours).ToLongTimeString().ToString();
                        }

                    }
                    last = last + Convert.ToDouble(shiftInfo[i].DURATION - shiftInfo[i].DurationRest);
                }
                throw new Exception("班次时间和时长维护有误");

            }
            //DateTime resdt;
            ////判断信息是否完整
            //if (string.IsNullOrEmpty(taskTime) || string.IsNullOrEmpty(shift) || string.IsNullOrEmpty(qty) || string.IsNullOrEmpty(startTime))
            //{
            //    throw new Exception("信息不完整");
            //}

            //double dTackTime = 0;
            //int iQty = 0;
            //DateTime dStart;
            //try
            //{
            //    dTackTime = Convert.ToDouble(taskTime);
            //    iQty = Convert.ToInt32(qty);
            //    dStart = Convert.ToDateTime(startTime);

            //}
            //catch
            //{
            //    throw new Exception("数据格式有误");
            //}
            //if (dStart < DateTime.Now)
            //{
            //    throw new Exception("开始排班时间不能小于当前时间");
            //}
            ////根据班次名称查找班次信息
            //IList<BasBanci> shiftInfo = DBContext.FindArray<BasBanci>(BasBanci.Meta.NAME == shift);
            //if (shiftInfo == null || string.IsNullOrEmpty(shiftInfo[0].StartTime) || string.IsNullOrEmpty(shiftInfo[0].StartResttime)
            //    || string.IsNullOrEmpty(shiftInfo[0].EndTime) || string.IsNullOrEmpty(shiftInfo[0].EndResttime))
            //{
            //    throw new Exception("班次信息不存在或信息不完整");
            //}
            //DateTime StartTime;
            //DateTime StartResttime;
            //DateTime EndTime;
            //DateTime EndResttime;

            //try
            //{
            //    StartTime = DateTime.ParseExact(shiftInfo[0].StartTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
            //    StartResttime = DateTime.ParseExact(shiftInfo[0].StartResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
            //    EndTime = DateTime.ParseExact(shiftInfo[0].EndTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
            //    EndResttime = DateTime.ParseExact(shiftInfo[0].EndResttime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);

            //}
            //catch
            //{
            //    throw new Exception("班次信息格式不正确");
            //}
            ////排班日期距当前日期天数
            //int start2Today = Convert.ToInt32(Math.Floor((Convert.ToDateTime(dStart.ToShortDateString().ToString()) -
            //    (Convert.ToDateTime(DateTime.Now.ToShortDateString().ToString()))).TotalDays));
            //if (start2Today > 0)
            //{
            //    StartTime = StartTime.AddDays(start2Today);
            //    StartResttime = StartResttime.AddDays(start2Today);
            //    EndTime = EndTime.AddDays(start2Today);
            //    EndResttime = EndResttime.AddDays(start2Today);
            //}
            //string week = dStart.DayOfWeek.ToString();
            ////判断开始排班时间是否为周末或节假日

            //if (week == "Saturday" || week == "Sunday")
            //{
            //    throw new Exception("开始排班时间不能为休息日");
            //}

            //// DateTime startHour = DateTime.ParseExact(dStart.GetDateTimeFormats('t')[0].ToString(), "HH:mm", System.Globalization.CultureInfo.CurrentCulture);//排班开始时间
            //if (dStart < StartTime || (dStart >= StartResttime && dStart < EndResttime) || dStart >= EndTime)
            //{
            //    throw new Exception("开始排班时间不能为休息时间");
            //}
            //double TotalHour = dTackTime * iQty;
            ////第一天工时数
            //double diffHour = 0;
            ////时间精度达到分钟
            //if (dStart >= StartTime && dStart < StartResttime)
            //{
            //    diffHour = (StartResttime - dStart).TotalHours + (EndTime - EndResttime).TotalHours;
            //}
            //else
            //{
            //    diffHour = (EndTime - dStart).TotalHours;
            //}
            ////如果当天可完成工作
            //if (TotalHour <= diffHour)
            //{
            //    //在午休前完成工作
            //    if (TotalHour <= (StartResttime - dStart).TotalHours)
            //    {
            //        resdt = dStart.AddHours(TotalHour);
            //        return resdt.ToString();
            //    }
            //    //午休后完成
            //    else
            //    {
            //        resdt = dStart.AddHours(TotalHour + (EndResttime - StartResttime).TotalHours);
            //        return resdt.ToString();
            //    }
            //}
            ////跨班完成工作
            //else
            //{
            //    //每天工作时间
            //    double workHour = Convert.ToDouble(shiftInfo[0].DURATION - shiftInfo[0].DurationRest);
            //    //除当天外需要的天数
            //    double days = (TotalHour - diffHour) / workHour;

            //    if (days > 1)
            //    {
            //        dStart = Convert.ToDateTime(dStart.ToShortDateString().ToString());
            //    }
            //    DateTime diffDays = dateDiff(dStart, Convert.ToInt32(Math.Floor(days))).AddDays(1);
            //    //最后一天需要的小时数
            //    double lastTime = (days - (Convert.ToInt32(Math.Floor(days)))) * workHour;
            //    if (lastTime == 0)
            //    {
            //        resdt = Convert.ToDateTime(diffDays.AddDays(-1).ToShortDateString().ToString() + " " + EndTime.ToLongTimeString().ToString());
            //        return resdt.ToString();
            //    }
            //    else
            //    {
            //        if (diffDays.DayOfWeek.ToString() == "Saturday")
            //        {
            //            diffDays = diffDays.AddDays(2);
            //        }
            //        if (lastTime > (StartResttime - StartTime).TotalHours)
            //        {
            //            resdt = Convert.ToDateTime(diffDays.ToShortDateString().ToString() + " " + StartTime.AddHours(lastTime + (EndResttime - StartResttime).TotalHours).ToLongTimeString().ToString());
            //            return resdt.ToString();
            //        }
            //        else
            //        {
            //            resdt = Convert.ToDateTime(diffDays.ToShortDateString().ToString() + " " + StartTime.AddHours(lastTime).ToLongTimeString().ToString());
            //            return resdt.ToString();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="days">天数</param>
        /// <returns>去掉周末的结束日期</returns>
        public DateTime dateDiff(DateTime startDate, int days)
        {
            for (int i = 0; i < days; i++)
            {
                if (startDate.AddDays(1).DayOfWeek.ToString() == "Saturday")
                {
                    startDate = startDate.AddDays(3);
                }
                else
                {
                    startDate = startDate.AddDays(1);
                }

            }
            return startDate;

        }

        public IList<TrackingWip> FindSNTrackingWIP(string workorder,string partsdrawingno,string station,DateTime starttime, DateTime endtime)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
                //ce = (WorkOrder.Meta.WO == workorder.WO);
                ce = (TrackingWip.Meta.WorkOrder.Like(workorder));
            }
            
            if (!string.IsNullOrEmpty(partsdrawingno))
            {
                //ce = (ce & WorkOrder.Meta.PartsdrawingCode == workorder.PartsdrawingCode);
                ce = (ce & TrackingWip.Meta.PartsdrawingCode.Like(partsdrawingno));
            }
            if (!string.IsNullOrEmpty(station))
            {
                ce = (ce & TrackingWip.Meta.StationName == station.Trim());
            }
            
            if (null != starttime && null != endtime)
            {
                if (DateTime.Compare((DateTime)starttime, (DateTime)endtime) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & TrackingWip.Meta.CreatedDate >= starttime);
                ce = (ce & TrackingWip.Meta.CreatedDate <= endtime);
            }
            if (ce == null)
            {
                ce =  TrackingWip.Meta.CreatedDate >= DateTime.Now.AddDays(-30);                 
            }
            return DBContext.FindArray<TrackingWip>(ce);
        }
        /// <summary>
        /// 交接任务
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string TaskHandover(string UserCode, string ID)
        {
            TrackingTemp tt = DBContext.Find<TrackingTemp>(TrackingTemp.Meta.ID == ID);
            if(tt!=null)
            {
                tt.NextEmp = UserCode;
                DBContext.SaveAndUpdate<TrackingTemp>(tt);
                return "OK";
            }
            else
            {
                return "没有查到交接任务";
            }
        }
        /// <summary>
        /// 加载或查找公司员工
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<SysUserPub> FindPubUsers(string userCode, string userName)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(userCode))
            {
                ce = (SysUserPub.Meta.UserCode == userCode);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                ce = (ce & SysUserPub.Meta.UserName.Like(userName));
            }

            
            if (ce == null)
            {
                return DBContext.LoadArray<SysUserPub>();
            }

            return DBContext.FindArray<SysUserPub>(ce);

        }
        public void SaveUserPub(SysUserPub user)
        {
            user.UserCode = user.UserCode.ToUpper();            
            DBContext.SaveAndUpdate<SysUserPub>(user);
        }

        public void RemoveUserPub(string userCode)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            { 
                DBContext.Remove<SysUserPub>(trans, userCode);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveUserPub");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public void CloseOrderInfo(string orderno)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                OrderDetail od = DBContext.Find<OrderDetail>(OrderDetail.Meta.OrderNo == orderno);
                if (od != null)
                {
                    od.STATUS = "3";
                    od.MEMO = "关闭";
                    od.UpdatedBy = this.UserCode;
                    od.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<OrderDetail>(od);
                     
                }
                 
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "CloseOrderInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }
        //查找新建的图号，以备安排工艺设计
        public IList<TechnologyWip> FindDrawingCodeToTechnology(string drawingno="")
        {
            //string sql = @"select * from mes_master.partsdrawing_code where parts_code not in(select distinct(partsdrawingno) from mes_master.technology_info where active='1')
            //                and id in(Select min(id) FROM  mes_master.partsdrawing_code  group by parts_code) and active='1' order by created_date";

            //string sql = "SELECT * FROM MES_MASTER.SYS_USER WHERE USER_CODE IN (SELECT USER_CODE FROM MES_MASTER.SYS_ROLE_USER WHERE ROLE_ID = '{0}')";
            //sql = string.Format(sql, roleId);
            //return DBContext.ExcuteSql(sql).ToBusiObjects<PartsdrawingCode>();
            ConditionExpress ce = null;
            ce = (TechnologyWip.Meta.STATUS == 0);
            if (!string.IsNullOrEmpty(drawingno))
            { 
                ce = (ce & TechnologyWip.Meta.PARTSDRAWINGNO.Like(drawingno));
            }
            
            return DBContext.FindArray<TechnologyWip>(ce,TechnologyWip.Meta.CreatedDate.DESC);

        }
        //查找进行中的工艺任务
        public IList<TechnologyWip> FindNoFinishTechnology(string drawing="")
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(drawing))
            {
                //状态：0：新建任务；1：分派工艺；2：提交工艺；3：工艺已审核；4：编程已分派；5：编程已提交；6：编程已审核；7：维护工时；8：工艺完成；9：工艺驳回；10：编程驳回
                  sql = @"select * from mes_master.technology_wip where status in('1','2','3','4','5','6','7','9','10') and active='1'";
            }
            else
            {
                sql = @"select * from mes_master.technology_wip where status in('1','2','3','4','5','6','7','9','10') and active='1'
                         and partsdrawingno like '%"+drawing+"%'";
            }
            //string sql = "SELECT * FROM MES_MASTER.SYS_USER WHERE USER_CODE IN (SELECT USER_CODE FROM MES_MASTER.SYS_ROLE_USER WHERE ROLE_ID = '{0}')";
            //sql = string.Format(sql, roleId);
            return DBContext.ExcuteSql(sql).ToBusiObjects<TechnologyWip>();

        }

        //根据角色找员工
        public TextValueInfo[] ListUserByRole(string rolename)
        {
            string sql = @"select * from sys_user where user_code in (select user_code from sys_role_user where role_id = (select ID from sys_role where role_name='{0}') ) and status='1'";
             
            //string sql = "SELECT * FROM MES_MASTER.SYS_USER WHERE USER_CODE IN (SELECT USER_CODE FROM MES_MASTER.SYS_ROLE_USER WHERE ROLE_ID = '{0}')";
            sql = string.Format(sql, rolename);
            IList<SysUser> users = DBContext.ExcuteSql(sql).ToBusiObjects<SysUser>();

            TextValueInfo[] vts = new TextValueInfo[users.Count];
            for (var i = 0; i < users.Count; i++)
            {
                vts[i] = new TextValueInfo();
                vts[i].Value = users[i].UserCode;
                vts[i].Text = users[i].UserName;
            }

            return vts;

        }
        public void SaveTechnologyInfo(TechnologyWip techinfo)
        {
            try
            {
                if(DBContext.Exist<TechnologyWip>(TechnologyWip.Meta.PARTSDRAWINGNO==techinfo.PARTSDRAWINGNO))
                {
                    //TechnologyWip tw = DBContext.Find<TechnologyWip>(TechnologyWip.Meta.PARTSDRAWINGNO== techinfo.PARTSDRAWINGNO));
                    if (techinfo.STATUS != null && techinfo.STATUS.ToString() != "")
                    {
                        techinfo.UpdatedDate = DateTime.Now;
                        techinfo.UpdatedBy = this.UserCode;
                        DBContext.SaveAndUpdate<TechnologyWip>(techinfo);
                    }
                }
                else
                {
                    techinfo.CreatedDate = DateTime.Now;
                    techinfo.UpdatedBy = this.UserCode;
                    techinfo.STATUS = 0;
                    techinfo.StatusMemo = "新建任务";
                    techinfo.ACTIVE = "1"; 
                    DBContext.SaveAndUpdate<TechnologyWip>(techinfo); 
                    
                }
                //存入历史记录
                TechnologyInfo techi = new TechnologyInfo();
                techi.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                techi.PARTSDRAWINGNO = techinfo.PARTSDRAWINGNO;
                techi.PlanDate = techinfo.PlanDate;
                techi.ProcessEngineer = techinfo.ProcessEngineer;
                techi.ProcessFname = techinfo.ProcessFname;
                techi.ProcessName = techinfo.ProcessName;
                techi.ProcessPath = techinfo.ProcessPath;
                techi.ProductCode = techinfo.ProductCode;
                techi.ProductName = techinfo.ProductName;
                techi.ProgramEngineer = techinfo.ProgramEngineer;
                techi.ProgramName = techinfo.ProgramName;
                techi.QZPARTSDRAWING = techinfo.QZPARTSDRAWING;
                techi.RealDate = techinfo.RealDate;
                techi.STATUS = techinfo.STATUS;
                techi.StatusMemo = techinfo.StatusMemo;
                techi.UpdatedBy = this.UserCode;
                techi.CreatedDate = DateTime.Now;
                techi.ACTIVE = "1";
                techi.CustCode = techinfo.CustCode;
                techi.CustName = techinfo.CustName;
                techi.DevplanDate = techinfo.DevplanDate;
                techi.DevrealDate = techinfo.DevrealDate;
                techi.MEMO = techinfo.MEMO;
                DBContext.SaveAndUpdate<TechnologyInfo>(techi);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveTechnologyInfo");
                throw ex;
            }
        }

        public void RemoveTechnologyInfo(string partdrawingno)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                TechnologyWip techinfo = DBContext.Find<TechnologyWip>(TechnologyWip.Meta.PARTSDRAWINGNO == partdrawingno);
                if(techinfo!=null)
                {
                    techinfo.ACTIVE = "0";
                    techinfo.UpdatedBy = this.UserCode;
                    techinfo.UpdatedDate = System.DateTime.Now;
                    DBContext.SaveAndUpdate<TechnologyWip>(techinfo);
                }               
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "RemoveTechnologyInfo");
                throw ex;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public IList<TechnologyWip> FindTechnologyTask(string partsDrawingNo)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(partsDrawingNo))
            {
                ce = (TechnologyWip.Meta.PARTSDRAWINGNO.Like(partsDrawingNo));
            } 
            if (ce == null)
            {
                return DBContext.FindArray<TechnologyWip>(TechnologyWip.Meta.STATUS==0| TechnologyWip.Meta.STATUS==3);//查询新建任务和审核工艺的任务
            }

            return DBContext.FindArray<TechnologyWip>(ce);

        }
        public IList<TechnologyWip> FindTechnology(string partsdrawingno, string status, string engineer, string starttime, string endtime)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(partsdrawingno))
            {
                ce = (TechnologyWip.Meta.PARTSDRAWINGNO.Like(partsdrawingno));
            }
            if (!string.IsNullOrEmpty(status))
            {
                string[] strstatus = status.Split(',');
                if (strstatus.Length > 1)
                {
                    ConditionExpress cetemp = null;
                    for (int i = 0; i < strstatus.Length; i++)
                    {
                        if (i == 0)
                        {
                            cetemp = (TechnologyWip.Meta.STATUS == Convert.ToInt32(strstatus[i].ToString()));
                        }
                        else
                        {
                            cetemp = cetemp | (TechnologyWip.Meta.STATUS == Convert.ToInt32(strstatus[i].ToString()));
                        }
                    }
                    ce = (ce & (cetemp));
                }
                else
                {
                    ce = (ce & TechnologyWip.Meta.STATUS == Convert.ToInt32(status));
                }
            }
            if (!string.IsNullOrEmpty(engineer))
            {
                ce = (ce & (TechnologyWip.Meta.ProcessEngineer == engineer.Trim() | TechnologyWip.Meta.ProgramEngineer == engineer.Trim()));
            }
            ce = (ce & TechnologyWip.Meta.ACTIVE == "1");
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                DateTime dt1 = Convert.ToDateTime(starttime);
                DateTime dt2 = Convert.ToDateTime(endtime);
                if (DateTime.Compare(dt1, dt2) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & TechnologyInfo.Meta.CreatedDate >= dt1);
                ce = (ce & TechnologyInfo.Meta.CreatedDate <= dt2);
            }
            if (ce == null)
            {
                return DBContext.LoadArray<TechnologyWip>();
            }
            return DBContext.FindArray<TechnologyWip>(ce);
        }

        public IList<TechnologyInfo> FindTechnologyHistory(string partsdrawingno,string status, string engineer, string starttime, string endtime)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(partsdrawingno))
            { 
                ce = (TechnologyInfo.Meta.PARTSDRAWINGNO.Like(partsdrawingno));
            }
            if (!string.IsNullOrEmpty(status))
            {
                string[] strstatus = status.Split(',');
                if(strstatus.Length>1)
                {
                    ConditionExpress cetemp = null;
                    for (int i = 0; i < strstatus.Length; i++)
                    {
                        if (i == 0)
                        {
                            cetemp = (TechnologyInfo.Meta.STATUS == Convert.ToInt32(strstatus[i].ToString()));
                        }
                        else
                        {
                            cetemp = cetemp | (TechnologyInfo.Meta.STATUS == Convert.ToInt32(strstatus[i].ToString()));
                        }
                    }
                    ce = (ce & (cetemp));
                }
                else
                {
                        ce = (ce & TechnologyInfo.Meta.STATUS == Convert.ToInt32(status));
                }
            }
            if (!string.IsNullOrEmpty(engineer))
            { 
                ce = (ce &(TechnologyInfo.Meta.ProcessEngineer==engineer.Trim()| TechnologyInfo.Meta.ProgramEngineer==engineer.Trim()));
            }
            ce = (ce & TechnologyInfo.Meta.ACTIVE == "1");
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                DateTime dt1 = Convert.ToDateTime(starttime);
                DateTime dt2 = Convert.ToDateTime(endtime);
                if (DateTime.Compare(dt1, dt2) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & TechnologyInfo.Meta.CreatedDate >= dt1);
                ce = (ce & TechnologyInfo.Meta.CreatedDate <= dt2);
            }
            if (ce == null)
            {
                return DBContext.LoadArray<TechnologyInfo>();
            }
            return DBContext.FindArray<TechnologyInfo>(ce);
        }

        public TechnologyWip FindTechnologyTaskByPD(string partsDrawingNo)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(partsDrawingNo))
            {
                ce = (TechnologyWip.Meta.PARTSDRAWINGNO.Like(partsDrawingNo)& TechnologyWip.Meta.ACTIVE=="1");
                return DBContext.Find<TechnologyWip>(ce);
            }
            return null;
        }
        /// <summary>
        /// 查找路由信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<RouteDetails> FindRouteDetails(string partsdrawingno,string stationid="")
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(partsdrawingno))
            {
                ce = (RouteDetails.Meta.RouteName == partsdrawingno&RouteDetails.Meta.FLAG==1);
            }
            if (!string.IsNullOrEmpty(stationid))
            {
                ce = ce&(RouteDetails.Meta.StationID == stationid);
            }
            if (ce == null)
            {
                return null;// DBContext.FindArray<RouteDetails>(RouteDetails.Meta.FLAG == 9);
            }

            IList<RouteDetails> res= DBContext.FindArray<RouteDetails>(ce, RouteDetails.Meta.OrderId.ASC);
            if(res==null||res.Count<1)
            {
                return DBContext.FindArray<RouteDetails>(RouteDetails.Meta.FLAG == 9);
            }
            return res;

        }

        public string SaveRouteInfo(string partsdrawingno, DataTable routes)
        {
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                IList<RouteDetails> rds = DBContext.FindArray<RouteDetails>(RouteDetails.Meta.RouteName == partsdrawingno);
                if (rds.Count>0)
                {
                    for(int i=0;i<rds.Count;i++)
                    {
                        rds[i].FLAG = 0;
                        //DBContext.SaveAndUpdate<RouteDetails>(rds[i]);
                        DBContext.Remove<RouteDetails>(rds[i]);
                    }
                   //string sql = "delete mes_master.route_details where route_name='" + partsdrawingno + "'";
                   //// string sql = "delete from mes_master.route_details   where route_name='" + partsdrawingno + "'";
                   // DBContext.ExcuteSql(trans, sql);
                    
                }
                int orderid = 1;
                for (int i = 0; i < routes.Rows.Count; i++)
                {
                    RouteDetails rd = new RouteDetails();
                    rd.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    rd.RouteName = partsdrawingno;
                    rd.StationName = routes.Rows[i]["StationName"].ToString();
                    rd.StationId = routes.Rows[i]["StationId"].ToString();
                    rd.MachineType = routes.Rows[i]["MachineType"].ToString();
                    rd.MactypeCode = routes.Rows[i]["MactypeCode"].ToString();
                    rd.FLAG = 1;
                    rd.OrderId = orderid;
                    orderid++;
                    DBContext.SaveAndUpdate<RouteDetails>(rd);
                }
                trans.Commit();
                return "OK";
            }
            catch(Exception ex)
            {
                trans.Rollback();
                return ex.ToString();
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        public string CheckTechnology(string obj,bool b)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return "请选择一个工艺进行审核";
            }
            string[] publishCount = obj.Split(';');
            if (publishCount == null || publishCount.Length == 0)
            {
                return "请选择一个工艺任务进行审核";
            }
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                foreach (string key in publishCount)
                {
                    TechnologyWip techinfo = DBContext.Find<TechnologyWip>(trans, TechnologyWip.Meta.PARTSDRAWINGNO == key);
                    // OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    if(b)
                    {
                        techinfo.STATUS = 3;
                        techinfo.StatusMemo = "工艺已审核";
                    }
                    else
                    {
                        techinfo.STATUS = 9;
                        techinfo.StatusMemo = "工艺驳回";
                    }
                   
                    techinfo.UpdatedBy = this.UserCode;
                    techinfo.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<TechnologyWip>(trans, techinfo);

                    //存入历史记录
                    TechnologyInfo techi = new TechnologyInfo();
                    techi.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    techi.PARTSDRAWINGNO = techinfo.PARTSDRAWINGNO;
                    techi.PlanDate = techinfo.PlanDate;
                    techi.ProcessEngineer = techinfo.ProcessEngineer;
                    techi.ProcessFname = techinfo.ProcessFname;
                    techi.ProcessName = techinfo.ProcessName;
                    techi.ProcessPath = techinfo.ProcessPath;
                    techi.ProductCode = techinfo.ProductCode;
                    techi.ProductName = techinfo.ProductName;
                    techi.ProgramEngineer = techinfo.ProgramEngineer;
                    techi.ProgramName = techinfo.ProgramName;
                    techi.QZPARTSDRAWING = techinfo.QZPARTSDRAWING;
                    techi.RealDate = techinfo.RealDate;
                    techi.STATUS = techinfo.STATUS;
                    techi.StatusMemo = techinfo.StatusMemo;
                    techi.UpdatedBy = this.UserCode;
                    techi.CreatedDate = DateTime.Now;
                    techi.ACTIVE = "1";
                    techi.CustCode = techinfo.CustCode;
                    techi.CustName = techinfo.CustName;
                    techi.DevplanDate = techinfo.DevplanDate;
                    techi.DevrealDate = techinfo.DevrealDate;
                    techi.MEMO = techinfo.MEMO;
                    DBContext.SaveAndUpdate<TechnologyInfo>(trans,techi);
                   
                }

                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "CheckTechnology");
                return ex.Message;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            } 
        }

        //查找工序
        public IList<RouteDetails> FindStationsByPDNo(string partdrawingno)
        { 
            //IList<RouteDetails> objs = DBContext.FindArray<RouteDetails>(RouteDetails.Meta.RouteName == partdrawingno,RouteDetails.Meta.OrderId.ASC);
            //IList<RouteDetails> objs = DBContext.FindArray<RouteDetails>(RouteDetails.Meta.RouteName == partdrawingno);
            string sql = "select * from mes_master.route_details where route_name='"+partdrawingno+"'";

            IList<RouteDetails> objs = DBContext.ExcuteSql(sql).ToBusiObjects<RouteDetails>();
            return objs;
        }

        public string SaveStationInfo(ProgramInfo pinfo)
        {
            try
            {
                ProgramInfo rds = DBContext.Find<ProgramInfo>(ProgramInfo.Meta.PARTSDRAWINGNO == pinfo.PARTSDRAWINGNO&ProgramInfo.Meta.ProcessNo==pinfo.ProcessNo);
                if (rds!=null)
                {
                    DBContext.Remove<ProgramInfo>(rds);
                }
                pinfo.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                pinfo.CreatedDate = DateTime.Now;
                pinfo.UpdatedBy = this.UserCode;
                
                DBContext.SaveAndUpdate<ProgramInfo>(pinfo);
                
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string CheckDevelop(string obj, bool b)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return "请选择一个程序进行审核";
            }
            string[] publishCount = obj.Split(';');
            if (publishCount == null || publishCount.Length == 0)
            {
                return "请选择一个编程任务进行审核";
            }
            IDbTransaction trans = DBContext.OpenTrans();
            try
            {
                foreach (string key in publishCount)
                {
                    TechnologyWip techinfo = DBContext.Find<TechnologyWip>(trans, TechnologyWip.Meta.PARTSDRAWINGNO == key);
                    // OrderHead oh = DBContext.Find<OrderHead>(trans, OrderHead.Meta.OrderNo == od.OrderNo);
                    if (b)
                    {
                        techinfo.STATUS = 6;
                        techinfo.StatusMemo = "编程已审核";
                    }
                    else
                    {
                        techinfo.STATUS = 10;
                        techinfo.StatusMemo = "编程驳回";
                    }

                    techinfo.UpdatedBy = this.UserCode;
                    techinfo.UpdatedDate = DateTime.Now;
                    DBContext.SaveAndUpdate<TechnologyWip>(trans, techinfo);

                    //存入历史记录
                    TechnologyInfo techi = new TechnologyInfo();
                    techi.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                    techi.PARTSDRAWINGNO = techinfo.PARTSDRAWINGNO;
                    techi.PlanDate = techinfo.PlanDate;
                    techi.ProcessEngineer = techinfo.ProcessEngineer;
                    techi.ProcessFname = techinfo.ProcessFname;
                    techi.ProcessName = techinfo.ProcessName;
                    techi.ProcessPath = techinfo.ProcessPath;
                    techi.ProductCode = techinfo.ProductCode;
                    techi.ProductName = techinfo.ProductName;
                    techi.ProgramEngineer = techinfo.ProgramEngineer;
                    techi.ProgramName = techinfo.ProgramName;
                    techi.QZPARTSDRAWING = techinfo.QZPARTSDRAWING;
                    techi.RealDate = techinfo.RealDate;
                    techi.STATUS = techinfo.STATUS;
                    techi.StatusMemo = techinfo.StatusMemo;
                    techi.UpdatedBy = this.UserCode;
                    techi.CreatedDate = DateTime.Now;
                    techi.ACTIVE = "1";
                    techi.CustCode = techinfo.CustCode;
                    techi.CustName = techinfo.CustName;
                    techi.DevplanDate = techinfo.DevplanDate;
                    techi.DevrealDate = techinfo.DevrealDate;
                    techi.MEMO = techinfo.MEMO;
                    DBContext.SaveAndUpdate<TechnologyInfo>(trans, techi);

                }

                trans.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "CheckDevelop");
                return ex.Message;
            }
            finally
            {
                DBContext.ReleaseTrans(trans);
            }
        }

        //查找编程信息
        public IList<ProgramInfo> FindDevelopmentDetails(string partdrawingno)
        {
            return DBContext.FindArray<ProgramInfo>(ProgramInfo.Meta.PARTSDRAWINGNO == partdrawingno, ProgramInfo.Meta.ProcessNo.ASC);
        }
        public SysUser FindUserByUserName(string userName)
        {
            return DBContext.Find<SysUser>(SysUser.Meta.UserName == userName);
        }
        public void UpdateWorkOrderAssign(WorkOrder obj)
        {
            try
            {
                WorkOrder wo = DBContext.Find<WorkOrder>(WorkOrder.Meta.WO == obj.WO);
                if (wo != null)
                {
                    
                            wo.WORKER = obj.WORKER;
                            wo.WorkerName = obj.WorkerName;
                        
                    } 
                    wo.UpdatedDate = DateTime.Now;
                    wo.UpdatedBy = this.UserCode;
 
                DBContext.SaveAndUpdate<WorkOrder>(wo);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateWorkOrderAssign");
                throw ex;
            }
        }

        public void SaveTechnologyInfoForImport(TechnologyWip techinfo)
        {
            try
            {
                if (DBContext.Exist<TechnologyWip>(TechnologyWip.Meta.PARTSDRAWINGNO == techinfo.PARTSDRAWINGNO))
                {
                    TechnologyWip tw = DBContext.Find<TechnologyWip>(TechnologyWip.Meta.PARTSDRAWINGNO == techinfo.PARTSDRAWINGNO);
                    tw.ProcessEngineer = techinfo.ProcessEngineer;
                    tw.ProcessName = techinfo.ProcessName;
                    tw.STATUS = techinfo.STATUS;
                    tw.StatusMemo = techinfo.StatusMemo;
                    tw.PlanDate = techinfo.PlanDate;
                    tw.UpdatedDate = DateTime.Now;
                    tw.UpdatedBy = this.UserCode;
                    DBContext.SaveAndUpdate<TechnologyWip>(tw);
                }
                //else
                //{
                //    techinfo.CreatedDate = DateTime.Now;
                //    techinfo.UpdatedBy = this.UserCode;
                //    techinfo.STATUS = 0;
                //    techinfo.StatusMemo = "新建任务";
                //    techinfo.ACTIVE = "1";
                //    DBContext.SaveAndUpdate<TechnologyWip>(techinfo);

                //}
                //存入历史记录
                TechnologyInfo techi = new TechnologyInfo();
                techi.ID = PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString();
                techi.PARTSDRAWINGNO = techinfo.PARTSDRAWINGNO;
                techi.PlanDate = techinfo.PlanDate;
                techi.ProcessEngineer = techinfo.ProcessEngineer;
                techi.ProcessFname = techinfo.ProcessFname;
                techi.ProcessName = techinfo.ProcessName;
                techi.ProcessPath = techinfo.ProcessPath;
                techi.ProductCode = techinfo.ProductCode;
                techi.ProductName = techinfo.ProductName;
                techi.ProgramEngineer = techinfo.ProgramEngineer;
                techi.ProgramName = techinfo.ProgramName;
                techi.QZPARTSDRAWING = techinfo.QZPARTSDRAWING;
                techi.RealDate = techinfo.RealDate;
                techi.STATUS = techinfo.STATUS;
                techi.StatusMemo = techinfo.StatusMemo;
                techi.UpdatedBy = this.UserCode;
                techi.CreatedDate = DateTime.Now;
                techi.ACTIVE = "1";
                techi.CustCode = techinfo.CustCode;
                techi.CustName = techinfo.CustName;
                techi.DevplanDate = techinfo.DevplanDate;
                techi.DevrealDate = techinfo.DevrealDate;
                techi.MEMO = techinfo.MEMO;
                DBContext.SaveAndUpdate<TechnologyInfo>(techi);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveTechnologyInfoForImport");
                throw ex;
            }
        }
        public IList<WorkOrder> FindWorkOrder(string status, bool isAssign, string drawingcode = "", string productname = "", string batchnumber = "")
        {
           // IList<WorkOrder> bseqs = null;
            ConditionExpress ce = null;
             
            if (!string.IsNullOrEmpty(status))
            {
                ce = (ce & WorkOrder.Meta.STATUS == status);
            }
            if (!string.IsNullOrEmpty(drawingcode))
            {
                ce = (ce & WorkOrder.Meta.PartsdrawingCode.Like(drawingcode));
            }
            if (!string.IsNullOrEmpty(productname))
            {
                ce = (ce & WorkOrder.Meta.ProductName.Like(productname));
            }
            if (!string.IsNullOrEmpty(batchnumber))
            {
                ce = (ce & WorkOrder.Meta.BatchNumber.Like(batchnumber));
            } 
            //if (ce == null)
            //{
            //    ce = WorkOrder.Meta.CreatedDate > DateTime.Now.AddDays(-30) & WorkOrder.Meta.STATUS != "5";
            //    return DBContext.FindArray<WorkOrder>(ce, WorkOrder.Meta.CreatedDate.DESC);//5为无效工单
            //}
            
            

            if (isAssign)
            {
                 
                return DBContext.FindArray<WorkOrder>(ce, WorkOrder.Meta.UpdatedDate.DESC);
                 
            }
            else
            {
                ce = (ce &(WorkOrder.Meta.WORKER == null | WorkOrder.Meta.WORKER == ""));
                return DBContext.FindArray<WorkOrder>(ce, WorkOrder.Meta.UpdatedDate.DESC);

                
            }
            
        }
        public IList<ProgramInfo> FindProgramInfo(string partsdrawingno)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(partsdrawingno))
            {
                ce = (ProgramInfo.Meta.PARTSDRAWINGNO.Like(partsdrawingno));
            } 
            if (ce == null)
            {
                return null;
            }
            return DBContext.FindArray<ProgramInfo>(ce);
        }
        public IList<OrderDetail> FindOrderInfoForCreateWorkOrder(string orderno, string parsdrawingno, string status = "", string starttime = "", string endtime = "", string id = "")
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(orderno))
            {
                ce = (OrderDetail.Meta.OrderNo==orderno);
            }
            if (!string.IsNullOrEmpty(parsdrawingno))
            {
                ce = (ce & OrderDetail.Meta.PartsdrawingCode==parsdrawingno);
            }
            if (!string.IsNullOrEmpty(status))
            {
                ce = (ce & OrderDetail.Meta.STATUS == status);
            }
            if (!string.IsNullOrEmpty(id))
            {
                ce = (ce & OrderDetail.Meta.ID == id);
            }
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                if (DateTime.Compare(Convert.ToDateTime(starttime), Convert.ToDateTime(endtime)) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & OrderDetail.Meta.CreatedDate >= Convert.ToDateTime(starttime));
                ce = (ce & OrderDetail.Meta.CreatedDate <= Convert.ToDateTime(endtime));
            }

            if (ce == null)
            {
                return DBContext.LoadArray<OrderDetail>();
            }

            return DBContext.FindArray<OrderDetail>(ce);

        }
        public string FindActualTotalUnitTime(string workorder, string partsdrawing)
        {
            ConditionExpress ce = null;
            IList<TrackingWip> uplist = null; 
            if (!string.IsNullOrEmpty(workorder))
            {
                ce = (TrackingWip.Meta.WorkOrder == workorder);
            }

            if (!string.IsNullOrEmpty(partsdrawing))
            {
                ce = (ce & TrackingWip.Meta.PartsdrawingCode == partsdrawing);
            }

            if (ce == null)
            {
                return "0";
            }
            else
            {
                uplist = DBContext.FindArray<TrackingWip>(ce);
            }
            if(uplist!=null&&uplist.Count>0)
            {
                TimeSpan tempNewTaskTime = new TimeSpan(0, 0, 0, 0);// TimeSpan.ParseExact("00.00.00:00:00.0000000", @"MM\.dd\.hh\:mm\:ss\.fffffff", null);
                for (int i=0;i<uplist.Count;i++)
                {
                    if(!string.IsNullOrEmpty(uplist[i].TaskTime))
                    {
                        TimeSpan tempPauseTaskTime;
                        try
                        {
                            tempPauseTaskTime = TimeSpan.ParseExact(uplist[i].TaskTime, @"hh\:mm\:ss\.fffffff", null);//@"hh\:mm\:ss\.fffffff"
                        }
                        catch(Exception ex)
                        {
                            tempPauseTaskTime = TimeSpan.ParseExact(uplist[i].TaskTime, @"d\.hh\:mm\:ss\.fffffff", null);//@"hh\:mm\:ss\.fffffff"
                        }
                        tempNewTaskTime += tempPauseTaskTime; 
                    }
                }
                return tempNewTaskTime.ToString();
            }
            return "0";
        }
        public IList<TrackingWip> FindSNTrackingWIP(string workorder, string partsdrawingno, string station, string starttime, string endtime)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
                //ce = (WorkOrder.Meta.WO == workorder.WO);
                ce = (TrackingWip.Meta.WorkOrder.Like(workorder));
            }

            if (!string.IsNullOrEmpty(partsdrawingno))
            {
                //ce = (ce & WorkOrder.Meta.PartsdrawingCode == workorder.PartsdrawingCode);
                ce = (ce & TrackingWip.Meta.PartsdrawingCode.Like(partsdrawingno));
            }
            if (!string.IsNullOrEmpty(station))
            {
                ce = (ce & TrackingWip.Meta.StationName == station.Trim());
            }

            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                DateTime d1= Convert.ToDateTime(starttime);
                DateTime d2 = Convert.ToDateTime(endtime);
                if (DateTime.Compare((DateTime)d1, (DateTime)d2) > 0)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }
                ce = (ce & TrackingWip.Meta.CreatedDate >= d1);
                ce = (ce & TrackingWip.Meta.CreatedDate <= d2);
            }
            if (ce == null)
            {
                ce = TrackingWip.Meta.CreatedDate >= DateTime.Now.AddDays(-30);
            }
            return DBContext.FindArray<TrackingWip>(ce);
        }
        public IList<WorkOrderDetails> FindWODetailsNew(string workorder, string status
            , string partNo, string cust
            , string worker, string orderno)
        {
            ConditionExpress ce = null;
            if (!string.IsNullOrEmpty(workorder))
            {
                ce = (WorkOrderDetails.Meta.WO.Like(workorder));
            }
            if (!string.IsNullOrEmpty(status) && status != "4")
            {
                ce = (ce & WorkOrderDetails.Meta.STATUS == status);
            }
            if (!string.IsNullOrEmpty(partNo))
            {
                ce = (ce & WorkOrderDetails.Meta.PartsdrawingCode.Like(partNo));
            }
            if (!string.IsNullOrEmpty(cust))
            {
                ce = (ce & WorkOrderDetails.Meta.CustName == cust);
            }
            if (!string.IsNullOrEmpty(worker))
            {
                ce = (ce & WorkOrderDetails.Meta.WorkerName.Like(worker));
            }
            if (!string.IsNullOrEmpty(orderno))
            {
                ce = (ce & WorkOrderDetails.Meta.OrderNumber.Like(orderno));
            }

            if (ce == null)
            {
                ce = WorkOrderDetails.Meta.CreatedDate > DateTime.Now.AddDays(-30) & WorkOrderDetails.Meta.STATUS != "5";
                return DBContext.FindArray<WorkOrderDetails>(ce, WorkOrderDetails.Meta.CreatedDate.DESC);//5为无效工单
            } 
            return DBContext.FindArray<WorkOrderDetails>(ce, WorkOrderDetails.Meta.CreatedDate.DESC);
        }
        public TextValueInfo[] ListBindStations(string workorder)
        {
            //查询已经绑定订单的未关闭的零件图号
           // string sql = "SELECT station_name,route_code FROM MES_MASTER.work_order_details WHERE worker_name is null and wo='"
            string sql = "SELECT station_name,route_code FROM MES_MASTER.work_order_details WHERE  wo='"
                + workorder+"' ORDER BY route_code ASC                  ";
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int rows = ds.Tables[0].Rows.Count;
                TextValueInfo[] vts = new TextValueInfo[rows];
                for (var i = 0; i < rows; i++)
                {
                    vts[i] = new TextValueInfo();
                    vts[i].Value = ds.Tables[0].Rows[i][0].ToString()+"|"+ ds.Tables[0].Rows[i][1].ToString();
                    vts[i].Text = ds.Tables[0].Rows[i][0].ToString() + "|" + ds.Tables[0].Rows[i][1].ToString();
                }
                return vts;
            }
            else
            {
                return null;
            }
        }
        public void UpdateWorkOrderAssign(WorkOrderDetails obj)
        {
            try
            {
                WorkOrderDetails wo = DBContext.Find<WorkOrderDetails>(WorkOrderDetails.Meta.WO == obj.WO
                    & WorkOrderDetails.Meta.RouteCode==obj.RouteCode);
                if (wo != null)
                {

                    wo.WORKER = obj.WORKER;
                    wo.WorkerName = obj.WorkerName;

                }
                wo.UpdatedDate = DateTime.Now;
                wo.UpdatedBy = this.UserCode;

                DBContext.SaveAndUpdate<WorkOrderDetails>(wo);
            }
            catch (Exception ex)
            {
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "UpdateWorkOrderAssign");
                throw ex;
            }
        }
        public string[] GetNextStation(string workorder,string stationid)
        { 
            string sql = "SELECT station_name,route_code FROM MES_MASTER.work_order_details WHERE  wo='"
                + workorder + "' ORDER BY route_code ASC                  ";
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            string[] re = { "", "" };
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if(stationid=="PRINT")
                {
                    string res = ds.Tables[0].Rows[0][0].ToString() + "," + ds.Tables[0].Rows[0][1].ToString();
                    return res.Split(',');
                }
                else
                {
                    for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                    {
                        if(stationid==ds.Tables[0].Rows[i][1].ToString())
                        {
                            if(i==ds.Tables[0].Rows.Count-1)
                            {
                                string res = "end,end";
                                return res.Split(',');
                            }
                            else
                            {
                                string res = ds.Tables[0].Rows[i + 1][0].ToString()
                                    + "," + ds.Tables[0].Rows[i + 1][1].ToString();
                                return res.Split(',');
                            }
                        }
                    }
                    return re;
                } 
            }
            else
            {
                return re;
            }
        }
        public string[] GetBeforeStation(string workorder, string stationid)
        {
            string sql = "SELECT station_name,route_code FROM MES_MASTER.work_order_details WHERE  wo='"
                + workorder + "' ORDER BY route_code ASC                  ";
            DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            string[] re = { "", "" };
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (stationid == "PRINT")
                {
                    string res = "PRINT,PRINT";
                    return res.Split(',');
                }
                else if(stationid=="end")
                {
                    int r = ds.Tables[0].Rows.Count - 1;
                    string res = ds.Tables[0].Rows[r][0].ToString()
                                    + "," + ds.Tables[0].Rows[r][1].ToString();
                    return res.Split(',');
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (stationid == ds.Tables[0].Rows[i][1].ToString())
                        {
                            if (i == 0)
                            {
                                string res = "PRINT,PRINT";
                                return res.Split(',');
                            }
                            else
                            {
                                string res = ds.Tables[0].Rows[i - 1][0].ToString()
                                    + "," + ds.Tables[0].Rows[i - 1][1].ToString();
                                return res.Split(',');
                            }
                        }
                    }
                   
                    return re;
                }
            }
            else
            {
                return re;
            }
        }
        public TrackingTemp LoadTrackingTemp()
        {
            ConditionExpress ce = null;
            ce = (TrackingTemp.Meta.UpdatedBy == this.UserCode | TrackingTemp.Meta.NextEmp == this.UserCode);
            return DBContext.Find<TrackingTemp>(ce);
        }
        public IList<WorkOrderDetails> FindWorkOrderTask(string worker)
        {
            ConditionExpress ce = (WorkOrderDetails.Meta.STATUS == "1");//运行

            if (!string.IsNullOrEmpty(worker))
            {
                //ce = (ce & WorkOrder.Meta.WORKER == worker);
                ce = (ce & WorkOrderDetails.Meta.WORKER.Like(worker));
            }
            else
            {
                ce = (ce & WorkOrderDetails.Meta.WORKER.Like(this.UserCode));
            }

            return DBContext.FindArray<WorkOrderDetails>(ce);
        }
    } 
}
