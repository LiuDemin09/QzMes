var PartsDrawingNo = function () {
    return {
        grid: null,
        PDNo: null,
        custCode: null,
        startTime: null,
        endTime: null,
        ID:null,
        //qzmateriel: null,
        //custmateriel:null,
        //main function to initiate the module
        init: function () {
            //$('#txtBasQty').val("1");
            //$('#txtQZMaterielNo').attr("disabled", "disabled");
            //初始化客户名称
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
                JeffComm.fillSelect($("#selCustName1"), result, true);
            });
            $('#selProductName').combobox({
                prompt: '输入首关键字自动检索',
                required: false,
                url: '../../services/WsSystem.asmx/ListBindProductNameEasyUI',
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
                PartsDrawingNo.PDNo = null;
                $('#txtPartCode').val("");
                $('#txtUnitTime').val("");                 
                $('#restartDialog').window('close');
            });
            jQuery('#btnNew').click(function () {
                PartsDrawingNo.PDNo = null;
                //WsSystem.GetBasBaseCode(function (res) {
                //    $('#txtQZMaterielNo').val(res);
                //});
                $('#txtPartCode').val("");
                $('#txtUnitTime').val("");
                WsSystem.ListBindCustName(function (result) {
                    JeffComm.fillSelect($("#selCustName"), result, true);                    
                });
            });           
            jQuery('#btnSave').click(function () {
                var tt = $("#selProductName").combobox("getValue");//$("#selProductName").find("option:selected").text();
                var ttext = $("#selProductName").combobox("getText");
               // tt = $("#selProductName").val();
                　var reg = new RegExp("[\\u4E00-\\u9FFF]+","g");
                if (reg.test($("#txtPartCode").val())) {
                       alert("不能输入汉字！");  
                       return;          
　　                }       
                 
                var partsdrawinginfo = {
                    ID:PartsDrawingNo.ID,
                    PartsCode: $("#txtPartCode").val(),
                    CustName: $("#selCustName").find("option:selected").text(),
                    CustCode: $("#selCustName").val(),
                    ProductName: ttext,
                    ProductCode: tt,
                    MEMO: $("#txtPartCode").val() + "|" + $("#selCustName").val(),
                    UnitTime: $("#txtUnitTime").val(),
                };
                WsSystem.SavePartsDrawing(partsdrawinginfo,
                    function (result) {
                        if (result == "OK"||result==null) {
                            JeffComm.alertSucc("保存成功", 500);
                            $('#txtPartCode').val("");
                            $('#selCustName').val("");
                            $('#selProductName').val("");
                            $('#txtUnitTime').val("");
                            PartsDrawingNo.ID = null;
                        } else {
                            alert(result, "提示");
                        }
                    }, function (err) {
                        alert(err.get_message(), "提示");
                    });         
            });
            jQuery('#btnClear').click(function () {
                $('#txtPartCode').val("");
                $('#txtUnitTime').val("");
                $('#selCustName').val("");
                $('#selProductName').val("");
            });
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/BaseInfo/PartsDrawingDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/BaseInfo/PartsDrawingUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
            });
        },
        bindGrid: function () {
            PartsDrawingNo.PDNo = $("#txtPartCode1").val();
            PartsDrawingNo.custCode = $("#selCustName1").val();
            PartsDrawingNo.startTime = $("#txtStartTime").val().trim();
            PartsDrawingNo.endTime = $("#txtEndTime").val().trim();
            var oDate1 = new Date(PartsDrawingNo.startTime);
            var oDate2 = new Date(PartsDrawingNo.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
                // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', { partCode: PartsDrawingNo.PDNo, custCode: PartsDrawingNo.custCode, startTime: PartsDrawingNo.startTime, endTime: PartsDrawingNo.endTime });
        },
        delCustInfo: function () {
            if (PartsDrawingNo.ID == null || PartsDrawingNo.ID == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
                WsSystem.RemovePartsDrawingInfo(PartsDrawingNo.ID,
                    function (result) {
                        if (result == null) {
                            PartsDrawingNo.custmaterielNo = null;
                            JeffComm.alertSucc("删除成功", 500);
                            PartsDrawingNo.ID = null;
                            //alert("删除成功", "提示");
                            PartsDrawingNo.PDNo = $("#txtPartCode").val();
                            PartsDrawingNo.custCode = $("#selCustName").val().trim();
                            PartsDrawingNo.startTime = $("#txtStartTime").val().trim();
                            PartsDrawingNo.endTime = $("#txtEndTime").val().trim();
                            var oDate1 = new Date(PartsDrawingNo.startTime);
                            var oDate2 = new Date(PartsDrawingNo.endTime);
                            if (oDate1.getTime() > oDate2.getTime()) {
                                JeffComm.alertErr("开始时间不能大于结束时间");
                                // alert("开始时间不能大于结束时间");
                                return;
                            }
                            $('#grid').datagrid('load', { partCode: PartsDrawingNo.PDNo, custCode: PartsDrawingNo.custCode, startTime: PartsDrawingNo.startTime, endTime: PartsDrawingNo.endTime });

                        } else {
                            alert(result, "提示");
                            PartsDrawingNo.PDNo = $("#txtPartCode1").val();
                            PartsDrawingNo.custCode = $("#selCustName1").val().trim();
                            PartsDrawingNo.startTime = $("#txtStartTime").val().trim();
                            PartsDrawingNo.endTime = $("#txtEndTime").val().trim();
                            var oDate1 = new Date(PartsDrawingNo.startTime);
                            var oDate2 = new Date(PartsDrawingNo.endTime);
                            if (oDate1.getTime() > oDate2.getTime()) {
                                JeffComm.alertErr("开始时间不能大于结束时间");
                                // alert("开始时间不能大于结束时间");
                                return;
                            }
                            $('#grid').datagrid('load', { partCode: PartsDrawingNo.PDNo, custCode: PartsDrawingNo.custCode, startTime: PartsDrawingNo.startTime, endTime: PartsDrawingNo.endTime });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            PartsDrawingNo.PDNo = $("#txtPartCode1").val();
            PartsDrawingNo.custCode = $("#selCustName1").val().trim();
            PartsDrawingNo.startTime = $("#txtStartTime").val().trim();
            PartsDrawingNo.endTime = $("#txtEndTime").val().trim();
            var oDate1 = new Date(PartsDrawingNo.startTime);
            var oDate2 = new Date(PartsDrawingNo.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
                // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', { partCode: PartsDrawingNo.PDNo, custCode: PartsDrawingNo.custCode, startTime: PartsDrawingNo.startTime, endTime: PartsDrawingNo.endTime });

        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            PartsDrawingNo.ID = rows[0].ID;
            $('#txtPartCode').val(rows[0].PartsCode);
            $('#selCustName').val(rows[0].CustCode);            
            //$("#selProductName").val(rows[0].ProductName);
            $('#selProductName').combobox('select', rows[0].ProductCode);
            $("#txtUnitTime").val(rows[0].UnitTime);           
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '图号维护',
                iconCls: 'icon-view',
                url: '../../services/WsOrder.asmx/FindDrawing',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                remoteSort: false,
                rownumbers: true,
                //sortName: 'CreatedDate', //初始排序字段  
                //sortOrder: 'desc', //初始排序方式  
                columns: [[ //选择
                     { title: '零件图号', field: 'PartsCode', width: PartsDrawingNo.fixWidth(0.09), sortable: true },
                     { title: '客户名称', field: 'CustName', width: PartsDrawingNo.fixWidth(0.08),sortable:true },
                     {
                         title: '客户代码', field: 'CustCode', width: PartsDrawingNo.fixWidth(0.08), sortable: true
                },
                     { title: '产品名称', field: 'ProductName', width: PartsDrawingNo.fixWidth(0.08), sortable: true },
                     { title: '产品代码', field: 'ProductCode', width: PartsDrawingNo.fixWidth(0.08), hidden: 'true'
                },
                     { title: '生产工时', field: 'UnitTime', width: PartsDrawingNo.fixWidth(0.08)
                },
                     //{ title: '质量编号', field: 'QualityCode', width: DrawingQuery.fixWidth(0.08) },
                     //{ title: '交付数量', field: 'AskQuantity', width: DrawingQuery.fixWidth(0.07) },
                     //{ title: '炉批号', field: 'BatchNumber', width: DrawingQuery.fixWidth(0.08) },
                     {
                         title: '交付时间', field: 'AskDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: PartsDrawingNo.fixWidth(0.1), sortable: true
                     },
                     { title: '创建人', field: 'UpdatedBy', width: PartsDrawingNo.fixWidth(0.06)
                },
                     {
                         title: '时间', field: 'CreatedDate', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                             }
                         }, width: PartsDrawingNo.fixWidth(0.1), sortable: true
                     },
                ]],
                toolbar: [{
                        text: '零件图号 <input type="text" id="txtPartCode1"/>'
                }, '-', {
                        text: '客户名称 <select tabindex="-1" name="selCustName1" id="selCustName1"><option value="0">请选择</option> </select>'
                }, '-', {
                    text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间 <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        PartsDrawingNo.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建图号',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建图号" });
                        //$('#btnDownload').show();
                        //$('#btnImport').show();
                        $('#btnNew').show();
                         $('#txtPartCode').removeAttr("disabled");
                        //WsSystem.GetBasBaseCode(function (res) {
                        //    $('#txtQZMaterielNo').val(res);
                        //});
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
                            $('#restartDialog').panel({ title: "修改图号" });
                            //$('#btnDownload').hide();
                            //$('#btnImport').hide();
                            //$('#btnNew').hide();
                            $('#txtPartCode').attr("disabled", "disabled");
                           // PartsDrawingNo.ID = rowData["ID"];
                            PartsDrawingNo.ShowEditOrViewDialog();//实现修改记录的方法
                        }
                        else
                        {
                            JeffComm.alertErr("请选择一条数据进行修改");
                           // alert("请选择一条数据进行修改", "提示");
                        }
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        PartsDrawingNo.delCustInfo();//实现直接删除数据的方法
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
                    PartsDrawingNo.PDNo = rowData["PartsCode"];
                    PartsDrawingNo.ID = rowData["ID"];
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