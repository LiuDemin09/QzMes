var PrintSet = function () {
    return {
        grid: null,
        templateid: null,
        templatetype: null,
        //main function to initiate the module
        init: function () {
            //绑定模板类型
            WsSystem.ListBindTemplateType(function (result) {
                JeffComm.fillSelect($("#selTemplateTypeSet"), result, true);
                JeffComm.fillSelect($("#selTemplateType"), result, true);
            });
            jQuery('#btnClose').click(function () {
                PrintSet.templateid = null;                
                $('#restartDialog').window('close');
            });
            
            jQuery('#btnUpload').click(function () {
                PrintSet.templateid = null;
                window.open('../../Pages/PrintManage/TemplateUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');

            });
            jQuery('#btnSave').click(function () {
                PrintSet.templateid =null;
                var isActive = $("input:radio:checked").val();
                if ($('#selTemplateCode').val().trim() == "" || $('#selTemplateCode').val()==null)
                {
                    JeffComm.alertErr("模板编号不能为空");
                   // alert("模板编号不能为空！");
                    return;
                }
                WsSystem.SavePrintSet($('#selTemplateTypeSet').val(), $('#selTemplateCode').val(), isActive,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                             // alert("保存成功", "提示");                              
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");
                      });
                $("#grid").datagrid("reload");
            });
            $("#selTemplateTypeSet").change(function () {
                WsSystem.QueryTemplateCodes($('#selTemplateTypeSet').val(),
                 function (result) {
                     JeffComm.fillSelect($("#selTemplateCode"), result, true);
                 });
            });
        },
        bindGrid: function () {
            PrintSet.templatetype = $("#selTemplateType").val();
            $('#grid').datagrid('load', { templateType: PrintSet.templatetype });
        },
        delCustInfo: function () {
            if (PrintSet.templateid == null || PrintSet.templateid == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemovePrintSet(PrintSet.templateid,
                    function (result) {
                        if (result == null) {
                            PrintSet.templateid = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            PrintSet.templatetype = $("#selTemplateType").val();
                            $('#grid').datagrid('load', { templateType: PrintSet.templatetype });
                        } else {
                            alert(result, "提示");
                            PrintSet.templatetype = $("#selTemplateType").val();
                            $('#grid').datagrid('load', { templateType: PrintSet.templatetype });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            PrintSet.templatetype = $("#selTemplateType").val();
            $('#grid').datagrid('load', { templateType: PrintSet.templatetype });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            PrintSet.templateid = rows[0].ID;
            $('#selTemplateTypeSet').val(rows[0].TemplateType);
            $('#selTemplateCode').val(rows[0].MEMO);             
            var vtemp = rows[0].ACTIVE;
            if (vtemp == 1) {
                $('#isActive1').attr('checked', 'checked');
            }
            else {
                $('#isActive2').attr('checked', 'checked');
            }
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '模板设置',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryPrintSet',
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
                     { title: '模板类型', field: 'MEMO', width: PrintSet.fixWidth(0.15) },
                     { title: '模板编号', field: 'ID', width: PrintSet.fixWidth(0.15) },
                     { title: '是否激活', field: 'ACTIVE', width: PrintSet.fixWidth(0.15) },
                     { title: '操作人', field: 'UpdatedBy', width: PrintSet.fixWidth(0.15) },
                     {
                         title: '时间', field: 'CreatedDate',formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: PrintSet.fixWidth(0.15)
                     },
                ]],
                toolbar: [{
                    text: '模板类型<select tabindex="-1" name="selTemplateType" id="selTemplateType"></select>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        PrintSet.bindGrid();
                    }
                }, '-', {
                    id: 'btnTemplateDownLoad',
                    text: '模板下载',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/PrintManage/TemplateDownload.aspx?TemplateType=' + $('#selTemplateType').val()
                    + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnSet',
                    text: '模板设置',
                    iconCls: 'icon-edit',
                    handler: function () {
                        $('#restartDialog').window('open');
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        PrintSet.delCustInfo();//实现直接删除数据的方法
                    }
                }, '-', {
                    id: 'btnCodeSoftDownLoad',
                    text: 'CodeSoft7下载',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/PrintManage/CodeSoftDownload.aspx'
                    + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnControlDownLoad',
                    text: '打印插件下载',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/PrintManage/CallControlDownload.aspx'
                    + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
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
                    PrintSet.templateid = rowData["ID"];
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
        }
    };
}();