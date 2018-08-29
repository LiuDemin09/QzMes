var AllocateTask = function () {
    return {       
        grid: null,
        allocatetaskID: null,
        partsdrawingno: null,
        taskstatus:null,
        //main function to initiate the module
        init: function () {
            $('#txtPartDrawingNo').attr("disabled", "disabled");
            $('#txtCustName').attr("disabled", "disabled");
            $('#txtProductName').attr("disabled", "disabled");

            //初始化工艺工程师             
            $('#selTechnologyEngineer').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListUserByRoleEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                }                
            });
             //初始化编程工程师
            $('#selDevelopmentEngineer').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListUserByRoleEasyUI',
                valueField: 'id',
                textField: 'text',
                editable: true,
                hasDownArrow: true,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) == 0;
                }
            });
            jQuery('#btnClose').click(function () {
                AllocateTask.allocatetaskID = null;
                $('#txtTechFinishTime').val("");
                $('#txtDevFinishTime').val("");
                $('#txtProductName').val("");
                $('#txtPartDrawingNo').val("");
                $("#txtCustName").val("");
                $('#restartDialog').window('close');
            });
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/TechnologyManage/AllocateTaskDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/TechnologyManage/AllocateTaskUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
            });
            //jQuery('#btnNew').click(function () {
            //    AllocateTask.allocatetaskID = null;
            //    WsSystem.GetBasBaseCode(function (result) {
            //        $('#txtBaseInfoNo').val(result);
            //        $('#txtBaseInfoName').val("");
            //       // $('#txtSubBaseInfoNo').val("");
            //        //$('#txtSubBaseInfoName').val("");
            //        $('#txtDesc').val("");
            //    });
            //});
            //jQuery('#btnNewNotCode').click(function () {
            //    AllocateTask.allocatetaskID = null;
            //    $('#txtBaseInfoNo').attr("disabled", false);
            //    $('#txtSubBaseInfoNo').val("");
            //    $('#txtSubBaseInfoName').val("");
            //    $('#txtDesc').val("");
            //});
            jQuery('#btnSave').click(function () {
                if ($('#txtTechFinishTime').val().trim() == null || $('#txtTechFinishTime').val().trim() == "")
                {
                    JeffComm.alertErr("工艺要求完成时间不能为空");
                   // alert("信息编号不能为空");
                    return;
                }
                var status;
                var statusmemo;
                if (AllocateTask.taskstatus == "0")
                {
                    status = 1;
                    statusmemo="分派工艺";
                }
                if (AllocateTask.taskstatus=="3")
                {
                    status = 4;
                    statusmemo = "分派编程";
                }

                //var baseinfo = {
                //    ID:AllocateTask.allocatetaskID,
                //    CODE: $("#txtBaseInfoNo").val().trim(),
                //    NAME: $("#txtBaseInfoName").val().trim(),
                //    SubCode: $("#txtSubBaseInfoNo").val().trim(),
                //    SubName: $("#txtSubBaseInfoName").val().trim(),
                //    MEMO: $("#txtDesc").val(),
                //};
                var tempinfo = {
                    ID: AllocateTask.allocatetaskID,
                    PARTSDRAWINGNO: $("#txtPartDrawingNo").val(),
                    CustName: $("#txtCustName").val(),
                    ProductName: $("#txtProductName").val(),
                    ProcessEngineer: $("#selTechnologyEngineer").combobox("getValue"),
                    ProcessName: $("#selTechnologyEngineer").combobox("getText"),
                    PlanDate: $("#txtTechFinishTime").val(),
                    ProgramEngineer: $("#selDevelopmentEngineer").combobox("getValue"),
                    ProgramName: $("#selDevelopmentEngineer").combobox("getText"),
                    DevplanDate: $("#txtDevFinishTime").val(),
                    STATUS:status,
                    StatusMemo: statusmemo
                };
                WsSystem.SaveTechnologyInfo(tempinfo, 
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                                //alert("保存成功", "提示");
                              $('#txtTechFinishTime').val("");
                              $('#txtDevFinishTime').val("");
                              $('#txtProductName').val("");
                              $('#txtPartDrawingNo').val("");
                              $("#txtCustName").val("");
                              $('#restartDialog').window('close');
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
            AllocateTask.partsdrawingno = $("#selPartsdrawingno").val();
            $('#grid').datagrid('load', { PartsDrawingNo: AllocateTask.partsdrawingno });
        },
        delCustInfo: function () {
            if (AllocateTask.allocatetaskID == null || AllocateTask.allocatetaskID == "") {
                JeffComm.alertErr("请选择一条数据进行删除"); 
            } else {
                WsSystem.RemoveTechnologyInfo(AllocateTask.allocatetaskID,
                    function (result) {
                        if (result == null) {
                            AllocateTask.allocatetaskID = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            AllocateTask.partsdrawingno = $("#selPartsdrawingno").val();
                            $('#grid').datagrid('load', { PartsDrawingNo: AllocateTask.partsdrawingno });
                        } else {
                            alert(result, "提示");
                            AllocateTask.partsdrawingno = $("#selBaseName").val();
                            $('#grid').datagrid('load', { PartsDrawingNo: AllocateTask.partsdrawingno });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            AllocateTask.partsdrawingno = $("#selPartsdrawingno").val();
            $('#grid').datagrid('load', { PartsDrawingNo: AllocateTask.partsdrawingno });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            if (rows.length > 0) {
                AllocateTask.allocatetaskID = rows[0].ID;
                AllocateTask.taskstatus = rows[0].STATUS;
                $('#txtPartDrawingNo').val(rows[0].PARTSDRAWINGNO);
                $('#txtCustName').val(rows[0].CustName);
                $('#txtProductName').val(rows[0].ProductName);
                if (rows[0].STATUS == 0) {
                    $('#selDevelopmentEngineer').combobox('disable');
                    // $('#selDevelopmentEngineer').attr("disabled", "disabled");
                    $('#txtDevFinishTime').attr("disabled", "disabled");
                    $('#selTechnologyEngineer').combobox('enable');
                    $('#txtTechFinishTime').attr("disabled", false);
                }
                else {
                    $('#selTechnologyEngineer').combobox('disable');
                    $('#txtTechFinishTime').attr("disabled", "disabled");
                    $('#selDevelopmentEngineer').combobox('enable');
                    $('#txtDevFinishTime').attr("disabled", false);
                }
                if (rows[0].ProcessEngineer != null) {
                    $('#selTechnologyEngineer').combobox('setValues', rows[0].ProcessEngineer);
                }
                if (rows[0].ProgramEngineer != null) {
                    $('#selDevelopmentEngineer').combobox('setValues', rows[0].ProgramEngineer);
                }
                if (rows[0].PlanDate != null) {
                    $("#txtTechFinishTime").val(rows[0].PlanDate);
                }
            }
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '分派任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryTechnologyTask',
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
                     { title: 'ID', field: 'ID', width: AllocateTask.fixWidth(0.1),hidden: 'true' },
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: AllocateTask.fixWidth(0.1) },
                     { title: '客户名称', field: 'CustName', width: AllocateTask.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: AllocateTask.fixWidth(0.1) },
                      { title: '状态', field: 'STATUS', width: AllocateTask.fixWidth(0.1),hidden: 'true' },
                       { title: '状态描述', field: 'StatusMemo', width: AllocateTask.fixWidth(0.1) },
                     { title: '工艺工程师', field: 'ProcessEngineer', width: AllocateTask.fixWidth(0.1) },
                     { title: '编程工程师', field: 'ProgramEngineer', width: AllocateTask.fixWidth(0.1) },
                     {
                         title: '工艺计划完成时间', field: 'PlanDate', formatter: function (value, row, index) {
                             if(value==null)return null;
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: AllocateTask.fixWidth(0.1)
                     },
                     {
                         title: '编程计划完成时间', field: 'DevplanDate', formatter: function (value, row, index) {
                              if(value==null)return null;
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: AllocateTask.fixWidth(0.1)
                     },
                     
                     { title: '操作人', field: 'UpdatedBy', width: AllocateTask.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: AllocateTask.fixWidth(0.1)
                     },
                ]],
                toolbar: [{
                    text: '零件图号<input type="text" id="selPartsdrawingno"/>'
                }, 
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        AllocateTask.bindGrid();
                    }
                }, 
                '-', {
                    id: 'btnEdit',
                    text: '分派',
                    iconCls: 'icon-edit',
                    handler: function () {
                        //var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        //if (rows.length == 1) {
                            $('#restartDialog').panel({ title: "分派任务" });
                            //$('#btnDownload').hide();
                            //$('#btnImport').hide();
                            //$('#btnNew').hide();
                           // $('#btnNewNotCode').hide();
                            AllocateTask.ShowEditOrViewDialog();//实现修改记录的方法
                        //}
                        //else {
                        //    JeffComm.alertErr("请选择一条数据进行修改");
                        //    //alert("请选择一条数据进行修改", "提示");
                        //}
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        AllocateTask.delCustInfo();//实现直接删除数据的方法
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
                    id: 'btnExport',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/TechnologyManage/export/AllocateTaskExport.aspx?partcode='
                            + $('#selPartsdrawingno').val()
                            // + '&partcode=' + $('#txtQPartCode').val()
                         + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');

                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "TechBossTable.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    AllocateTask.allocatetaskID = rowData["ID"];
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
        }
    };
}();