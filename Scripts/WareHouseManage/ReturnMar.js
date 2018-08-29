var ReturnMar = function () {

    return {

        init: function () {

            WsSystem.ListBindWorkOrder("1", function (result) {
                JeffComm.fillSelect($("#selWorkOrder"), result, true);
            });

            $('#txtReMSN').keypress(function (e) {
                if (e.which == 13) {
                    if ($("#txtReMSN").val() == null || $("#txtReMSN").val().trim() == "") {
                        JeffComm.alertErr("请扫入条码", 500);
                        //alert("请扫入条码");
                    } else {
                        WsSystem.SaveReturnMaterialInfo($('#txtReMSN').val().toUpperCase().trim(),
                            function (result) {
                                JeffComm.alertSucc("退料成功", 500);
                               // alert("退料成功", "提示");
                                $('#txtMSN').val("");
                                $("#grid").datagrid("reload");
                                $("#grid1").datagrid("reload");
                            }, function (err) {
                                alert(err.get_message(), "提示");
                            });

                        return false;
                    }
                }
            });

            $("#selWorkOrder").change(function () {
                $('#grid').datagrid('load', { workOrder: $('#selWorkOrder').val() });
                $('#grid1').datagrid('load', { workOrder: $('#selWorkOrder').val() });

            });

            $('#txtMSN').keypress(function (e) {
                if (e.which == 13) {
                    if ($("#txtMSN").val() == null || $("#txtMSN").val().trim() == "" ||
                        $("#txtEmp").val() == null || $("#txtEmp").val().trim() == "" ||
                        $("#selWorkOrder").val() == null || $("#selWorkOrder").val().trim() == "") {
                        JeffComm.alertErr("信息填写不完全，请检查");
                       // alert("信息填写不完全，请检查");
                    } else {
                        var outmaterialinfo = {
                            MSN: $("#txtMSN").val(),
                            MaterialHandler: $("#txtEmp").val(),
                            WorkOrder: $("#selWorkOrder").val(),
                        };
                        WsSystem.SaveReSendMaterialInfo(outmaterialinfo,
                            function (result) {
                                JeffComm.alertSucc("补料成功", 500);
                                //alert("补料成功", "提示");
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
                            }, function (err) {
                                alert(err.get_message(), "提示");
                            });

                        return false;
                    }
                }
            });

            jQuery('#btnReSend').click(function () {
                
                    if ($("#txtMSN").val() == null || $("#txtMSN").val().trim() == "" ||
                        $("#txtEmp").val() == null || $("#txtEmp").val().trim() == "" ||
                        $("#selWorkOrder").val() == null || $("#selWorkOrder").val().trim() == "") {
                        JeffComm.alertErr("信息填写不完全，请检查");
                       // alert("信息填写不完全，请检查");
                    } else {
                        var outmaterialinfo = {
                            MSN: $("#txtMSN").val(),
                            MaterialHandler: $("#txtEmp").val(),
                            WorkOrder: $("#selWorkOrder").val(),
                        };
                        WsSystem.SaveReSendMaterialInfo(outmaterialinfo,
                            function (result) {
                                JeffComm.alertSucc("补料成功", 500);
                                //alert("补料成功", "提示");
                                $('#txtMSN').val("");
                                $("#grid").datagrid("reload");
                                $("#grid1").datagrid("reload");
                            }, function (err) {
                                alert(err.get_message(), "提示");
                            });
                    }
                
            });

            jQuery('#btnReturn').click(function () {

                if ($("#txtReMSN").val() == null || $("#txtReMSN").val().trim() == "") {
                    JeffComm.alertErr("请扫入条码", 500);
                   // alert("请扫入条码");
                } else {
                    WsSystem.SaveReturnMaterialInfo($('#txtReMSN').val(),
                        function (result) {
                            JeffComm.alertSucc("退料成功", 500);
                            //alert("退料成功", "提示");
                            $('#txtMSN').val("");
                            $("#grid").datagrid("reload");
                            $("#grid1").datagrid("reload");
                        }, function (err) {
                            alert(err.get_message(), "提示");
                        });
                }
            });

        },


        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '备料信息',
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
                pageSize: 5,
                pageNumber: 1,
                pageList: [5, 10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                      { title: '物料条码', field: 'MSN', width: ReturnMar.fixWidth(0.11) },
                     { title: '物料编码', field: 'MaterialCode', width: ReturnMar.fixWidth(0.08) },
                     { title: '物料名称', field: 'MaterialName', width: ReturnMar.fixWidth(0.08) },
                     { title: '批号', field: 'BatchNumber', width: ReturnMar.fixWidth(0.08) },
                     { title: '购货单位', field: 'CustName', width: ReturnMar.fixWidth(0.08) },
                     { title: '发货仓库', field: 'StockHouse', width: ReturnMar.fixWidth(0.08) },
                     { title: '单据编号', field: 'DOCUMENTID', width: ReturnMar.fixWidth(0.08) },
                     { title: '数量', field: 'BasQty', width: ReturnMar.fixWidth(0.06) },
                     { title: '单位', field: 'UNIT', width: ReturnMar.fixWidth(0.08) }
                ]],
                toolbar: [
                //    {
                //    text: '工单号 <select tabindex="-1" name="selWorkOrder" id="selWorkOrder"><option value="0">请选择</option> </select>'
                //}, '-',
                //{
                //    text: '领料员 <input type="text" id="txtEmp"/>'
                //}, '-',
                //{
                //    text: '条码 <input type="text" id="txtMSN"/>'
                //},
                //'-', {
                //    id: 'btnReSend',
                //    text: '补料',
                //    iconCls: 'icon-add',
                //    handler: function () {
                //        //实现补料
                //        var outmaterialinfo = {
                //            MSN: $("#txtMSN").val(),
                //            MaterialHandler: $("#txtEmp").val(),
                //            WorkOrder: $("#selWorkOrder").val(),
                //        };
                //        WsSystem.SaveReSendMaterialInfo(outmaterialinfo,
                //            function (result) {
                //                alert("补料成功", "提示");
                //                $('#txtMSN').val("");
                //                $("#grid").datagrid("reload");
                //                $("#grid1").datagrid("reload");
                //            }, function (err) {
                //                alert(err.get_message(), "提示");
                //            });

                //    }
                //},
                //'-',
                {
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
                    $("#txtMSN").val(rowData["MSN"]);
                },

            });


            $('#grid1').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '补退料记录',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindMarInOut',
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
                     { title: '来料条码', field: 'MSN', width: ReturnMar.fixWidth(0.11) },
                     { title: '工单号码', field: 'WorkOrder', width: ReturnMar.fixWidth(0.11) },
                     { title: '物料代码', field: 'MaterialCode', width: ReturnMar.fixWidth(0.08) },
                     { title: '物料名称', field: 'MaterialName', width: ReturnMar.fixWidth(0.08) },
                     { title: '批号', field: 'BatchNumber', width: ReturnMar.fixWidth(0.08) },
                     { title: '数量', field: 'QUANTITY', width: ReturnMar.fixWidth(0.06) },
                     { title: '发货仓库', field: 'StockHouse', width: ReturnMar.fixWidth(0.08) },
                     { title: '源单单号', field: 'DOCUMENTID', width: ReturnMar.fixWidth(0.08) },
                     { title: '领料人', field: 'UpdatedBy', width: ReturnMar.fixWidth(0.08) }
                ]],
                toolbar: [
                //    {
                //    text: '条码 <input type="text" id="txtReMSN"/>'
                //},
                //'-', {
                //    id: 'btnReturn',
                //    text: '退料',
                //    iconCls: 'icon-redo',
                //    handler: function () {
                //        //实现退料
                //        WsSystem.SaveReturnMaterialInfo($('#txtReMSN').val(),
                //        function (result) {
                //            alert("退料成功", "提示");
                //            $('#txtMSN').val("");
                //            $("#grid").datagrid("reload");
                //            $("#grid1").datagrid("reload");
                //        }, function (err) {
                //            alert(err.get_message(), "提示");
                //        });
                //    }
                //}, '-',
                {
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