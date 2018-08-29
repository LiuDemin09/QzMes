var BasCodeQuery = function () {

    return {
        grid: null,
        bascodeid: null,
        codename: null,
        //main function to initiate the module
        init: function () {
           
            jQuery('#btnAddCust').click(function () {
                BasCodeQuery.bascodeid = null;
                $('#restartDialog').window('open');
            });
             
        },

        bindGrid: function () {
            BasCodeQuery.codename = $("#txtCodeName").val().trim();
            $('#grid').datagrid('load', { codeName: BasCodeQuery.codename });
        },
         
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '编码查询',
                iconCls: 'icon-view',
                url: '../../services/WsBasic.asmx/LisBasCode',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',

                columns: [[ //选择
                     { title: 'ID', field: 'ID', width: BasCodeQuery.fixWidth(0.05), hidden: 'true' },
                     { title: '编码名称', field: 'NAME', width: BasCodeQuery.fixWidth(0.09) },
                     { title: '编码类型', field: 'TYPE', width: BasCodeQuery.fixWidth(0.09) },
                     { title: '前缀字符', field: 'PREFIX', width: BasCodeQuery.fixWidth(0.09) },
                     { title: '日期格式', field: 'DateFormat', width: BasCodeQuery.fixWidth(0.09) },
                     { title: '绑定序列', field: 'BindSequence', width: BasCodeQuery.fixWidth(0.09) },
                     { title: '编码长度', field: 'CodeLen', width: BasCodeQuery.fixWidth(0.09) },
                     { title: '操作人', field: 'UpdatedBy', width: BasCodeQuery.fixWidth(0.09) },
                     {
                         title: '时间', field: 'CreatedDate',formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: BasCodeQuery.fixWidth(0.12)
                     }
                ]],
                toolbar: [{
                    text: '编码名称 <input type="text" id="txtCodeName" />'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        BasCodeQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/BaseInfo/export/BasCodeExport.aspx?codename=' + $('#txtCodeName').val()
                    + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
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
                onClickRow: function (rowIndex, rowData) {
                    BasCodeQuery.bascodeid = rowData["ID"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
        }
    };

}();