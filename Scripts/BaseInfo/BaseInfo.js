var BaseInfo = function () {
    return {       
        grid: null,
        baseinfoID: null,
        basename: null,
        //main function to initiate the module
        init: function () {
            $('#txtBaseInfoNo').attr("disabled", "disabled");
            //初始化基本信息名称
            WsSystem.ListBaseName(function (result) {
                JeffComm.fillSelect($("#selBaseName"), result, true);
            });
            jQuery('#btnClose').click(function () {
                BaseInfo.baseinfoID = null;
                $('#txtBaseInfoNo').val("");
                $('#txtBaseInfoName').val("");
                //$('#txtSubBaseInfoNo').val("");
               // $("#txtSubBaseInfoName").val("");
                $("#txtDesc").val("");
                $('#restartDialog').window('close');
            });           
            jQuery('#btnNew').click(function () {
                BaseInfo.baseinfoID = null;
                WsSystem.GetBasBaseCode(function (result) {
                    $('#txtBaseInfoNo').val(result);
                    $('#txtBaseInfoName').val("");
                   // $('#txtSubBaseInfoNo').val("");
                    //$('#txtSubBaseInfoName').val("");
                    $('#txtDesc').val("");
                });
            });
            //jQuery('#btnNewNotCode').click(function () {
            //    BaseInfo.baseinfoID = null;
            //    $('#txtBaseInfoNo').attr("disabled", false);
            //    $('#txtSubBaseInfoNo').val("");
            //    $('#txtSubBaseInfoName').val("");
            //    $('#txtDesc').val("");
            //});
            jQuery('#btnSave').click(function () {
                if ($('#txtBaseInfoNo').val().trim() == null || $('#txtBaseInfoNo').val().trim() == "")
                {
                    JeffComm.alertErr("信息编号不能为空");
                   // alert("信息编号不能为空");
                    return;
                }
                //var baseinfo = {
                //    ID:BaseInfo.baseinfoID,
                //    CODE: $("#txtBaseInfoNo").val().trim(),
                //    NAME: $("#txtBaseInfoName").val().trim(),
                //    SubCode: $("#txtSubBaseInfoNo").val().trim(),
                //    SubName: $("#txtSubBaseInfoName").val().trim(),
                //    MEMO: $("#txtDesc").val(),
                //};
                var baseinfo = {
                    ID: BaseInfo.baseinfoID,
                    CODE: $("#selFather").combobox("getValue"),
                    NAME: $("#selFather").combobox("getText"),
                    SubCode: $("#txtBaseInfoNo").val().trim(),
                    SubName: $("#txtBaseInfoName").val().trim(),
                    MEMO: $("#txtDesc").val(),
                };
                WsSystem.SaveBaseInfo(baseinfo,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                                //alert("保存成功", "提示");
                              $('#txtBaseInfoNo').val("");
                              $('#txtBaseInfoNo').val("");
                                $('#txtDesc').val("");
                                $("#grid").datagrid("reload");
                                //初始化基本信息名称
                                WsSystem.ListBaseName(function (result) {
                                    JeffComm.fillSelect($("#selBaseName"), result, true);
                                });
                            } else {
                                alert(result, "提示");
                            }
                        }, function (err) {
                            alert("保存失败:" + err.get_message(), "提示");

                        });
            });
            jQuery('#btnClear').click(function () {
                $('#txtBaseInfoNo').val("");
                $('#txtBaseInfoName').val("");
                //$('#txtSubBaseInfoNo').val("");
               // $("#txtSubBaseInfoName").val("");
                $("#txtDesc").val("");
            });
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/BaseInfo/BasInfoDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/BaseInfo/BaseInfoUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');

            });
            $('#selFather').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListBaseNameEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                }
                
            });
        },
        bindGrid: function () {
            BaseInfo.basename = $("#selBaseName").val();
            $('#grid').datagrid('load', { baseName: BaseInfo.basename });
        },
        delCustInfo: function () {
            if (BaseInfo.baseinfoID == null || BaseInfo.baseinfoID == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveBaseInfo(BaseInfo.baseinfoID,
                    function (result) {
                        if (result == null) {
                            BaseInfo.baseinfoID = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            BaseInfo.basename = $("#selBaseName").val();
                            $('#grid').datagrid('load', { baseName: BaseInfo.basename });
                        } else {
                            alert(result, "提示");
                            BaseInfo.basename = $("#selBaseName").val();
                            $('#grid').datagrid('load', { baseName: BaseInfo.basename });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            BaseInfo.basename = $("#selBaseName").val();
            $('#grid').datagrid('load', { baseName: BaseInfo.basename });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            BaseInfo.baseinfoID = rows[0].ID;
            $('#txtBaseInfoNo').val(rows[0].SubCode);
            $('#txtBaseInfoName').val(rows[0].SubName);
           // $('#txtSubBaseInfoNo').val(rows[0].SubCode);
            // $("#txtSubBaseInfoName").val(rows[0].SubName);
            $('#selFather').combobox('setValues', rows[0].CODE)
            $("#txtDesc").val(rows[0].MEMO);            
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '基本信息维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryBaseInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,               
                columns: [[ //选择
                     { title: '信息编号', field: 'CODE', width: BaseInfo.fixWidth(0.15) },
                     { title: '信息名称', field: 'NAME', width: BaseInfo.fixWidth(0.15) },
                     { title: '子信息编号', field: 'SubCode', width: BaseInfo.fixWidth(0.15) },
                     { title: '子信息名称', field: 'SubName', width: BaseInfo.fixWidth(0.15) },
                     { title: '操作人', field: 'UpdatedBy', width: BaseInfo.fixWidth(0.15) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: BaseInfo.fixWidth(0.15)
                     },
                ]],
                toolbar: [{
                    text: '信息名称<select tabindex="-1" name="selBaseName" id="selBaseName"><option value="0">请选择</option> </select>'
                }, 
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        BaseInfo.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建信息',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建信息" });
                        $('#btnDownload').show();
                        $('#btnImport').show();
                        $('#btnNew').show();
                       // $('#btnNewNotCode').show();
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
                            $('#restartDialog').panel({ title: "修改信息" });
                            $('#btnDownload').hide();
                            $('#btnImport').hide();
                            $('#btnNew').hide();
                           // $('#btnNewNotCode').hide();
                            BaseInfo.ShowEditOrViewDialog();//实现修改记录的方法
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
                        BaseInfo.delCustInfo();//实现直接删除数据的方法
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
                    BaseInfo.baseinfoID = rowData["ID"];
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