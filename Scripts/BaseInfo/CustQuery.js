var CustQuery = function () {
    return {
        grid: null,
        custCode: null,
        startTime: null,
        endTime: null,
        delCustCode:null,
        init: function () {
        },
        bindGrid: function () {
            CustQuery.custCode = $("#selCustCode").val().trim();
            CustQuery.startTime = $("#txtStartTime").val().trim();
            CustQuery.endTime = $("#txtendTime").val().trim();
            var oDate1 = new Date(CustQuery.startTime);
            var oDate2 = new Date(CustQuery.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
               // alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', { custCode: CustQuery.custCode, startTime: CustQuery.startTime, endTime: CustQuery.endTime });

        },
      
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '客户查询',
                iconCls: 'icon-view',
                url: '../../services/WsBasic.asmx/FindCust',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                autoRowHeight: false,
                singleSelect: true,
                //   pagination: true,
                rownumbers: true,
                //  pageSize: 5,
                //  pageNumber: 1,
                //  pageList: [5, 20, 30, 40, 50],
                //  beforePageText: '第',//页数文本框前显示的汉字   
                //  afterPageText: '页    共 {pages} 页',
                //  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
               
                columns: [[ //选择
                     { title: '客户代码', field: 'CODE', width: CustQuery.fixWidth(0.06) },
                     { title: '客户名称', field: 'NAME', width: CustQuery.fixWidth(0.07) },
                     { title: '地址', field: 'ADDRESS', width: CustQuery.fixWidth(0.09) },
                     { title: '运输方式', field: 'TransType', width: CustQuery.fixWidth(0.07) },
                     { title: '收货地点', field: 'ReceiveArea', width: CustQuery.fixWidth(0.07) },
                     { title: '联系人', field: 'CONTACT', width: CustQuery.fixWidth(0.07) },
                     { title: '电话', field: 'MOBILE', width: CustQuery.fixWidth(0.07) },
                     { title: '传真', field: 'FAX', width: CustQuery.fixWidth(0.07) },
                     { title: '开票名称', field: 'InvoiceName', width: CustQuery.fixWidth(0.08) },
                     { title: '开票税号', field: 'InvoiceNumber', width: CustQuery.fixWidth(0.08) },
                     { title: '操作人', field: 'UpdatedBy', width: CustQuery.fixWidth(0.07) },
                     {
                         title: '时间', field: 'CreatedDate',formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: CustQuery.fixWidth(0.09)
                     }
                ]],
                toolbar: [{
                    text: '客户代码 <input type="text" id="selCustCode" />'
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
                        CustQuery.bindGrid();
                    }
                  }, '-', {
                      id: 'btnExpoet',
                      text: '导出',
                      iconCls: 'icon-save',
                      handler: function () {
                          window.open('../../Pages/BaseInfo/export/CustExport.aspx?startTime=' + $('#txtStartTime').val()
                      + '&endTime=' + $('#txtEndTime').val() + '&custCode=' + $('#selCustCode').val()
                      + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                      }
                  },'-',{
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