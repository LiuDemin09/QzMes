var Publish = function () {

    return {
        grid: null,
        OrderNo: null,
        Partsdrawing: null,
        startTime: null,
        endTime: null,
        OrderNoID:null,
        myArray:new Array(),
        init: function () {


        },
        bindGrid: function () {
            Publish.OrderNo = $("#txtOrderNo").val().trim();
            Publish.Partsdrawing = $("#txtPartCode").val().trim();
            Publish.startTime = $("#txtStartTime").val();
            Publish.endTime = $("#txtEndTime").val();

            $('#grid').datagrid('load', { orderNo: Publish.OrderNo, partsdrawing: Publish.Partsdrawing, startTime: Publish.startTime, endTime: Publish.endTime });

        },

        publishOrder: function () {

            var checkedItems = $('#grid').datagrid('getChecked');
            var names = [];
            $.each(checkedItems, function (index, item){

               // names.push(item.ORDER_NO + "," + item.PARTSDRAWING_CODE);
                names.push(item.ID);
                });

            //alert(names.join(";"));
            WsOrder.PublishOrder(names.join(";"),
                   function (result) {
                       if (result == "OK") {
                           JeffComm.alertSucc("保存成功", 500);
                           //alert("保存成功", "提示");
                           $("#grid").datagrid("reload");
                       } else {
                           alert(result, "提示");
                       }
                   }, function (err) {
                       // JeffComm.errorAlert(err.get_message(), "divMsg");
                       alert("保存失败:" + err.get_message(), "提示");

                   });
        },
        RejectOrder: function () {

            var checkedItems = $('#grid').datagrid('getChecked');
            var names = [];
            $.each(checkedItems, function (index, item) {

                // names.push(item.ORDER_NO + "," + item.PARTSDRAWING_CODE);
                names.push(item.ID);
            });

            //alert(names.join(";"));
            WsOrder.RejectOrder(names.join(";"),
                   function (result) {
                       if (result == "OK") {
                           JeffComm.alertSucc("保存成功", 500);
                          // alert("保存成功", "提示");
                           $("#grid").datagrid("reload");
                       } else {
                           alert(result, "提示");
                       }
                   }, function (err) {
                       // JeffComm.errorAlert(err.get_message(), "divMsg");
                       alert("保存失败:" + err.get_message(), "提示");

                   });
        },
    
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsOrder.asmx/FindAllPublishOrder',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
               // singleSelect: true,
                   pagination: true,
                rownumbers: true,
                  pageSize: 10,
                  pageNumber: 1,
                  pageList: [10, 20, 30, 40, 50],
                  beforePageText: '第',//页数文本框前显示的汉字   
                  afterPageText: '页    共 {pages} 页',
                  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                  columns: [[ //选择
                      { field: 'ck', checkbox: true },
                      { title: 'ID', field: 'ID', width: Publish.fixWidth(0.09), hidden: 'true' },
                     { title: '订单单号', field: 'ORDER_NO', width: Publish.fixWidth(0.08) },
                     { title: '零件图号', field: 'PARTSDRAWING_CODE', width: Publish.fixWidth(0.08) },
                     { title: '订单状态', field: 'STATUS', width: Publish.fixWidth(0.05) },
                     { title: '客户名称', field: 'CUST_NAME', width: Publish.fixWidth(0.06) },
                     { title: '客户代码', field: 'CUST_CODE', width: Publish.fixWidth(0.06) },
                     { title: '投产总数', field: 'ORDER_QUANTITY', width: Publish.fixWidth(0.05) },
                     { title: '质量编号', field: 'QUALITY_CODE', width: Publish.fixWidth(0.06) },
                     { title: '交付数量', field: 'ASK_QUANTITY', width: Publish.fixWidth(0.05) },
                     { title: '炉批号', field: 'BATCH_NUMBER', width: Publish.fixWidth(0.06) },
                     {
                         title: '交付日期', field: 'OUT_DATE', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         },  width: Publish.fixWidth(0.09) },
                     { title: '创建人', field: 'CREATED_BY', width: Publish.fixWidth(0.05) },
                     {
                         title: '时间', field: 'CREATED_DATE',formatter: function (value, row, index) {
                         if (value != null & value != "") {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();}
                         }, width: Publish.fixWidth(0.09)
                     }
                ]],
                toolbar: [{
                    text: '订单号码 <input type="text" id="txtOrderNo"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtPartCode"/>'
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
                        Publish.bindGrid();
                    }
                },
                '-', {
                    id: 'btnPublish',
                    text: '发布',
                    iconCls: 'icon-ok',
                    handler: function () {
                        Publish.publishOrder();
                    }
                },
               '-', {
                   id: 'btnReject',
                   text: '驳回',
                   iconCls: 'icon-remove',
                   handler: function () {
                       Publish.RejectOrder();
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
                        window.location.href = "OrderCenter.aspx";
                    }
                }],
              
               

            });



        }

    };

}();