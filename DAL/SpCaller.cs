using System;
using System.Data;
using System.Collections.Generic;
using Freeworks.ORM;
using Freeworks.ORM.Core;

namespace DAL
{	
	public class SpCaller
	{
        private SpCaller(DataContext _db)
        {
            DBContext = _db;
        }

        /// <summary>
        /// 獲得對象實例
        /// </summary>
        /// <param name="_db"></param>
        /// <returns></returns>
        public static SpCaller GetCaller(DataContext _db){
            return new SpCaller(_db);
        }
		#region Member Variables
        private DataContext DBContext;
		//public const string DB_NAME = "Newfuse (fmgr)";
		#endregion
		
		#region stored procedure	
			
		public DataExcutor CallChkMenuPrivilege(string pMenuCode,string pUserCode)
		{
			return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.CHK_MENU_PRIVILEGE")
					.AddInParameter("P_MENU_CODE", DbType.AnsiString, pMenuCode)
					.AddInParameter("P_USER_CODE", DbType.AnsiString, pUserCode)
					.AddOutParameter("P_RET", DbType.AnsiString, 100)
			;
		}
		 
		public DataExcutor CallFnGetObjectName()
		{
			return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.FN_GET_OBJECT_NAME")
;
		}
		public DataExcutor CallGetMenusBylevel(string pUserCode,decimal? pLevel)
		{
			return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.GET_MENUS_BYLEVEL")
					.AddInParameter("P_USER_CODE", DbType.AnsiString, pUserCode)
					.AddInParameter("P_LEVEL", DbType.Decimal, pLevel)
			.AddCursor("P_RET")
;
		}
        public DataExcutor CallGetNextSeq(string pSeqId)
        {
            return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.GET_NEXT_SEQ")
                    .AddInParameter("P_SEQ_ID", DbType.AnsiString, pSeqId)
                    .AddOutParameter("P_RET", DbType.AnsiString, 10)
                    .AddOutParameter("P_MSG", DbType.AnsiString, 1000)
            ;
        }
        public DataExcutor CallGetNextSn(string pSnId)
        {
            return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.GET_NEXT_SN")
                    .AddInParameter("P_SN_ID", DbType.AnsiString, pSnId)
                    .AddOutParameter("P_RET", DbType.AnsiString, 10)
                                .AddOutParameter("P_MSG", DbType.AnsiString, 1000)
            ;
        }
		
		public DataExcutor CallGetSubmenus(string pUserCode,string pMenuCode)
		{
			return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.GET_SUBMENUS")
					.AddInParameter("P_USER_CODE", DbType.AnsiString, pUserCode)
					.AddInParameter("P_MENU_CODE", DbType.AnsiString, pMenuCode)
			.AddCursor("P_RET")
;
		}

        public DataExcutor CallGetAuthSubmenus(string pUserCode, string pMenuCode)
        {
            return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.GET_AUTH_SUBMENUS")
                    .AddInParameter("P_USER_CODE", DbType.AnsiString, pUserCode)
                    .AddInParameter("P_MENU_CODE", DbType.AnsiString, pMenuCode)                 
            .AddCursor("P_RET")
;
        }

		public DataExcutor CallGetUserRoles(string pUserCode)
		{
			return DBContext
                    .ExcuteSp("MES_MASTER.APP_SYS.GET_USER_ROLES")
					.AddInParameter("P_USER_CODE", DbType.AnsiString, pUserCode)					
			.AddCursor("P_RET")
;
		}
        public DataExcutor CallGetLabelParams(string funcName, string pPid)
        {
            return DBContext
                  .ExcuteSp(funcName)
                  .AddInParameter("P_PID", DbType.AnsiString, pPid)
                    .AddOutParameter("P_RET", DbType.AnsiString, 20)
                    .AddOutParameter("P_MSG", DbType.AnsiString, 1000)
;
        }
        public DataExcutor CallGetLabelParameters(string pData, string pLabelId)
        {
            return DBContext
                    .ExcuteSp("MES_MASTER.APP_LABEL.GET_LABEL_PARAMETERS")
                    .AddInParameter("P_DATA", DbType.AnsiString, pData)
                    .AddInParameter("P_LABEL_ID", DbType.AnsiString, pLabelId)
                    .AddOutParameter("P_RET", DbType.AnsiString, 10)
                                .AddOutParameter("P_MSG", DbType.AnsiString, 1000)
            ;
        }
        #endregion
    }	
}

