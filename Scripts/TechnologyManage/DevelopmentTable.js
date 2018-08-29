var DevelopmentTable = function () {
    return {
        init: function () {
            setInterval("DevelopmentTable.bindGrid()", 300000);//每隔300秒刷新一次
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGrid: function () {
            $('#grid').datagrid('load', {Status:"4,10"});            
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryTechnologyEngineerTask',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                queryParams: { Status: "4,10" },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 20,
                pageNumber: 1,
                pageList: [ 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                      //{ title: 'ID', field: 'ID', width: DevelopmentTable.fixWidth(0.1), hidden: 'true' },
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: DevelopmentTable.fixWidth(0.1), sortable: true },
                     { title: '客户名称', field: 'CustName', width: DevelopmentTable.fixWidth(0.1), sortable: true },
                     { title: '产品名称', field: 'ProductName', width: DevelopmentTable.fixWidth(0.1), sortable: true },
                     { title: '状态', field: 'StatusMemo', width: DevelopmentTable.fixWidth(0.1), sortable: true },
                     { title: '工艺工程师', field: 'ProcessName', width: DevelopmentTable.fixWidth(0.1), sortable: true },
                     { title: '编程工程师', field: 'ProgramName', width: DevelopmentTable.fixWidth(0.1), sortable: true },
                     { title: '工艺文件路径', field: 'ProcessPath', width: DevelopmentTable.fixWidth(0.06), hidden: 'true' },
                     {
                         title: '工艺文件名', field: 'ProcessFname', width: DevelopmentTable.fixWidth(0.1),
                         formatter: function (value, row, index) {
                             return '<a style="color:blue" href="' + row.ProcessPath + '", target="_blank">' + row.ProcessFname + '</a>';
                             // alert(row.ProcessPath);
                         }
                     },
                     {
                         title: '计划完成时间', field: 'DevplanDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: DevelopmentTable.fixWidth(0.15)
                     },
                     { title: '操作人', field: 'UpdatedBy', width: DevelopmentTable.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: DevelopmentTable.fixWidth(0.15), sortable: true
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