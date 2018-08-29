var CartonPrint = function () {
    return {
        cartonNo: null,
        startTime: null,
        endTime: null,
        init: function () {
            $("#divMsg").hide();
            $("#divMsg1").hide();
            $("#txtPartsdrawingNo").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;                    
                    $("#txtQualityCode").focus();
                }
                return false;
            });
            $("#txtQualityCode").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;                    
                    $("#txtQty").focus();
                }
            });

            $("#txtPartsdrawingNo1").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;                    
                    $("#txtProductName1").focus();
                }
                return false;
            });
            $("#txtProductName1").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;                    
                    $("#txtBatchNum1").focus();
                }
            });
            $("#txtBatchNum1").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;                    
                    $("#txtQty1").focus();
                }
            });
            $("#txtQty").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;       
                    if ($('#txtPartsdrawingNo').val() == null || $('#txtPartsdrawingNo').val() == "" ||
                        $('#txtQualityCode').val() == null || $('#txtQualityCode').val() == "" ||
                        $('#txtQty').val() == null || $('#txtQty').val() == "") {
                        JeffComm.alertErr("零件图号，质量编号，数量等信息输入不完全，请检查");
                       // alert("信息输入不完全，请检查");
                    } else {
                        WsSystem.SaveCartonTemp($('#txtPartsdrawingNo').val(), $('#txtQualityCode').val(),"","" ,$('#txtQty').val(),"left",
                               function (result) {
                                   //JeffComm.succAlert("Saved successfully.", "divMsg");
                                   $('#txtQualityCode').val("");
                                   $("#txtPartsdrawingNo").val("");
                                   $("#txtQty").val("");
                                   $("#txtPartsdrawingNo").focus();
                                   CartonPrint.bindGridData();//更新中间列表                               
                               }, function (err) {
                                   JeffComm.errorAlert(err.get_message(), "divMsg");
                               });
                    }
                }
            });
            $("#txtQty1").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;       
                    if ($('#txtPartsdrawingNo1').val() == null || $('#txtPartsdrawingNo1').val() == "" ||
                        $('#txtProductName1').val() == null || $('#txtProductName1').val() == "" ||
                         $('#txtBatchNum1').val() == null || $('#txtBatchNum1').val() == "" ||
                        $('#txtQty1').val() == null || $('#txtQty1').val() == "") {
                        JeffComm.alertErr("零件图号，产品名称，生产批号，数量等信息输入不完全，请检查");
                        // alert("信息输入不完全，请检查");
                    } else {
                        WsSystem.SaveCartonTemp($('#txtPartsdrawingNo1').val(),"", $('#txtProductName1').val(), $('#txtBatchNum1').val(), $('#txtQty1').val(),"right",
                               function (result) {
                                   //JeffComm.succAlert("Saved successfully.", "divMsg");
                                   $('#txtProductName1').val("");
                                   $('#txtBatchNum1 ').val("");
                                   $("#txtPartsdrawingNo1").val("");
                                   $("#txtQty1").val("");
                                   $("#txtPartsdrawingNo1").focus();
                                   CartonPrint.bindGridData();//更新中间列表                               
                               }, function (err) {
                                   JeffComm.errorAlert(err.get_message(), "divMsg");
                               });
                    }
                }
            });
            //重置
            jQuery('#btnReset').click(function () {
                WsSystem.RemoveCartonTemp("left");
                $('#txtQualityCode').val("");
                $("#txtPartsdrawingNo").val("");
                $("#txtQty").val("");
                $("#txtPartsdrawingNo").focus();
                CartonPrint.bindGridData();//更新中间列表 
            });
            jQuery('#btnReset1').click(function () {
                WsSystem.RemoveCartonTemp("right");
                $('#txtProductName1').val("");
                $('#txtBatchNum1 ').val("");
                $("#txtPartsdrawingNo1").val("");
                $("#txtQty1").val("");
                $("#txtPartsdrawingNo1").focus();
                CartonPrint.bindGridData();//更新中间列表 
            });
            //保存
            jQuery('#btnSave').click(function () {

                WsSystem.SaveCartonInfo("left",function (result) {
                    WsSystem.RemoveCartonTemp("left");
                    $('#txtQualityCode').val("");
                    $("#txtPartsdrawingNo").val("");
                    $("#txtQty").val("");
                    $("#txtPartsdrawingNo").focus();
                    CartonPrint.bindGridHistoryData();//更新下边列表 
                    CartonPrint.bindGridData();
                    //开始打印
                    //var vres = new Array();
                    // vres = result.split(",");
                    //for (i = 0; i < vres.length; i++) {
                    //打印條碼  
                    var filePath = JeffComm.GetLocalPath() + "csn.lab";
                    var qty = 1;
                    var ID = '3';//1.PSN  2.MSN  3.CSN
                    if (!JeffComm.CommonLabelPrint(filePath, result, ID, qty)) {
                        //JeffComm.errorAlert("打印失敗", "divMsg");
                        JeffComm.errorAlert(StringRes.PrintFail, "divMsg");
                        var htmtxt = "<tr><td >#</td>";
                        htmtxt += "<td>" + result + "</td>";
                        var htmlRes = "";
                        htmlRes = '<span class="label label-warning">FAIL</span>';
                        htmtxt += "<td>" + htmlRes + "</td>";
                        htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
                        $("#tblHistory tr:first").after(htmtxt);
                        return false;
                    }
                    // }
                }, function (err) {
                    JeffComm.errorAlert(err.get_message(), "divMsg");
                });

            });
            jQuery('#btnSave1').click(function () {

                WsSystem.SaveCartonInfo("right", function (result) {
                    WsSystem.RemoveCartonTemp("right");
                    $('#txtProductName1').val("");
                    $('#txtBatchNum1 ').val("");
                    $("#txtPartsdrawingNo1").val("");
                    $("#txtQty1").val("");
                    $("#txtPartsdrawingNo1").focus();
                    CartonPrint.bindGridHistoryData();//更新下边列表 
                    CartonPrint.bindGridData();
                    //开始打印
                    //var vres = new Array();
                    // vres = result.split(",");
                    //for (i = 0; i < vres.length; i++) {
                    //打印條碼  
                    var filePath = JeffComm.GetLocalPath() + "csn.lab";
                    var qty = 1;
                    var ID = '3';//1.PSN  2.MSN  3.CSN
                    if (!JeffComm.CommonLabelPrint(filePath, result, ID, qty)) {
                        //JeffComm.errorAlert("打印失敗", "divMsg");
                        JeffComm.errorAlert(StringRes.PrintFail, "divMsg");
                        var htmtxt = "<tr><td >#</td>";
                        htmtxt += "<td>" + result + "</td>";
                        var htmlRes = "";
                        htmlRes = '<span class="label label-warning">FAIL</span>';
                        htmtxt += "<td>" + htmlRes + "</td>";
                        htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
                        $("#tblHistory tr:first").after(htmtxt);
                        return false;
                    }
                    // }
                }, function (err) {
                    JeffComm.errorAlert(err.get_message(), "divMsg");
                });

            });
        },


        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGridData: function () {
            $('#grid').datagrid('load', {});
        },
        bindGridHistoryData: function () {
            $('#gridcartonhistory').datagrid('load', {});
        },
        bindGrid: function () {
            CartonPrint.cartonNo = $("#txtCartonNo").val();
            CartonPrint.startTime = $("#txtStartTime").val();
            CartonPrint.endTime = $("#txtEndTime").val();
            if (CartonPrint.startTime != null & CartonPrint.startTime != ""
                & CartonPrint.endTime != null & CartonPrint.endTime != ""
                & CartonPrint.startTime > CartonPrint.endTime) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间", "提示");
            } else {

                $('#gridcartonhistory').datagrid('load', {
                    cartonNo: CartonPrint.cartonNo,
                    startTime: CartonPrint.startTime, endTime: CartonPrint.endTime
                });
            }
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '信息预览',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryCartonTemp',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 5,
                pageNumber: 1,
                pageList: [5, 10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '产品图号', field: 'PartsdrawingCode', width: CartonPrint.fixWidth(0.11) },
                     { title: '质量编号', field: 'QualityCode', width: CartonPrint.fixWidth(0.08) },
                     { title: '产品名称', field: 'ProductName', width: CartonPrint.fixWidth(0.08) },
                     { title: '生产批号', field: 'BatchNumber', width: CartonPrint.fixWidth(0.08) },
                     { title: '件数', field: 'QUANTITY', width: CartonPrint.fixWidth(0.08) }
                ]]
            });


            $('#gridcartonhistory').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '历史记录',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryCartonInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 5,
                pageNumber: 1,
                pageList: [5, 10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '箱号', field: 'CSN', width: CartonPrint.fixWidth(0.11) },
                     { title: '产品图号', field: 'PartsdrawingCode', width: CartonPrint.fixWidth(0.11) },
                     { title: '质量编号', field: 'QualityCode', width: CartonPrint.fixWidth(0.08) },
                     { title: '产品名称', field: 'ProductName', width: CartonPrint.fixWidth(0.08) },
                     { title: '生产批号', field: 'BatchNumber', width: CartonPrint.fixWidth(0.08) },
                     { title: '件数', field: 'QUANTITY', width: CartonPrint.fixWidth(0.08) },
                     { title: '操作人', field: 'UpdatedBy', width: CartonPrint.fixWidth(0.08) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: CartonPrint.fixWidth(0.1)
                     }

                ]],
                toolbar: [{
                    text: '箱号 <input type="text" id="txtCartonNo" />'
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
                       CartonPrint.bindGrid();
                   }
               }],
            });
        }

    };

}();