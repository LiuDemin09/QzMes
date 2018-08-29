var MarStock = function () {

    return {
        MSN: null,
        MaterialCode: null,
        Batch: null,
        CustName: null,
        StartTime: null,
        EndTime: null,

        init: function () {
            $('#centerPanel').tabs({
                plain: false,
                fit: false,
                tabPosition: 'top',// 选项卡位置
                tabWidth: 200,
                tabHeight: 40,
                selected: 0,// 初始化选中一个 tab 页, 从0开始                
                showHeader: true// 是否显示 tab 页标题
            });
            //初始化购货单位
            //WsSystem.ListBindCustName(function (result) {
            //    JeffComm.fillSelect($("#selCustName"), result, true);
            //});


        },
        bindGridI: function () {
            MarStock.MSN = $("#txtIMSN").val().trim();
            MarStock.MaterialCode = $("#txtIMaterialName").val().trim();
            MarStock.Batch = $("#txtICustomer").val().trim();
            MarStock.CustName = $("#txtIBatchNumber").val().trim();
            MarStock.StartTime = $("#txtIStartTime").val().trim();
            MarStock.EndTime = $("#txtIEndTime").val().trim();
            if (MarStock.StartTime!=null&MarStock.StartTime!=""
                & MarStock.EndTime!=null& MarStock.EndTime!=""
                & MarStock.StartTime > MarStock.EndTime) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间","提示");
            } else {
                $('#grid').datagrid('load', {
                    msn: MarStock.MSN, materialCode: MarStock.MaterialCode,
                    batch: MarStock.Batch, custName: MarStock.CustName,
                    startTime: MarStock.StartTime, endTime: MarStock.EndTime
                });
            }
            

        },
        bindGridO: function () {
            MarStock.MSN = $("#txtOMSN").val().trim();
            MarStock.MaterialCode = $("#txtOMaterialName").val().trim();
            MarStock.Batch = $("#txtOCustomer").val().trim();
            MarStock.CustName = $("#txtOBatchNumber").val().trim();
            MarStock.StartTime = $("#txtOStartTime").val().trim();
            MarStock.EndTime = $("#txtOEndTime").val().trim();
            if (MarStock.StartTime != null & MarStock.StartTime != ""
                & MarStock.EndTime != null & MarStock.EndTime != ""
                & MarStock.StartTime > MarStock.EndTime) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间", "提示");
            } else {
                $('#grid1').datagrid('load', {
                    msn: MarStock.MSN, materialCode: MarStock.MaterialCode,
                    batch: MarStock.Batch, custName: MarStock.CustName,
                    startTime: MarStock.StartTime, endTime: MarStock.EndTime
                });
            }

        },
        bindGridIO: function () {
            MarStock.MaterialCode = $("#txtIOMaterialName").val().trim();
            MarStock.Batch = $("#txtIOCustomer").val().trim();
            MarStock.CustName = $("#txtIOBatchNumber").val().trim();
            MarStock.StartTime = $("#txtIOStartTime").val().trim();
            MarStock.EndTime = $("#txtIOEndTime").val().trim();
            if (MarStock.StartTime != null & MarStock.StartTime != ""
                & MarStock.EndTime != null & MarStock.EndTime != ""
                & MarStock.StartTime > MarStock.EndTime) {
                JeffComm.alertErr("开始时间不能大于结束时间");
                //alert("开始时间不能大于结束时间", "提示");
            } else {
                $('#grid2').datagrid('load', {
                    materialCode: MarStock.MaterialCode,
                    batch: MarStock.Batch, custName: MarStock.CustName,
                    startTime: MarStock.StartTime, endTime: MarStock.EndTime
                });
            }

        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '入库列表',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/QueryInMaterial',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '来料条码', field: 'MSN', width: MarStock.fixWidth(0.1) },
                     { title: '购货单位', field: 'CustName', width: MarStock.fixWidth(0.08) },
                     {
                         title: '入库时间', field: 'CreatedDate', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: MarStock.fixWidth(0.1)
                     },
                     { title: '收料仓库', field: 'StockHouse', width: MarStock.fixWidth(0.07) },
                     { title: '单据编号', field: 'DOCUMENTID', width: MarStock.fixWidth(0.07) },
                     { title: '物料长代码', field: 'MaterialCode', width: MarStock.fixWidth(0.08) },
                     { title: '物料名称', field: 'MaterialName', width: MarStock.fixWidth(0.07) },
                     { title: '批号', field: 'BatchNumber', width: MarStock.fixWidth(0.07) },
                     { title: '数量', field: 'QUANTITY', width: MarStock.fixWidth(0.05) },
                      { title: '备注', field: 'Description', width: MarStock.fixWidth(0.05) },
                     { title: '保管员', field: 'UpdatedBy', width: MarStock.fixWidth(0.06) },
                     {
                         title: '时间', field: 'CreatedDate', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: MarStock.fixWidth(0.06)
                     }
                ]],
                toolbar: [{
                    text: '来料条码 <input type="text" id="txtIMSN"/>'
                }, '-',
                {
                    text: '物料名称 <input type="text" id="txtIMaterialName"/>'
                }, '-',
                {
                    text: '备注 <input type="text" id="txtICustomer"/>'
                }, '-',
                {
                    text: '购货单位 <input type="text" id="txtIBatchNumber"/>'
                }, '-',
                 {
                     text: '开始时间 <input type="text" id="txtIStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                 '-', {
                     text: '结束时间<input type="text" id="txtIEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearchI',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        MarStock.bindGridI();
                    }
                }, '-', {
                    id: 'btnExpoetI',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/WareHouseManage/export/ISNExport.aspx?sn=' + $('#txtIMSN').val() + '&starttime=' + $('#txtIStartTime').val()
                      + '&endtime=' + $('#txtIEndTime').val() + '&custname=' + $('#txtICustomer').val() + '&materialname=' + $('#txtIMaterialName').val()
                      + '&batchnumber=' + $('#txtIBatchNumber').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "WHTable.aspx";
                    }
                }]

            });


            $('#grid1').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '出库列表',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/QueryOutMaterial',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '来料条码', field: 'MSN', width: MarStock.fixWidth(0.11) },
                     { title: '工单号码', field: 'WorkOrder', width: MarStock.fixWidth(0.11) },
                     { title: '物料代码', field: 'MaterialCode', width: MarStock.fixWidth(0.1) },
                     { title: '物料名称', field: 'MaterialName', width: MarStock.fixWidth(0.07) },
                     { title: '备注', field: 'BatchNumber', width: MarStock.fixWidth(0.07) },
                     { title: '数量', field: 'QUANTITY', width: MarStock.fixWidth(0.08) },
                     { title: '发料仓库', field: 'StockHouse', width: MarStock.fixWidth(0.07) },
                     { title: '源单单号', field: 'DOCUMENTID', width: MarStock.fixWidth(0.07) },
                     { title: '发料员', field: 'UpdatedBy', width: MarStock.fixWidth(0.05) },
                     { title: '领料员', field: 'MaterialHandler', width: MarStock.fixWidth(0.06) },
                     {
                         title: '领料时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: MarStock.fixWidth(0.12)
                     }
                ]],
                toolbar: [{
                    text: '来料条码 <input type="text" id="txtOMSN"/>'
                }, '-',
                {
                    text: '物料名称 <input type="text" id="txtOMaterialName"/>'
                }, '-',
                {
                    text: '备注 <input type="text" id="txtOCustomer"/>'
                }, '-',
                {
                    text: '购货单位 <input type="text" id="txtOBatchNumber"/>'
                }, '-',
                 {
                     text: '开始时间 <input type="text" id="txtOStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                 '-', {
                     text: '结束时间<input type="text" id="txtOEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearchO',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        MarStock.bindGridO();
                    }
                }, '-', {
                    id: 'btnExpoetO',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/WareHouseManage/export/OSNExport.aspx?sn=' + $('#txtOMSN').val() + '&starttime=' + $('#txtOStartTime').val()
                     + '&endtime=' + $('#txtOEndTime').val() + '&custname=' + $('#txtOCustomer').val() + '&materialname=' + $('#txtOMaterialName').val()
                     + '&batchnumber=' + $('#txtOBatchNumber').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }]

            });

            $('#grid2').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '库存列表',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/QueryInOutMaterial',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '来料条码', field: 'MSN', width: MarStock.fixWidth(0.11) },
                     { title: '物料代码', field: 'MaterialCode', width: MarStock.fixWidth(0.1) },
                     { title: '物料名称', field: 'MaterialName', width: MarStock.fixWidth(0.07) },
                     { title: '备注', field: 'BatchNumber', width: MarStock.fixWidth(0.1) },
                     { title: '数量', field: 'BasQty', width: MarStock.fixWidth(0.08) },
                     { title: '单据编号', field: 'DOCUMENTID', width: MarStock.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: MarStock.fixWidth(0.15)
                     }
                ]],
                toolbar: [
                {
                    text: '物料名称 <input type="text" id="txtIOMaterialName"/>'
                }, '-',
                {
                    text: '备注 <input type="text" id="txtIOCustomer"/>'
                }, '-',
                {
                    text: '购货单位 <input type="text" id="txtIOBatchNumber"/>'
                }, '-',
                 {
                     text: '开始时间 <input type="text" id="txtIOStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                 '-', {
                     text: '结束时间<input type="text" id="txtIOEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearchIO',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        MarStock.bindGridIO();
                    }
                }, '-', {
                    id: 'btnExpoetIO',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/WareHouseManage/export/IOSNExport.aspx?starttime=' + $('#txtIOStartTime').val()
                     + '&endtime=' + $('#txtIOEndTime').val() + '&custname=' + $('#txtIOCustomer').val() + '&materialname=' + $('#txtIOMaterialName').val()
                     + '&batchnumber=' + $('#txtIOBatchNumber').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }] 
            }); 
        } 
    }; 
}();