using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using Freeworks.Common;
using Freeworks.Common.Util;
using Freeworks.Common.Paging;
using Freeworks.Common.Cryptography;

using Freeworks.ORM;
using Freeworks.ORM.Core;
using DAL;
using QRCodeGenerate;
namespace BLL
{
    public class PubHelper
    {
        private DataContext DBContext;

        private PubHelper(DataContext _db)
        {
            this.DBContext = _db;
        }

        public static PubHelper GetHelper(DataContext _db)
        {
            return new PubHelper(_db);
        }

        public static PubHelper GetHelper()
        {
            return new PubHelper(DataServiceFactory.Create(BLLConstants.SITE_DEFAULT + BLLConstants.BU_DEFAULT));
        }

        public string GetCurrShift(string site,string bu)
        {
            string sql = "SELECT FLX.APP_ROUTE_V2.GET_CURR_SHIFT('{0}','{1}') FROM DUAL";
            sql = string.Format(sql, site, bu);
            return (string)DBContext.ExcuteSql(sql).ToSingle();
        }

        public decimal GetNextID()
        {
            return (decimal)DBContext.ExcuteSql("SELECT MES_MASTER.SEQ_COM_IDS.nextval FROM DUAL").ToSingle();
        }

        public decimal GetNextID(string seqName)
        {
            return (decimal)DBContext.ExcuteSql("SELECT " +  seqName + ".nextval FROM DUAL").ToSingle();
        }
        
        public bool ExistUser(string userCode)
        {
            return DBContext.Exist<SysUser>(userCode);
        }

        public string GetUserEmail(string userCode)
        {
            SysUser user = DBContext.Find<SysUser>(SysUser.Meta.UserCode == userCode);
            if (user != null && !string.IsNullOrEmpty(user.EMAIL))
            {
                return user.EMAIL;
            }

            return "";
        }

        //public IList<BasBuInfo> ListBU()
        //{
        //    return DBContext.FindArray<BasBuInfo>(BasBuInfo.Meta.STATUS == BLLConstants.COM_STATUS_ACTIVED);
        //}

        //public IList<BasBuInfo> GetSiteBU(string siteCode)
        //{
        //    IList<BasSiteBu> bus = DBContext.FindArray<BasSiteBu>(BasSiteBu.Meta.SITE == siteCode);
        //    IList<BasBuInfo> rets = new List<BasBuInfo>();
        //    IList<BasBuInfo> buis = DBContext.FindArray<BasBuInfo>(BasBuInfo.Meta.STATUS == BLLConstants.COM_STATUS_ACTIVED);

        //    foreach (BasSiteBu siteBU in bus)
        //    {
        //        if (buis.Count(c => c.CODE == siteBU.BuCode) > 0)
        //        {
        //            rets.Add(buis.Single(c => c.CODE == siteBU.BuCode));
        //        }
        //    }
        //    return rets;
        //}

        //public IList<BasSiteInfo> ListSite()
        //{
        //    return DBContext.FindArray<BasSiteInfo>(BasSiteInfo.Meta.STATUS == BLLConstants.COM_STATUS_ACTIVED);
        //}

        //public IList<TextValueInfo> ListSiteBU()
        //{
        //    IList<BasSiteBu> bus = DBContext.LoadArray<BasSiteBu>();
        //    IList<TextValueInfo> rets = new List<TextValueInfo>();
        //    foreach (BasSiteBu sb in bus)
        //    {
        //        BasSiteInfo site = DBContext.Find<BasSiteInfo>(BasSiteInfo.Meta.CODE == sb.SITE);
        //        rets.Add(new TextValueInfo() { Text = site.SiteName + "-" + sb.BuName, Value = site.CODE + "^" + sb.BuCode });
        //    }
        //    return rets;
        //}
        /// <summary>
        /// 通過功能代碼取得菜單項目信息
        /// </summary>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        //public VMenuInfo FindMenuByID(string menuCode, UserInfo user)
        //{
        //    return DBContext.Find<VMenuInfo>(VMenuInfo.Meta.CODE == menuCode & VMenuInfo.Meta.BU == user.BUCode
        //        & VMenuInfo.Meta.SITE == user.SiteCode);
        //}


        public IList<SysRole> GetUserRoles(string userCode)
        {
            return SpCaller.GetCaller(DBContext).CallGetUserRoles(userCode).ToBusiObjects<SysRole>();
        }

