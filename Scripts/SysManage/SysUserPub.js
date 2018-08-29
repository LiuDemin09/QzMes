var SysUserPub = function () {
    return {
        grid: null,
        tree: null,
        userCode: null,
        userName: null,
        name:null,
        //main function to initiate the module
        init: function () {
            
            jQuery('#btnClose').click(function () {
                SysUserPub.userCode = null;
                $('#txtUserCode').val("");
                $('#txtUserName').val("");
                $('#txtDeptName').val("");
                $('#txtIphone').val("");
                $('#restartDialog').window('close');
            });
            
           
            jQuery('#btnSave').click(function () {
                var o = {
                    UserCode: $("#txtUserCode").val(),
                    UserName: $("#txtUserName").val(),
                    DeptName: $("#txtDeptName").val(),
                    Mobile: $("#txtIphone").val()
                    
                };
                WsSystem.SaveUserPub(o,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                              $('#txtUserCode').removeAttr("disabled");
                              $('#txtUserCode').val("");
                              $('#txtUserName').val("");
                              $('#txtDeptName').val("");
                              $('#txtIphone').val("");                              
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");
                      });
            });
            jQuery('#btnClear').click(function () {
                SysUserPub.userCode =null;
                
                $('#txtUserCode').val("");
                $('#txtUserName').val("");
                $('#txtDeptName').val("");
                $('#txtIphone').val("");
              
            });             
        },
        delCustInfo: function () {
            if (SysUserPub.userCode == null || SysUserPub.userCode == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveUserPub(SysUserPub.userCode,
                    function (result) {
                        if (result == null) {
                            SysUserPub.userCode = null;
                            JeffComm.alertSucc("删除成功", 500);
                           // alert("删除成功", "提示");
                            SysUserPub.userCode = $("#txtSUserCode").val();
                            SysUserPub.userName = $("#txtSUserName").val();
                            $('#grid').datagrid('load', { userCode: SysUserPub.userCode, userName: SysUserPub.userName });
                        } else {
                            alert(result, "提示");
                            SysUserPub.userCode = $("#txtSUserCode").val();
                            SysUserPub.userName = $("#txtSUserName").val();
                            $('#grid').datagrid('load', { userCode: SysUserPub.userCode, userName: SysUserPub.userName });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            SysUserPub.userCode = $("#txtSUserCode").val();
            SysUserPub.userName = $("#txtSUserName").val();
            $('#grid').datagrid('load', { userCode: SysUserPub.userCode, userName: SysUserPub.userName });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            SysUserPub.userCode = rows[0].UserCode;
            $('#txtUserCode').attr("disabled", "disabled");
            $('#txtUserCode').val(rows[0].UserCode);
            $('#txtUserName').val(rows[0].UserName);
            $('#txtDeptName').val(rows[0].DeptName);
            $('#txtIphone').val(rows[0].MOBILE);
             

            $("#restartDialog").dialog('open');
        },
       
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        
        bindGrid: function () {
            SysUserPub.userCode = $("#txtSUserCode").val();
            SysUserPub.userName = $("#txtSUserName").val();
            $('#grid').datagrid('load', { userCode: SysUserPub.userCode, userName: SysUserPub.userName });
        },
        
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '员工维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/FindUsersPub',
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
                     { title: '员工工号', field: 'UserCode', width: SysUserPub.fixWidth(0.1) },
                     { title: '员工姓名', field: 'UserName', width: SysUserPub.fixWidth(0.12) },
                     { title: '部门名称', field: 'DeptName', width: SysUserPub.fixWidth(0.1) }                                       
                     
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
                        SysUserPub.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建员工',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建员工" });
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
                            $('#restartDialog').panel({ title: "修改员工" });
                            SysUserPub.ShowEditOrViewDialog();//实现修改记录的方法
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
                        SysUserPub.delCustInfo();//实现直接删除数据的方法
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
                }
                ],
                onClickRow: function (rowIndex, rowData) {
                    SysUserPub.userCode = rowData["UserCode"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 50,
                pageNumber: 1,
                pageList: [50,60, 70, 40, 80],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        
        }
        
    };
}();