var OrderTable = function () {

    return {
        grid: null,
        OrderNo: null,
        Partsdrawing: null,
        delOrder: null,
        delContract:null,
        orderID:null,
        OrderNoAdd: null,
        init: function () {
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
                JeffComm.fillSelect($("#selCustName2"), result, true);
            });
            //初始化产品名称
            //WsSystem.ListBindProductName(function (result) {
            //    JeffComm.fillSelect($("#selProductName"), result, true);
            //   // JeffComm.fillSelect($("#selProductName2"), result, true);
            //});
            //WsSystem.ListPartCode(function (result) {
            //    JeffComm.fillSelect($("#txtPartsDrawingAdd"), result, true);
            //});
            $('#selProductName2,#selProductName').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListBindProductNameEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                }
                
            });
            
            $('#txtPartsDrawingAdd').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListPartCodeEasyUI',
                valueField: 'id',
                textField:'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                },
                onChange: function (n,r) {
                    WsSystem.GetcustByPartCode(n, function (result) {
                        $('#selCustName').val(result);
                    });
                    WsSystem.GetProductByPartCode(n, function (result) {
                        //$('#selProductName').val(result);
                        $("#selProductName").combobox("setValue", result);
                    });
                },
            });
            //WsSystem.ListBindPartsDrawingNobyOrder($('#selOrderNo').val(),
            //   function (result) {
            //       JeffComm.fillSelect($("#selPartsDrawing"), result, true);
            //   });
            $("#txtPartsDrawingAdd").change(function () {
                WsSystem.GetcustByPartCode($("#txtPartsDrawingAdd").val(),function (result) {                   
                    $('#selCustName').val(result);
                });
                WsSystem.GetProductByPartCode($("#txtPartsDrawingAdd").val(), function (result) {
                    //$('#selProductName').val(result);
                    $("#selProductName").combobox("setValue", result);
                });
            });
            
            jQuery('#btnClose').click(function () {
                $('#restartDialog').window('close');
                OrderTable.orderID = null;
            });
            jQuery('#btnAddOrder').click(function () {
                OrderTable.OrderNoAdd = null;
                $('#restartDialog').panel({ title: "新建订单" });
                $('#txtOrderNoAdd').attr("disabled", "disabled");
                $('#btnDownload').show();
                $('#btnImport').show();
                $('#btnNew').show();
                $('#btnNewNoNumber').show();
                $('#txtOrderNoAdd').val("");
                $('#selCustName').val("");
                $('#txtContractNo').val("");
                //$('#selProductName').val("");
                $("#selProductName").combobox("setValue", "");
                $('#txtPartsDrawingAdd').val("");
                $('#txtBatchNo').val("");
                $('#txtNumber').val("");
                $('#txtPlanTime').val("");
                $('#txtMemo').val("");
                $('#restartDialog').window('open');
                OrderTable.reloadPartsDrawing();

            });
            jQuery('#btnDownload').click(function () {

                window.open('../../Pages/OrderManage/OrdermrgDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/OrderManage/OrderMrgUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');

            });
            jQuery('#btnNew').click(function () {
                //SysRole.roleId = null;
                OrderTable.orderID=null;
                WsSystem.GetOrderNoCode(function (result) {
                    $('#txtOrderNoAdd').val(result);
                    $('#txtOrderNoAdd').attr("disabled", "disabled");
                    $('#selCustName').val("");
                    $('#txtContractNo').val("");
                    $('#txtMemo').val("");
                    $("#selProductName").combobox("setValue", "");
                    $('#txtPartsDrawingAdd').val("");
                    $('#txtBatchNo').val("");
                    $('#txtNumber').val("");
                    $('#txtPlanTime').val("");
                });
            });
            jQuery('#btnNewNoNumber').click(function () {
                OrderTable.orderNoAdd = null;
                OrderTable.orderID = null;
                $('#txtOrderNoAdd').removeAttr("disabled");                
                $('#txtOrderNoAdd').val("");
                $('#selCustName').val("");
                $('#txtContractNo').val("");
                $('#txtMemo').val("");
                $("#selProductName").combobox("setValue", "");
                $('#txtPartsDrawingAdd').val("");
                $('#txtBatchNo').val("");
                $('#txtNumber').val("");
                $('#txtPlanTime').val("");
            });
            jQuery('#btnDel').click(function () {
                //if (OrderTable.orderNo == "" || OrderTable.orderNo == null) {
                //    JeffComm.errorAlert(StringRes.E0001, "divMsg");//"Pls at lease select one item to delete on the left grid.",
                //    return;
                //}

                //WsSystem.RemoveOrderInfo(CreateOrder.orderNo, function (result) {
                //    JeffComm.succAlert(StringRes.M0003, "divMsg");//"Deleted successfully.",
                //    CreateOrder.orderNo = null;
                //    $('#txtCreateOrderNo').val("");
                //    $('#txtContractNo').val("");
                //    $('#txtPartsDrawing').val("");
                //    $('#txtBatchNo').val("");
                //    $('#txtNumber').val("");
                //    CreateOrder.bindGridData();//更新左边列表
                //}, function (err) {
                //    JeffComm.errorAlert(err.get_message(), "divMsg");
                //});
            });
            jQuery('#btnSave').click(function () {
                if ($("#txtOrderNoAdd").val() == "") {
                    JeffComm.alertErr("订单号不能为空");
                   // alert("订单号不能为空", "提示");
                    return;
                }
                if ($("#selCustName").val() == "") {
                    JeffComm.alertErr("客户不能为空",2000);
                    //alert("客户不能为空", "提示");
                    return;
                }
                if ( $('#selProductName').combobox("getValue") == "" && $("#selCustName").val() != "SF") {
                    JeffComm.alertErr("产品不能为空");
                    //alert("产品不能为空", "提示");
                    return;
                }
                if ($("#txtPartsDrawingAdd").combobox("getValue") == "") {
                    JeffComm.alertErr("图号不能为空");
                    //alert("图号不能为空", "提示");
                    return;
                }
                if ($("#txtNumber").val() == "") {
                    JeffComm.alertErr("订单数量不能为空");
                    //alert("订单数量不能为空", "提示");
                    return;
                }
                if ($("#txtBatchNo").val() == "") {
                    JeffComm.alertErr("批次号不能为空");
                    //alert("订单数量不能为空", "提示");
                    return;
                }
                $('#btnSave').attr("disabled", "disabled");
                var orderinfo = {
                    ID:OrderTable.orderID,
                    OrderNo: $("#txtOrderNoAdd").val(),
                    CustName: $("#selCustName").val(),
                    CONTRACT: $("#txtContractNo").val(),
                    ProductName:$('#selProductName').combobox("getText"),// $("#selProductName").find("option:selected").text(),
                    ProductCode: $('#selProductName').combobox("getValue"),
                    PartsdrawingCode: $("#txtPartsDrawingAdd").combobox("getValue"),
                    BatchNumber: $("#txtBatchNo").val(),
                    OrderQuantity: $("#txtNumber").val(),
                    OutDate: $("#txtPlanTime").val(),
                    MEMO1: $("#txtMemo").val(),
                };
                WsSystem.SaveOrderInfo(orderinfo,
                    function (result) {
                        if (result == "OK" || result == null) {
                            JeffComm.alertSucc("保存成功", 500);
                           // alert("保存成功", "提示");
                            //JeffComm.succAlert("Saved successfully.", "divMsg");
                            $('#btnSave').removeAttr("disabled");
                            WsSystem.GetOrderNoCode(function (result) {
                                $('#txtOrderNoAdd').val(result);
                                $('#txtOrderNoAdd').attr("disabled", "disabled");                                
                            });
                            
                            $('#selCustName').val("");
                            $('#txtContractNo').val("");
                            $("#selProductName").combobox("setValue", "");
                            $("#txtMemo").val("");
                            $('#txtPartsDrawingAdd').val("");
                            $('#txtBatchNo').val("");
                            $('#txtNumber').val("");
                            $('#txtPlanTime').val("");
                            OrderTable.orderID = null;
                            $("#grid").datagrid("reload");
                        } else {
                            alert(result, "提示");
                            $('#btnSave').removeAttr("disabled");
                        }
                    }, function (err) {
                        // JeffComm.errorAlert(err.get_message(), "divMsg");
                        alert("保存失败" + err.get_message(), "提示");
                        $('#btnSave').removeAttr("disabled");
                    });
            });
            jQuery('#btnClear').click(function () {
                OrderTable.orderNo = null;
                $('#txtOrderNoAdd').val("");
                $('#selCustName').val("");
                $('#txtContractNo').val("");
                $("#txtMemo").val("");
                $("#selProductName").combobox("setValue", "");
                $('#txtPartsDrawingAdd').val("");
                $('#txtBatchNo').val("");
                $('#txtNumber').val("");
                $('#txtPlanTime').val("");
            });

            jQuery('#btnClose2').click(function () {
                $('#restartDialog2').window('close');

            });

            jQuery('#btnAddParts').click(function () {
                $('#restartDialog2').window('open');
                OrderTable.reloadProductData();

            });
            jQuery('#btnDownload2').click(function () {

                window.open('../../Pages/OrderManage/PartsDrawingDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport2').click(function () {
                window.open('../../Pages/OrderManage/PartsDrawingUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');

            });
            jQuery('#btnNew2').click(function () {
                $('#txtPartCode2').val("");
                $('#selCustName2').val("");
                $('#selProductName2').val("");
                //$('#txtPlanQuantity2').val("");
                //$('#txtQuantityCode2').val("");
                //$('#txtAskQuantity2').val("");
                //$('#txtBatch2').val("");
                //$('#txtPlanTime2').val("");
            });
            jQuery('#btnClear2').click(function () {
                $('#txtPartCode2').val("");
                $('#selCustName2').val("");
                $('#selProductName2').val("");
                //$('#txtPlanQuantity2').val("");
                //$('#txtQuantityCode2').val("");
                //$('#txtAskQuantity2').val("");
                //$('#txtBatch2').val("");
                //$('#txtPlanTime2').val("");
            });
            jQuery('#btnSave2').click(function () {
                if ($("#txtPartCode2").val() == "") {
                    JeffComm.alertErr("图号不能为空");
                    //alert("图号不能为空", "提示");
                    return;
                }
                if ($("#selCustName2").val() == "") {
                    JeffComm.alertErr("客户名称不能为空");
                    //alert("图号不能为空", "提示");
                    return;
                }
                if ($("#selCustName2").val() == "") {
                    JeffComm.alertErr("产品名称不能为空");
                    //alert("图号不能为空", "提示");
                    return;
                }
                var partsdrawinginfo = {
                    PartsCode: $("#txtPartCode2").val(),
                    CustName: $("#selCustName2").find("option:selected").text(),
                    CustCode: $("#selCustName2").val(),
                    ProductName: $("#selProductName2").find("option:selected").text(),
                    ProductCode: $("#selProductName2").val(),
                    MEMO: $("#txtPartCode2").val() +"|"+ $("#selCustName2").val(),
                    //PlanQuantity: $("#txtPlanQuantity2").val(),//投产数量
                    //QualityCode: $("#txtQuantityCode2").val().trim(),//质量编号
                    //AskQuantity: $("#txtAskQuantity2").val(),//交付数量
                    //BatchNumber: $("#txtBatch2").val().trim(),//炉批号
                    //AskDate: $("#txtPlanTime2").val(),
                };
                WsSystem.SavePartsDrawing(partsdrawinginfo,
                    function (result) {
                        if (result == "OK"||result==null) {
                            JeffComm.alertSucc("保存成功", 500);
                        
                        //JeffComm.succAlert("Saved successfully.", "divMsg");
                        $('#txtPartCode2').val("");
                        $('#selCustName2').val("");
                        $('#selProductName2').val("");
                        //$('#txtPlanQuantity2').val("");
                        //$('#txtQuantityCode2').val("");
                        //$('#txtAskQuantity2').val("");
                        //$('#txtBatch2').val("");
                        //$('#txtPlanTime2').val("");
                            // $("#grid").datagrid("reload");//更新左边列表
                        } else {
                            alert(result, "提示");
                        }
                    }, function (err) {
                        alert(err.get_message(), "提示");
                    });
            });

        },
        reloadPartsDrawing: function () {
            $('#txtPartsDrawingAdd').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListPartCodeEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                },
                onChange: function (n,r) {
                    WsSystem.GetcustByPartCode(n, function (result) {
                        $('#selCustName').val(result);
                    });
                    WsSystem.GetProductByPartCode(n, function (result) {
                        //$('#selProductName').val(result);
                        $("#selProductName").combobox("setValue", result);
                    });
                },
            });
        },
        reloadProductData:function(){
            $('#selProductName2').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListBindProductNameEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                }
                
            });
        },
        bindGrid: function () {
            OrderTable.OrderNo = $("#txtOrderNo").val();
            OrderTable.Partsdrawing = $("#txtPartCode").val();

            $('#grid').datagrid('load', {
                orderNo: OrderTable.OrderNo
                , partsdrawing: OrderTable.Partsdrawing
                , productname: $("#txtProductName").val()
            });

        },
        delOrderInfo: function () {
            //if (OrderTable.delOrder == null || OrderTable.delContract == null || OrderTable.delOrder == "" || OrderTable.delContract == "") {
            if (OrderTable.orderID == null) {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除","提示");
            } else {
                //WsOrder.DelOrderInfo(OrderTable.delOrder, OrderTable.delContract,
                WsOrder.DelOrderInfoByID(OrderTable.orderID,
                    function (result) {
                        if (result == "OK") {
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            OrderTable.OrderNo = $("#txtOrderNo").val();
                            OrderTable.Partsdrawing = $("#txtPartCode").val();
                            $('#grid').datagrid('load', { orderNo: OrderTable.OrderNo, partsdrawing: OrderTable.Partsdrawing });
                        } else {
                            alert(result, "提示");
                            OrderTable.OrderNo = $("#txtOrderNo").val();
                            OrderTable.Partsdrawing = $("#txtPartCode").val();
                            $('#grid').datagrid('load', { orderNo: OrderTable.OrderNo, partsdrawing: OrderTable.Partsdrawing });
                        }
                    }, function (err) {
                        // JeffComm.errorAlert(err.get_message(), "divMsg");
                        alert("保存失败:" + err.get_message(), "提示");

                    });
            }

            $('#grid').datagrid('load', { orderNo: OrderTable.OrderNo, partsdrawing: OrderTable.Partsdrawing });

        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行  
            $('#txtOrderNoAdd').attr("disabled", "disabled");
            $('#btnDownload').hide();
            $('#btnImport').hide();
            $('#btnNew').hide();
            $('#btnNewNoNumber').hide();
            $('#txtOrderNoAdd').val(rows[0].OrderNo);
            $('#selCustName').val(rows[0].CustName);
            $('#txtContractNo').val(rows[0].CONTRACT);
            $('#txtPartsDrawingAdd').val(rows[0].PartsdrawingCode);
            $("#selProductName").combobox("setValue", rows[0].ProductCode);
           // $('#selProductName').val(rows[0].ProductCode);
            $('#txtBatchNo').val(rows[0].BatchNumber);
            $('#txtNumber').val(rows[0].OrderQuantity);
            $('#txtPlanTime').val(rows[0].OutDate);
            $('#restartDialog').panel({ title: "修改订单" });
            $("#restartDialog").dialog('open');

        },
        ShowEditOrViewPublishDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行  
            $('#btnDownload').hide();
            $('#btnImport').hide();
            $('#btnNew').hide();
            $('#btnNewNoNumber').hide();
            $('#txtOrderNoAdd').val(rows[0].OrderNo);
            $('#selCustName').val(rows[0].CustName);
            //$("#selCustName").attr("value", rows[0].CustName);
            //$("#selCustName option[text='" + rows[0].CustName + "']").attr("selected", "selected");
            
            //OrderTable.selectValue("#selCustName", rows[0].CustName);
            $('#txtContractNo').val(rows[0].CONTRACT);
            $("#txtPartsDrawingAdd").combobox("setValue", rows[0].PartsdrawingCode); 
            $("#selProductName").combobox("setText", rows[0].ProductName);
            // $('#selProductName').val(rows[0].ProductCode);
            $('#txtBatchNo').val(rows[0].BatchNumber);
            $('#txtNumber').val(rows[0].OrderQuantity);
            var unixTimestamp = new Date(rows[0].OutDate); 
            $('#txtPlanTime').val(unixTimestamp.toLocaleString());
            $("#selCustName").attr("readOnly", true); 
            $("#txtBatchNo").attr("readOnly", true);
            $("#txtNumber").attr("readOnly", true);
            
            $('#txtOrderNoAdd').attr("disabled", "disabled");
            $('#txtPlanTime').attr("disabled", "disabled");
            $('#txtPartsDrawingAdd').attr("disabled", "disabled");
            $('#restartDialog').panel({ title: "修改订单" });
            $("#restartDialog").dialog('open');

        },
        selectValue:function(sId,value){  
            //var s = document.getElementById(sId);  
            var ops = $("#selCustName").options.ops;
            for(var i=0;i<ops.length; i++){  
                var tempValue = ops[i].value;  
                if(tempValue == value)  
                {  
                    ops[i].selected = true;  
                }  
            }  
        } ,
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsOrder.asmx/FindMyOrder',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                remoteSort:false,
                autoRowHeight: false,
                singleSelect: true,
                   pagination: true,
                rownumbers: true,
                pageSize: 1000,
                  pageNumber: 1,
                  pageList: [1000, 2000, 3000, 4000, 5000],
                  beforePageText: '第',//页数文本框前显示的汉字   
                  afterPageText: '页    共 {pages} 页',
                  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                  columns: [[ //选择
                      { title: 'ID', field: 'ID', width: OrderTable.fixWidth(0.09), hidden: 'true' },
                     { title: '订单单号', field: 'OrderNo', width: OrderTable.fixWidth(0.09),sortable:true },
                     { title: '合同号', field: 'CONTRACT', width: OrderTable.fixWidth(0.09) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: OrderTable.fixWidth(0.09) },
                     { title: '订单状态', field: 'STATUS', width: OrderTable.fixWidth(0.06), sortable: true },
                     { title: '客户名称', field: 'CustName', width: OrderTable.fixWidth(0.06), sortable: true },
                     { title: '产品名称', field: 'ProductName', width: OrderTable.fixWidth(0.06), sortable: true },
                     { title: '产品ID', field: 'ProductCode', width: OrderTable.fixWidth(0.09), hidden: 'true' },
                     { title: '订单数量', field: 'OrderQuantity', width: OrderTable.fixWidth(0.06) },
                     { title: '批次号', field: 'BatchNumber', width: OrderTable.fixWidth(0.06) },
                     {
                         title: '交付日期', field: 'OutDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: OrderTable.fixWidth(0.09), sortable: true
                     },
                     {
                         title: '考核日期', field: 'CheckTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: OrderTable.fixWidth(0.09), sortable: true
                     },
                     { title: '备注', field: 'MEMO1', width: OrderTable.fixWidth(0.06) },
                     { title: '创建人', field: 'CreatedBy', width: OrderTable.fixWidth(0.07) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: OrderTable.fixWidth(0.09), sortable: true
                     }
                ]],
                toolbar: [{
                    text: '订单号码 <input type="text" id="txtOrderNo"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtPartCode"/>'
                }, '-', {
                    text: '产品名称 <input type="text" id="txtProductName"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        OrderTable.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建订单',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建订单" });
                        $('#txtOrderNoAdd').attr("disabled", "disabled");
                        $('#btnDownload').show();
                        $('#btnImport').show();
                        $('#btnNew').show();
                        $('#btnNewNoNumber').show();
                        $('#txtOrderNoAdd').val("");
                        $('#selCustName').val("");
                        $('#txtContractNo').val("");
                        $('#txtMemo').val("");
                        $("#selProductName").combobox("setValue", "");
                        $('#txtPartsDrawingAdd').val("");
                        $('#txtBatchNo').val("");
                        $('#txtNumber').val("");
                        $('#txtPlanTime').val("");

                        $("#selCustName").attr("readOnly", false);
                        $("#txtBatchNo").attr("readOnly", false);
                        $("#txtNumber").attr("readOnly", false);
                        $('#txtOrderNoAdd').removeAttr("disabled");
                        $('#txtPlanTime').removeAttr("disabled");
                        $('#txtPartsDrawingAdd').removeAttr("disabled"); 

                        $("#restartDialog").dialog('open');  //实现添加记录的页面
                        OrderTable.reloadPartsDrawing();
                    }
                },
                '-', {
                    id: 'btnEdit',
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            var status = rows[0].STATUS;
                            if (status == "创建"||status=="驳回")
                                {
                                OrderTable.ShowEditOrViewDialog();//实现修改记录的方法

                            } else {
                                if (status == "发布")
                                {
                                    OrderTable.ShowEditOrViewPublishDialog();
                                }
                                else
                                {
                                    JeffComm.alertErr("只有创建或发布状态的订单才能修改");
                                }
                                
                               // alert("只有创建状态的订单才能修改");
                            }
                           
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行修改");
                           // alert("请选择一条数据进行修改", "提示");
                        }                        
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        OrderTable.delOrderInfo();//实现直接删除数据的方法
                    }
                },
                '-', {
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid").datagrid("reload");
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    OrderTable.delOrder = rowData["OrderNo"];
                    OrderTable.delContract = rowData["CONTRACT"];
                    OrderTable.orderID = rowData["ID"];
                    // ShowEditOrViewDialog();
                }

            });



        }

    };

}();