        //public IList<SysGroupSection> GetUserSections(string userCode, string userBU, string userSite)
        //{
        //    return SpCaller.GetCaller(DBContext).CallGetUserSections(userCode, userBU, userSite).ToBusiObjects<SysGroupSection>();
        //}

        //public string GetCodeText(string name, string value)
        //{
        //    return DBContext.Find<BasCodeInfo>(BasCodeInfo.Meta.KeyName == name
        //        & BasCodeInfo.Meta.ItemValue == value
        //       ).ItemText;
        //}

        //public BasCodeInfo GetCodeValue(string name, string text)
        //{
        //    return DBContext.Find<BasCodeInfo>(BasCodeInfo.Meta.KeyName == name
        //        & BasCodeInfo.Meta.ItemText == text
        //        & BasCodeInfo.Meta.BU == BLLConstants.BU_DEFAULT);
        //}

        //public IList<BasCodeInfo> ListDefaultCodeText(string name)
        //{
        //    return DBContext.FindArray<BasCodeInfo>(BasCodeInfo.Meta.KeyName == name
        //        & BasCodeInfo.Meta.BU == BLLConstants.BU_DEFAULT 
        //        & BasCodeInfo.Meta.SITE == BLLConstants.SITE_DEFAULT);
        //}

        //public IList<BasCodeInfo> ListCodeText(string name,string site,string bu)
        //{
        //    return DBContext.FindArray<BasCodeInfo>(BasCodeInfo.Meta.KeyName == name
        //        & BasCodeInfo.Meta.BU == bu
        //        & BasCodeInfo.Meta.SITE == site);
        //}

        public string GetConfigValue(string name)
        {
            SysConfigInfo ret = DBContext.Find<SysConfigInfo>(SysConfigInfo.Meta.KeyName == name);
            if (ret != null)
            {
                return ret.ItemValue;
            }
            else
            {
                return "";
            }
        }

        //public string GetConfigValue(string name)
        //{
        //    return DBContext.Find<SysConfigInfo>(SysConfigInfo.Meta.KeyName == name).ItemValue;
        //}

        //public IList<BasCodeInfo> LoadAllCode(string site ,string bu)
        //{
        //    return DBContext.FindArray<BasCodeInfo>(BasCodeInfo.Meta.SITE == site & BasCodeInfo.Meta.BU == bu);            
        //}

        //public IDictionary<string, string> LoadAllConfig(string site , string bu)
        //{
        //    IList<SysConfigInfo> objs = DBContext.FindArray<SysConfigInfo>(SysConfigInfo.Meta.SITE == site & SysConfigInfo.Meta.BU == bu);
        //    IDictionary<string, string> ret = new Dictionary<string, string>();
        //    foreach (SysConfigInfo o in objs)
        //    {
        //        ret.Add(o.KeyName, o.ItemValue);
        //    }

        //    return ret;
        //}

        public IDictionary<string, string> LoadAllBalInfo()
        {
            ICollection<SysBllConfig> objs = DBContext.FindArray<SysBllConfig>(SysBllConfig.Meta.ID!=null);
            IDictionary<string, string> ret = new Dictionary<string, string>();
            foreach (SysBllConfig o in objs)
            {
                ret.Add(o.BaseName + o.BU + o.SITE, o.BllName);
            }

            return ret;
        }

        //public IDictionary<string, SysMessageInfo> LoadAllMsgInfo()
        //{
        //    ICollection<SysMessageInfo> objs = DBContext.LoadArray<SysMessageInfo>();
        //    IDictionary<string, SysMessageInfo> ret = new Dictionary<string, SysMessageInfo>();
        //    foreach (SysMessageInfo o in objs)
        //    {
        //        ret.Add(o.CODE, o);
        //    }

        //    return ret;
        //}

        //public GroupConnInfo LoadSAPGroupConfig(string site,string bu)
        //{
        //    string config = PubHelper.GetHelper().GetConfigValue(BLLConstants.CONFIG_SAP_GINFO,site,bu);
        //    string[] configs = config.Split(';');
        //    GroupConnInfo sapInfo = new GroupConnInfo();
        //    sapInfo.Client = configs[0];
        //    sapInfo.MessageServer = configs[1];
        //    sapInfo.System = configs[2];
        //    sapInfo.Language = configs[3];
        //    sapInfo.User = configs[4];
        //    sapInfo.Password = CryptographyManager.Instance.SymmetricDecrpyt(configs[5], CryptographyManager.DEFAULT_KEY);
        //    sapInfo.CodePage = configs[6];
        //    sapInfo.GroupName = configs[7];
        //    return sapInfo;
        //}
        //public ConnInfo LoadSAPConfig(string site, string bu)
        //{
        //    string config = PubHelper.GetHelper(DBContext).GetConfigValue(BLLConstants.CONFIG_SAP_INFO,site,bu);
        //    string[] configs = config.Split(';');
        //    ConnInfo sapInfo = new ConnInfo();
        //    sapInfo.Client = configs[0];
        //    sapInfo.ApplicationServer = configs[1];
        //    sapInfo.SystemNumber = configs[2];
        //    sapInfo.Language = configs[3];
        //    sapInfo.User = configs[4];
        //    sapInfo.Password = CryptographyManager.Instance.SymmetricDecrpyt(configs[5], CryptographyManager.DEFAULT_KEY);
        //    sapInfo.CodePage = configs[6];

