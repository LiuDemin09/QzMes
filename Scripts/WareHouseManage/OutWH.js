var OutWH = function () {

    return {
        productCode: null,
        init: function () {
            $("#divMsg").hide();
            var orderNo = getParam('orderNo');
            var partNo = getParam('partNo');
            if(orderNo!=null&&orderNo!=""&&partNo!=null&&partNo!=""){
            WsWareHouse.FindWOByOrderAndPart(orderNo,partNo,function (result) {
                
                $('#grid1').datagrid('load', { workOrder: result });
                });
            }

            function getParam(paramName) {
                paramValue = "";
                isFound = false;
                if (this.location.search.indexOf("?") == 0 && this.location.search.indexOf("=") > 1) {
                    arrSource = unescape(this.location.search).substring(1, this.location.search.length).split("&");
                    i = 0;
                    while (i < arrSource.length && !isFound) {
                        if (arrSource[i].indexOf("=") > 0) {
                            if (arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase()) {
                                paramValue = arrSource[i].split("=")[1];
                                isFound = true;
                            }
                        }
                        i++;
                    }
                }
                return paramValue;
            }
        },


        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '待出库信息',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindStandByOutWH',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                // pagination: true,
                rownumbers: true,
                //pageSize: 10,
                //pageNumber: 1,
                //pageList: [10, 20, 30, 40, 50],
                //beforePageText: '第',//页数文本框前显示的汉字   
                //afterPageText: '页    共 {pages} 页',
                //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '订单号码', field: 'ORDER_NO', width: OutWH.fixWidth(0.1) },
                     { title: '工单单号', field: 'WO', width: OutWH.fixWidth(0.1) },
                     { title: '零件图号', field: 'PARTSDRAWING_CODE', width: OutWH.fixWidth(0.1) },
                     { title: '工单状态', field: 'STATUS', width: OutWH.fixWidth(0.05) },
                     { title: '产品名称', field: 'PRODUCT_NAME', width: OutWH.fixWidth(0.08) },
                     //{
                     //    title: '开始时间', field: 'CREATED_DATE', formatter: function (value, row, index) {
                     //        var unixTimestamp = new Date(value);
                     //        return unixTimestamp.toLocaleString();
                     //    }, width: OutWH.fixWidth(0.1)
                     //},
                     //{
                     //    title: '结束时间', field: 'UPDATED_DATE', formatter: function (value, row, index) {
                     //        var unixTimestamp = new Date(value);
                     //        return unixTimestamp.toLocaleString();
                     //    }, width: OutWH.fixWidth(0.1)
                     //},
                     { title: '批次', field: 'BATCH_NUMBER', width: OutWH.fixWidth(0.08) },
                     { title: '工单数量', field: 'QUANTITY', width: OutWH.fixWidth(0.05) },
                     { title: '质量编号', field: 'QUALITY_CODE', width: OutWH.fixWidth(0.05) },
                     //{
                     //    title: '检验时间', field: 'CHECK_TIME',  formatter: function (value, row, index) {
                     //        var unixTimestamp = new Date(value);
                     //        return unixTimestamp.toLocaleString();
                     //    }, width: OutWH.fixWidth(0.1)
                     //},
                     {
                         title: '入库时间', field: 'IN_TIME',formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: OutWH.fixWidth(0.1)
                     }
                ]],
                toolbar: [{
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
                onClickRow: function (rowIndex, rowData) {
                    $('#grid1').datagrid('load', { workOrder: rowData["WO"] });
                }

            });

            $('#grid1').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '待出库详细信息',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindStandByDetail',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                autoRowHeight: false,
                singleSelect: true,
                //  pagination: true,
                rownumbers: true,
                //pageSize: 10,
                //pageNumber: 1,
                //pageList: [10, 20, 30, 40, 50],
                //beforePageText: '第',//页数文本框前显示的汉字   
                //afterPageText: '页    共 {pages} 页',
                //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '产品条码', field: 'PSN', width: OutWH.fixWidth(0.12) },
                     { title: '工单号码', field: 'WorkOrder', width: OutWH.fixWidth(0.12) },
                     { title: '交货单位', field: 'MANUFACTURE', width: OutWH.fixWidth(0.08) },
                     { title: '收货仓库', field: 'StockHouse', width: OutWH.fixWidth(0.08) },
                     { title: '物料料号', field: 'ProductCode', width: OutWH.fixWidth(0.1) },
                     { title: '物料名称', field: 'ProductName', width: OutWH.fixWidth(0.1) },
                     { title: '批号', field: 'BatchNumber', width: OutWH.fixWidth(0.1) }
                ]],
                toolbar: [{
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid1").datagrid("reload");
                    }
                }],
                onClickRow: function (rowIndex, rowData) {

                }

            });

            $('#grid2').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '出库记录',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindProductOutWH',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                autoRowHeight: false,
                singleSelect: true,
                //  pagination: true,
                rownumbers: true,
                //pageSize: 10,
                //pageNumber: 1,
                //pageList: [10, 20, 30, 40, 50],
                //beforePageText: '第',//页数文本框前显示的汉字   
                //afterPageText: '页    共 {pages} 页',
                //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '产品条码', field: 'PSN', width: OutWH.fixWidth(0.11) },
                     { title: '工单号码', field: 'WorkOrder', width: OutWH.fixWidth(0.11) },
                     { title: '单据编号', field: 'DOCUMENTID', width: OutWH.fixWidth(0.1) },
                     { title: '购货单位', field: 'MANUFACTURE', width: OutWH.fixWidth(0.07) },
                     { title: '发货仓库', field: 'StockHouse', width: OutWH.fixWidth(0.07) },
                    // { title: '产品料号', field: 'ProductCode', width: OutWH.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: OutWH.fixWidth(0.07) },
                     { title: '单位', field: 'UNIT', width: OutWH.fixWidth(0.05) },
                     { title: '批号', field: 'BatchNumber', width: OutWH.fixWidth(0.1) },
                     { title: '数量', field: 'QUANTITY', width: OutWH.fixWidth(0.05) },
                     {
                         title: '出库日期', field: 'UpdatedDate',  formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: OutWH.fixWidth(0.1)
                     }
                ]],
                toolbar: [{
                    text: '产品条码 <input type="text" id="txtProductCode"/>'
                }, '-', {
                    id: 'btnOutWH',
                    text: '出库',
                    iconCls: 'icon-remove',
                    handler: function () {
                        OutWH.productCode = $("#txtProductCode").val().trim().toUpperCase();
                        if (OutWH.productCode == null || OutWH.productCode == "") {
                            JeffComm.errorAlert("产品条码不能为空", "divMsg");
                           // alert("产品条码不能为空", "提示");
                            $("#txtProductCode").focus();
                            return;
                        }
                        else {
                            //保存数据
                            WsWareHouse.SaveProductOutWH(OutWH.productCode,
                                function (result) {
                                    if (result == "OK") {
                                        JeffComm.succAlert("成功出库，请继续扫描", "divMsg");
                                       // alert("成功出库，请继续扫描", "提示");//"Deleted successfully.",

                                        $("#grid").datagrid("reload");
                                        $("#grid1").datagrid("reload");
                                        $("#grid2").datagrid("reload");
                                        $('#txtProductCode').val("");
                                        OutWH.productCode = "";
                                        $("#txtProductCode").focus();
                                    } else {
                                        JeffComm.errorAlert(result, "divMsg");
                                       // alert("出库失败，请检查条码是否正确", "提示");

                                    }
                                });

                        }
                    }
                }, '-', {
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid2").datagrid("reload");
                    }
                }],
                onClickRow: function (rowIndex, rowData) {

                }

            });

            $('#txtProductCode').keydown(function (e) {
                if (e.keyCode == 13) {

                    OutWH.productCode = $("#txtProductCode").val().trim();
                    if (OutWH.productCode == null || OutWH.productCode == "") {
                        JeffComm.errorAlert("产品条码不能为空", "divMsg");
                       // alert("产品条码不能为空", "提示");
                        $("#txtProductCode").focus();
                        return;
                    }
                    else {
                        //保存数据
                        WsWareHouse.SaveProductOutWH(OutWH.productCode,
                            function (result) {
                                if (result == "OK") {
                                    JeffComm.succAlert("成功出库，请继续扫描", "divMsg");
                                   // alert("成功出库，请继续扫描", "提示");//"Deleted successfully.",

                                    $("#grid").datagrid("reload");
                                    $("#grid1").datagrid("reload");
                                    $("#grid2").datagrid("reload");
                                    $('#txtProductCode').val("");
                                    OutWH.productCode = "";
                                    $("#txtProductCode").focus();
                                } else {
                                    JeffComm.errorAlert("出库失败，请检查条码是否正确", "divMsg");
                                    //alert("出库失败，请检查条码是否正确", "提示");

                                }
                            });

                    }
                }
            });



        }

    };

}();