var QCCheck = function () {
    return {
        grid: null,
        gridproducthistory: null,
        ID: null,
        //main function to initiate the module
        init: function () {
            $("#divMsg").hide();
            $('#txtInputData').focus();
            //防止回車提交
            $('#txtInputData111').keyup(function (e) {
                if (e.which == 13) {
                    //if ($('.form-horizontal').validate().form()) {
                    //    var outmaterialinfo = {
                    //        MSN: $("#txtResendMSN").val(),
                    //        MaterialHandler: $("#txtGetBy").val(),
                    //        WorkOrder: $("#selWorkOrder").val(),                           
                    //    };
                    if ($("#txtInputData").val() == "snquery")
                    {
                        window.open("../../Pages/ProductionManage/SNTracking.aspx");
                        $('#txtInputData').val("");
                        $('#txtInputData').focus();
                        return true;
                    }
                    WsSystem.DealwithQCScanData($("#txtInputData").val(),QCCheck.ID,
                            function (result) {
                                //如果ID为空，表示新的开始，此时应扫描检验结果，以获取新的ID
                                if (QCCheck.ID == null || QCCheck.ID == "")
                                {
                                    QCCheck.ID = result;
                                    $("#divMsg").hide();
                                }
                                //如果扫描的是cancel，则说明取消检验，此时ID赋为空
                                if ($("#txtInputData").val() == "cancel")
                                {
                                    QCCheck.ID = null;
                                    $('#btnresult').css("background", "gray");
                                    $('#btnfailcode').css("background", "gray");
                                    $('#btnpsn').css("background", "gray");
                                    $("#divMsg").hide();
                                    $('#btnresult').text("扫合格或不合格-->");
                                    $('#btnfailcode').text("扫不良代码-->");
                                    $('#btnpsn').text("扫成品条码");
                                }
                                else if ($("#txtInputData").val() == "pass") {
                                    $('#btnresult').css("background", "green");
                                    $('#btnfailcode').css("background", "green");
                                    $('#btnpsn').css("background", "yellow");
                                    $('#btnresult').text($("#txtInputData").val());
                                    $('#btnfailcode').text("-->");
                                }
                                else if ($("#txtInputData").val() == "fail") {
                                    $('#btnresult').css("background", "green");
                                    $('#btnfailcode').css("background", "yellow");
                                    $('#btnpsn').css("background", "yellow");
                                    $('#btnresult').text($("#txtInputData").val() + "-->");
                                }
                                else if ($("#txtInputData").val().length == 15) {
                                    $('#btnresult').css("background", "gray");
                                    $('#btnfailcode').css("background", "gray");
                                    $('#btnpsn').css("background", "gray");
                                    $('#btnresult').text("扫合格或不合格-->");
                                    $('#btnfailcode').text("扫不良代码-->");
                                    $('#btnpsn').text("扫成品条码");
                                    QCCheck.ID = null;
                                    QCCheck.bindGrid();//更新上边列表
                                    QCCheck.bindGridHistory();//更新下边列表
                                }
                                else {
                                    $('#btnresult').css("background", "green");
                                    $('#btnfailcode').css("background", "green");
                                    $('#btnpsn').css("background", "yellow");
                                    $('#btnfailcode').text($("#txtInputData").val() + "-->");
                                }
                                $('#txtInputData').val("");
                                $('#txtInputData').focus();
                               
                            }, function (err) {
                                JeffComm.alertErr(err.get_message(), 5000);
                                //JeffComm.errorAlert(err.get_message(), "divMsg");
                                $('#txtInputData').val("");
                                $('#txtInputData').focus();
                            });
                    }
                   return false;
                });
        },
      
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        bindGrid: function () {           
            $('#grid').datagrid('load', { });
        },
        bindGridHistory: function () {            
            $('#gridhistory').datagrid('load', { });
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryQCTask',
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
                     { title: '工单单号', field: 'WO', width: QCCheck.fixWidth(0.1) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: QCCheck.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: QCCheck.fixWidth(0.08) },
                     { title: '备注', field: 'BatchNumber', width: QCCheck.fixWidth(0.08) },
                     { title: '计划数量', field: 'PlanQuantity', width: QCCheck.fixWidth(0.05) },
                     { title: '完成数量', field: 'QUANTITY', width: QCCheck.fixWidth(0.05) },
                     { title: '机床类型', field: 'MachineType', width: QCCheck.fixWidth(0.05) },
                     { title: '机床名称', field: 'MachineName', width: QCCheck.fixWidth(0.08) },
                     { title: '负责人员', field: 'WorkerName', width: QCCheck.fixWidth(0.06) },                     
                     {
                         title: '计划开始时间', field: 'StartTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: QCCheck.fixWidth(0.1)
                     },
                     { title: '计划结束时间', field: 'EndTime',formatter: function (value, row, index) {
                         if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }
                         },  width: QCCheck.fixWidth(0.1) },
                     
                ]],
                toolbar: [{
                    text: '终检作业 <input type="text" autocomplete="off" id="txtInputData"/>'
                } ],
                //toolbar: [{
                //    id: 'btnReload',
                //    text: '刷新',
                //    iconCls: 'icon-reload',
                //    handler: function () {
                //        //实现刷新栏目中的数据
                //        $("#grid").datagrid("reload");
                //    }
                //}]
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
                    WsSystem.DealwithQCScanData($("#txtInputData").val(), QCCheck.ID,
                            function (result) {
                                //如果ID为空，表示新的开始，此时应扫描检验结果，以获取新的ID
                                if (QCCheck.ID == null || QCCheck.ID == "") {
                                    QCCheck.ID = result;
                                    $("#divMsg").hide();
                                }
                                //如果扫描的是cancel，则说明取消检验，此时ID赋为空
                                if ($("#txtInputData").val() == "cancel") {
                                    QCCheck.ID = null;
                                    $('#btnresult').css("background", "gray");
                                    $('#btnfailcode').css("background", "gray");
                                    $('#btnpsn').css("background", "gray");
                                    $("#divMsg").hide();
                                    $('#btnresult').text("扫合格或不合格-->");
                                    $('#btnfailcode').text("扫不良代码-->");
                                    $('#btnpsn').text("扫成品条码");
                                }
                                else if ($("#txtInputData").val() == "pass") {
                                    $('#btnresult').css("background", "green");
                                    $('#btnfailcode').css("background", "green");
                                    $('#btnpsn').css("background", "yellow");
                                    $('#btnresult').text($("#txtInputData").val());
                                    $('#btnfailcode').text("-->");
                                }
                                else if ($("#txtInputData").val() == "fail") {
                                    $('#btnresult').css("background", "green");
                                    $('#btnfailcode').css("background", "yellow");
                                    $('#btnpsn').css("background", "yellow");
                                    $('#btnresult').text($("#txtInputData").val() + "-->");
                                }
                                else if ($("#txtInputData").val().length == 15) {
                                    $('#btnresult').css("background", "gray");
                                    $('#btnfailcode').css("background", "gray");
                                    $('#btnpsn').css("background", "gray");
                                    $('#btnresult').text("扫合格或不合格-->");
                                    $('#btnfailcode').text("扫不良代码-->");
                                    $('#btnpsn').text("扫成品条码");
                                    QCCheck.ID = null;
                                    QCCheck.bindGrid();//更新上边列表
                                    QCCheck.bindGridHistory();//更新下边列表
                                }
                                else {
                                    $('#btnresult').css("background", "green");
                                    $('#btnfailcode').css("background", "green");
                                    $('#btnpsn').css("background", "yellow");
                                    $('#btnfailcode').text($("#txtInputData").val() + "-->");
                                }
                                $('#txtInputData').val("");
                                $('#txtInputData').focus();

                            }, function (err) {
                                JeffComm.alertErr(err.get_message(), 5000);
                                //JeffComm.errorAlert(err.get_message(), "divMsg");
                                $('#txtInputData').val("");
                                $('#txtInputData').focus();
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
                title: '检验记录',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryQCCheckHistory',
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
                     { title: '产品条码', field: 'PSN', width: QCCheck.fixWidth(0.1) },
                     { title: '来料条码', field: 'MSN', width: QCCheck.fixWidth(0.12) },
                     { title: '检验结果', field: 'STATUS', width: QCCheck.fixWidth(0.1) },
                     { title: '工单号码', field: 'WorkOrder', width: QCCheck.fixWidth(0.1) },
                     { title: '工件图号', field: 'PartsdrawingCode', width: QCCheck.fixWidth(0.1) },
                     { title: '工件名称', field: 'PartsName', width: QCCheck.fixWidth(0.12) },
                     { title: '生产批号', field: 'BatchNumber', width: QCCheck.fixWidth(0.1) },
                     { title: '加工工序', field: 'StationName', width: QCCheck.fixWidth(0.1) },
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