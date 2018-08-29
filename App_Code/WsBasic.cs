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


/// <summary>
/// WsSystem 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WsBasic : WsBase
{
    private BasicBO _bal;
    private SystemBO _balsystem;
    public WsBasic() : base()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        _bal = BLLFactory.GetBal<BasicBO>(_userInfo); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FindUserNameByCode(string userCode)
    {
        WsSystem ws = new WsSystem();
        SysUser su = ws.FindUserByCode(userCode);
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
    public void FindBaseByCode()
    {
       string startTime= Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        string baseCode = Context.Request.Form["baseName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasBase> baseInfo = _bal.FindBaseByCode(startTime,endTime,baseCode);
        List<BasBase> bs = new List<BasBase>();
        int istart = (Convert.ToInt32(page)-1)*Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows)+1;
        int j = 1;
        foreach (BasBase bb in baseInfo)
        {
            if(j>istart&&j<iend)
            {
                BasBase bbtemp = new BasBase();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        if(baseInfo != null& baseInfo.Count>0)
        map.Add("total", baseInfo.Count);
        map.Add("rows", bs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindCust()
    {
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        string custCode = Context.Request.Form["custCode"];
        IList<BasCustom> custInfo = _bal.FindCust(startTime, endTime, custCode);

        Dictionary<String, Object> map = new Dictionary<String, Object>();
        //by tony modify 2017-6-3
        if (custInfo != null & custInfo.Count > 0)
        {
            foreach (BasCustom bb in custInfo)
            {
                bb.UpdatedBy = FindUserNameByCode(bb.UpdatedBy);
            }
                map.Add("total", custInfo.Count);
           
        }
        map.Add("rows", custInfo);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DelCust(string custCode)
    {
        return _bal.DelCust(custCode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveCust(BasCustom obj)
    {
        return _bal.SaveCust(obj);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindSquenceByCode()
    {
        string squence = Context.Request.Form["squenceName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasSequence> sInfo = _bal.FindSquenceByCode(squence);
        List<BasSequence> bs = new List<BasSequence>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (BasSequence bb in sInfo)
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
        //by tony modify 2017-6-3
        if (sInfo != null & sInfo.Count > 0)        
            map.Add("total", sInfo.Count);
            map.Add("rows", sInfo);
         
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LisBasCode()
    {
        string codename = Context.Request.Form["codeName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasCode> objs = _bal.FindBasCode("", codename);
        List<BasCode> bs = new List<BasCode>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (BasCode bb in objs)
        {
            if (j > istart && j < iend)
            {
                BasCode bbtemp = new BasCode();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bb.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }

        Dictionary<String, Object> map = new Dictionary<String, Object>();
        //by tony modify 2016-6-3
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
            map.Add("rows", objs);
        
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LisBasBanCi()
    {
        //string codename = Context.Request.Form["codeName"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<BasBanci> objs = _bal.FindBasBanCi();
        List<BasBanci> bs = new List<BasBanci>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (BasBanci bb in objs)
        {
            if (j > istart && j < iend)
            {
                BasBanci bbtemp = new BasBanci();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bb.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }

        Dictionary<String, Object> map = new Dictionary<String, Object>();
        //by tony modify 2016-6-3
        if (objs != null & objs.Count > 0)
            map.Add("total", objs.Count);
        map.Add("rows", objs);

        Context.Response.Write(JsonConvert.SerializeObject(map));
    }
}
