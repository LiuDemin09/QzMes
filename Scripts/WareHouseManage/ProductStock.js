var ProductStock = function () {

    return {
        workOrder: null,
        productName: null,
        batch: null,
        status: null,
        startTime: null,
        endTime: null,
        init: function () {
         

        },
        bindGrid: function () {
            ProductStock.workOrder = $("#txtWorkOrder").val();
            ProductStock.productName = $("#txtProductName").val();
            ProductStock.batch = $("#txtBatch").val();
            ProductStock.status = $("#txtStatus").val();
            ProductStock.startTime = $("#txtStartTime").val();
            ProductStock.endTime = $("#txtEndTime").val();

            $('#grid').datagrid('load', {
                workOrder: ProductStock.workOrder, productName: ProductStock.productName,
                batch: ProductStock.batch, status: ProductStock.status,
                startTime: ProductStock.startTime, endTime: ProductStock.endTime
            });

        },
       
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '成品查询',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/QueryProductINOut',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                  pageSize: 25,
                  pageNumber: 1,
                  pageList: [15, 25, 30, 40, 50],
                  beforePageText: '第',//页数文本框前显示的汉字   
                  afterPageText: '页    共 {pages} 页',
                  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '产品条码', field: 'PSN', width: ProductStock.fixWidth(0.11) },
                     { title: '工单号码', field: 'WorkOrder', width: ProductStock.fixWidth(0.11) },
                     { title: '交货单位', field: 'MANUFACTURE', width: ProductStock.fixWidth(0.08) },
                     { title: '单据编号', field: 'DOCUMENTID', width: ProductStock.fixWidth(0.08) },
                     { title: '收货仓库', field: 'StockHouse', width: ProductStock.fixWidth(0.08) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: ProductStock.fixWidth(0.08) },
                     { title: '单位', field: 'UNIT', width: ProductStock.fixWidth(0.05) },
                     { title: '数量', field: 'QUANTITY', width: ProductStock.fixWidth(0.05) },
                     { title: '备注', field: 'BatchNumber', width: ProductStock.fixWidth(0.08) },
                     { title: '质量编号', field: 'QualityCode', width: ProductStock.fixWidth(0.08) },
                     { title: '保管员', field: 'UpdatedBy', width: ProductStock.fixWidth(0.06) },
                     { title: '移交人', field: 'FromBy', width: ProductStock.fixWidth(0.06) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: ProductStock.fixWidth(0.08)
                     }
                ]],
                toolbar: [{
                    text: '工单号码 <input type="text" id="txtWorkOrder" />'
                }, '-', {
                    text: '产品名称 <input type="text" id="txtProductName" />'
                }, '-', {
                    text: '备注 <input type="text" id="txtBatch" />'
                }, '-', {
                    text: '成品状态 <select tabindex="-1" name="txtStatus" id="txtStatus"><option value="4">请选择</option> <option value="0">入库</option><option value="1">出库</option> <option value="2">报废</option><option value="3">库存</option> </select>'
                }, '-', {
                     text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间 <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        ProductStock.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/WareHouseManage/export/ProductExport.aspx?workorder=' + $('#txtWorkOrder').val() + '&starttime=' + $('#txtStartTime').val()
                     + '&endtime=' + $('#txtEndTime').val() + '&status=' + $('#txtStatus').val() + '&productname=' + $('#txtProductName').val()
                     + '&batchnumber=' + $('#txtBatch').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "WHTable.aspx";
                    }
                }],
            });
            $('#grid').datagrid('getPager').pagination({                
                pageSize: 25,
                pageNumber: 1,
                pageList: [10,15, 25, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
               // layout:['refresh']
            });
            

        }

    };

}();