var DrawingQuery = function () {

    return {
        grid: null,
        partCode: null,
        custCode: null,
        startTime: null,
        endTime: null,
        init: function () {
            //加载客户
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
            });

        },
        bindGrid: function () {
            DrawingQuery.partCode = $("#txtPartCode").val().trim();
            DrawingQuery.custCode = $("#selCustName").val().trim();
            DrawingQuery.startTime = $("#txtStartTime").val().trim();
            DrawingQuery.endTime = $("#txtEndTime").val().trim();
            var oDate1 = new Date(DrawingQuery.startTime);
            var oDate2 = new Date(DrawingQuery.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', { partCode: DrawingQuery.partCode, custCode: DrawingQuery.custCode, startTime: DrawingQuery.startTime, endTime: DrawingQuery.endTime });

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '图号查询',
                iconCls: 'icon-view',
                url: '../../services/WsOrder.asmx/FindDrawing',
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
                     { title: '零件图号', field: 'PartsCode', width: DrawingQuery.fixWidth(0.09) },
                     { title: '客户名称', field: 'CustName', width: DrawingQuery.fixWidth(0.08) },
                     { title: '客户代码', field: 'CustCode', width: DrawingQuery.fixWidth(0.08) },
                     { title: '产品名称', field: 'ProductName', width: DrawingQuery.fixWidth(0.08) },
                     //{ title: '投产数量', field: 'PlanQuantity', width: DrawingQuery.fixWidth(0.08) },
                     //{ title: '质量编号', field: 'QualityCode', width: DrawingQuery.fixWidth(0.08) },
                     //{ title: '交付数量', field: 'AskQuantity', width: DrawingQuery.fixWidth(0.07) },
                     //{ title: '炉批号', field: 'BatchNumber', width: DrawingQuery.fixWidth(0.08) },
                     //{
                     //    title: '交付时间', field: 'AskDate', formatter: function (value, row, index) {
                     //        var unixTimestamp = new Date(value);
                     //        return unixTimestamp.toLocaleString();
                     //    }, width: DrawingQuery.fixWidth(0.1)
                     //},
                     { title: '创建人', field: 'UpdatedBy', width: DrawingQuery.fixWidth(0.06) },
                     {
                         title: '时间', field: 'CreatedDate', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: DrawingQuery.fixWidth(0.1)
                     },
                ]],
                toolbar: [{
                    text: '零件图号 <input type="text" id="txtPartCode"/>'
                }, '-', {
                    text: '客户名称 <select tabindex="-1" name="selCustName" id="selCustName"><option value="0">请选择</option> </select>'
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
                        DrawingQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/OrderManage/export/PartsDrawingExport.aspx?starttime=' + $('#txtStartTime').val()
                   + '&endtime=' + $('#txtEndTime').val() + '&custcode=' + $('#selCustName').val() + '&partsdrawingno=' + $('#selCustName').val()
                   + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "BossQuery.aspx";;
                    }
                }],


            });



        }

    };

}();