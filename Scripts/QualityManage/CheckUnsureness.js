var CheckUnsureness = function () {

    return {
        grid: null,
        PSN: null,
        Partsdrawing: null,
        init: function () {
        },
        bindGrid: function () {
            CheckUnsureness.PSN = $("#txtPSN").val(); 
            $('#grid').datagrid('load', { PSN: CheckUnsureness.PSN });

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
           // alert("审核完毕", "提示");
            $("#grid").datagrid("reload"); 
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
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
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                    { field: 'ck', checkbox: true },
                    //{ title: 'ID', field: 'ID', width: CheckUnsureness.fixWidth(0.09), hidden: 'true' },
                   { title: '产品条码', field: 'PSN', width: CheckUnsureness.fixWidth(0.1) },
                   { title: '工单号码', field: 'WorkOrder', width: CheckUnsureness.fixWidth(0.1) },
                   { title: '不良代码', field: 'FailCode', width: CheckUnsureness.fixWidth(0.05) },
                   { title: '不良项', field: 'FailMemo', width: CheckUnsureness.fixWidth(0.08) },
                   { title: '状态', field: 'MEMO', width: CheckUnsureness.fixWidth(0.06) },
                   { title: '加工工序', field: 'StationName', width: CheckUnsureness.fixWidth(0.05) },
                   { title: '数量', field: 'QUANTITY', width: CheckUnsureness.fixWidth(0.06) },
                   { title: '来料条码', field: 'MSN', width: CheckUnsureness.fixWidth(0.1) },
                   { title: '工件图号', field: 'PartsdrawingCode', width: CheckUnsureness.fixWidth(0.1) },
                   { title: '工件名称', field: 'ProductName', width: CheckUnsureness.fixWidth(0.09) },
                   { title: '备注', field: 'BatchNumber', width: CheckUnsureness.fixWidth(0.09) },
                   { title: '操作人', field: 'UpdatedBy', width: CheckUnsureness.fixWidth(0.05) },
                   {
                       title: '时间', field: 'UpdatedDate', formatter: function (value, row, index) {
                           if (value != null & value != "") {
                               var unixTimestamp = new Date(value);
                               return unixTimestamp.toLocaleString();
                           }
                       }, width: CheckUnsureness.fixWidth(0.1)
                   }
                ]],
                toolbar: [{
                    text: '产品条码 <input type="text" id="txtPSN"/>'
                },  '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        CheckUnsureness.bindGrid();
                    }
                },
                '-', {
                    id: 'btnConfirm',
                    text: '审核',
                    iconCls: 'icon-ok',
                    handler: function () {
                        CheckUnsureness.CheckUnsurenessOK();
                    }
                }, '-', {
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
        }
    };

}();