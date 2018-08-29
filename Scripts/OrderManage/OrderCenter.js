var OrderCenter = function () {

    return {
        grid: null,
        OrderNo: null,
        Partsdrawing: null,
        startTime: null,
        endTime:null,
        orderStatus:null,
        init: function () {

            setInterval("OrderCenter.bindGrid()", 30000);//每隔30秒刷新一次
        },
        bindGrid: function () {
            OrderCenter.OrderNo = $("#txtOrderNo").val().trim();
            OrderCenter.Partsdrawing = $("#txtPartCode").val().trim();
            OrderCenter.orderStatus = $("#selOrderStatus").val();
            OrderCenter.startTime = $("#txtStartTime").val().trim();
            OrderCenter.endTime = $("#txtEndTime").val().trim();
            var oDate1 = new Date(OrderCenter.startTime);
            var oDate2 = new Date(OrderCenter.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', {
                orderNo: OrderCenter.OrderNo
                , partsdrawing: OrderCenter.Partsdrawing
                , startTime: OrderCenter.startTime
                , endTime: OrderCenter.endTime
                , OrderStatus: OrderCenter.orderStatus
                ,contract:$("#txtContractNo").val()
            });

        },
    
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        CloseOrder: function () {
            //var rows = $('#grid').datagrid('getSelections');  //得到所选择的行            
            //WsSystem.CloseOrderInfo(rows[0].ORDER_NO, function (result) {
            //    JeffComm.succAlert("关闭成功", "divMsg");
            //})
            var checkedItems = $('#grid').datagrid('getChecked');
            if (checkedItems.length > 0)
            {
                for(var i=0;i<checkedItems.length;i++)
                {
                    WsSystem.CloseOrderInfo(rows[i].ORDER_NO, function (result) {
                        //JeffComm.succAlert("关闭成功", "divMsg");
                    });
                }
                JeffComm.succAlert("关闭成功", "divMsg");
            }
            //实现刷新栏目中的数据
            $("#grid").datagrid("reload");
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsOrder.asmx/FindAllOrder',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                remoteSort: false,
                autoRowHeight: false,
                singleSelect: false,
                   pagination: true,
                rownumbers: true,
                  pageSize: 1000,
                  pageNumber: 1,
                  pageList: [1000, 1200, 3000],
                  beforePageText: '第',//页数文本框前显示的汉字   
                  afterPageText: '页    共 {pages} 页',
                  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                  columns: [[ //选择
                      { field: 'ck', checkbox: true },
                     { title: '订单单号', field: 'ORDER_NO', width: OrderCenter.fixWidth(0.08), sortable: true },
                      { title: '合同号', field: 'CONTRACT', width: OrderCenter.fixWidth(0.08) },
                     { title: '零件图号', field: 'PARTSDRAWING_CODE', width: OrderCenter.fixWidth(0.08), sortable: true },
                     { title: '订单状态', field: 'STATUS', width: OrderCenter.fixWidth(0.05),sortable:true },
                     { title: '客户名称', field: 'CUST_NAME', width: OrderCenter.fixWidth(0.06), sortable: true },
                    
                     //{ title: '交付数量', field: 'ASK_QUANTITY', width: OrderCenter.fixWidth(0.05) },
                     { title: '订单数量', field: 'ORDER_QUANTITY', width: OrderCenter.fixWidth(0.05) },
                     { title: '入库数量', field: 'IN_QUANTITY', width: OrderCenter.fixWidth(0.05) },
                     { title: '已发货数量', field: 'OUT_QUANTITY', width: OrderCenter.fixWidth(0.05) },
                    { title: '库存数量', field: 'STOCKQTY', width: OrderCenter.fixWidth(0.05) },
                     { title: '本次应发货数量', field: 'OUT_NOTICE_QTY', width: OrderCenter.fixWidth(0.05) },
                     { title: '本次已发货数量', field: 'THIS_OUT_QTY', width: OrderCenter.fixWidth(0.05) },
                     
                     //{ title: '质量编号', field: 'QUALITY_CODE', width: OrderCenter.fixWidth(0.06) },                     
                     { title: '批次号', field: 'BATCH_NUMBER', width: OrderCenter.fixWidth(0.06), sortable: true },
                     { title: '备注', field: 'MEMO1', width: OrderCenter.fixWidth(0.06) },
                     {
                         title: '交付日期', field: 'OUT_DATE', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: OrderCenter.fixWidth(0.09), sortable: true
                     },
                     { title: '创建人', field: 'CREATED_BY', width: OrderCenter.fixWidth(0.05) },
                     {
                         title: '时间', field: 'CREATED_DATE', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: OrderCenter.fixWidth(0.09), sortable: true
                     }
                ]],
                toolbar: [{
                    text: '合同号码 <input type="text" id="txtContractNo"/>'
                }, '-', {
                    text: '订单号码 <input type="text" id="txtOrderNo"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtPartCode"/>'
                }, '-', {
                    text: '订单状态 <select tabindex="-1" name="selOrderStatus" id="selOrderStatus"><option value="">请选择</option><option value="0">创建</option> <option value="1">发布</option><option value="2">发货通知</option><option value="3">关闭</option></select>'
                }, '-', {
                    text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间 <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        OrderCenter.bindGrid();
                    }
                },
                '-', {
                    id: 'btnClose',
                    text: '关闭',
                    iconCls: 'icon-close',
                    handler: function () {
                        OrderCenter.CloseOrder();//强制关闭订单
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
               

            });



        }

    };

}();