using System;
using System.Collections.Generic;

using System.Text;

namespace BLL
{
    public class TextValueInfo
    {
        private string _Text;

        private string _Value;

        public string Text { get { return _Text; } set { this._Text = value; } }

        public string Value { get { return _Value; } set { this._Value = value; } }
    }
}