        //    return sapInfo;
        //}

        //public IDictionary<string, string> LoadAllSqlText(string site,string bu)
        //{
        //    ICollection<SysSqlConfig> objs = DBContext.FindArray<SysSqlConfig>(SysSqlConfig.Meta.SITE == site & SysSqlConfig.Meta.BU == bu);
        //    IDictionary<string, string> ret = new Dictionary<string, string>();
        //    foreach (SysSqlConfig o in objs)
        //    {
        //        ret.Add(o.ClassName + o.MethodName + o.SqlName, o.SqlText);
        //    }

        //    return ret;
        //}

        /// <summary>
        /// 取得所有錯誤消息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="bu"></param>
        /// <returns></returns>
        //public IList<SysMessageInfo> LoadAllSysMsg(string site, string bu)
        //{
        //    return DBContext.FindArray<SysMessageInfo>(SysMessageInfo.Meta.SITE == BLLConstants.SITE_DEFAULT &
        //        SysMessageInfo.Meta.BU == BLLConstants.BU_DEFAULT);
        //}


        /// <summary>
        /// 獲取客戶端登錄的用戶信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        //public UserInfo GetStationUserInfo(string ip)
        //{
        //    SysStationInfo ssi = PubHelper.GetHelper().GetLoginStatus(ip, "");
        //    if (ssi == null)
        //    {
        //        throw new BusiException("The station has not login.");
        //    }

        //    UserInfo ui = new UserInfo();
        //    ui.BUCode = ssi.BU;
        //    ui.SiteCode = ssi.SITE;
        //    ui.StationID = ssi.StationId;
        //    ui.UserCode = ssi.LoginId;
        //    ui.IP = ssi.LoginIp;
        //    ui.LoginTime = (DateTime)ssi.UpdatedDate;

        //    return ui;
        //}

        /// <summary>
        /// 獲取IP地址登錄狀態
        /// </summary>
        /// <param name="ip">客戶端IP</param>
        /// <param name="stationId">分配的工站ID</param>
        /// <returns></returns>
        //public SysStationInfo GetLoginStatus(string ip, string stationId)
        //{
        //    SysStationInfo ssi = null;
        //    if (!string.IsNullOrWhiteSpace(ip))
        //    {
        //        ssi = DBContext.Find<SysStationInfo>(SysStationInfo.Meta.LoginIp == ip);
        //    }

        //    if (ssi == null && !string.IsNullOrWhiteSpace(stationId))
        //    {
        //        ssi = DBContext.Find<SysStationInfo>(SysStationInfo.Meta.StationId == stationId);
        //    }

        //    return ssi;
        //}

        /// <summary>
        /// 更新登錄者信息
        /// </summary>
        /// <param name="userInfo"></param>
        //public void UpdateLoginStatus(UserInfo userInfo){
        //    //檢查Station ID不重複
        //    if (!string.IsNullOrWhiteSpace(userInfo.StationID))
        //    {
        //        SysStationInfo ssi1 = DBContext.Find<SysStationInfo>(SysStationInfo.Meta.StationId == userInfo.StationID
        //            & SysStationInfo.Meta.SITE == userInfo.SiteCode
        //            & SysStationInfo.Meta.BU == userInfo.BUCode);
        //        if (ssi1!=null&&(ssi1.LoginIp != userInfo.IP))
        //        {
        //            new BusiException("The station has not been binded to the IP.");
        //            return;
        //        }
        //    }

        //    SysStationInfo ssi = DBContext.Find<SysStationInfo>(SysStationInfo.Meta.LoginIp == userInfo.IP);
        //    if (ssi == null)
        //    {
        //        ssi = new SysStationInfo();
        //        ssi.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
        //        ssi.STATUS = 0;
        //        ssi.CreatedDate = DateTime.Now;
        //    }

