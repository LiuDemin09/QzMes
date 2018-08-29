using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AjaxPro;

using DAL;
using BLL;

/// <summary>
/// Summary description for AjaxInterface
/// </summary>
//[AjaxPro.AjaxNamespace("AppAjax")]
public class AjaxInterface
{
     

    public AjaxInterface()
	{
        //
        // TODO: Add constructor logic here
        //       
       
    }
    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public string GetToken()
    {
        UserInfo userInfo = WebHelper.GetUserInfo();
        string retStr = userInfo.UserCode + "^" + userInfo.UserName
                    + "^" + userInfo.SiteCode + "^" + userInfo.BUCode
                    + "^" + userInfo.Lang + "^" + userInfo.StationID;
        //產生Token
        retStr = HttpUtility.UrlEncode(DecryptUtil.EncryptForBase64(retStr));
        return retStr;
    }
    //[AjaxMethod(HttpSessionStateRequirement.Read)]
    //public string ConvertSN(string input)
    //{
    //    return LXSpCaller.GetCaller(WebHelper.GetDB(WebHelper.GetUserInfo())).ConvertSn(input);
    //}

    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public string GetTplDownloadUrl()
    {
        UserInfo userInfo = WebHelper.GetUserInfo();
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(WebHelper.GetUserInfo());
        IList<SysMidsvcConfig> svcList = _bal.GetMidsvcList();
        if (svcList.Count() > 0)
        {
            return svcList[0].MidSvcUrl + "/DownloadPrintTpl.ashx";
        }

        return ConstantsHelper.GetHelper(userInfo.SiteCode, userInfo.BUCode).GetSystemConfig("LABEL_TPL_DL_URL");
    }

    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public string GetSystemConfig(string key)
    {
        UserInfo user = WebHelper.GetUserInfo();      
        string ret = ConstantsHelper.GetHelper(user.SiteCode, user.BUCode).GetSystemConfig(key);
        return ret;
    }
    //[AjaxMethod(HttpSessionStateRequirement.Read)]
    //public TextValueInfo[] GetSystemCode(string key)
    //{
    //    UserInfo user = WebHelper.GetUserInfo();
    //    IList<BasCodeInfo> codes = ConstantsHelper.GetHelper(user.SiteCode, user.BUCode).GetSystemCode(key).OrderByDescending(p => p.ID).ToArray();
    //    TextValueInfo[] rets = new TextValueInfo[codes.Count];
    //    for (int i = 0; i < codes.Count; i++)
    //    {
    //        rets[i] = new TextValueInfo() { Text = codes[i].ItemText, Value = codes[i].ItemValue };
    //    }
    //    return rets;
    //}
    //[AjaxMethod(HttpSessionStateRequirement.Read)]
    //public SPMessage GetNextSN(string snID)
    //{
    //    RouteBO _bal = BLLFactory.GetBal<RouteBO>();
    //    return _bal.GetNextSN(snID);
    //}

    /// <summary>
    /// 獲取模版參數
    /// </summary>
    /// <param name="labelType">模版類型</param>
    /// <param name="pid">產品ID</param>
    /// <returns></returns>
    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public IList<TextValueInfo> GetLabelParams(string labelType, string pid)
    {
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(WebHelper.GetUserInfo());
        //RouteBO _bal = BLLFactory.GetBal<RouteBO>(WebHelper.GetUserInfo());
        return _bal.ListLabelParams(labelType, pid);
    }

    [AjaxMethod(HttpSessionStateRequirement.Read)]
    public SPMessage GetLabelParameters(string data, string labelId)
    {
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(WebHelper.GetUserInfo());
        return _bal.GetLabelParameters(data, labelId);

        //SPMessage ret = new SPMessage();
        //ret.Result = "OK";
        //ret.Message = "VarPsn =lt12312";
        //return ret;
    }

    /// <summary>
    /// 得到產品配置項目值
    /// </summary>
    /// <param name="modelName">產品料號</param>
    /// <param name="verNo">版本號</param>
    /// <param name="key">配置項目名稱</param>
    /// <returns>配置項目值</returns>
    //[AjaxMethod(HttpSessionStateRequirement.Read)]
    //public string GetProductConfigByKey(string modelName, string verNo, string key)
    //{
    //    RouteBO _bal = BLLFactory.GetBal<RouteBO>();
    //    return _bal.GetProductConfig(modelName, verNo, key);
    //}

    /// <summary>
    /// 檢查SN格式是否匹配
    /// </summary>
    /// <param name="snID"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    //[AjaxMethod(HttpSessionStateRequirement.Read)]
    //public bool CheckSNFormat(string snID, string data)
    //{
    //    string msg = LXSpCaller.GetCaller(WebHelper.GetDB(WebHelper.GetUserInfo())).CheckSNFormat(snID, data);
    //    return msg == "TRUE";
    //}

    /// <summary>
    /// 取得工單信息
    /// </summary>
    /// <param name="wo">工單號</param>
    /// <returns>工單詳細信息</returns>
    //[AjaxMethod(HttpSessionStateRequirement.Read)]
    //public static SfcWoInfo GetWoInfo(string wo)
    //{
    //    RouteBO _bal = BLLFactory.GetBal<RouteBO>();
    //    return _bal.GetWoInfo(wo);
    //}

    /// <summary>
    /// 查詢異常代碼
    /// </summary>
    /// <param name="family"></param>
    /// <param name="term"></param>
    /// <returns></returns>
    //[AjaxMethod(HttpSessionStateRequirement.Read)]
    //public static BasErrorCode[] GetErrorCodeList(string family, string term)
    //{
        
    //    RouteBO _bal = BLLFactory.GetBal<RouteBO>();
    //    IList<BasErrorCode> rets = _bal.ListErrorCode(family);
    //    if (string.IsNullOrWhiteSpace(term))
    //    {
    //        return rets.ToArray();
    //    }
    //    return rets.Where<BasErrorCode>(c => (c.ErrorCode.IndexOf(term) != -1)).ToArray();
    //}
}