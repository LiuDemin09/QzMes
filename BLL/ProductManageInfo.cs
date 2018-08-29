using System;
using System.Collections.Generic;

using System.Text;

namespace BLL
{

    public class ProductManageInfo
    {
        public ProductManageInfo()
        {
            this._WO = "";
            this._STATUS = "";
            this._OrderNo = "";
            this._CustName = "";
            this._PartsdrawingCode = "";
            this._StationId = "";
            this._StationName = "";
            this._Operator = "";
            this._OperatorName = "";
            this._EquipName = "";
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
            this._PlanUnitTime = "";
            this._ActualUnitTime = "";
            this._PlanTotalUnitTime = "";
            this._ActualTotalUnitTime = "";
            this._UpdateDateTime = "";          
        }
        private string _WO;//工单号码
        private string _STATUS;//工单状态
        private string _OrderNo;//订单号码
        private string _CustName; //客户名称
        private string _PartsdrawingCode;//零件图号
        private string _StationId;//工序ID
        private string _StationName;//工序名
        private string _Operator;//操作工
        private string _OperatorName;//操作工姓名
        private string _EquipName;//设备名称
        private string _ProductName;//产品名称
        private string _BatchNumber;//批号
        private int _PlanQuantity;//计划数量
        private int _QUANTITY;//产出数量
        private int _PassCount;//一类品数量
        private int _FailCount;//待处理品数量
        private int _ReturnCount;//二类返工数量
        private int _SecondPass;//二类让步数量
        private int _DiscardCount;//报废数量
        private string _PassRate;//一类品率
        private string _FailRate;//不良率
        private string _ReturnRate;//二类返工率
        private string _SecPassRate;//二类品率
        private string _DiscardRate;//报废率
        private string _PlanUnitTime;//计划单件工时
        private string _ActualUnitTime;//实际单件工时
        private string _PlanTotalUnitTime;//计划总工时
        private string _ActualTotalUnitTime;//实际总工时
        private string _UpdateDateTime;//发生时间

        public string WO { get { return _WO; } set { this._WO = value; } }
        public string STATUS { get { return _STATUS; } set { this._STATUS = value; } }
        public string OrderNo { get { return _OrderNo; } set { this._OrderNo = value; } }
        public string CustName { get { return _CustName; } set { this._CustName = value; } } 
        
        public string PartsdrawingCode { get { return _PartsdrawingCode; } set { this._PartsdrawingCode = value; } }
        public string StationId { get { return _StationId; } set { this._StationId = value; } }
        public string StationName { get { return _StationName; } set { this._StationName = value; } }
        public string Operator { get { return _Operator; } set { this._Operator = value; } }
        public string OperatorName { get { return _OperatorName; } set { this._OperatorName = value; } }
        public string EquipName { get { return _EquipName; } set { this._EquipName = value; } }
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
        public string PlanUnitTime { get { return _PlanUnitTime; } set { this._PlanUnitTime = value; } }
        public string ActualUnitTime { get { return _ActualUnitTime; } set { this._ActualUnitTime = value; } }
        public string PlanTotalUnitTime { get { return _PlanTotalUnitTime; } set { this._PlanTotalUnitTime = value; } }
        public string ActualTotalUnitTime { get { return _ActualTotalUnitTime; } set { this._ActualTotalUnitTime = value; } }
        public string UpdateDateTime { get { return _UpdateDateTime; } set { this._UpdateDateTime = value; } }
    }
}
