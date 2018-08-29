var QualityMain = function () {
    return {
        grid: null,
        gridyield: null,
        PSN: null,
        Partsdrawing: null,
        init: function () {
            setInterval("QualityMain.bindGrid()", 300000);//每隔5分钟刷新一次
            setInterval("QualityMain.bindGridYield()", 60000);//每隔60秒刷新一次
        },
       
        CheckUnsurenessOK: function () {
            var checkedItems = $('#grid').datagrid('getChecked');
            var names = [];
            $.each(checkedItems, function (index, item) {
                WsSystem.SaveCheckUnsureness(item.PSN, "1",
                                          function (result) {
                                          });
            });
            JeffComm.alertSucc("审核完毕", 500);
            //alert("审核完毕", "提示");
            $("#grid").datagrid("reload");
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGrid: function () {
            QualityMain.PSN = $("#txtPSN").val();
            $('#grid').datagrid('load', { PSN: QualityMain.PSN });
        },

        bindGridYield: function () { 
            $('#gridyield').datagrid('load', { custname: "", partsdrawing: "", startT: "", endT:"" });
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryCheckUnsureness',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                // singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 6,
                pageNumber: 1,
                pageList: [6,10, 12],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                    { field: 'ck', checkbox: true },
                    //{ title: 'ID', field: 'ID', width: QualityMain.fixWidth(0.09), hidden: 'true' },
                   { title: '产品条码', field: 'PSN', width: QualityMain.fixWidth(0.1) },
                   { title: '工单号码', field: 'WorkOrder', width: QualityMain.fixWidth(0.1) },
                   { title: '不良代码', field: 'FailCode', width: QualityMain.fixWidth(0.05) },
                   { title: '不良项', field: 'FailMemo', width: QualityMain.fixWidth(0.08) },
                   { title: '状态', field: 'MEMO', width: QualityMain.fixWidth(0.06) },
                   { title: '加工工序', field: 'StationName', width: QualityMain.fixWidth(0.05) },
                   { title: '数量', field: 'QUANTITY', width: QualityMain.fixWidth(0.06) },
                   { title: '来料条码', field: 'MSN', width: QualityMain.fixWidth(0.1) },
                   { title: '工件图号', field: 'PartsdrawingCode', width: QualityMain.fixWidth(0.1) },
                   { title: '工件名称', field: 'ProductName', width: QualityMain.fixWidth(0.06) },
                   { title: '生产批号', field: 'BatchNumber', width: QualityMain.fixWidth(0.09) },
                   { title: '操作人', field: 'UpdatedBy', width: QualityMain.fixWidth(0.05) },
                   {
                       title: '时间', field: 'UpdatedDate',formatter: function (value, row, index) {
                           if (value != null & value != "") {
                               var unixTimestamp = new Date(value);
                               return unixTimestamp.toLocaleString();
                           }
                       }, width: QualityMain.fixWidth(0.1)
                   }
                ]],
                toolbar: [{
                    text: '产品条码 <input type="text" id="txtPSN"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        QualityMain.bindGrid();
                    }
                },
                '-', {
                    id: 'btnConfirm',
                    text: '审核',
                    iconCls: 'icon-ok',
                    handler: function () {
                        QualityMain.CheckUnsurenessOK();
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
                }],
                    onLoadError: function (error) {
                        alert(error.responseText);
                    }
            });
        },
        initGridYield: function () {
            $('#gridyield').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryYieldInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                autoRowHeight: false,
                // singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 6,
                pageNumber: 1,
                pageList: [6,10, 12],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择                    
                   { title: '工件图号', field: 'PartsdrawingCode', width: QualityMain.fixWidth(0.1) },
                   { title: '工件名称', field: 'ProductName', width: QualityMain.fixWidth(0.1) },
                   { title: '产出数量', field: 'QUANTITY', width: QualityMain.fixWidth(0.1) },
                   { title: '合格数量', field: 'PassCount', width: QualityMain.fixWidth(0.1) },
                   { title: '不良数量', field: 'FailCount', width: QualityMain.fixWidth(0.1) },
                   { title: '合格率', field: 'PassRate', width: QualityMain.fixWidth(0.1) },
                   { title: '不良率', field: 'FailRate', width: QualityMain.fixWidth(0.1) },
                    
                ]]
                
            });
        }
    };

}();