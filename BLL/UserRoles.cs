using System;
using System.Collections.Generic;

using System.Text;

namespace BLL
{

    public class UserRoles
    {
        public UserRoles()
        {
            this._ID = "";
            this._RoleName = "";
            this._MEMO = "";
            this._checked = false;            
        }
        private string _ID;
        private string _RoleName; 
        private string _MEMO;        
        private bool _checked;
        

        public string ID { get { return _ID; } set { this._ID = value; } }
        public string RoleName { get { return _RoleName; } set { this._RoleName = value; } } 
        
        public string MEMO { get { return _MEMO; } set { this._MEMO = value; } }
     
        public bool Checked { get { return _checked; } set { this._checked = value; } } 
    }
}
