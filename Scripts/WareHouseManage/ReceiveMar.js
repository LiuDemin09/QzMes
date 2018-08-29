var ReceiveMar = function () {

    return {
        grid: null,
        custName: null,
        stockHouse: null,
        documentId: null,
        MaterialCode: null,
        MaterialName: null,
        batchNumber: null,
        unit: null,
        qty: null,
        selMaterialCode: null,
        delMSN: null,

        OrderNoAdd: null,
        init: function () {
            //初始化购货单位
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
            });
            //初始化收料仓库
            WsSystem.ListBindReceiveHouse(function (result) {
                JeffComm.fillSelect($("#selRecStock"), result, true);
            });
            //初始化单位
            WsSystem.ListBindUnit(function (result) {
                JeffComm.fillSelect($("#txtUnit"), result, true);
                $("#txtUnit").val("QZB1704000601");
            });
            //得到单据编号
            WsSystem.GetInlistNO(function (result) {
                $('#txtDocumentId').val(result);

            });
            //加载物料编码
            $('#txtMarCode').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListMaterialCodeEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                //Sorted:true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                },
                //onSelect: function (r) {
                //    WsSystem.GetCMaterialByQMaterialCode(r.id, function (result) {
                //        $('#txtMarName').val(result);
                //    }); 
                //},
            });
            jQuery('#btnNewMar').click(function () {
                window.location.href = "../BaseInfo/MaterielMgr.aspx";

            });
            jQuery('#btnClose').click(function () {
                $('#restartDialog').window('close');

            });
            //jQuery('#btnAddOrder').click(function () {
            //    $('#restartDialog').window('open');

            //});
            jQuery('#btnNew').click(function () {
                //SysRole.roleId = null;
                //InWareHouse.msn = null;
                WsSystem.GetInlistNO(function (result) {
                    $('#txtDocumentId').val(result);
                    $("#selCustName").val("");
                    $("#selRecStock").val("");
                    //$("#txtMarCode").val("");
                    $('#txtMarCode').combobox({
                        prompt: '输入首关键字自动检索',
                        required: false,
                        url: '../../services/WsSystem.asmx/ListMaterialCodeEasyUI',
                        valueField: 'id',
                        textField: 'text',
                        editable: true,
                        //Sorted:true,
                        hasDownArrow: true,
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) == 0;
                        },
                        //onSelect: function (r) {
                        //    WsSystem.GetCMaterialByQMaterialCode(r.id, function (result) {
                        //        $('#txtMarName').val(result);
                        //    }); 
                        //},
                    });
                    $("#txtBatchNum").val("");
                    $("#txtUnit").val("QZB1704000601");
                    $("#txtQuantity").val("");
                });
            });

            jQuery('#btnSave').click(function () {
                //var vvv = $("#txtMarCode").combobox("getValue");
                if ($("#selCustName").val() == null || $("#selCustName").val() == "") {
                    JeffComm.alertErr("购货单位不能为空");
                   // alert("购货单位不能为空");
                }
                else if ($("#selRecStock").val() == null || $("#selRecStock").val() == "") {
                    JeffComm.alertErr("收料仓库不能为空");
                    //alert("收料仓库不能为空");
                }
                else if ($("#txtDocumentId").val() == null || $("#txtDocumentId").val() == "") {
                    JeffComm.alertErr("单据编号不能为空");
                    //alert("单据编号不能为空");
                }
                else if ($("#txtMarCode").combobox("getValue") == null || $("#txtMarCode").combobox("getValue") == "") {
                    JeffComm.alertErr("物料代码不能为空");
                    //alert("物料代码不能为空");
                }
               
                else if ($("#txtBatchNum").val() == null || $("#txtBatchNum").val() == "") {
                    JeffComm.alertErr("批号不能为空");
                    //alert("批号不能为空");
                }
                else if ($("#txtUnit").val() == null || $("#txtUnit").val() == "") {
                    JeffComm.alertErr("单位不能为空");
                   // alert("单位不能为空");
                }
                else if ($("#txtQuantity").val() == null || $("#txtQuantity").val() == "") {
                    JeffComm.alertErr("数量不能为空");
                    //alert("数量不能为空");

                } else { 
                    var inmaterialinfo = {
                        CustName: $("#selCustName").val(),
                        StockHouse: $("#selRecStock").val(),
                        DocumentID: $("#txtDocumentId").val(),
                        MaterialCode: $("#txtMarCode").combobox("getValue").trim(),
                        MaterialName: $("#txtMarCode").combobox("getText").trim(),
                        BatchNumber: $("#txtBatchNum").val().trim(),
                        Unit: $("#txtUnit").val(),
                        BasQty: $("#txtQuantity").val(),
                        Description: $("#txtMemo").val(),
                    };
                    WsSystem.SaveInWareHouseInfo(inmaterialinfo,
                                function (result) {
                                    if (result == "ERR") {
                                        JeffComm.alertErr("物料名称有误");
                                       // alert("物料名称有误");
                                    } else {
                                        JeffComm.alertSucc("保存成功", 500);
                                       // alert("Saved successfully.", "提示");
                                        //$("#selCustName").val("");
                                        //$("#selRecStock").val("");
                                        // $("#txtMarCode").val("");
                                        WsSystem.GetInlistNO(function (result) {
                                            $('#txtDocumentId').val(result);

                                        });
                                        //$("#txtBatchNum").val("");
                                        $("#txtUnit").val("QZB1704000601");
                                        //$("#txtUnit").val("");
                                       // $("#txtQuantity").val("");
                                        ReceiveMar.selMaterialCode = $("#selMaterialCode").val();

                                        $('#grid').datagrid('load', { materialCode: ReceiveMar.selMaterialCode });
                                        return;
                                        //开始打印
                                        var vres = new Array();
                                        vres = result.split(",");
                                        for (i = 0; i < vres.length; i++) {
                                            //打印條碼  
                                            var filePath = JeffComm.GetLocalPath() + "msn.lab";
                                            var qty = 1;
                                            var ID = '1';
                                            if (!JeffComm.CommonLabelPrint(filePath, vres[i], ID, qty)) {
                                                //JeffComm.errorAlert("打印失敗", "divMsg");
                                                alert(StringRes.PrintFail, "提示");
                                                var htmtxt = "<tr><td >#</td>";
                                                htmtxt += "<td>" + vres[i] + "</td>";
                                                var htmlRes = "";
                                                htmlRes = '<span class="label label-warning">FAIL</span>';
                                                htmtxt += "<td>" + htmlRes + "</td>";
                                                htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
                                                $("#tblHistory tr:first").after(htmtxt);
                                                return false;
                                            }
                                        }
                                    }
                                }, function (err) {
                                    alert(err.get_message(), "提示");

                                });
                }
            });
            jQuery('#btnClear').click(function () {
                $("#selCustName").val("");
                $("#selRecStock").val("");
                // $("#txtMarCode").val("");
                $('#txtMarCode').combobox({
                    prompt: '输入首关键字自动检索',
                    required: false,
                    url: '../../services/WsSystem.asmx/ListMaterialCodeEasyUI',
                    valueField: 'id',
                    textField: 'text',
                    editable: true,
                    //Sorted:true,
                    hasDownArrow: true,
                    filter: function (q, row) {
                        var opts = $(this).combobox('options');
                        return row[opts.textField].indexOf(q) == 0;
                    },
                    //onSelect: function (r) {
                    //    WsSystem.GetCMaterialByQMaterialCode(r.id, function (result) {
                    //        $('#txtMarName').val(result);
                    //    }); 
                    //},
                });
                $("#txtBatchNum").val("");
                $("#txtUnit").val("QZB1704000601");
                //$("#txtUnit").val("");
                $("#txtQuantity").val("");
            });



        },
        bindGrid: function () {
            ReceiveMar.selMaterialCode = $("#selMaterialCode").val();

            $('#grid').datagrid('load', { materialCode: ReceiveMar.selMaterialCode });

        },
        delReceiveMar: function () {
            if (ReceiveMar.delMSN == null) {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemovInWareHouseInfo(ReceiveMar.delMSN, function (result) {
                    alert(StringRes.M0003, "提示");//"Deleted successfully.",
                    ReceiveMar.delMSN = null;
                    ReceiveMar.selMaterialCode = $("#selMaterialCode").val();
                    $('#grid').datagrid('load', { materialCode: ReceiveMar.selMaterialCode });
                }, function (err) {
                    alert(err.get_message(), "提示");
                });
            }

        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindReceiveMar',
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
                     { title: '物料条码', field: 'MSN', width: ReceiveMar.fixWidth(0.12) },
                     { title: '物料编码', field: 'MaterialCode', width: ReceiveMar.fixWidth(0.1) },
                     { title: '物料名称', field: 'MaterialName', width: ReceiveMar.fixWidth(0.1) },
                     { title: '批号', field: 'BatchNumber', width: ReceiveMar.fixWidth(0.1) },
                     { title: '购货单位', field: 'CustName', width: ReceiveMar.fixWidth(0.1) },
                     { title: '单据编号', field: 'DOCUMENTID', width: ReceiveMar.fixWidth(0.1) },
                     { title: '数量', field: 'BasQty', width: ReceiveMar.fixWidth(0.06) }
                ]],
                toolbar: [{
                    id: 'btnAdd',
                    text: '收料',
                    iconCls: 'icon-add',
                    handler: function () {
                        $("#restartDialog").dialog('open');
                        //实现添加记录的页面
                        $('#txtMarCode').combobox({
                            prompt: '输入首关键字自动检索',
                            required: false,
                            url: '../../services/WsSystem.asmx/ListMaterialCodeEasyUI',
                            valueField: 'id',
                            textField: 'text',
                            editable: true,
                            //Sorted:true,
                            hasDownArrow: true,
                            filter: function (q, row) {
                                var opts = $(this).combobox('options');
                                return row[opts.textField].indexOf(q) == 0;
                            },
                            //onSelect: function (r) {
                            //    WsSystem.GetCMaterialByQMaterialCode(r.id, function (result) {
                            //        $('#txtMarName').val(result);
                            //    }); 
                            //},
                        });
                    }
                },
                '-', {
                    text: '物料料号 <input type="text" id="selMaterialCode"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        ReceiveMar.bindGrid();
                    }
                }, '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        ReceiveMar.delReceiveMar();//实现直接删除数据的方法
                    }
                },
                '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "WHTable.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    ReceiveMar.delMSN = rowData["MSN"];
                    // ShowEditOrViewDialog();
                } 
            }); 
        } 
    }; 
}();