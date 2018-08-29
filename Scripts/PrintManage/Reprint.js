var Reprint = function () {
    return {
        grid: null,
        sn: null,
        //main function to initiate the module
        init: function () {
            //$('#txtReprintSN').bind('keypress', function (event) {
            //    if (event.keyCode == "13") {
            //        alert('你输入的内容为：' + $('#txtReprintSN').val());
            //    }
            //});
        },
        print:function(){
            if ($("#txtReprintSN").val() == '') {
                JeffComm.alertErr("条码不能为空");
               // alert("条码不能为空");
                return;
            }
            //打印條碼
            var filePath = "";
            var ID = "";
            var qty = 1;
            //来料条码
            if ($("#selLabelType").val() == "1") {
                filePath = JeffComm.GetLocalPath() + "msn.lab";
                ID = "1";
                if( $("#txtReprintSN").val().trim().length==11)
                {
                    WsSystem.FindMSNByDocumentID($("#txtReprintSN").val().trim(), function (res) {
                        if(res!=null)
                        {
                            for(var i=0;i<res.length;i++)
                            {
                                JeffComm.CommonLabelPrint(filePath, res[i], ID, qty);
                            }
                            return;
                        }
                    })
                    return;
                }
            }//产品条码
            else if ($("#selLabelType").val() == "2") {
                filePath = JeffComm.GetLocalPath() + "psn.lab";
                ID = "2";
            }//箱号条码
            else if ($("#selLabelType").val() == "3") {
                filePath = JeffComm.GetLocalPath() + "csn.lab";
                ID = "3";
            }//物料挂签
            else {
                filePath = JeffComm.GetLocalPath() + "msninfo.lab";
                ID="4";
            }
           
            //var ID = '2';
            var v_sn = $("#txtReprintSN").val().toUpperCase().trim();
            if (!JeffComm.CommonLabelPrint(filePath, $("#txtReprintSN").val().toUpperCase().trim(), ID, qty)) {
                //JeffComm.errorAlert("打印失敗", "divMsg");
                JeffComm.errorAlert(StringRes.PrintFail, "divMsg");
                //var htmtxt = "<tr><td >#</td>";
                //htmtxt += "<td>" + $("#txtReprintSN").val() + "</td>";
                //var htmlRes = "";
                //htmlRes = '<span class="label label-warning">FAIL</span>';
                //htmtxt += "<td>" + htmlRes + "</td>";
                //htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
                //$("#tblHistory tr:first").after(htmtxt);
                return false;
            }
            //var htmtxt = "<tr><td >#</td>";
            //htmtxt += "<td>" + $("#txtReprintSN").val() + "</td>";
            //var htmlRes = "";
            //htmlRes = '<span class="label label-warning">OK</span>';
            //htmtxt += "<td>" + htmlRes + "</td>";
            //htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
            //$("#tblHistory tr:first").after(htmtxt);
            // App.unblockUI($("#divScanArea"));
            //保存记录
            var reprintLog = {
                LabelType: $("#selLabelType").val(),
                SN: $("#txtReprintSN").val(),
            };
            WsSystem.SaveLabelReprint(reprintLog,
                            function (result) { });
        },
        bindGrid: function () {
            Reprint.sn = $("#txtSN").val();
            $('#grid').datagrid('load', { sn: Reprint.sn });
        },        
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '条码补印',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryReprintLog',
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
                     { title: '模板类型', field: 'LabelType', width: Reprint.fixWidth(0.15) },
                     { title: '条码', field: 'SN', width: Reprint.fixWidth(0.15) },
                     { title: '操作人', field: 'UpdatedBy', width: Reprint.fixWidth(0.15) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: Reprint.fixWidth(0.15)
                     },
                ]],
                toolbar: [{
                    text: '模板类型<select tabindex="-1" name="selLabelType" id="selLabelType"><option value="2">产品条码</option><option value="1">来料条码</option><option value="3">箱号条码</option><option value="4">物料挂签</option></select>'
                }, '_', {
                    text: '条码<input type="text" id="txtReprintSN"/>'
                }, '-', {
                    id: 'btnPrint',
                    text: '打印',
                    iconCls: 'icon-print',
                    handler: function () {
                        Reprint.print();
                    }
                }, '_', {
                    text: '条码<input type="text" id="txtSN"/>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        Reprint.bindGrid();
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