using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Freeworks.Common;
using Freeworks.Common.Reflection;
using Freeworks.ORM;

using DAL;

namespace BLL
{
    public class BLLFactory
    {
        public static T GetBal<T>(UserInfo user) where T:IBOBase
        {
            if (user == null)
            {
                user = new UserInfo();               
            }
            string baseName = typeof(T).Name;                      
            
            return (T)ReflectionUtil.GetNewEntity(ReflectionUtil.GetTypeByName(ConstantsHelper.GetHelper(user.SiteCode, user.BUCode).GetBLLInfo(baseName)),user);
        }

        public static T GetBal<T>() where T : BOBase
        {
            UserInfo user = new UserInfo();           
            
            string baseName = typeof(T).Name;
            return (T)ReflectionUtil.GetNewEntity(ReflectionUtil.GetTypeByName(ConstantsHelper.GetHelper(user.SiteCode, user.BUCode).GetBLLInfo(baseName)), user);
        }
    }
}
