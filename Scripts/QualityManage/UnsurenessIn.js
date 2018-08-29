var UnsurenessIn = function () {
    return {
        grid: null,
        psn: null,       
        //main function to initiate the module
        init: function () {
            $('#txtFailMemo').attr("disabled", "disabled");
            //初始化客户名称
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
            });
            jQuery('#btnClose').click(function () {
                UnsurenessIn.psn = null;
                $('#txtPSN').val("");
                $('#txtFailCode').val("");
                $('#txtFailMemo').val("");                 
                $('#restartDialog').window('close');
            });
            jQuery('#btnNew').click(function () {
                UnsurenessIn.psn = null;
                $('#txtPSN').val("");
                $('#txtFailCode').val("");
                $('#txtFailMemo').val("");
            });
            jQuery('#btnSave').click(function () {
                if ($('#txtPSN').val() != "" && $('#txtFailCode').val() != "" && $('#txtFailMemo').val() != "") {
                    WsSystem.SaveUnsurenessIn($('#txtPSN').val().toUpperCase(), $('#txtFailCode').val(), $('#txtFailMemo').val(),
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                              //alert("保存成功", "提示");
                              $('#txtPSN').val("");
                              $('#txtFailCode').val("");
                              $('#txtFailMemo').val("");
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败" + err.get_message(), "提示");
                      });
                }
                else {
                    JeffComm.alertErr("产品条码，不良项，不良描述均不能为空哟，亲！");
                    //alert("产品条码，不良项，不良描述均不能为空哟，亲！");
                }
            });
            jQuery('#btnClear').click(function () {
                $('#txtPSN').val("");
                $('#txtFailCode').val("");
                $('#txtFailMemo').val("");
            });
            jQuery('#txtFailCode').keyup(function (e) {
                if (e.keyCode == 13)
                    WsSystem.FindFailMemoByFailCode($('#txtPSNName').val(),
                           function (result) {
                               $('#txtFailMemo').val(result);
                           }, function (err) {
                               JeffComm.errorAlert(err.get_message(), "divMsg");
                           });
            });
        },
        bindGrid: function () {
            UnsurenessIn.psn = $("#txtPSNName").val();
           
            $('#grid').datagrid('load', { psn: UnsurenessIn.psn });
        },
         
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            UnsurenessIn.psn = rows[0].PSN;
            $('#txtPSN').val(rows[0].PSN);
            $('#txtFailCode').val(rows[0].FailCode);
            $('#txtFailMemo').val(rows[0].FailMemo);
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '待处理品入库',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryUnsurenessIn',
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
                     { title: '产品条码', field: 'PSN', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '工单号码', field: 'WorkOrder', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '不良代码', field: 'FailCode', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '不良项', field: 'FailMemo', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '状态', field: 'MEMO', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '加工工序', field: 'StationName', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '数量', field: 'QUANTITY', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '来料条码', field: 'MSN', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '工件名称', field: 'ProductName', width: UnsurenessIn.fixWidth(0.1) },
                     { title: '工件图号', field: 'PartsdrawingCode', width: UnsurenessIn.fixWidth(0.1) },
                ]],
                toolbar: [{
                    text: '产品条码 <input type="text" id="txtPSNName"/>'
                },  
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        UnsurenessIn.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '信息录入',
                    iconCls: 'icon-add',
                    handler: function () {
                        UnsurenessIn.psn = null;
                        $('#restartDialog').panel({ title: "新录待处理品信息" });
                        jQuery('#btnNew').show();
                        $('#txtPSN').val("");
                        $('#txtFailCode').val("");
                        $('#txtFailMemo').val("");
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
                            $('#restartDialog').panel({ title: "修改待处理品信息" });
                            jQuery('#btnNew').hide();
                            UnsurenessIn.ShowEditOrViewDialog();//实现修改记录的方法
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
                    UnsurenessIn.psn = rowData["PSN"];
                    // ShowEditOrViewDialog();
                },
                onDblClickRow: function (rowIndex, rowData) {
                    UnsurenessIn.ShowEditOrViewDialog();
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