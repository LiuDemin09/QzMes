var CreateWorkOrder = function () {
    return {
        grid: null,
        workorderNo: null,
        status: null,
        partNo: null,
        custName: null,
        worker: null,
        ID:null,
        //main function to initiate the module
        init: function () {
            $('#txtProductName').attr("disabled", "disabled");
            $('#txtOrderQty').attr("disabled", "disabled");
            $('#txtAskTime').attr("disabled", "disabled");
           // $('#txtWorkOrderNo').attr("disabled", "disabled");
            $('#txtMaterialQty').attr("disabled", "disabled");
            $('#txtProgramStatus').attr("disabled", "disabled");
           
           // $('#txtUnitTime').attr("disabled", "disabled");
            //初始化零件图号
            WsSystem.ListBindPartsDrawingCodeBindOrder(function (result) {
                JeffComm.fillSelect($("#selPartsDrawing"), result, true);
            });
            
            //初始化订单单号
            //WsSystem.ListBindOrderNo(function (result) {
            //    JeffComm.fillSelect($("#selOrderNo"), result, true);
            //});
            jQuery('#btnQuery').click(function () {
                $("#restartDialog1").dialog('open');
            });
            jQuery('#btnTechnologyQuery').click(function () {
                window.open("../TechnologyManage/TechnologyQueryToWO.aspx");
            });
            jQuery('#btnClose').click(function () {
                CreateWorkOrder.workorderNo = null;
                $('#txtWorkOrderNo').val("");
                $('#txtProductName').val("");
                $('#txtBatchNumber').val("");
                $('#txtPlanQuantity').val("");
                $('#txtStartTime').val("");
                $('#txtEndTime').val("");
                $('#txtPlanCheckTime').val("");
                $('#txtPlanInTime').val("");
                $('#txtQualityCode').val("");
                //$('#selEquitmentType').combobox('setValues', "")
                //$('#selEquipment').combobox('setValues', "")
                $('#selOrderNo').val("");
                $('#selPartsDrawing').val("");

                $('#restartDialog').window('close');
            });
            jQuery('#btnNew').click(function () {
                CreateWorkOrder.workorderNo = null;
              
                //初始化订单单号
                
                WsSystem.GetWorkOrderCode(function (result) {
                    $('#txtWorkOrderNo').val(result);
                    //$('#txtProductName').val("");
                    //$('#txtBatchNumber').val("");
                    //$('#txtPlanQuantity').val("");
                    //$('#txtStartTime').val("");
                    //$('#txtEndTime').val("");
                    //$('#txtPlanCheckTime').val("");
                    //$('#txtPlanInTime').val("");
                    //$('#txtQualityCode').val("");
                    //$('#selEquitmentType').combobox('setValues', "")
                    //$('#selEquipment').combobox('setValues', "")
                    //$('#selOrderNo').val("");
                    //$('#selPartsDrawing').val("");
                    //$('#txtBaseQty').val("1");
                });
            });
            //当计划生产数量输入完后，开始计算结束时间
            $("#txtPlanQuantity").keyup(function () {
                if (event.keyCode == 13) {
                    if ($("#txtStartTime").val() == "") {
                        JeffComm.alertErr("计划开始时间不能为空"); 
                        return;
                    }
                    if ($("#txtUnitTime").val() == "") {
                        JeffComm.alertErr("生产工时不能为空");
                        return;
                    }
                    if ($("#txtPlanQuantity").val() == "") {
                        JeffComm.alertErr("计划生产数量不能为空");
                        return;
                    }
                    if ($("#selBanci").val() == "") {
                        JeffComm.alertErr("生产班次不能为空");
                        return;
                    }
                    WsSystem.GetPlanEndTime($("#txtStartTime").val(),$("#selBanci").val(),$("#txtUnitTime").val(),$("#txtPlanQuantity").val(),function (result) {
                        $('#txtEndTime').val(result);
                    });
                }
                return false;
            });
            jQuery('#btnSave').click(function () { 
                var selectedEquipmentType = $("#selEquitmentType").combobox("getText");
                var selectedEquipment = $("#selEquipment").combobox("getText");
                var selectedBanci = $("#selBanci").combobox("getText");
                var selectedStationname = $("#selStationName").combobox("getText");
                if ($("#txtWorkOrderNo").val() == "") {
                    JeffComm.alertErr("工单号不能为空"); 
                    return;
                }
                if (selectedStationname == "") {
                    JeffComm.alertErr("工序不能为空"); 
                    return;
                }
                if ($("#selOrderNo").val() == "") {
                    JeffComm.alertErr("订单号不能为空");
                    //alert("订单号不能为空", "提示");
                    return;
                }
                if ($("#selPartsDrawing").val() == "") {
                    JeffComm.alertErr("图号不能为空");
                    //alert("图号不能为空", "提示");
                    return;
                }
                
                if ($("#txtProductName").val() == "") {
                    JeffComm.alertErr("产品名称不能为空");
                    //alert("产品名称不能为空", "提示");
                    return;
                }
                if ($("#txtStartTime").val() == "") {
                    JeffComm.alertErr("计划开始时间不能为空");
                    //alert("计划开始时间不能为空", "提示");
                    return;
                }
                if ($("#txtEndTime").val() == "") {
                    JeffComm.alertErr("计划结束时间不能为空");
                    //alert("计划结束时间不能为空", "提示");
                    return;
                }
                if ($("#txtQualityCode").val() == "") {
                    JeffComm.alertErr("质量编号不能为空");
                    //alert("计划结束时间不能为空", "提示");
                    return;
                }
                if ($("#txtBatchNumber").val() == "") {
                    JeffComm.alertErr("炉/批号不能为空");
                    //alert("计划结束时间不能为空", "提示");
                    return;
                }
                var workorderinfo = {
                    WO: $("#txtWorkOrderNo").val().trim(),
                    OrderNumber: $("#selOrderNo").val(),
                    PartsdrawingCode: $("#selPartsDrawing").val(),
                    MachineType: selectedEquipmentType,
                    MachineName: selectedEquipment,
                    ProductName: $("#txtProductName").val().trim(),
                    ProductCode: $("#txtProductCode").val().trim(),
                    StartTime: $("#txtStartTime").val().trim(),
                    EndTime: $("#txtEndTime").val().trim(),
                    BatchNumber: $("#txtBatchNumber").val().trim(),
                    PlanQuantity: $("#txtPlanQuantity").val().trim(),
                    CheckTime: $("#txtPlanCheckTime").val().trim(),
                    InStockTime: $("#txtPlanInTime").val().trim(),
                    CustName: $("#txtCustName").val().trim(),
                    BaseQty: $('#txtBaseQty').val(),
                    QualityCode: $('#txtQualityCode').val(),
                    UnitTime: $('#txtUnitTime').val(),
                    CLASS: selectedBanci,
                    RouteCode: $('#txtStationId').val(),//工序ID
                    StationName:selectedStationname
                };
                WsSystem.SaveWorkOrderInfo(workorderinfo,
                      function (result) {
                          if (result == "OK") {
                             // JeffComm.alertSucc("保存成功", 500);

                              alert("保存成功", "提示");
                             // $('#txtWorkOrderNo').val("");
                              //$('#txtProductName').val("");
                              //$('#txtBatchNumber').val("");
                              //$('#txtPlanQuantity').val("");
                              //$('#txtStartTime').val("");
                              //$('#txtEndTime').val("");
                              //$('#txtPlanCheckTime').val("");
                              //$('#txtPlanInTime').val("");
                              //$('#txtQualityCode').val("");
                              //$('#txtUnitTime').val("");
                              //$('#selEquitmentType').combobox('setValues', "")
                              //$('#selEquipment').combobox('setValues', "")
                              //$('#selOrderNo').val("");
                              //$('#selPartsDrawing').val("");
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败" + err.get_message(), "提示");

                      });
            });
            jQuery('#btnClear').click(function () {
                $('#txtWorkOrderNo').val("");
                $('#txtProductName').val("");
                $('#txtBatchNumber').val("");
                $('#txtPlanQuantity').val("");
                $('#txtStartTime').val("");
                $('#txtEndTime').val("");
                $('#txtPlanCheckTime').val("");
                $('#txtPlanInTime').val("");
                $('#txtQualityCode').val("");
                $('#txtUnitTime').val("");
                //$('#selEquitmentType').combobox('setValues', "")
                //$('#selEquipment').combobox('setValues', "")
                $('#selOrderNo').val("");
                $('#selPartsDrawing').val("");
            });
            $("#selOrderNo").change(function () {
                //WsSystem.ListBindPartsDrawingNobyOrder($('#selOrderNo').val(),
                //function (result) {
                //    JeffComm.fillSelect($("#selPartsDrawing"), result, true);
                //});
                WsSystem.QueryProductNameByOrder($('#selOrderNo').val(),$('#selPartsDrawing').val(),
                 function (result) {
                     // alert(result);
                     var vres = result.split('^');
                     $('#txtProductName').val(vres[0]);
                     $('#txtProductCode').val(vres[1]);
                     $('#txtCustName').val(vres[2]);
                     $('#txtBatchNumber').val(vres[3]);
                     $('#txtOrderQty').val(vres[4]);
                     $('#txtAskTime').val(vres[5]);
                 });
            });
            $("#selPartsDrawing").change(function () {
                WsSystem.ListBindOrderbyPartsDrawingCode($('#selPartsDrawing').val(),
                function (result) {
                    JeffComm.fillSelect($("#selOrderNo"), result, true);
                });
                WsSystem.GetUnitTimebyPartsDrawingCode($('#selPartsDrawing').val(),
                 function (result) {
                     // alert(result);
                    // var vres = result.split('^');
                     $('#txtUnitTime').val(result);                     
                 });
                WsWareHouse.QueryInOutMaterialQty($('#selPartsDrawing').val(),
                function (result) { 
                    $('#txtMaterialQty').val(result);
                });
                WsSystem.QueryTechnologyStatus($('#selPartsDrawing').val(),
               function (result) {
                   $('#txtTechnologyFile').val(result);
               });
                WsSystem.QueryMachineTypeByDrawingNo($('#selPartsDrawing').val(),"",
              function (result) {
                  $('#selEquitmentType').combobox('setValues', result);
              });
                //查询工序号
                $('#selStationName').combobox({
                        //prompt: '输入首关键字自动检索',
                        required: false,
                        url: '../../services/WsSystem.asmx/ListStationNamesEasyUI',
                        queryParams: { partsdrawingno: $('#selPartsDrawing').val() },
                        valueField: 'id',
                        textField: 'text',
                        editable: true,
                        multiple: false,
                        panelHeight: 'auto',
                        hasDownArrow: true,
                        filter: function (q, row) {
                            var opts = $(this).combobox('options'); 
                            return row[opts.textField].indexOf(q) == 0;
                        } 
                    });
                });
            //    $.ajax({
            //        url:'../../services/WsSystem.asmx/ListStationNamesEasyUI',
            //        type:"post",
            //        datatype:"json",
            //        success:function(data){
            //            $('#selStationName').combobox({ 
            //                data:data.rows,
            //                valueField:'id', 
            //                textField:'text',
            //                onLoadSuccess: function () { //加载完成后,val[0]写死设置选中第一项
            //                    var val = $(this).combobox("getData");
            //                    for (var item in val[0]) {
            //                        if (item == "id") {
            //                            $(this).combobox("select", val[0][item]);
            //                        }
            //                    }
            //                }
            //            });        
            //        }
            //    });
            //});
            $('#selStationName').combobox({
                onChange: function (n, o) {
                    var stationid = $('#selStationName').combobox("getValue");
                    $('#txtStationId').val(stationid);
                    WsSystem.QueryMachineTypeByDrawingNo($('#selPartsDrawing').val(),stationid,
             function (result) {
                 $('#selEquitmentType').combobox('setValues', result);
             });
                }
            });
            $("#selEquitmentType").combobox({
                onChange: function (n, o) {
                    var a = "";
                    for (var i = 0; i < n.length; i++)
                    {
                        a += n[i] + ",";
                    }
                    $('#selEquipment').combobox({
                        //prompt: '输入首关键字自动检索',
                        required: false,
                        url: '../../services/WsSystem.asmx/QueryMachineByType',
                        queryParams: { machines: a},
                        valueField: 'id',
                        textField: 'text',
                    editable: true,
                    multiple: true,
                    panelHeight: 'auto',
                        hasDownArrow: true,
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) == 0;
                        }

                    });
                    //var params = { machines:a,ma:"342" }
                    //var url = "../../services/WsSystem.asmx/QueryMachineByType";//ListBindMachineInfo"
                    //$.post(url, params, LoadData, "json");
                    //function LoadData(data) {
                    //    $('#selEquipment').combobox({
                    //        valueField: 'id',
                    //        textField: 'text',
                    //        multiple: true,
                    //        panelHeight: 'auto',
                    //        onLoadSuccess: function () {//设置默认值  
                    //        }
                    //    });
                    //}
                    //var selPD = $("#selEquitmentType").combobox("getText");// $("#selEquitmentType").find("option:selected").text();
                    //$("#selEquipment").combobox({
                    //    url:'../../services/WsSystem.asmx/ListBindMachineInfo',
                    //    method:'get',
                    //    valueField:'id',
                    //    textField:'text',
                    //    multiple:true,
                    //    panelHeight:'auto'                        
                    //});

                }

            });
            
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/WorkOrderManage/WorkOrderDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/WorkOrderManage/WorkOrderUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
            });
        },
        bindGrid: function () {
            CreateWorkOrder.workorderNo = $('#txtWorkOrder').val().trim();
            CreateWorkOrder.status = $('#selWOStatus2').val().trim();
            CreateWorkOrder.partNo = $('#txtPartsDrawingNo2').val().trim();
            CreateWorkOrder.custName = $('#selCustName2').val().trim();
            CreateWorkOrder.worker = $('#txtWorker2').val().trim();
            $('#grid').datagrid('load', {
                workorder: CreateWorkOrder.workorderNo
                , status: CreateWorkOrder.status
                , partNo: CreateWorkOrder.partNo
                , custName: CreateWorkOrder.custName
                , worker: CreateWorkOrder.worker
                , orderno: $('#txtFOrderNo').val()
            });
        },
        bindGrid1: function () { 
            $('#grid1').datagrid('load', { 
                partscode: $('#txtQPartsDrawingNo').val()
                , orderno: $('#txtQOrderNo').val()
            });
        },
        RunWO: function () {
            //var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            var checkedItems = $('#grid').datagrid('getChecked');
           // var rowstr = JSON.stringify(checkedItems);
            for (var i = 0; i < checkedItems.length; i++) {
                if (checkedItems[i].StatusMemo == "关闭") {
                    //JeffComm.alertErr("关闭的工单禁止再运行", 500);
                     alert("关闭的工单禁止再运行");
                    return;
                }
            }
            for (var i = 0; i < checkedItems.length; i++) {
                CreateWorkOrder.workorderNo = checkedItems[i].WO;
                WsSystem.UpdateWorkOrderInfo(checkedItems[i].ID, "1", function (result) {
                    //CreateWorkOrder.bindGrid();
                });
            }
            CreateWorkOrder.bindGrid();
        },
        PauseWO: function () {
            var checkedItems = $('#grid').datagrid('getChecked');             
            for (var i = 0; i < checkedItems.length; i++) {
                if (checkedItems[i].StatusMemo == "关闭") {
                    alert("关闭的工单禁止再暂停");
                    return;
                }
            }
            for (var i = 0; i < checkedItems.length; i++) {
                CreateWorkOrder.workorderNo = checkedItems[i].WO;
                WsSystem.UpdateWorkOrderInfo(checkedItems[i].ID, "2", function (result) {
                    //CreateWorkOrder.bindGrid();
                });
            }
            CreateWorkOrder.bindGrid();
        },
        CloseWO: function () {
            //var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            //CreateWorkOrder.workorderNo = rows[0].WO;
            //WsSystem.UpdateWorkOrderInfo(CreateWorkOrder.workorderNo, "3", function (result) {
            //    CreateWorkOrder.bindGrid();
            //});
            var checkedItems = $('#grid').datagrid('getChecked'); 
            for (var i = 0; i < checkedItems.length; i++) {
                CreateWorkOrder.workorderNo = checkedItems[i].WO;
                WsSystem.UpdateWorkOrderInfo(checkedItems[i].ID, "3", function (result) {
                    //CreateWorkOrder.bindGrid();
                });
            }
            CreateWorkOrder.bindGrid();
        },
        delCustInfo: function () {
            var checkedItems = $('#grid').datagrid('getChecked');
            if (checkedItems.length > 0) {
                for (var i = 0; i < checkedItems.length; i++) {
                    if (checkedItems[i].StatusMemo != "创建") {
                        JeffComm.alertErr("只有创建状态下的计划才允许删除", 2000);
                        return;
                    }
                }
                for (var i = 0; i < checkedItems.length; i++) {
                    WsSystem.RemoveWorkOrderInfo(checkedItems[i].ID,
                    function (result) {
                        if (result == null) {
                            CreateWorkOrder.workorderNo = null;
                            JeffComm.alertSucc("删除成功", 500); 
                            //CreateWorkOrder.workorderNo = $('#txtWorkOrder').val();
                            //$('#grid').datagrid('load', { workorder: CreateWorkOrder.workorderNo });
                        } else {
                            alert(result, "提示");
                            //CreateWorkOrder.workorderNo = $('#txtWorkOrder').val();
                            //$('#grid').datagrid('load', { workorder: CreateWorkOrder.workorderNo });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
                }
            }
            else {
                JeffComm.alertErr("请选择数据进行删除", 2000);
                return;
            }
            //if (CreateWorkOrder.workorderNo == null || CreateWorkOrder.workorderNo == "") {
            //    JeffComm.alertErr("请选择一条数据进行删除", 500); 
            //} else {
            //    WsSystem.RemoveWorkOrderInfo(CreateWorkOrder.workorderNo,
            //        function (result) {
            //            if (result == null) {
            //                CreateWorkOrder.workorderNo = null;
            //                JeffComm.alertSucc("删除成功", 500); 
            //                CreateWorkOrder.workorderNo = $('#txtWorkOrder').val();
            //                $('#grid').datagrid('load', { workorder: CreateWorkOrder.workorderNo });
            //            } else {
            //                alert(result, "提示");
            //                CreateWorkOrder.workorderNo = $('#txtWorkOrder').val();
            //                $('#grid').datagrid('load', { workorder: CreateWorkOrder.workorderNo });
            //            }
            //        }, function (err) {
            //            alert("删除失败:" + err.get_message(), "提示");
            //        });
            //}
            CreateWorkOrder.workorderNo = $('#txtWorkOrder').val().trim();
            CreateWorkOrder.status = $('#selWOStatus2').val().trim();
            CreateWorkOrder.partNo = $('#txtPartsDrawingNo2').val().trim();
            CreateWorkOrder.custName = $('#selCustName2').val().trim();
            CreateWorkOrder.worker = $('#txtWorker2').val().trim();
            $('#grid').datagrid('load', { workorder: CreateWorkOrder.workorderNo, status: CreateWorkOrder.status, partNo: CreateWorkOrder.partNo, custName: CreateWorkOrder.custName, worker: CreateWorkOrder.worker });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            if (rows[0].StatusMemo != "创建") {
                JeffComm.alertErr("只能修改状态为创建的工单");
                //alert("只能修改状态为创建的工单");
                return;
            }
            WsSystem.QueryProductCodeCustNameByWorkOrder(rows[0].WO,
                function (result) {
                    var vres = new Array();
                    vres = result.split("^");
                    $('#txtProductCode').val(vres[0]);
                    $('#txtCustName').val(vres[1]);
                });
            $('#selOrderNo').append("<option value='" + rows[0].OrderNumber + "'>" + rows[0].OrderNumber + "</option>");
            $('#selPartsDrawing').append("<option value='" + rows[0].PartsdrawingCode + "'>" + rows[0].PartsdrawingCode + "</option>");
            CreateWorkOrder.workorderNo = rows[0].WO;
            $('#txtWorkOrderNo').val(rows[0].WO);
            $('#selOrderNo').val(rows[0].OrderNumber);
            $('#selPartsDrawing').val(rows[0].PartsdrawingCode);
            $('#txtProductName').val(rows[0].ProductName);
            $('#txtStartTime').val(rows[0].StartTime);
            $('#txtEndTime').val(rows[0].EndTime);
            $('#txtBatchNumber').val(rows[0].BatchNumber);
            $('#txtPlanQuantity').val(rows[0].PlanQuantity);
            $('#txtPlanCheckTime').val(rows[0].CheckTime);
            $('#txtPlanInTime').val(rows[0].InstockTime);
            $('#txtBaseQty').val(rows[0].BaseQty);
            $('#txtQualityCode').val(rows[0].QualityCode);
            $('#selEquitmentType').combobox('setValues', rows[0].MachineType.split(','));
            $('#selEquipment').combobox('setValues', rows[0].MachineName.split(','));
            $('#selBanci').combobox('setValues', rows[0].CLASS.split(','));
            $('#selStationName').combobox('setValues', rows[0].StationName);
            $('#txtStationId').val(rows[0].RouteCode);
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '工单维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryWODetailsNew',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                //styler: function(value, row, index){
                //    if (row[0].MEMO == "运行") {
                //        return 'background-color:green;';
                //    }
                //},
                fit: true,
                nowrap: true,
                remoteSort: false,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: false,
                pagination: true,
                rownumbers: true,
                columns: [[ //选择
                    { field: 'ck', checkbox: true },
                    { title: 'ID', field: 'ID', width: CreateWorkOrder.fixWidth(0.1),hidden: true },
                     { title: '工单单号', field: 'WO', width: CreateWorkOrder.fixWidth(0.1) ,sortable:true},
                     { title: '工单状态', field: 'StatusMemo', width: CreateWorkOrder.fixWidth(0.05), sortable: true },
                     { title: '订单单号', field: 'OrderNumber', width: CreateWorkOrder.fixWidth(0.1), sortable: true },
                     { title: '零件图号', field: 'PartsdrawingCode', width: CreateWorkOrder.fixWidth(0.1), sortable: true },
                     { title: '机床类型', field: 'MachineType', width: CreateWorkOrder.fixWidth(0.05), sortable: true },
                     { title: '机床名称', field: 'MachineName', width: CreateWorkOrder.fixWidth(0.08), sortable: true },
                     { title: '工序名', field: 'StationName', width: CreateWorkOrder.fixWidth(0.05), sortable: true },
                     { title: '工序号', field: 'RouteCode', width: CreateWorkOrder.fixWidth(0.05), sortable: true },
                     { title: '负责人员', field: 'WorkerName', width: CreateWorkOrder.fixWidth(0.05) },
                     { title: '产品名称', field: 'ProductName', width: CreateWorkOrder.fixWidth(0.06) },
                     { title: '质量编号', field: 'QualityCode', width: CreateWorkOrder.fixWidth(0.08), sortable: true },
                     {
                         title: '计划开始', field: 'StartTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: CreateWorkOrder.fixWidth(0.1), sortable: true
                     },
                     {
                         title: '计划结束', field: 'EndTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: CreateWorkOrder.fixWidth(0.1),sortable:true
                     },
                     { title: '备注', field: 'BatchNumber', width: CreateWorkOrder.fixWidth(0.1) },
                     { title: '计划数量', field: 'PlanQuantity', width: CreateWorkOrder.fixWidth(0.05) },
                     {
                         title: '计划检验时间', field: 'CheckTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: CreateWorkOrder.fixWidth(0.1)
                     },
                     {
                         title: '计划入库时间', field: 'InstockTime', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: CreateWorkOrder.fixWidth(0.1)
                     },
                     { title: '生产班次', field: 'CLASS', width: CreateWorkOrder.fixWidth(0.08) },
                     { title: '工时', field: 'UnitTime', width: CreateWorkOrder.fixWidth(0.05) },
                       
                    {
                        title: '创建时间', field: 'CreatedDate', formatter: function (value, row, index) {
                            if (value != null & value != "") {
                                var unixTimestamp = new Date(value);
                                return unixTimestamp.toLocaleString();
                            }
                        }, width: CreateWorkOrder.fixWidth(0.1), sortable: true
                    }
                ]],
                rowStyler: function (index, row) {
                    if (row.StatusMemo == "创建") {
                        return 'background-color:#B7FF4A;color:#000;';
                    }
                    else if (row.StatusMemo == "运行") {
                         //如果没有按计划完成的任务单，浅红色表示
                        //var date1 = new Date(row.EndTime);
                        //var date2 = new Date();
                        //var s1 = date1.getTime(), s2 = date2.getTime();
                        //var total = (s2 - s1) / 1000;
                        //var day = parseInt(total / (24 * 60 * 60));//计算整数天数

                        //if (day > -3 && (row.MEMO == "运行")) {
                        //    return 'background-color:#ff0000;color:#000;font-weight:bold;';
                        //}
                        return 'background-color:#00ff00;color:#000;';
                    } else if (row.StatusMemo == "暂停") {
                        return 'background-color:#D9B300;color:#000;';
                    }

                   
                },
                toolbar: [
                //    {
                //        text: '订单单号 <input type="text" id="txtFOrderNo"/>'
                //}, '-',{
                //    text: '工单单号<input type="text" id="txtWorkOrder"/>'
                //}, '-', {
                //    text: '工单状态 <select tabindex="-1" name="selWOStatus2" id="selWOStatus2"><option value="4">工单状态</option><option value="0">创建</option> <option value="1">运行</option><option value="2">暂停</option><option value="3">关闭</option></select>'
                //}, '-', {
                //    text: '零件图号 <input type="text" id="txtPartsDrawingNo2"/>'
                //}, '-', {
                //    text: '客户名称 <select tabindex="-1" name="selCustName2" id="selCustName2"><option value="0">请选择</option> </select>'
                //}, '-', {
                //    text: '负责人 <input type="text" id="txtWorker2"/>'
                //},

                //'-', 
                {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        CreateWorkOrder.bindGrid();
                    }
                },
                '-', {
                    id: 'btnRun',
                    text: '运行',
                    iconCls: 'icon-ok',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length > 0) {
                            CreateWorkOrder.RunWO();
                        }
                        else {
                            JeffComm.alertErr("请选择至少一条数据进行运行");
                            // alert("请选择一条数据进行运行", "提示");
                        }
                    }
                },
                '-', {
                    id: 'btnPause',
                    text: '暂停',
                    iconCls: 'icon-remove',
                    handler: function () {
                        var checkedItems = $('#grid').datagrid('getChecked');             
                        
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (checkedItems.length > 0) {
                            for (var i = 0; i < checkedItems.length; i++) {
                                var status = checkedItems[i].StatusMemo;
                                if (status == "创建" || status == "关闭") {
                                    JeffComm.alertErr("此"+status+"状态下的工单不允许暂停");
                                    //alert("此状态下的工单不允许暂停");
                                    return;
                                }
                            }
                             
                                CreateWorkOrder.PauseWO();
                            
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行暂停");
                            //alert("请选择一条数据进行暂停", "提示");
                        }
                    }
                },
                '-', {
                    id: 'btnClose',
                    text: '关闭',
                    iconCls: 'icon-no',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length >0) {
                            CreateWorkOrder.CloseWO();
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行关闭");
                            // alert("请选择一条数据进行关闭", "提示");
                        }
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建工单',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建工单" });
                        $('#btnDownload').show();
                        $('#btnImport').show();
                        $('#btnNew').show();
                        $('#txtWorkOrderNo').val("");
                        $('#txtProductName').val("");
                        $('#txtBatchNumber').val("");
                        $('#txtPlanQuantity').val("");
                        $('#txtStartTime').val("");
                        $('#txtEndTime').val("");
                        $('#txtPlanCheckTime').val("");
                        $('#txtPlanInTime').val("");
                        $('#selOrderNo').val("");
                        $('#txtQualityCode').val("");
                        $('#selPartsDrawing').val("");
                        $('#txtUnitTime').val("");
                        //初始化起止日期
                        var date = new Date();
                        var m = date.getMonth() + 1;//获取当前月份的日期 
                        var d = date.getDate();
                        var h = date.getHours() + 1;
                        var min = date.getMinutes();
                        if (m.toString().length == 1) {
                            m = "0" + m;
                        }
                        if (d.toString().length == 1) {
                            d = "0" + d;
                        }
                        if (h.toString().length == 1) {
                            h = "0" + h;
                        }
                        if (min.toString().length == 1) {
                            min = "0" + min;
                        }
                        var sdate = date.getFullYear() + "-" + m + "-" + d + " " + h + ":" + min + ":00";
                        $("#txtStartTime").val(sdate);
                        $("#restartDialog").dialog('open');  //实现添加记录的页面
                        CreateWorkOrder.workorderNo = null;
                        //WsSystem.GetWorkOrderCode(function (result) {
                        //    $('#txtWorkOrderNo').val(result);
                        //    $('#txtProductName').val("");
                        //    $('#txtBatchNumber').val("");
                        //    $('#txtPlanQuantity').val("");
                        //    //$('#txtStartTime').val("");
                        //    $('#txtEndTime').val("");
                        //    $('#txtPlanCheckTime').val("");
                        //    $('#txtPlanInTime').val("");
                        //    $('#txtQualityCode').val("");
                        //    $('#txtUnitTime').val("");
                        //    //$('#selEquitmentType').combobox('setValues', "")
                        //    //$('#selEquipment').combobox('setValues', "")
                        //    $('#selOrderNo').val("");
                        //    $('#selPartsDrawing').val("");
                        //    $('#txtBaseQty').val("1");
                        //});

                    }
                },
                '-', {
                    id: 'btnEdit',
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            $('#restartDialog').panel({ title: "修改工单" });
                            $('#btnDownload').hide();
                            $('#btnImport').hide();
                            $('#btnNew').hide();
                            CreateWorkOrder.ShowEditOrViewDialog();//实现修改记录的方法
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行修改");
                            // alert("请选择一条数据进行修改", "提示");
                        }
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        $.messager.confirm('确认', '您确认想要删除工单吗？', function (r) {
                            if (r) {
                                CreateWorkOrder.delCustInfo();//实现直接删除数据的方法
                            }
                        });
                       
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
                        window.location.href = "WorkOrderMain.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    CreateWorkOrder.workorderNo = rowData["WO"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50,500],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
            
            $('#grid1').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryOrderPublish',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                autoRowHeight: false,
                // singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [6, 10, 15],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择                    
                    //{ title: 'ID', field: 'ID', width: WorkOrderMain.fixWidth(0.09), hidden: 'true' },
                   { title: '订单单号', field: 'OrderNo', width: CreateWorkOrder.fixWidth(0.08) },
                   { title: '零件图号', field: 'PartsdrawingCode', width: CreateWorkOrder.fixWidth(0.08) },
                   { title: '订单状态', field: 'MEMO', width: CreateWorkOrder.fixWidth(0.05) },
                   { title: '客户名称', field: 'CustName', width: CreateWorkOrder.fixWidth(0.06) },
                   { title: '投产总数', field: 'OrderQuantity', width: CreateWorkOrder.fixWidth(0.06) },
                   { title: '产品名称', field: 'ProductName', width: CreateWorkOrder.fixWidth(0.07) },
                   { title: '交付数量', field: 'OutQuantity', width: CreateWorkOrder.fixWidth(0.06) },
                   {
                       title: '交付时间', field: 'OutDate', formatter: function (value, row, index) {
                           if (value != null & value != "") {
                               var unixTimestamp = new Date(value);
                               return unixTimestamp.toLocaleString();
                           }
                       }, width: CreateWorkOrder.fixWidth(0.08)
                   }
                ]],
                 toolbar: [
                    {
                        text: '订单单号 <input type="text" id="txtQOrderNo"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtQPartsDrawingNo"/>'
                }, 
                '-', 
                {
                    id: 'btnQSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        CreateWorkOrder.bindGrid1();
                    }
                }, ],
                 onClickRow: function (rowIndex, rowData) {
                     //CreateWorkOrder.workorderNo = rowData["WO"]; 
                     $("#selPartsDrawing").val(rowData["PartsdrawingCode"]);
                     //$("#selOrderNo").val(rowData["OrderNo"]);
                     WsSystem.ListBindOrderbyPartsDrawingCode(rowData["PartsdrawingCode"],
                       function (result) {
                           JeffComm.fillSelect($("#selOrderNo"), result, true);
                       });
                     WsSystem.GetUnitTimebyPartsDrawingCode(rowData["PartsdrawingCode"],
                      function (result) {
                          // alert(result);
                          // var vres = result.split('^');
                          $('#txtUnitTime').val(result);
                      });
                     WsWareHouse.QueryInOutMaterialQty(rowData["PartsdrawingCode"],
                        function (result) {
                            $('#txtMaterialQty').val(result);
                        });
                     WsSystem.QueryTechnologyStatus(rowData["PartsdrawingCode"],
                        function (result) {
                            $('#txtTechnologyFile').val(result);
                        });
                     WsSystem.QueryMachineTypeByDrawingNo($('#selPartsDrawing').val(),"",
                      function (result) {
                          $('#selEquitmentType').combobox('setValues', result);
                      });
                     //查询工序号
                     $('#selStationName').combobox({
                         //prompt: '输入首关键字自动检索',
                         required: false,
                         url: '../../services/WsSystem.asmx/ListStationNamesEasyUI',
                         queryParams: { partsdrawingno: $('#selPartsDrawing').val() },
                         valueField: 'id',
                         textField: 'text',
                         editable: true,
                         multiple: false,
                         panelHeight: 'auto',
                         hasDownArrow: true,
                         filter: function (q, row) {
                             var opts = $(this).combobox('options');
                             return row[opts.textField].indexOf(q) == 0;
                         }
                     });
                     //$.ajax({
                     //    url: '../../services/WsSystem.asmx/ListStationNamesEasyUI',
                     //    queryParams: { partsdrawingno: $('#selPartsDrawing').val() },
                     //    type: "post",
                     //    datatype: "json",
                     //    success: function (data) {
                     //        $('#selStationName').combobox({
                     //            data: data.rows,
                     //            valueField: 'id',
                     //            textField: 'text',
                     //            onLoadSuccess: function () { //加载完成后,val[0]写死设置选中第一项
                     //                var val = $(this).combobox("getData");
                     //                for (var item in val[0]) {
                     //                    if (item == "id") {
                     //                        $(this).combobox("select", val[0][item]);
                     //                    }
                     //                }
                     //            }
                     //        });
                     //    }
                     //});
                 },
            });
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName2"), result, true);
            });
            
        }
    };
}();