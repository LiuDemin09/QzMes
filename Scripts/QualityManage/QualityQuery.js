var QualityQuery = function () {
    return {
        grid: null,
        partCode: null,
        custCode: null,
        startTime: null,
        endTime: null,
        Partsdrawing: null,
        CustName:null,
        init: function () {
            //加载客户
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
            });

        },
        bindGrid: function () {
            QualityQuery.CustName = $("#selCustName").val().trim();
            QualityQuery.Partsdrawing = $("#txtPartsdrawing").val().trim();
            QualityQuery.startTime = $("#txtStartTime").val().trim();
            QualityQuery.endTime = $("#txtEndTime").val().trim();
            $('#grid').datagrid('load', { CustName: QualityQuery.CustName, Partsdrawing: QualityQuery.Partsdrawing, StartTime: QualityQuery.startTime, EndTime: QualityQuery.endTime });

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '质量查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryQualityInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                  pageSize: 10,
                  pageNumber: 1,
                  pageList: [10, 20, 30, 40, 50],
                  beforePageText: '第',//页数文本框前显示的汉字   
                  afterPageText: '页    共 {pages} 页',
                  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                  columns: [[ //选择
                      { title: '客户名称', field: 'CustName', width: QualityQuery.fixWidth(0.1) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: QualityQuery.fixWidth(0.1) },
                     { title: '工单号码', field: 'WO', width: QualityQuery.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: QualityQuery.fixWidth(0.1) },
                     { title: '计划数量', field: 'PlanQuantity', width: QualityQuery.fixWidth(0.1) },
                     { title: '产出数量', field: 'QUANTITY', width: QualityQuery.fixWidth(0.1) },
                     { title: '不良数', field: 'FailCoun', width: QualityQuery.fixWidth(0.1) },
                     { title: '不良率', field: 'FailRate', width: QualityQuery.fixWidth(0.1) },
                ]],
                toolbar: [{
                    text: '零件图号 <input type="text" id="txtPartsdrawing"/>'
                }, '-', {
                    text: '客户名称 <select tabindex="-1" name="selCustName" id="selCustName"><option value="0">请选择</option> </select>'
                }, '-', {
                    text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间 <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        QualityQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/QualityManage/export/QualityQueryExport.aspx?custname=' + $('#selCustName').val() + '&partsdrawing=' + $('#txtPartsdrawing').val() + '&starttime=' + $('#txtStartTime').val() + '&endtime=' + $('#txtEndTime').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "QualityMain.aspx";
                    }
                }],
                    onLoadError: function (error) {
                        alert(error.responseText);
                    }
            });
        }
    };
}();