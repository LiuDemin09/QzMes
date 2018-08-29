var TrackingWipQuery = function () {
    return {
        grid: null,
        workorderNo: null,
        OrderNumber:null,
        PartsdrawingCode:null,
        station:null,
        StartTime:null,
        EndTime: null,
        //main function to initiate the module
        init: function () {
            
            //初始化订单单号
            //WsSystem.ListBindOrderNo(function (result) {
            //    JeffComm.fillSelect($("#selOrderNo"), result, true);
            //});
           
        },
        bindGrid: function () {
            TrackingWipQuery.workorderNo = $('#txtWorkOrder').val().trim();             
            TrackingWipQuery.PartsdrawingCode = $('#txtPartsDrawingNo').val().trim();
            TrackingWipQuery.station = $('#selStation').val();
            TrackingWipQuery.StartTime = $('#txtStartTime').val().trim();
            TrackingWipQuery.EndTime = $('#txtEndTime').val().trim();
            var oDate1 = new Date(TrackingWipQuery.StartTime);
            var oDate2 = new Date(TrackingWipQuery.EndTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', {
                workorder: TrackingWipQuery.workorderNo,  PartsdrawingCode: TrackingWipQuery.PartsdrawingCode,
                station: TrackingWipQuery.station, StartTime: TrackingWipQuery.StartTime, EndTime: TrackingWipQuery.EndTime
            });
        },
       
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '条码追溯查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QuerySNTrackingWIP',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                remoteSort: false,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,                
                columns: [[ //选择
                     { title: '产品条码', field: 'PSN', width: TrackingWipQuery.fixWidth(0.09), sortable: true },
                     { title: '物料条码', field: 'MSN', width: TrackingWipQuery.fixWidth(0.08), sortable: true },
                     { title: '工单单号', field: 'WorkOrder', width: TrackingWipQuery.fixWidth(0.09),sortable:true },
                     { title: '零件图号', field: 'PartsdrawingCode', width: TrackingWipQuery.fixWidth(0.09), sortable: true },
                     { title: '批次号', field: 'BatchNumber', width: TrackingWipQuery.fixWidth(0.08) },
                     { title: '工站', field: 'StationName', width: TrackingWipQuery.fixWidth(0.07), sortable: true },
                     { title: '状态', field: 'STATUS', width: TrackingWipQuery.fixWidth(0.05), sortable: true },
                     { title: '工时', field: 'TaskTime', width: TrackingWipQuery.fixWidth(0.1) },
                     { title: '下一站', field: 'NextStation', width: TrackingWipQuery.fixWidth(0.05) },                     
                     {
                         title: '创建时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: TrackingWipQuery.fixWidth(0.1),sortable:true
                     },
                      
                     { title: '操作人', field: 'UpdatedBy', width: TrackingWipQuery.fixWidth(0.1) }
                     
                ]],
                toolbar: [{
                    text: '工单单号 <input type="text" id="txtWorkOrder"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtPartsDrawingNo"/>'
                }, '-', {
                    text: '工站 <select tabindex="-1" name="selStation" id="selStation"><option value="">请选择</option><option value="PRINT">打印</option> <option value="CHEXI">车铣</option><option value="QIANGONG">钳工</option><option value="QC">终检</option><option value="INSTOCK">入库</option><option value="OUTSTOCK">出库</option></select>'
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
                        TrackingWipQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/WorkOrderManage/export/TrackingWipExport.aspx?workorder=' + $('#txtWorkOrder').val() + '&starttime=' + $('#txtStartTime').val()
                    + '&endtime=' + $('#txtEndTime').val() + '&station=' + $('#selStation').val()
                    + '&partsdrawingno=' + $('#txtPartsDrawingNo').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "')}
                    }, 
                '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "WorkOrderMain.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    TrackingWipQuery.workorderNo = rowData["WO"];
                    // ShowEditOrViewDialog();
                },
               // loader: function (param, success, error) { }
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