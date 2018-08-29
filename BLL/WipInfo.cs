using System;
using System.Collections.Generic;

using System.Text;

namespace BLL
{

    public class WipInfo
    {
        public WipInfo()
        {
            this._WO = "";
            this._CustName = "";
            this._PartsdrawingCode = "";
            this._BatchNumber = "";
            this._ProductName = "";
            this._PlanQuantity = 0;
            this._QUANTITY = 0;
            this._CheXiPassCount = 0;
            this._CheXiFailCount = 0;
            this._ChePassCount = 0;
            this._CheFailCount = 0;
            this._XiPassCount = 0;
            this._XiFailCount = 0;
            this._QianGongPassCount = 0;
            this._QianGongFailCount = 0;
            this._QCPassCount = 0;
            this._QCFailCount = 0;
            this._InStockQty = 0;         
        }
        private string _WO;//工单号码
        private string _CustName; //客户名称
        private string _PartsdrawingCode;//零件图号
        private string _ProductName;//产品名称
        private string _BatchNumber;//批号
        private int _PlanQuantity;//计划数量
        private int _QUANTITY;//产出数量
        private int _CheXiPassCount;//车铣合格数量
        private int _CheXiFailCount;//车铣不合格数量
        private int _ChePassCount;//车合格数量
        private int _CheFailCount;//车不合格数量
        private int _XiPassCount;//铣合格数量
        private int _XiFailCount;//铣不合格数量
        private int _QianGongPassCount;//钳工合格数量
        private int _QianGongFailCount;//钳工不合格数量
        private int _QCPassCount;//QC合格数量
        private int _QCFailCount;//QC不合格数量
        private int _InStockQty;//入库数量
        
        

        public string WO { get { return _WO; } set { this._WO = value; } }
        public string CustName { get { return _CustName; } set { this._CustName = value; } }         
        public string PartsdrawingCode { get { return _PartsdrawingCode; } set { this._PartsdrawingCode = value; } }
        public string ProductName { get { return _ProductName; } set { this._ProductName = value; } }
        public string BatchNumber { get { return _BatchNumber; } set { this._BatchNumber = value; } }
        public int PlanQuantity { get { return _PlanQuantity; } set { this._PlanQuantity = value; } }
        public int QUANTITY { get { return _QUANTITY; } set { this._QUANTITY = value; } }
        public int CheXiPassCount { get { return _CheXiPassCount; } set { this._CheXiPassCount = value; } }
        public int CheXiFailCount { get { return _CheXiFailCount; } set { this._CheXiFailCount = value; } }
        public int ChePassCount { get { return _ChePassCount; } set { this._ChePassCount = value; } }
        public int CheFailCount { get { return _CheFailCount; } set { this._CheFailCount = value; } }
        public int XiPassCount { get { return _XiPassCount; } set { this._XiPassCount = value; } }
        public int XiFailCount { get { return _XiFailCount; } set { this._XiFailCount = value; } }
        public int QianGongPassCount { get { return _QianGongPassCount; } set { this._QianGongPassCount = value; } }
        public int QianGongFailCount { get { return _QianGongFailCount; } set { this._QianGongFailCount = value; } }
        public int QCPassCount { get { return _QCPassCount; } set { this._QCPassCount = value; } }
        public int QCFailCount { get { return _QCFailCount; } set { this._QCFailCount = value; } }
        public int InStockQty { get { return _InStockQty; } set { this._InStockQty = value; } }
      

    }
}
