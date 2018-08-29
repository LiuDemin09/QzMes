var BossQuery = function () {
    return {
        grid: null, 
        init: function () {
           // var sec = 0;
            //function Reflash() {
            //    BossQuery.bindGrid();
            //    // $('#dg').datagrid('reload');
            //};
            //setInterval("BossQuery.bindGrid()", 10000);//每隔10秒刷新一次
           
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGrid: function () {
            $('#grid').datagrid('load', {});
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryWIPInfo',
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
                pageList: [15, 10, 12],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择                    
                    //{ title: 'ID', field: 'ID', width: BossQuery.fixWidth(0.09), hidden: 'true' },
                   { title: '工单单号', field: 'WO', width: BossQuery.fixWidth(0.1) },
                   { title: '零件图号', field: 'PartsdrawingCode', width: BossQuery.fixWidth(0.1) },
                   { title: '产品名称', field: 'ProductName', width: BossQuery.fixWidth(0.08) },
                   { title: '计划数量', field: 'PlanQuantity', width: BossQuery.fixWidth(0.05) },
                   { title: '车铣良品数', field: 'CheXiPassCount', width: BossQuery.fixWidth(0.06) },
                   { title: '车铣不良数', field: 'CheXiFailCount', width: BossQuery.fixWidth(0.06) },
                   { title: '钳工良品数', field: 'QianGongPassCount', width: BossQuery.fixWidth(0.06) },
                   { title: '钳工不良数', field: 'QianGongFailCount', width: BossQuery.fixWidth(0.06) },
                    { title: '检验良品数', field: 'QCPassCount', width: BossQuery.fixWidth(0.06) },
                    { title: '检验不良数', field: 'QCFailCount', width: BossQuery.fixWidth(0.06) },
                   { title: '入库数量', field: 'InStockQty', width: BossQuery.fixWidth(0.05) }
                ]]
            });
        }
         
    };

}();