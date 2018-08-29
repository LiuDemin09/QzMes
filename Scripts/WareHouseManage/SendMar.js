var SendMar = function () {
    return {
        init: function () {
            $("#divMsg").hide();
            WsSystem.ListBindWorkOrder("1", function (result) {
                JeffComm.fillSelect($("#selWorkOrder"), result, true);
            });
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '发料信息',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindPreparedMar',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                      { title: '物料条码', field: 'MSN', width: SendMar.fixWidth(0.11) },
                     { title: '物料编码', field: 'MaterialCode', width: SendMar.fixWidth(0.08) },
                     { title: '物料名称', field: 'MaterialName', width: SendMar.fixWidth(0.08) },
                     { title: '批号', field: 'BatchNumber', width: SendMar.fixWidth(0.08) },
                     { title: '购货单位', field: 'CustName', width: SendMar.fixWidth(0.08) },
                     { title: '发货仓库', field: 'StockHouse', width: SendMar.fixWidth(0.08) },
                     { title: '单据编号', field: 'DOCUMENTID', width: SendMar.fixWidth(0.08) },
                     { title: '数量', field: 'BasQty', width: SendMar.fixWidth(0.06) },
                     { title: '保管员', field: 'UpdatedBy', width: SendMar.fixWidth(0.08) },
                     {
                         title: '日期', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: SendMar.fixWidth(0.1)
                     }
                ]],
                toolbar: [{
                    text: '工单号 <select tabindex="-1" name="selWorkOrder" id="selWorkOrder"><option value="0">请选择</option> </select>'
                }, '-', {
                    text: ' <input type="text" id="txtFaLiaoWorkOrder"/>'
                }, '-',
                {
                    text: '领料员 <input type="text" id="txtEmp"/>'
                }, '-',
                {
                    text: '条码 <input type="text" id="txtMSN"/>'
                }, '-', {
                    id: 'btnend',
                    text: '发料',
                    iconCls: 'icon-undo',
                    handler: function () {
                        if ($("#txtMSN").val() == null || $("#txtMSN").val().trim() == "") {
                            JeffComm.alertErr("物料条码不能为空");
                            //alert("物料条码不能为空");
                            return;
                        }
                        if ($("#txtEmp").val() == null || $("#txtEmp").val().trim() == "") {
                            JeffComm.alertErr("领料人不能为空");
                            // alert("领料人不能为空");
                            return;
                        }
                        if ($("#txtFaLiaoWorkOrder").val() == ""
                            || $("#txtFaLiaoWorkOrder").val() == null) {
                            JeffComm.alertErr("请选择发料工单");
                            //alert("请选择发料工单");
                            return;
                        }
                        WsSystem.QueryExistUserNameByCode($("#txtEmp").val(),
                            function (result) {
                                if (result == "") {
                                    JeffComm.alertErr("无此领料人", "divMsg");
                                    return;
                                }
                            });
                        var outmaterialinfo = {
                            MSN: $("#txtMSN").val().trim(),
                            MaterialHandler: $("#txtEmp").val().trim(),
                            WorkOrder: $("#txtFaLiaoWorkOrder").val().trim(),
                        };
                        //result返回psn
                        WsSystem.SaveOutWareHouseInfo(outmaterialinfo,
                            function (result) {

                                JeffComm.succAlert("发料成功", "divMsg");
                                //alert("发料成功", "提示");
                                var psns = result.split(",");
                                $('#txtMSN').val("");
                                $("#grid").datagrid("reload");
                                $("#grid1").datagrid("reload");
                                //开始打印

                                var filePath = JeffComm.GetLocalPath() + "psn.lab";
                                var qty = psns.length;
                                var ID = '2';
                                for (var i = 0; i < qty; i++) {
                                    // if (!JeffComm.CommonLabelPrint(filePath, outmaterialinfo.MSN, ID, 1)) {
                                    if (!JeffComm.CommonLabelPrint(filePath, psns[i], ID, 1)) {
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
                                //打印结束
                            }, function (err) {
                                JeffComm.errorAlert(err.get_message(), "divMsg");
                                //  alert(err.get_message(), "提示");
                            });
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
                onClickRow: function (rowIndex, rowData) {
                    $('#txtMSN').val(rowData["MSN"]);
                    // ShowEditOrViewDialog();
                },

            });
            $("#selWorkOrder").change(function () {
                $('#txtFaLiaoWorkOrder').val($('#selWorkOrder').val());
                $('#grid').datagrid('load', { workOrder: $('#selWorkOrder').val() });
                $('#grid1').datagrid('load', { workOrder: $('#selWorkOrder').val() });

            });
            $('#txtFaLiaoWorkOrder').keypress(function (e) {
                if (e.which == 13) {
                    $('#grid').datagrid('load', { workOrder: $('#txtFaLiaoWorkOrder').val() });
                    $('#grid1').datagrid('load', { workOrder: $('#txtFaLiaoWorkOrder').val() });
                }
            });
            $('#txtMSN').keypress(function (e) {
                if (e.which == 13) {
                    if ($("#txtMSN").val() == null || $("#txtMSN").val().trim() == "") {
                        JeffComm.alertErr("物料条码不能为空");
                        // alert("物料条码不能为空");
                        return;
                    }
                    if ($("#txtEmp").val() == null || $("#txtEmp").val().trim() == "") {
                        JeffComm.alertErr("领料人不能为空");
                        // alert("领料人不能为空");
                        return;
                    }
                    if ($("#selWorkOrder").val() == "0") {
                        JeffComm.alertErr("请选择发料工单");
                        // alert("请选择发料工单");
                        return;
                    }
                    var outmaterialinfo = {
                        MSN: $("#txtMSN").val().toUpperCase().trim(),
                        MaterialHandler: $("#txtEmp").val(),
                        WorkOrder: $("#selWorkOrder").val(),
                    };
                    WsSystem.SaveOutWareHouseInfo(outmaterialinfo,
                        function (result) {
                            JeffComm.succAlert("发料成功", "divMsg");
                            //alert("发料成功", "提示");
                            $('#txtMSN').val("");
                            $("#grid").datagrid("reload");
                            $("#grid1").datagrid("reload");
                            //开始打印
                            var filePath = JeffComm.GetLocalPath() + "psn.lab";
                            var qty = psns.length;
                            var ID = '2';
                            for (var i = 0; i < qty; i++) {
                                // if (!JeffComm.CommonLabelPrint(filePath, outmaterialinfo.MSN, ID, 1)) {
                                if (!JeffComm.CommonLabelPrint(filePath, psns[i], ID, 1)) {
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
                            //打印结束
                        }, function (err) {
                            JeffComm.errorAlert(err.get_message(), "divMsg");
                            //alert(err.get_message(), "提示");
                        });

                    return false;
                }
            });

            $('#grid1').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '发料记录',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindSendMar',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '来料条码', field: 'MSN', width: SendMar.fixWidth(0.11) },
                     { title: '工单号码', field: 'WorkOrder', width: SendMar.fixWidth(0.11) },
                     { title: '物料代码', field: 'MaterialCode', width: SendMar.fixWidth(0.08) },
                     { title: '物料名称', field: 'MaterialName', width: SendMar.fixWidth(0.08) },
                     { title: '批号', field: 'BatchNumber', width: SendMar.fixWidth(0.08) },
                     { title: '数量', field: 'QUANTITY', width: SendMar.fixWidth(0.06) },
                     { title: '购货单位', field: 'CustName', width: SendMar.fixWidth(0.08) },
                     { title: '发货仓库', field: 'StockHouse', width: SendMar.fixWidth(0.08) },
                     { title: '单据编号', field: 'DOCUMENTID', width: SendMar.fixWidth(0.08) },
                     { title: '领料人', field: 'UpdatedBy', width: SendMar.fixWidth(0.08) }
                ]],
                toolbar: [{
                    id: 'btnReload1',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid1").datagrid("reload");
                    }
                }],


            });



        }

    };

}();