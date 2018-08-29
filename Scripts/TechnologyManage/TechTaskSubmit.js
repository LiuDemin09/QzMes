var TechTaskSubmit = function () {
    return {       
        grid: null,
        techtasksubmitID: null,
        partsdrawingno: null,
        taskstatus: null,
        islook:false,
        //main function to initiate the module
        init: function () {
            $('#txtCustName').attr("disabled", "disabled");
            $('#txtProductName').attr("disabled", "disabled");
            $('#txtPlanTime').attr("disabled", "disabled");
            $('#txtuploadpath').attr("disabled", "disabled");

            //初始化零件图号             
            $('#selPartDrawingNo').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListPartDrawingNoToTechEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                }                
            });
            $("#selPartDrawingNo").combobox({
                onChange: function (n, o) {
                    if (!TechTaskSubmit.islook) {
                        var selPD = $("#selPartDrawingNo").combobox("getText");// $("#selEquitmentType").find("option:selected").text();
                        selPD = $("#selPartDrawingNo").combobox("getValue");
                        WsSystem.QueryPDNoInfo(selPD,
                         function (result) {
                             if (result != "") {
                                 var vres = new Array();
                                 vres = result.split(",");
                                 if (vres[3] == "1" || vres[3] == "9") {
                                     $('#txtCustName').val(vres[0]);
                                     $('#txtProductName').val(vres[1]);
                                     $('#txtPlanTime').val(vres[2]);
                                     TechTaskSubmit.bindGridRoute();//绑定路由信息
                                     TechTaskSubmit.taskstatus = "P";
                                 }
                                 else {
                                     TechTaskSubmit.taskstatus = "F";
                                     JeffComm.alertErr("该图号现在是" + vres[4] + "状态，禁止修改提交");
                                 }
                             }
                         });
                    }
                } 
            });
            //$("#selPartDrawingNo").change(function () {
            //    var selPD = $("#selPartDrawingNo").combobox("getText");// $("#selEquitmentType").find("option:selected").text();
            //    selPD = $("#selPartDrawingNo").combobox("getValue");
            //    WsSystem.QueryPDNoInfo(selPD,
            //     function (result) {
            //         if (result != "") {
            //             var vres = new Array();
            //             vres = result.split(",");
            //             $('#txtCustName').val(vres[0]);
            //             $('#txtProductName').val(vres[1]);
            //             $('#txtPlanTime').val(vres[2]);
            //         }
            //     });
            //});
            $("#uploadify").uploadify({
                //指定swf文件
                'swf': '../../Scripts/uploadify/uploadify.swf',
                //后台处理的页面
                'uploader': '../../services/UploadHandler.ashx',
                //按钮显示的文字
                'buttonText': '上传文件',
                //显示的高度和宽度，默认 height 30；width 120
                //'height': 15,
                //'width': 80,
                //上传文件的类型  默认为所有文件    'Image/All Files'  ;  '*.*'
                //在浏览窗口底部的文件类型下拉菜单中显示的文本
                'fileTypeDesc': 'All Files',
                //允许上传的文件后缀
                // 'fileTypeExts': '*.gif; *.jpg; *.png',
                //发送给后台的其他参数通过formData指定
                //'formData': { 'someKey': 'someValue', 'someOtherKey': 1 },
                //上传文件页面中，你想要用来作为文件队列的元素的id, 默认为false  自动生成,  不带#
                //'queueID': 'fileQueue',
                //设置文件的大小
                // 'fileSizeLimit':'204800',
                //选择文件后自动上传
                'auto': true,
                //设置为true将允许多文件上传
                'multi': true,
                //上传成功后执行
                'onUploadSuccess': function (file, data, response) {
                    //$('#' + file.id).find('.data').html(' 上传完毕');
                    $("#txtuploadpath").val(data);
                }
            });

            jQuery('#btnClose').click(function () {
                TechTaskSubmit.techtasksubmitID = null;
                $('#txtCustName').val("");
                $('#txtProductName').val("");
                $('#txtPlanTime').val("");
                $('#txtuploadpath').val("");                
                $('#restartDialog').window('close');
            });
            jQuery('#btnClear').click(function () {
                TechTaskSubmit.techtasksubmitID = null;
                $('#txtCustName').val("");
                $('#txtProductName').val("");
                $('#txtPlanTime').val("");
                $('#txtuploadpath').val("");                
            });
           
            jQuery('#btnSave').click(function () {
                //if ($('#txtTechFinishTime').val().trim() == null || $('#txtTechFinishTime').val().trim() == "")
                //{
                //    JeffComm.alertErr("工艺要求完成时间不能为空");                  
                //    return;
                //}               
                if (TechTaskSubmit.taskstatus == "F" || TechTaskSubmit.taskstatus == null)
                {
                    JeffComm.alertErr("禁止修改和提交");
                    return;
                }
                var tempinfo = {
                    PARTSDRAWINGNO: $("#selPartDrawingNo").combobox("getValue"),
                    CustName: $("#txtCustName").val(),
                    ProductName: $("#txtProductName").val(),
                    PlanDate: $("#txtPlanTime").val(),
                    ProcessPath: $("#txtuploadpath").val()
                };
                WsSystem.SaveTechnologyTask(tempinfo,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                                //alert("保存成功", "提示");
                              $('#txtCustName').val("");
                              $('#txtProductName').val("");
                              $('#txtPlanTime').val("");
                              $('#txtuploadpath').val("");
                             // $('#restartDialog').window('close');
                                $("#grid").datagrid("reload");
                                ////初始化基本信息名称
                                //WsSystem.ListBaseName(function (result) {
                                //    JeffComm.fillSelect($("#selBaseName"), result, true);
                                //});
                            } else {
                                alert(result, "提示");
                            }
                        }, function (err) {
                            alert("保存失败:" + err.get_message(), "提示");

                        });
            }); 
        },
        bindGrid: function () {
            // TechTaskSubmit.partsdrawingno = $("#selPartsdrawingno").val();
            $('#grid').datagrid('load', {Status:"1,9,2,3,4,5,6,7,8" ,PartsDrawingNo:$("#txtPDNo").val()});
        },
        //delCustInfo: function () {
        //    if (TechTaskSubmit.techtasksubmitID == null || TechTaskSubmit.techtasksubmitID == "") {
        //        JeffComm.alertErr("请选择一条数据进行删除"); 
        //    } else {
        //        WsSystem.RemoveTechnologyInfo(TechTaskSubmit.techtasksubmitID,
        //            function (result) {
        //                if (result == null) {
        //                    TechTaskSubmit.techtasksubmitID = null;
        //                    JeffComm.alertSucc("删除成功", 500);
        //                    //alert("删除成功", "提示");
        //                    TechTaskSubmit.partsdrawingno = $("#selPartsdrawingno").val();
        //                    $('#grid').datagrid('load', { PartsDrawingNo: TechTaskSubmit.partsdrawingno });
        //                } else {
        //                    alert(result, "提示");
        //                    TechTaskSubmit.partsdrawingno = $("#selBaseName").val();
        //                    $('#grid').datagrid('load', { PartsDrawingNo: TechTaskSubmit.partsdrawingno });
        //                }
        //            }, function (err) {
        //                alert("删除失败:" + err.get_message(), "提示");
        //            });
        //    }
        //    TechTaskSubmit.partsdrawingno = $("#selPartsdrawingno").val();
        //    $('#grid').datagrid('load', { PartsDrawingNo: TechTaskSubmit.partsdrawingno });
        //},
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            //TechTaskSubmit.techtasksubmitID = rows[0].ID;
            //TechTaskSubmit.taskstatus = rows[0].STATUS;             
            $('#selPartDrawingNo').combobox('setValues', rows[0].PARTSDRAWINGNO)
            $('#txtCustName').val(rows[0].CustName);
            $('#txtProductName').val(rows[0].ProductName);
            $('#txtPlanTime').val(rows[0].PlanDate);
            //TechTaskSubmit.bindGridRoute();//绑定路由信息 
            $('#gridRoute').datagrid('load', { PartsDrawingNo: rows[0].PARTSDRAWINGNO });
            //WsSystem.QueryPDNoInfo(rows[0].PARTSDRAWINGNO,
            //         function (result) {
            //             if (result != "") {
            //                 var vres = new Array();
            //                 vres = result.split(",");                             
            //                $('#txtCustName').val(vres[0]);
            //                $('#txtProductName').val(vres[1]);
            //                $('#txtPlanTime').val(vres[2]);
            //                TechTaskSubmit.bindGridRoute();//绑定路由信息                                                         
            //             }
            //         });            
           
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '任务列表',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryTechnologyEngineerTask',
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
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: TechTaskSubmit.fixWidth(0.1) },
                     { title: '客户名称', field: 'CustName', width: TechTaskSubmit.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: TechTaskSubmit.fixWidth(0.1) },
                      { title: '状态', field: 'STATUS', width: TechTaskSubmit.fixWidth(0.1),hidden: 'true' },
                       { title: '状态描述', field: 'StatusMemo', width: TechTaskSubmit.fixWidth(0.1) },
                     { title: '工艺工程师', field: 'ProcessEngineer', width: TechTaskSubmit.fixWidth(0.1) },
                     { title: '编程工程师', field: 'ProgramEngineer', width: TechTaskSubmit.fixWidth(0.1) },
                     {
                         title: '工艺计划完成时间', field: 'PlanDate', formatter: function (value, row, index) {
                             if(value==null)return null;
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: TechTaskSubmit.fixWidth(0.1)
                     },
                      { title: '上传文件', field: 'ProcessFname', width: TechTaskSubmit.fixWidth(0.1) },
                     
                     { title: '操作人', field: 'UpdatedBy', width: TechTaskSubmit.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: TechTaskSubmit.fixWidth(0.1)
                     },
                ]],
                toolbar: [{
                    text: '零件图号<input type="text" id="txtPDNo"/>'
                }, 
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        TechTaskSubmit.bindGrid();
                    }
                }, 
                '-', {
                    id: 'btnEdit',
                    text: '提交任务',
                    iconCls: 'icon-edit',
                    handler: function () {
                        TechTaskSubmit.islook = false;
                        $('#btnSave').show();
                        $('#btnClear').show();
                        $('#btnClose').show();
                        $('#btnSaveRoute').show();
                        TechTaskSubmit.initGridRoute();
                        $('#restartDialog').panel({ title: "任务提交" });
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) { 
                            $('#selPartDrawingNo').combobox("setValue", rows[0].PARTSDRAWINGNO);
                            $('#txtCustName').val(rows[0].CustName);
                            $('#txtProductName').val(rows[0].ProductName);
                            $('#txtPlanTime').val(rows[0].PlanDate);//隐藏保存路由按钮 
                        }
                        $("#restartDialog").dialog('open');
                       
                       
                        
                        //var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        //if (rows.length == 1) {
                        //    $('#restartDialog').panel({ title: "任务提交" });                           
                        //    TechTaskSubmit.ShowEditOrViewDialog();//实现修改记录的方法
                        //}
                        //else {
                        //    JeffComm.alertErr("请选择一条数据进行任务提交");
                            
                        //}
                    }
                },
                '-', {
                    id: 'btnLook',
                    text: '查看',
                    iconCls: 'icon-tip',
                    handler: function () {
                      //  $("#restartDialog").dialog('open');
                        TechTaskSubmit.initGridRoute();
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            $('#restartDialog').panel({ title: "任务查看" });
                            $('#btnSave').hide();
                            $('#btnClear').hide();
                            $('#btnClose').hide();
                            $('#btnSaveRoute').hide();//隐藏保存路由按钮
                            TechTaskSubmit.islook = true;
                            TechTaskSubmit.ShowEditOrViewDialog();//实现修改记录的方法
                            
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行任务查看");

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
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "TechnologyTable.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    TechTaskSubmit.techtasksubmitID = rowData["ID"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 20,
                pageNumber: 1,
                pageList: [20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        },
        bindGridRoute: function () {
            TechTaskSubmit.partsdrawingno = $("#selPartDrawingNo").combobox("getValue");
            $('#gridRoute').datagrid('load', { PartsDrawingNo: TechTaskSubmit.partsdrawingno });
        },
        initGridRoute: function () {
            var editRow = undefined;
            $("#gridRoute").datagrid({
                //height: 300,
                //width: 450,
                title: '路由维护',
                collapsible: true,
                singleSelect: false,
                url: '../../services/WsSystem.asmx/QueryRouteDetails',
                idField: 'StationId',
                columns: [[
                    { field: 'ck', checkbox: true },
                    { title: 'ID', field: 'ID', width: TechTaskSubmit.fixWidth(0.1), hidden: 'true' },
                   
                     { title: '工序号', field: 'StationId', width: TechTaskSubmit.fixWidth(0.1), editor: { type: 'text', options: { required: true } } },
                     { title: '工序名称', field: 'StationName', width: TechTaskSubmit.fixWidth(0.1), editor: { type: 'text', options: { required: true } } },
                     { title: '机床类型', field: 'MachineType', width: TechTaskSubmit.fixWidth(0.3), editor: { type: 'text', options: { required: true } } }
                    //{
                    //    title: '机床类型', field: 'MactypeCode', width: TechTaskSubmit.fixWidth(0.1),
                    //    formatter: function (value, row) {
                    //        return row.MachineType;
                    //    },
                    //    editor: {
                    //        type: 'combobox',
                    //        options: {
                    //            valueField: 'MactypeCode',
                    //            textField: 'MachineType',
                    //            method: 'get',
                    //            url: '../../services/WsSystem.asmx/ListBindMachineTypeToRoute',
                    //            required: true
                    //        }
                    //    }
                    //}
                ]],
                toolbar: [{
                    text: '添加', iconCls: 'icon-add', handler: function () {
                        if (editRow != undefined) {
                            $("#gridRoute").datagrid('endEdit', editRow);
                        }
                        if (editRow == undefined) {
                            //var irows = $("#gridRoute").datagrid('getRows').length;
                           // irows = irows + 1;
                            $("#gridRoute").datagrid('insertRow', {
                                //index:0,// irows,//0,
                                row: {}
                            });

                            $("#gridRoute").datagrid('beginEdit', 0);
                            editRow = 0;
                        }
                    }
                }, '-', {
                    id: 'btnConfirm',
                    text: '确定', iconCls: 'icon-ok', handler: function () {
                        $("#gridRoute").datagrid('endEdit', editRow);
                    }
                }, '-', {
                    id: 'btnSaveRoute',
                    text: '保存路由', iconCls: 'icon-save', handler: function () {
                        $("#gridRoute").datagrid('endEdit', editRow);

                        //如果调用acceptChanges(),使用getChanges()则获取不到编辑和新增的数据。

                        //使用JSON序列化datarow对象，发送到后台。
                       // var rows = $("#gridRoute").datagrid('getChanges');
                        var rows = $("#gridRoute").datagrid("getRows");
                        var rowstr = JSON.stringify(rows);
                        if(TechTaskSubmit.partsdrawingno==null||TechTaskSubmit.partsdrawingno=="")
                        {

                            JeffComm.alertErr("请选择零件图号");
                            return;
                        }
                        WsSystem.SaveRouteInfo(TechTaskSubmit.partsdrawingno, rowstr,
                     function (result) {
                         if(result=="OK")
                         {
                             //JeffComm.succAlert("保存成功");
                             alert("保存成功");
                         }
                         else
                         {
                             JeffComm.alertErr(result);
                         }
                     });
                       
                        //$.post('/Home/Create', rowstr, function (data) {

                        //});
                    }
                  },
                 //   , '-', {
                //    text: '撤销', iconCls: 'icon-redo', handler: function () {
                //        editRow = undefined;
                //        $("#gridRoute").datagrid('rejectChanges');
                //        $("#gridRoute").datagrid('unselectAll');
                //    }
                    //},
                    '-', {
                    text: '删除', iconCls: 'icon-remove', handler: function () {
                        //var row = $("#gridRoute").datagrid('getSelections');
                        //var index = $("#gridRoute").datagrid('getRowIndex', row);
                        //if (row == undefined) { return }
                        //$('#gridRoute').datagrid('cancelEdit', index)
                        //        .datagrid('deleteRow', index);
                        //editRow = undefined;
                        var row = $('#gridRoute').datagrid('getChecked');
                        if (row.length > 1) {
                            for (var i = 0; i < row.length; i++) {
                                var index = $("#gridRoute").datagrid('getRowIndex', row[i]);
                                if (row[i] == undefined) { return }
                                $('#gridRoute').datagrid('cancelEdit', index)
                                        .datagrid('deleteRow', index);
                            }
                        } else if (row.length == 1) {
                            var index = $("#gridRoute").datagrid('getRowIndex', row[0]);
                            if (row == undefined) { return }
                            if (index == -1) {
                                index = 0;
                            }
                            $('#gridRoute').datagrid('cancelEdit', index)
                                    .datagrid('deleteRow', index);
                        }
                        editRow = undefined;
                    }
                }, '-', {
                    text: '修改', iconCls: 'icon-edit', handler: function () {
                       // var row = $("#gridRoute").datagrid('getSelected');
                        var row = $('#gridRoute').datagrid('getChecked');
                        if (row != null) {
                            if (editRow != undefined) {
                                $("#gridRoute").datagrid('endEdit', editRow);
                            }

                            if (editRow == undefined) {
                                var index = $("#gridRoute").datagrid('getRowIndex', row);
                                $("#gridRoute").datagrid('beginEdit', index);
                                editRow = index;
                                $("#gridRoute").datagrid('unselectAll');
                            }
                        } else {

                        }
                    }
                }, '-', {
                    text: '上移', iconCls: 'icon-up', handler: function () {
                        TechTaskSubmit.MoveUp();
                    }
                }, '-', {
                    text: '下移', iconCls: 'icon-down', handler: function () {
                        TechTaskSubmit.MoveDown();
                    }
                }],
                onAfterEdit: function (rowIndex, rowData, changes) {
                    editRow = undefined;                    
                },
                onEndEdit:function (index, row){
                    var ed = $(this).datagrid('getEditor', {
                        index: index,
                        field: 'StationId'
                    });
                     
                   // row.MactypeCode = $(ed.target).combobox('getText');
                },
                onDblClickRow: function (rowIndex, rowData) {
                    if (editRow != undefined) {
                        $("#gridRoute").datagrid('endEdit', editRow);
                    }

                    if (editRow == undefined) {
                        $("#gridRoute").datagrid('beginEdit', rowIndex);
                        editRow = rowIndex;
                    }
                },
                onClickRow: function (rowIndex, rowData) {
                    if (editRow != undefined) {
                        $("#gridRoute").datagrid('endEdit', editRow);

                    }

                }

            });
        },
        MoveUp:function (){
            var row = $("#gridRoute").datagrid('getSelected');
            var index = $("#gridRoute").datagrid('getRowIndex', row);
            TechTaskSubmit.mysort(index, 'up', 'gridRoute');
     
        },
        //下移
        MoveDown:function () {
            var row = $("#gridRoute").datagrid('getSelected');
            var index = $("#gridRoute").datagrid('getRowIndex', row);
            TechTaskSubmit.mysort(index, 'down', 'gridRoute');
     
        },
        mysort:function(index, type, gridname) {
            if ("up" == type) {
                if (index != 0) {
                    var toup = $('#' + gridname).datagrid('getData').rows[index];
                    var todown = $('#' + gridname).datagrid('getData').rows[index - 1];
                    $('#' + gridname).datagrid('getData').rows[index] = todown;
                    $('#' + gridname).datagrid('getData').rows[index - 1] = toup;
                    $('#' + gridname).datagrid('refreshRow', index);
                    $('#' + gridname).datagrid('refreshRow', index - 1);
                    $('#' + gridname).datagrid('selectRow', index - 1);
                }
            } else if ("down" == type) {
                var rows = $('#' + gridname).datagrid('getRows').length;
                if (index != rows - 1) {
                    var todown = $('#' + gridname).datagrid('getData').rows[index];
                    var toup = $('#' + gridname).datagrid('getData').rows[index + 1];
                    $('#' + gridname).datagrid('getData').rows[index + 1] = todown;
                    $('#' + gridname).datagrid('getData').rows[index] = toup;
                    $('#' + gridname).datagrid('refreshRow', index);
                    $('#' + gridname).datagrid('refreshRow', index + 1);
                    $('#' + gridname).datagrid('selectRow', index + 1);
                }
            }
 
        }

    };
}();