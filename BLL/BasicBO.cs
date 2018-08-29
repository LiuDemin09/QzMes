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
    public class BasicBO : BOBase
    {
        string tplPath = "";
        public BasicBO(UserInfo userInfo)
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

  
        public IList<BasBase> FindBaseByCode(string startTime, string endTime,string baseCode)
        {
            string sql = "SELECT * FROM MES_MASTER.BAS_BASE  WHERE 1=1                                      ";
            
            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')         ";
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')         ";
            }
            if (!string.IsNullOrEmpty(baseCode))
            {
                sql = sql + " AND CODE=  '"+baseCode+"'  ";
            }

            return DBContext.ExcuteSql(sql).ToBusiObjects<BasBase>();
            
        }

        public IList<BasCustom> FindCust(string startTime, string endTime, string custCode)
        {
            string sql = "SELECT * FROM MES_MASTER.BAS_CUSTOM  WHERE 1=1                                      ";

            if (!string.IsNullOrEmpty(startTime))
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')         ";
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                sql = sql + " AND CREATED_DATE>=  to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')         ";
            }
            if (!string.IsNullOrEmpty(custCode))
            {
                sql = sql + " AND CODE=  '" + custCode + "'  ";
            }

            return DBContext.ExcuteSql(sql).ToBusiObjects<BasCustom>();

        }

        public string DelCust(string custCode)
        {
            
            try
            {
                if (string.IsNullOrEmpty(custCode) )
                {
                    return "请选择一条数据进行删除";
                }
                BasCustom bc = DBContext.Find<BasCustom>( BasCustom.Meta.CODE == custCode );
                if (bc == null)
                {
                    return "删除数据不存在";
                }
                
                //if (!string.IsNullOrEmpty(bc.UpdatedBy) && bc.UpdatedBy != this.UserCode)
                //{
                //    return "无权删除该客户";
                //}
                DBContext.Remove<BasCustom>( BasCustom.Meta.CODE == bc.CODE);
              
                return "OK";
            }
            catch (Exception ex)
            {
               
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "DelCust");
                return ex.Message;
            }
            

        }

        public string SaveCust(BasCustom obj)
        {
           
            try
            {
                if (obj == null || string.IsNullOrEmpty(obj.CODE) || string.IsNullOrEmpty(obj.NAME))
                {
                    return "请检查输入数据是否输入完全";
                }
                BasCustom bc = DBContext.Find<BasCustom>(BasCustom.Meta.CODE==obj.CODE);
                if (bc != null)
                {
                    return "客户编号已使用";
                }

                
                obj.UpdatedBy = this.UserCode;
                obj.CreatedDate = DateTime.Now;

                DBContext.SaveAndUpdate<BasCustom>(obj);
                return "OK";

            }
            catch (Exception ex)
            {
               
                PubHelper.GetHelper(DBContext).Error(ex, this.UserCode, "SaveCustInfo");
                throw ex;
            }
           

        }

        public IList<BasSequence> FindSquenceByCode(string squence)
        {
            string sql = "SELECT * FROM MES_MASTER.BAS_SEQUENCE                                       ";

            
            if (!string.IsNullOrEmpty(squence))
            {
                sql = sql + " WHERE SEQ_NAME=  '" + squence + "'  ";
            }

            return DBContext.ExcuteSql(sql).ToBusiObjects<BasSequence>();

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

        public IList<BasBanci> FindBasBanCi()
        {

            return DBContext.FindArray<BasBanci>(BasBanci.Meta.ACTIVE == "1");

        }
    }

}
