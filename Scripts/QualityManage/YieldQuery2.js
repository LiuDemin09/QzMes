var YieldQuery = function () {
    return {
        grid: null, 
        startT: null,
        endT: null,
        partsdrawing: null,
        custname: null,
        init: function () {
            //加载客户
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
            });
        },
        bindGrid: function () {
            YieldQuery.CustName = $("#selCustName").val().trim();
            YieldQuery.Partsdrawing = $("#txtPartsdrawing").val().trim();
            YieldQuery.startTime = $("#txtStartTime").val().trim();
            YieldQuery.endTime = $("#txtEndTime").val().trim();
            $('#grid').datagrid('load', { custname: YieldQuery.CustName, partsdrawing: YieldQuery.Partsdrawing, startT: YieldQuery.startTime, endT: YieldQuery.endTime });

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '良率查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryYieldInfo2',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                    { title: '客户名称', field: 'CustName', width: YieldQuery.fixWidth(0.1) },
                    //{ title: '工单号码', field: 'WO', width: YieldQuery.fixWidth(0.1) },
                   { title: '零件图号', field: 'PartsdrawingCode', width: YieldQuery.fixWidth(0.1) },                   
                   { title: '产品名称', field: 'ProductName', width: YieldQuery.fixWidth(0.1) },
                   { title: '批号', field: 'BatchNumber', width: YieldQuery.fixWidth(0.1) },
                   { title: '产出数量', field: 'QUANTITY', width: YieldQuery.fixWidth(0.05) },
                   { title: '合格数量', field: 'PassCount', width: YieldQuery.fixWidth(0.05) },
                   { title: '不良数量', field: 'FailCount', width: YieldQuery.fixWidth(0.05) },
                   { title: '返工数量', field: 'ReturnCount', width: YieldQuery.fixWidth(0.05) },
                   { title: '让步数量', field: 'SecondPass', width: YieldQuery.fixWidth(0.05) },
                   { title: '废品数量', field: 'DiscardCount', width: YieldQuery.fixWidth(0.05) },
                   { title: '合格率', field: 'PassRate', width: YieldQuery.fixWidth(0.05) },
                   { title: '不良率', field: 'FailRate', width: YieldQuery.fixWidth(0.05) },
                   { title: '返工率', field: 'ReturnRate', width: YieldQuery.fixWidth(0.05) },
                   { title: '让步率', field: 'SecPassRate', width: YieldQuery.fixWidth(0.05) },
                   { title: '废品率', field: 'DiscardRate', width: YieldQuery.fixWidth(0.05) },
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
                        YieldQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/QualityManage/export/YieldQueryExport.aspx?custname=' + $('#selCustName').val() + '&partsdrawing=' + $('#txtPartsdrawing').val() + '&starttime=' + $('#txtStartTime').val() + '&endtime=' + $('#txtEndTime').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "../../Pages/WorkOrderManage/BossQuery.aspx";
                    }
                }],
                    onLoadError: function (error) {
                        alert(error.responseText);
                    }
            });
        }
    };
}();