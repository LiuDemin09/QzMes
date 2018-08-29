var TechnologyTable = function () {
    return {
        init: function () {
            setInterval("TechnologyTable.bindGrid()", 30000);//每隔30秒刷新一次
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGrid: function () {
            $('#grid').datagrid('load', {Status:"1,9"});            
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryTechnologyEngineerTask',
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
                      { title: 'ID', field: 'ID', width: TechnologyTable.fixWidth(0.1), hidden: 'true' },
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: TechnologyTable.fixWidth(0.1), sortable: true },
                     { title: '客户名称', field: 'CustName', width: TechnologyTable.fixWidth(0.1), sortable: true },
                     { title: '产品名称', field: 'ProductName', width: TechnologyTable.fixWidth(0.1), sortable: true },
                     { title: '状态', field: 'StatusMemo', width: TechnologyTable.fixWidth(0.1), sortable: true },
                     { title: '工艺工程师', field: 'ProcessName', width: TechnologyTable.fixWidth(0.1), sortable: true },
                    // { title: '编程工程师', field: 'ProgramName', width: TechnologyQuery.fixWidth(0.1), sortable: true },
                     //{ title: '工艺文件名', field: 'ProcessFname', width: TechnologyQuery.fixWidth(0.1) },
                     {
                         title: '工艺计划完成时间', field: 'PlanDate', formatter: function (value, row, index) {
                             if (value == null) return null;
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: TechnologyTable.fixWidth(0.15)
                     },
                     { title: '操作人', field: 'UpdatedBy', width: TechnologyTable.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: TechnologyTable.fixWidth(0.15), sortable: true
                     }
                ]],
                toolbar: [{
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid").datagrid("reload");
                    }
                }],
            });

            
        }
    };

}();