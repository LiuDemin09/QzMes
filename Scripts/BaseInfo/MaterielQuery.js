var MaterielQuery = function () {
    return {
        grid: null,
        custmaterielNo: null,
        qzmateriel: null,
        custmateriel: null,
        //main function to initiate the module
        init: function () {
            
        },
        bindGrid: function () {
            MaterielQuery.qzmateriel = $("#txtQZMateriel").val().trim();
            MaterielQuery.custmateriel = $("#txtCustMateriel").val().trim();
            $('#grid').datagrid('load', { qzMateriel: MaterielQuery.qzmateriel, custMateriel: MaterielQuery.custmateriel });
        }, 
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '料号查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryMaterielInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                columns: [[ //选择
                     { title: '钦纵料号', field: 'QPARTNO', width: MaterielQuery.fixWidth(0.1) },
                     { title: '客户料号', field: 'CPARTNO', width: MaterielQuery.fixWidth(0.1) },
                     { title: '料号名称', field: 'NAME', width: MaterielQuery.fixWidth(0.1) },
                     { title: '基板数', field: 'BasQty', width: MaterielQuery.fixWidth(0.1) },
                     { title: '客户名称', field: 'CUSTOMER', width: MaterielQuery.fixWidth(0.1) },
                     { title: '备注', field: 'MEMO', width: MaterielQuery.fixWidth(0.1) },
                     { title: '操作人', field: 'UpdatedBy', width: MaterielQuery.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: MaterielQuery.fixWidth(0.1)
                     },
                ]],
                toolbar: [{
                    text: '客户料号 <input type="text" id="txtCustMateriel"/>'
                }, '-', {
                    text: '钦纵料号 <input type="text" id="txtQZMateriel"/>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        MaterielQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/BaseInfo/export/MaterielExport.aspx?qmateriel=' + $('#txtQZMateriel').val()
                    + '&cmateriel=' + $('#txtCustMateriel').val()
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
                    MaterielQuery.custmaterielNo = rowData["CPARTNO"];
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