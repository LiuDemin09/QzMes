var SendNotice = function () {

    return {
        grid: null,
        OrderNo: null,
        Partsdrawing: null,
        editIndex: undefined,
        init: function () {


        },
        bindGrid: function () {
            SendNotice.OrderNo = $("#txtOrderNo").val().trim();
            SendNotice.Partsdrawing = $("#txtPartCode").val().trim();

            $('#grid').datagrid('load', { orderNo: SendNotice.OrderNo, partsdrawing: SendNotice.Partsdrawing });

        },

        SendOrder: function () { 
            var checkedItems = $('#grid').datagrid('getChecked');
            var names = [];
            $.each(checkedItems, function (index, item) {
                // names.push(item.ORDER_NO + "," + item.PARTSDRAWING_CODE);
               // names.push(item.ID + "," + item.ASK_QUANTITY + "," + item.OUT_QUANTITY + "," + item.OUT_NOTICE_QTY + "," + item.OUT_NOTICE_TIME);
                names.push(item.ID + "," + item.STOCKQTY + "," + item.OUT_QUANTITY + "," + item.OUT_NOTICE_QTY + "," + item.OUT_NOTICE_TIME);
            });


            WsOrder.SendOrder(names.join(";"),
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
                       alert(err.get_message(), "提示");

                   });
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        endEditing: function (percent) {
            if (SendNotice.editIndex == undefined) { return true }
            if ($('#grid').datagrid('validateRow', SendNotice.editIndex)) {

                var rows = $('#grid').datagrid('getRows');
                var rowData = rows[SendNotice.editIndex];
                //结束编辑:如果在结束编辑之前，rowData还是编辑之前的数据，只有结束编辑之后，rowData才是编辑完成的数据
                $('#grid').datagrid('endEdit', SendNotice.editIndex);

                SendNotice.editIndex = undefined;
                return true;
            } else {
                return false;
            }
        },


        initGrid: function () { 
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
               
                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsOrder.asmx/FindAllSendOrder',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                autoRowHeight: false,
                // singleSelect: true,
                fit: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 400, 5000],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                    { field: 'ck', checkbox: true },
                    { title: 'ID', field: 'ID', width: SendNotice.fixWidth(0.09), hidden: 'true' },
                   { title: '订单单号', field: 'ORDER_NO', width: SendNotice.fixWidth(0.08) },
                   { title: '零件图号', field: 'PARTSDRAWING_CODE', width: SendNotice.fixWidth(0.08) },
                   { title: '订单状态', field: 'STATUS', width: SendNotice.fixWidth(0.05) },
                   { title: '客户名称', field: 'CUST_NAME', width: SendNotice.fixWidth(0.06) },
                   { title: '客户代码', field: 'CUST_CODE', width: SendNotice.fixWidth(0.06) },
                   { title: '投产总数', field: 'ORDER_QUANTITY', width: SendNotice.fixWidth(0.05) },
                   { title: '质量编号', field: 'QUALITY_CODE', width: SendNotice.fixWidth(0.06) },
                   { title: '交付数量', field: 'ASK_QUANTITY', width: SendNotice.fixWidth(0.05) },
                   { title: '发货数量', field: 'OUT_QUANTITY', width: SendNotice.fixWidth(0.05) },
                   { title: '库存数量', field: 'STOCKQTY', width: SendNotice.fixWidth(0.05) },
                   { title: '本次发货通知数量 ', field: 'OUT_NOTICE_QTY', editor: 'text', width: SendNotice.fixWidth(0.07) },
                   { title: '本次出货时间 ', field: 'OUT_NOTICE_TIME', editor: 'datetimebox', width: SendNotice.fixWidth(0.09) },
                   { title: '备注', field: 'BATCH_NUMBER', width: SendNotice.fixWidth(0.06) },
                   {
                       title: '交付日期', field: 'OUT_DATE', formatter: function (value, row, index) {
                           if (value != null & value != "") {
                           var unixTimestamp = new Date(value);
                           return unixTimestamp.toLocaleString();}
                       }, width: SendNotice.fixWidth(0.09)
                   },
                   { title: '创建人', field: 'CREATED_BY', width: SendNotice.fixWidth(0.05) },
                   {
                       title: '时间', field: 'CREATED_DATE', formatter: function (value, row, index) {
                           if (value != null & value != "") {
                           var unixTimestamp = new Date(value);
                           return unixTimestamp.toLocaleString();}
                       }, width: SendNotice.fixWidth(0.09)
                   }
                ]],

                toolbar: [{
                    text: '订单号码 <input type="text" id="txtOrderNo"/>'
                }, '-', {
                    text: '零件图号 <input type="text" id="txtPartCode"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        SendNotice.bindGrid();
                    }
                },
                '-', {
                    id: 'btnSend',
                    text: '发货',
                    iconCls: 'icon-save',
                    handler: function () {
                        SendNotice.SendOrder();
                    }
                },
                 '-', {
                     id: 'btnConfirm',
                     text: '确认',
                     iconCls: 'icon-ok',
                     handler: function () {
                         $("#grid").datagrid("acceptChanges");
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
                onClickRow: function (index, field, value) {
                    //field:如果事件为onDblClickCell，则field，为双击某个单元格的属性名，如'login_name'
                    if (SendNotice.editIndex != index) {
                        if (SendNotice.endEditing()) {
                            $('#grid').datagrid('selectRow', index).datagrid('beginEdit', index);
                            SendNotice.editIndex = index;
                        } else {
                            $('#grid').datagrid('selectRow', SendNotice.editIndex);
                        }
                    }

                    else SendNotice.editIndex = undefined;
                    var currentEdatagrid = $(this);
                    $('.datagrid-editable .textbox,.datagrid-editable .datagrid-editable-input,.datagrid-editable .textbox-text').bind('keydown', function (e) {
                        var code = e.keyCode || e.which;
                        if (code == 13) {
                            $(currentEdatagrid).datagrid('acceptChanges');
                            $(currentEdatagrid).datagrid('endEdit', index);
                        }
                    });
                },
                onAfterEdit: function (rowIndex, rowData, changes) {
                    editRow = undefined;
                }



            });


            $.extend($.fn.datagrid.defaults.editors, {
                datetimebox: {
                    init: function (container, options) {
                        var input = $('<input type="text" class="easyui-datetimebox">')
                          .appendTo(container);
                        //编辑框延迟加载
                        window.setTimeout(function () {
                            input.datetimebox($.extend({ editable: false }, options));
                        }, 10);
                        return input;
                    },
                    getValue: function (target) {
                        return $(target).datetimebox('getValue');
                    },
                    setValue: function (target, value) {
                        $(target).val(value);
                        window.setTimeout(function () {
                            $(target).datetimebox('setValue', value);
                        }, 150);
                    },
                    resize: function (target, width) {
                        var input = $(target);
                        if ($.boxModel == true) {
                            input.width(width - (input.outerWidth() - input.width()));
                        } else {
                            input.width(width);
                        }
                    }
                }
            });


        }

    };

}();