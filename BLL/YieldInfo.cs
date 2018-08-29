using System;
using System.Collections.Generic;

using System.Text;

namespace BLL
{

    public class YieldInfo
    {
        public YieldInfo()
        {
            this._WO = "";
            this._CustName = "";
            this._PartsdrawingCode = "";
            this._BatchNumber = "";
            this._ProductName = "";
            this._PlanQuantity = 0;
            this._QUANTITY = 0;
            this._PassCount = 0;
            this._FailCount = 0;
            this._ReturnCount = 0;
            this._SecondPass = 0;
            this._DiscardCount = 0;
            this._PassRate = "";
            this._FailRate = "";
            this._ReturnRate = "";
            this._SecPassRate = "";
            this._DiscardRate = "";          
        }
        private string _WO;//工单号码
        private string _CustName; //客户名称
        private string _PartsdrawingCode;//零件图号
        private string _ProductName;//产品名称
        private string _BatchNumber;//批号
        private int _PlanQuantity;//计划数量
        private int _QUANTITY;//产出数量
        private int _PassCount;//合格数量
        private int _FailCount;//不合格数量
        private int _ReturnCount;//返工数量
        private int _SecondPass;//让步数量
        private int _DiscardCount;//报废数量
        private string _PassRate;
        private string _FailRate;
        private string _ReturnRate;
        private string _SecPassRate;
        private string _DiscardRate;
        

        public string WO { get { return _WO; } set { this._WO = value; } }
        public string CustName { get { return _CustName; } set { this._CustName = value; } } 
        
        public string PartsdrawingCode { get { return _PartsdrawingCode; } set { this._PartsdrawingCode = value; } }

        public string ProductName { get { return _ProductName; } set { this._ProductName = value; } }

        public string BatchNumber { get { return _BatchNumber; } set { this._BatchNumber = value; } }
        public int PlanQuantity { get { return _PlanQuantity; } set { this._PlanQuantity = value; } }
        public int QUANTITY { get { return _QUANTITY; } set { this._QUANTITY = value; } }
        public int PassCount { get { return _PassCount; } set { this._PassCount = value; } }
        public int FailCount { get { return _FailCount; } set { this._FailCount = value; } }
        public int ReturnCount { get { return _ReturnCount; } set { this._ReturnCount = value; } }
        public int SecondPass { get { return _SecondPass; } set { this._SecondPass = value; } }
        public int DiscardCount { get { return _DiscardCount; } set { this._DiscardCount = value; } }
        public string PassRate { get { return _PassRate; } set { this._PassRate = value; } }
        public string FailRate { get { return _FailRate; } set { this._FailRate = value; } }
        public string ReturnRate { get { return _ReturnRate; } set { this._ReturnRate = value; } }
        public string SecPassRate { get { return _SecPassRate; } set { this._SecPassRate = value; } }
        public string DiscardRate { get { return _DiscardRate; } set { this._DiscardRate = value; } }
    }
}
