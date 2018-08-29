var DevlopTaskSubmit = function () {
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
           // $('#txtPlanTime').attr("disabled", "disabled");
            $('#txtuploadpath').attr("disabled", "disabled");
            $('#txtknifeuploadpath').attr("disabled", "disabled");
            //初始化零件图号             
            $('#selPartDrawingNo').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListPartDrawingNoToDevlopEasyUI',
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
                    if (!DevlopTaskSubmit.islook) {
                        var selPD = $("#selPartDrawingNo").combobox("getText");// $("#selEquitmentType").find("option:selected").text();
                        selPD = $("#selPartDrawingNo").combobox("getValue");
                        WsSystem.QueryPDNoInfo(selPD,
                         function (result) {
                             if (result != "") {
                                 var vres = new Array();
                                 vres = result.split(",");
                                 if (vres[3] == "4" || vres[3] == "10") {
                                     $('#txtCustName').val(vres[0]);
                                     $('#txtProductName').val(vres[1]);
                                    // $('#txtPlanTime').val(vres[2]);
                                    // DevlopTaskSubmit.bindGridRoute();//绑定路由信息
                                     DevlopTaskSubmit.taskstatus = "P";
                                 }
                                 else {
                                     DevlopTaskSubmit.taskstatus = "F";
                                     JeffComm.alertErr("该图号现在是" + vres[4] + "状态，禁止修改提交");
                                 }
                             }
                         });
                        //初始化工序
                        $('#selStations').combobox({
                            //prompt: '输入首关键字自动检索',
                            required: false,
                            url: '../../services/WsSystem.asmx/QueryStationsByPDNo',
                            queryParams: { PartsDrawingNo: selPD },
                            valueField: 'id',
                            textField: 'text',
                            editable: true,
                           // multiple: true,
                            panelHeight: 'auto',
                            hasDownArrow: true,
                            filter: function (q, row) {
                                var opts = $(this).combobox('options');
                                return row[opts.textField].indexOf(q) == 0;
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
            $("#knifeuploadify").uploadify({
                //指定swf文件
                'swf': '../../Scripts/uploadify/uploadify.swf',
                //后台处理的页面
                'uploader': '../../services/UploadHandler.ashx',
                //按钮显示的文字
                'buttonText': '上传文件',                
                //上传文件的类型  默认为所有文件    'Image/All Files'  ;  '*.*'
                //在浏览窗口底部的文件类型下拉菜单中显示的文本
                'fileTypeDesc': 'All Files',                 
                //选择文件后自动上传
                'auto': true,
                //设置为true将允许多文件上传
                'multi': true,
                //上传成功后执行
                'onUploadSuccess': function (file, data, response) {
                    $("#txtknifeuploadpath").val(data);
                }
            });
            jQuery('#btnClose').click(function () {
                DevlopTaskSubmit.techtasksubmitID = null;
                $('#txtCustName').val("");
                $('#txtProductName').val("");
                $('#txtknifeuploadpath').val("");
                $('#txtuploadpath').val("");
                $('#txtUnitTime').val("");
                $('#restartDialog').window('close');
            });
            jQuery('#btnClear').click(function () {
                DevlopTaskSubmit.techtasksubmitID = null;
                $('#txtCustName').val("");
                $('#txtProductName').val("");
                $('#txtknifeuploadpath').val("");
                $('#txtuploadpath').val("");
                $('#txtUnitTime').val("");
            });
           
            jQuery('#btnSaveStation').click(function () {
                if (DevlopTaskSubmit.taskstatus == "F" || DevlopTaskSubmit.taskstatus == null)
                {
                    JeffComm.alertErr("禁止修改和提交");
                    return;
                }
                var tempinfo = {
                    PARTSDRAWINGNO: $("#selPartDrawingNo").combobox("getValue"),
                    CustName: $("#txtCustName").val(),
                    ProductName: $("#txtProductName").val(),
                    ProcessNo: $("#selStations").combobox("getValue"),
                    ProcessName: $("#selStations").combobox("getText"),
                    UnitTime: $("#txtUnitTime").val(),
                    ProgramPath: $("#txtuploadpath").val(),
                    ToolPath: $("#txtknifeuploadpath").val()
                };
                WsSystem.SaveStationInfo(tempinfo,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);                             
                              $('#txtknifeuploadpath').val("");
                              $('#txtuploadpath').val("");
                              $('#txtUnitTime').val("");
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
            //提交任务
            jQuery('#btnSave').click(function () {
                if (DevlopTaskSubmit.taskstatus == "F" || DevlopTaskSubmit.taskstatus == null) {
                    JeffComm.alertErr("禁止修改和提交");
                    return;
                }
                var tempinfo = {
                    PARTSDRAWINGNO: $("#selPartDrawingNo").combobox("getValue")                    
                };
                WsSystem.SaveDevelopInfo(tempinfo,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                              $('#txtCustName').val("");
                              $('#txtProductName').val("");
                              $('#txtknifeuploadpath').val("");
                              $('#txtuploadpath').val("");
                              $('#txtUnitTime').val("");
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
            // DevlopTaskSubmit.partsdrawingno = $("#selPartsdrawingno").val();
            $('#grid').datagrid('load', {Status:"4,10" ,PartsDrawingNo:$("#txtPDNo").val()});
        },
       
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            //DevlopTaskSubmit.techtasksubmitID = rows[0].ID;
            //DevlopTaskSubmit.taskstatus = rows[0].STATUS;             
            $('#selPartDrawingNo').combobox('setValues', rows[0].PARTSDRAWINGNO)
            $('#txtCustName').val(rows[0].CustName);
            $('#txtProductName').val(rows[0].ProductName);
            $('#txtPlanTime').val(rows[0].PlanDate);
            //DevlopTaskSubmit.bindGridRoute();//绑定路由信息 
            $('#gridRoute').datagrid('load', { PartsDrawingNo: rows[0].PARTSDRAWINGNO });
            //WsSystem.QueryPDNoInfo(rows[0].PARTSDRAWINGNO,
            //         function (result) {
            //             if (result != "") {
            //                 var vres = new Array();
            //                 vres = result.split(",");                             
            //                $('#txtCustName').val(vres[0]);
            //                $('#txtProductName').val(vres[1]);
            //                $('#txtPlanTime').val(vres[2]);
            //                DevlopTaskSubmit.bindGridRoute();//绑定路由信息                                                         
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
                url: '../../services/WsSystem.asmx/QueryDevlopEngineerTask',
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
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: DevlopTaskSubmit.fixWidth(0.1) },
                     { title: '客户名称', field: 'CustName', width: DevlopTaskSubmit.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: DevlopTaskSubmit.fixWidth(0.1) },
                      { title: '状态', field: 'STATUS', width: DevlopTaskSubmit.fixWidth(0.1),hidden: 'true' },
                       { title: '状态描述', field: 'StatusMemo', width: DevlopTaskSubmit.fixWidth(0.1) },
                     { title: '工艺工程师', field: 'ProcessEngineer', width: DevlopTaskSubmit.fixWidth(0.1) },
                     { title: '编程工程师', field: 'ProgramEngineer', width: DevlopTaskSubmit.fixWidth(0.1) },
                     {
                         title: '编程计划完成时间', field: 'DevplanDate', formatter: function (value, row, index) {
                             if(value==null)return null;
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: DevlopTaskSubmit.fixWidth(0.1)
                     },
                      { title: '工艺文件路径', field: 'ProcessPath', width: DevlopTaskSubmit.fixWidth(0.06), hidden: 'true' },
                      {
                          title: '工艺文件', field: 'ProcessFname', width: DevlopTaskSubmit.fixWidth(0.1),
                          formatter: function (value, row, index) {
                              return '<a style="color:blue" href="' + row.ProcessPath + '", target="_blank">' + row.ProcessFname + '</a>';
                             // alert(row.ProcessPath);
                          }
                      },
                     
                     { title: '操作人', field: 'UpdatedBy', width: DevlopTaskSubmit.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: DevlopTaskSubmit.fixWidth(0.1)
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
                        DevlopTaskSubmit.bindGrid();
                    }
                }, 
                '-', {
                    id: 'btnEdit',
                    text: '提交任务',
                    iconCls: 'icon-edit',
                    handler: function () {
                        DevlopTaskSubmit.islook = false;
                        $('#btnSave').show();
                        $('#btnClear').show();
                        $('#btnClose').show();
                        $('#btnSaveStation').show();
                        //DevlopTaskSubmit.initGridRoute();
                        $('#restartDialog').panel({ title: "任务提交" });
                        $("#restartDialog").dialog('open'); 
                    }
                },
                //'-', {
                //    id: 'btnLook',
                //    text: '查看',
                //    iconCls: 'icon-tip',
                //    handler: function () {
                //      //  $("#restartDialog").dialog('open');
                //       // DevlopTaskSubmit.initGridRoute();
                //        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                //        if (rows.length == 1) {
                //            $('#restartDialog').panel({ title: "任务查看" });
                //            $('#btnSave').hide();
                //            $('#btnClear').hide();
                //            $('#btnClose').hide();
                //            $('#btnSaveStation').hide();//隐藏保存路由按钮
                //            DevlopTaskSubmit.islook = true;
                //            DevlopTaskSubmit.ShowEditOrViewDialog();//实现修改记录的方法
                            
                //        }
                //        else {
                //            JeffComm.alertErr("请选择一条数据进行任务查看");

                //        }
                        
                //    }
                //},
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
                   // DevlopTaskSubmit.techtasksubmitID = rowData["ID"];
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