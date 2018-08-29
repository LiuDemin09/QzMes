using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class BLLConstants
    {
       
        static BLLConstants()
        {
           
           //O_SAP_GINFO = SAPHelper.LoadSAPGroupConfig();
           //O_SAP_INFO = PubHelper.LoadSAPConfig(); 
        }

        public static int I_ROUTE_RESULT_PASS = 0;
        public static int I_ROUTE_RESULT_FAIL = 1;
        public static string Route_BO_NAME = "RouteBO";
        public const char SEPARATOR = ',';

        public const string COM_STATUS_ACTIVED = "A";
        public const string COM_STATUS_DISABLED = "D";

        public const string SITE_DEFAULT = "Mes";
        public const string BU_DEFAULT = "LF";        

        public const string ROLE_SATUS_CODE = "ROLE_STATUS";
        public const string USER_TYPE_CODE = "USER_TYPE";
        public const string ROLE_ADMIN = "ADMIN";
        public const string DEAULT_PWD = "password";

        //pubConfig Keys
        public const string CONFIG_VIEW_PROCESS_URL = "VIEW_PROCESS_URL";
        public const string CONFIG_SAP_INFO = "SAP_INFO";
        public const string CONFIG_SAP_GINFO = "SAP_GINFO";
        public const string ORDER_STATUS = "ORDER_STATUS";
        public const string BLT = "BLT";
        public const string ASSY = "ASSY";
        public const string PACKING = "PACKING";
        public const string SHIPPING = "SHIPPING";

        public const int I_SHOW_NUM = 10;
        public const int I_OUT_SHOW_NUM = 50;

    }
}
