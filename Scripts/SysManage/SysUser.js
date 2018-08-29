var SysUser = function () {
    return {
        grid: null,
        tree: null,
        userCode: null,
        userName: null,
        name:null,
        //main function to initiate the module
        init: function () {
            $('#txtUserCode').click(function () {
                $('#selectUserWindow').window('open');
            });
            jQuery('#btnClose').click(function () {
                SysUser.userCode = null;
                $('#txtUserCode').val("");
                $('#txtUserName').val("");
                $('#txtDeptName').val("");
                $('#txtIphone').val("");
                $('#restartDialog').window('close');
            });
            jQuery('#btnClosePub').click(function () {                
                $('#selectUserWindow').window('close');
            });
            jQuery('#btnCloseRole').click(function () {                
                $('#selectRoleWindow').window('close');
            });
            jQuery('#btnSave').click(function () {
                var o = {
                    UserCode: $("#txtUserCode").val(),
                    UserName: $("#txtUserName").val(),
                    DeptName: $("#txtDeptName").val(),
                    Mobile: $("#txtIphone").val(),
                    LoginMode: $('#chkWfLogin').attr("checked") == "checked" ? "0" : "1",
                    Status: $('#chkWfLogin').attr("checked") == "checked" ? "0" : "1"
                };
                WsSystem.SaveUser(o,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                              $('#txtUserCode').removeAttr("disabled");
                              $('#txtUserCode').val("");
                              $('#txtUserName').val("");
                              $('#txtDeptName').val("");
                              $('#txtIphone').val("");
                              $('#chkWfLogin').attr("checked", "checked");
                              $('#chkWfLogin').trigger("change");
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");
                      });
            });
            jQuery('#btnClear').click(function () {
                SysUser.userCode =null;
                $('#txtUserCode').removeAttr("disabled");
                $('#txtUserCode').val("");
                $('#txtUserName').val("");
                $('#txtDeptName').val("");
                $('#txtIphone').val("");
                $('#chkWfLogin').attr("checked", "checked");
                $('#chkWfLogin').trigger("change");
            });             
        },
        delCustInfo: function () {
            if (SysUser.userCode == null || SysUser.userCode == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveUser(SysUser.userCode,
                    function (result) {
                        if (result == null) {
                            SysUser.userCode = null;
                            JeffComm.alertSucc("删除成功", 500);
                           // alert("删除成功", "提示");
                            SysUser.userCode = $("#txtSUserCode").val();
                            SysUser.userName = $("#txtSUserName").val();
                            $('#grid').datagrid('load', { userCode: SysUser.userCode, userName: SysUser.userName });
                        } else {
                            alert(result, "提示");
                            SysUser.userCode = $("#txtSUserCode").val();
                            SysUser.userName = $("#txtSUserName").val();
                            $('#grid').datagrid('load', { userCode: SysUser.userCode, userName: SysUser.userName });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            SysUser.userCode = $("#txtSUserCode").val();
            SysUser.userName = $("#txtSUserName").val();
            $('#grid').datagrid('load', { userCode: SysUser.userCode, userName: SysUser.userName });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            SysUser.userCode = rows[0].UserCode;
            $('#txtUserCode').attr("disabled", "disabled");
            $('#txtUserCode').val(rows[0].UserCode);
            $('#txtUserName').val(rows[0].UserName);
            $('#txtDeptName').val(rows[0].DeptName);
            $('#txtIphone').val(rows[0].MOBILE);
            if (rows[0].LoginMode == 1) {
                $('#chkWfLogin').attr("checked", "checked");
            } else {
                $('#chkWfLogin').removeAttr("checked");
            }
            $('#chkWfLogin').trigger("change");

            $("#restartDialog").dialog('open');
        },
        ConfirmPubUserDialog: function () {
            var rows = $('#gridselectUser').datagrid('getSelections');  //得到所选择的行             
            $('#txtUserCode').attr("disabled", "disabled");
            $('#txtUserCode').val(rows[0].UserCode);
            $('#txtUserName').val(rows[0].UserName);
            $('#txtDeptName').val(rows[0].DeptName);
            $('#txtIphone').val(rows[0].MOBILE);
            
            $("#selectUserWindow").dialog('close');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        
        bindGrid: function () {
            SysUser.userCode = $("#txtSUserCode").val();
            SysUser.userName = $("#txtSUserName").val();
            $('#grid').datagrid('load', { userCode: SysUser.userCode, userName: SysUser.userName });
        },
        bindGridSelectUser: function () {
            SysUser.userCode = $("#txtUserCodePub").val();
            SysUser.userName = $("#txtUserNamePub").val();
            $('#gridselectUser').datagrid('load', { userCode: SysUser.userCode, userName: SysUser.userName });
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '用户维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/FindUsers',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '用户工号', field: 'UserCode', width: SysUser.fixWidth(0.1) },
                     { title: '用户名', field: 'UserName', width: SysUser.fixWidth(0.12) },
                     { title: '部门名称', field: 'DeptName', width: SysUser.fixWidth(0.1) },                                       
                     {
                         title: '操作人', field: 'UpdatedBy',   width: SysUser.fixWidth(0.1)
                     },
                     {
                         title: '时间', field: 'CreatedDate',  formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: SysUser.fixWidth(0.1)
                     },
                ]],
                toolbar: [{
                    text: '工号 <input type="text" id="txtSUserCode"/>'
                }, '-', {
                    text: '姓名 <input type="text" id="txtSUserName"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        SysUser.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建用户',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建用户" });
                        $('#txtUserCode').removeAttr("disabled");
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
                            $('#restartDialog').panel({ title: "修改用户" });
                            SysUser.ShowEditOrViewDialog();//实现修改记录的方法
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
                        SysUser.delCustInfo();//实现直接删除数据的方法
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
                },
                '-', {
                    id: 'btnPermission',
                    text: '分配角色',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            SysUser.permissionPower();
                            $("#selectRoleWindow").dialog('open');
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行角色分配");
                            //alert("请选择一条数据进行角色分配", "提示");
                        }
                        
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    SysUser.userCode = rowData["UserCode"];
                    // ShowEditOrViewDialog();
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
        },
        initGridSelectUser: function () {
            $('#gridselectUser').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '用户',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/FindPublicUsers',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                columns: [[ //选择
                     { title: '用户工号', field: 'UserCode', width: SysUser.fixWidth(0.1) },
                     { title: '用户名', field: 'UserName', width: SysUser.fixWidth(0.12) },
                     { title: '部门名称', field: 'DeptName', width: SysUser.fixWidth(0.1) },
                     { title: '联系方式', field: 'MOBILE', width: SysUser.fixWidth(0.1) },
                ]],
                toolbar: [{
                    text: '工号 <input type="text" id="txtUserCodePub"/>'
                }, '-', {
                    text: '姓名 <input type="text" id="txtUserNamePub"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        SysUser.bindGridSelectUser();
                    }
                },  '-', {
                    id: 'btnConfirm',
                    text: '确定',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#gridselectUser').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            SysUser.ConfirmPubUserDialog();
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行角色分配");
                        }
                    }
                }, '-', {
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#gridselectUser").datagrid("reload");
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    SysUser.userCode = rowData["UserCode"];
                    // ShowEditOrViewDialog();
                }
            });
            $('#gridselectUser').datagrid('getPager').pagination({
                pageSize: 6,
                pageNumber: 1,
                pageList: [6,8,10,20, 30, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        },
        permissionPower: function () {
            $('#gridRole').datagrid({
                title: '分配角色',
                url: '../../services/WsSystem.asmx/TreeRoleUser',
                queryParams: { usercode: SysUser.userCode },
                width: function () { return document.body.clientWidth * 0.6 },
                rownumbers: true,
                columns: [[
                { field: 'ck', checkbox: true, width: SysUser.fixWidth(0.1) },
                { field: 'ID', title: '角色ID', width: SysUser.fixWidth(0.1), hidden: 'true' },
                { field: 'RoleName', title: '角色名称', width: SysUser.fixWidth(0.1) },
                { field: 'MEMO', title: '角色描述', width: SysUser.fixWidth(0.3) }                
                ]],
                singleSelect: false,
                selectOnCheck: true,
                checkOnSelect: true,
                onLoadSuccess: function (data) {
                    if (data) {
                        $.each(data.rows, function (index, item) {
                            if (item.Checked) {
                                $('#gridRole').datagrid('checkRow', index);
                            }
                        });
                    }
                },toolbar: [{
                    id: 'btnConfirm',
                    text: '确定',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var checkedItems = $('#gridRole').datagrid('getChecked');
                        var names = [];
                        $.each(checkedItems, function (index, item) {
                            names.push(item.ID);
                        });
                        WsSystem.UpdateUserRole(names, SysUser.userCode, function () {
                            $("#selectRoleWindow").dialog('close');
                            JeffComm.alertSucc("保存成功", 500);
                            //alert("保存成功", "提示");
                        }, function (err) {
                            alert("更新失败" + err.get_message(), "提示");
                        });
                    }
                }]
            });
        }
        
    };
}();