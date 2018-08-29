using System;
using System.Collections.Generic;

using System.Text;

namespace BLL
{

    public class UserInfo
    {
        public UserInfo()
        {
            this.BUCode = BLLConstants.BU_DEFAULT;
            this.BUName = "ALL";
            this.SiteCode = BLLConstants.SITE_DEFAULT;
            this.SiteName = "ALL";
            this.UserCode = "";
            this.UserName = "";
            this.TokeString = "";
            this.UserID = -1; 
            this.StationID = "";
            this.Lang = "";
            this.Line = "";
            this.Mac = "";
            this.IP = "";

        }
        private string _UserName;
        private string _UserCode; 
        private int _UserID;
        private string _TokeString;
        private string[] _Roles;
        private string[] _Sections;
        private string _Password;
        private DateTime _LoginTime;
        private string _DeptCode;
        private string _DeptName;
        private string _SiteCode;
        private string _SiteName;
        private string _BUCode;
        private string _BUName;
        private string _Lang;
        private string _IP;
        private string _EMail;
        private string _ExtendPhone;
        private bool _IsAdmin;
        private string _StationID;       
        private string _Line;
        private string _Mac;


        public string UserName { get { return _UserName; } set { this._UserName = value; } }
        public string UserCode { get { return _UserCode; } set { this._UserCode = value; } } 
        public int UserID { get { return _UserID; } set { this._UserID = value; } }
        public string TokeString { get { return _TokeString; } set { this._TokeString = value; } }
        public string[] Roles { get { return _Roles; } set { this._Roles = value; } }
        public string[] Sections { get { return _Sections; } set { this._Sections = value; } }
        public string Password { get { return _Password; } set { this._Password = value; } }
        public DateTime LoginTime { get { return _LoginTime; } set { this._LoginTime = value; } }
        public string DeptCode { get { return _DeptCode; } set { this._DeptCode = value; } }
        public string DeptName { get { return _DeptName; } set { this._DeptName = value; } }
        public string SiteCode { get { return _SiteCode; } set { this._SiteCode = value; } }
        public string SiteName { get { return _SiteName; } set { this._SiteName = value; } }
        public string BUCode { get { return _BUCode; } set { this._BUCode = value; } }
        public string BUName { get { return _BUName; } set { this._BUName = value; } }
        public string Lang { get { return _Lang; } set { this._Lang = value; } }
        public string IP { get { return _IP; } set { this._IP = value; } }
        public string EMail { get { return _EMail; } set { this._EMail = value; } }
        public string ExtendPhone { get { return _ExtendPhone; } set { this._ExtendPhone = value; } }
        public bool IsAdmin { get { return _IsAdmin; } set { this._IsAdmin = value; } }
        public string StationID { get { return _StationID; } set { this._StationID = value; } }
        public string Line { get { return _Line; } set { this._Line = value; } }
        public string Mac { get { return _Mac; } set { this._Mac = value; } }

        public override string ToString()
        {
            return "{" + string.Format("UserName:'{0}',UserCode:'{1}',DeptCode:'{2}',DeptName:'{3}',BUCode:'{4}',BUName:'{5}',SiteCode:'{6}',SiteName:'{7}',Lang:'{8}',TokeString:'{9}',StationID:'{10}',Line:'{11}',Mac:'{12}',IP:'{13}'",
                this.UserName, 
                this.UserCode, 
                this.DeptCode,
                this.DeptName,
                this.BUCode,
                this.BUName,
                this.SiteCode,
                this.SiteName,
                this.Lang,
                this.TokeString,
                this.StationID,
                this.Line,
                this.Mac,
                this.IP) + "}";
        }
    }
}