        //    ssi.SITE = userInfo.SiteCode;
        //    ssi.BU = userInfo.BUCode;
        //    ssi.LoginLang = userInfo.Lang;
        //    ssi.LoginIp = userInfo.IP;
        //    ssi.StationId = userInfo.StationID;
        //    ssi.LoginId = userInfo.UserCode;
        //    ssi.UpdatedBy = userInfo.UserCode;
        //    ssi.UpdatedDate = DateTime.Now;

        //    DBContext.SaveAndUpdate<SysStationInfo>(ssi);
        //}

        /// <summary>
        /// 登錄系統
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public void DoLogin(UserInfo userInfo)
        {
            if (!string.IsNullOrEmpty(userInfo.UserCode) && userInfo.UserCode.Length > 5)
            {

                string decryptDes = JiaMiJieMiCls.DecryptDes(userInfo.UserCode.Trim()).Trim();
                string code = decryptDes.Substring(0, 3);
                string other = decryptDes.Substring(3, decryptDes.Length - 3);
                string temp = "_QZ_MES_20";
                //if (string.Equals(other, "_QZ_MES_201707"))
                if (other.IndexOf(temp)!=-1)
                {
                    userInfo.UserCode = code;
                }
                SysUser su = DBContext.Find<SysUser>(SysUser.Meta.UserCode == userInfo.UserCode);
                if (su == null)
                {
                    throw new BusiException("此用戶不存在或已失效，不能進入系統。");
                }
                else
                {
                    userInfo.Password = CryptographyManager.Instance.SymmetricDecrpyt(su.PWD, CryptographyManager.DEFAULT_KEY);

                }
               // return;
            }

            if (!DBContext.Exist<SysUser>(SysUser.Meta.UserCode == userInfo.UserCode & SysUser.Meta.PWD == CryptographyManager.Instance.SymmetricEncrpyt(userInfo.Password, CryptographyManager.DEFAULT_KEY)))
            {
                throw new BusiException("用户或密码无效", "100001");
            }
            else
            {

                string userCode = userInfo.UserCode;

                SysUser user = DBContext.Find<SysUser>(SysUser.Meta.UserCode == userCode & SysUser.Meta.STATUS == 1);
                if (user == null)
                {
                    throw new BusiException("此用戶不存在或已失效，不能進入系統。");
                }
                
                userInfo.UserName = user.UserName;
                userInfo.DeptCode = user.DeptCode;
                userInfo.DeptName = user.DeptName;

                //if (userInfo.BUCode == BLLConstants.BU_DEFAULT && userInfo.SiteCode == BLLConstants.SITE_DEFAULT)
                //{
                //    userInfo.SiteCode = user.SITE;
                //    userInfo.BUCode = user.BU;
                //}
               
                userInfo.LoginTime = DateTime.Now;
                //獲取用戶角色
                IList<SysRole> roles = GetUserRoles(userCode);
                string[] roleNames = new string[roles.Count];
                for (int i = 0; i < roles.Count; i++)
                {
                    roleNames[i] = roles[i].RoleName;
                }
                ////獲取用戶工段
                //IList<SysGroupSection> sections = GetUserSections(userCode, user.BU, user.SITE);
                //string[] sectionNames = new string[sections.Count];
                //for (int i = 0; i < sections.Count; i++)
                //{
                //    sectionNames[i] = sections[i].SectionName;
                //}

                //if (user.UserType == Convert.ToInt32(EUserType.ADMIN))
                //{
                //    userInfo.IsAdmin = true;
                //}
                userInfo.Roles = roleNames;
            }
        }


