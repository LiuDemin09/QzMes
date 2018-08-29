var FailItems = function () {
    return {
        grid: null,
        FailItemID: null,
        FailCode: null,
        FailType:null,
        //main function to initiate the module
        init: function () {
            //初始化不良类别
            WsSystem.ListBindFailType(function (result) {
                JeffComm.fillSelect($("#selFailType"), result, true);
                JeffComm.fillSelect($("#selFailTypeEdit"), result, true);
            });
            jQuery('#btnClose').click(function () {
                FailItems.FailItemID = null;
                $('#txtFailMemo').val("");
                $('#txtFailCodeEdit').val("");                 
                $('#restartDialog').window('close');
            });
            jQuery('#btnNew').click(function () {
                FailItems.FailItemID = null;
                $('#txtFailMemo').val("");
                $('#txtFailCodeEdit').val("");
            });
            
            jQuery('#btnSave').click(function () {
                if ($("#selFailTypeEdit").val() == "") {
                    JeffComm.alertErr("请选择不良类别");
                    //alert("请选择不良类别", "提示");
                    return;
                }
                if ($("#txtFailMemo").val() == "") {
                    JeffComm.alertErr("不良描述不能为空");
                   // alert("不良描述不能为空", "提示");
                    return;
                }
                
                var failitem = {
                    FailCode: $("#txtFailCodeEdit").val(),
                    FailType: $("#selFailTypeEdit").find("option:selected").text(),
                    FailMemo: $("#txtFailMemo").val(),
                    Memo1: $("#selFailTypeEdit").val(),
                };
                WsSystem.SaveFailItems(failitem,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                             // alert("保存成功", "提示");
                              $('#txtFailMemo').val("");
                              $('#txtFailCodeEdit').val("");
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");

                      });
            });
            jQuery('#btnClear').click(function () {
                $('#txtFailMemo').val("");
                $('#txtFailCodeEdit').val("");
            });
            jQuery('#btnDownload').click(function () {

                window.open('../../Pages/QualityManage/FailItemsDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/QualityManage/FailItemsUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');

            });
        },
        bindGrid: function () {
            FailItems.FailCode = $("#txtFailCode").val();
            FailItems.FailType = $("#selFailType").val();
            $('#grid').datagrid('load', { FailCode: FailItems.FailCode, FailType: FailItems.FailType });
        },
        delCustInfo: function () {
            if (FailItems.FailItemID == null || FailItems.FailItemID == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveFailItems(FailItems.FailItemID,
                    function (result) {
                        if (result == null) {
                            FailItems.FailItemID = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            FailItems.FailCode = $("#txtFailCode").val();
                            FailItems.FailType = $("#selFailType").val();
                            $('#grid').datagrid('load', { FailCode: FailItems.FailCode, FailType: FailItems.FailType });
                        } else {
                            alert(result, "提示");
                            FailItems.FailCode = $("#txtFailCode").val();
                            FailItems.FailType = $("#selFailType").val();
                            $('#grid').datagrid('load', { FailCode: FailItems.FailCode, FailType: FailItems.FailType });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            FailItems.FailCode = $("#txtFailCode").val();
            FailItems.FailType = $("#selFailType").val();
            $('#grid').datagrid('load', { FailCode: FailItems.FailCode, FailType: FailItems.FailType });
        },
        //ShowEditOrViewDialog: function () {
        //    var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
        //    FailItems.FailItemID = rows[0].ID;
        //    $('#txtBaseInfoNo').val(rows[0].CODE);
        //    $('#txtBaseInfoName').val(rows[0].NAME);
        //    $('#txtSubBaseInfoNo').val(rows[0].SubCode);
        //    $("#txtSubBaseInfoName").val(rows[0].SubName);
        //    $("#txtDesc").val(rows[0].MEMO);
        //    $("#restartDialog").dialog('open');
        //},
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '不良项维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryFailItems',
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
                     { title: '不良项', field: 'FailCode', width: FailItems.fixWidth(0.15) },
                     { title: '不良类型', field: 'FailType', width: FailItems.fixWidth(0.15) },
                     { title: '不良描述', field: 'FailMemo', width: FailItems.fixWidth(0.15) },
                     { title: '操作人', field: 'UpdatedBy', width: FailItems.fixWidth(0.15) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: FailItems.fixWidth(0.15)
                     },
                ]],
                toolbar: [{
                    text: '不良代码 <input type="text" id="txtFailCode"/>'
                }, '-', {
                    text: '不良类型<select tabindex="-1" name="selFailType" id="selFailType"></select>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        FailItems.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建不良项',
                    iconCls: 'icon-add',
                    handler: function () {
                        $("#restartDialog").dialog('open');  //实现添加记录的页面
                    }
                },
                //'-', {
                //    id: 'btnEdit',
                //    text: '修改',
                //    iconCls: 'icon-edit',
                //    handler: function () {
                //        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                //        if (rows.length == 1) {
                //            FailItems.ShowEditOrViewDialog();//实现修改记录的方法
                //        }
                //        else {
                //            alert("请选择一条数据进行修改", "提示");
                //        }
                //    }
                //},
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        FailItems.delCustInfo();//实现直接删除数据的方法
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
                    FailItems.FailItemID = rowData["FailCode"];
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