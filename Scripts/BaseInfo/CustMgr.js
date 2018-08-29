var CustMgr = function () {
    return {
        grid: null,
        custCode: null,
        startTime: null,
        endTime: null,
        delCustCode:null,
        init: function () {
            jQuery('#btnClose').click(function () {
                $('#restartDialog').window('close');
            });
            jQuery('#btnAddCust').click(function () {
                $('#restartDialog').window('open');
            });
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/BaseInfo/CustMgrDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/BaseInfo/CustMgrUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
                
            });
            jQuery('#btnNew').click(function () {
                $('#txtCustCode').val("");
                $('#txtCustName').val("");
                $('#txtAddress').val("");
                $('#txtTransType').val("");
                $('#txtReceiveArea').val("");
                $('#txtContact').val("");
                $('#txtMobile').val("");
                $('#txtFax').val("");
                $('#txtInvoiceName').val("");
                $('#txtInvoiceNumber').val("");
            });

            jQuery('#btnSave').click(function () {
                var custInfo = {
                    CODE: $("#txtCustCode").val().trim(),
                    NAME: $("#txtCustName").val().trim(),
                    ADDRESS: $("#txtAddress").val(),
                    TransType: $("#txtTransType").val(),
                    ReceiveArea: $("#txtReceiveArea").val(),
                    CONTACT: $("#txtContact").val(),
                    MOBILE: $("#txtMobile").val(),
                    FAX: $("#txtFax").val(),
                    InvoiceName: $("#txtInvoiceName").val(),
                    InvoiceNumber: $("#txtInvoiceNumber").val()
                };
                WsBasic.SaveCust(custInfo,
                    function (result) {
                        if (result == "OK") {
                            JeffComm.alertSucc("保存成功", 500);
                           // alert("保存成功", "提示");
                            //JeffComm.succAlert("Saved successfully.", "divMsg");
                            $('#txtCustCode').val("");
                            $('#txtCustName').val("");
                            $('#txtAddress').val("");
                            $('#txtTransType').val("");
                            $('#txtReceiveArea').val("");
                            $('#txtContact').val("");
                            $('#txtMobile').val("");
                            $('#txtFax').val("");
                            $('#txtInvoiceName').val("");
                            $('#txtInvoiceNumber').val("");
                            $("#grid").datagrid("reload");
                        } else {
                            alert(result, "提示");
                        }
                    }, function (err) {
                        alert("保存失败:" + err.get_message(), "提示");
                    });
            });
            jQuery('#btnClear').click(function () {
                $('#txtCustCode').val("");
                $('#txtCustName').val("");
                $('#txtAddress').val("");
                $('#txtTransType').val("");
                $('#txtReceiveArea').val("");
                $('#txtContact').val("");
                $('#txtMobile').val("");
                $('#txtFax').val("");
                $('#txtInvoiceName').val("");
                $('#txtInvoiceNumber').val("");
            });
        },
        bindGrid: function () {
            CustMgr.custCode = $("#selCustCode").val().trim();
            CustMgr.startTime = $("#txtStartTime").val().trim();
            CustMgr.endTime = $("#txtendTime").val().trim();
            var oDate1 = new Date(CustMgr.startTime);
            var oDate2 = new Date(CustMgr.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
                //alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', { custCode: CustMgr.custCode, startTime: CustMgr.startTime, endTime: CustMgr.endTime });
        },
        delCustInfo: function () {
            if (CustMgr.delCustCode == null || CustMgr.delCustCode == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
                //alert("请选择一条数据进行删除","提示");
            } else {
                WsBasic.DelCust(CustMgr.delCustCode,
                    function (result) {
                        if (result == "OK") {
                            JeffComm.alertSucc("删除成功", 500);
                           // alert("删除成功", "提示");
                            CustMgr.custCode = $("#selCustCode").val();
                            CustMgr.startTime = $("#txtStartTime").val();
                            CustMgr.endTime = $("#txtendTime").val();

                            $('#grid').datagrid('load', { custCode: CustMgr.custCode, startTime: CustMgr.startTime, endTime: CustMgr.endTime });
                        } else {

                            alert(result, "提示");
                            CustMgr.custCode = $("#selCustCode").val();
                            CustMgr.startTime = $("#txtStartTime").val();
                            CustMgr.endTime = $("#txtendTime").val();

                            $('#grid').datagrid('load', { custCode: CustMgr.custCode, startTime: CustMgr.startTime, endTime: CustMgr.endTime });
                        }
                    }, function (err) {
                        alert("保存失败:" + err.get_message(), "提示");
                    });
            }

            CustMgr.custCode = $("#selCustCode").val();
            CustMgr.startTime = $("#txtStartTime").val();
            CustMgr.endTime = $("#txtendTime").val();
            $('#grid').datagrid('load', { custCode: CustMgr.custCode, startTime: CustMgr.startTime, endTime: CustMgr.endTime });
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '客户维护',
                iconCls: 'icon-view',
                url: '../../services/WsBasic.asmx/FindCust',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                autoRowHeight: false,
                singleSelect: true,
                fit:true,
                //   pagination: true,
                rownumbers: true,
                //  pageSize: 5,
                //  pageNumber: 1,
                //  pageList: [5, 20, 30, 40, 50],
                //  beforePageText: '第',//页数文本框前显示的汉字   
                //  afterPageText: '页    共 {pages} 页',
                //  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
               
                columns: [[ //选择
                     { title: '客户代码', field: 'CODE', width: CustMgr.fixWidth(0.05) },
                     { title: '客户名称', field: 'NAME', width: CustMgr.fixWidth(0.07) },
                     { title: '地址', field: 'ADDRESS', width: CustMgr.fixWidth(0.09) },
                     { title: '运输方式', field: 'TransType', width: CustMgr.fixWidth(0.06) },
                     { title: '收货地点', field: 'ReceiveArea', width: CustMgr.fixWidth(0.06) },
                     { title: '联系人', field: 'CONTACT', width: CustMgr.fixWidth(0.05) },
                     { title: '电话', field: 'MOBILE', width: CustMgr.fixWidth(0.07) },
                     { title: '传真', field: 'FAX', width: CustMgr.fixWidth(0.07) },
                     { title: '开票名称', field: 'InvoiceName', width: CustMgr.fixWidth(0.08) },
                     { title: '开票税号', field: 'InvoiceNumber', width: CustMgr.fixWidth(0.07) },
                     { title: '操作人', field: 'UpdatedBy', width: CustMgr.fixWidth(0.07) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: CustMgr.fixWidth(0.08)
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
                        CustMgr.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建客户',
                    iconCls: 'icon-add',
                    handler: function () {
                        $("#restartDialog").dialog('open');  //实现添加记录的页面
                    }
                },
                //'-', {
                //    id: 'btnEdit',
                //    text: '修改',
                //    iconCls: 'icon-edit',
                //    handler: function () {
                //        ShowEditOrViewDialog();//实现修改记录的方法
                //    }
                //},
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        CustMgr.delCustInfo();//实现直接删除数据的方法
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
                    CustMgr.delCustCode = rowData["CODE"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
        }
    };
}();