        /// <summary>
        /// 登錄系統
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public void DoLogin4Dc(UserInfo userInfo)
        {
            if (!DBContext.Exist<SysUser>(SysUser.Meta.UserCode == userInfo.UserCode & SysUser.Meta.PWD == CryptographyManager.Instance.SymmetricEncrpyt(userInfo.Password, CryptographyManager.DEFAULT_KEY)))
            {
                throw new BusiException("User Code or password is invalidated.", "100001");
            }
            else
            {
                string userCode = userInfo.UserCode;

                SysUser user = DBContext.Find<SysUser>(SysUser.Meta.UserCode == userCode & SysUser.Meta.STATUS == 1);
                if (user == null)
                {
                    throw new BusiException("此用戶不存在或已失效，不能進入系統。");
                }
                                
                userInfo.UserName = user.UserName;
                userInfo.DeptCode = user.DeptCode;
                userInfo.DeptName = user.DeptName;

                if (userInfo.BUCode == BLLConstants.BU_DEFAULT && userInfo.SiteCode == BLLConstants.SITE_DEFAULT)
                {
                    userInfo.SiteCode = user.SITE;
                    userInfo.BUCode = user.BU;
                }

                userInfo.LoginTime = DateTime.Now;
                //獲取用戶角色
                IList<SysRole> roles = GetUserRoles(userCode);
                string[] roleNames = new string[roles.Count];
                for (int i = 0; i < roles.Count; i++)
                {
                    roleNames[i] = roles[i].RoleName;
                }
                //獲取用戶工段
                //IList<SysGroupSection> sections = GetUserSections(userCode, user.BU, user.SITE);
                //string[] sectionNames = new string[sections.Count];
                //for (int i = 0; i < sections.Count; i++)
                //{
                //    sectionNames[i] = sections[i].SectionName;
                //}

                //if (user.UserType == Convert.ToInt32(EUserType.ADMIN))
                //{
                //    userInfo.IsAdmin = true;
                //}
                userInfo.Roles = roleNames;

                //if (!userInfo.IsAdmin)
                //{
                //    if (!DBContext.Exist<SysUserSitebu>(SysUserSitebu.Meta.UserCode == userInfo.UserCode &
                //        SysUserSitebu.Meta.SiteCode == userInfo.SiteCode
                //        & SysUserSitebu.Meta.BuCode == userInfo.BUCode))
                //    {
                //        throw new BusiException("此用戶沒有權限訪問選擇的Site。");
                //    }
                //}

            }
        }

        //public IList<BasBuInfo> GetBUList()
        //{
        //    return DBContext.FindArray<BasBuInfo>(ConditionExpress.EMPTY, BasBuInfo.Meta.CODE.ASC);
        //}

        //public string GetBUName(string code)
        //{
        //    return DBContext.Load<BasBuInfo>(code).BuName;
        //}

        //public SysErrorLog GetErrorLog(string id)
        //{
        //    return DBContext.Find<SysErrorLog>(SysErrorLog.Meta.ID == id);
        //}

        //public string GetSiteName(string code)
        //{
        //    return DBContext.Load<BasSiteInfo>(code).SiteName;
        //}

        //public IList<BasSiteInfo> GetSiteList()
        //{
        //    return DBContext.FindArray<BasSiteInfo>(ConditionExpress.EMPTY, BasSiteInfo.Meta.CODE.ASC);
        //}

        //public IList<BasBuInfo> GetValidBUList()
        //{
        //    return DBContext.FindArray<BasBuInfo>(BasBuInfo.Meta.STATUS == BLLConstants.COM_STATUS_ACTIVED, BasBuInfo.Meta.CODE.ASC);
        //}

        //public IList<BasSiteInfo> GetValidSiteList()
        //{
        //    return DBContext.FindArray<BasSiteInfo>(BasSiteInfo.Meta.STATUS == BLLConstants.COM_STATUS_ACTIVED, BasSiteInfo.Meta.CODE.ASC);
        //}

        //public IList<BasAttachmentInfo> LoadFiles(string key)
        //{
        //    return DBContext.FindArray<BasAttachmentInfo>(BasAttachmentInfo.Meta.FriendlyKey == key);
        //}

        //public BasLabelTemplate GetLabelTemplate(string labelType, string stationId, string site, string bu)
        //{
        //    BasLabelTemplate lbl = DBContext.Find<BasLabelTemplate>(BasLabelTemplate.Meta.LabelType == labelType
        //        & BasLabelTemplate.Meta.BU == bu
        //        & BasLabelTemplate.Meta.SITE == site
        //        & BasLabelTemplate.Meta.StationId == stationId);
        //    if (lbl == null)
        //    {
        //        lbl = DBContext.Find<BasLabelTemplate>(BasLabelTemplate.Meta.LabelType == labelType
        //        & BasLabelTemplate.Meta.BU == bu
        //        & BasLabelTemplate.Meta.SITE == site
        //        & BasLabelTemplate.Meta.StationId == "ALL");
        //    }

        //    return lbl;
        //}

        public void SaveAttachment(BasAttachmentInfo f)
        {
            DBContext.SaveAndUpdate<BasAttachmentInfo>(f);
        }

        public BasAttachmentInfo FindAttachment(string fileNo)
        {
            return DBContext.Find<BasAttachmentInfo>(BasAttachmentInfo.Meta.FileNo == fileNo);
        }

