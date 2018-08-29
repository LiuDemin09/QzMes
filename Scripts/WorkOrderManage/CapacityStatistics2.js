var CapacityStatistics = function () {
    return {
        grid: null,
        workorderNo: null,
        OrderNumber: null,
        PartsdrawingCode: null,
        Status: null,
        StartTime: null,
        EndTime: null,
        //main function to initiate the module
        init: function () {

            //初始化订单单号
            //WsSystem.ListBindOrderNo(function (result) {
            //    JeffComm.fillSelect($("#selOrderNo"), result, true);
            //});

        },
        bindGrid: function () {
            CapacityStatistics.workorderNo = $('#txtWorkOrder').val().trim();
            CapacityStatistics.OrderNumber = $('#txtOrderNumber').val().trim();
            CapacityStatistics.PartsdrawingCode = $('#txtPartsDrawingNo').val().trim();
            CapacityStatistics.Status = $('#selWOStatus').val();
            CapacityStatistics.StartTime = $('#txtStartTime').val().trim();
            CapacityStatistics.EndTime = $('#txtEndTime').val().trim();
            var oDate1 = new Date(CapacityStatistics.StartTime);
            var oDate2 = new Date(CapacityStatistics.EndTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', {
                workorder: CapacityStatistics.workorderNo, OrderNumber: CapacityStatistics.OrderNumber, PartsdrawingCode: CapacityStatistics.PartsdrawingCode,
                Status: CapacityStatistics.Status, StartTime: CapacityStatistics.StartTime, EndTime: CapacityStatistics.EndTime
            });
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                view: detailview,
                title: '产能统计',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryCapacityStatistics',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                columns: [[ //选择
                     { title: '工单单号', field: 'WO', width: CapacityStatistics.fixWidth(0.1) },
                     { title: '工单状态', field: 'MEMO', width: CapacityStatistics.fixWidth(0.05) },
                     { title: '订单单号', field: 'OrderNumber', width: CapacityStatistics.fixWidth(0.1) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: CapacityStatistics.fixWidth(0.1) },
                     { title: '机床类型', field: 'MachineType', width: CapacityStatistics.fixWidth(0.05) },
                     { title: '机床名称', field: 'MachineName', width: CapacityStatistics.fixWidth(0.08) },
                     { title: '质量编号', field: 'QualityCode', width: CapacityStatistics.fixWidth(0.1) },
                     { title: '负责人员', field: 'WorkerName', width: CapacityStatistics.fixWidth(0.05) },
                     { title: '计划数量', field: 'PlanQuantity', width: CapacityStatistics.fixWidth(0.05) },
                     { title: '产出数量', field: 'QUANTITY', width: CapacityStatistics.fixWidth(0.05) },
                     { title: '产品名称', field: 'ProductName', width: CapacityStatistics.fixWidth(0.08) },
                     {
                         title: '计划开始', field: 'StartTime',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: CapacityStatistics.fixWidth(0.1)
                     },
                     {
                         title: '计划结束', field: 'EndTime',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: CapacityStatistics.fixWidth(0.1)
                     },
                     { title: '备注', field: 'BatchNumber', width: CapacityStatistics.fixWidth(0.1) },
                     
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
                            {
                                field: 'StatusMemo', title: '状态', width: 30
                            },
                             { field: 'OrderNumber', title: '订单单号', width: 50 },
                            { field: 'PartsdrawingCode', title: '零件图号', width: 50 },
                            { field: 'RouteCode', title: '工序号', width: 30 },
                            { field: 'StationName', title: '工序', width: 50 },
                             { field: 'MachineType', title: '机床类型', width: 50 },
                            { field: 'MachineName', title: '机床名称', width: 50 },
                             { field: 'WorkerName', title: '负责人', width: 50 },
                            { field: 'PlanQuantity', title: '计划数量', width: 30 },
                            { field: 'QUANTITY', title: '产出数量', width: 30 },
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
                            $('#grid').datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess: function () {
                            setTimeout(function () {
                                $('#grid').datagrid('fixDetailRowHeight', index);
                                $('#grid').datagrid('fixRowHeight', index);
                            }, 0);
                        }
                    });
                },
                toolbar: [{
                    text: '工单单号 <input type="text" id="txtWorkOrder"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtPartsDrawingNo"/>'
                }, '-', {
                    text: '订单单号 <input type="text" id="txtOrderNumber"/>'
                }, '-', {
                    text: '工单状态 <select tabindex="-1" name="selWOStatus" id="selWOStatus"><option value="4">工单状态</option><option value="0">创建</option> <option value="1">运行</option><option value="2">暂停</option><option value="3">关闭</option></select>'
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
                        CapacityStatistics.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/WorkOrderManage/export/CapacityStatisticsExport.aspx?workorder=' + $('#txtWorkOrder').val() + '&starttime=' + $('#txtStartTime').val()
                    + '&endtime=' + $('#txtEndTime').val() + '&status=' + $('#selWOStatus').val() + '&order=' + $('#txtOrderNumber').val()
                    + '&partsdrawingno=' + $('#txtPartsDrawingNo').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "')
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "BossQuery.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    CapacityStatistics.workorderNo = rowData["WO"];
                    // ShowEditOrViewDialog();
                },
                // loader: function (param, success, error) { }
                onLoadError: function (error) {
                    alert(error.responseText);
                }

            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        }
    };
}();