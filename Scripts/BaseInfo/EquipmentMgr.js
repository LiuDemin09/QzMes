var EquipmentMgr = function () {
    return {
        grid: null,
        equipNo: null,
        equipName: null,
        //main function to initiate the module
        init: function () {
            //初始化机床类型
            //WsSystem.ListBindMachineType(function (result) {
            //    JeffComm.fillSelect($("#txtEquipmentType"), result, true);
            //});
            jQuery('#btnClose').click(function () {
                EquipmentMgr.equipNo = null;
                $('#txtEquipNo').val("");
                $('#txtCountry').val("");
                $('#txtEquipmentName').val("");
                $("#txtEquipmentType").val("");
                $('#txtAxisCount').val("");
                $('#txtModel').val("");
                $('#txtPower').val("");
                $('#txtLocalation').val("");
                $('#txtStatus').val("");
                $('#txtOutCode').val("");
                $('#restartDialog').window('close');
            });
            jQuery('#btnNew').click(function () {
                EquipmentMgr.equipNo = null;
                $('#txtEquipNo').val("");
                $('#txtCountry').val("");
                $('#txtEquipmentName').val("");
                $("#txtEquipmentType").val("");
                $('#txtAxisCount').val("");
                $('#txtModel').val("");
                $('#txtPower').val("");
                $('#txtLocalation').val("");
                $('#txtStatus').val("");
                $('#txtOutCode').val("");
            });
            jQuery('#btnSave').click(function () {
                if (isNaN($("#txtAxisCount").val())) {
                    JeffComm.alertErr("请输入数字", 500);
                   // alert("请输入数字");
                    $("#txtAxisCount").val().focus();
                    return;
                }

                var equipinfo = {
                    CODE: $("#txtEquipNo").val().trim(),
                    COMPANY: $("#txtCountry").val().trim(),
                    MachineName: $("#txtEquipmentName").val(),
                    MachineType: $("#txtEquipmentType").val(),
                    AxisNumber: $("#txtAxisCount").val().trim(),
                    MODEL: $("#txtModel").val().trim(),
                    POWER: $("#txtPower").val().trim(),
                    LOCATION: $("#txtLocalation").val().trim(),
                    STATUS: $("#txtStatus").val().trim(),
                    OutCode: $("#txtOutCode").val().trim(),
                    UseDate: $("#txtUseTime").val().trim(),
                };
                WsSystem.SaveEquipmentInfo(equipinfo,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                              //alert("保存成功", "提示");
                              $('#txtEquipNo').val("");
                              $('#txtCountry').val("");
                              $('#txtEquipmentName').val("");
                              $("#txtEquipmentType").val("");
                              $('#txtAxisCount').val("");
                              $('#txtModel').val("");
                              $('#txtPower').val("");
                              $('#txtLocalation').val("");
                              $('#txtStatus').val("");
                              $('#txtOutCode').val("");
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");
                      });
            });
            jQuery('#btnClear').click(function () {
                $('#txtEquipNo').val("");
                $('#txtCountry').val("");
                $('#txtEquipmentName').val("");
                $("#txtEquipmentType").val("");
                $('#txtAxisCount').val("");
                $('#txtModel').val("");
                $('#txtPower').val("");
                $('#txtLocalation').val("");
                $('#txtStatus').val("");
                $('#txtOutCode').val("");
            });
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/BaseInfo/EquipmentDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/BaseInfo/EquipmentUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
            });
        },
        bindGrid: function () {
            EquipmentMgr.equipName = $("#txtEquipName").val();
            $('#grid').datagrid('load', { equipName: EquipmentMgr.equipName });
        },
        delCustInfo: function () {
            if (EquipmentMgr.equipNo == null || EquipmentMgr.equipNo == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
                //alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveEquipmentInfo(EquipmentMgr.equipNo,
                    function (result) {
                        if (result == null) {
                            EquipmentMgr.equipNo = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            EquipmentMgr.equipName = $("#txtEquipName").val();
                            $('#grid').datagrid('load', { equipName: EquipmentMgr.equipName });
                        } else {
                            alert(result, "提示");
                            EquipmentMgr.equipName = $("#txtEquipName").val();
                            $('#grid').datagrid('load', { equipName: EquipmentMgr.equipName });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            EquipmentMgr.equipName = $("#txtEquipName").val().trim();
            $('#grid').datagrid('load', { equipName: EquipmentMgr.equipName });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            EquipmentMgr.equipNo = rows[0].CODE;
            $('#txtEquipNo').val(rows[0].CODE);
            $('#txtCountry').val(rows[0].COMPANY);
            $('#txtEquipmentName').val(rows[0].MachineName);
            $('#txtEquipmentType').val(rows[0].MachineType);
            $('#txtAxisCount').val(rows[0].AxisNumber);
            $('#txtModel').val(rows[0].MODEL);
            $('#txtPower').val(rows[0].POWER);
            $('#txtLocalation').val(rows[0].LOCATION);
            $('#txtStatus').val(rows[0].STATUS);
            $('#txtOutCode').val(rows[0].OutCode);
            $('#txtUseTime').val(rows[0].UseDate);
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '设备维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryEquipmentInfo',
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
                     { title: '设备编号', field: 'CODE', width: EquipmentMgr.fixWidth(0.08) },
                     { title: '国别厂家', field: 'COMPANY', width: EquipmentMgr.fixWidth(0.12) },
                     { title: '设备名称', field: 'MachineName', width: EquipmentMgr.fixWidth(0.08) },
                     { title: '机床类型', field: 'MachineType', width: EquipmentMgr.fixWidth(0.08) },
                     { title: '轴数', field: 'AxisNumber', width: EquipmentMgr.fixWidth(0.03) },
                     { title: '型号', field: 'MODEL', width: EquipmentMgr.fixWidth(0.08) },
                     { title: '功率', field: 'POWER', width: EquipmentMgr.fixWidth(0.05) },
                     { title: '车间位置', field: 'LOCATION', width: EquipmentMgr.fixWidth(0.05) },
                     { title: '状态', field: 'STATUS', width: EquipmentMgr.fixWidth(0.05) },
                     { title: '出厂编号', field: 'OutCode', width: EquipmentMgr.fixWidth(0.08) },
                     {
                         title: '启用日期', field: 'UseDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: EquipmentMgr.fixWidth(0.08)
                     },
                     { title: '操作人', field: 'UpdatedBy', width: EquipmentMgr.fixWidth(0.08) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: EquipmentMgr.fixWidth(0.08)
                     },
                ]],
                toolbar: [{
                    text: '设备名称 <input type="text" id="txtEquipName"/>'
                },'-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        EquipmentMgr.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建设备',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建设备" });
                        $('#btnDownload').show();
                        $('#btnImport').show();
                        $('#btnNew').show();
                        $('#txtEquipNo').removeAttr("disabled");
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
                            $('#restartDialog').panel({ title: "修改设备" });
                            $('#btnDownload').hide();
                            $('#btnImport').hide();
                            $('#btnNew').hide();
                            $('#txtEquipNo').attr("disabled", "disabled");
                            EquipmentMgr.ShowEditOrViewDialog();//实现修改记录的方法
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行修改");
                            //alert("请选择一条数据进行修改", "提示");
                        }
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        EquipmentMgr.delCustInfo();//实现直接删除数据的方法
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
                    EquipmentMgr.equipNo = rowData["CODE"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        }
    };
}();