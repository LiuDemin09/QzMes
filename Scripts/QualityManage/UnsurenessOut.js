var UnsurenessOut = function () {
    return {
        grid: null,
        psn: null,
        //main function to initiate the module
        init: function () {           
            jQuery('#btnClose').click(function () {
                UnsurenessOut.psn = null;
                $('#txtPSN').val("");
                $('#selresult').val("");                
                $('#restartDialog').window('close');
            });
            jQuery('#btnNew').click(function () {
                UnsurenessOut.psn = null;
                $('#txtPSN').val("");
                $('#selresult').val("");
            });
            jQuery('#btnSave').click(function () {
                if ($('#txtPSN').val() != "") {
                    WsSystem.SaveUnsurenessOut($('#txtPSN').val().toUpperCase(), $('#selresult').val(), $("#selresult").find("option:selected").text(),
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                             // alert("保存成功", "提示");
                              $('#txtPSN').val("");
                              $('#selresult').val("");
                              UnsurenessOut.psn = "";
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败" + err.get_message(), "提示");
                      });
                }
                else {
                    JeffComm.alertErr("产品条码不能为空");
                   // alert("产品条码不能为空");
                }
            });
            jQuery('#btnClear').click(function () {
                $('#txtPSN').val("");
                $('#selresult').val("");
            });             
        },
        bindGrid: function () {
            UnsurenessOut.psn = $("#txtPSNName").val();

            $('#grid').datagrid('load', { psn: UnsurenessOut.psn });
        },

        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            UnsurenessOut.psn = rows[0].PSN;
            $('#txtPSN').val(rows[0].PSN);
            $('#selresult').val(rows[0].STATUS);            
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '待处理品出库',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryUnsurenessOut',
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
                     { title: '产品条码', field: 'PSN', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '工单号码', field: 'WorkOrder', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '不良代码', field: 'FailCode', width: UnsurenessOut.fixWidth(0.05) },
                     { title: '不良项', field: 'FailMemo', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '状态', field: 'MEMO', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '加工工序', field: 'StationName', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '数量', field: 'QUANTITY', width: UnsurenessOut.fixWidth(0.05) },
                     { title: '来料条码', field: 'MSN', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '工件名称', field: 'ProductName', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '工件图号', field: 'PartsdrawingCode', width: UnsurenessOut.fixWidth(0.1) },
                     { title: '状态值', field: 'STATUS', width: UnsurenessOut.fixWidth(0.1), hidden: 'true' },
                ]],
                toolbar: [{
                    text: '产品条码 <input type="text" id="txtPSNName"/>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        UnsurenessOut.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '待处理品出库',
                    iconCls: 'icon-add',
                    handler: function () {
                        UnsurenessOut.psn = null;
                        $('#restartDialog').panel({ title: "待处理品出库" });
                        jQuery('#btnNew').show();
                        $('#txtPSN').val("");
                        $('#selresult').val("");
                        $("#restartDialog").dialog('open');  //实现添加记录的页面
                    }
                },
                '-', {
                    id: 'btnEdit',
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            $('#restartDialog').panel({ title: "修改待处理品出库" });
                            jQuery('#btnNew').hide();
                            UnsurenessOut.ShowEditOrViewDialog();//实现修改记录的方法
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行修改");
                           // alert("请选择一条数据进行修改", "提示");
                        }
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
                    UnsurenessOut.psn = rowData["PSN"];
                    // ShowEditOrViewDialog();
                },
                onDblClickRow: function (rowIndex, rowData) {
                    UnsurenessOut.ShowEditOrViewDialog();
                },
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