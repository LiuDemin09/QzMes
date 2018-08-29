var SysRole = function () {
    return {
        grid: null,
        tree: null,
        roleId: null,
        //main function to initiate the module
        init: function () {
            //$('#txtUserCode').click(function () {
            //    $('#selectUserWindow').window('open');
            //});
            jQuery('#btnClose').click(function () {
                SysRole.roleId = null;
                $('#txtRoleName').val("");
                $('#txtDesc').val("");              
                $('#restartDialog').window('close');
            });
            jQuery('#btnUpdatePermission').click(function () {
                SysRole.getChecked();
                $('#selectPermissionWindow').window('close');
            });
            jQuery('#btnClosePermission').click(function () {
                $('#selectPermissionWindow').window('close');
            });
            jQuery('#btnSave').click(function () {
                var role = {
                    ID: SysRole.roleId,
                    RoleName: $("#txtRoleName").val(),
                    MEMO: $("#txtDesc").val(),
                    STATUS: $('#chkActived').attr("checked") == "checked" ? "0" : "1",
                };
                WsSystem.SaveRole(role,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                              $('#txtRoleName').val("");
                              $('#txtDesc').val("");
                              $('#chkActived').attr("checked", "checked");
                              $('#chkActived').trigger("change");
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");
                      });
            });
            jQuery('#btnClear').click(function () {
                SysRole.roleId = null;
                $('#txtRoleName').val("");
                $('#txtDesc').val("");
                $('#chkActived').attr("checked", "checked");
                $('#chkActived').trigger("change");
            });
        },
        delCustInfo: function () {
            if (SysRole.roleId == null || SysRole.roleId == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
                //alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveRole(SysRole.roleId,
                    function (result) {
                        if (result == null) {
                            SysRole.roleId = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            //SysRole.roleId = $("#txtSUserCode").val();
                            //SysRole.userName = $("#txtSUserName").val();
                            $('#grid').datagrid('load', { });
                        } else {
                            alert(result, "提示");
                            //SysRole.roleId = $("#txtSUserCode").val();
                            //SysRole.userName = $("#txtSUserName").val();
                            $('#grid').datagrid('load', {  });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            //SysRole.roleId = $("#txtSUserCode").val();
            //SysRole.userName = $("#txtSUserName").val();
            $('#grid').datagrid('load', {  });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            SysRole.roleId = rows[0].ID;
            $('#txtRoleName').val(rows[0].RoleName);
            $('#txtDesc').val(rows[0].MEMO); 
            if (rows[0].STATUS == 1) {
                $('#chkActived').attr("checked", "checked");
            } else {
                $('#chkActived').removeAttr("checked");
            }
            $('#chkActived').trigger("change");
            $("#restartDialog").dialog('open');
        },
        //ConfirmPubUserDialog: function () {
        //    var rows = $('#gridselectUser').datagrid('getSelections');  //得到所选择的行             
        //    $('#txtUserCode').attr("disabled", "disabled");
        //    $('#txtUserCode').val(rows[0].UserCode);
        //    $('#txtUserName').val(rows[0].UserName);
        //    $('#txtDeptName').val(rows[0].DeptName);
        //    $('#txtIphone').val(rows[0].MOBILE);

        //    $("#selectUserWindow").dialog('close');
        //},
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        bindGrid: function () {
            //SysRole.roleId = $("#txtSUserCode").val();
            //SysRole.userName = $("#txtSUserName").val();
            $('#grid').datagrid('load', {  });
        },
        //bindGridSelectUser: function () {
        //    SysRole.roleId = $("#txtUserCodePub").val();
        //    SysRole.userName = $("#txtUserNamePub").val();
        //    $('#gridselectUser').datagrid('load', { roleId: SysRole.roleId, userName: SysRole.userName });
        //},
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '角色维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/ListRole',
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
                    { title: '角色ID', field: 'ID', width: SysRole.fixWidth(0.1), hidden: 'true' },
                     { title: '角色名称', field: 'RoleName', width: SysRole.fixWidth(0.1) },
                     { title: '描述', field: 'MEMO', width: SysRole.fixWidth(0.12) },
                     { title: '状态', field: 'STATUS', width: SysRole.fixWidth(0.1), hidden: 'true' },
                     { title: '操作人', field: 'UpdatedBy', width: SysRole.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: SysRole.fixWidth(0.1)
                     },
                ]],
                toolbar: [
                //    {
                //    text: '工号 <input type="text" id="txtSUserCode"/>'
                //}, '-', {
                //    text: '姓名 <input type="text" id="txtSUserName"/>'
                //}, '-', {
                //    id: 'btnSearch',
                //    text: '查询',
                //    iconCls: 'icon-search',
                //    handler: function () {
                //        SysRole.bindGrid();
                //    }
                //}, '-',
                {
                    id: 'btnAdd',
                    text: '新建角色',
                    iconCls: 'icon-add',
                    handler: function () {
                        // $('#txtUserCode').removeAttr("disabled");
                        SysRole.roleId = null;
                        $('#restartDialog').panel({ title: "新建角色" });
                        $('#txtRoleName').val("");
                        $('#txtDesc').val("");
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
                            $('#restartDialog').panel({ title: "修改角色" });
                            SysRole.ShowEditOrViewDialog();//实现修改记录的方法
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
                        SysRole.delCustInfo();//实现直接删除数据的方法
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
                    text: '权限',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            SysRole.permissionPower();
                            $("#selectPermissionWindow").dialog('open');
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行权限分配");
                            //alert("请选择一条数据进行权限分配", "提示");
                        }

                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    SysRole.roleId = rowData["ID"];
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
        },
       
        permissionPower: function () {
            //var data = [{
            //    "id": 1,
            //    "text": "系统",
            //    "children": [{
            //        "id": 11,
            //        "text": "用户管理",
            //        "children": [{
            //            "id": 19,
            //            "text": "增加"
            //        }, {
            //            "id": 3,
            //            "text": "修改"
            //        }, {
            //            "id": 5,
            //            "text": "删除"
            //        }]
            //    }, {
            //        "id": 12,
            //        "text": "角色管理",
            //        "children": [{
            //            "id": 13,
            //            "text": "增加"
            //        }, {
            //            "id": 3,
            //            "text": "修改"
            //        }, {
            //            "id": 5,
            //            "text": "删除"
            //        }]
            //    }]
            //}, {
            //    "id": 2,
            //    "text": "其他",
            //    "state": "closed"
            //}];

            $('#selectPermission').tree({
                lines: true,
                checkbox: true,
                onlyLeafCheck: true,
                cascadeCheck: true,
                //data:data,
                url: '../../services/WsSystem.asmx/GetAllMenuChkBoxTree',
                queryParams: { roleID: SysRole.roleId }//,
            //    onCheck: function (node, checked) {
            //    if (checked) {
            //        var parentNode = $("#selectPermission").tree('getParent', node.target);
            //        if (parentNode != null) {
            //            $("#selectPermission").tree('check', parentNode.target);
            //        }
            //    } else {
            //        var childNode = $("#selectPermission").tree('getChildren', node.target);
            //        if (childNode.length > 0) {
            //            for (var i = 0; i < childNode.length; i++) {
            //                $("#selectPermission").tree('uncheck', childNode[i].target);
            //            }
            //        }
            //    }
            //}
            });      
        },
        getChecked:function (){
            var arr = [];
            var checkeds = $('#selectPermission').tree('getChecked', 'checked');
            for (var i = 0; i < checkeds.length; i++) {
                arr.push(checkeds[i].id);
            }
            // alert(arr.join(','));
            WsSystem.UpdateRolePerm(arr, SysRole.roleId, function () {
                JeffComm.alertSucc("保存成功", 500);
               // alert("保存成功");
            });
        }
    };
}();