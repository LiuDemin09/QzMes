var ProductMarQuery = function () {
    return {
        grid: null, 
        startT: null,
        endT: null,
        partsdrawing: null,
        custname: null,
        init: function () {
            ////加载客户
            //WsSystem.ListBindCustName(function (result) {
            //    JeffComm.fillSelect($("#selCustName"), result, true);
            //});
        },
        bindGrid: function () { 
            $('#grid').datagrid('load', {
                workorder: $("#txtQWorkOrder").val()
                , partsdrawing: $("#txtQPartsdrawing").val()
                , startT: $("#txtQStartTime").val()
                , endT: $("#txtQEndTime").val()
            });

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '综合查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryProductManageInfo',
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
                    { title: '订单号码', field: 'OrderNo', width: ProductMarQuery.fixWidth(0.1) },
                    { title: '工单号码', field: 'WO', width: ProductMarQuery.fixWidth(0.1) },
                   { title: '零件图号', field: 'PartsdrawingCode', width: ProductMarQuery.fixWidth(0.1) },                   
                   { title: '产品名称', field: 'ProductName', width: ProductMarQuery.fixWidth(0.1) },
                   { title: '生产人员', field: 'OperatorName', width: ProductMarQuery.fixWidth(0.1) },
                   { title: '机床名称', field: 'EquipName', width: ProductMarQuery.fixWidth(0.1) },
                   { title: '计划数量', field: 'PlanQuantity', width: ProductMarQuery.fixWidth(0.05) },
                   { title: '产出数量', field: 'QUANTITY', width: ProductMarQuery.fixWidth(0.05) },
                   { title: '合格数量', field: 'PassCount', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '不良数量', field: 'FailCount', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '返工数量', field: 'ReturnCount', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '让步数量', field: 'SecondPass', width: ProductMarQuery.fixWidth(0.05) },
                   { title: '废品数量', field: 'DiscardCount', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '合格率', field: 'PassRate', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '不良率', field: 'FailRate', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '返工率', field: 'ReturnRate', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '让步率', field: 'SecPassRate', width: ProductMarQuery.fixWidth(0.05) },
                   //{ title: '废品率', field: 'DiscardRate', width: ProductMarQuery.fixWidth(0.05) },
                   { title: '额定工时', field: 'PlanUnitTime', width: ProductMarQuery.fixWidth(0.05) },
                   { title: '额定总工时', field: 'PlanTotalUnitTime', width: ProductMarQuery.fixWidth(0.1) },
                   { title: '实际总工时', field: 'ActualTotalUnitTime', width: ProductMarQuery.fixWidth(0.1) },
                ]],
                toolbar: [{
                    text: '零件图号 <input type="text" id="txtQPartsdrawing"/>'
                }, '-', {
                    text: '工单号 <input type="text" id="txtQWorkOrder"/>'
                }, '-', {
                    text: '开始时间 <input type="text" id="txtQStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间 <input type="text" id="txtQEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        ProductMarQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/WorkOrderManage/export/ProductMarQueryExport.aspx?custname=' + $('#selCustName').val() + '&partsdrawing=' + $('#txtPartsdrawing').val() + '&starttime=' + $('#txtStartTime').val() + '&endtime=' + $('#txtEndTime').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }
                //, '-', {
                //    id: 'btnBack',
                //    text: '返回',
                //    iconCls: 'icon-back',
                //    handler: function () {
                //        window.location.href = "QualityMain.aspx";
                //    }
                //}
                ],
                    onLoadError: function (error) {
                        alert(error.responseText);
                    }
            });
        }
    };
}();