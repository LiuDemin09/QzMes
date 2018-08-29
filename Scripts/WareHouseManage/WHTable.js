var WHTable = function () {
    return {
        init: function () {
            setInterval("WHTable.bindGrid()", 30000);//每隔30秒刷新一次
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGrid: function () {
            $('#grid').datagrid('load', {
                workorder: $('#txtQworkorder').val()
                , partsdrawingno: $('#txtQpartsdrawingcode').val()
            });
            $('#grid1').datagrid('load', {});
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '备料任务',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindPrepareMar',
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
                     { title: '工单号码', field: 'WO', width: WHTable.fixWidth(0.1) },
                     { title: '物料名称', field: 'PartsdrawingCode', width: WHTable.fixWidth(0.1) },
                     { title: '钦纵料号', field: 'MEMO', width: WHTable.fixWidth(0.1) },
                     { title: '炉批号', field: 'BatchNumber', width: WHTable.fixWidth(0.1) },
                     { title: '购货单位', field: 'CustName', width: WHTable.fixWidth(0.1) },
                     { title: '收货仓库', field: 'MachineName', width: WHTable.fixWidth(0.1) },
                     { title: '需求数量', field: 'PlanQuantity', width: WHTable.fixWidth(0.07) },
                     { title: '库存数量', field: 'QUANTITY', width: WHTable.fixWidth(0.07) },
                     { title: '已发数量', field: 'MaterialQty', width: WHTable.fixWidth(0.07) },
                ]],
                toolbar: [{
                    text: '工单号码<input type="text" id="txtQworkorder"/>'
                }, '-', {
                    text: '零件图号<input type="text" id="txtQpartsdrawingcode"/>'
                }, '-',
                    {
                        id: 'btnQuery',
                        text: '查询',
                        iconCls: 'icon-search',
                        handler: function () {
                            //实现刷新栏目中的数据
                            WHTable.bindGrid();
                        }
                    }, '-',
                    {
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid").datagrid("reload");
                    }
                }],


            });

            $('#grid1').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '出库任务',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/FindOutWH',
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
                     { title: '订单号码', field: 'OrderNo', width: WHTable.fixWidth(0.1) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: WHTable.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: WHTable.fixWidth(0.1) },                     
                      {
                          title: '本次出货时间', field: 'ThisOutTime', formatter: function (value, row, index) {
                              if (value != null & value != "") {
                                  var unixTimestamp = new Date(value);
                                  return unixTimestamp.toLocaleString();
                              }
                          }, width: WHTable.fixWidth(0.1)
                      },
                     { title: '订单数量', field: 'OrderQuantity', width: WHTable.fixWidth(0.1) },
                     { title: '入库数量', field: 'InQuantity', width: WHTable.fixWidth(0.07) },
                     { title: '出库数量', field: 'OutQuantity', width: WHTable.fixWidth(0.07) },
                     { title: '库存数量', field: 'MEMO', width: WHTable.fixWidth(0.07) },
                ]],
                toolbar: [{
                    id: 'btnReload1',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid1").datagrid("reload");
                    }
                }],
                onDblClickRow: function (rowIndex, rowData) {
                    location.href = "OutWH.aspx?" + "orderNo=" + rowData.OrderNo + "&partNo=" + rowData.PartsdrawingCode;
                }

            });



        }

    };

}();