var SNTracking = function () {
    return {
        grid: null,       
        startTime: null,
        endTime: null,
        sn:null,
        init: function () {
            jQuery('#txtSN').focus();
            
            
        },
        bindGrid: function () {
            SNTracking.sn = $("#txtSN").val().trim().toUpperCase();
            
            $('#grid').datagrid('load', { sn: SNTracking.sn});

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '条码查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QuerySNTrackingInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                remoteSort: false,
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                    { title: '产品条码', field: 'PSN', width: SNTracking.fixWidth(0.1), sortable: true },
                   //{ title: '来料条码', field: 'MSN', width: SNTracking.fixWidth(0.1) },
                   { title: '工单单号', field: 'WorkOrder', width: SNTracking.fixWidth(0.1), sortable: true },
                   { title: '零件图号', field: 'PartsdrawingCode', width: SNTracking.fixWidth(0.1), sortable: true },
                   { title: '工序号', field: 'StationId', width: SNTracking.fixWidth(0.08), sortable: true },
                   { title: '工序', field: 'StationName', width: SNTracking.fixWidth(0.08), sortable: true },
                   { title: '下一序', field: 'NextStation', width: SNTracking.fixWidth(0.08), sortable: true },
                   { title: '下序号', field: 'NextStationId', width: SNTracking.fixWidth(0.08), sortable: true },
                   {
                       title: '状态', field: 'STATUS', formatter: function (value, row, index) {
                           if (value == "P") {
                               return "成功";
                           } else {
                               return "失败";
                           }
                       }, width: SNTracking.fixWidth(0.06)
                   },
                    { title: '状态描述', field: 'MEMO', width: SNTracking.fixWidth(0.06) },
                   { title: '失败描述', field: 'EXCEPTION', width: SNTracking.fixWidth(0.08) },
                   { title: '机床名称', field: 'MachineName', width: SNTracking.fixWidth(0.1), sortable: true },
                   { title: '产品名称', field: 'PartsName', width: SNTracking.fixWidth(0.08), sortable: true },
                    {
                        title: '生产工时', field: 'TaskTime', formatter: function (value, row, index) {
                            if(value!=null&&value!="")
                            {
                                var tempTask = value.substr(0,8);                                 
                                return tempTask;
                            }
                        }, width: SNTracking.fixWidth(0.06)
                    },
                   {
                       title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                           if (value != null & value != "") {
                           var unixTimestamp = new Date(value);
                           return unixTimestamp.toLocaleString();}
                       }, width: SNTracking.fixWidth(0.1), sortable: true
                   },
                   { title: '备注', field: 'BatchNumber', width: SNTracking.fixWidth(0.1) },
                   //{ title: '负责人', field: 'UpdatedBy', width: SNTracking.fixWidth(0.05) },
                   { title: '数量', field: 'QUANTITY', width: SNTracking.fixWidth(0.05) },
                   { title: '操作人', field: 'UpdatedBy', width: SNTracking.fixWidth(0.05) },
                ]],
                toolbar: [{
                    text: '条码 <input type="text" id="txtSN"/>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        SNTracking.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/ProductionManage/SNTrackingExport.aspx?sn=' + $('#txtSN').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }],
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
      

        
        $('#txtSN').keydown(function (e) {                
        if (e.which == 13)
        {
            SNTracking.bindGrid();
        }
        });

        }

    };
}();