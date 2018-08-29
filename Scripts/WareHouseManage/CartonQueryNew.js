var CartonQueryNew = function () {

    return {
        cartonNo: null,
        startTime: null,
        endTime: null,
        init: function () {


        },
        bindGrid: function () {
            CartonQueryNew.cartonNo = $("#txtCartonNo").val();
            CartonQueryNew.startTime = $("#txtStartTime").val();
            CartonQueryNew.endTime = $("#txtEndTime").val();
            if (CartonQueryNew.startTime != null & CartonQueryNew.startTime != ""
                & CartonQueryNew.endTime != null & CartonQueryNew.endTime != ""
                & CartonQueryNew.startTime > CartonQueryNew.endTime) {
                JeffComm.alertErr("开始时间不能大于结束时间");
                //alert("开始时间不能大于结束时间", "提示");
            } else {
                $('#grid').datagrid('load', {
                    cartonNo: CartonQueryNew.cartonNo,
                    startTime: CartonQueryNew.startTime, endTime: CartonQueryNew.endTime
                });
            }

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '箱号查询',
                iconCls: 'icon-view',
                url: '../../services/WsWareHouse.asmx/ListCartonInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                //pageSize: 5,
                //pageNumber: 1,
                //pageList: [5, 20, 30, 40, 50],
                //beforePageText: '第',//页数文本框前显示的汉字   
                //afterPageText: '页    共 {pages} 页',
                //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '箱号', field: 'CSN', width: CartonQueryNew.fixWidth(0.12) },
                     { title: '订单号码', field: 'OrderNumber', width: CartonQueryNew.fixWidth(0.1) },
                     { title: '零件图号', field: 'PartsdrawingCode', width: CartonQueryNew.fixWidth(0.1) },
                     { title: '质量编号', field: 'QualityCode', width: CartonQueryNew.fixWidth(0.08) },
                     { title: '数量', field: 'QUANTITY', width: CartonQueryNew.fixWidth(0.08) },
                     { title: '操作人', field: 'UpdatedBy', width: CartonQueryNew.fixWidth(0.06) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: CartonQueryNew.fixWidth(0.1)
                     }
                ]],
                toolbar: [{
                    text: '箱号 <input type="text" id="txtCartonNo" />'
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
                        CartonQueryNew.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/WareHouseManage/export/CartonExport.aspx?sn=' + $('#txtCartonNo').val() + '&starttime=' + $('#txtStartTime').val()
                    + '&endtime=' + $('#txtEndTime').val() + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }],
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });


        }

    };

}();