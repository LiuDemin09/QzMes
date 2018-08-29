var MaterielMgr = function () {
    return {
        grid: null,
        custmaterielNo: null,
        qzmateriel: null,
        custmateriel:null,
        //main function to initiate the module
        init: function () {

            $('#txtBasQty').val("1");
            $('#txtQZMaterielNo').attr("disabled", "disabled");
            //初始化客户名称
            WsSystem.ListBindCustName(function (result) {
                JeffComm.fillSelect($("#selCustName"), result, true);
            });
            jQuery('#txtCustMaterielNo').keydown(function (e) {
                if (e.keyCode == 13) {
                    if ($("#txtCustMaterielNo").val().trim() == null || $("#txtCustMaterielNo").val().trim() == "") {
                        alert("客户料号不能为空", "提示");
                        $("#txtCustMaterielNo").focus();
                    } else {
                        $('#txtMaterielName').val($("#txtCustMaterielNo").val().trim());
                        $("#txtBasQty").focus();
                    }
                }
                
            });
            jQuery('#btnClose').click(function () {
                MaterielMgr.custmaterielNo = null;
                $('#txtCustMaterielNo').val("");
                $('#txtQZMaterielNo').val("");
                $('#txtEquipmentName').val("");
                $('#txtMaterielName').val("");
                $('#txtBasQty').val("1");
                $('#txtMemo').val("");
                $('#restartDialog').window('close');
            });
            jQuery('#btnNew').click(function () {
                MaterielMgr.custmaterielNo = null;
                WsSystem.GetBasBaseCode(function (res) {
                    $('#txtQZMaterielNo').val(res);
                });
                    $('#txtCustMaterielNo').val("");
                    $('#txtQZMaterielNo').val("");
                    $('#txtEquipmentName').val("");
                    $('#txtMaterielName').val("");
                    $('#txtBasQty').val("1");
                    $('#txtMemo').val("");                
            });           
            jQuery('#btnSave').click(function () {
                var materielinfo = {
                    CPARTNO: $("#txtCustMaterielNo").val().trim(),
                    QPARTNO: $("#txtQZMaterielNo").val().trim(),
                    NAME: $("#txtMaterielName").val().trim(),
                    CUSTOMER: $("#selCustName").find("option:selected").text(),
                    BasQty: $("#txtBasQty").val(),
                    MEMO: $("#txtMemo").val(),

                };
                WsSystem.SaveMaterielInfo(materielinfo,
                      function (result) {
                          if (result == null) {
                              JeffComm.alertSucc("保存成功", 500);
                             // alert("保存成功", "提示");
                              $('#txtCustMaterielNo').val("");
                              WsSystem.GetBasBaseCode(function (res) {
                                  $('#txtQZMaterielNo').val(res);
                              });
                              $('#txtEquipmentName').val("");
                              $('#txtMaterielName').val("");
                              $('#txtBasQty').val("1");
                              $('#txtMemo').val("");
                              $("#grid").datagrid("reload");
                          } else {
                              alert(result, "提示");
                          }
                      }, function (err) {
                          alert("保存失败:" + err.get_message(), "提示");
                      });
            });
            jQuery('#btnClear').click(function () {
                $('#txtCustMaterielNo').val("");
                $('#txtQZMaterielNo').val("");
                $('#txtEquipmentName').val("");
                $('#txtMaterielName').val("");
                $('#txtBasQty').val("1");
                $('#txtMemo').val("");
            });
            jQuery('#btnDownload').click(function () {
                window.open('../../Pages/BaseInfo/MaterielDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/BaseInfo/MaterielUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
            });
        },
        bindGrid: function () {
            MaterielMgr.qzmateriel = $("#txtQZMateriel").val();
            MaterielMgr.custmateriel = $("#txtCustMateriel").val();
            $('#grid').datagrid('load', { qzMateriel: MaterielMgr.qzmateriel, custMateriel: MaterielMgr.custmateriel });
        },
        delCustInfo: function () {
            if (MaterielMgr.custmaterielNo == null || MaterielMgr.custmaterielNo == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
                WsSystem.RemoveMaterielInfo(rows[0].QPARTNO,rows[0].CPARTNO,
                    function (result) {
                        if (result == null) {
                            MaterielMgr.custmaterielNo = null;
                            JeffComm.alertSucc("删除成功", 500);
                            //alert("删除成功", "提示");
                            MaterielMgr.qzmateriel = $("#txtQZMateriel").val();
                            MaterielMgr.custmateriel = $("#txtCustMateriel").val();
                            $('#grid').datagrid('load', { qzMateriel: MaterielMgr.qzmateriel, custMateriel: MaterielMgr.custmateriel });
                        } else {
                            alert(result, "提示");
                            MaterielMgr.qzmateriel = $("#txtQZMateriel").val();
                            MaterielMgr.custmateriel = $("#txtCustMateriel").val();
                            $('#grid').datagrid('load', { qzMateriel: MaterielMgr.qzmateriel, custMateriel: MaterielMgr.custmateriel });
                        }
                    }, function (err) {
                        alert("删除失败:" + err.get_message(), "提示");
                    });
            }
            MaterielMgr.qzmateriel = $("#txtQZMateriel").val();
            MaterielMgr.custmateriel = $("#txtCustMateriel").val();
            $('#grid').datagrid('load', { qzMateriel: MaterielMgr.qzmateriel, custMateriel: MaterielMgr.custmateriel });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            MaterielMgr.custmaterielNo = rows[0].CPARTNO;
            $('#txtCustMaterielNo').val(rows[0].CPARTNO);
            $('#txtQZMaterielNo').val(rows[0].QPARTNO);
            WsSystem.QueryCustomerCodeByCustomer(rows[0].CUSTOMER,
                function (result) {
                    $('#selCustName').val(result);
                });
            $("#txtMaterielName").val(rows[0].NAME);
            $("#txtBasQty").val(rows[0].BasQty);
            $("#txtMemo").val(rows[0].MEMO);
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '料号维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryMaterielInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                remoteSort: false,
                rownumbers: true,
                sortName: 'CreatedDate', //初始排序字段  
                sortOrder: 'desc', //初始排序方式  
                columns: [[ //选择
                     { title: '钦纵料号', field: 'QPARTNO', width: MaterielMgr.fixWidth(0.1) },
                     { title: '客户料号', field: 'CPARTNO', width: MaterielMgr.fixWidth(0.1) },
                     { title: '料号名称', field: 'NAME', width: MaterielMgr.fixWidth(0.1) },
                     { title: '基板数', field: 'BasQty', width: MaterielMgr.fixWidth(0.1) },
                     { title: '客户名称', field: 'CUSTOMER', width: MaterielMgr.fixWidth(0.1) },
                     { title: '备注', field: 'MEMO', width: MaterielMgr.fixWidth(0.1) },
                     { title: '操作人', field: 'UpdatedBy', width: MaterielMgr.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: MaterielMgr.fixWidth(0.1), sortable: true
                     },
                ]],
                toolbar: [{
                    text: '客户料号 <input type="text" id="txtCustMateriel"/>'
                }, '-', {
                    text: '钦纵料号 <input type="text" id="txtQZMateriel"/>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        MaterielMgr.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建料号',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建料号" });
                        $('#btnDownload').show();
                        $('#btnImport').show();
                        $('#btnNew').show();
                        $('#txtCustMaterielNo').removeAttr("disabled");
                        WsSystem.GetBasBaseCode(function (res) {
                            $('#txtQZMaterielNo').val(res);
                        });
                        $("#restartDialog").dialog('open');  //实现添加记录的页面
                    }
                },
                '-', {
                    id: 'btnEdit',
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            $('#restartDialog').panel({ title: "修改料号" });
                            $('#btnDownload').hide();
                            $('#btnImport').hide();
                            $('#btnNew').hide();
                            $('#txtCustMaterielNo').attr("disabled", "disabled");
                            MaterielMgr.ShowEditOrViewDialog();//实现修改记录的方法
                        }
                        else
                        {
                            JeffComm.alertErr("请选择一条数据进行修改");
                           // alert("请选择一条数据进行修改", "提示");
                        }
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        MaterielMgr.delCustInfo();//实现直接删除数据的方法
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
                        window.location.href = "../WareHouseManage/ReceiveMar.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    MaterielMgr.custmaterielNo = rowData["CPARTNO"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
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