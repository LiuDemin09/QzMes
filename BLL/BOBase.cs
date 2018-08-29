using System;
using System.Collections.Generic;
using System.Text;

using Freeworks.ORM;
using Freeworks.ORM.Core;
using DAL;

namespace BLL
{
    public abstract class BOBase : IBOBase
    {
        public DataContext DBContext { get; set; }

        private string _UserBU;
        private string _UserSite;
        private string _UserLang;
        private string _UserCode;
        private int _UserID;
        private string _TokenString;
        private string _StationID;
        private string _Line;
        private string _Mac;
        private string _IP;

        private bool _IsAdmin;
        private UserInfo _MyUserInfo;

        protected string UserBU { get { return _UserBU; } set { this._UserBU = value; } }
        protected string UserSite { get { return _UserSite; } set { this._UserSite = value; } }
        protected string UserLang { get { return _UserLang; } set { this._UserLang = value; } }
        protected string UserCode { get { return _UserCode; } set { this._UserCode = value; } }
        protected int UserID { get { return _UserID; } set { this._UserID = value; } }
        protected string TokenString { get { return _TokenString; } set { this._TokenString = value; } }
        protected string StationID { get { return _StationID; } set { this._StationID = value; } }
        protected string Line { get { return _Line; } set { this._Line = value; } }
        protected string Mac { get { return _Mac; } set { this._Mac = value; } }
        protected string IP { get { return _IP; } set { this._IP = value; } }

        protected bool IsAdmin { get { return _IsAdmin; } set { this._IsAdmin = value; } }
        protected UserInfo MyUserInfo { get { return _MyUserInfo; } set { this._MyUserInfo = value; } }

        public BOBase() : this(new UserInfo())
        {

        }

        public BOBase(UserInfo userInfo)
        {
            this.MyUserInfo = userInfo;
            this.UserSite = this.MyUserInfo.SiteCode;
            this.UserBU = this.MyUserInfo.BUCode;
            this.UserLang = this.MyUserInfo.Lang;
            this.UserCode = this.MyUserInfo.UserCode;
            this.IsAdmin = this.MyUserInfo.IsAdmin;
            this.UserID = this.MyUserInfo.UserID;
            this.TokenString = this.MyUserInfo.TokeString;
            this.StationID = this.MyUserInfo.StationID;
            this.Line = this.MyUserInfo.Line;
            this.Mac = this.MyUserInfo.Mac;
            this.IP = this.MyUserInfo.IP;

            DBContext = DataServiceFactory.Create(this.UserSite + this.UserBU);
        }
    }
}

