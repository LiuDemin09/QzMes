var SquenceQuery = function () {

    return {
        grid: null,
        squenceName: null,
        init: function () {
           

        },
        bindGrid: function () {
            SquenceQuery.squenceName = $("#txtSquenceName").val().trim();
            $('#grid').datagrid('load', { squenceName: SquenceQuery.squenceName });

        },
       
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '序列号查询',
                iconCls: 'icon-view',
                url: '../../services/WsBasic.asmx/FindSquenceByCode',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
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
                     { title: '序列号名称', field: 'SeqName', width: SquenceQuery.fixWidth(0.13) },
                     { title: '产品系列', field: 'FAMILY', width: SquenceQuery.fixWidth(0.12) },
                     { title: '数字长度', field: 'DigitalLen', width: SquenceQuery.fixWidth(0.12) },
                     { title: '数字类型', field: 'DigitalTypeMemo', width: SquenceQuery.fixWidth(0.12) },
                     { title: '增长模式', field: 'IncreaseModeMemo', width: SquenceQuery.fixWidth(0.12) },
                     { title: '操作人', field: 'UpdatedBy', width: SquenceQuery.fixWidth(0.13) },
                     {
                         title: '时间', field: 'CreatedDate',formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: SquenceQuery.fixWidth(0.13)
                     },
                ]],
                toolbar: [{
                    text: '序列号名称<input type="text"  id="txtSquenceName"/>'
                }, '-',  {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        SquenceQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/BaseInfo/export/SequenceExport.aspx?squenceName=' + $('#txtSquenceName').val()
                    + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
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