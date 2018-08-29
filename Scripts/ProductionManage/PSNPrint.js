var PSNPrint = function () {
    return {
        grid: null,
        sn: null,
        //main function to initiate the module
        init: function () {
            $('#txtMSN').keypress(function (e) {
                if (e.which == 13) { 
                    PSNPrint.print();
                    return false;
                }
            });
        },
        print: function () {
            if ($("#txtMSN").val() == '') {
                JeffComm.alertErr("来料条码不能为空");
                //alert("来料条码不能为空");
                return;
            }
            if ($("#txtPrintQty").val() == '') {
                JeffComm.alertErr("打印数量不能为空");
               //alert("打印数量不能为空");
            }
            //打印條碼
            var filePath = JeffComm.GetLocalPath() + "psn.lab";
            var qty = $("#txtPrintQty").val();
            var ID = '2';//1:MSN的包  2：PSN的包 在sfc_product_label   bas_label_type_config表中
            var vPSNs = "";
            for (var i = 0; i < qty; i++) {
                //得到PSN条码
                WsSystem.GetPSNByMSN($('#txtMSN').val().toUpperCase().trim(),
                                       function (result) {
                                           //打印PSN条码
                                           vPSNs += "\'"+result+"\'";
                                           if (!JeffComm.CommonLabelPrint(filePath, result, ID, 1)) {
                                               //JeffComm.errorAlert("打印失敗", "divMsg");
                                               JeffComm.errorAlert(StringRes.PrintFail, "divMsg");
                                               //var htmtxt = "<tr><td >#</td>";
                                               //htmtxt += "<td>" + result + "</td>";
                                               //var htmlRes = "";
                                               //htmlRes = '<span class="label label-warning">FAIL</span>';
                                               //htmtxt += "<td>" + htmlRes + "</td>";
                                               //htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
                                               //$("#tblHistory tr:first").after(htmtxt);
                                               return false;
                                           }
                                       });
                       
            }
                   
            //var htmtxt = "<tr><td >#</td>";
            //htmtxt += "<td>" + vPSNs + "</td>";
            //var htmlRes = "";
            //htmlRes = '<span class="label label-warning">OK</span>';
            //htmtxt += "<td>" + htmlRes + "</td>";
            //htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
            //$("#tblHistory tr:first").after(htmtxt);
            //// App.unblockUI($("#divScanArea"));
            $("#txtMSN").val("");
            $("#txtMSN").focus();
            PSNPrint.bindGrid();
        },
        bindGrid: function () {
            PSNPrint.sn = $("#txtPSN").val();
            $('#grid').datagrid('load', { sn: PSNPrint.sn });
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '条码打印',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryPSN',
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
                     { title: '产品条码', field: 'PSN', width: PSNPrint.fixWidth(0.15) },
                     { title: '来料条码', field: 'MSN', width: PSNPrint.fixWidth(0.15) },
                     { title: '工单号码', field: 'WorkOrder', width: PSNPrint.fixWidth(0.15) },
                     { title: '工件图号', field: 'PartsdrawingCode', width: PSNPrint.fixWidth(0.15) },
                     { title: '工件名称', field: 'PartsName', width: PSNPrint.fixWidth(0.15) },
                     { title: '备注', field: 'BatchNumber', width: PSNPrint.fixWidth(0.15) },
                     { title: '数量', field: 'QUANTITY', width: PSNPrint.fixWidth(0.15) },
                ]],
                toolbar: [{
                    text: '来料条码<input type="text" id="txtMSN"/>'
                }, '_', {
                    text: '打印数量<input type="text" id="txtPrintQty"/>'
                }, '-', {
                    id: 'btnPrint',
                    text: '打印',
                    iconCls: 'icon-print',
                    handler: function () {
                        PSNPrint.print();
                    }
                }, '_', {
                    text: '条码<input type="text" id="txtPSN"/>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        PSNPrint.bindGrid();
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
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        }
    };
}();