        public void Error(Exception ex, string userCode, string funcCode)
        {
            SysErrorLog log = new SysErrorLog();
            log.ID = GetNextID().ToString();
            log.CreatedDate = DateTime.Now;
            log.FuncCode = funcCode;
            log.UserCode = userCode;
            log.INFO = ex == null ? "" : ex.StackTrace;
            log.LogType = "SYSTEM";
            DBContext.SaveAndUpdate<SysErrorLog>(log);
        }

        public void Error(string msg, string userCode, string funcCode)
        {
            SysErrorLog log = new SysErrorLog();
            log.ID = GetNextID().ToString();
            log.CreatedDate = DateTime.Now;
            log.FuncCode = funcCode;
            log.UserCode = userCode;
            log.INFO = msg;
            log.LogType = "USER";
            DBContext.SaveAndUpdate<SysErrorLog>(log);
        }

        //public void Log(string msg, string funcCode, string userCode)
        //{
        //    SysOperationLog log = new SysOperationLog();
        //    log.ID = GetNextID().ToString();
        //    log.UpdatedDate = DateTime.Now;
        //    log.UserCode = userCode;
        //    log.MEMO = msg;
        //    log.FuncCode = funcCode;
        //    DBContext.SaveAndUpdate<SysOperationLog>(log);
        //}

        public void SaveLog(SysErrorLog log)
        {
            log.CreatedDate = DateTime.Now;
            log.UpdatedDate = DateTime.Now;
            log.ID = PubHelper.GetHelper(DBContext).GetNextID().ToString();
            DBContext.Create<SysErrorLog>(log);
        }

        public IList<SysErrorLog> SearchLogs(string userCode, string startDate, string endDate)
        {
            string sql = "SELECT * FROM SYS_ERROR_LOG WHERE 1=1";
            if (!string.IsNullOrEmpty(userCode))
            {
                sql += " AND User_Code = '" + userCode + "'";
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                sql += " AND (CREATED_DATE BETWEEN TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI') and TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI'))";
            }
            sql += " ORDER BY CREATED_DATE DESC";
            return DBContext.ExcuteSql(sql).ToBusiObjects<SysErrorLog>();
        }

        //public bool ChkUserAccount(string userCode, string serviceName, string methodName)
        //{
        //    SysServicePermConfig wsp = DBContext.Find<SysServicePermConfig>(SysServicePermConfig.Meta.ServiceName == serviceName & SysServicePermConfig.Meta.MethodName == methodName);
        //    if (wsp != null && wsp.PermName.ToUpper() == "ALL")
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public bool ChkFunc(string userCode, string menuCode)
        {

            object o = SpCaller.GetCaller(DBContext).CallChkMenuPrivilege(menuCode, userCode).ExecuteOutParameters()["P_RET"];
            if (o != null && Convert.ToInt32(o) == 0)
            {
                return false;
            }

            return true;
        }

        //public bool ChkPermision(string userCode, string serviceName, string methodName)
        //{
        //    SysServicePermConfig wsp = DBContext.Find<SysServicePermConfig>(SysServicePermConfig.Meta.ServiceName == serviceName & SysServicePermConfig.Meta.MethodName == methodName);
        //    if (wsp != null)
        //    {
        //        try
        //        {
        //            object o = SpCaller.GetCaller(DBContext).CallChkPermPrivilege(wsp.MenuCode, wsp.PermName, userCode).ExecuteOutParameters()["P_RET"];
        //            if (o != null && Convert.ToInt32(o) == 0)
        //            {
        //                return false;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //public BasAttachmentInfo GetAttachInfo(string fileNo)
        //{
        //    return DBContext.Find<BasAttachmentInfo>(BasAttachmentInfo.Meta.FileNo == fileNo);
        //}

        public string ToStr(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 返回工站路徑
        /// </summary>
        /// <param name="family"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        //public string GetStationURL(string family, string groupName)
        //{
        //    BasStationGroupType sgt = null;
        //    if (!string.IsNullOrEmpty(family))
        //    {
        //        sgt = DBContext.Find<BasStationGroupType>(BasStationGroupType.Meta.FAMILY == family &
        //             BasStationGroupType.Meta.GroupName == groupName);
        //    }
        //    else
        //    {
        //        sgt = DBContext.Find<BasStationGroupType>(
        //             BasStationGroupType.Meta.GroupName == groupName);
        //    }
        //    if (sgt != null)
        //    {
        //        return sgt.FuncUrl;
        //    }

        //    return "";
        //}
    }
}
