var WorkOrderMain = function () {
    return {
        grid: null,
        gridWaitOutlist: null,
       
        init: function () {
           // setInterval("WorkOrderMain.bindGrid()", 60000);//每隔60秒刷新一次
            //setInterval("WorkOrderMain.bindGridWaitOut()", 20000);//每隔20秒刷新一次
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGrid: function () {           
            $('#grid').datagrid('load', {
                orderno: $("#txtQOrderNo").val(),
                partscode: $("#txtQPartCode").val()
            });
        },

        bindGridWaitOut: function () {
            $('#gridWaitOutlist').datagrid('load', { });
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryOrderPublish',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                remoteSort: false,
                autoRowHeight: false,
                // singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [6,10, 15,500],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择                    
                    //{ title: 'ID', field: 'ID', width: WorkOrderMain.fixWidth(0.09), hidden: 'true' },
                   { title: '订单单号', field: 'OrderNo', width: WorkOrderMain.fixWidth(0.1), sortable: true },
                   { title: '零件图号', field: 'PartsdrawingCode', width: WorkOrderMain.fixWidth(0.1),sortable:true },
                   { title: '订单状态', field: 'MEMO', width: WorkOrderMain.fixWidth(0.05), sortable: true },
                   { title: '客户名称', field: 'CustName', width: WorkOrderMain.fixWidth(0.08), sortable: true },
                   { title: '投产总数', field: 'OrderQuantity', width: WorkOrderMain.fixWidth(0.06) },
                   { title: '产品名称', field: 'ProductName', width: WorkOrderMain.fixWidth(0.1) },
                   { title: '交付数量', field: 'OutQuantity', width: WorkOrderMain.fixWidth(0.06) },
                   {
                       title: '交付时间', field: 'OutDate',  formatter: function (value, row, index) {
                           if (value != null & value != "") {
                               var unixTimestamp = new Date(value);
                               return unixTimestamp.toLocaleString();
                           }
                       }, width: WorkOrderMain.fixWidth(0.1), sortable: true
                   }
                ]],
                toolbar: [{
                    text: '订单号码 <input type="text" id="txtQOrderNo"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtQPartCode"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        WorkOrderMain.bindGrid();
                    }
                },
               '-', {
                   id: 'btnExport',
                   text: '导出',
                   iconCls: 'icon-save',
                   handler: function () {
                       window.open('../../Pages/WorkOrderManage/export/WorkOrderMainExport.aspx?orderno='
                           + $('#txtQOrderNo').val()
                            + '&partcode=' + $('#txtQPartCode').val()
                        + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');

                   }
               }]
            });
        },
        initGridWaitOut: function () {
            $('#gridWaitOutlist').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '我的看板',
                view: detailview,
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryWorkOrderMain',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                // singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 6,
                pageNumber: 1,
                pageList: [6, 10, 12],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择                    
                   { title: '工单单号', field: 'WO', width: WorkOrderMain.fixWidth(0.1) },
                   { title: '零件图号', field: 'PartsdrawingCode', width: WorkOrderMain.fixWidth(0.1) },
                   { title: '工单状态', field: 'MEMO', width: WorkOrderMain.fixWidth(0.05) },
                   //{ title: '机床名称', field: 'MachineName', width: WorkOrderMain.fixWidth(0.08) },
                   //{ title: '操作人', field: 'WorkerName', width: WorkOrderMain.fixWidth(0.1) },
                   { title: '产品名称', field: 'ProductName', width: WorkOrderMain.fixWidth(0.1) },
                   //{
                   //    title: '计划开始时间', field: 'StartTime', formatter: function (value, row, index) {
                   //        var unixTimestamp = new Date(value);
                   //        return unixTimestamp.toLocaleString();
                   //    }, width: WorkOrderMain.fixWidth(0.1)
                   //},
                   //{
                   //    title: '计划结束时间', field: 'EndTime', formatter: function (value, row, index) {
                   //        var unixTimestamp = new Date(value);
                   //        return unixTimestamp.toLocaleString();
                   //    }, width: WorkOrderMain.fixWidth(0.1)
                   //},
                   { title: '计划数量', field: 'PlanQuantity', width: WorkOrderMain.fixWidth(0.65) },
                   //{ title: '产出数量', field: 'QUANTITY', width: WorkOrderMain.fixWidth(0.05) },

                ]],
                detailFormatter: function (index, row) {//注意2  
                    return '<div ><table id="ddv-' + index + '" style=""></table></div>';
                },
                onExpandRow: function (index, row) {
                    var ddv = $(this).datagrid('getRowDetail', index).find('#ddv-' + index);//严重注意喔 
                    ddv.datagrid({
                        url: '../../services/WsSystem.asmx/QueryWODetailsNewBySubQuery?workorder=' + row.WO,
                        autoRowHeight: true,
                        fitColumns: true,
                        singleSelect: true,
                        height: 'auto',
                        columns: [[
                            { field: 'WO', title: '工单单号', width: 50 },
                            {field: 'StatusMemo', title: '状态', width: 30},
                             { field: 'OrderNumber', title: '订单单号', width: 50 },
                            { field: 'PartsdrawingCode', title: '零件图号', width: 50 },
                            { field: 'RouteCode', title: '工序号', width: 30 },
                            { field: 'StationName', title: '工序', width: 50 },
                             { field: 'PlanQuantity', title: '计划数量', width: 30 },
                            { field: 'QUANTITY', title: '产出数量', width: 30 },
                             { field: 'MachineType', title: '机床类型', width: 50 },
                            { field: 'MachineName', title: '机床名称', width: 50 },
                             { field: 'WorkerName', title: '负责人', width: 50 }, 
                            { field: 'ProductName', title: '产品名称', width: 50 },
                            { field: 'QualityCode', title: '质量编号', width: 50 },
                            {
                                field: 'StartTime', title: '计划开始', formatter: function (value, row, index) {
                                    if (value != null & value != "") {
                                        var unixTimestamp = new Date(value);
                                        return unixTimestamp.toLocaleString();
                                    }
                                }, width: 70
                            },
                            {
                                field: 'EndTime', title: '计划结束', formatter: function (value, row, index) {
                                    if (value != null & value != "") {
                                        var unixTimestamp = new Date(value);
                                        return unixTimestamp.toLocaleString();
                                    }
                                }, width: 70
                            },
                            { field: 'BatchNumber', title: '备注', width: 50 },
                            {
                                field: 'CheckTime', title: '计划检验', formatter: function (value, row, index) {
                                    if (value != null & value != "") {
                                        var unixTimestamp = new Date(value);
                                        return unixTimestamp.toLocaleString();
                                    }
                                }, width: 70
                            },
                            {
                                field: 'InstockTime', title: '计划入库', formatter: function (value, row, index) {
                                    if (value != null & value != "") {
                                        var unixTimestamp = new Date(value);
                                        return unixTimestamp.toLocaleString();
                                    }
                                }, width: 70
                            },
                             { title: '生产班次', field: 'CLASS', width: 30 },
                            { title: '工时', field: 'UnitTime', width: 20 }

                        ]],
                        rowStyler: function (index, row) {
                            if (row.StatusMemo == "创建") {
                                return 'background-color:#B7FF4A;color:#000;';
                            }
                            else if (row.StatusMemo == "运行") {
                                return 'background-color:#00ff00;color:#000;';
                            } else if (row.StatusMemo == "暂停") {
                                return 'background-color:#D9B300;color:#000;';
                            }
                        },
                        onResize: function () {
                            $('#gridWaitOutlist').datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess: function () {
                            setTimeout(function () {
                                $('#gridWaitOutlist').datagrid('fixDetailRowHeight', index);
                                $('#gridWaitOutlist').datagrid('fixRowHeight', index);
                            }, 0);
                        }
                    });
                },
            });
        }
    };

}();