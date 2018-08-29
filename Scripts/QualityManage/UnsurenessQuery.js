var UnsurenessQuery = function () {
    return {
        grid: null,
        psn: null,
        partsdrawing: null,
        wo: null,
        status: null,
        startTime: null,
        endTime: null,
        //main function to initiate the module
        init: function () {            
            //初始化客户名称
            //WsSystem.ListBindCustName(function (result) {
            //    JeffComm.fillSelect($("#selCustName"), result, true);
            //});
        },
        bindGrid: function () {
            UnsurenessQuery.psn = $("#txtPSN").val();
            UnsurenessQuery.partsdrawing = $("#txtPartsdrawing").val();
            UnsurenessQuery.wo = $("#txtWO").val();
            UnsurenessQuery.status = $("#selStatus").val();
            UnsurenessQuery.startTime =  $("#txtStartTime").val();
            UnsurenessQuery.endTime = $("#txtEndTime").val();
            $('#grid').datagrid('load', { psn: UnsurenessQuery.psn, partsdrawing: UnsurenessQuery.partsdrawing, wo: UnsurenessQuery.wo, status: UnsurenessQuery.status, startTime: UnsurenessQuery.startTime, endTime: UnsurenessQuery.endTime });
        },

       
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '待处理品查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryUnsurenessHistory',
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
                     { title: '产品条码', field: 'PSN', width: UnsurenessQuery.fixWidth(0.1) },
                     { title: '工单号码', field: 'WorkOrder', width: UnsurenessQuery.fixWidth(0.1) },                     
                     { title: '不良项', field: 'FailMemo', width: UnsurenessQuery.fixWidth(0.1) },
                     { title: '状态', field: 'MEMO', width: UnsurenessQuery.fixWidth(0.08) },
                     { title: '加工工序', field: 'StationName', width: UnsurenessQuery.fixWidth(0.06) },
                     { title: '数量', field: 'QUANTITY', width: UnsurenessQuery.fixWidth(0.05) },
                     { title: '来料条码', field: 'MSN', width: UnsurenessQuery.fixWidth(0.1) },
                     { title: '工件名称', field: 'ProductName', width: UnsurenessQuery.fixWidth(0.08) },
                     { title: '工件图号', field: 'PartsdrawingCode', width: UnsurenessQuery.fixWidth(0.1) },
                     { title: '批号', field: 'BatchNumber', width: UnsurenessQuery.fixWidth(0.1) },
                      { title: '操作人', field: 'UpdatedBy', width: UnsurenessQuery.fixWidth(0.05) },
                       {
                           title: '时间', field: 'UpdatedDate',formatter: function (value, row, index) {
                               if (value != null & value != "") {
                                   var unixTimestamp = new Date(value);
                                   return unixTimestamp.toLocaleString();
                               }
                           }, width: UnsurenessQuery.fixWidth(0.1)
                       },
                ]],
                toolbar: [{
                    text: '产品条码 <input type="text" id="txtPSN"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtPartsdrawing"/>'
                }, '-', {
                    text: '工单号码 <input type="text" id="txtWO"/>'
                }, '-', {
                    text: '状态 <select tabindex="-1" name="selStatus" id="selStatus"><option value="">请选择</option><option value="0">待处理</option><option value="1">返工</option><option value="2">让步接收</option><option value="3">报废</option><option value="4">已审核</option> </select>'
                }, '-', {
                    text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间 <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        UnsurenessQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/QualityManage/export/UnsurenessQueryExport.aspx?psn=' + $('#txtPSN').val() + '&partsdrawing=' + $('#txtPartsdrawing').val()
                    + '&wo=' + $('#txtWO').val() + '&status=' + $('#selStatus').val() + '&starttime=' + $('#txtStartTime').val() + '&endtime=' + $('#txtEndTime').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "QualityMain.aspx";
                    }
                }],
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