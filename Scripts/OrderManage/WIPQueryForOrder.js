var WIPQueryForOrder = function () {
    return {
        grid: null,
        workorderNo: null,
       
        //main function to initiate the module
        init: function () {
             
        },
        bindGrid: function () {            
            $('#grid').datagrid('load', {
               // workorder: $("#txtWorkOrder").val(),
                partsdrawingcode: $("#txtPartsDrawingNo").val()
                , productname: $("#txtProductName").val()
                , batchnumber: $("#txtBatchNumber").val()
            });
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: 'WIP信息',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryWIPInfoForOrderByCondition',
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
                    { title: '工单单号', field: 'WO', width: WIPQueryForOrder.fixWidth(0.1) },
                   { title: '零件图号', field: 'PartsdrawingCode', width: WIPQueryForOrder.fixWidth(0.1) },
                   { title: '产品名称', field: 'ProductName', width: WIPQueryForOrder.fixWidth(0.08) },
                   { title: '批号', field: 'BatchNumber', width: WIPQueryForOrder.fixWidth(0.08) },
                   { title: '计划数量', field: 'PlanQuantity', width: WIPQueryForOrder.fixWidth(0.05) },
                   { title: '车铣良品数', field: 'CheXiPassCount', width: WIPQueryForOrder.fixWidth(0.06) },
                   { title: '车铣不良数', field: 'CheXiFailCount', width: WIPQueryForOrder.fixWidth(0.06) },
                   { title: '钳工良品数', field: 'QianGongPassCount', width: WIPQueryForOrder.fixWidth(0.06) },
                   { title: '钳工不良数', field: 'QianGongFailCount', width: WIPQueryForOrder.fixWidth(0.06) },
                    { title: '检验良品数', field: 'QCPassCount', width: WIPQueryForOrder.fixWidth(0.06) },
                    { title: '检验不良数', field: 'QCFailCount', width: WIPQueryForOrder.fixWidth(0.06) },
                   { title: '入库数量', field: 'InStockQty', width: WIPQueryForOrder.fixWidth(0.05) }
                ]],
                toolbar: [
                //    {
                //    id: 'btnBack',
                //    text: '返回',
                //    iconCls: 'icon-back',
                //    handler: function () {
                //        window.location.href = "WorkOrderMain.aspx";
                //    }
                //}
                   //{ text: '工单单号 <input type="text" id="txtWorkOrder"/>'
                   //}, '-',
                {
                    text: '零件图号 <input type="text" id="txtPartsDrawingNo"/>'
                }
                , '-', {
                    text: '产品名称 <input type="text" id="txtProductName"/>'
                }
                , '-', {
                    text: '批号 <input type="text" id="txtBatchNumber"/>'
                }
                //, '-', {
                //    text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                //},
                // '-', {
                //     text: '结束时间 <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                // },
                ,'-',
                {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        WIPQueryForOrder.bindGrid();
                    }
                    //}, '-', {
                    //    id: 'btnExpoet',
                    //    text: '导出',
                    //    iconCls: 'icon-save',
                    //    handler: function () {

                    //        window.open('../../Pages/WorkOrderManage/export/CapacityStatisticsExport.aspx?workorder=' + $('#txtWorkOrder').val() + '&starttime=' + $('#txtStartTime').val()
                    //    + '&endtime=' + $('#txtEndTime').val() + '&status=' + $('#selWOStatus').val() + '&order=' + $('#txtOrderNumber').val()
                    //    + '&partsdrawingno=' + $('#txtPartsDrawingNo').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "')
                    //    }
                    //},
                    //'-', {
                    //    id: 'btnReload',
                    //    text: '刷新',
                    //    iconCls: 'icon-reload',
                    //    handler: function () {
                    //        //实现刷新栏目中的数据
                    //        $("#grid").datagrid("reload");
                    //    }
                    //}
                } ],
                //onClickRow: function (rowIndex, rowData) {
                //    WIPQueryForOrder.workorderNo = rowData["WO"];
                //    // ShowEditOrViewDialog();
                //},
                // loader: function (param, success, error) { }
                onLoadError: function (error) {
                    alert(error.responseText);
                }

            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 20,
                pageNumber: 1,
                pageList: [20, 30, 40, 50,500,5000],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        }
    };
}();