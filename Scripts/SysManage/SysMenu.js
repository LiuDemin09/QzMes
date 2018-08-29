var SysMenu = function () {
    return {
        menuCode: null,
        tree: null,
        //main function to initiate the module
        init: function () {
            //$('#txtUserCode').click(function () {
            //    $('#selectUserWindow').window('open');
            //});
            WsSystem.ListParentMenu(function (result) {
                JeffComm.fillSelect($("#selParent"), result, true);
            });
            jQuery('#btnClear').click(function () {
                SysMenu.menuCode = null;
                $('#selParent').val("");
                $('#txtCode').val("");
                $('#txtCode').removeAttr("disabled");
                $('#txtCN').val("");
                $('#txtPageURL').val("");
            });
            
            jQuery('#btnSave').click(function () {
                if ($("#txtCode").val() == "") {
                    JeffComm.alertErr("标识码不能为空");
                    //alert("标识码不能为空", "提示");
                    return;
                }
                if ($("#txtCN").val() == "") {
                    JeffComm.alertErr("菜单名不能为空");
                    //alert("菜单名不能为空", "提示");
                    return;
                }
                if ($("#txtPageURL").val() == "") {
                    JeffComm.alertErr("链接不能为空");
                    //alert("链接不能为空", "提示");
                    return;
                }
                var menu = {
                    CODE: $("#txtCode").val(),
                    ParentCode: $("#selParent").val(),
                    Name: $("#txtCN").val(),
                    PageUrl: $("#txtPageURL").val(),                    
                };
                WsSystem.SaveMenu(menu,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                              $('#selParent').val("");
                              $('#txtCode').val("");
                              $('#txtCN').val("");
                              $('#txtPageURL').val("");
                              SysMenu.initMenu();
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");
                      });
            });
            jQuery('#btndel').click(function () {
                SysMenu.delCustInfo();
                SysMenu.menuCode = null;
                $('#selParent').val("");
                $('#txtCode').val("");
                $('#txtCode').removeAttr("disabled");
                $('#txtCN').val("");
                $('#txtPageURL').val("");
            });
        },
        delCustInfo: function () {
            if (SysMenu.menuCode == null || SysMenu.menuCode == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
                //alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveMenu($('#txtCode').val(),
                    function (result) {
                        if (result == null) {
                            SysMenu.menuCode = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            SysMenu.initMenu();
                        } else {
                            alert(result, "提示");
                            SysMenu.initMenu();
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            SysMenu.initMenu();
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            SysMenu.menuCode = rows[0].ID;
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
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },        
        initMenu: function () {            
            $('#Menu').tree({
                lines: true,
                //checkbox: true,
                //onlyLeafCheck: true,
                //cascadeCheck: true,
                //data:data,
                url: '../../services/WsSystem.asmx/GetAllMenuTree',
                onClick:function(node){
                    //alert(node.text);
                    SysMenu.menuCode = node.id;
                    WsSystem.FindMenuByCode(SysMenu.menuCode, function (result) {
                        $('#selParent').val(result.ParentCode);
                        $('#txtCode').val(result.CODE);
                        $('#txtCode').attr("disabled", "disabled");
                        $('#txtCN').val(result.Name);
                        $('#txtPageURL').val(result.PageUrl);                        
                    });
                 }
               // queryParams: { roleID: SysMenu.menuCode }//,
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
        }
    };
}();