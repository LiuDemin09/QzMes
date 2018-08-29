var EquipmentQuery = function () {
    return {
        grid: null,
        equipNo: null,
        equipName: null,
        //main function to initiate the module
        init: function () {  
        },
        bindGrid: function () {
            EquipmentQuery.equipName = $("#txtEquipName").val().trim();
            $('#grid').datagrid('load', { equipName: EquipmentQuery.equipName });
        },
       
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '设备查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryEquipmentInfo',
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
                     { title: '设备编号', field: 'CODE', width: EquipmentQuery.fixWidth(0.08) },
                     { title: '国别厂家', field: 'COMPANY', width: EquipmentQuery.fixWidth(0.12) },
                     { title: '设备名称', field: 'MachineName', width: EquipmentQuery.fixWidth(0.08) },
                     { title: '机床类型', field: 'MachineType', width: EquipmentQuery.fixWidth(0.08) },
                     { title: '轴数', field: 'AxisNumber', width: EquipmentQuery.fixWidth(0.03) },
                     { title: '型号', field: 'MODEL', width: EquipmentQuery.fixWidth(0.08) },
                     { title: '功率', field: 'POWER', width: EquipmentQuery.fixWidth(0.05) },
                     { title: '车间位置', field: 'LOCATION', width: EquipmentQuery.fixWidth(0.05) },
                     { title: '状态', field: 'STATUS', width: EquipmentQuery.fixWidth(0.05) },
                     { title: '出厂编号', field: 'OutCode', width: EquipmentQuery.fixWidth(0.08) },
                     {
                         title: '启用日期', field: 'UseDate',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: EquipmentQuery.fixWidth(0.08)
                     },
                     { title: '操作人', field: 'UpdatedBy', width: EquipmentQuery.fixWidth(0.08) },
                     {
                         title: '时间', field: 'CreatedDate',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: EquipmentQuery.fixWidth(0.08)
                     },
                ]],
                toolbar: [{
                    text: '设备名称 <input type="text" id="txtEquipName"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        EquipmentQuery.bindGrid();
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
                onClickRow: function (rowIndex, rowData) {
                    EquipmentQuery.equipNo = rowData["CODE"];
                    // ShowEditOrViewDialog();
                },
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