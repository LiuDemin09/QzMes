var WorkOrderAssign = function () {
    return {
        grid: null,
        gridassignhistory: null,
        ID: null,
        status: null,
        isAssign:null,
        //main function to initiate the module
        init: function () {
            $("#divMsg").hide();
            // 初始化工单号状态（0：创建；1：运行；2：暂停；3：关闭；）
            //WsSystem.ListBindWorkOrder("1", function (result) {
            //    JeffComm.fillSelect($("#selWorkOrder"), result, true);
            //});
            // 初始化操作人员
            //WsSystem.ListBindUserByOperators(function (result) {
                 
            //   // JeffComm.fillSelect($("#selOperater"), result, true);
                
            //});
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/ProductionManage/WorkOrderAssignDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/ProductionManage/WorkOrderAssignUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
            });
            jQuery('#btnSave').click(function () {
                var selOperatervalue = $("#selOperater").val();
                var  selOperatername= $("#selOperater").combobox("getText");
                var selstation = $("#selStation").val();
                //var count = $("#selOperater option").length;
                //for (var i = 0; i < count; i++) {
                //    if ($("#selOperater").get(0).options[i].selected == true) {
                //        if (selOperatername == null) {
                //            selOperatername = $("#selOperater").get(0).options[i].text;
                //            selOperatervalue = $("#selOperater").get(0).options[i].value;
                //        }
                //        else {
                //            selOperatername += "," + $("#selOperater").get(0).options[i].text;
                //            selOperatervalue += "," + $("#selOperater").get(0).options[i].value;
                //        }
                //    }
                //};
                WsSystem.SaveWorkOrderAssign($("#txtWorkOrder").val(), selOperatername, selOperatervalue,selstation,
                    function (result) {
                        WorkOrderAssign.bindGrid();
                        WorkOrderAssign.bindGridHistory();
                        JeffComm.errorAlert(result, "divMsg");
                    }, function (err) {
                        JeffComm.errorAlert(err.get_message(), "divMsg");
                    });
            });
            //防止回車提交
            $('#txtInputData').keypress(function (e) {
                if (e.which == 13) {
                     }
            });
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        bindGrid: function () {
            WorkOrderAssign.status = "1";
            WorkOrderAssign.isAssign = "false";
            $('#grid').datagrid('load', { status: WorkOrderAssign.status, isAssign: WorkOrderAssign.isAssign, drawingcode: $('#txtPartCode').val() });
        },
        bindGridHistory: function () {
            WorkOrderAssign.status = "1";
            WorkOrderAssign.isAssign = "true";
            $('#gridhistory').datagrid('load', { status: WorkOrderAssign.status, isAssign: WorkOrderAssign.isAssign, drawingcode: $('#txtRPartCode').val() });
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                view: detailview,
                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryWorkOrderAssign',
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
                     { title: '工单单号', field: 'WO', width: WorkOrderAssign.fixWidth(0.1) },
                     { title: '订单单号', field: 'PartsdrawingCode', width: WorkOrderAssign.fixWidth(0.1) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: WorkOrderAssign.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: WorkOrderAssign.fixWidth(0.08) },
                     { title: '备注', field: 'BatchNumber', width: WorkOrderAssign.fixWidth(0.08) },
                     { title: '计划数量', field: 'PlanQuantity', width: WorkOrderAssign.fixWidth(0.05) },                     
                     { title: '机床类型', field: 'MachineType', width: WorkOrderAssign.fixWidth(0.05) },
                     { title: '机床名称', field: 'MachineName', width: WorkOrderAssign.fixWidth(0.08) },
                     {
                         title: '计划开始时间', field: 'StartTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: WorkOrderAssign.fixWidth(0.1)
                     },
                     {
                         title: '计划结束时间', field: 'EndTime',formatter: function (value, row, index) {
                         if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: WorkOrderAssign.fixWidth(0.1)
                     },

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
                    text: '零件图号 <input type="text" id="txtPartCode"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        WorkOrderAssign.bindGrid();
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    $('#txtWorkOrder').val(rowData["WO"]);
                    WsSystem.ListBindStations(rowData["WO"],function (result) {
                        JeffComm.fillSelect($("#selStation"), result, true);
                    });
                }
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 5,
                pageNumber: 1,
                pageList: [5, 8, 10],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        },
        initGridHistory: function () {
            $('#gridhistory').datagrid({   //定位到Table标签，Table标签的ID是grid
                view: detailview,
                title: '指派记录',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryWorkOrderAssign',
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
                     { title: '工单单号', field: 'WO', width: WorkOrderAssign.fixWidth(0.1) },                      
                     { title: '零件图号', field: 'PartsdrawingCode', width: WorkOrderAssign.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: WorkOrderAssign.fixWidth(0.08) },
                     { title: '负责人员', field: 'WorkerName', width: WorkOrderAssign.fixWidth(0.05) },
                     { title: '备注', field: 'BatchNumber', width: WorkOrderAssign.fixWidth(0.08) },
                     { title: '计划数量', field: 'PlanQuantity', width: WorkOrderAssign.fixWidth(0.05) },
                     { title: '机床类型', field: 'MachineType', width: WorkOrderAssign.fixWidth(0.05) },
                     { title: '机床名称', field: 'MachineName', width: WorkOrderAssign.fixWidth(0.08) },
                     {
                         title: '计划开始时间', field: 'StartTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: WorkOrderAssign.fixWidth(0.1)
                     },
                     { title: '计划结束时间', field: 'EndTime', formatter: function (value, row, index) {
                     if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         },  width: WorkOrderAssign.fixWidth(0.1) },
                ]],
                toolbar: [{
                    text: '零件图号 <input type="text" id="txtRPartCode"/>'
                }, '-', {
                    id: 'btnRSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        WorkOrderAssign.bindGridHistory();
                    }
                }],
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
                onClickRow: function (rowIndex, rowData) {
                $('#txtWorkOrder').val(rowData["WO"]);
                WsSystem.ListBindStations(rowData["WO"], function (result) {
                    JeffComm.fillSelect($("#selStation"), result, true);
                });
            }
            });
            $('#gridhistory').datagrid('getPager').pagination({
                pageSize: 5,
                pageNumber: 1,
                pageList: [5, 8, 10],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        }
    };
}();