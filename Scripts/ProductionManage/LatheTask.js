var LatheTask = function () {
    return {
        grid: null,
        gridproducthistory: null,
        ID: null,
        //main function to initiate the module
        init: function () {
            $("#divMsg").hide();
            $('#txtInputData').focus();
            WsSystem.LoadNoScanData(function (result) {
                    if(result!=null)
                    {                         
                        LatheTask.ID = result[0];
                         if (result[4] == "1")//条码
                        {
                            $('#btnpsn').css("background", "green");
                            $('#btnpsn').text(result[1] + "-->");
                            $('#btnresult').css("background", "yellow");
                            //$('#btnfinish').css("background", "yellow");
                        }
                        else if (result[4] == "2")//结果
                        {
                            //$('#btnstart').css("background", "green");
                            //$('#btnstart').text("已开工-->");
                            $('#btnpsn').css("background", "green");
                            $('#btnpsn').text(result[1] + "-->");
                            $('#btnresult').css("background", "green");
                            $('#btnresult').text(result[3] + "-->");
                            // $('#btnfinish').css("background", "yellow");
                        } 
                    }
                }) 
        },
        isFinish: function (psn, res) {
            var temppsn = psn.split(",");
            var templen1 = temppsn.length;
            var tempres = res.split(",");
            var templen2 = tempres.length;
            if (templen1 == templen2) {
                $('#btnpsn').css("background", "gray");
                $('#btnresult').css("background", "gray");
                $('#btnpsn').text("扫成品条码-->");
                $('#btnresult').text("扫合格或不合格");
                LatheTask.ID = null;
                LatheTask.bindGrid();//更新上边列表
                LatheTask.bindGridHistory();//更新下边列表
            }
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        bindGrid: function () {
            $('#grid').datagrid('load', { worker:""});
        },
        bindGridHistory: function () {
            $('#gridhistory').datagrid('load', { worker: "" });
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryLatheTask',
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
                     { title: '工单单号', field: 'WO', width: LatheTask.fixWidth(0.1) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: LatheTask.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: LatheTask.fixWidth(0.08) },
                     { title: '备注', field: 'BatchNumber', width: LatheTask.fixWidth(0.08) },
                     { title: '计划数量', field: 'PlanQuantity', width: LatheTask.fixWidth(0.05) },
                     { title: '完成数量', field: 'QUANTITY', width: LatheTask.fixWidth(0.05) },
                     { title: '工序', field: 'StationName', width: LatheTask.fixWidth(0.05) },
                     { title: '工序号', field: 'RouteCode', width: LatheTask.fixWidth(0.05) },
                     { title: '机床类型', field: 'MachineType', width: LatheTask.fixWidth(0.05) },
                     { title: '机床名称', field: 'MachineName', width: LatheTask.fixWidth(0.08) },
                     {
                         title: '计划开始时间', field: 'StartTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: LatheTask.fixWidth(0.1)
                     },
                     {
                         title: '计划结束时间', field: 'EndTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: LatheTask.fixWidth(0.1)
                     },

                ]],
                 toolbar: [{
                     text: '生产报工 <input type="text" autocomplete="off" id="txtInputData"/>'
                 
               
                }],
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

            //产品条码回车事件
            $('#txtInputData').keydown(function (e) {
               // if (e.keyCode == 13) {
                    if (e.which == 13) {
                        if ($("#txtInputData").val() == "snquery") {
                            window.open("../../Pages/ProductionManage/SNTracking.aspx");
                            $('#txtInputData').val("");
                            $('#txtInputData').focus();
                            return true;
                        }
                        if ($("#txtInputData").val() == "nextemp") {
                            window.open("../../Pages/ProductionManage/TaskHandover.aspx?ID=" + LatheTask.ID);
                            //$("#taskhandoverDialog").dialog('open');
                            //$("#txtUserCode").focus();
                            $('#txtInputData').val("");
                            $('#txtInputData').focus();
                            return true;
                        }
                        var temp = $("#txtInputData");
                        temp = temp.val();
                        WsSystem.DealwithScanDataNew($("#txtInputData").val(), LatheTask.ID,
                                function (result) {
                                    //如果ID为空，表示新的开始，此时应扫描检验结果，以获取新的ID
                                    if (LatheTask.ID == null || LatheTask.ID == "") {
                                        LatheTask.ID = result;
                                        $("#divMsg").hide();
                                    }
                                    //如果扫描的是cancel或finish，则说明取消加工或加工完成，此时ID赋为空
                                    if ($("#txtInputData").val() == "cancel" || $("#txtInputData").val() == "CANCEL") {
                                        LatheTask.ID = null; 
                                        $('#btnpsn').css("background", "gray");
                                        $('#btnresult').css("background", "gray"); 
                                        $('#btnpsn').text("扫成品条码-->");
                                        $('#btnresult').text("扫合格或不合格"); 
                                        LatheTask.bindGrid();//更新上边列表
                                        LatheTask.bindGridHistory();//更新下边列表
                                    } 
                                    else if ($("#txtInputData").val() == "pass" || $("#txtInputData").val() == "PASS") { 
                                        $('#btnpsn').css("background", "green");
                                        $('#btnresult').css("background", "green");
                                        if ($('#btnresult').text() == "扫合格或不合格") {
                                            $('#btnresult').text("合格");
                                        }
                                        else {
                                            $('#btnresult').text($('#btnresult').text() + ",合格");
                                        }
                                       // $('#btnresult').text($('#btnresult').text() + "," + $("#txtInputData").val() + "-->");
                                        LatheTask.isFinish($('#btnpsn').text(), $('#btnresult').text());
                                    }
                                    else if ($("#txtInputData").val() == "fail" || $("#txtInputData").val() == "FAIL") {
                                        $('#btnpsn').css("background", "green");
                                        $('#btnresult').css("background", "green");
                                        if ($('#btnresult').text() == "扫合格或不合格") {
                                            $('#btnresult').text("不合格");
                                        }
                                        else {
                                            $('#btnresult').text($('#btnresult').text() + ",不合格");
                                        }
                                        // $('#btnresult').text($('#btnresult').text() + "," + $("#txtInputData").val() + "-->");
                                        LatheTask.isFinish($('#btnpsn').text(), $('#btnresult').text());
                                    }
                                    else if ($("#txtInputData").val().length == 15) { 
                                        $('#btnpsn').css("background", "green");
                                        $('#btnresult').css("background", "yellow"); 
                                        //$('#btnpsn').text($('#btnpsn').text() + "," + $("#txtInputData").val() + "-->");
                                        if ($('#btnpsn').text() == "扫成品条码-->") {
                                            $('#btnpsn').text($("#txtInputData").val());
                                        }
                                        else {
                                            $('#btnpsn').text($('#btnpsn').text() + "," + $("#txtInputData").val());
                                        }
                                    }
                                    else if ($("#txtInputData").val() == "pause") {
                                        $('#btnpsn').css("background", "yellow");
                                        $('#btnresult').css("background", "yellow");
                                        $('#btnpsn').text("扫成品条码-->");
                                        $('#btnresult').text("扫合格或不合格");
                                    }
                                    $('#txtInputData').val("");
                                    $('#txtInputData').focus();
                                }, function (err) {
                                    $("#divMsg").show();
                                    JeffComm.alertErr(err.get_message(), 5000);
                                    $('#txtInputData').val("");
                                    $('#txtInputData').focus();
                                });
                   // }
                   
                }
            });
        },
        initGridHistory: function () {
            $('#gridhistory').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '生产记录',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryLatheTaskHistory',
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
                     { title: '产品条码', field: 'PSN', width: LatheTask.fixWidth(0.1) },
                     
                     { title: '结果', field: 'STATUS', width: LatheTask.fixWidth(0.05) },
                     { title: '工序', field: 'StationName', width: LatheTask.fixWidth(0.05) },
                     { title: '工序号', field: 'StationId', width: LatheTask.fixWidth(0.05) },
                     { title: '下一序', field: 'NextStation', width: LatheTask.fixWidth(0.05) },
                     { title: '下工序号', field: 'NextStationId', width: LatheTask.fixWidth(0.05) },
                     { title: '工单号码', field: 'WorkOrder', width: LatheTask.fixWidth(0.1) },
                     { title: '工件图号', field: 'PartsdrawingCode', width: LatheTask.fixWidth(0.1) },
                     { title: '工件名称', field: 'PartsName', width: LatheTask.fixWidth(0.08) },
                     { title: '生产批号', field: 'BatchNumber', width: LatheTask.fixWidth(0.1) },
                     { title: '数量', field: 'QUANTITY', width: LatheTask.fixWidth(0.05) },
                     { title: '来料条码', field: 'MSN', width: LatheTask.fixWidth(0.1) },
                     {
                         title: '加工时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: LatheTask.fixWidth(0.1)
                     },
                ]]
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