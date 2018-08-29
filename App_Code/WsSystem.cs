using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Freeworks.Common;
using BLL;
using DAL;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Collections;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using QRCodeGenerate;
using System.Text.RegularExpressions;
/// <summary>
/// WsSystem 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 [System.Web.Script.Services.ScriptService]
public class WsSystem : WsBase
{
    private SystemBO _bal;
    public WsSystem() : base()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        _bal = BLLFactory.GetBal<SystemBO>(_userInfo);
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveBaseInfo(BasBase bbase)
    {
        _bal.SaveBaseInfo(bbase);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public HttpResponseMessage GetUserMenu()
    public ArrayList GetUserMenu()
    {
       // string sres =  "{'menus':[";
          
        IList<SysMenu> lmenu = _bal.GetMenus();
        ArrayList alist = new ArrayList();
        foreach(SysMenu sm in lmenu)
        {
            string sreshead = string.Empty;
            IList<SysMenu> lsubmenu = _bal.GetSubMenus(sm.CODE);
            if(lsubmenu.Count>0)
            {
                ShowMenu smenu = new ShowMenu();
                smenu.menuid = sm.CODE;
                smenu.icon = "icon-sys";
                smenu.menuname = sm.Name;
               // sreshead = "{'menuid':'" + sm.CODE + "','icon':'icon-sys','menuname':'" + sm.Name + "','menus':[";
                string srescontent = string.Empty;
                foreach (SysMenu ssm in lsubmenu)
                {
                    ShowSubMenu ssmenu = new ShowSubMenu();
                    ssmenu.menuid = ssm.CODE;
                    ssmenu.menuname = ssm.Name;
                    ssmenu.icon = "icon-nav";
                    ssmenu.url = ssm.PageUrl;
                    smenu.menus.Add(ssmenu);
                    //if (string.IsNullOrEmpty(srescontent))
                    //{
                    //    srescontent = "{'menuname':'" + ssm.Name + "','icon':'icon-nav','url':'" + ssm.PageUrl + "'}";
                    //}
                    //else
                    //{
                    //    srescontent += ",{'menuname':'" + ssm.Name + "','icon':'icon-nav','url':'" + ssm.PageUrl + "'}";
                    //}
                }
                // sres += sreshead + srescontent + "]}";
                alist.Add(smenu);
            }
        }
        // sres += "]";
        // sres += "]}";

        //HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(sres, Encoding.GetEncoding("UTF-8"), "application/json") };
        
        return alist;
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveUser(SysUser user)
    {
        _bal.SaveUser(user);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveUser(string userCode)
    {
        _bal.RemoveUser(userCode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UpdateUserRole(string[] items, string userCode)
    {
        _bal.UpdateUserRole(items, userCode);
    }

    //[WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    //public string TreeRoleUser(string userCode)
    //{
    //    IList<SysRole> objs = _bal.ListRole();
    //    StringBuilder xml = new StringBuilder("<?xml version='1.0' encoding='utf-8'?>");
    //    xml.Append("<tree id='0'>");
    //    foreach (SysRole o in objs)
    //    {
    //        xml.Append(string.Format("<item text='[{0}]:{1}' id='{2}' {3} > ", o.RoleName, o.MEMO, o.ID, _bal.ExistRoleUser(userCode, o.ID) ? "checked='1'" : ""));

    //        xml.Append("</item>");
    //    }
    //    xml.Append("</tree>");
    //    return xml.ToString();
    //}
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void TreeRoleUser(string userCode)
    {
        IList<SysRole> objs = _bal.ListRole();
        List<UserRoles> urs = new List<UserRoles>();
        foreach (SysRole o in objs)
        {
            UserRoles ur = new UserRoles();
            ur.ID = o.ID;
            ur.RoleName = o.RoleName;
            ur.MEMO = o.MEMO;
            ur.Checked = _bal.ExistRoleUser(userCode, o.ID);
            urs.Add(ur);
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (urs != null & urs.Count > 0)
            map.Add("total", urs.Count);
        map.Add("rows", urs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindUsers()
    {
        string userCode = Context.Request.Form["userCode"];
        string userName = Context.Request.Form["userName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<SysUser> objs = _bal.FindUsers(userCode, userName);
        List<SysUser> bs = new List<SysUser>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (SysUser bb in objs)
        {
            if (j > istart && j < iend)
            {
                SysUser bbtemp = new SysUser();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<SysUser> objs = _bal.FindUsers(userCode, userName);

        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //foreach (SysUser o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.UserCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UserCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UserName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.DeptName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate));
        //    //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindPublicUsers()
    {
        string userCode = Context.Request.Form["userCode"];
        string userName = Context.Request.Form["userName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<SysUser> objs = _bal.FindPublicUsers(userCode, userName);
        List<SysUser> bs = new List<SysUser>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (SysUser bb in objs)
        {
            if (j > istart && j < iend)
            {
                SysUser bbtemp = new SysUser();
                bbtemp = bb;
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<SysUser> objs = _bal.FindPublicUsers(userCode, userName);

        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //foreach (SysUser o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.UserCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UserCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UserName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.DeptName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MOBILE));
        //    //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public SysUser FindUserByCode(string userCode)
    {
        return _bal.FindUserByCode(userCode);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FindUserNameByCode(string userCode)
    {
        SysUser su = _bal.FindUserByCode(userCode);
        //Dictionary<String, Object> map = new Dictionary<String, Object>();

        //    map.Add("name", su.UserName);

        //Context.Response.Write(JsonConvert.SerializeObject(map));
        if (su != null)
        {
            return su.UserName;
        }
        else
        {
            return "";
        }
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public SysUser FindPublicUserByCode(string userCode)
    {
        IList < SysUser > objs =_bal.FindPublicUserByCode(userCode);
        if(objs.Count>0)
        {
            return objs[0];
        }
        else
        {
            return null;
        }
          
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListRole()
    {
        //string userCode = Context.Request.Form["userCode"];
        //string userName = Context.Request.Form["userName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<SysRole> objs = _bal.ListRole();
        List<SysRole> bs = new List<SysRole>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (SysRole bb in objs)
        {
            if (j > istart && j < iend)
            {
                SysRole bbtemp = new SysRole();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

        //IList<SysRole> objs = _bal.ListRole();
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //foreach (SysRole o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.RoleName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.STATUS));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate));
        //    //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();

    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<SysUser> ListUsersByRole(string roleId)
    {
        return (List<SysUser>)_bal.ListUsersByRole(roleId);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveRole(SysRole role)
    {
        _bal.SaveRole(role);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveRole(string id)
    {
        _bal.RemoveRole(id);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UpdateRoleUsers(string roleId, string[] userCode)
    {
        _bal.UpdateRoleUsers(roleId, userCode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public SysMenu FindMenuByCode(string code)
    {
        return _bal.FindMenuByCode(code);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListParentMenu()
    {
        return _bal.ListParentMenu();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveMenu(string code)
    {
        _bal.RemoveMenu(code);
    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveMenu(SysMenu menuInfo)
    {
        _bal.SaveMenu(menuInfo);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetAllMenuTree()
    {
        IList<SysMenu> menus = _bal.LoadMenuAll();
        var m1 = menus.Where(c => c.LevelNo == 1).OrderBy(p => p.CODE).ToArray();
        string res = "[{\"id\":0,\"text\":\"菜单\",\"state\":\"open\",\"children\":[";        
        foreach (SysMenu m in m1)
        {
            res += "{\"id\":\"" + m.CODE + "\",\"parentId\":0,\"text\":\"[" + m.CODE + "]" + m.Name + "\",\"nocheckbox\":true,\"state\":\"closed\",\"children\":[";

            var m2 = _bal.ListAuthorizabledSubFuncs(m.CODE);
            foreach (SysMenu sm in m2)
            {
                res += "{\"id\":\"" + sm.CODE + "\",\"parentId\":\"" + m.CODE + "\",\"text\":\"["+sm.CODE+"]" + sm.Name + "\"" + "},";

            }
            res = res.Substring(0, res.Length - 1);
            res += "]},";

        }
        res = res.Substring(0, res.Length - 1);
        res += "]}]";
        Context.Response.Write(res);


        //IList<SysMenu> menus = _bal.LoadMenuAll();
        //var m1 = menus.Where(c => c.LevelNo == 1).OrderBy(p => p.CODE).ToArray();        
        //StringBuilder xml = new StringBuilder();
        //xml.Append("<tree id='0'>");
        //foreach (SysMenu m in m1)
        //{
        //    if (menus.Count(c => c.ParentCode == m.CODE) > 0)
        //    {
        //        xml.Append(string.Format("<item text='[{1}]{0}' id='{1}' >", m.Name, m.CODE));
        //    }
        //    else
        //    {
        //        xml.Append(string.Format("<item text='[{1}]{0}' id='{1}'>", m.Name, m.CODE));
        //    }
        //    var m2 = menus.Where(c => c.ParentCode == m.CODE).OrderBy(p => p.CODE).ToArray();
        //    foreach (SysMenu sm in m2)
        //    {
        //        xml.Append(string.Format("<item text='[{1}]{0}' id='{1}'>", sm.Name, sm.CODE));

        //        xml.Append("</item>");
        //    }

        //    xml.Append("</item>");
        //}
        //xml.Append("</tree>");

        //return xml.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetAllMenuChkBoxTree(string roleId)
    {
        IList<SysMenu> menus = _bal.ListAuthorizabledMenus();
        var m1 = menus.Where(c => c.LevelNo == 1).OrderBy(p => p.CODE).ToArray();
        string res = "[{\"id\":0,\"text\":\"菜单\",\"state\":\"open\",\"children\":[";
        //xml.Append("<tree id='0'>");
        foreach (SysMenu m in m1)
        {
            res += "{\"id\":\"" + m.CODE + "\",\"parentId\":0,\"text\":\"" + m.Name + "\",\"nocheckbox\":true,\"state\":\"closed\",\"children\":[";
            
            var m2 = _bal.ListAuthorizabledSubFuncs(m.CODE);
            foreach (SysMenu sm in m2)
            {
                string bcheck = _bal.ExistRoleMenu(roleId, sm.CODE) ? "true" : "false";
                res += "{\"id\":\"" + sm.CODE + "\",\"parentId\":\"" + m.CODE + "\",\"text\":\"" + sm.Name + "\",\"checked\":" + bcheck + "},";
               
            }
            res = res.Substring(0, res.Length - 1);
            res += "]},";
            
        }
        res = res.Substring(0, res.Length - 1);
        res += "]}]";
        Context.Response.Write(res);
       // Context.Response.Write(JsonConvert.SerializeObject(res));
        //IList<SysMenu> menus = _bal.ListAuthorizabledMenus();
        //var m1 = menus.Where(c => c.LevelNo == 1).OrderBy(p => p.CODE).ToArray();
        //StringBuilder xml = new StringBuilder("<?xml version='1.0' encoding='utf-8'?>");
        //xml.Append("<tree id='0'>");
        //foreach (SysMenu m in m1)
        //{
        //    if (menus.Count(c => c.ParentCode == m.CODE) > 0)
        //    {
        //        xml.Append(string.Format("<item text='{0}' id='+{1}' {2} >", m.Name, m.CODE, _bal.ExistRoleMenu(roleId, m.CODE) ? "nocheckbox='1'" : "nocheckbox='1'"));
        //    }
        //    else
        //    {
        //        xml.Append(string.Format("<item text='{0}' id='+{1}' {2} >", m.Name, m.CODE, _bal.ExistRoleMenu(roleId, m.CODE) ? "nocheckbox='1'" : "nocheckbox='1'"));
        //    }

        //    var m2 = _bal.ListAuthorizabledSubFuncs(m.CODE);
        //    foreach (SysMenu sm in m2)
        //    {

        //            xml.Append(string.Format("<item text='{0}' id='+{1}' {2} >", sm.Name, sm.CODE, _bal.ExistRoleMenu(roleId, sm.CODE) ? "checked='1'" : ""));

        //        xml.Append("</item>");
        //    }

        //    xml.Append("</item>");
        //}
        //xml.Append("</tree>");

        //return xml.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UpdateRolePerm(string[] items, string roleId)
    {
        _bal.UpdateRolePerm(items, roleId);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UpdatePassword(String oldPassword, String newPassword)
    {
        _bal.UpdatePassword(oldPassword, newPassword);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetBasBaseCode()
    {
        string res = "QZ" + "B";
        string strYear = DateTime.Today.Year.ToString();
        res += strYear.Substring(2);
        string strMonth = DateTime.Today.Month.ToString().PadLeft(2, '0');
        res += strMonth;
        string seq = PubHelper.GetHelper().GetNextID("SEQ_BASINFO_NO").ToString().PadLeft(4, '0');
        res += seq;
        return res;
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveBaseInfo(string Code)
    {
        _bal.RemoveBaseInfo(Code);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string ListBaseInfo()
    {
        IList<BasBase> objs = _bal.FindBaseInfo("", "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        foreach (BasBase o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.CODE));
            sb.Append(string.Format("<cell>{0}</cell>", o.NAME));
            sb.Append(string.Format("<cell>{0}</cell>", o.SubCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();

    }

    //[WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    //public string QueryBaseInfo(string code, string name)
    //{
    //    IList<BasBase> objs = _bal.FindBaseInfo(code, name);
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
    //    sb.Append("<rows>");
    //    foreach (BasBase o in objs)
    //    {
    //        sb.Append(string.Format("<row id='{0}'>", o.ID));
    //        sb.Append(string.Format("<cell>{0}</cell>", o.CODE));
    //        sb.Append(string.Format("<cell>{0}</cell>", o.NAME));
    //        sb.Append(string.Format("<cell>{0}</cell>", o.SubCode));
    //        sb.Append(string.Format("<cell>{0}</cell>", o.SubName));
    //        sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
    //        sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
    //        sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
    //        //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
    //        sb.Append("</row>");
    //    }
    //    sb.Append("</rows>");
    //    return sb.ToString();

    //}
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryBaseInfo()
    { 
        string code = Context.Request.Form["baseName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasBase> baseInfo = _bal.FindBaseInfo(code, "");
        List<BasBase> bs = new List<BasBase>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (BasBase bb in baseInfo)
        {
            if (j > istart && j < iend)
            {
                BasBase bbtemp = new BasBase();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null & baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));         
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveSequence(BasSequence bseq)
    {
        _bal.SaveBasSequence(bseq);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveSequence(string ID)
    {
        _bal.RemoveBasSequence(ID);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBasSequence()
    {
        string seqName = Context.Request.Form["seqName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasSequence> objs = _bal.FindBasSequence(seqName);
        List<BasSequence> bs = new List<BasSequence>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (BasSequence bb in objs)
        {
            if (j > istart && j < iend)
            {
                BasSequence bbtemp = new BasSequence();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bb.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)        
            map.Add("total", objs.Count);
            map.Add("rows", objs);
        
        Context.Response.Write(JsonConvert.SerializeObject(map)); 
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveBasCode(BasCode bcode)
    {
        
        _bal.SaveBasCode(bcode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveBasCode(string ID)
    {
        _bal.RemoveBasCode(ID);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LisBasCode()
    {        
        string codename = Context.Request.Form["codeName"];
        IList<BasCode> objs = _bal.FindBasCode("", codename);       
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", objs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindSeqNo()
    {
        return _bal.ListBindSeqNo();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindCustName()
    {
        return _bal.ListBindCustName();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveOrderInfo(OrderDetail obj)
    {
       return _bal.SaveOrderInfo(obj);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetOrderNoCode()
    {
        string res = "QZ" + "O";
        string strYear = DateTime.Today.Year.ToString();
        res += strYear.Substring(2);
        string strMonth = DateTime.Today.Month.ToString().PadLeft(2, '0');
        res += strMonth;
        string seq = PubHelper.GetHelper().GetNextID("SEQ_ORDER_NO").ToString().PadLeft(4, '0');
        res += seq;
        return res;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveOrderInfo(string ID)
    {
        _bal.RemoveOrderInfo(ID);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryOrderInfo(string orderno, string parsdrawingno)
    {
        IList<OrderDetail> objs = _bal.FindOrderInfo(orderno, parsdrawingno);
        
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");

        foreach (OrderDetail o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
            sb.Append(string.Format("<cell>{0}</cell>", o.CONTRACT));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
            sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.OutDate));
            //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryProductNameByOrder(string orderno,string drawingno)
    {
        IList<OrderDetail> objs = _bal.FindOrderInfoForCreateWorkOrder(orderno, drawingno);
        if(objs!=null)
        {
            return objs[0].ProductName+"^"+objs[0].ProductCode+"^"+objs[0].CustName+"^"+objs[0].BatchNumber+"^"+objs[0].OrderQuantity+"^"+objs[0].OutDate;
        }
        
        return "";
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryOrderStatusInfo(string orderno, string parsdrawingno)
    {
        IList<OrderDetail> objs = _bal.FindOrderInfo(orderno, parsdrawingno);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (OrderDetail o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
            sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
           
            switch(o.STATUS)
            {
                case "0":
                    strstatus = "创建";
                    break;
                case "1":
                    strstatus = "发布";
                    break;
                case "2":
                    strstatus = "发货通知";
                    break;
                case "3":
                    strstatus = "关闭";
                    break;
                default:
                    strstatus = "状态异常";
                    break;
            }
            sb.Append(string.Format("<cell>{0}</cell>", strstatus));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.OutDate));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    /// <summary>
    /// 得到单据编号
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetInlistNO()
    {
        string res = "QZ" + "D";
        string strYear = DateTime.Today.Year.ToString();
        res += strYear.Substring(2);
        //string strMonth = DateTime.Today.Month.ToString().PadLeft(2, '0');
        //res += strMonth;
        string seq = PubHelper.GetHelper().GetNextID("SEQ_INLIST_NO").ToString().PadLeft(6, '0');
        res += seq;
        return res;
    }
    /// <summary>
    /// 得到来料条码
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetMSN()
    {
        IList<BasCode> bcs = _bal.FindBasCode("", "来料条码");
        if (bcs != null && bcs.Count > 0)
        {
            string res = bcs[0].PREFIX;// "QZ" + "M";
            string strYear = DateTime.Today.Year.ToString();
            res += strYear.Substring(2);
            string strMonth = DateTime.Today.Month.ToString().PadLeft(2, '0');
            res += strMonth;
            //string strDay = DateTime.Today.Day.ToString().PadLeft(2, '0');
            //res += strDay;
            string seq = PubHelper.GetHelper().GetNextID("SEQ_MSN_NO").ToString().PadLeft(6, '0');
            res += seq;
            return res;
        }
        else
        {
            return string.Empty;
        }
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveInWareHouseInfo(MaterialStock obj)
    {
        int qty = (int)obj.BasQty;
        string res = string.Empty;

        for (int i = 0; i < qty; i++)
        {
            obj.MSN = GetMSN();
            obj.BasQty = 1;
            _bal.SaveInWareHouseInfo(obj);
            if (string.IsNullOrEmpty(res))
            {
                res = obj.MSN;
            }
            else
            {
                res += "," + obj.MSN;
            }
        }
        if (_bal.SaveInWareHouseInfo(obj) == "ERR")
        {
            return "ERR";
        }
        return res;
        //int qty =(int) obj.BasQty;
        //string res = string.Empty;
        //for(int i=0;i<qty;i++)
        //{
        //    obj.MSN = GetMSN();
        //    obj.BasQty = 1;
        //    _bal.SaveInWareHouseInfo(obj);
        //    if(string.IsNullOrEmpty(res))
        //    {
        //        res = obj.MSN;
        //    }
        //    else
        //    {
        //        res += "," + obj.MSN;
        //    }
        //}
        //return res;

    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemovInWareHouseInfo(string msn)
    {
        _bal.RemoveInWareHouseInfo(msn);
    }
    
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindReceiveHouse()
    {
        return _bal.ListBindReceiveHouse();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindUnit()
    {
        return _bal.ListBindUnit();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryInWareHouseInfo(string msn, string materialno)
    {
        IList<MaterialStock> objs = _bal.FindInWareHouseInfo(msn, materialno);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (MaterialStock o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialName));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
            IList<BasBase> ibb = _bal.FindBaseBySubCode(o.StockHouse);

            sb.Append(string.Format("<cell>{0}</cell>", ibb[0].SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            sb.Append(string.Format("<cell>{0}</cell>", o.UNIT));
            sb.Append(string.Format("<cell>{0}</cell>", o.BasQty));
            //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
   
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindWorkOrder(string status)
    {
        return _bal.ListBindWorkOrder(status);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryBackupInfo(string workorder)
    {
        IList<MaterialStock> objs = _bal.QueryBackupInfo(workorder);
        if(objs==null)
        {
            return string.Empty;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (MaterialStock o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialName));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
            IList<BasBase> ibb = _bal.FindBaseBySubCode(o.StockHouse);

            sb.Append(string.Format("<cell>{0}</cell>", ibb[0].SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            sb.Append(string.Format("<cell>{0}</cell>", o.UNIT));
            sb.Append(string.Format("<cell>{0}</cell>", o.BasQty));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryBackupWOInfo(string workorder)
    {
        WorkOrder wo = new WorkOrder();
        wo.STATUS = "1";
        wo.WO = workorder;
        IList<WorkOrder> wos = _bal.FindWorkOrderInfo(wo);
        if (wos == null)
        {
            return string.Empty;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        foreach (WorkOrder o in wos)
        {
            //只显示没有备完料的工单信息
            if (o.MaterialQty == null || o.MaterialQty != o.PlanQuantity)
            {
                sb.Append(string.Format("<row id='{0}'>", o.WO));
                sb.Append(string.Format("<cell>{0}</cell>", o.WO));
                sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));//当前是零件图号为物料名称
                IList<MaterialStock> objs = _bal.QueryBackupInfo(o.WO);
                if(objs!=null&&objs.Count>0)
                {
                    sb.Append(string.Format("<cell>{0}</cell>", objs[0].CustName));
                    IList<BasBase> ibb = _bal.FindBaseBySubCode(objs[0].StockHouse);
                    sb.Append(string.Format("<cell>{0}</cell>", ibb[0].SubName));
                }
                else
                {
                    sb.Append(string.Format("<cell>{0}</cell>", ""));
                    sb.Append(string.Format("<cell>{0}</cell>",""));
                }                
                sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", objs.Count));
                sb.Append(string.Format("<cell>{0}</cell>", o.MaterialQty));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveOutWareHouseInfo(MaterialStockHistory obj)
    { 
         return _bal.SaveOutWareHouseInfo(obj);  
    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveReSendMaterialInfo(MaterialStockHistory obj)
    {
        return _bal.SaveReSendMaterialInfo(obj);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveReturnMaterialInfo(string MSN)
    {
        _bal.SaveReturnMaterialInfo(MSN);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryOutHistoryInfo(string workorder,string status)
    {
        IList<MaterialStockHistory> objs = _bal.QueryOutHistoryInfo(workorder, status);
        if(objs==null)
        {
            return string.Empty;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (MaterialStockHistory o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialName));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
            IList<BasBase> ibb = _bal.FindBaseBySubCode(o.StockHouse);
            string strStockHouse = string.Empty;
            if(ibb!=null)
            {
                strStockHouse = ibb[0].SubName;
            }
            sb.Append(string.Format("<cell>{0}</cell>", strStockHouse));
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            SysUser su = _bal.FindUserByCode(o.MaterialHandler);
            string struser = string.Empty;
            if(su!=null)
            {
                struser = su.UserName;
            }
            sb.Append(string.Format("<cell>{0}</cell>", struser));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveLabelReprint(ReprintLog obj)
    {
        _bal.SaveLabelReprint(obj);
        return "OK";
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryReprintLog()
    {
        string sn = Context.Request.Form["sn"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<ReprintLog> objs = _bal.FindReprintLogInfo(sn);
        List<ReprintLog> bs = new List<ReprintLog>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (ReprintLog bb in objs)
        {
            if (j > istart && j < iend)
            {
                ReprintLog bbtemp = new ReprintLog();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

        //IList<ReprintLog> objs = _bal.FindReprintLogInfo(sn);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (ReprintLog o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.LabelType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.SN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));             
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));      
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindProductName()
    {
        return _bal.ListBindProductName();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SavePartsDrawing(PartsdrawingCode obj)
    {
        return _bal.SavePartsDrawing(obj);        
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemovePartsdrawingNo(string id)
    {
        _bal.RemovePartsdrawingNo(id);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryPartsDrawingInfo(string partsdrawingno)
    {
        IList<PartsdrawingCode> objs = _bal.FindPartsdrawingInfo(partsdrawingno);
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (PartsdrawingCode o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.PartsCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
            sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.QualityCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.AskQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.AskDate));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QuerybatchQtyByPartsDrawing(string partsdrawingno)
    {
        string[] res = partsdrawingno.Split('|');
        IList<PartsdrawingCode> objs = _bal.FindPartsdrawingInfo(res[0]);
        if(objs!=null)
        {
            foreach (PartsdrawingCode pc in objs)
            {
                if (pc.BatchNumber == res[1])
                {
                    return pc.BatchNumber + "," + pc.AskQuantity;
                }
            }
        }
         
        return ",";
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetWorkOrderCode()
    {
        string res = "QZ" + "W";
        string strYear = DateTime.Today.Year.ToString();
        res += strYear.Substring(2);
        string strMonth = DateTime.Today.Month.ToString().PadLeft(2, '0');
        res += strMonth;
        string strDay = DateTime.Today.Day.ToString().PadLeft(2, '0');
        res += strDay;
        string seq = PubHelper.GetHelper().GetNextID("SEQ_WORKORDER_NO").ToString().PadLeft(4, '0');
        res += seq;
        return res;
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveWorkOrderInfo(WorkOrderDetails obj)
    {
        if(string.IsNullOrEmpty(obj.WO))
        {
            return "工单单号不能为空";
        }
        if(string.IsNullOrEmpty(obj.OrderNumber))
        {
            return "订单单号不能为空";
        }
        if(string.IsNullOrEmpty(obj.PartsdrawingCode))
        {
            return "零件图号不能为空";
        }
        
        if(string.IsNullOrEmpty(obj.ProductName))
        {
            return "产品名称不能为空";
        }
        if(string.IsNullOrEmpty(obj.BatchNumber))
        {
            return "批次不能为空";
        }
        //if (string.IsNullOrEmpty(obj.QualityCode))
        //{
        //    return "质量编号不能为空";
        //}
        if (DateTime.Compare((DateTime)obj.StartTime,(DateTime)obj.EndTime)>0)
        {
            return "开始时间不能大于结束时间";
        }
        if (DateTime.Compare((DateTime)obj.StartTime, (DateTime)obj.CheckTime) > 0)
        {
            return "开始时间不能大于检验时间";
        }
        if (DateTime.Compare((DateTime)obj.CheckTime, (DateTime)obj.InstockTime) > 0)
        {
            return "检验时间不能大于入库时间";
        }
        obj.STATUS = "0";//状态（0：创建；1：运行；2：暂停；3：关闭；）
        obj.StatusMemo = "创建";
        obj.MEMO = "创建";
        obj.CreatedDate = DateTime.Now;
        _bal.SaveWorkOrderInfo(obj);
        return "OK";
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveWorkOrderInfo(string id)
    {
        _bal.RemoveWorkOrder(id);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindOrderNo()
    {
        return _bal.ListBindOrderNo();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBindMachineType()
    {
        // return _bal.ListBindMachineType();
        TextValueInfo[] tvi = _bal.ListBindMachineType();
        string res = "[";
        for (int i = 0; i < tvi.Length; i++)
        {
            if (i == 0)
            {
                res += "{\"id\":\"" + tvi[0].Text + "\",\"text\":\"" + tvi[0].Text + "\"}";
            }
            else
            {
                res += ",{\"id\":\"" + tvi[i].Text + "\",\"text\":\"" + tvi[i].Text + "\"}";
            }
        }
        res += "]";
        Context.Response.Write(res);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindPartsDrawingNo()
    {
        return _bal.ListBindPartsDrawingCode();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindPartsDrawingCodeBindOrder()
    {
        return _bal.ListBindPartsDrawingCodeBindOrder();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindPartsDrawingNobyOrder(string order)
    {
        return _bal.ListBindPartsDrawingCodebyOrder(order);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindOrderbyPartsDrawingCode(string PartsDrawingCode)
    {
        return _bal.ListBindOrderbyPartsDrawingCode(PartsDrawingCode);
    }

    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWorkOrderInfo()
    {
        string workorder = Context.Request.Form["workorder"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(workorder);
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrder bbtemp = new WorkOrder();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<WorkOrder> objs = _bal.FindWorkOrderInfo(workorder);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (WorkOrder o in objs)
        //{
        //    if (o.STATUS == "1")
        //    {
        //        sb.Append(string.Format("<row id='{0}' bgColor='green'>", o.WO));
        //    }
        //    else if (o.STATUS == "2")
        //    {
        //        sb.Append(string.Format("<row id='{0}' bgColor='yellow'>", o.WO));
        //    }

        //    else if (o.STATUS == "3")
        //    {
        //        sb.Append(string.Format("<row id='{0}' bgColor='grey'>", o.WO));
        //    }
        //    else
        //    {
        //        sb.Append(string.Format("<row id='{0}' >", o.WO));
        //    } 
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CheckTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.InTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] QueryMachinesInfo(string code)
    {
        return _bal.ListBindMachines(code);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBindMachineInfo()
    {
        TextValueInfo[] t1 = _bal.ListBindMachines("QZB1704000901");//铣床
        TextValueInfo[] t2 = _bal.ListBindMachines("QZB1704000902");//车床
        TextValueInfo[] t3 = _bal.ListBindMachines("QZB1704000903");//车铣
        TextValueInfo[] t = new TextValueInfo[t1.Length + t2.Length + t3.Length];
        int i = 0;
        for (i = 0; i < t1.Length; i++)
        {
            t[i] = new TextValueInfo();
            t[i].Value = t1[i].Value;
            t[i].Text = t1[i].Text;
        }
        for (var j=0; j < t2.Length; j++)
        {
            t[i] = new TextValueInfo();
            t[i].Value = t2[j].Value;
            t[i].Text = t2[j].Text;
            i++;
        }
        for (var k=0; k < t3.Length; k++)
        {
            t[i] = new TextValueInfo();
            t[i].Value = t3[k].Value;
            t[i].Text = t3[k].Text;
            i++;
        }
        //return t;
        string res = "[";
        for (i = 0; i < t.Length; i++)
        {
            if (i == 0)
            {
                res += "{\"id\":\"" + t[0].Value + "\",\"text\":\"" + t[0].Text + "\"}";
            }
            else
            {
                res += ",{\"id\":\"" + t[i].Value + "\",\"text\":\"" + t[i].Text + "\"}";
            }
        }
        res += "]";
        Context.Response.Write(res);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UpdateWorkOrderInfo(string id, string status)
    {
          _bal.UpdateWorkOrderInfo(id, status);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWorkOrderAssign()
    {
        string status = Context.Request.Form["status"];
        string isAssign = Context.Request.Form["isAssign"];
        string drawingcode = Context.Request.Form["drawingcode"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        bool bassign = isAssign == "true" ? true : false;
        IList<WorkOrder> objs = _bal.FindWorkOrderByStatus(status, bassign, drawingcode);
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                if (bassign)
                {
                    if (!string.IsNullOrEmpty(bb.WORKER))
                    {
                        WorkOrder bbtemp = new WorkOrder();
                        bbtemp = bb;
                        bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                        bs.Add(bbtemp);
                        continue;
                    }
                }
                WorkOrder bbtemp2 = new WorkOrder();
                bbtemp2 = bb;
                bs.Add(bbtemp2);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<WorkOrder> objs = _bal.FindWorkOrderByStatus(status, isAssign);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (WorkOrder o in objs)
        //{
        //    if(isAssign)
        //    {
        //       if(!string.IsNullOrEmpty(o.WORKER))
        //        {
        //            sb.Append(string.Format("<row id='{0}'>", o.WO));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.MachineType));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.CheckTime));
        //            sb.Append(string.Format("<cell>{0}</cell>", o.InTime));
        //            sb.Append("</row>");
        //            continue;
        //        }
        //    }
        //    sb.Append(string.Format("<row id='{0}'>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CheckTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.InTime));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBindUserByOperators()
    {
       // return _bal.ListBindUserByOperators();

        TextValueInfo[] tvi = _bal.ListBindUserByOperators();
        string res = "[";
        for (int i = 0; i < tvi.Length; i++)
        {
            if (i == 0)
            {
                res += "{\"id\":\"" + tvi[0].Value + "\",\"text\":\"" + tvi[0].Text + "\"}";
            }
            else
            {
                res += ",{\"id\":\"" + tvi[i].Value + "\",\"text\":\"" + tvi[i].Text + "\"}";
            }
        }          
        res += "]";
        Context.Response.Write(res);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveWorkOrderAssign(string workorder, string operater, string operatercode,string station)
    {
        _bal.SaveWorkOrderAssign(workorder, operater, operatercode,station);
        return "OK";
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPSNByMSN(string MSN)
    {
       string psn = _bal.GetPSNByMSN(MSN);
        return psn;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryPSN()
    {
        string sn = Context.Request.Form["sn"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TrackingWip> objs = _bal.FindPSNPrint(sn);
        List<TrackingWip> bs = new List<TrackingWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TrackingWip bb in objs)
        {
            if (j > istart && j < iend)
            {
                TrackingWip bbtemp = new TrackingWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<TrackingWip> objs = _bal.FindPSNPrint(psn);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //foreach (TrackingWip o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));           
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    /// <summary>
    /// 查询生产任务，根据作业员工号查询
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryLatheTask()
    {
        string worker = Context.Request.Form["worker"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        if (string.IsNullOrEmpty(worker))
        {
            worker = this._userInfo.UserCode;
        }
        IList<WorkOrderDetails> objs = _bal.FindWorkOrderTask(worker);
        List<WorkOrderDetails> bs = new List<WorkOrderDetails>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrderDetails bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrderDetails bbtemp = new WorkOrderDetails();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<WorkOrder> objs = _bal.FindLatheTask(worker);
        //List<WorkOrder> bs = new List<WorkOrder>();
        //int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        //int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        //int j = 1;
        //foreach (WorkOrder bb in objs)
        //{
        //    if (j > istart && j < iend)
        //    {
        //        WorkOrder bbtemp = new WorkOrder();
        //        bbtemp = bb;
        //        bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
        //        bs.Add(bbtemp);
        //    }
        //    j++;
        //}
        //Dictionary<String, Object> map = new Dictionary<String, Object>();
        //if (objs != null & objs.Count > 0)
        //    map.Add("total", objs.Count);
        //map.Add("rows", bs);
        //Context.Response.Write(JsonConvert.SerializeObject(map)); 
    }
    /// <summary>
    /// 查询生产记录，根据作业员工号查询
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryLatheTaskHistory()
    {
        string worker = Context.Request.Form["worker"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TrackingHistory> objs = _bal.FindLatheTaskHistory(worker);
        List<TrackingHistory> bs = new List<TrackingHistory>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TrackingHistory bb in objs)
        {
            if (j > istart && j < iend)
            {
                TrackingHistory bbtemp = new TrackingHistory();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<TrackingHistory> objs = _bal.FindLatheTaskHistory(worker);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (TrackingHistory o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StationName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate));            
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] LoadNoScanData()
    {
        TrackingTemp tt = _bal.LoadTrackingTemp();
        string[] res = null;
        if(tt!=null)
        {
            res = new string[5];
            res[0] = tt.ID;
            res[1] = tt.PSN;
            res[2] = tt.MSN;
            if (!string.IsNullOrEmpty(tt.STATUS))
            {
                res[3] = tt.STATUS.Trim().Replace("P", "合格");
                res[3].Replace("F", "不合格");//=="P"?"pass":"fail";
            }
            else
            {
                res[3] = "";
            }
            res[4] = tt.STEP;

        }
        return res;
    }

    /// <summary>
    /// 处理车铣报工的扫描数据
    /// </summary>
    /// <param name="inputdata"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DealwithScanData(string inputdata ,string ID)
    {
        TrackingTemp tt = null;
        if (!string.IsNullOrEmpty(inputdata)&&(inputdata.Length==13||inputdata.Length==15))
        {
            inputdata = inputdata.ToUpper().Trim();
        }
        string strID = string.Empty;
        IList<TrackingWip> gtws = null;
        if (!string.IsNullOrEmpty(ID))
        {
             tt = _bal.FindTrackingTemp(ID);
            gtws = _bal.FindTrackingWip(tt.MSN);
        }
        switch(inputdata)
        {
            //case "snquery":
            //    Server.Transfer("..//Pages//ProductionManage//SNTracking.aspx");
            //    break;
            case "cancel":
                _bal.RemoveTrackingTemp(ID);
                break;
            case "fail":
                //if(tt.STEP!="psn")
                //{
                    //判断成品条码是否扫完
                    int totalpsn = 0;
                    int scanpsn = 0;
                    if(gtws != null)
                    {
                        totalpsn = gtws.Count;
                    }
                    
                    if(!string.IsNullOrEmpty(tt.PSN))
                    {
                        string[] strpsns = tt.PSN.Split(',');
                        scanpsn = strpsns.Length;
                    }

                    if (totalpsn != scanpsn)
                    {
                        throw new Exception("请先扫描产品条码！");
                    }
              
                //判断扫描结果是否扫完
                if (!string.IsNullOrEmpty(tt.STATUS))
                {
                    string[] strpsns = tt.STATUS.Split(',');
                    scanpsn = strpsns.Length;

                    if (totalpsn == scanpsn)
                    {
                        throw new Exception("产品结果扫描完毕，请直接收工！");
                    }
                } 
                if (string.IsNullOrEmpty(tt.STATUS))
                {
                    tt.STATUS = "F";
                }
               else
                {
                    tt.STATUS += ",F";
                }
                tt.STEP = "fail";
                _bal.SaveTrackingTemp(tt);
                break;
            case "pass":
                //if (tt.STEP != "psn")
                //{
                    //判断成品条码是否扫完
                    totalpsn = 0;
                    scanpsn = 0;
                    if (gtws != null)
                    {
                        totalpsn = gtws.Count;
                    }

                    if (!string.IsNullOrEmpty(tt.PSN))
                    {
                        string[] strpsns = tt.PSN.Split(',');
                        scanpsn = strpsns.Length;
                    }

                    if (totalpsn != scanpsn)
                    {
                        throw new Exception("该产品时一块料出"+totalpsn+"件成品，请扫描完所有的产品条码！");
                    }
                // }
                //判断扫描结果是否扫完
                if (!string.IsNullOrEmpty(tt.STATUS))
                {
                    string[] strpsns = tt.STATUS.Split(',');
                    scanpsn = strpsns.Length;

                    if (totalpsn == scanpsn)
                    {
                        throw new Exception("产品结果扫描完毕，请直接收工！");
                    }
                } 
                if (string.IsNullOrEmpty(tt.STATUS))
                {
                    tt.STATUS = "P";
                }
                else
                {
                    tt.STATUS += ",P";
                } 
                tt.STEP = "pass";
                _bal.SaveTrackingTemp(tt);
                break;
            case "start":
                tt.CreatedDate = DateTime.Now;
                tt.STEP = "start";
                _bal.SaveTrackingTemp(tt);
                break;
            case "pause":
                tt.UpdatedDate = DateTime.Now;
                if (string.IsNullOrEmpty(tt.TaskTime))
                {
                    tt.TaskTime = (tt.UpdatedDate - tt.CreatedDate).ToString();
                }
                else
                {
                    //计算有暂停情况的工时
                    TimeSpan tempPauseTaskTime = TimeSpan.ParseExact(tt.TaskTime, @"hh\:mm\:ss\.fffffff", null);
                    TimeSpan tempNewTaskTime = (TimeSpan)(tt.UpdatedDate - tt.CreatedDate);
                    tt.TaskTime = (tempPauseTaskTime + tempNewTaskTime).ToString();
                }
                tt.STEP = "pause";
                _bal.SaveTrackingTemp(tt);
                break;
            case "finish":
                if (tt.STEP != "pass"&tt.STEP != "fail")
                {
                    throw new Exception("请先扫描合格或不合格！");
                }
                else
                {
                    //判断成品条码是否扫完
                    totalpsn = 0;
                    scanpsn = 0;
                    if (gtws != null)
                    {
                        totalpsn = gtws.Count;
                    }

                    if (!string.IsNullOrEmpty(tt.PSN))
                    {
                        string[] strpsns = tt.PSN.Split(',');
                        scanpsn = strpsns.Length;
                    }

                    if (totalpsn != scanpsn)
                    {
                        throw new Exception("产品条码未扫描完毕，请继续扫描产品条码！");
                    }
                    //判断扫描结果是否扫完
                    if (!string.IsNullOrEmpty(tt.STATUS))
                    {
                        string[] strpsns = tt.STATUS.Split(',');
                        scanpsn = strpsns.Length;
                    }
                     

                    if (totalpsn != scanpsn)
                    {
                        throw new Exception("产品结果未扫描完毕，请继续扫描产品结果！");
                    }
                }
                tt.UpdatedDate = DateTime.Now;
                //计算工时
                if (string.IsNullOrEmpty(tt.TaskTime))
                {
                    tt.TaskTime = (tt.UpdatedDate - tt.CreatedDate).ToString();
                }
                else
                {
                    //计算有暂停情况的工时
                    TimeSpan tempPauseTaskTime = TimeSpan.ParseExact(tt.TaskTime, @"hh\:mm\:ss\.fffffff", null);
                    TimeSpan tempNewTaskTime = (TimeSpan)(tt.UpdatedDate - tt.CreatedDate);
                    tt.TaskTime = (tempPauseTaskTime + tempNewTaskTime).ToString();
                }
                string[] psns = tt.PSN.Split(',');
                string[] statuss = tt.STATUS.Split(',');
                for (int i = 0; i < psns.Length; i++)
                {
                    IList<TrackingWip> tw = _bal.FindTrackingWip(psns[i]);
                    if (tw.Count > 0)
                    {
                        tw[0].StationName = "CHEXI";
                        tw[0].STATUS = statuss[i];// tt.STATUS;
                        tw[0].InStatioonTime = tt.CreatedDate;
                        tw[0].OutStationTime = tt.UpdatedDate;
                        tw[0].TaskTime = tt.TaskTime;
                        tw[0].UpdatedDate = DateTime.Now;
                        tw[0].UpdatedBy = tt.UpdatedBy;
                    }
                    _bal.SaveTrackingWip(tw[0]);
                    TrackingHistory th = new TrackingHistory();
                    th.PSN = psns[i];
                    th.MSN = tt.MSN;
                    th.WorkOrder = tt.WorkOrder;
                    th.PartsdrawingCode = tt.PartsdrawingCode;
                    th.PartsName = tt.PartsName;
                    th.PartsCode = tt.PartsCode;
                    th.BatchNumber = tt.BatchNumber;
                    th.StationName = "CHEXI";
                    th.QUANTITY = 1;
                    th.STATUS = statuss[i];
                    th.InStationTime = tt.CreatedDate;
                    th.OutStationTime = tt.UpdatedDate;
                    th.TaskTime = tt.TaskTime;
                    th.MachineName = tt.MachineName;
                    th.MachineType = tt.MachineType;
                    th.CreatedDate = DateTime.Now;
                    th.UpdatedBy = tt.UpdatedBy;
                    _bal.SaveTrackingHistory(th);
                    //工单加1
                    IList<WorkOrderDetails> wos = _bal.FindWorkOrderDetailsInfo(tw[0].WorkOrder);
                    wos[0].QUANTITY = _bal.FindWorkOrderCount(tw[0].WorkOrder, "PRINT");
                    _bal.SaveWorkOrderInfo(wos[0]);
                                   
                    //如果不合格，直接入待处理品表
                    if (tt.STATUS == "F")
                    {
                        UnsurenessProduct up = new UnsurenessProduct();
                        up.PSN = th.PSN;
                        up.MSN = th.MSN;
                        up.WorkOrder = th.WorkOrder;
                        up.STATUS = "0";
                        up.StationName = "CHEXI";
                        up.MEMO = "待处理";
                        up.QUANTITY = th.QUANTITY;
                        up.PartsdrawingCode = th.PartsdrawingCode;
                        up.ProductName = th.PartsName;
                        up.BatchNumber = th.BatchNumber;
                        up.CreatedDate = DateTime.Now;
                        _bal.SaveUnsurenessProduct(up);

                        //入完待处理品后，再次保存历史记录
                        th.ID = string.Empty;
                        th.StationName = "UnsurenessIn";
                        th.STATUS = "P";
                        th.CreatedDate = DateTime.Now;
                        _bal.SaveTrackingHistory(th);
                    }

                    _bal.RemoveTrackingTemp(tt.ID);
                    //保存到实时统计表
                    RealtimeStatistics rs = new RealtimeStatistics();
                    rs.PSN = th.PSN;
                    rs.MSN = th.MSN;
                    rs.WorkOrder = th.WorkOrder;
                    rs.StationName = th.StationName;
                    rs.MachineType = th.MachineType;
                    rs.MachineName = th.MachineName;
                    rs.STATUS = th.STATUS;
                    rs.QUANTITY = th.QUANTITY;
                    rs.OPERATOR = th.UpdatedBy;
                    if (wos.Count > 0)
                    {
                        rs.OrderNumber = wos[0].OrderNumber;
                    }
                    rs.ProductName = th.PartsName;
                    rs.ProductCode = th.PartsCode;
                    IList<PartsdrawingCode> pc = _bal.FindPartsdrawingInfo(th.PartsdrawingCode);
                    if (pc.Count > 0)
                    {
                        rs.CustName = pc[0].CustName;
                    }
                    rs.PartsdrawingCode = th.PartsdrawingCode;
                    _bal.SaveRealtimeStatistics(rs);
                }
                break;
            default:
                //表示扫描的是来料条码，因为来料条码长度为13位
                if(inputdata.Length==13)
                {
                    inputdata = inputdata.ToUpper();
                    IList<TrackingWip> tws = _bal.FindTrackingWip(inputdata);
                    //检查来料条码有没有打印出成品条码
                    if(tws.Count>0)
                    {
                        //IList<WorkOrder> wo = _bal.FindWorkOrderInfo(tws[0].WorkOrder);
                        IList<WorkOrderDetails> wo = _bal.FindWorkOrderDetailsInfo(tws[0].WorkOrder,tws[0].NextStationId);
                        //检查该料是否本床子的任务
                        //if(wo[0].WORKER!=_userInfo.UserCode)
                        if (wo[0].WORKER.IndexOf(_userInfo.UserCode)==-1)
                        {
                            throw new Exception("该料号非本人任务，应为" + wo[0].WorkerName + "的任务！");
                        }
                        TrackingTemp ttemp = new TrackingTemp();
                        ttemp.MSN = tws[0].MSN;
                        ttemp.WorkOrder = tws[0].WorkOrder;
                        ttemp.PartsdrawingCode = tws[0].PartsdrawingCode;
                        ttemp.PartsName = tws[0].PartsName;
                        ttemp.PartsCode = tws[0].PartsCode;
                        ttemp.BatchNumber = tws[0].BatchNumber;
                        ttemp.QUANTITY = tws[0].QUANTITY.ToString();
                        //ttemp.StationName = "CHEXI";
                        ttemp.StationName = tws[0].NextStation;
                        ttemp.StationId = tws[0].NextStationId;
                        ttemp.STEP = "msn";
                        if (wo.Count > 0)
                        {
                            ttemp.MachineName = wo[0].MachineName;
                            ttemp.MachineType = wo[0].MachineType;
                        }
                        strID = _bal.SaveTrackingTemp(ttemp);
                    }
                    else
                    {
                        throw new Exception("该料号未打印产生产品条码，请先打印产品条码");
                    }
                }
                else if(inputdata.Length==15)//表示扫描的是产品条码，因为产品条码是15位
                {
                    inputdata = inputdata.ToUpper();
                    if (tt.STEP != "start")
                    {
                        if (string.IsNullOrEmpty(tt.PSN))
                        {
                            throw new Exception("请先扫描开工！");
                        }
                        else
                        {
                            //判断成品条码是否扫完
                            totalpsn = 0;
                            scanpsn = 0;
                            if (gtws != null)
                            {
                                totalpsn = gtws.Count;
                            }

                            if (!string.IsNullOrEmpty(tt.PSN))
                            {
                                string[] strpsns = tt.PSN.Split(',');
                                scanpsn = strpsns.Length;
                            }

                            if (totalpsn == scanpsn)
                            {
                                throw new Exception("产品条码已扫描完毕，请继续扫产品结果！");
                            }
                            
                        }
                    }
                    //验证产品条码是否和来料条码匹配
                    IList<TrackingWip> tws = _bal.FindTrackingWip(inputdata);
                    //检查是否重复过本站
                    if (tws[0].StationName != "PRINT")
                    {
                        IList<UnsurenessProduct> up = _bal.FindUnsurenessProduct(tws[0].PSN);
                        if (up.Count > 0)
                        {
                            if (up[0].STATUS != "1" && up[0].STATUS != "4")//表明待处理品的处理结果不是返工类型，不能过本站
                            {
                                throw new Exception("该产品已过本站，且非返工产品，不能重复过站！");
                            }
                        }
                        else
                        {
                            throw new Exception("该产品已过本站，不能重复过站！");
                        }
                    }
                    //验证是否待处理品未审核
                    if (!_bal.CheckUnSurenessOut(inputdata))
                    {
                        throw new Exception("该产品待处理中未审核,请审核完后过本站");
                    }
                    TrackingTemp ttt = _bal.FindTrackingTemp(ID);
                    if (tws.Count > 0)
                    {
                        if(ttt.MSN==tws[0].MSN)
                        {
                            if (string.IsNullOrEmpty(ttt.PSN))
                            {
                                ttt.PSN = inputdata;
                            }
                            else
                            {
                                ttt.PSN += "," + inputdata;
                            }
                            ttt.STEP = "psn";
                            _bal.SaveTrackingTemp(ttt);
                        }
                        else
                        {
                            throw new Exception("条码不匹配,请重新扫描条码");
                        }
                    }
                    else
                    {
                        throw new Exception("无此条码,请重新扫描条码");
                    }
                   
                }
                break;
        }
        return strID;
    }

    /// <summary>
    /// 查询生产记录，根据作业员工号查询
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QuerySNTrackingInfo()
    {
        string sn = Context.Request.Form["sn"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TrackingHistory> objs = _bal.FindSNTrackingInfo(sn);
        List<TrackingHistory> bs = new List<TrackingHistory>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TrackingHistory bb in objs)
        {
            if (j > istart && j < iend)
            {
                TrackingHistory bbtemp = new TrackingHistory();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<TrackingHistory> objs = _bal.FindSNTrackingInfo(SN);
        //if (objs == null)
        //    return "";
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (TrackingHistory o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StationName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));            
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    /// <summary>
    /// 查询检验任务
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryQCTask()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrder> objs = _bal.FindQCTask();
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrder bbtemp = new WorkOrder();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<WorkOrder> objs = _bal.FindQCTask();
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (WorkOrder o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CheckTime));             
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    /// <summary>
    /// 查询检验记录
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryQCCheckHistory()
    { 
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TrackingHistory> objs = _bal.FindQCCheckHistory();
        List<TrackingHistory> bs = new List<TrackingHistory>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TrackingHistory bb in objs)
        {
            if (j > istart && j < iend)
            {
                TrackingHistory bbtemp = new TrackingHistory();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<TrackingHistory> objs = _bal.FindQCCheckHistory();
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (TrackingHistory o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.STATUS));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StationName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate));            
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    /// <summary>
    /// 处理质检员的扫描数据
    /// </summary>
    /// <param name="inputdata"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DealwithQCScanData(string inputdata, string ID)
    {
        TrackingTemp tt = null;
        if (string.IsNullOrEmpty(inputdata) && (inputdata.Length == 13 || inputdata.Length == 15))
        {
            inputdata = inputdata.ToUpper();
        }
        string strID = string.Empty;
        if (!string.IsNullOrEmpty(ID))
        {
            tt = _bal.FindTrackingTemp(ID);
        }
        switch (inputdata)
        {
            case "cancel":
                _bal.RemoveTrackingTemp(ID);
                break;
            case "fail":
                if(!string.IsNullOrEmpty(ID))
                {
                    throw new Exception("请进行下一步扫描!");
                }
                TrackingTemp ttempF = new TrackingTemp();
                ttempF.STEP = "fail";
                ttempF.STATUS = "F";
                strID = _bal.SaveTrackingTemp(ttempF); //保存到临时表
                break;
            case "pass":
                if (!string.IsNullOrEmpty(ID))
                {
                    throw new Exception("请进行下一步扫描!");
                }
                TrackingTemp ttempP = new TrackingTemp();//保存到临时表
                ttempP.STEP = "pass";
                ttempP.STATUS = "P";
                strID = _bal.SaveTrackingTemp(ttempP);
                break;
            default:
                if (inputdata.Length == 15)//表示扫描的是产品条码，因为产品条码是15位
                {
                    inputdata = inputdata.ToUpper();
                    if(tt==null)
                    {
                        throw new Exception("请先扫检验结果！");
                    }
                    if (tt.STEP != "pass"&& tt.STEP!="failcode")
                    {
                        throw new Exception("请先扫描不良代码！");
                    }
                    //更新trackingwip信息                                    
                    //验证是否待处理品未审核
                    if (!_bal.CheckUnSurenessOut(inputdata))
                    {
                        throw new Exception("该产品待处理中未审核,请审核完后过本站");
                    }
                    IList<TrackingWip> tw = _bal.FindTrackingWip(inputdata);
                    if (tw.Count > 0 )
                    {
                        if(tw[0].StationName == "PRINT")
                        {
                            throw new Exception("该产品没有报工，请先报工");
                        }
                        //如果是qc工站挂的，又经过待处理品流程再回到QC扫描的，让其通过
                        if (tw[0].STATUS=="F"&& tw[0].StationName != "QC")
                        {
                            throw new Exception("此产品上一站已失败,禁止过本站");
                        }
                        //检查是否流程已走完
                        if (tw[0].NextStation.Trim()!="end")
                        {
                            throw new Exception("此产品"+tw[0].NextStation+"工序未过,禁止过本站");
                        }
                        tw[0].StationName = "QC";
                        tw[0].StationId = "QC";
                        tw[0].NextStation = "INSTOCK";
                        tw[0].NextStationId = "INSTOCK";
                        tw[0].STATUS = tt.STATUS.Trim();
                        if(tt.STATUS=="F")
                        {
                            tw[0].NextStation = "";
                        }
                        tw[0].InStatioonTime = tt.CreatedDate;
                        tw[0].OutStationTime = DateTime.Now;                         
                        tw[0].UpdatedDate = DateTime.Now;
                        tw[0].UpdatedBy = tt.UpdatedBy;
                    }
                    else
                    {
                        throw new Exception("无此条码");
                    }
                    _bal.SaveTrackingWip(tw[0]);
                    //保存检查记录
                    TrackingHistory th = new TrackingHistory();
                    th.PSN = tw[0].PSN;
                    th.MSN = tw[0].MSN;
                    th.WorkOrder = tw[0].WorkOrder;
                    th.PartsdrawingCode = tw[0].PartsdrawingCode;
                    th.PartsName = tw[0].PartsName;
                    th.PartsCode = tw[0].PartsCode;
                    th.BatchNumber = tw[0].BatchNumber;
                    th.StationName = "QC";
                    th.StationId = "QC";
                    th.NextStation = tw[0].NextStation;
                    th.NextStationId = tw[0].NextStationId;
                    th.QUANTITY = 1;
                    th.STATUS = tt.STATUS.Trim();
                    if(tt.STATUS=="F")
                    {
                        th.EXCEPTION = tt.EXCEPTION.Trim();
                    }
                    th.InStationTime = tt.CreatedDate;
                    th.OutStationTime = DateTime.Now;
                    IList<WorkOrder> wo = _bal.FindWorkOrderInfo(tw[0].WorkOrder);
                    th.MachineName = wo[0].MachineName;
                    th.MachineType = wo[0].MachineType;
                    th.CreatedDate = DateTime.Now;
                    th.UpdatedBy = tt.UpdatedBy.Trim();
                    _bal.SaveTrackingHistory(th);
                    //如果不合格，直接入待处理品表
                    if(tt.STATUS=="F")
                    {
                        UnsurenessProduct up = new UnsurenessProduct();
                        up.PSN = th.PSN;
                        up.MSN = th.MSN;
                        up.WorkOrder = th.WorkOrder;
                        up.FailCode = th.EXCEPTION.Split('^')[0].ToString();
                        up.FailMemo = th.EXCEPTION.Split('^')[1].ToString();
                        up.STATUS = "0";
                        up.MEMO = "待处理";
                        up.StationName = "QC";
                        up.QUANTITY = th.QUANTITY;
                        up.PartsdrawingCode = th.PartsdrawingCode;
                        up.ProductName = th.PartsName;
                        up.BatchNumber = th.BatchNumber;
                        up.CreatedDate = DateTime.Now;
                        _bal.SaveUnsurenessProduct(up);

                        //入完待处理品后，再次保存历史记录
                        th.ID = string.Empty;
                        th.StationName = "UnsurenessIn"; 
                        th.STATUS = "P";
                        th.CreatedDate = DateTime.Now;                         
                        _bal.SaveTrackingHistory(th);
                    }
                    _bal.RemoveTrackingTemp(tt.ID.Trim());
                    //保存到实时统计表
                    RealtimeStatistics rs = new RealtimeStatistics();
                    rs.PSN = th.PSN;
                    rs.MSN = th.MSN;
                    rs.WorkOrder = th.WorkOrder;
                    rs.StationName = th.StationName;
                    rs.MachineType = th.MachineType;
                    rs.MachineName = th.MachineName;
                    rs.STATUS = th.STATUS;
                    rs.QUANTITY = th.QUANTITY;
                    rs.OPERATOR = th.UpdatedBy;
                    if (wo.Count > 0)
                    {
                        rs.OrderNumber = wo[0].OrderNumber;
                    }
                    rs.ProductName = th.PartsName;
                    rs.ProductCode = th.PartsCode;
                    IList<PartsdrawingCode> pc = _bal.FindPartsdrawingInfo(th.PartsdrawingCode);
                    if (pc.Count > 0)
                    {
                        rs.CustName = pc[0].CustName;
                    }
                    rs.PartsdrawingCode = th.PartsdrawingCode;
                    _bal.SaveRealtimeStatistics(rs);
                    break;
                }
                else//处理不良代码的情况
                {
                    if (tt.STEP!="fail")
                    {
                        throw new Exception("请进行不合格的扫描!");
                    }
                    IList<FailItems> fis = _bal.FindFailItems(inputdata,"","");
                    if (fis.Count > 0)
                    {
                        tt.EXCEPTION = fis[0].FailCode + "^" + fis[0].FailMemo;
                        tt.STEP = "failcode";
                        _bal.SaveTrackingTemp(tt);
                    }
                }
                 
                break;
        }
        return strID;
    }
    /// <summary>
    /// 查询待处理品记录
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryUnsurenessIn()
    {
        string psn = Context.Request.Form["psn"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<UnsurenessProduct> objs = _bal.FindUnsurenessProduct(psn);
        List<UnsurenessProduct> bs = new List<UnsurenessProduct>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (UnsurenessProduct bb in objs)
        {
            if (j > istart && j < iend)
            {
                UnsurenessProduct bbtemp = new UnsurenessProduct();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<UnsurenessProduct> objs = _bal.FindUnsurenessProduct(psn);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (UnsurenessProduct o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailMemo));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StationName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveUnsurenessIn(string psn, string failcode, string failmemo)
    {
        _bal.SaveUnsurenessProduct(psn, failcode, failmemo);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FindFailMemoByFailCode(string failcode)
    {
        IList<FailItems> fis = _bal.FindFailItems(failcode,"","");
        if(fis.Count>0)
        {
            return fis[0].FailMemo;
        }
        else
        {
            throw new Exception("无此不良项代码"); 
        }
    }

    /// <summary>
    /// 查询待处理品记录
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryUnsurenessOut()
    {
        string psn = Context.Request.Form["psn"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<UnsurenessProduct> objs = _bal.FindUnsurenessProductOut(psn);
        List<UnsurenessProduct> bs = new List<UnsurenessProduct>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (UnsurenessProduct bb in objs)
        {
            if (j > istart && j < iend)
            {
                UnsurenessProduct bbtemp = new UnsurenessProduct();
                switch(bb.MEMO)
                {
                    case "让步接收":
                        bb.STATUS = "2";
                        break;
                    case "返工":
                        bb.STATUS = "1";
                        break;
                    case "报废":
                        bb.STATUS = "3";
                        break;
                }
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<UnsurenessProduct> objs = _bal.FindUnsurenessProductOut(psn);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (UnsurenessProduct o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailMemo));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StationName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.STATUS));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveUnsurenessOut(string psn, string status, string statusmemo)
    {
        IList<UnsurenessProduct> up = _bal.FindUnsurenessProduct(psn.Trim());
        if(up.Count>0)
        {
            up[0].STATUS = status;
            up[0].MEMO = statusmemo;
            _bal.SaveUnsurenessProduct(up[0]);
        }
        //保存到route_history表中一份
        IList<TrackingWip> tw = _bal.FindTrackingWip(psn.Trim());
        if (tw.Count > 0)
        {
            TrackingHistory th = new TrackingHistory();
            th.PSN = tw[0].PSN;
            th.MSN = tw[0].MSN;
            th.WorkOrder = tw[0].WorkOrder;
            th.PartsdrawingCode = tw[0].PartsdrawingCode;
            th.PartsName = tw[0].PartsName;
            th.PartsCode = tw[0].PartsCode;
            th.BatchNumber = tw[0].BatchNumber;
            th.StationName = "UnsurenessOut";
            th.StationId = "UnsurenessOut";
            th.NextStation = "CheckUnsureness";
            th.NextStationId = "CheckUnsureness";
            th.QUANTITY = 1;
            th.STATUS = "P";
            th.MEMO = statusmemo;
            th.EXCEPTION = up[0].FailMemo;
            th.InStationTime = up[0].CreatedDate;
            th.OutStationTime = DateTime.Now;
            IList<WorkOrder> wo = _bal.FindWorkOrderInfo(tw[0].WorkOrder);
            th.MachineName = wo[0].MachineName;
            th.MachineType = wo[0].MachineType;           
            th.CreatedDate = DateTime.Now;
            th.UpdatedBy = this._userInfo.UserCode;
            _bal.SaveTrackingHistory(th);
        }
        
       
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindTemplateType()
    {
        return _bal.ListBindTemplateType();
    }

    /// <summary>
    /// 查询模板设置内容
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryPrintSet()
    {
        string templateType = Context.Request.Form["templateType"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<PrintSet> objs = _bal.FindPrintSet(templateType);
        List<PrintSet> bs = new List<PrintSet>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (PrintSet bb in objs)
        {
            if (j > istart && j < iend)
            {
                PrintSet bbtemp = new PrintSet();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));


        //IList<PrintSet> objs = _bal.FindPrintSet(templatetype);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (PrintSet o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.TemplateType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ACTIVE));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));           
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    /// <summary>
    /// 查询模板设置内容
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public PrintSet QueryPrintSetPath(string templateType)
    {
        
        IList<PrintSet> objs = _bal.FindPrintSet(templateType);        
        foreach (PrintSet bb in objs)
        {
            if(bb.ACTIVE=="1")
            {
                return bb;
            }
        }
        return null;
        
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SavePrintSet(string templatetype, string templatecode, string isactive)
    {
         _bal.SavePrintSet(templatetype, templatecode, isactive);
        return "OK";      
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemovePrintSet(string templatecode)
    {
        _bal.RemovePrintSet(templatecode);        
    }

    /// <summary>
    /// 查询模板编号
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] QueryTemplateCodes(string templatetype)
    {
        IList<PrintSet> objs = _bal.FindPrintSet(templatetype);
        TextValueInfo[] vts = new TextValueInfo[objs.Count];
        for (var i = 0; i < objs.Count; i++)
        {
            vts[i] = new TextValueInfo();
            vts[i].Value = objs[i].ID;
            vts[i].Text = objs[i].ID;
        }
        return vts;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SavePrintLabelTemplate(PrintSet obj)
    {
        _bal.SavePrintLabelTemplate(obj);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
   // public void SaveCartonTemp(string partsdrawing,string qualitycode,string qty)
    public void SaveCartonTemp(string partsdrawing, string qualitycode, string productName, string batchNumber, string qty, string type)
    {
        if (type == "left")
        {
            if (string.IsNullOrEmpty(partsdrawing) | string.IsNullOrEmpty(qualitycode) | string.IsNullOrEmpty(qty))
            {
                return;
            }
        }
        else
        {
            if (string.IsNullOrEmpty(partsdrawing) | string.IsNullOrEmpty(productName) | string.IsNullOrEmpty(batchNumber) | string.IsNullOrEmpty(qty))
            {
                return;
            }
        }
        CartonTemp ct = new CartonTemp();
        ct.IP = WebHelper.GetClientIPv4Address();
        ct.PartsdrawingCode = partsdrawing;
        ct.QualityCode = qualitycode;
        ct.ProductName = productName;
        ct.BatchNumber = batchNumber;
        ct.QUANTITY = Convert.ToDecimal(qty);
        ct.TYPE = type;

        _bal.SaveCartonTemp(ct);
        //if(string.IsNullOrEmpty(partsdrawing)|string.IsNullOrEmpty(qualitycode)|string.IsNullOrEmpty(qty))
        //{
        //    return;
        //}
        //CartonTemp ct = new CartonTemp();
        //ct.IP = WebHelper.GetClientIPv4Address();
        //ct.PartsdrawingCode = partsdrawing;
        //ct.QualityCode = qualitycode;
        //ct.QUANTITY = Convert.ToDecimal(qty);
        //_bal.SaveCartonTemp(ct);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveCartonTemp(string type)
    {
        string ip = WebHelper.GetClientIPv4Address();
        _bal.RemoveCartonTemp(ip, type);
    }
    //public void RemoveCartonTemp()
    //{
    //    string ip = WebHelper.GetClientIPv4Address();
    //    _bal.RemoveCartonTemp(ip);
    //}

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryCartonTemp()
    {        
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string ip = WebHelper.GetClientIPv4Address();
        IList<CartonTemp> objs = _bal.FindCartonTemp(ip);
        List<CartonTemp> bs = new List<CartonTemp>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (CartonTemp bb in objs)
        {
            if (j > istart && j < iend)
            {
                CartonTemp bbtemp = new CartonTemp();
                bbtemp = bb;
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //string ip = WebHelper.GetClientIPv4Address();
        //IList<CartonTemp>cts = _bal.FindCartonTemp(ip);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (CartonTemp o in cts)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QualityCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY.ToString()));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    /// <summary>
    /// 得到来料条码
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetCartonNO()
    {
        IList<BasCode> bcs = _bal.FindBasCode("", "来料条码");
        if (bcs != null && bcs.Count > 0)
        {
            string res = bcs[0].PREFIX;// "QZ" + "C";
            string strYear = DateTime.Today.Year.ToString();
            res += strYear.Substring(2);
            string strMonth = DateTime.Today.Month.ToString().PadLeft(2, '0');
            res += strMonth;
            string strDay = DateTime.Today.Day.ToString().PadLeft(2, '0');
            res += strDay;
            string seq = PubHelper.GetHelper().GetNextID("SEQ_CARTON_NO").ToString().PadLeft(4, '0');
            res += seq;
            return res;
        }
        else
        {
            return string.Empty;
        }
    }

    //[WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public string SaveCartonInfo()
    //{
    //    string carton = GetCartonNO();
    //    string ip = WebHelper.GetClientIPv4Address();
    //   _bal.SaveCartonInfo(ip, carton);
    //    return carton;
    //}
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveCartonInfo(string type)
    {
        string carton = GetCartonNO();
        string ip = WebHelper.GetClientIPv4Address();
        _bal.SaveCartonInfo(ip, carton, type);
        return carton;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryCartonInfo()
    {
        string cartonNo = Context.Request.Form["cartonNo"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        DateTime dtstart; //DateTime.Now.Date;
        DateTime dtend;// DateTime.Now;
        if(!string.IsNullOrEmpty(startTime))
        {
            dtstart = Convert.ToDateTime(startTime);
        }
        else
        {
            dtstart = DateTime.Now.AddDays(-30);
        }
        if(!string.IsNullOrEmpty(endTime))
        {
            dtend = Convert.ToDateTime(endTime);
        }
        else
        {
            dtend = DateTime.Now;
        }
        IList<CartonInfo> objs = _bal.FindCartonInfo("", "", this._userInfo.UserCode, dtstart, dtend);
        List<CartonInfo> bs = new List<CartonInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (CartonInfo bb in objs)
        {
            if (j > istart && j < iend)
            {
                CartonInfo bbtemp = new CartonInfo();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //DateTime dtstart = DateTime.Now.Date;
        //DateTime dtend = DateTime.Now;
        //IList<CartonInfo> cts = _bal.FindCartonInfo("","",this._userInfo.UserCode,dtstart,dtend);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (CartonInfo o in cts)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QualityCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY.ToString()));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    /// <summary>
    /// 查询基本信息--yajiao
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBaseByCode(string code)
    {
        return _bal.ListBaseByCode(code);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string ListCartonInfo(string sn,string starttime,string endtime)
    {
        DateTime dtstart = DateTime.Today.AddDays(-7);
        DateTime dtend = DateTime.Now ;
        if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
        {
             dtstart = Convert.ToDateTime(starttime);
             dtend = Convert.ToDateTime(endtime);
        }
        IList<CartonInfo> cts = _bal.FindCartonInfo(sn, "", "", dtstart, dtend);
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (CartonInfo o in cts)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.CSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.QualityCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY.ToString()));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryInMaterial(string workorder, string status, string MSN, string materialname, string custname, string starttime, string endtime, string batchnumber)
    {
        IList<MaterialStockHistory> objs = _bal.FindMaterialHistory(workorder, status, MSN, materialname, custname, starttime, endtime, batchnumber);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (MaterialStockHistory o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
            sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate));
            IList<BasBase> ibb = _bal.FindBaseBySubCode(o.StockHouse);
            sb.Append(string.Format("<cell>{0}</cell>", ibb[0].SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialName));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.UNIT));
            sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryOutMaterial(string workorder, string status, string MSN, string materialname, string custname, string starttime, string endtime, string batchnumber)
    {
        IList<MaterialStockHistory> objs = _bal.FindMaterialHistory(workorder, status, MSN, materialname, custname, starttime, endtime, batchnumber);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (MaterialStockHistory o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialName));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
            IList<BasBase> ibb = _bal.FindBaseBySubCode(o.StockHouse);
            sb.Append(string.Format("<cell>{0}</cell>", ibb[0].SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));           
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialHandler));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            //sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryInOutMaterial( string status, string materialname, string custname, string starttime, string endtime, string batchnumber)
    {
        IList<MaterialStock> objs = _bal.FindStockInfo( status,  materialname, custname, starttime, endtime, batchnumber);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (MaterialStock o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.MaterialName));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.BasQty));
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            //sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWorkOrder()
    {
        string workorder = Context.Request.Form["workorder"];
        string OrderNumber = Context.Request.Form["OrderNumber"];
        string PartsdrawingCode = Context.Request.Form["PartsdrawingCode"];
        string Status = Context.Request.Form["Status"];
        string StartTime = Context.Request.Form["StartTime"];
        string EndTime = Context.Request.Form["EndTime"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        WorkOrder wo = new WorkOrder();
        wo.WO = workorder;
        wo.OrderNumber = OrderNumber;
        wo.PartsdrawingCode = PartsdrawingCode;
        wo.STATUS = Status;
        if (!string.IsNullOrEmpty(StartTime))
        {
            wo.StartTime = Convert.ToDateTime(StartTime);
        }
        if (!string.IsNullOrEmpty(EndTime))
        {
            wo.EndTime = Convert.ToDateTime(EndTime);
        }
        if (Status == "4")//4表示没有选择状态
        {
            wo.STATUS = "";
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrder bbtemp = new WorkOrder();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

        //if (workorder.STATUS=="4")//4表示没有选择状态
        //{
        //    workorder.STATUS = "";
        //}
        //IList<WorkOrder> objs = _bal.FindWorkOrderInfo(workorder);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (WorkOrder o in objs)
        //{
        //    if (o.STATUS == "1")
        //    {
        //        sb.Append(string.Format("<row id='{0}' bgColor='green'>", o.WO));
        //    }
        //    else if (o.STATUS == "2")
        //    {
        //        sb.Append(string.Format("<row id='{0}' bgColor='yellow'>", o.WO));
        //    }

        //    else if (o.STATUS == "3")
        //    {
        //        sb.Append(string.Format("<row id='{0}' bgColor='grey'>", o.WO));
        //    }
        //    else
        //    {
        //        sb.Append(string.Format("<row id='{0}' >", o.WO));
        //    } 
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CheckTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.InTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryPartsDrawingNO(string partsdrawingno,string custcode,string starttime,string endtime)
    {
        if(custcode=="-1")
        {
            custcode = "";
        }
        IList<PartsdrawingCode> objs = _bal.FindPartsdrawingInfo(partsdrawingno, custcode, starttime, endtime);
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (PartsdrawingCode o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.PartsCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
            sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.QualityCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.AskQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.AskDate));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    /// <summary>
    /// 查询钳工任务
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryFitterTask()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrder> objs = _bal.FindQCTask();
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrder bbtemp = new WorkOrder();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<WorkOrder> objs = _bal.FindQCTask();
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (WorkOrder o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CheckTime));             
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    /// <summary>
    /// 处理钳工的扫描数据
    /// </summary>
    /// <param name="inputdata"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DealwithQGScanData(string inputdata, string ID)
    {
        TrackingTemp tt = null;
        if (string.IsNullOrEmpty(inputdata) && (inputdata.Length == 13 || inputdata.Length == 15))
        {
            inputdata = inputdata.ToUpper();
        }
        string strID = string.Empty;
        if (!string.IsNullOrEmpty(ID))
        {
            tt = _bal.FindTrackingTemp(ID);
        }
        switch (inputdata)
        {
            case "cancel":
                _bal.RemoveTrackingTemp(ID);
                break;
            case "fail":
                if (!string.IsNullOrEmpty(ID))
                {
                    throw new Exception("请进行下一步扫描!");
                }
                TrackingTemp ttempF = new TrackingTemp();
                ttempF.STEP = "fail";
                ttempF.STATUS = "F";
                strID = _bal.SaveTrackingTemp(ttempF);
                break;
            case "pass":
                if (!string.IsNullOrEmpty(ID))
                {
                    throw new Exception("请进行下一步扫描!");
                }
                TrackingTemp ttempP = new TrackingTemp();
                ttempP.STEP = "pass";
                ttempP.STATUS = "P";
                strID = _bal.SaveTrackingTemp(ttempP);
                break;
            default:
                if (inputdata.Length == 15)//表示扫描的是产品条码，因为产品条码是15位
                {
                    inputdata = inputdata.ToUpper();
                    if (tt == null)
                    {
                        throw new Exception("请先扫检验结果！");
                    }
                    if (tt.STEP != "pass" && tt.STEP != "failcode")
                    {
                        throw new Exception("请先扫描不良代码！");
                    }
                    //更新trackingwip信息                                    
                    //验证是否待处理品未审核
                    if (!_bal.CheckUnSurenessOut(inputdata))
                    {
                        throw new Exception("该产品待处理中未审核,请审核完后过本站");
                    }
                    IList<TrackingWip> tw = _bal.FindTrackingWip(inputdata);
                    if (tw.Count > 0)
                    {
                        if(tw[0].STATUS=="F")
                        {
                            throw new Exception("此条码上一站已失败，禁止过本站");
                        }
                        tw[0].StationName = "QIANGONG";
                        tw[0].STATUS = tt.STATUS;
                        tw[0].InStatioonTime = tt.CreatedDate;
                        tw[0].OutStationTime = DateTime.Now;
                        tw[0].UpdatedDate = DateTime.Now;
                        tw[0].UpdatedBy = tt.UpdatedBy;
                    }
                    else
                    {
                        throw new Exception("无此条码，请重新扫描条码");
                    }
                    _bal.SaveTrackingWip(tw[0]);
                    //保存检查记录
                    TrackingHistory th = new TrackingHistory();
                    th.PSN = tw[0].PSN;
                    th.MSN = tw[0].MSN;
                    th.WorkOrder = tw[0].WorkOrder;
                    th.PartsdrawingCode = tw[0].PartsdrawingCode;
                    th.PartsName = tw[0].PartsName;
                    th.PartsCode = tw[0].PartsCode;
                    th.BatchNumber = tw[0].BatchNumber;
                    th.StationName = "QIANGONG";
                    th.QUANTITY = 1;
                    th.STATUS = tt.STATUS;
                    if (tt.STATUS == "F")
                    {
                        th.EXCEPTION = tt.EXCEPTION;
                    }
                    th.InStationTime = tt.CreatedDate;
                    th.OutStationTime = DateTime.Now;
                    IList<WorkOrder> wo = _bal.FindWorkOrderInfo(tw[0].WorkOrder);
                    th.MachineName = wo[0].MachineName;
                    th.MachineType = wo[0].MachineType;
                    th.CreatedDate = DateTime.Now;
                    th.UpdatedBy = tt.UpdatedBy;
                    _bal.SaveTrackingHistory(th);
                    //如果不合格，直接入待处理品表
                    if (tt.STATUS == "F")
                    {
                        UnsurenessProduct up = new UnsurenessProduct();
                        up.PSN = th.PSN;
                        up.MSN = th.MSN;
                        up.WorkOrder = th.WorkOrder;
                        up.FailCode = th.EXCEPTION.Split('^')[0].ToString();
                        up.FailMemo = th.EXCEPTION.Split('^')[1].ToString();
                        up.STATUS = "0";
                        up.MEMO = "待处理";
                        up.StationName = "QIANGONG";
                        up.QUANTITY = th.QUANTITY;
                        up.PartsdrawingCode = th.PartsdrawingCode;
                        up.ProductName = th.PartsName;
                        up.BatchNumber = th.BatchNumber;
                        up.CreatedDate = DateTime.Now;
                        _bal.SaveUnsurenessProduct(up);

                        //入完待处理品后，再次保存历史记录
                        th.ID = string.Empty;
                        th.StationName = "UnsurenessIn";
                        th.STATUS = "P";
                        th.CreatedDate = DateTime.Now;
                        _bal.SaveTrackingHistory(th);

                    }
                    _bal.RemoveTrackingTemp(tt.ID);
                    //保存到实时统计表
                    RealtimeStatistics rs = new RealtimeStatistics();
                    rs.PSN = th.PSN;
                    rs.MSN = th.MSN;
                    rs.WorkOrder = th.WorkOrder;
                    rs.StationName = th.StationName;
                    rs.MachineType = th.MachineType;
                    rs.MachineName = th.MachineName;
                    rs.STATUS = th.STATUS;
                    rs.QUANTITY = th.QUANTITY;
                    rs.OPERATOR = th.UpdatedBy;
                    if (wo.Count > 0)
                    {
                        rs.OrderNumber = wo[0].OrderNumber;
                    }
                    rs.ProductName = th.PartsName;
                    rs.ProductCode = th.PartsCode;
                    IList<PartsdrawingCode> pc = _bal.FindPartsdrawingInfo(th.PartsdrawingCode);
                    if (pc.Count > 0)
                    {
                        rs.CustName = pc[0].CustName;
                    }
                    rs.PartsdrawingCode = th.PartsdrawingCode;
                    _bal.SaveRealtimeStatistics(rs);
                    break;
                }
                else//处理不良代码的情况
                {
                    if (tt.STEP != "fail")
                    {
                        throw new Exception("请进行不合格的扫描!");
                    }
                    IList<FailItems> fis = _bal.FindFailItems(inputdata, "", "");
                    if (fis.Count > 0)
                    {
                        tt.EXCEPTION = fis[0].FailCode + "^" + fis[0].FailMemo;
                        tt.STEP = "failcode";
                        _bal.SaveTrackingTemp(tt);
                    }
                }

                break;
        }
        return strID;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindFailType()
    {
        return _bal.ListBaseByCode("QZB17040011");//不良类别
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryFailItems()
    {
        string FailCode = Context.Request.Form["FailCode"];
        string FailType = Context.Request.Form["FailType"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        if (FailType == "-1")
        {
            FailType = "";
        }
        IList<FailItems> objs = _bal.FindFailItems(FailCode, FailType);  
        List<FailItems> bs = new List<FailItems>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (FailItems bb in objs)
        {
            if (j > istart && j < iend)
            {
                FailItems bbtemp = new FailItems();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

        //if(failtype=="-1")
        //{
        //    failtype = "";
        //}
        //IList<FailItems> objs = _bal.FindFailItems(failcode, failtype);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //foreach (FailItems o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.FailCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailType));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailMemo));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO1));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();

    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveFailItems(string failcode)
    {
        _bal.RemoveFailItems(failcode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveFailItems(FailItems bbase)
    {
        _bal.SaveFailItems(bbase);
    }
    /// <summary>
    /// 查询待审核品记录
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryCheckUnsureness()
    {
        string PSN = Context.Request.Form["PSN"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<UnsurenessProduct> objs = _bal.FindCheckUnsurenessProduct(PSN);
         
        List<UnsurenessProduct> bs = new List<UnsurenessProduct>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (UnsurenessProduct bb in objs)
        {
            if (j > istart && j < iend)
            {
                UnsurenessProduct bbtemp = new UnsurenessProduct();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<UnsurenessProduct> objs = _bal.FindCheckUnsurenessProduct(psn);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (UnsurenessProduct o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", ""));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailMemo));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StationName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveCheckUnsureness(string psn, string result)
    {
        _bal.SaveCheckUnsureness(psn,result);
    }

    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryCapacityStatistics()
    {
        string workorder = Context.Request.Form["workorder"];
        string OrderNumber = Context.Request.Form["OrderNumber"];
        string PartsdrawingCode = Context.Request.Form["PartsdrawingCode"];
        string Status = Context.Request.Form["Status"];
        string StartTime = Context.Request.Form["StartTime"];
        string EndTime = Context.Request.Form["EndTime"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        WorkOrder wo = new WorkOrder();
        wo.WO = workorder;
        wo.OrderNumber = OrderNumber;
        wo.PartsdrawingCode = PartsdrawingCode;
        wo.STATUS = Status;
        if (!string.IsNullOrEmpty(StartTime))
        {
            wo.StartTime = Convert.ToDateTime(StartTime);
        }
        if (!string.IsNullOrEmpty(EndTime))
        {
            wo.EndTime = Convert.ToDateTime(EndTime);
        }
        if (Status == "4")//4表示没有选择状态
        {
            wo.STATUS = "";
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrder bbtemp = new WorkOrder();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryOutNotice(string orderno, string parsdrawingno,string status)
    {
        IList<OrderDetail> objs = _bal.FindOrderInfo(orderno, parsdrawingno, status);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");

        foreach (OrderDetail o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", ""));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
            sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.InQuantity));
            sb.Append(string.Format("<cell>{0}</cell>", o.OutDate));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate != null ? o.CreatedDate : o.UpdatedDate));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveOutNotice(string id,string res)
    {
        IList<OrderDetail> obj = _bal.FindOrderInfo("", "", "", "", "", id);
        if (obj.Count > 0)
        {
            if (res == "1")
            {
                obj[0].STATUS = "2";
                obj[0].MEMO = "发货通知";
                _bal.SaveOrderInfo(obj[0]);
            }
        }
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryOrderFinish(string orderno, string parsdrawingno)
    {
        IList<OrderDetail> objs = _bal.FindOrderInfo(orderno, parsdrawingno,"1");

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");

        foreach (OrderDetail o in objs)
        {
            if (o.OrderQuantity == o.InQuantity)//筛选已完成订单
            {
                sb.Append(string.Format("<row id='{0}'>", o.ID));
                sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
                sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
                sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
                sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", o.InQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", o.OutDate));
                sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
                sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryOrderProducting(string orderno, string parsdrawingno)
    {
        IList<OrderDetail> objs = _bal.FindOrderInfo(orderno, parsdrawingno,"1");

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");

        foreach (OrderDetail o in objs)
        {
            if (o.OrderQuantity != o.InQuantity)//筛选未完成订单
            {
                sb.Append(string.Format("<row id='{0}'>", o.ID));
                sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
                IList<WorkOrder> wo = _bal.FindWorkOrderByPartsdrawingCode(o.PartsdrawingCode);
                if(wo.Count>0)
                {
                    sb.Append(string.Format("<cell>{0}</cell>", wo[0].WO));
                }
                else
                {
                    sb.Append(string.Format("<cell>{0}</cell>",""));
                }
                sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
                sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
                sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
                sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", o.InQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", o.OutDate));
                //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
                sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate == null ? o.UpdatedDate : o.CreatedDate));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryStandByOutInfo()
    {
        IList<OrderDetail> objs = _bal.FindOrderInfo("","","2");

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");

        foreach (OrderDetail o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
            sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.CustName));           
            sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));            
            int inqty = 0;
            int outqty = 0;
            if (o.InQuantity!=null)
            {
                inqty = (int)o.InQuantity;
            }
            if(o.OutQuantity!=null)
            {
                outqty = (int)o.OutQuantity;
            }
            sb.Append(string.Format("<cell>{0}</cell>", inqty));
            sb.Append(string.Format("<cell>{0}</cell>", outqty));
            sb.Append(string.Format("<cell>{0}</cell>", inqty-outqty));
            //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            //sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryOrderPublish()
    {
        string orderno = Context.Request.Form["orderno"];
        string partscode = Context.Request.Form["partscode"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<OrderDetail> objs = _bal.FindOrderInfo(orderno, partscode, "1");
        List<OrderDetail> bs = new List<OrderDetail>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (OrderDetail bb in objs)
        {
            //IList<WorkOrder> wos = _bal.FindWorkOrderByPartsdrawingCode(bb.PartsdrawingCode);
            //if (wos == null || wos.Count < 1)
            //{
                if (j > istart && j < iend)
                {
                    OrderDetail bbtemp = new OrderDetail();
                    bbtemp = bb;
                    bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                    bs.Add(bbtemp);
                }
                j++;
           // }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", j-1);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<OrderDetail> objs = _bal.FindOrderInfo("","","1");
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");

        //foreach (OrderDetail o in objs)
        //{
        //    //过滤掉已经建立的工单
        //    IList<WorkOrder> wos = _bal.FindWorkOrderByPartsdrawingCode(o.PartsdrawingCode);
        //    if (wos == null || wos.Count < 1)
        //    {
        //        sb.Append(string.Format("<row id='{0}'>", o.ID));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));                
        //        sb.Append(string.Format("<cell>{0}</cell>", o.OutQuantity));                
        //        sb.Append(string.Format("<cell>{0}</cell>", o.OutDate));
        //        sb.Append("</row>");
        //    }

        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWorkOrderMain()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        WorkOrder wo = new WorkOrder();
        wo.STATUS = "1";
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {             
            if (j > istart && j < iend)
            {
                WorkOrder bbtemp = new WorkOrder();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
             
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //WorkOrder wo = new WorkOrder();
        //wo.STATUS = "1";
        //IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (WorkOrder o in objs)
        //{              
        //    sb.Append(string.Format("<row id='{0}'>", o.WO));
        //    //sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryQualityInfo()
    {
        string custname = Context.Request.Form["CustName"];
        string partsdrawing = Context.Request.Form["Partsdrawing"];
        string startT = Context.Request.Form["StartTime"];
        string endT = Context.Request.Form["EndTime"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        DateTime dtstart = DateTime.Now.Date;
        DateTime dtend = DateTime.Now;
        WorkOrder wo = new WorkOrder();
        wo.CustName = custname;
        wo.PartsdrawingCode = partsdrawing;
        if (!string.IsNullOrEmpty(startT))
        {
            wo.StartTime = Convert.ToDateTime(startT);
        }
        if (!string.IsNullOrEmpty(endT))
        {
            wo.EndTime = Convert.ToDateTime(endT);
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<YieldInfo> bs = new List<YieldInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                YieldInfo bbtemp = new YieldInfo();
                bbtemp.WO = bb.WO;
                bbtemp.CustName = bb.CustName;
                bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                bbtemp.ProductName = bb.ProductName;
                bbtemp.BatchNumber = bb.BatchNumber;
                bbtemp.PlanQuantity = bb.PlanQuantity == null ? 0 : (int)bb.PlanQuantity;
                bbtemp.QUANTITY = bb.QUANTITY == null ? 0 : (int)bb.QUANTITY;
                int failcount = _bal.FindFailCountbyWorkOrder(bb.WO, "");                
                bbtemp.FailCount = failcount;               
                bbtemp.PassRate = (Math.Round((double)(bbtemp.PassCount * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.FailRate = (Math.Round((double)(failcount * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //DateTime dtstart = DateTime.Now.Date;
        //DateTime dtend = DateTime.Now;
        //WorkOrder wo = new WorkOrder();
        //wo.CustName = custname;
        //wo.PartsdrawingCode = partsdrawing;
        //if (!string.IsNullOrEmpty(startT))
        //{
        //    wo.StartTime = Convert.ToDateTime(startT);
        //}
        //if(!string.IsNullOrEmpty(endT))
        //{
        //    wo.EndTime = Convert.ToDateTime(endT);
        //}
        //IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (WorkOrder o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    int failcount = _bal.FindFailCountbyWorkOrder(o.WO, "");
        //    sb.Append(string.Format("<cell>{0}</cell>", failcount));
        //    string failrate = (Math.Round((double)(failcount*100 / (o.QUANTITY == null ? 1 : o.QUANTITY)),2)).ToString()+"%";
        //    sb.Append(string.Format("<cell>{0}</cell>", failrate));             
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    /// <summary>
    /// 查询待处理品记录
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryUnsurenessHistory()
    {
        string psn = Context.Request.Form["psn"];
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string wo = Context.Request.Form["wo"];
        string status = Context.Request.Form["status"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];

        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        UnsurenessHistory uh = new UnsurenessHistory();
        uh.PSN = psn;
        uh.PartsdrawingCode = partsdrawing;
        uh.WorkOrder = wo;
        uh.STATUS = status;
        if (!string.IsNullOrEmpty(startTime))
        {
            uh.CreatedDate = Convert.ToDateTime(startTime);
        }
        if (!string.IsNullOrEmpty(endTime))
        {
            uh.UpdatedDate = Convert.ToDateTime(endTime);
        }
        IList<UnsurenessHistory> objs = _bal.FindUnsurenessHistory(uh);// _bal.FindUnsurenessProductOut(psn);
        List<UnsurenessHistory> bs = new List<UnsurenessHistory>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (UnsurenessHistory bb in objs)
        {
            if (j > istart && j < iend)
            {
                UnsurenessHistory bbtemp = new UnsurenessHistory();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

        //UnsurenessHistory uh = new UnsurenessHistory();
        //uh.PSN = psn;
        //uh.PartsdrawingCode = partsdrawing;
        //uh.WorkOrder = wo;
        //uh.STATUS = status;
        //if (!string.IsNullOrEmpty(startT))
        //{
        //    uh.CreatedDate = Convert.ToDateTime(startT);
        //}
        //if (!string.IsNullOrEmpty(endT))
        //{
        //    uh.UpdatedDate = Convert.ToDateTime(endT);
        //}
        //IList<UnsurenessHistory> objs = _bal.FindUnsurenessHistory(uh);// _bal.FindUnsurenessProductOut(psn);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (UnsurenessHistory o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.FailMemo));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.StationName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));         
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryYieldInfo()
    {
        string custname = Context.Request.Form["custname"];
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string startT = Context.Request.Form["startT"];
        string endT = Context.Request.Form["endT"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        DateTime dtstart = DateTime.Now.Date;
        DateTime dtend = DateTime.Now;
        WorkOrder wo = new WorkOrder();
        wo.CustName = custname;
        wo.PartsdrawingCode = partsdrawing;
        if (!string.IsNullOrEmpty(startT))
        {
            wo.StartTime = Convert.ToDateTime(startT);
        }
        if (!string.IsNullOrEmpty(endT))
        {
            wo.EndTime = Convert.ToDateTime(endT);
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);       
        List<YieldInfo> bs = new List<YieldInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                YieldInfo bbtemp = new YieldInfo();
                bbtemp.WO = bb.WO;
                bbtemp.CustName = bb.CustName;
                bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                bbtemp.ProductName = bb.ProductName;
                bbtemp.BatchNumber = bb.BatchNumber;
                bbtemp.QUANTITY = bb.QUANTITY==null?0: (int)bb.QUANTITY;
                int[] fails = _bal.FindYieldCountInfo(bb.WO, bb.PartsdrawingCode);
                bbtemp.PassCount = (int)(bb.QUANTITY == null ? 0 : bb.QUANTITY) - fails[0];
                bbtemp.FailCount = fails[0];
                bbtemp.ReturnCount = fails[1];
                bbtemp.SecondPass = fails[2];
                bbtemp.DiscardCount = fails[3];
                bbtemp.PassRate = (Math.Round((double)(bbtemp.PassCount * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.FailRate = (Math.Round((double)(fails[0] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.ReturnRate = (Math.Round((double)(fails[1] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.SecPassRate = (Math.Round((double)(fails[2] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.DiscardRate = (Math.Round((double)(fails[3] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map)); 
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWIPInfo()
    {
        string drawingno = Context.Request.Form["drawingno"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrder> objs = _bal.FindWorkOrderByStatus("1", true, drawingno);//查询运行中的工单
        RealtimeStatistics rs = new RealtimeStatistics();
        List<WipInfo> bs = new List<WipInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WipInfo bbtemp = new WipInfo();
                bbtemp.WO = bb.WO;
                bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                bbtemp.ProductName = bb.ProductName;
                bbtemp.PlanQuantity = bb.PlanQuantity==null?0:(int)bb.PlanQuantity;
                rs.WorkOrder = bb.WO;
                rs.StationName = "数铣";// "CHEXI";
                rs.STATUS = "P";
                int chexipass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.CheXiPassCount = chexipass;
                rs.STATUS = "F";
                int chexifail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.CheXiFailCount = chexifail;
                rs.StationName = "钳工";// "QIANGONG";
                int qiangongfail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QianGongFailCount = qiangongfail;
                rs.STATUS = "P";
                int qiangongpass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QianGongPassCount = qiangongpass;
                rs.StationName = "QC";
                int qcpass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QCPassCount = qcpass;
                rs.STATUS = "F";
                int qcfail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QCFailCount = qcfail;
                rs.StationName = "INSTOCK";
                rs.STATUS = "";
                int instockqty = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.InStockQty = instockqty;
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));       
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveEquipmentInfo(BasEquipment obj)
    {
        _bal.SaveBasEquipment(obj);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveEquipmentInfo(string code)
    {
        _bal.RemoveBasEquipment(code);
    }
   
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryEquipmentInfo()
    {
        string equipName = Context.Request.Form["equipName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasEquipment> objs = _bal.FindBasEquipment("", equipName);
        List<BasEquipment> bs = new List<BasEquipment>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (BasEquipment bb in objs)
        {
            if (j > istart && j < iend)
            {
                BasEquipment bbtemp = new BasEquipment();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //IList<BasEquipment> objs = _bal.FindBasEquipment(code, name);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;        
        //foreach (BasEquipment o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.CODE));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CODE));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.COMPANY));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MachineType)); 
        //    sb.Append(string.Format("<cell>{0}</cell>", o.AxisNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MODEL));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.POWER));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.LOCATION));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.STATUS));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.OutCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UseDate));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveMaterielInfo(BasMateriel obj)
    {
        _bal.SaveBasMateriel(obj);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveMaterielInfo(string qzmateriel,string custmateriel)
    {
        _bal.RemoveBasMateriel(custmateriel, qzmateriel);//.RemoveBasEquipment(code);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryMaterielInfo()
    {
        string qzMateriel = Context.Request.Form["qzMateriel"];
        string custMateriel = Context.Request.Form["custMateriel"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasMateriel> objs = _bal.FindBasMateriel(custMateriel, qzMateriel, "");
        List<BasMateriel> bs = new List<BasMateriel>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (BasMateriel bb in objs)
        {
            if (j > istart && j < iend)
            {
                BasMateriel bbtemp = new BasMateriel();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

        //IList<BasMateriel> objs = _bal.FindBasMateriel(cmateriel, qmateriel, "");//.FindBasEquipment(code, name);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (BasMateriel o in objs)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.CPARTNO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QPARTNO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CPARTNO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.NAME));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.BasQty));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CUSTOMER));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryCustcodeByName(string name)
    {
        return _bal.ListCustCodeByName(name);//.RemoveBasEquipment(code);
    }
	/// <summary>
    /// 查询基本编码及名称
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBaseName()
    {
        return  _bal.ListBaseName();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryCustomerCodeByCustomer(string customer)
    {
        IList<BasCustom> obj = _bal.FindBasCustom("", customer);
        if(obj!=null&&obj.Count>0)
        {
            return obj[0].CODE;
        }
        return customer;
    }
    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryProductCodeCustNameByWorkOrder(string workorder)
    {
         
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(workorder);
        if(objs!=null&&objs.Count>0)
        {
            return objs[0].ProductCode + "^" + objs[0].CustName;
        }
        return "^"; 
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListPartCode()
    {
        return _bal.ListPartCode();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetcustByPartCode(string partCode)
    {
        return _bal.GetcustByPartCode(partCode);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProductByPartCode(string partCode)
    {
        return _bal.GetProductByPartCode(partCode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListPartCodeEasyUI()
    {
        TextValueInfo[] tvis = _bal.ListPartCode();
        string res = "[";
        for (int i = 0; i < tvis.Length; i++)
        {
            res += "{\"id\":\"" + tvis[i].Value + "\",\"text\":\"" + tvis[i].Text + "\"},";
        }
        
        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] FindMSNByDocumentID(string documentid)
    {
        IList<MaterialStock> mss = _bal.FindMSNByDocumentID(documentid);
        if(mss==null||mss.Count==0)
        {
            return null;
        }
        string[] res = new string[mss.Count];
        for(int i=0;i<mss.Count;i++)
        {
            res[i] = mss[i].MSN;
        }
        return res;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListMaterialCodeEasyUI()
    {
        TextValueInfo[] tvis = _bal.ListMaterialCodeName();
        string res = "[";
        for (int i = 0; i < tvis.Length; i++)
        {
            res += "{\"id\":\"" + tvis[i].Value + "\",\"text\":\"" + tvis[i].Text + "\"},";
        }

        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);

        //TextValueInfo[] tvis = _bal.ListMaterialCode();
        //string res = "[";
        //for (int i = 0; i < tvis.Length; i++)
        //{
        //    res += "{\"id\":\"" + tvis[i].Value + "\",\"text\":\"" + tvis[i].Text + "\"},";
        //}

        //res = res.Substring(0, res.Length - 1);
        //res += "]";
        //Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetCMaterialByQMaterialCode(string qmaterialcode)
    {
        return _bal.GetCMaterialByQMaterialCode(qmaterialcode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryCurOutQty()
    {
        int restotal = _bal.QueryCurOutQty();
 

        string res = "[{\"count\":\"" + restotal.ToString() + "\"}]";
        
        Context.Response.Write(res);
        
    }
    /// <summary>
    /// 查询产品已完成数量和未完成数量的比例
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryOutQtyProportion()
    {
        string res = "[{\"propertion\":[{\"data\":[";



        IList<WorkOrder> wo = _bal.FindWorkOrderByStatus("1",true);//1表示运行状态         
        int demandTotal = 0;
        int finishTotal = 0;
        int noFinishTotal = 0;
        if (wo != null && wo.Count > 0)
        {
            for (int i = 0; i < wo.Count; i++)
            {
                demandTotal += (int)wo[i].PlanQuantity;
                noFinishTotal += (int)(wo[i].PlanQuantity - (wo[i].QUANTITY == null ? 0 : wo[i].QUANTITY));
                finishTotal += (int)(wo[i].QUANTITY == null ? 0 : wo[i].QUANTITY);

            }

        }
        float ffinishTotal = (float)(Math.Round(((double)finishTotal / (demandTotal == 0 ? 1 : demandTotal)) * 10000) / 100);
        float fnofinishTotal = 100 - ffinishTotal;
        res += "{\"name\":\"已完成占比\",\"y\":" + ffinishTotal + ",\"drilldown\":\"finish\"},{\"name\":\"待完成占比\",\"y\":"
                + fnofinishTotal + ",\"drilldown\":\"nofinish\"}]}],";
        res += "\"drilldown\":[{\"name\":\"finish\",\"id\":\"finish\",\"data\":[";
        for (int i = 0; i < wo.Count; i++)
        {
            float proportion = (float)(Math.Round(((double)((wo[i].QUANTITY == null ? 0 : wo[i].QUANTITY) / wo[i].PlanQuantity)) * 10000) / 100);
            res += "[\"";
            //res += wo[i].PartsdrawingCode + "\",\"" + (wo[i].MaterialQty == null ? 0 : wo[i].MaterialQty) + "/" + wo[i].PlanQuantity + "\"],";
            res += wo[i].PartsdrawingCode + "\"," + proportion + "],";
        }
        res = res.Substring(0, res.Length - 1);
        res += "]},{\"name\":\"nofinish\",\"id\":\"nofinish\",\"data\":[";

        for (int i = 0; i < wo.Count; i++)
        {
            res += "[\"";
            int inosend = (int)(wo[i].PlanQuantity - (wo[i].QUANTITY == null ? 0 : wo[i].QUANTITY));
            float proportion = (float)(Math.Round(((double)(inosend / wo[i].PlanQuantity)) * 10000) / 100);
            res += wo[i].PartsdrawingCode + "\"," + proportion + "],";
            // res += wo[i].PartsdrawingCode + "\",\"" + inosend + "/" + wo[i].PlanQuantity + "\"],";
        }
        res = res.Substring(0, res.Length - 1);
        res += "]}]}]";        
        Context.Response.Write(res);
    }
    /// <summary>
    /// 查询产品良率和不良率的比例
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryOutYeildProportion()
    {
        string custname = string.Empty ;
        string partsdrawing = string.Empty;
        string startT = string.Empty;// System.DateTime.Today.ToShortDateString();
        string endT = string.Empty;// System.DateTime.Today.AddDays(1).ToShortDateString();
        string rows = string.Empty;
        string page = string.Empty;
        DateTime dtstart = DateTime.Now.Date;
        DateTime dtend = DateTime.Now;
        WorkOrder wo = new WorkOrder();
        wo.CustName = custname;
        wo.PartsdrawingCode = partsdrawing;
        wo.STATUS = "1";
        if (!string.IsNullOrEmpty(startT))
        {
            wo.StartTime = Convert.ToDateTime(startT);
        }
        if (!string.IsNullOrEmpty(endT))
        {
            wo.EndTime = Convert.ToDateTime(endT);
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<YieldInfo> bs = new List<YieldInfo>();       
        
        foreach (WorkOrder bb in objs)
        {           
                YieldInfo bbtemp = new YieldInfo();
                bbtemp.WO = bb.WO;
                bbtemp.CustName = bb.CustName;
                bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                bbtemp.ProductName = bb.ProductName;
                bbtemp.BatchNumber = bb.BatchNumber;
                bbtemp.QUANTITY = bb.QUANTITY == null ? 0 : (int)bb.QUANTITY;
                int[] fails = _bal.FindYieldCountInfo(bb.WO, bb.PartsdrawingCode);
                bbtemp.PassCount = (int)(bb.QUANTITY == null ? 0 : bb.QUANTITY) - fails[0];
                bbtemp.FailCount = fails[0];
                bbtemp.ReturnCount = fails[1];
                bbtemp.SecondPass = fails[2];
                bbtemp.DiscardCount = fails[3];
                bbtemp.PassRate = (Math.Round((double)(bbtemp.PassCount * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.FailRate = (Math.Round((double)(fails[0] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.ReturnRate = (Math.Round((double)(fails[1] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.SecPassRate = (Math.Round((double)(fails[2] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.DiscardRate = (Math.Round((double)(fails[3] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bs.Add(bbtemp);           
        }
       
        string res = "[{\"propertion\":[{\"data\":[";             
        int outTotal = 0;
        int passTotal = 0;
        int failTotal = 0;
        if (bs != null && bs.Count > 0)
        {
            for (int i = 0; i < bs.Count; i++)
            {
                outTotal +=  bs[i].QUANTITY;
                failTotal += bs[i].FailCount;
                passTotal += bs[i].PassCount;
            }

        }
        float fpassTotal = (float)(Math.Round(((double)passTotal / (outTotal == 0 ? 1 : outTotal)) * 10000) / 100);
        float ffailTotal = 100 - fpassTotal;
        res += "{\"name\":\"一类\",\"y\":" + fpassTotal + ",\"drilldown\":\"PassRate\"},{\"name\":\"二类\",\"y\":"
                + ffailTotal + ",\"drilldown\":\"FailRate\"}]}],";
        res += "\"drilldown\":[{\"name\":\"PassRate\",\"id\":\"PassRate\",\"data\":[";
        bool bsub = false;
        for (int i = 0; i < bs.Count; i++)
        {
            //double dd = (double)((double)bs[i].PassCount / (bs[i].QUANTITY == 0 ? 1 : bs[i].QUANTITY));
            //double dd1 = dd * 10000;
            //float ff = (float)(Math.Round(dd1 / 100));
            float proportion = (float)(Math.Round(((double)((double)bs[i].PassCount / (bs[i].QUANTITY==0?1:bs[i].QUANTITY))) * 10000) / 100);
            res += "[\"";
            //res += wo[i].PartsdrawingCode + "\",\"" + (wo[i].MaterialQty == null ? 0 : wo[i].MaterialQty) + "/" + wo[i].PlanQuantity + "\"],";
            res += bs[i].PartsdrawingCode + "\"," + proportion + "],";
            bsub = true;
        }
        if (bsub)
        {
            res = res.Substring(0, res.Length - 1);
        }
        res += "]},{\"name\":\"FailRate\",\"id\":\"FailRate\",\"data\":[";
        bsub = false;
        for (int i = 0; i < bs.Count; i++)
        {
            res += "[\"";
            float proportion = (float)(Math.Round(((double)(bs[i].FailCount / (bs[i].QUANTITY == 0 ? 1 : bs[i].QUANTITY))) * 10000) / 100);
            res += bs[i].PartsdrawingCode + "\"," + proportion + "],";
            // res += wo[i].PartsdrawingCode + "\",\"" + inosend + "/" + wo[i].PlanQuantity + "\"],";
            bsub = true;
        }
        if (bsub)
        {
            res = res.Substring(0, res.Length - 1);
        }
        res += "]}]}]";
        Context.Response.Write(res);
    }

    /// <summary>
    /// 查询产品已完成数量和未完成数量以及百分比
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryPlanAndOutQtyProportion()
    {
        string res = "[{\"categories\":[{\"data\":";
        IList<WorkOrder> wo = _bal.FindWorkOrderByStatus("1", true);//1表示运行状态         

        string strCategories = "[";
        string strPlanQty = "[";
        string strQty = "[";
        string strQtyRate = "[";
        bool bsub = false;
        for (int i = 0; i < wo.Count; i++)
        {
            strCategories += "\"" + wo[i].PartsdrawingCode + "\",";
            strQty += (wo[i].QUANTITY==null?0:wo[i].QUANTITY) + ",";
            strPlanQty += wo[i].PlanQuantity + ",";
            float proportion = (float)(Math.Round(((double)((wo[i].QUANTITY == null ? 0 : wo[i].QUANTITY) / (wo[i].PlanQuantity == 0 ? 1 : wo[i].PlanQuantity))) * 10000) / 100);
            strQtyRate += proportion + ",";
            bsub = true;
        }
        if(bsub)
        {
            strCategories = strCategories.Substring(0, strCategories.Length - 1);
            strQty = strQty.Substring(0, strQty.Length - 1);
            strPlanQty = strPlanQty.Substring(0, strPlanQty.Length - 1);
            strQtyRate = strQtyRate.Substring(0, strQtyRate.Length - 1);
        }
        strCategories += "]";
        strQty += "]";
        strPlanQty += "]";
        strQtyRate += "]";
        res += strCategories + "}],\"planqty\":[{\"data\":" + strPlanQty + "}],\"qty\":[{\"data\":" + strQty + "}],\"qtyrate\":[{\"data\":" + strQtyRate + "}]}]";
         
        Context.Response.Write(res);
    }

    /// <summary>
    /// 查询产品不良数量和良品数量以及它们的的比例
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryYeildQtyProportion()
    {
        string custname = string.Empty;
        string partsdrawing = string.Empty;
        string startT = string.Empty;// System.DateTime.Today.ToShortDateString();
        string endT = string.Empty;// System.DateTime.Today.AddDays(1).ToShortDateString();
        string rows = string.Empty;
        string page = string.Empty;
        DateTime dtstart = DateTime.Now.Date;
        DateTime dtend = DateTime.Now;
        WorkOrder wo = new WorkOrder();
        wo.CustName = custname;
        wo.PartsdrawingCode = partsdrawing;
        wo.STATUS = "1";
        if (!string.IsNullOrEmpty(startT))
        {
            wo.StartTime = Convert.ToDateTime(startT);
        }
        if (!string.IsNullOrEmpty(endT))
        {
            wo.EndTime = Convert.ToDateTime(endT);
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<YieldInfo> bs = new List<YieldInfo>();

        foreach (WorkOrder bb in objs)
        {
            YieldInfo bbtemp = new YieldInfo();
            bbtemp.WO = bb.WO;
            bbtemp.CustName = bb.CustName;
            bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
            bbtemp.ProductName = bb.ProductName;
            bbtemp.BatchNumber = bb.BatchNumber;
            bbtemp.QUANTITY = bb.QUANTITY == null ? 0 : (int)bb.QUANTITY;
            int[] fails = _bal.FindYieldCountInfo(bb.WO, bb.PartsdrawingCode);
            bbtemp.PassCount = (int)(bb.QUANTITY == null ? 0 : bb.QUANTITY) - fails[0];
            bbtemp.FailCount = fails[0];
            bbtemp.ReturnCount = fails[1];
            bbtemp.SecondPass = fails[2];
            bbtemp.DiscardCount = fails[3];
            bbtemp.PassRate = (Math.Round((double)(bbtemp.PassCount * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
            bbtemp.FailRate = (Math.Round((double)(fails[0] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
            bbtemp.ReturnRate = (Math.Round((double)(fails[1] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
            bbtemp.SecPassRate = (Math.Round((double)(fails[2] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
            bbtemp.DiscardRate = (Math.Round((double)(fails[3] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
            bs.Add(bbtemp);
        }

        string res = "[{\"categories\":[{\"data\":";       
        string strCategories = "[";
        string strPlanQty = "[";//产出数量
        string strQty = "[";//合格数量
        string strQtyRate = "[";//良率
        bool bsub = false;
        for (int i = 0; i < bs.Count; i++)
        {
            strCategories += "\"" + bs[i].PartsdrawingCode + "\",";
            strQty += bs[i].PassCount + ",";
            strPlanQty += bs[i].QUANTITY + ",";
            float proportion = (float)(Math.Round(((double)((double)bs[i].PassCount / (bs[i].QUANTITY == 0 ? 1 : bs[i].QUANTITY))) * 10000) / 100);
            strQtyRate += proportion + ",";
            bsub = true;
        }
        if (bsub)
        {
            strCategories = strCategories.Substring(0, strCategories.Length - 1);
            strQty = strQty.Substring(0, strQty.Length - 1);
            strPlanQty = strPlanQty.Substring(0, strPlanQty.Length - 1);
            strQtyRate = strQtyRate.Substring(0, strQtyRate.Length - 1);
        }
        strCategories += "]";
        strQty += "]";
        strPlanQty += "]";
        strQtyRate += "]";
        res += strCategories + "}],\"planqty\":[{\"data\":" + strPlanQty + "}],\"qty\":[{\"data\":" + strQty + "}],\"qtyrate\":[{\"data\":" + strQtyRate + "}]}]";

        Context.Response.Write(res);
    }
    /// <summary>
    /// 查询机床的产出信息:WO,PARTSDRAWING_CODE,PLAN_QUANTITY,QUANTITY,WORKER,WORKER_NAME,CurQty
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryCurMachineInfo()
    {
        string machinename = Context.Request.Form["machinename"];
        string[] resmachine = _bal.QueryCurMachineInfo(machinename);
        string temp = resmachine[3] == null ? "0" : resmachine[3].ToString();
        temp = temp == "" ? "0" : temp;
        string res = "[";
        res += "{\"curqty\":\"" + temp + "\"";
        res += ",\"wo\":\"" + (resmachine[0] == null ? "暂无生产" : resmachine[0].ToString()) + "\"";
        res += ",\"partsdrawingno\":\"" + (resmachine[1] == null ? "暂无图号" : resmachine[1].ToString()) + "\"";
        res += ",\"planqty\":\"" + (resmachine[2] == null ? "0" : resmachine[2].ToString()) + "\"";
       
        res += ",\"qty\":\"" + temp + "\"";
        res += ",\"worker\":\"" + (resmachine[4] == null ? "" : resmachine[4].ToString()) + "\"";
        res += ",\"workername\":\"" + (resmachine[5] == null ? "" : resmachine[5].ToString()) + "\"}";
        res += "]";
        Context.Response.Write(res);

    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryYield()
    {
        string month = Context.Request.Form["month"];


        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;



        List<YieldInfo> bs = new List<YieldInfo>();



        YieldInfo bbtemp = new YieldInfo();
        int[] fails = _bal.FindYield(month);
        bbtemp.QUANTITY = (fails[0] + fails[1]);
        bbtemp.PassCount = fails[0];
        bbtemp.FailCount = fails[1];
        bbtemp.ReturnCount = fails[2];
        bbtemp.SecondPass = fails[3];
        bbtemp.DiscardCount = fails[4];
        bbtemp.PassRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
        bbtemp.FailRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round(100 - (double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
        bbtemp.ReturnRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.ReturnCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
        bbtemp.SecPassRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.SecondPass * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
        bbtemp.DiscardRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.DiscardCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
        bs.Add(bbtemp);


        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (bs != null & bs.Count > 0)
        {
            map.Add("total", bs.Count);
        }
        else
        {
            map.Add("total", 0);
        }
        map.Add("rows", bs);

        Context.Response.Write(JsonConvert.SerializeObject(map));

    }



    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void PassRateProportion()
    {
        string month = Context.Request.Form["month"];
        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;

        string res = "[{\"propertion\":[{\"data\":[";
        int[] allRate = _bal.FindYield(month);
        int QUANTITY = (allRate[0] + allRate[1]);
        int PassCount = allRate[0];
        int FailCount = allRate[1];
        int ReturnCount = allRate[2];
        int SecondPass = allRate[3];
        int DiscardCount = allRate[4];
        double PassRate = QUANTITY == 0 ? 0 : (Math.Round((double)(PassCount * 100 / QUANTITY), 2));
        double FailRate = QUANTITY == 0 ? 0 : (Math.Round(100 - (double)(PassCount * 100 / QUANTITY), 2));
        double ReturnRate = QUANTITY == 0 ? 0 : (Math.Round((double)(ReturnCount * 100 / QUANTITY), 2));
        double SecPassRate = QUANTITY == 0 ? 0 : (Math.Round((double)(SecondPass * 100 / QUANTITY), 2));
        double DiscardRate = QUANTITY == 0 ? 0 : (Math.Round((double)(DiscardCount * 100 / QUANTITY), 2));

        res += "{\"name\":\"一类品率\",\"y\":" + PassRate + ",\"drilldown\":\"passed\"},{\"name\":\"返工率\",\"y\":"
                + ReturnRate + ",\"drilldown\":\"returned\"},{\"name\":\"让步率\",\"y\":"
                + SecPassRate + ",\"drilldown\":\"secpass\"},{\"name\":\"废品率\",\"y\":"
                + DiscardRate + ",\"drilldown\":\"discard\"}]}],";
        res += "\"drilldown\":[{\"name\":\"passed\",\"id\":\"passed\",\"data\":[";
        DataSet ds = _bal.FindCustYield("passed", month);


        if (ds != null && ds.Tables.Count > 0 & ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                res += "[\"";
                res += ds.Tables[0].Rows[i]["CUST_NAME"] + "\"," + ds.Tables[0].Rows[i]["COUNT"] + "],";
            }
            res = res.Substring(0, res.Length - 1);
        }

        res += "]},{\"name\":\"returned\",\"id\":\"returned\",\"data\":[";
        ds = _bal.FindCustYield("returned", month);
        if (ds != null && ds.Tables.Count > 0 & ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                res += "[\"";
                res += ds.Tables[0].Rows[i]["CUST_NAME"] + "\"," + ds.Tables[0].Rows[i]["COUNT"] + "],";
            }
            res = res.Substring(0, res.Length - 1);
        }

        res += "]},{\"name\":\"secpass\",\"id\":\"secpass\",\"data\":[";

        ds = _bal.FindCustYield("secpass", month);
        if (ds != null && ds.Tables.Count > 0 & ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                res += "[\"";
                res += ds.Tables[0].Rows[i]["CUST_NAME"] + "\"," + ds.Tables[0].Rows[i]["COUNT"] + "],";
            }
            res = res.Substring(0, res.Length - 1);
        }

        res += "]},{\"name\":\"discard\",\"id\":\"discard\",\"data\":[";

        ds = _bal.FindCustYield("discard", month);
        if (ds != null && ds.Tables.Count > 0 & ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                res += "[\"";
                res += ds.Tables[0].Rows[i]["CUST_NAME"] + "\"," + ds.Tables[0].Rows[i]["COUNT"] + "],";
            }
            res = res.Substring(0, res.Length - 1);
        }

        res += "]}]}]";

        Context.Response.Write(res);
    }





    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindMachineName()
    {
        string month = Context.Request.Form["month"];

        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;


        IList<RealtimeStatistics> machines = _bal.findMachineCode();
        List<YieldInfo> bs = new List<YieldInfo>();
        for (int i = 0; i < machines.Count; i++)
        {

            YieldInfo bbtemp = new YieldInfo();
            bbtemp.WO = machines[i].MachineName;
            int[] fails = _bal.FindMachineYield(machines[i].MachineName, month);
            bbtemp.QUANTITY = (fails[0] + fails[1]);
            bbtemp.PassCount = fails[0];
            bbtemp.FailCount = fails[1];
            bbtemp.ReturnCount = fails[2];
            bbtemp.SecondPass = fails[3];
            bbtemp.DiscardCount = fails[4];
            bbtemp.PassRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.FailRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round(100 - (double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.ReturnRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.ReturnCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.SecPassRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.SecondPass * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.DiscardRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.DiscardCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bs.Add(bbtemp);
        }
        Context.Response.Write(JsonConvert.SerializeObject(bs));

    }



    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MachineYieldChart()
    {
        string month = Context.Request.Form["month"];

        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;

        //加载所有机床名称
        IList<RealtimeStatistics> machines = _bal.findMachineCode();
        List<YieldInfo> bs = new List<YieldInfo>();
        //拼接返回字符串
        IList<HighCharts> hc = new List<HighCharts>();
        HighCharts hc1 = new HighCharts();
        hc1.name = "一类品率";
        hc1.data = new double[machines.Count];
        HighCharts hc2 = new HighCharts();
        hc2.name = "二类品率";
        hc2.data = new double[machines.Count];

        IList<HighCharts> hcD = new List<HighCharts>();
        HighCharts hcD1 = new HighCharts();
        hcD1.name = "返工率";
        hcD1.data = new double[machines.Count];
        HighCharts hcD2 = new HighCharts();
        hcD2.name = "让步率";
        hcD2.data = new double[machines.Count];
        HighCharts hcD3 = new HighCharts();
        hcD3.name = "报废率";
        hcD3.data = new double[machines.Count];
        for (int i = 0; i < machines.Count; i++)
        {

            YieldInfo bbtemp = new YieldInfo();
            bbtemp.WO = machines[i].MachineName;
            int[] fails = _bal.FindMachineYield(machines[i].MachineName, month);
            bbtemp.QUANTITY = (fails[0] + fails[1]);
            bbtemp.PassCount = fails[0];
            bbtemp.FailCount = fails[1];
            bbtemp.ReturnCount = fails[2];
            bbtemp.SecondPass = fails[3];
            bbtemp.DiscardCount = fails[4];
            bbtemp.PassRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.FailRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round(100 - (double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.ReturnRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.ReturnCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.SecPassRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.SecondPass * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.DiscardRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.DiscardCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bs.Add(bbtemp);
            hc1.data[i] = fails[0] + fails[1] == 0 ? 0 : Math.Round((double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2);
            hc2.data[i] = fails[0] + fails[1] == 0 ? 0 : Math.Round(100 - (double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2);
            hcD1.data[i] = fails[0] + fails[1] == 0 ? 0 : Math.Round((double)(bbtemp.ReturnCount * 100 / bbtemp.QUANTITY), 2);
            hcD2.data[i] = fails[0] + fails[1] == 0 ? 0 : Math.Round((double)(bbtemp.SecondPass * 100 / bbtemp.QUANTITY), 2);
            hcD3.data[i] = fails[0] + fails[1] == 0 ? 0 : Math.Round((double)(bbtemp.DiscardCount * 100 / bbtemp.QUANTITY), 2);

        }
        hc.Add(hc1);
        hc.Add(hc2);
        hcD.Add(hcD1);
        hcD.Add(hcD2);
        hcD.Add(hcD3);
        Dictionary<String, Object> all = new Dictionary<String, Object>();
        all.Add("HC", hc);
        all.Add("DETAIL", hcD);
        Context.Response.Write(JsonConvert.SerializeObject(all));
    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryMachineYield()
    {
        string month = Context.Request.Form["month"];


        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;

        IList<RealtimeStatistics> machines = _bal.findMachineCode();
        List<YieldInfo> bs = new List<YieldInfo>();

        for (int i = 0; i < machines.Count; i++)
        {

            YieldInfo bbtemp = new YieldInfo();
            bbtemp.WO = machines[i].MachineName;


            int[] fails = _bal.FindMachineYield(machines[i].MachineName, month);
            bbtemp.QUANTITY = (fails[0] + fails[1]);
            bbtemp.PassCount = fails[0];
            bbtemp.FailCount = fails[1];
            bbtemp.ReturnCount = fails[2];
            bbtemp.SecondPass = fails[3];
            bbtemp.DiscardCount = fails[4];
            bbtemp.PassRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.FailRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round(100 - (double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.ReturnRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.ReturnCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.SecPassRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.SecondPass * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.DiscardRate = fails[0] + fails[1] == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.DiscardCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bs.Add(bbtemp);

        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (bs != null & bs.Count > 0)
        {
            map.Add("total", bs.Count);
        }
        else
        {
            map.Add("total", 0);
        }
        map.Add("rows", bs);

        Context.Response.Write(JsonConvert.SerializeObject(map));

    }



    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void CustRateProportion()
    {
        string month = Context.Request.Form["month"];
        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;

        string res = "[{\"propertion\":[{\"data\":[";
        IList<BasCustom> bs = _bal.FindCustName();
        if (bs == null || bs.Count == 0)
        {
            return;
        }
        for (int i = 0; i < bs.Count; i++)
        {
            int[] allRate = _bal.FindCustQty(month, bs[i].NAME);
            int QUANTITY = (allRate[0] + allRate[1]);
            int FailCount = allRate[1];

            res += "{\"name\":\"" + bs[i].NAME + "\",\"y\":" + FailCount + ",\"drilldown\":\"" + bs[i].NAME + "\"},";
        }
        res = res.Substring(0, res.Length - 1);
        res += "]}],";
        //res += "{\"name\":\"合格率\",\"y\":" + PassRate + ",\"drilldown\":\"passed\"},{\"name\":\"返工率\",\"y\":"
        //        + ReturnRate + ",\"drilldown\":\"returned\"},{\"name\":\"让步率\",\"y\":"
        //        + SecPassRate + ",\"drilldown\":\"secpass\"},{\"name\":\"废品率\",\"y\":"
        //        + DiscardRate + ",\"drilldown\":\"discard\"}]}],";
        res += "\"drilldown\":[";
        for (int i = 0; i < bs.Count; i++)
        {
            res += "{\"name\":\"" + bs[i].NAME + "\",\"id\":\"" + bs[i].NAME + "\",\"data\":[";
            int[] allRate = _bal.FindCustQty(month, bs[i].NAME);
            int QUANTITY = (allRate[0] + allRate[1]);
            int PassCount = allRate[0];
            int FailCount = allRate[1];
            int ReturnCount = allRate[2];
            int SecondPass = allRate[3];
            int DiscardCount = allRate[4];
            double PassRate = QUANTITY == 0 ? 0 : (Math.Round((double)(PassCount * 100 / QUANTITY), 2));
            double FailRate = QUANTITY == 0 ? 0 : (Math.Round(100 - (double)(PassCount * 100 / QUANTITY), 2));
            double ReturnRate = QUANTITY == 0 ? 0 : (Math.Round((double)(ReturnCount * 100 / QUANTITY), 2));
            double SecPassRate = QUANTITY == 0 ? 0 : (Math.Round((double)(SecondPass * 100 / QUANTITY), 2));
            double DiscardRate = QUANTITY == 0 ? 0 : (Math.Round((double)(DiscardCount * 100 / QUANTITY), 2));

            res += "[\"" + "返工数量" + "\"," + ReturnCount + "],";
            res += "[\"" + "让步数量" + "\"," + SecondPass + "],";
            res += "[\"" + "报废数量" + "\"," + DiscardCount + "]]},";




        }
        res = res.Substring(0, res.Length - 1);
        res += "]}]";

        Context.Response.Write(res);
    }




    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryCustYield()
    {
        string month = Context.Request.Form["month"];


        int iYear = DateTime.Now.Year;
        if (string.IsNullOrEmpty(month) || month == "0")
        {
            month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
        }
        month = iYear + "-" + month;



        List<YieldInfo> bs = new List<YieldInfo>();

        IList<BasCustom> cus = _bal.FindCustName();
        if (cus == null || cus.Count == 0)
        {
            return;
        }
        for (int i = 0; i < cus.Count; i++)
        {
            int[] fails = _bal.FindCustQty(month, cus[i].NAME);

            YieldInfo bbtemp = new YieldInfo();
            bbtemp.WO = cus[i].NAME;
            bbtemp.QUANTITY = (fails[0] + fails[1]);
            bbtemp.PassCount = fails[0];
            bbtemp.FailCount = fails[1];
            bbtemp.ReturnCount = fails[2];
            bbtemp.SecondPass = fails[3];
            bbtemp.DiscardCount = fails[4];
            bbtemp.PassRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.FailRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round(100 - (double)(bbtemp.PassCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.ReturnRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.ReturnCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.SecPassRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.SecondPass * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bbtemp.DiscardRate = bbtemp.QUANTITY == 0 ? 0 + "%" : (Math.Round((double)(bbtemp.DiscardCount * 100 / bbtemp.QUANTITY), 2)).ToString() + "%";
            bs.Add(bbtemp);
        }

        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (bs != null & bs.Count > 0)
        {
            map.Add("total", bs.Count);
        }
        else
        {
            map.Add("total", 0);
        }
        map.Add("rows", bs);

        Context.Response.Write(JsonConvert.SerializeObject(map));

    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBindProductNameEasyUI()
    { 
        TextValueInfo[] tvis = _bal.ListBindProductName();
        string res = "[";
        for (int i = 0; i < tvis.Length; i++)
        {
            res += "{\"id\":\"" + tvis[i].Value + "\",\"text\":\"" + tvis[i].Text + "\"},";
        }

        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FailTypeProportion()
    {

        IList<FailItems> fi = _bal.FindFailType();
        if (fi == null || fi.Count == 0)
        {
            Context.Response.Write(null);
            return;
        }
        string res = "[";
        for (int i = 0; i < fi.Count; i++)
        {

            DataSet up = _bal.FindUPByFail(fi[i].FailType);
            if (up != null && up.Tables != null && up.Tables[0].Rows.Count > 0)
            {
                res += "{\"name\":\"" + fi[i].FailType + "\",\"data\":[";
                for (int j = 0; j < up.Tables[0].Rows.Count; j++)
                {
                    DateTime oldTime = new DateTime(1970, 1, 1);
                    double milliseconds = (Convert.ToDateTime(up.Tables[0].Rows[j]["CREATED_DATE"]) - oldTime).TotalMilliseconds;
                    res += "[" + milliseconds + ", " + up.Tables[0].Rows[j]["COUNT"] + "],";
                }
                res = res.Substring(0, res.Length - 1);
                res += "]},";

            }

        }

        res = res.Substring(0, res.Length - 1);
        res += "]";

        Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemovePartsDrawingInfo(string id)
    {
        _bal.RemovePartsDrawingInfo(id);//.RemoveBasEquipment(code);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveBanCi(BasBanci bbanci)
    {

        _bal.SaveBanCi(bbanci);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveBanCi(string ID)
    {
        _bal.RemoveBanCi(ID);
    }


    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWONew()
    {
        string workorder = Context.Request.Form["workorder"];
        string status = Context.Request.Form["status"];
        string partNo = Context.Request.Form["partNo"];
        string cust = Context.Request.Form["cust"];
        string worker = Context.Request.Form["worker"];
        string orderno = Context.Request.Form["orderno"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrder> objs = _bal.FindWONew(workorder, status, partNo, cust, worker, orderno);
        List<WorkOrder> bs = new List<WorkOrder>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrder bbtemp = new WorkOrder();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBindBanCi()
    {
        TextValueInfo[] t = _bal.ListBindBanCi();//铣床
       
        int i = 0;       
        string res = "[";
        for (i = 0; i < t.Length; i++)
        {
            if (i == 0)
            {
                res += "{\"id\":\"" + t[0].Value + "\",\"text\":\"" + t[0].Text + "\"}";
            }
            else
            {
                res += ",{\"id\":\"" + t[i].Value + "\",\"text\":\"" + t[i].Text + "\"}";
            }
        }
        res += "]";
        Context.Response.Write(res);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPlanEndTime(string starttime,string banci,string unittime,string qty)
    {
        string[] bancis = banci.Split(',');
       if((banci.IndexOf("正常班")!=-1&&banci.IndexOf("早班")!=-1)||(banci.IndexOf("正常班")!=-1&&banci.IndexOf("中班")!=-1))
        {
            throw new Exception("班次冲突，请重新选择");
        }
        return _bal.GetPlanEndTime(starttime, banci, unittime, qty);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUnitTimebyPartsDrawingCode(string PartsDrawingCode)
    {
        if(string.IsNullOrEmpty(PartsDrawingCode))
        {
            return "";
        }
        return _bal.GetUnitTimebyPartsDrawingCode(PartsDrawingCode);
    }
    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QuerySNTrackingWIP()
    {
        string workorder = Context.Request.Form["workorder"]; 
        string PartsdrawingCode = Context.Request.Form["PartsdrawingCode"];
        string station = Context.Request.Form["station"];
        string StartTime = Context.Request.Form["StartTime"];
        string EndTime = Context.Request.Form["EndTime"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"]; 
        IList<TrackingWip> objs = _bal.FindSNTrackingWIP(workorder, PartsdrawingCode, station, StartTime, EndTime);
        List<TrackingWip> bs = new List<TrackingWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TrackingWip bb in objs)
        {
            if (j > istart && j < iend)
            {
                TrackingWip bbtemp = new TrackingWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

      
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryMachineByType( )
    {
        string machinetype = Context.Request.Form["machines"];
        //string rows = Context.Request.Form["rows"];
        //string page = Context.Request.Form["page"];
        // return;
        string[] machines = machinetype.Split(',');
        List<TextValueInfo[]> ltvis = new List<TextValueInfo[]>() ;
        int totalcount = 0;
        for (int k = 0; k < machines.Length-1; k++)//最后一个分割符不算数
        {
            TextValueInfo[] tvi = _bal.ListBindMachines(machines[k]);
            ltvis.Add(tvi);
            totalcount += tvi.Length;
        }
          
        TextValueInfo[] t = new TextValueInfo[totalcount];// + t2.Length + t3.Length];
        int i = 0;
        foreach(TextValueInfo[] tvis in ltvis)
        {
            for(int j=0;j<tvis.Length;j++)
            {
                t[i] = new TextValueInfo();
                t[i].Value = tvis[j].Value;
                t[i].Text = tvis[j].Text;
                i++;
            }
        }
         
        string res = "[";
        for (i = 0; i < t.Length; i++)
        {
            if (i == 0)
            {
                res += "{\"id\":\"" + t[0].Value + "\",\"text\":\"" + t[0].Text + "\"}";
            }
            else
            {
                res += ",{\"id\":\"" + t[i].Value + "\",\"text\":\"" + t[i].Text + "\"}";
            }
        }
        res += "]";
        Context.Response.Write(res);
 
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DecryptUserCode(string EncryptUserCode)
    {
        if (string.IsNullOrEmpty(EncryptUserCode))
        {
            return "";
        }
        string decryptDes = JiaMiJieMiCls.DecryptDes(EncryptUserCode.Trim()).Trim();
        string code = decryptDes.Substring(0, 3);
        string other = decryptDes.Substring(3, decryptDes.Length - 3);
         
        return code;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TaskHandover(string EncryptUserCode,string ID)
    {
        if (string.IsNullOrEmpty(EncryptUserCode))
        {
            return "";
        }
        string decryptDes = JiaMiJieMiCls.DecryptDes(EncryptUserCode.Trim()).Trim();
        string code = decryptDes.Substring(0, 3);
        string other = decryptDes.Substring(3, decryptDes.Length - 3);

        string res = _bal.TaskHandover(code, ID);
        return res;
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveUserPub(SysUserPub user)
    {
        _bal.SaveUserPub(user);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveUserPub(string userCode)
    {
        _bal.RemoveUserPub(userCode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindUsersPub()
    {
        string userCode = Context.Request.Form["userCode"];
        string userName = Context.Request.Form["userName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<SysUserPub> objs = _bal.FindPubUsers(userCode, userName);
        List<SysUserPub> bs = new List<SysUserPub>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (SysUserPub bb in objs)
        {
            if (j > istart && j < iend)
            {
                SysUserPub bbtemp = new SysUserPub();
                bbtemp = bb;              
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
         
    }

    /// <summary>
    /// 查询基本编码及名称
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBaseNameEasyUI()
    {
       // TextValueInfo[] tvi = _bal.ListBaseName();
        TextValueInfo[] tvis = _bal.ListBaseName();
        string res = "[";
        for (int i = 0; i < tvis.Length; i++)
        {
            res += "{\"id\":\"" + tvis[i].Value + "\",\"text\":\"" + tvis[i].Text + "\"},";
        }

        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void CloseOrderInfo(string orderno)
    {
        _bal.CloseOrderInfo(orderno);
    }

    /// <summary>
    /// 查询未分派工艺的零件图号
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindDrawingCodeToTechnology()
    {
       string drawingno = Context.Request.Form["drawingno"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TechnologyWip> objs = _bal.FindDrawingCodeToTechnology(drawingno);
        List<TechnologyWip> bs = new List<TechnologyWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TechnologyWip bb in objs)
        {
            if (j > istart && j < iend)
            {
                TechnologyWip bbtemp = new TechnologyWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        
    }
    /// <summary>
    /// 查询进行中的任务
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindNoFinishTechnology()
    {
        string drawingno = Context.Request.Form["drawingno"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TechnologyWip> objs = _bal.FindNoFinishTechnology(drawingno);
        List<TechnologyWip> bs = new List<TechnologyWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TechnologyWip bb in objs)
        {
            if (j > istart && j < iend)
            {
                TechnologyWip bbtemp = new TechnologyWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

    }
    /// <summary>
    /// 查询角色人员
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListUserByRoleEasyUI()
    {
        // TextValueInfo[] tvi = _bal.ListBaseName();
        TextValueInfo[] tvis = _bal.ListUserByRole("技术员");
        string res = "[";
        for (int i = 0; i < tvis.Length; i++)
        {
            res += "{\"id\":\"" + tvis[i].Value + "\",\"text\":\"" + tvis[i].Text + "\"},";
        }

        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveTechnologyInfo(TechnologyWip techinfo)
    {
        _bal.SaveTechnologyInfo(techinfo);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void RemoveTechnologyInfo(string ID)
    {
        _bal.RemoveTechnologyInfo(ID);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryTechnologyTask()
    {
        string code = Context.Request.Form["PartsDrawingNo"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TechnologyWip> baseInfo = _bal.FindTechnologyTask(code);
        List<TechnologyWip> bs = new List<TechnologyWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TechnologyWip bb in baseInfo)
        {
            if (j > istart && j < iend)
            {
                TechnologyWip bbtemp = new TechnologyWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null & baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 查询工艺信息
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryTechnology()
    {
        string partsdrawingno = Context.Request.Form["PartsDrawingNo"];
        string status = Context.Request.Form["Status"];
        string engineer = Context.Request.Form["Engineer"];
        string starttime = Context.Request.Form["StartTime"];
        string endtime = Context.Request.Form["EndTime"];       
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TechnologyInfo> baseInfo = _bal.FindTechnologyHistory(partsdrawingno, status, engineer, starttime, endtime);
        List<TechnologyInfo> bs = new List<TechnologyInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TechnologyInfo bb in baseInfo)
        {
            if (j > istart && j < iend)
            {
                TechnologyInfo bbtemp = new TechnologyInfo();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null & baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    /// <summary>
    /// 根据角色查用户
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListUserByRole()
    {
        return _bal.ListUserByRole("技术员");
    }

    /// <summary>
    /// 查询工艺任务
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryTechnologyEngineerTask()
    {        
        string status = Context.Request.Form["Status"];
        string PDNo = Context.Request.Form["PartsDrawingNo"];
        string engineer = this._userInfo.UserCode;       
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TechnologyWip> baseInfo = _bal.FindTechnology(PDNo, status, engineer, "", "");
        List<TechnologyWip> bs = new List<TechnologyWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TechnologyWip bb in baseInfo)
        {
            if (j > istart && j < iend)
            {
                TechnologyWip bbtemp = new TechnologyWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null & baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 查询零件图号给工艺人员
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListPartDrawingNoToTechEasyUI()
    {
        // TextValueInfo[] tvi = _bal.ListBaseName();
        string status = "1,9";
        string engineer = this._userInfo.UserCode;       
        IList<TechnologyWip> baseInfo = _bal.FindTechnology("", status, engineer, "", "");
        string res = "[";
        foreach (TechnologyWip bb in baseInfo)
        {
 
            res += "{\"id\":\"" +bb.PARTSDRAWINGNO + "\",\"text\":\"" + bb.PARTSDRAWINGNO + "\"},";
        }

        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveTechnologyTask(TechnologyWip techinfo)
    {
        TechnologyWip techif = _bal.FindTechnologyTaskByPD(techinfo.PARTSDRAWINGNO);
        if(techif!=null)
        {
            techif.ProcessFname = techinfo.ProcessPath.Substring(techinfo.ProcessPath.LastIndexOf("\\")+1);
            techif.ProcessPath = techinfo.ProcessPath;
            techif.STATUS = 2;
            techif.StatusMemo = "工艺已提交";
            techif.RealDate = DateTime.Now;
            _bal.SaveTechnologyInfo(techif);
        }
        
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryPDNoInfo(string partsdrawingno)
    {
        TechnologyWip techif = _bal.FindTechnologyTaskByPD(partsdrawingno);
        if (techif != null)
        {
            string res = techif.CustName + "," + techif.ProductName + "," + techif.PlanDate.ToString()+","+techif.STATUS+","+techif.StatusMemo;
            return res;
        }
        return "";
    }

    /// <summary>
    /// 查询路由详细信息
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryRouteDetails()
    { 
        string PDNo = Context.Request.Form["PartsDrawingNo"];  
        IList<RouteDetails> baseInfo = _bal.FindRouteDetails(PDNo);
        List<RouteDetails> bs = new List<RouteDetails>();
        if (baseInfo != null)
        {
            foreach (RouteDetails bb in baseInfo)
            {
                RouteDetails bbtemp = new RouteDetails();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);

            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null && baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListBindMachineTypeToRoute()
    {
        // return _bal.ListBindMachineType();
        TextValueInfo[] tvi = _bal.ListBindMachineType();
        string res = "[";
        for (int i = 0; i < tvi.Length; i++)
        {
            if (i == 0)
            {
                res += "{\"MactypeCode\":\"" + tvi[0].Value + "\",\"MachineType\":\"" + tvi[0].Text + "\"}";
            }
            else
            {
                res += ",{\"MactypeCode\":\"" + tvi[i].Value + "\",\"MachineType\":\"" + tvi[i].Text + "\"}";
            }
        }
        res += "]";
        Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveRouteInfo(string partsdrawingno,string routes)
    {        
        DataTable dt = JsonToDataTable(routes);
        return  _bal.SaveRouteInfo(partsdrawingno, dt);
     
    }
    private DataTable JsonToDataTable(string strJson)
    {
        //转换json格式
        strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
        //取出表名   
        var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
        string strName = rg.Match(strJson).Value;
        DataTable tb = null;
        //去除表名   
        strJson = strJson.Substring(strJson.IndexOf("[") + 1);
        strJson = strJson.Substring(0, strJson.IndexOf("]"));
        //获取数据   
        rg = new Regex(@"(?<={)[^}]+(?=})");
        MatchCollection mc = rg.Matches(strJson);
        for (int i = 0; i < mc.Count; i++)
        {
            string strRow = mc[i].Value;
            string[] strRows = strRow.Split('*');
            //创建表   
            if (tb == null)
            {
                tb = new DataTable();
                tb.TableName = strName;
                foreach (string str in strRows)
                {
                    var dc = new DataColumn();
                    string[] strCell = str.Split('#');
                    if (strCell[0].Substring(0, 1) == "\"")
                    {
                        int a = strCell[0].Length;
                        dc.ColumnName = strCell[0].Substring(1, a - 2);
                    }
                    else
                    {
                        dc.ColumnName = strCell[0];
                    }
                    tb.Columns.Add(dc);
                }
                tb.AcceptChanges();
            }
            //增加内容   
            DataRow dr = tb.NewRow();
            for (int r = 0; r < strRows.Length; r++)
            {
                dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
            }
            tb.Rows.Add(dr);
            tb.AcceptChanges();
        }
        return tb;
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string AgreeTechnology(string obj)
    {
        return _bal.CheckTechnology(obj,true);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string RejectTechnology(string obj)
    {
        return _bal.CheckTechnology(obj,false);
    }

    /// <summary>
    /// 查询工艺任务
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryTechnologyInfo()
    {
        string status = Context.Request.Form["Status"];
        string PDNo = Context.Request.Form["PartsDrawingNo"];
        string engineer = Context.Request.Form["engineer"]; 
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TechnologyWip> baseInfo = _bal.FindTechnology(PDNo, status, engineer, "", "");
        List<TechnologyWip> bs = new List<TechnologyWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TechnologyWip bb in baseInfo)
        {
            if (j > istart && j < iend)
            {
                TechnologyWip bbtemp = new TechnologyWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null & baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 查询零件图号给编程人员
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListPartDrawingNoToDevlopEasyUI()
    {
        // TextValueInfo[] tvi = _bal.ListBaseName();
        string status = "4,10";
        string engineer = this._userInfo.UserCode;
        IList<TechnologyWip> baseInfo = _bal.FindTechnology("", status, engineer, "", "");
        string res = "[";
        foreach (TechnologyWip bb in baseInfo)
        {

            res += "{\"id\":\"" + bb.PARTSDRAWINGNO + "\",\"text\":\"" + bb.PARTSDRAWINGNO + "\"},";
        }

        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);
    }
    /// <summary>
    /// 查询工序
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryStationsByPDNo()
    {
        string PDNo = Context.Request.Form["PartsDrawingNo"]; 
        IList<RouteDetails> baseInfo = _bal.FindStationsByPDNo(PDNo);
        string res = "[";
        foreach (RouteDetails bb in baseInfo)
        {

            res += "{\"id\":\"" + bb.StationId + "\",\"text\":\"" + bb.StationName + "\"},";
        }

        res = res.Substring(0, res.Length - 1);
        res += "]";
        Context.Response.Write(res);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveStationInfo(ProgramInfo techinfo)
    {
        TechnologyWip techif = _bal.FindTechnologyTaskByPD(techinfo.PARTSDRAWINGNO);
        if (techif != null)
        {
            techinfo.ProgramFname = techinfo.ProgramPath.Substring(techinfo.ProgramPath.LastIndexOf("\\") + 1);
            techinfo.CustCode = techif.CustCode;
            techinfo.ProductCode = techif.ProductCode;
            techinfo.StatusMemo = "提交任务";
            techinfo.STATUS = "1";
            techinfo.ProgramEngineer = techif.ProgramEngineer;
            techinfo.ProgramName = techif.ProgramName;
            techinfo.ToolName = techinfo.ToolPath.Substring(techinfo.ToolPath.LastIndexOf("\\") + 1);
            techinfo.PlanDate = techif.DevplanDate;
            techinfo.RealDate = DateTime.Now;
            _bal.SaveStationInfo(techinfo);
        }

    }
    /// <summary>
    /// 查询编程任务
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryDevlopEngineerTask()
    {
        string status = Context.Request.Form["Status"];
        string PDNo = Context.Request.Form["PartsDrawingNo"];
        string engineer = this._userInfo.UserCode;
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<TechnologyWip> baseInfo = _bal.FindTechnology(PDNo, status, engineer, "", "");
        List<TechnologyWip> bs = new List<TechnologyWip>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (TechnologyWip bb in baseInfo)
        {
            if (j > istart && j < iend)
            {
                TechnologyWip bbtemp = new TechnologyWip();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null & baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveDevelopInfo(TechnologyWip techinfo)
    {
        TechnologyWip techif = _bal.FindTechnologyTaskByPD(techinfo.PARTSDRAWINGNO);
        if (techif != null)
        {            
            techif.STATUS =5;
            techif.StatusMemo = "编程已提交";
            techif.DevrealDate = DateTime.Now;
            _bal.SaveTechnologyInfo(techif);
        }

    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string AgreeDevelop(string obj)
    {
        return _bal.CheckDevelop(obj, true);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string RejectDevelop(string obj)
    {
        return _bal.CheckDevelop(obj, false);
    }

    /// <summary>
    /// 查询编程工程师的工序信息
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryDevelopmentDetails()
    { 
        string PDNo = Context.Request.Form["PartsDrawingNo"];        
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        
        IList<ProgramInfo> baseInfo = _bal.FindDevelopmentDetails(PDNo);
        List<ProgramInfo> bs = new List<ProgramInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (ProgramInfo bb in baseInfo)
        {
            if (j > istart && j < iend)
            {
                ProgramInfo bbtemp = new ProgramInfo();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null & baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 查询哦用户是否存在
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryExistUserNameByCode(string usercode)
    {
       string res = FindUserNameByCode(usercode);
        return res;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FindUserCodeByUserName(string userName)
    {
        string res = string.Empty;
        if(userName.IndexOf(',')!=-1)
        {
            string[] usernames = userName.Split(',');
            for(int i=0;i<usernames.Length;i++)
            {
                SysUser su = _bal.FindUserByUserName(usernames[i].ToString());
                if (su != null)
                {
                    res += su.UserCode + ",";
                }
            }
            if(res!=string.Empty)
            {
                res = res.Substring(0, res.Length - 1);
            }
        }
        else
        {
            SysUser su = _bal.FindUserByUserName(userName);
            if (su != null)
            {
                res = su.UserCode;
            }
        }
        return res;
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveWorkOrderAssign(WorkOrder obj)
    {
        if (string.IsNullOrEmpty(obj.WO))
        {
            return "工单单号不能为空";
        }
        
        if (string.IsNullOrEmpty(obj.WorkerName))
        {
            return "负责人不能为空";
        }
          
        _bal.UpdateWorkOrderAssign(obj);
        return "OK";
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWIPInfoByCondition()
    {
       // string workorder = Context.Request.Form["workorder"];
        string partsdrawingcode = Context.Request.Form["partsdrawingcode"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrder> objs = _bal.FindWorkOrderByStatus("1", true, partsdrawingcode);//查询运行中的工单
        RealtimeStatistics rs = new RealtimeStatistics();
        List<WipInfo> bs = new List<WipInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WipInfo bbtemp = new WipInfo();
                bbtemp.WO = bb.WO;
                bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                bbtemp.ProductName = bb.ProductName;
                bbtemp.PlanQuantity = bb.PlanQuantity == null ? 0 : (int)bb.PlanQuantity;
                rs.WorkOrder = bb.WO;
                rs.StationName = "CHEXI";
                rs.STATUS = "P";
                int chexipass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.CheXiPassCount = chexipass;
                rs.STATUS = "F";
                int chexifail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.CheXiFailCount = chexifail;
                rs.StationName = "QIANGONG";
                int qiangongfail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QianGongFailCount = qiangongfail;
                rs.STATUS = "P";
                int qiangongpass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QianGongPassCount = qiangongpass;
                rs.StationName = "QC";
                int qcpass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QCPassCount = qcpass;
                rs.STATUS = "F";
                int qcfail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QCFailCount = qcfail;
                rs.StationName = "INSTOCK";
                rs.STATUS = "";
                int instockqty = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.InStockQty = instockqty;
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
         
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveTechnologyInfoForImport(TechnologyWip techinfo)
    {
        _bal.SaveTechnologyInfoForImport(techinfo);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWIPInfoForOrderByCondition()
    {
         string batchnumber = Context.Request.Form["batchnumber"];
        string productname = Context.Request.Form["productname"];
        string partsdrawingcode = Context.Request.Form["partsdrawingcode"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrder> objs = _bal.FindWorkOrder("1", true, partsdrawingcode, productname, batchnumber);//查询运行中的工单
        RealtimeStatistics rs = new RealtimeStatistics();
        List<WipInfo> bs = new List<WipInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                WipInfo bbtemp = new WipInfo();
                bbtemp.WO = bb.WO;
                bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                bbtemp.ProductName = bb.ProductName;
                bbtemp.PlanQuantity = bb.PlanQuantity == null ? 0 : (int)bb.PlanQuantity;
                rs.WorkOrder = bb.WO;
                rs.StationName = "CHEXI";
                rs.STATUS = "P";
                int chexipass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.CheXiPassCount = chexipass;
                rs.STATUS = "F";
                int chexifail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.CheXiFailCount = chexifail;
                rs.StationName = "QIANGONG";
                int qiangongfail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QianGongFailCount = qiangongfail;
                rs.STATUS = "P";
                int qiangongpass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QianGongPassCount = qiangongpass;
                rs.StationName = "QC";
                int qcpass = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QCPassCount = qcpass;
                rs.STATUS = "F";
                int qcfail = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.QCFailCount = qcfail;
                rs.StationName = "INSTOCK";
                rs.STATUS = "";
                int instockqty = _bal.FindCountFromRealtimeStatistics(rs);
                bbtemp.InStockQty = instockqty;
                bbtemp.BatchNumber = bb.BatchNumber;
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map)); 
    }
    /// <summary>
    /// 查询工艺信息
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string  QueryTechnologyStatus(string partsdrawingno)
    { 
        IList<TechnologyWip> baseInfo = _bal.FindTechnologyTask(partsdrawingno);
         
        if (baseInfo != null && baseInfo.Count > 0)
        {
            return baseInfo[0].StatusMemo.ToString();
        }
        else
        {
            return "无此工艺任务";
        }
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryYieldInfo2()
    {
        string custname = Context.Request.Form["custname"];
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string startT = Context.Request.Form["startT"];
        string endT = Context.Request.Form["endT"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        DateTime dtstart = DateTime.Now.Date;
        DateTime dtend = DateTime.Now;
        WorkOrder wo = new WorkOrder();
        wo.CustName = custname;
        wo.PartsdrawingCode = partsdrawing;
        if (!string.IsNullOrEmpty(startT))
        {
            wo.StartTime = Convert.ToDateTime(startT);
        }
        if (!string.IsNullOrEmpty(endT))
        {
            wo.EndTime = Convert.ToDateTime(endT);
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<YieldInfo> bs = new List<YieldInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrder bb in objs)
        {
            if (j > istart && j < iend)
            {
                YieldInfo bbtemp = new YieldInfo();
                bbtemp.WO = bb.WO;
                bbtemp.CustName = bb.CustName;
                bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                bbtemp.ProductName = bb.ProductName;
                bbtemp.BatchNumber = bb.BatchNumber;
                bbtemp.QUANTITY = bb.QUANTITY == null ? 0 : (int)bb.QUANTITY;
                int[] fails = _bal.FindYieldCountInfo(bb.WO, bb.PartsdrawingCode);
                bbtemp.PassCount = (int)(bb.QUANTITY == null ? 0 : bb.QUANTITY) - fails[0];
                bbtemp.FailCount = fails[0];
                bbtemp.ReturnCount = fails[1];
                bbtemp.SecondPass = fails[2];
                bbtemp.DiscardCount = fails[3];
                bbtemp.PassRate = (Math.Round((double)(bbtemp.PassCount * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.FailRate = (Math.Round((double)(fails[0] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.ReturnRate = (Math.Round((double)(fails[1] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.SecPassRate = (Math.Round((double)(fails[2] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bbtemp.DiscardRate = (Math.Round((double)(fails[3] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                bs.Add(bbtemp);
            }
            j++;
        }
        List<YieldInfo> bsres = new List<YieldInfo>();
        if (bs != null && bs.Count > 0)
        {
           
            string drawingnos = string.Empty;
            foreach (YieldInfo bb in bs)
            {
                if(string.IsNullOrEmpty(drawingnos))
                {
                    drawingnos = bb.PartsdrawingCode;
                    YieldInfo bb_temp = new YieldInfo();
                    bb_temp.WO = bb.WO;
                    bb_temp.CustName = bb.CustName;
                    bb_temp.PartsdrawingCode = bb.PartsdrawingCode;
                    bb_temp.ProductName = bb.ProductName;
                    bb_temp.BatchNumber = bb.BatchNumber;
                    int[] fails_temp ={ 0, 0, 0, 0 };
                    foreach (YieldInfo bbb in bs)
                    {
                        if(bbb.PartsdrawingCode== drawingnos)
                        {
                            bb_temp.QUANTITY += (int)bbb.QUANTITY;
                            int[] fails_temp1 = _bal.FindYieldCountInfo(bbb.WO, bbb.PartsdrawingCode);
                            bb_temp.PassCount += (int)bbb.QUANTITY - fails_temp1[0];
                            bb_temp.FailCount += fails_temp1[0];
                            bb_temp.ReturnCount += fails_temp1[1];
                            bb_temp.SecondPass += fails_temp1[2];
                            bb_temp.DiscardCount += fails_temp1[3];
                        }
                        
                    }
                    bb_temp.PassRate = (Math.Round((double)(bb_temp.PassCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                    bb_temp.FailRate = (Math.Round((double)(bb_temp.FailCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                    bb_temp.ReturnRate = (Math.Round((double)(bb_temp.ReturnCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                    bb_temp.SecPassRate = (Math.Round((double)(bb_temp.SecondPass * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                    bb_temp.DiscardRate = (Math.Round((double)(bb_temp.DiscardCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                    bsres.Add(bb_temp);
                }
                else
                {
                    if(drawingnos.IndexOf(bb.PartsdrawingCode.ToString())!=-1)
                    {
                        continue;
                    }
                    else
                    {
                        drawingnos +=","+ bb.PartsdrawingCode;
                        YieldInfo bb_temp = new YieldInfo();
                        bb_temp.WO = bb.WO;
                        bb_temp.CustName = bb.CustName;
                        bb_temp.PartsdrawingCode = bb.PartsdrawingCode;
                        bb_temp.ProductName = bb.ProductName;
                        bb_temp.BatchNumber = bb.BatchNumber;
                        int[] fails_temp = { 0, 0, 0, 0 };
                        foreach (YieldInfo bbb in bs)
                        {
                            if (bbb.PartsdrawingCode == bb.PartsdrawingCode)
                            {
                                bb_temp.QUANTITY += (int)bbb.QUANTITY;
                                int[] fails_temp1 = _bal.FindYieldCountInfo(bbb.WO, bbb.PartsdrawingCode);
                                bb_temp.PassCount += (int)bbb.QUANTITY - fails_temp1[0];
                                bb_temp.FailCount += fails_temp1[0];
                                bb_temp.ReturnCount += fails_temp1[1];
                                bb_temp.SecondPass += fails_temp1[2];
                                bb_temp.DiscardCount += fails_temp1[3];
                            }

                        }
                        bb_temp.PassRate = (Math.Round((double)(bb_temp.PassCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                        bb_temp.FailRate = (Math.Round((double)(bb_temp.FailCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                        bb_temp.ReturnRate = (Math.Round((double)(bb_temp.ReturnCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                        bb_temp.SecPassRate = (Math.Round((double)(bb_temp.SecondPass * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                        bb_temp.DiscardRate = (Math.Round((double)(bb_temp.DiscardCount * 100 / (bb_temp.QUANTITY == 0 ? 1 : bb_temp.QUANTITY)), 2)).ToString() + "%";
                        bsres.Add(bb_temp);
                    }
                } 
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (bsres != null & bsres.Count > 0)
            map.Add("total", bsres.Count);
        map.Add("rows", bsres);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 查询工序信息
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryRouteInoDetails()
    {
        string PDNo = Context.Request.Form["PartsDrawingNo"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];

        IList<RouteDetails> baseInfo = _bal.FindRouteDetails(PDNo);
        List<RouteDetails> bs = new List<RouteDetails>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        if (baseInfo != null)
        {
            foreach (RouteDetails bb in baseInfo)
            {
                if (j > istart && j < iend)
                {
                    RouteDetails bbtemp = new RouteDetails();
                    bbtemp = bb;
                    bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                    bs.Add(bbtemp);
                }
                j++;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null && baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 查询编程分配工时任务
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryDevlopRouteTask()
    { 
        string PDNo = Context.Request.Form["PartsDrawingNo"];
        string engineer = this._userInfo.UserCode;
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<ProgramInfo> baseInfo = _bal.FindProgramInfo(PDNo);
        List<ProgramInfo> bs = new List<ProgramInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        if (baseInfo != null)
        {
            foreach (ProgramInfo bb in baseInfo)
            {
                if (j > istart && j < iend)
                {
                    ProgramInfo bbtemp = new ProgramInfo();
                    bbtemp = bb;
                    bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                    bs.Add(bbtemp);
                }
                j++;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (baseInfo != null && baseInfo.Count > 0)
            map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 根据图号查询机床类型
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryMachineTypeByDrawingNo(string partsdrawingno,string stationid="")
    {
        IList<RouteDetails> baseInfo = _bal.FindRouteDetails(partsdrawingno, stationid);
        string res = string.Empty;
        if (baseInfo != null && baseInfo.Count > 0)
        {
            for(int i=0;i<baseInfo.Count;i++)
            {
                if(baseInfo[i].MachineType!=null&&baseInfo[i].MachineType.ToString()!="null")
                {
                    res += baseInfo[i].MachineType.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(res))
            {
                res = res.Substring(0, res.Length - 1);
            }
            return res;
        }
        else
        {
            return res;
        }
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryProductManageInfo()
    {
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string workorder = Context.Request.Form["workorder"];
        string operators = Context.Request.Form["operators"];
        string equip = Context.Request.Form["equip"];
        string startT = Context.Request.Form["startT"];
        string endT = Context.Request.Form["endT"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        DateTime dtstart = DateTime.Now.Date;
        DateTime dtend = DateTime.Now;
        WorkOrder wo = new WorkOrder();
        wo.PartsdrawingCode = partsdrawing;
        wo.WO = workorder;
        wo.WORKER = operators;
        wo.MachineName = equip; 
        if (!string.IsNullOrEmpty(startT))
        {
            wo.StartTime = Convert.ToDateTime(startT);
        }
        if (!string.IsNullOrEmpty(endT))
        {
            wo.EndTime = Convert.ToDateTime(endT);
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);
        List<ProductManageInfo> bs = new List<ProductManageInfo>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        if (objs != null)
        {
            foreach (WorkOrder bb in objs)
            {
                if (j > istart && j < iend)
                {
                    ProductManageInfo bbtemp = new ProductManageInfo();
                    bbtemp.WO = bb.WO;
                    bbtemp.CustName = bb.CustName;
                    bbtemp.PartsdrawingCode = bb.PartsdrawingCode;
                    bbtemp.OrderNo = bb.OrderNumber;
                    bbtemp.Operator = bb.WORKER;
                    bbtemp.OperatorName = bb.WorkerName;
                    bbtemp.ProductName = bb.ProductName;
                    bbtemp.BatchNumber = bb.BatchNumber;
                    bbtemp.EquipName = bb.MachineName;
                    bbtemp.PlanUnitTime = bb.UnitTime.ToString();
                    bbtemp.PlanQuantity =(int) bb.PlanQuantity;
                    bbtemp.QUANTITY = bb.QUANTITY == null ? 0 : (int)bb.QUANTITY;
                    bbtemp.PlanTotalUnitTime = (bbtemp.QUANTITY * bb.UnitTime).ToString();//计划总工时=生产数量*单件计划工时
                    bbtemp.ActualTotalUnitTime = _bal.FindActualTotalUnitTime(bb.WO, bb.PartsdrawingCode);
                    int[] fails = _bal.FindYieldCountInfo(bb.WO, bb.PartsdrawingCode);
                    bbtemp.PassCount = (int)(bb.QUANTITY == null ? 0 : bb.QUANTITY) - fails[0];
                    //bbtemp.FailCount = fails[0];
                    //bbtemp.ReturnCount = fails[1];
                    //bbtemp.SecondPass = fails[2];
                    bbtemp.DiscardCount = fails[3];
                    //bbtemp.PassRate = (Math.Round((double)(bbtemp.PassCount * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                    //bbtemp.FailRate = (Math.Round((double)(fails[0] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                    //bbtemp.ReturnRate = (Math.Round((double)(fails[1] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                    //bbtemp.SecPassRate = (Math.Round((double)(fails[2] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                    //bbtemp.DiscardRate = (Math.Round((double)(fails[3] * 100 / (bb.QUANTITY == null ? 1 : bb.QUANTITY)), 2)).ToString() + "%";
                    bs.Add(bbtemp);
                }
                j++;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    /// <summary>
    /// 查询工序号
    /// </summary>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListStationNamesEasyUI()
    {
        string partsdrawingno = Context.Request.Form["partsdrawingno"];
        IList<RouteDetails> objs = _bal.FindStationsByPDNo(partsdrawingno);
        string res = "[";
        if (objs != null && objs.Count > 0)
        {
            TextValueInfo[] tvis = new TextValueInfo[objs.Count];
            string temp_stationid = "0";
            for (int i = 0; i < tvis.Length; i++)
            {

                if (objs[i].StationId != null)
                {
                    temp_stationid = objs[i].StationId.ToString();
                }
                res += "{\"id\":\"" + temp_stationid + "\",\"text\":\"" + objs[i].StationName.ToString() + "\"},";
            }

            res = res.Substring(0, res.Length - 1);
            
        }
        res += "]";
        Context.Response.Write(res);
    }
    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWODetailsNew()
    {
        string workorder = Context.Request.Form["workorder"];
        string status = Context.Request.Form["status"];
        string partNo = Context.Request.Form["partNo"];
        string cust = Context.Request.Form["cust"];
        string worker = Context.Request.Form["worker"];
        string orderno = Context.Request.Form["orderno"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<WorkOrderDetails> objs = _bal.FindWODetailsNew(workorder, status, partNo, cust, worker, orderno);
        List<WorkOrderDetails> bs = new List<WorkOrderDetails>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (WorkOrderDetails bb in objs)
        {
            if (j > istart && j < iend)
            {
                WorkOrderDetails bbtemp = new WorkOrderDetails();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));

    }
    [WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryWODetailsNewBySubQuery()
    {
        string workorder = Context.Request["workorder"];
        IList<WorkOrderDetails> objs = _bal.FindWODetailsNew(workorder, "", "", "", "", "");
        Context.Response.Write(JsonConvert.SerializeObject(objs)); 
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public TextValueInfo[] ListBindStations(string workorder)
    {
        return _bal.ListBindStations(workorder);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveWorkOrderAssign(WorkOrderDetails obj)
    {
        if (string.IsNullOrEmpty(obj.WO))
        {
            return "工单单号不能为空";
        }

        if (string.IsNullOrEmpty(obj.WorkerName))
        {
            return "负责人不能为空";
        }
        if (string.IsNullOrEmpty(obj.RouteCode))
        {
            return "工序号不能为空";
        }

        _bal.UpdateWorkOrderAssign(obj);
        return "OK";
    }
    /// <summary>
    /// 处理报工的扫描数据:cancel,psn,result
    /// </summary>
    /// <param name="inputdata"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DealwithScanDataNew(string inputdata, string ID)
    {
        TrackingTemp tt = null; 
        string strID = string.Empty;
        IList<TrackingWip> gtws = null;
        if (!string.IsNullOrEmpty(ID))
        {
            tt = _bal.FindTrackingTemp(ID);
            if (tt != null)
            {
                gtws = _bal.FindTrackingWip(tt.PSN);
            }
            //tt = _bal.FindTrackingTemp(ID);
            //gtws = _bal.QueryTrackingWip(tt.PSN.ToString());//查出来可能有多个记录
        }
        switch (inputdata)
        {
            case "CANCEL":
            case "cancel":
                _bal.RemoveTrackingTemp(ID);
                break;
            case "PASS": //合格
            case "pass": //合格
                //判断条码是否已扫
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("请先扫描条码！");
                }
                DealwithScanRes(gtws, tt, "P"); 
                break; 
            case "FAIL":
            case "fail":
                //判断条码是否已扫
                if (string.IsNullOrEmpty(ID))
                {
                    throw new Exception("请先扫描条码！");
                }
                DealwithScanRes(gtws, tt, "F"); 
                break;
            case "pause":
                tt.UpdatedDate = DateTime.Now;
                if (string.IsNullOrEmpty(tt.TaskTime))
                {
                    tt.TaskTime = (tt.UpdatedDate - tt.CreatedDate).ToString();
                }
                else
                {
                    //计算有暂停情况的工时
                    TimeSpan tempPauseTaskTime = TimeSpan.ParseExact(tt.TaskTime, @"hh\:mm\:ss\.fffffff", null);
                    TimeSpan tempNewTaskTime = (TimeSpan)(tt.UpdatedDate - tt.CreatedDate);
                    tt.TaskTime = (tempPauseTaskTime + tempNewTaskTime).ToString();
                }
                tt.STEP = "pause";
                _bal.SaveTrackingTemp(tt);
                break;
            default:
                //表示扫描的是条码，因为条码长度为12位，随批单长度为9位
                //if (inputdata.Length == 9|| inputdata.Length == 12)
                //{
                inputdata = inputdata.ToUpper().Trim();
                IList<TrackingWip> tws = _bal.FindTrackingWip(inputdata);

                if (tws != null && tws.Count > 0)
                {
                    //检查是否重复扫描(1.看tracking_temp表的psn有没有记录;2.看tracking_history表有没有记录,防止跳站)
                    if (tt != null)
                    {
                        if (tt.PSN.ToString().IndexOf(inputdata) != -1)
                        {
                            throw new Exception("该条码已扫描，禁止重复扫描!");
                        }
                    }
                    //更新wip信息，切换为本站信息，即更新班组,工单，工站，工站ID，下一工站信息
                    //查找任务单
                    // string temp_nextstation = tws[0].NextStation == null ? "" : tws[0].NextStation.ToString();
                    //增加检验部分 begin
                    //if (temp_nextstation.IndexOf("检验") != -1)
                    //{
                    //    throw new Exception("该产品未检验，请去" + temp_nextstation + "工序！");
                    //}
                    //end 
                    //验证是否待处理品未审核
                    if (!_bal.CheckUnSurenessOut(inputdata))
                    {
                        throw new Exception("该产品待处理中未审核,请审核完后过本站");
                    }
                    IList<WorkOrderDetails> wos = _bal.FindWorkOrderDetailsInfo(tws[0].WorkOrder, tws[0].NextStationId);
                    if (wos != null && wos.Count > 0)
                    {
                        //检查任务单状态是否运行中
                        if (wos[0].STATUS.ToString() != "1")
                        {
                            throw new Exception("该产品的任务单" + wos[0].WO.ToString() + "没有运行，请联系调度！");
                        }
                        //更新wip信息
                        //检查本站是否已经扫过:如果下一站的信息跟本站相同，表明已经扫描过了
                        if (tws[0].WorkOrder == wos[0].WO && tws[0].StationId == wos[0].RouteCode
                            && tws[0].StationName == wos[0].StationName)
                        {
                            //检查扫描结果，如果扫描结果是合格，超差，降级，则不能在本站扫描，其他情况允许在本站继续扫描
                            if (tws[0].STATUS.ToString() == "P")
                            {
                                throw new Exception("已过本站：" + tws[0].StationName + "！");
                            }
                        }
                        //检查本工站的操作员是否正确
                        if (wos[0].WORKER.ToString().IndexOf(this._userInfo.UserCode.ToUpper()) == -1)
                        {
                            throw new Exception("本产品" + tws[0].NextStation + "工序未过，请" + wos[0].WorkerName + "报工！");
                        } 
                        //检查是否已走完流程
                        if (tws[0].NextStation != null && tws[0].NextStation.ToString() == "end")
                        {
                            throw new Exception("该产品流程已走完，请及时入库!");
                        }
                        //更新下一站信息
                        tws[0].WorkOrder = wos[0].WO;
                        tws[0].StationId = wos[0].RouteCode;
                        tws[0].StationName = wos[0].StationName;
                        string[] stationtemp = _bal.GetNextStation(wos[0].WO, wos[0].RouteCode);
                        tws[0].NextStation = stationtemp[0];
                        tws[0].NextStationId = stationtemp[1];  
                        tws[0].InStatioonTime = DateTime.Now;
                        tws[0].BatchNumber = wos[0].BatchNumber;
                        
                    }
                    else
                    {
                        if (tws[0].NextStation != null && tws[0].NextStation.ToString() == "end")
                        { 
                           throw new Exception("该产品流程已走完，请及时入库!"); 
                        }
                        else
                        {
                            if (tws[0].NextStation.ToString() == "INSTOCK")
                            {
                                throw new Exception("该产品该入库了，请及时入库！");
                            }
                            else
                            {
                                throw new Exception("没有该产品在工序" + tws[0].NextStation.ToString() + "运行中的任务单，请联系调度！");
                            }
                        }
                    }
                    
                    if (tt == null)//说明扫描的是第一个条码
                    {
                        TrackingTemp ttemp = new TrackingTemp();
                        ttemp.PSN = inputdata;
                        ttemp.WorkOrder = tws[0].WorkOrder;
                        ttemp.PartsdrawingCode = tws[0].PartsdrawingCode;
                        ttemp.PartsName = tws[0].PartsName;
                        ttemp.BatchNumber = tws[0].BatchNumber;
                        ttemp.StationName = tws[0].StationName;
                        ttemp.StationId = tws[0].StationId;
                        ttemp.InStationTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"); 
                        ttemp.PartsCode = tws[0].PartsCode;
                        ttemp.QUANTITY = tws[0].QUANTITY.ToString();
                        ttemp.STEP = "1";
                        if (wos != null && wos.Count > 0)
                        {
                            ttemp.MachineName = wos[0].MachineName;
                            ttemp.MachineType = wos[0].MachineType;
                        } 
                        strID = _bal.SaveTrackingTemp(ttemp);
                    }
                    else//扫描的是非第一个条码
                    {
                        if (tt.STEP.ToString() == "2")
                        {
                            throw new Exception("请扫描结果，条码本次已扫完");
                        }
                        tt.PSN += "," + inputdata;
                        //因考虑到一个人报2种以上不同工序的情况，特佳此代码start
                        tt.WorkOrder += "," + tws[0].WorkOrder;
                        tt.PartsdrawingCode += "," + tws[0].PartsdrawingCode;
                        tt.PartsName += "," + tws[0].PartsName;
                        tt.BatchNumber += "," + tws[0].BatchNumber;
                        tt.StationName += "," + tws[0].StationName;
                        tt.StationId += "," + tws[0].StationId;
                        tt.InStationTime += "," + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                        tt.PartsCode += "," + tws[0].PartsCode;
                        tt.QUANTITY += "," + tws[0].QUANTITY;
                        if (wos != null && wos.Count > 0)
                        {
                            tt.MachineName += "," + wos[0].MachineName;
                            tt.MachineType += "," + wos[0].MachineType;
                        }
                        tt.STEP = "1";
                        //end
                        strID = _bal.SaveTrackingTemp(tt);
                    }
                }
                else
                {
                    throw new Exception("该条码未关联生产信息，请先去仓库关联");
                }
                // }  
                break;
        }
        return strID;
    }
    /// <summary>
    /// 处理报工的扫描结果数据:结果:1合格、2超差、3报废、4降级、5指定用途、6待处理
    /// </summary>
    /// <param name="inputdata"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DealwithScanRes(IList<TrackingWip> gtws, TrackingTemp tt, string s_status )
    {
        int totalpsn = 0;//条码数量
        int scanres = 0;//扫描结果的数量
        if (gtws != null)
        {
            totalpsn = gtws.Count;
        }

        if (!string.IsNullOrEmpty(tt.STATUS))
        {
            string[] strres = tt.STATUS.ToString().Split(',');
            scanres = strres.Length;
        }
        //结果扫描完毕，可以保存。
        if (tt.STATUS == null || tt.STATUS.ToString() == "")
        {
            tt.STATUS = s_status;
            tt.OutStationTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"); 
        }
        else
        {
            tt.STATUS = tt.STATUS.Trim() + "," + s_status;
            tt.OutStationTime += "," + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        }
        //判断扫描结果是否扫完
        if (totalpsn == scanres + 1)
        {
            //计算工时
            //计算工时
            //if (string.IsNullOrEmpty(tt.TaskTime))
            //{
            //    tt.TaskTime = (tt.UpdatedDate - tt.CreatedDate).ToString();
            //}
            //else if(totalpsn==1&&!string.IsNullOrEmpty(tt.TaskTime))
            //{
            //    //计算有暂停情况的工时
            //    TimeSpan tempPauseTaskTime = TimeSpan.ParseExact(tt.TaskTime, @"hh\:mm\:ss\.fffffff", null);
            //    TimeSpan tempNewTaskTime = (TimeSpan)(tt.UpdatedDate - tt.CreatedDate);
            //    tt.TaskTime = (tempPauseTaskTime + tempNewTaskTime).ToString();
            //}
            //else
            //{
                //一个人同时投多个工序需添加以下代码start
                if (totalpsn == 1)
                {
                    DateTime t1 = DateTime.Parse(tt.InStationTime.ToString());
                    DateTime t2 = DateTime.Now;
                    System.TimeSpan t3 = t2 - t1;
                    tt.TaskTime = t3.ToString();
                }
                else
                {
                    string[] temp_instationtimes = tt.InStationTime.ToString().Split(',');
                    string[] temp_outstationtimes = tt.OutStationTime.ToString().Split(',');
                    for (int p = 0; p < totalpsn; p++)
                    {
                        DateTime t1 = DateTime.Parse(temp_instationtimes[p].ToString());
                        DateTime t2 = DateTime.Parse(temp_outstationtimes[p].ToString());
                    System.TimeSpan t3 = t2 - t1;
                        if (p == 0)
                        {
                            tt.TaskTime = t3.ToString();
                        }
                        else
                        {
                            tt.TaskTime += "," + t3.ToString();
                        }
                    }
                }
                //end
            //}
            string[] psns = tt.PSN.ToString().Split(',');
            string[] statuss = tt.STATUS.ToString().Split(','); 
            //一个人同时投多个工序需添加以下代码start
            string[] tasktimes = tt.TaskTime.ToString().Split(',');
            string[] instationtimes = tt.InStationTime == null ? null : tt.InStationTime.ToString().Split(',');
            string[] qualitys = tt.QUANTITY.Split(',');
            string[] stationnames = tt.StationName.ToString().Split(',');
            string[] stationids = tt.StationId.ToString().Split(',');
            string[] workorders = tt.WorkOrder.ToString().Split(',');
            //string[] partscodes = tt.PartsCode.ToString().Split(',');
            string[] partsnames =  tt.PartsName.ToString().Split(',');
            string[] machinenames = null;
            string[] machinetypes = null;
            if (tt.MachineName != null)
            {
                 machinenames = tt.MachineName.Split(',');
                 machinetypes = tt.MachineType.Split(',');
            }
                //end
                //此方法支持多个条码查找
                IList<TrackingWip> tw = _bal.FindTrackingWip(tt.PSN.ToString());
            for (int i = 0; i < tw.Count; i++)
            {
                int q = 0;
                //查找tracking_wip同tracking_temp的psn对应关系
                for (int k = 0; k < psns.Length; k++)
                {
                    if (tw[i].PSN.ToString() == psns[k])
                    {
                        q = k;
                        break;
                    }
                } 
                tw[i].STATUS = statuss[q].Trim(); 
                tw[i].InStatioonTime = Convert.ToDateTime(instationtimes[q]);// tt.InStationTime;//一个人同时投多个工序需添加
                tw[i].OutStationTime = DateTime.Now;// t2.ToString("yyyy-MM-dd hh:mm:ss");
                tw[i].TaskTime = tasktimes[q];// tt.TaskTime;//一个人同时投多个工序需添加
                string[] tempstation = _bal.GetNextStation(workorders[q], stationids[q]);
                tw[i].NextStation = tempstation[0];// tt.NextStation;//一个人同时投多个工序需添加
                tw[i].NextStationId = tempstation[1];
                tw[i].StationName = stationnames[q];// tt.StationName;//一个人同时投多个工序需添加
                tw[i].StationId = stationids[q];// tt.StationId;//一个人同时投多个工序需添加
                tw[i].WorkOrder = workorders[q];// tt.WorkOrder;//一个人同时投多个工序需添加
               // tw[i].PartsCode = partscodes[q];// tt.ClassName;//一个人同时投多个工序需添加
                tw[i].PartsName = partsnames[q];// tt.IsAllowNext;//一个人同时投多个工序需添加
                tw[i].QUANTITY = Convert.ToInt32(qualitys[q]);
                //保存多人共同干一件活的员工
                if (tt.NextEmp != null && tt.NextEmp.ToString() != "")
                {
                    tw[i].UpdatedBy = tt.NextEmp.ToString(); 
                }
                else
                {
                    tw[i].UpdatedBy = this._userInfo.UserCode;
                }
                 _bal.SaveTrackingWip(tw[i]);
                //保存历史记录
                TrackingHistory th = new TrackingHistory();
                th.PSN = psns[i];
                th.MSN = tw[i].MSN;
                th.WorkOrder = tw[i].WorkOrder;
                th.PartsdrawingCode = tw[i].PartsdrawingCode;
                th.PartsName = tw[i].PartsName;
                th.PartsCode = tw[i].PartsCode;
                th.BatchNumber = tw[i].BatchNumber;
                th.StationName = tw[i].StationName;
                th.QUANTITY = tw[i].QUANTITY;
                th.STATUS = tw[i].STATUS;
                th.InStationTime = tw[i].InStatioonTime;
                th.OutStationTime = tw[i].OutStationTime;
                th.TaskTime = tw[i].TaskTime;
                if (tt.MachineName != null)
                { 
                    th.MachineName = machinenames[q];
                    th.MachineType = machinetypes[q];
                }
                th.NextStation = tw[i].NextStation;
                th.NextStationId = tw[i].NextStationId;
                if (tw[i].STATUS == "F")
                {
                    th.NextStation = "UnsurenessIn";
                    th.NextStationId = "UnsurenessIn";
                }
                th.StationId = tw[i].StationId;
                th.CreatedDate = DateTime.Now;
                th.UpdatedBy = tw[i].UpdatedBy;
                _bal.SaveTrackingHistory(th);
                //工单加1
                IList<WorkOrderDetails> wos = _bal.FindWorkOrderDetailsInfo(tw[i].WorkOrder, stationids[q]);
                wos[0].QUANTITY = _bal.FindWorkOrderCount(tw[0].WorkOrder,stationnames[q],stationids[q]);
                _bal.SaveWorkOrderInfo(wos[0]);

                //如果不合格，直接入待处理品表
                if (tw[i].STATUS == "F")
                {
                    UnsurenessProduct up = new UnsurenessProduct();
                    up.PSN = th.PSN;
                    up.MSN = th.MSN;
                    up.WorkOrder = th.WorkOrder;
                    up.STATUS = "0";
                    up.StationName = stationnames[q];
                    up.MEMO = "待处理";
                    up.QUANTITY = th.QUANTITY;
                    up.PartsdrawingCode = th.PartsdrawingCode;
                    up.ProductName = th.PartsName;
                    up.BatchNumber = th.BatchNumber;
                    up.CreatedDate = DateTime.Now;
                    _bal.SaveUnsurenessProduct(up);

                    //入完待处理品后，再次保存历史记录
                    th.ID = string.Empty;
                    th.StationName = "UnsurenessIn";
                    th.StationId = "UnsurenessIn";
                    th.STATUS = "P";
                    th.NextStation = "UnsurenessOut";
                    th.NextStationId = "UnsurenessOut";
                    th.CreatedDate = DateTime.Now;
                    _bal.SaveTrackingHistory(th);
                }

                _bal.RemoveTrackingTemp(tt.ID);
                //保存到实时统计表
                RealtimeStatistics rs = new RealtimeStatistics();
                rs.PSN = th.PSN;
                rs.MSN = th.MSN;
                rs.WorkOrder = th.WorkOrder;
                rs.StationName = th.StationName;
                rs.MachineType = th.MachineType;
                rs.MachineName = th.MachineName;
                rs.STATUS = th.STATUS;
                rs.QUANTITY = th.QUANTITY;
                rs.OPERATOR = th.UpdatedBy;
                if (wos.Count > 0)
                {
                    rs.OrderNumber = wos[0].OrderNumber;
                }
                rs.ProductName = th.PartsName;
                rs.ProductCode = th.PartsCode;
                IList<PartsdrawingCode> pc = _bal.FindPartsdrawingInfo(th.PartsdrawingCode);
                if (pc.Count > 0)
                {
                    rs.CustName = pc[0].CustName;
                }
                rs.PartsdrawingCode = th.PartsdrawingCode;
                _bal.SaveRealtimeStatistics(rs);
            }
        }
        else if (totalpsn > (scanres + 1))
        {//没扫描完，继续扫描
            tt.STEP = "2";
            _bal.SaveTrackingTemp(tt);
        }
    }
}
