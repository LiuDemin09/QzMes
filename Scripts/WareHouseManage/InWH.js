var InWH = function () {

    return {
        grid: null,
        transferEmp: null,
        delivery: null,
        priductCode: null,
        init: function () {
            $("#divMsg").hide();
            //WsSystem.ListBaseByCode("QZB17040003", function (result) {
            //    JeffComm.fillSelect($("#selDelivery"), result, true);
            //});
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '入库作业',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/ListTopProductInWH',
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
                pageList: [10, 15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                      { title: '产品条码', field: 'PSN', width: InWH.fixWidth(0.1) },
                     { title: '工单单号', field: 'WorkOrder', width: InWH.fixWidth(0.1) },
                     { title: '交货单位', field: 'MANUFACTURE', width: InWH.fixWidth(0.07) },
                     { title: '单据编号', field: 'DOCUMENTID', width: InWH.fixWidth(0.08) },
                     { title: '收货仓库', field: 'StockHouse', width: InWH.fixWidth(0.06) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: InWH.fixWidth(0.08) },
                     { title: '物料名称', field: 'ProductName', width: InWH.fixWidth(0.08) },
                     { title: '单位', field: 'UNIT', width: InWH.fixWidth(0.05) },
                     { title: '数量', field: 'QUANTITY', width: InWH.fixWidth(0.05) },
                     { title: '批号', field: 'BatchNumber', width: InWH.fixWidth(0.06) },
                     { title: '移交员', field: 'FromBy', width: InWH.fixWidth(0.06) },
                     { title: '操作人', field: 'UpdatedBy', width: InWH.fixWidth(0.06) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: InWH.fixWidth(0.1)
                     }
                ]],
                toolbar: [
                {
                    text: '移交员 <input type="text" id="txtTransferEmp"/>'
                }
                , '-',
                //{
                //    text: '交货单位 <select tabindex="-1" name="selDelivery" id="selDelivery"><option value="0">请选择</option> </select>'
                //}, '-',
                {
                    text: '产品条码 <input type="text" id="txtProductCode"/>'
                },
                '-', {
                    id: 'btnInWH',
                    text: '入库',
                    iconCls: 'icon-add',
                    handler: function () {
                        InWH.transferEmp = $("#txtTransferEmp").val().trim();
                        //InWH.delivery = $("#selDelivery").val().trim();
                        InWH.productCode = $("#txtProductCode").val().trim().toUpperCase();
                        if (InWH.transferEmp == null || InWH.transferEmp == "") {
                            //alert("移交员不能为空", "提示");
                            JeffComm.alertErr("移交员不能为空", 5000);
                           // JeffComm.errorAlert("移交员不能为空", "divMsg");
                            $("#txtTransferEmp").focus();
                            return;
                        }
                        //else if (InWH.delivery == null || InWH.delivery == "") {
                        //   // JeffComm.errorAlert("请选择交货单位", "divMsg");
                        //    JeffComm.alertErr("请选择交货单位", 5000);
                        //    //alert("请选择交货单位", "提示");
                        //    $("#selDelivery").focus();
                        //    return;
                        //}
                        else if (InWH.productCode == null || InWH.productCode == "") {
                            //JeffComm.errorAlert("产品条码不能为空", "divMsg");
                            JeffComm.alertErr("产品条码不能为空", 5000);
                            // alert("产品条码不能为空", "提示");
                            $("#txtProductCode").focus();
                            return;
                        }
                        else {
                            $("#divMsg").hide();
                            //保存数据
                            WsWareHouse.SaveProductInWH(InWH.transferEmp,
                                "", InWH.productCode,
                                function (result) {
                                    if (result == "OK") {
                                        JeffComm.succAlert("成功入库，请继续扫描", "divMsg");
                                        //alert("成功入库，请继续扫描", "提示");//"Deleted successfully.",
                                        $("#grid").datagrid("reload");
                                        $('#txtProductCode').val("");

                                        InWH.transferEmp = "";
                                        //InWH.delivery = "";
                                        InWH.productCode = "";

                                        $("#txtProductCode").focus();
                                    }
                                    else if (result == "errorNull") {
                                        JeffComm.errorAlert("请填写必须项", "divMsg");
                                        //alert("请填写必须项", "提示");
                                    }
                                    else if (result == "errorPSN") {
                                        $('#txtProductCode').val("");
                                        InWH.productCode = "";
                                        ("#txtProductCode").focus();
                                        //JeffComm.errorAlert("该产品为待处理品，不允许入库", "divMsg");
                                        JeffComm.alertErr("该产品为待处理品，不允许入库", 5000);
                                        // alert("该产品为待处理品，不允许入库", "提示");
                                    }
                                    else if (result == "errorStation") {
                                        $('#txtTransferEmp').val("");
                                        // $('#selDelivery').val("");
                                        // $('#txtProductCode').val("");

                                        InWH.transferEmp = "";
                                        //InWH.delivery = "";
                                        InWH.productCode = "";

                                        $("#txtTransferEmp").focus();
                                        //JeffComm.errorAlert("该产品不是待入库产品，请检查", "divMsg");
                                        JeffComm.alertErr("该产品不是待入库产品，请检查", 5000);
                                        //alert("该产品不是待入库产品，请检查", "提示");
                                    }
                                    else {
                                        //JeffComm.errorAlert("入库失败，请检查扫描数据是否正确", "divMsg");
                                        //alert("入库失败，请检查扫描数据是否正确", "提示");
                                        //JeffComm.errorAlert(result, "divMsg");
                                        JeffComm.alertErr(result, 5000);
                                    }
                                }, function (err) {
                                   // JeffComm.errorAlert(err.get_message(), "divMsg");
                                    JeffComm.alertErr(err.get_message(), 5000);
                                });

                        }
                    }
                }, '-', {
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid").datagrid("reload");
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "WHTable.aspx";
                    }
                }],


            });
            //移交员回车事件
            $('#txtTransferEmp').keydown(function (e) {
                if (e.keyCode == 13) {
                    InWH.transferEmp = $("#txtTransferEmp").val().trim();
                    if (InWH.transferEmp == null || InWH.transferEmp == "") {
                        JeffComm.errorAlert("移交员不能为空", "divMsg");
                        //alert("移交员不能为空", "提示");
                        $("#txtTransferEmp").focus();
                        return;
                    }
                    $("#txtProductCode").focus();
                }
            });
            //交货单位回车事件
            //$('#selDelivery').keydown(function (e) {
            //    if (e.keyCode == 13) {
            //        InWH.delivery = $("#selDelivery").val().trim();
            //        if (InWH.delivery == null || InWH.delivery == "") {
            //            JeffComm.errorAlert("请选择交货单位", "divMsg");
            //            // alert("请选择交货单位", "提示");
            //            $("#selDelivery").focus();
            //            return;
            //        }
            //        $("#txtProductCode").focus();
            //    }
            //});
            //产品条码回车事件
            $('#txtProductCode').keydown(function (e) {
                if (e.keyCode == 13) {
                    InWH.transferEmp = $("#txtTransferEmp").val().trim();
                   // InWH.delivery = $("#selDelivery").val().trim();
                    InWH.productCode = $("#txtProductCode").val().trim();
                    if (InWH.transferEmp == null || InWH.transferEmp == "") {
                        JeffComm.errorAlert("移交员不能为空", "divMsg");
                        //   alert("移交员不能为空", "提示");
                        $("#txtTransferEmp").focus();
                        return;
                    }
                    //else if (InWH.delivery == null || InWH.delivery == "") {
                    //    JeffComm.errorAlert("请选择交货单位", "divMsg");
                    //    //  alert("请选择交货单位", "提示");
                    //    $("#selDelivery").focus();
                    //    return;
                    //}
                    else if (InWH.productCode == null || InWH.productCode == "") {
                        JeffComm.errorAlert("产品条码不能为空", "divMsg");
                        // alert("产品条码不能为空", "提示");
                        $("#txtProductCode").focus();
                        return;
                    }
                    else {
                        $("#divMsg").hide();
                        //保存数据
                        WsWareHouse.SaveProductInWH(InWH.transferEmp,
                            "", InWH.productCode,
                            function (result) {
                                if (result == "OK") {
                                    JeffComm.succAlert("成功入库，请继续扫描", "divMsg");
                                    // alert("成功入库，请继续扫描", "提示");//"Deleted successfully.",
                                    $("#grid").datagrid("reload");
                                    $('#txtProductCode').val("");

                                    InWH.transferEmp = "";
                                    //InWH.delivery = "";
                                    InWH.productCode = "";

                                    $("#txtProductCode").focus();
                                }
                                else if (result == "errorNull") {
                                    JeffComm.errorAlert("请填写必须项", "divMsg");
                                    //  alert("请填写必须项", "提示");
                                }
                                else if (result == "errorPSN") {
                                    $('#txtProductCode').val("");
                                    InWH.productCode = "";
                                    ("#txtProductCode").focus();
                                    JeffComm.errorAlert("该产品为待处理品，不允许入库", "divMsg");
                                    // alert("该产品为待处理品，不允许入库", "提示");
                                }
                                else if (result == "errorStation") {
                                    $('#txtTransferEmp').val("");
                                    // $('#selDelivery').val("");
                                    // $('#txtProductCode').val("");

                                    InWH.transferEmp = "";
                                    //InWH.delivery = "";
                                    InWH.productCode = "";

                                    $("#txtTransferEmp").focus();
                                    JeffComm.errorAlert("该产品不是待入库产品，请检查", "divMsg");
                                    // alert("该产品不是待入库产品，请检查", "提示");
                                }
                                else {
                                    //JeffComm.errorAlert("入库失败，请检查扫描数据是否正确", "divMsg");
                                    // alert("入库失败，请检查扫描数据是否正确", "提示");
                                    JeffComm.errorAlert(result, "divMsg");
                                }
                            });
                    }
                }
            }); 
        } 
    };

}();