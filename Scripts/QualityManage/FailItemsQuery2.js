var FailItemsQuery = function () {
    return {
        grid: null,
        FailItemID: null,
        FailCode: null,
        FailType: null,
        //main function to initiate the module
        init: function () {
            //初始化不良类别
            WsSystem.ListBindFailType(function (result) {
                JeffComm.fillSelect($("#selFailType"), result, true);               
            });
            
        },
        bindGrid: function () {
            FailItemsQuery.FailCode = $("#txtFailCode").val();
            FailItemsQuery.FailType = $("#selFailType").val();
            $('#grid').datagrid('load', { FailCode: FailItemsQuery.FailCode, FailType: FailItemsQuery.FailType });
        },
         
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '不良项查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryFailItems',
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
                     { title: '不良项', field: 'FailCode', width: FailItemsQuery.fixWidth(0.15) },
                     { title: '不良类型', field: 'FailType', width: FailItemsQuery.fixWidth(0.15) },
                     { title: '不良描述', field: 'FailMemo', width: FailItemsQuery.fixWidth(0.15) },
                     { title: '操作人', field: 'UpdatedBy', width: FailItemsQuery.fixWidth(0.15) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: FailItemsQuery.fixWidth(0.15)
                     },
                ]],
                toolbar: [{
                    text: '不良代码 <input type="text" id="txtFailCode"/>'
                }, '-', {
                    text: '不良类型<select tabindex="-1" name="selFailType" id="selFailType"></select>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        FailItemsQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/QualityManage/export/FailItemsExport.aspx?failcode=' + $('#txtFailCode').val()
                    + '&failtype=' + $('#selFailType').val() 
                    + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "QualityMain.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    FailItemsQuery.FailItemID = rowData["FailCode"];
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