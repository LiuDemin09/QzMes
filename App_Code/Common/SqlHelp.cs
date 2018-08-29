using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Net;

namespace MES
{
    /// <summary>
    /// sql语句生成代码
    /// </summary>
    public class SqlHelp
    {
        #region sql语句生成代码
        /// <summary>
        /// 分页sql语句生成代码
        /// </summary>
        /// <param name="SelectFields"></param>
        /// <param name="TblName"></param>
        /// <param name="FldName">排序字段,唯一性</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="OrderType"></param>
        /// <param name="whereStr"></param>
        /// <returns></returns>

        public static string GetSql(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr)
        {
            string StrTemp = "";
            string StrSql = "";
            string StrOrder = "";
            //根据排序方式生成相关代码
            if (OrderType.ToUpper() == "ASC")
            {
                StrTemp = "> (SELECT MAX(" + FldName + ")";
                StrOrder = " ORDER BY " + FldName + " ASC";
            }
            else
            {
                StrTemp = "< (SELECT MIN(" + FldName + ")";
                StrOrder = " ORDER BY " + FldName + " DESC";
            }
            PageIndex = MES.Validator.StrToInt(PageIndex.ToString(), 0);
            PageIndex = PageIndex == 0 ? 1 : PageIndex;
            //若是第1页则无须复杂的语句
            if (PageIndex == 1)
            {
                StrTemp = "";
                if (whereStr != "")
                    StrTemp = " Where " + whereStr;
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " From " + TblName + "" + StrTemp + StrOrder;
            }
            else
            {
                //若不是第1页，构造sql语句
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " From " + TblName + " WHERE " + FldName + "" + StrTemp + " From (SELECT TOP " + (PageIndex - 1) * PageSize + " " + FldName + " From " + TblName + "";
                if (whereStr != "")
                    StrSql += " Where " + whereStr;
                StrSql += StrOrder + ") As Tbltemp)";
                if (whereStr != "")
                    StrSql += " And " + whereStr;
                StrSql += StrOrder;
            }
            //返回sql语句
            return StrSql;
        }

        /// <summary>
        /// 连表分页sql语句生成代码
        /// <param name="SelectFields">连表查询的字段</param>
        /// <param name="TblNameA">表1</param>
        /// <param name="TblNameB">表2</param>
        /// <param name="FldName">表1的排序字段名</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="OrderType">排序方式：DESC ASC</param>
        /// <param name="joinStr">连接条件</param>
        /// <param name="whereStr1">外围条件带A.</param>
        /// <param name="whereStr2">分页条件:不带A.</param>
        /// </summary>

        public static string GetSql(string SelectFields, string TblNameA, string TblNameB, string FldName, int PageSize, int PageIndex, string OrderType, string joinStr, string whereStr1, string whereStr2)
        {
            string StrTemp = "";
            string StrSql = "";
            string StrOrder1 = "";
            string StrOrder2 = "";
            //根据排序方式生成相关代码
            if (OrderType.ToUpper() == "ASC")
            {
                StrTemp = "> (SELECT MAX(" + FldName + ")";
                StrOrder1 = " ORDER BY A." + FldName + " ASC";
                StrOrder2 = " ORDER BY " + FldName + " ASC";
            }
            else
            {
                StrTemp = "< (SELECT MIN(" + FldName + ")";
                StrOrder1 = " ORDER BY A." + FldName + " DESC";
                StrOrder2 = " ORDER BY " + FldName + " DESC";
            }
            PageIndex = MES.Validator.StrToInt(PageIndex.ToString(), 0);
            PageIndex = PageIndex == 0 ? 1 : PageIndex;
            //若是第1页则无须复杂的语句
            if (PageIndex == 1)
            {
                StrTemp = "";
                if (whereStr1 != "")
                    StrTemp = " WHERE " + whereStr1;
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A LEFT JOIN [" + TblNameB + "] B on " + joinStr + " " + StrTemp + StrOrder1;
            }
            else
            {
                //若不是第1页，构造sql语句
                StrSql = "SELECT TOP " + PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A LEFT JOIN [" + TblNameB + "] B on " + joinStr + " WHERE A." + FldName + "" + StrTemp + " From (SELECT TOP " + (PageIndex - 1) * PageSize + " " + FldName + " From [" + TblNameA + "] ";
                if (whereStr2 != "")
                    StrSql += " Where " + whereStr2;
                StrSql += StrOrder2 + ") As Tbltemp)";
                if (whereStr1 != "")
                    StrSql += " And " + whereStr1;
                StrSql += StrOrder1;
            }
            //返回sql语句
            return StrSql;
        }
        #endregion
    }
}
