var OrderQuery = function () {

    return {
        grid: null,
        OrderNo: null,
        Partsdrawing: null,
        startTime: null,
        endTime: null,
        orderStatus: null,
        init: function () { 
        },
        bindGrid: function () {
            OrderQuery.OrderNo = $("#txtOrderNo").val().trim();
            OrderQuery.Partsdrawing = $("#txtPartCode").val().trim();
            OrderQuery.orderStatus = $("#selOrderStatus").val();
            OrderQuery.startTime = $("#txtStartTime").val().trim();
            OrderQuery.endTime = $("#txtEndTime").val().trim();
            var oDate1 = new Date(OrderQuery.startTime);
            var oDate2 = new Date(OrderQuery.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', { orderNo: OrderQuery.OrderNo, partsdrawing: OrderQuery.Partsdrawing, startTime: OrderQuery.startTime, endTime: OrderQuery.endTime, OrderStatus: OrderCenter.orderStatus  });

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '订单查询',
                iconCls: 'icon-view',
                url: '../../services/WsOrder.asmx/FindAllOrder',
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
                     { title: '订单单号', field: 'ORDER_NO', width: OrderQuery.fixWidth(0.08) },
                     { title: '零件图号', field: 'PARTSDRAWING_CODE', width: OrderQuery.fixWidth(0.08) },
                     { title: '订单状态', field: 'STATUS', width: OrderQuery.fixWidth(0.05) },
                     { title: '客户名称', field: 'CUST_NAME', width: OrderQuery.fixWidth(0.06) },
                     { title: '客户代码', field: 'CUST_CODE', width: OrderQuery.fixWidth(0.06) },
                     { title: '投产总数', field: 'ORDER_QUANTITY', width: OrderQuery.fixWidth(0.05) },
                     { title: '质量编号', field: 'QUALITY_CODE', width: OrderQuery.fixWidth(0.06) },
                     { title: '交付数量', field: 'ASK_QUANTITY', width: OrderQuery.fixWidth(0.05) },
                     { title: '炉批号', field: 'BATCH_NUMBER', width: OrderQuery.fixWidth(0.06) },
                     {
                         title: '交付日期', field: 'OUT_DATE', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: OrderQuery.fixWidth(0.09)
                     },
                     { title: '创建人', field: 'CREATED_BY', width: OrderQuery.fixWidth(0.05) },
                     {
                         title: '时间', field: 'CREATED_DATE',formatter: function (value, row, index) {
                         if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: OrderQuery.fixWidth(0.09)
                     }
                ]],
                toolbar: [{
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
                         OrderQuery.bindGrid();
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
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "../../Pages/WorkOrderManage/BossQuery.aspx";
                    }
                }],


            });



        }

    };

}();