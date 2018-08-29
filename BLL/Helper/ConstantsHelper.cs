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

namespace BLL
{
    public class ConstantsHelper
    {
        //緩存
        private static IDictionary<string, string> Cache_Keys = new Dictionary<string, string>();
        private static IDictionary<string, object> Cache_Data = new Dictionary<string, object>();

        private DataContext DBContext;
        private string _site = BLLConstants.SITE_DEFAULT;
        private string _bu = BLLConstants.BU_DEFAULT;
       
        //打印模版存放路徑
        public string S_LABEL_TPL_PATH
        {
            get
            {                                 
                    string v = PubHelper.GetHelper(DBContext).GetConfigValue("LABEL_TPL_PATH");                    
                    return v;                
            }
        }

        public string GetSystemConfig(string keyName)
        {
            //string key = _site + _bu + keyName;
            //if (!Cache_Keys.Keys.Contains(key))
            //{
            string v = PubHelper.GetHelper(DBContext).GetConfigValue(keyName);
            if (v == "")
            {
                v = PubHelper.GetHelper(DBContext).GetConfigValue(keyName);
            }
            return v;
            //Cache_Keys.Add(key, v);
            //}
            //return Cache_Keys[key];
        }
        public void UpdateSystemconfig(string keyName,string value) {
            string key = _site + _bu + keyName;
            if (Cache_Keys.Keys.Contains(key)) {
                Cache_Keys.Remove(key);
            }
            Cache_Keys.Add(key,value);
        }
       
 
       

        private ConstantsHelper()
        {
            this.DBContext = DataServiceFactory.Create(_site + _bu);
        }
        private ConstantsHelper(string site,string bu)
        {
            _site = string.IsNullOrEmpty(site)?BLLConstants.SITE_DEFAULT:site;
            _bu = string.IsNullOrEmpty(bu)?BLLConstants.BU_DEFAULT:bu;
            this.DBContext = DataServiceFactory.Create(_site+_bu); 
        }

        public static ConstantsHelper GetHelper(string site, string bu)
        {
            return new ConstantsHelper(site, bu);
        }

        public static ConstantsHelper GetHelper()
        {
            return new ConstantsHelper();
        }

        public string GetBLLInfo(string baseName)
        {
            string key = _site + _bu + baseName + "BLLINFO";

            if (!D_BAL_INFO.Keys.Contains(key))
            {
                SysBllConfig balInfo = DBContext.Find<SysBllConfig>(SysBllConfig.Meta.BaseName == baseName);

                if (balInfo == null)
                {                   
                    throw new BusiException("The BLL has not been configurated in table 'MES_MASTER.Sys_Bll_Config'.");                   
                }
                else
                {
                    D_BAL_INFO.Add(key, balInfo.BllName);
                }
            }
            return D_BAL_INFO[key];
        }

        public IDictionary<string, string> D_BAL_INFO
        {
            get
            {
                string key = _site + _bu + "D_BAL_INFO";
                if (!Cache_Data.Keys.Contains(key))
                {
                    Cache_Data.Add(key, PubHelper.GetHelper(DBContext).LoadAllBalInfo());
                }

                return (IDictionary<string, string>)Cache_Data[key];
            }
        }
    }
}
